using AIJobCareer.Models;
using AIJobCareer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

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
            var token = GenerateJwtToken(result.User);

            return Ok(new
            {
                message = result.Message,
                token = token,
                user = new
                {
                    userId = result.User.user_id,
                    username = result.User.username,
                    email = result.User.user_email
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

            var result = await _authService.LoginAsync(model.UsernameOrEmail, model.Password);

            if (!result.Success)
            {
                return Unauthorized(new { message = result.Message });
            }

            // Generate JWT token
            var token = GenerateJwtToken(result.User);

            return Ok(new
            {
                message = result.Message,
                token = token,
                user = new
                {
                    userId = result.User.user_id,
                    username = result.User.username,
                    email = result.User.user_email
                }
            });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

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
        public string UsernameOrEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
