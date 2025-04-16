using AIJobCareer.Models;
using AIJobCareer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace AIJobCareer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            (bool Success, string Message, User? User) result = await _authService.RegisterAsync(model);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            // Generate JWT token
            string? token = GenerateJwtToken(result.User);

            return Ok(new
            {
                message = result.Message,
                token = token,
                user = new
                {
                    userId = result.User.user_id,
                    username = result.User.username,
                    email = result.User.user_email,
                }
            });
        }

        [HttpPost("RegisterBusiness")]
        public async Task<IActionResult> RegisterBusinessUser([FromBody] BusinessRegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            (bool Success, string Message, User? User) result = await _authService.RegisterBusinessAsync(model);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            // Generate JWT token
            string? token = GenerateJwtToken(result.User);

            return Ok(new
            {
                message = result.Message,
                token = token,
                user = new
                {
                    userId = result.User.user_id,
                    username = result.User.username,
                    email = result.User.user_email,
                    user_company_id = result.User.user_company_id,
                }
            });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(model.username_or_email, model.Password);

            if (!result.Success)
            {
                return Unauthorized(new { message = result.Message });
            }

            // Generate JWT token
            string? token = GenerateJwtToken(result.User);

            return Ok(new
            {
                message = result.Message,
                token = token,
                user = new UserResponse
                {
                    userId = result.User.user_id.ToString(),
                    user_fullname = result.User.user_first_name + " " + result.User.user_last_name,
                    username = result.User.username,
                    email = result.User.user_email,
                    user_company_id = result.User.user_company_id,
                    user_icon = result.User.user_icon
                }
            });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? _configuration["Jwt:Secret"];

            if (string.IsNullOrEmpty(secret))
            {
                throw new InvalidOperationException("JWT secret is not configured.");
            }

            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.user_id.ToString()),
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.Email, user.user_email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Validates a JWT token and user information
        /// </summary>
        /// <param name="request">The token validation request containing JWT token and user information</param>
        /// <returns>Validation result with user details if valid</returns>
        [HttpPost("validate")]
        [SwaggerOperation(
            Summary = "Validate JWT token and user information",
            Description = "Validates the provided JWT token signature and checks if the embedded claims match the provided user information",
            OperationId = "ValidateToken",
            Tags = new[] { "Authentication" }
        )]
        [SwaggerResponse(200, "Token is valid and user information matches", typeof(TokenValidationResponse))]
        [SwaggerResponse(400, "Invalid request", typeof(ErrorResponse))]
        [SwaggerResponse(401, "Token is invalid or doesn't match user information", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public async Task<IActionResult> ValidateToken([FromBody] TokenValidationRequest request)
        {
            try
            {
                // Validate request
                if (request == null || string.IsNullOrEmpty(request.Token))
                {
                    return BadRequest(new ErrorResponse
                    {
                        IsValid = false,
                        Message = "Token is required"
                    });
                }

                // Initialize token handler
                var tokenHandler = new JwtSecurityTokenHandler();

                // Check if token is in valid format
                if (!tokenHandler.CanReadToken(request.Token))
                {
                    return BadRequest(new ErrorResponse
                    {
                        IsValid = false,
                        Message = "Invalid token format"
                    });
                }

                // Get secret key from environment variable or configuration
                var key = Encoding.ASCII.GetBytes(
                    Environment.GetEnvironmentVariable("JWT_SECRET") ??
                    _configuration["Jwt:Secret"]);

                // Set up validation parameters
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                // Validate token
                ClaimsPrincipal principal;
                try
                {
                    principal = tokenHandler.ValidateToken(request.Token, validationParameters, out SecurityToken validatedToken);
                }
                catch (SecurityTokenExpiredException)
                {
                    return Unauthorized(new ErrorResponse
                    {
                        IsValid = false,
                        Message = "Token has expired"
                    });
                }
                catch (Exception ex)
                {
                    return Unauthorized(new ErrorResponse
                    {
                        IsValid = false,
                        Message = "Invalid token signature",
                        Error = ex.Message
                    });
                }

                // Extract claims from token
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = principal.FindFirst(ClaimTypes.Name)?.Value;
                var email = principal.FindFirst(ClaimTypes.Email)?.Value;

                // Validate user information against token claims
                bool userInfoMatches = true;
                string mismatchField = null;

                // Validate user ID if provided
                if (!string.IsNullOrEmpty(request.UserId) && userId != request.UserId)
                {
                    userInfoMatches = false;
                    mismatchField = "User ID";
                }
                // Validate username if provided
                else if (!string.IsNullOrEmpty(request.Email) && email != request.Email)
                {
                    userInfoMatches = false;
                    mismatchField = "Email";
                }

                if (!userInfoMatches)
                {
                    return Unauthorized(new ErrorResponse
                    {
                        IsValid = false,
                        Message = $"{mismatchField} doesn't match the value in the token"
                    });
                }

                User? user = await _authService.ValidateAsync(userId, username);

                // Success - token is valid and matches user information
                return Ok(new TokenValidationResponse
                {
                    IsValid = true,
                    user = new UserResponse
                    {
                        userId = user.user_id.ToString(),
                        user_fullname = user.user_first_name + " " + user.user_last_name,
                        username = user.username,
                        email = user.user_email,
                        user_company_id = user.user_company_id,
                        user_icon = user.user_icon
                    }, 
                    ExpiresAt = GetExpirationDateFromToken(principal)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    IsValid = false,
                    Message = "An error occurred during validation",
                    Error = ex.Message
                });
            }
        }

        private DateTime? GetExpirationDateFromToken(ClaimsPrincipal principal)
        {
            var expClaim = principal.FindFirst(JwtRegisteredClaimNames.Exp);
            if (expClaim != null && long.TryParse(expClaim.Value, out long expUnix))
            {
                // Convert Unix timestamp to DateTime
                return DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
            }
            return null;
        }
    }

    /// <summary>
    /// Request model for token validation
    /// </summary>
    public class TokenValidationRequest
    {
        /// <summary>
        /// JWT token to validate
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// User ID to validate against token claims (optional)
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Email to validate against token claims (optional)
        /// </summary>
        public string Email { get; set; }
    }

    /// <summary>
    /// Response model for successful token validation
    /// </summary>
    public class TokenValidationResponse
    {
        /// <summary>
        /// Indicates if the token is valid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// User ID extracted from token
        /// </summary>
        public UserResponse user { get; set; }

        /// <summary>
        /// Token expiration date and time
        /// </summary>
        public DateTime? ExpiresAt { get; set; }
    }

    public class UserResponse
    {
        public string username { get; set; }
        public string user_fullname { get; set; }
        public string user_icon { get; set; }
        public string email { get; set; }
        public string userId { get; set; }
        public string user_company_id { get; set; }
    }

    /// <summary>
    /// Response model for validation errors
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Indicates if the token is valid (false for error responses)
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Additional error details (if available)
        /// </summary>
        public string Error { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string user_first_name { get; set; }
        [Required]
        public string user_last_name { get; set; }
        [Required]
        public string user_email { get; set; }
        [Required]
        public string user_password { get; set; }
        [Required]
        public string user_privacy_status { get; set; }
        [Required]
        public DateTime user_account_created_time { get; set; }
        [Required]
        public string user_role { get; set; }
    }


    public class LoginModel
    {
        [Required]
        public string username_or_email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    // View models for registration and updates
    public class BusinessRegistrationModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Age { get; set; }
        public int? UserAreaId { get; set; }

        // Company information
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyIntro { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyIndustry { get; set; }
        public int? CompanyAreaId { get; set; }
    }

    public class BusinessUpdateModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Age { get; set; }
        public string UserIntro { get; set; }
        public string UserIcon { get; set; }
        public int? UserAreaId { get; set; }

        // Company information
        public string CompanyName { get; set; }
        public string CompanyIntro { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyIndustry { get; set; }
        public int? CompanyAreaId { get; set; }
    }
}

