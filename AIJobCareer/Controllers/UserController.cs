using AIJobCareer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AIJobCareer.Data;

namespace AIJobCareer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserController(ApplicationDBContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // POST: api/User/RegisterBusiness
        [HttpPost("RegisterBusiness")]
        public async Task<IActionResult> RegisterBusinessUser([FromBody] BusinessRegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Check if username or email already exists
                if (await _context.User.AnyAsync(u => u.username == model.Username))
                {
                    return BadRequest("Username already exists");
                }

                if (await _context.User.AnyAsync(u => u.user_email == model.Email))
                {
                    return BadRequest("Email already exists");
                }

                // Create company first
                var company = new Company
                {
                    company_id = model.CompanyId,
                    company_name = model.CompanyName,
                    company_intro = model.CompanyIntro ?? string.Empty,
                    company_website = model.CompanyWebsite ?? string.Empty,
                    company_industry = model.CompanyIndustry ?? string.Empty,
                    company_icon = model.CompanyIcon ?? string.Empty,
                    company_area_id = model.CompanyAreaId
                };

                _context.Company.Add(company);
                await _context.SaveChangesAsync();

                // Create user with reference to the new company
                var user = new User
                {
                    username = model.Username,
                    user_first_name = model.FirstName,
                    user_last_name = model.LastName,
                    user_email = model.Email,
                    user_role = "business", // Set role as business
                    user_company_id = company.company_id,
                    user_area_id = model.UserAreaId ?? model.CompanyAreaId, // Use company area if user area not provided
                    user_contact_number = model.ContactNumber,
                    user_age = model.Age,
                    user_privacy_status = model.PrivacyStatus ?? "public"
                };

                // Hash the password
                user.user_password = _passwordHasher.HashPassword(user, model.Password);

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                // Strip password before returning
                user.user_password = null;

                return CreatedAtAction(nameof(GetUser), new { id = user.user_id }, new
                {
                    User = user,
                    Company = company
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _context.User
                .Include(u => u.Area)
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.user_id == id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Don't return password
            user.user_password = null;

            return Ok(user);
        }

        // PUT: api/User/UpdateBusinessUser/{id}
        [HttpPut("UpdateBusinessUser/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBusinessUser(Guid id, [FromBody] BusinessUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the current user
            var user = await _context.User
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.user_id == id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Check if the current user is authorized to update this user
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != id.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid("You don't have permission to update this user");
            }

            // Check if user is a business user
            if (user.user_role != "business")
            {
                return BadRequest("This endpoint is only for business users");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Update user properties
                if (!string.IsNullOrEmpty(model.Username) && model.Username != user.username)
                {
                    // Check if new username is unique
                    if (await _context.User.AnyAsync(u => u.username == model.Username && u.user_id != id))
                    {
                        return BadRequest("Username already exists");
                    }
                    user.username = model.Username;
                }

                if (!string.IsNullOrEmpty(model.Email) && model.Email != user.user_email)
                {
                    // Check if new email is unique
                    if (await _context.User.AnyAsync(u => u.user_email == model.Email && u.user_id != id))
                    {
                        return BadRequest("Email already exists");
                    }
                    user.user_email = model.Email;
                }

                // Update other user properties
                if (!string.IsNullOrEmpty(model.FirstName))
                    user.user_first_name = model.FirstName;

                if (!string.IsNullOrEmpty(model.LastName))
                    user.user_last_name = model.LastName;

                if (model.Age.HasValue)
                    user.user_age = model.Age;

                if (model.UserAreaId.HasValue)
                    user.user_area_id = model.UserAreaId;

                if (!string.IsNullOrEmpty(model.UserIntro))
                    user.user_intro = model.UserIntro;

                if (!string.IsNullOrEmpty(model.ContactNumber))
                    user.user_contact_number = model.ContactNumber;

                if (!string.IsNullOrEmpty(model.UserIcon))
                    user.user_icon = model.UserIcon;

                if (!string.IsNullOrEmpty(model.PrivacyStatus))
                    user.user_privacy_status = model.PrivacyStatus;

                if (!string.IsNullOrEmpty(model.Password))
                    user.user_password = _passwordHasher.HashPassword(user, model.Password);

                // Update company if user has a company
                if (user.Company != null && !string.IsNullOrEmpty(user.user_company_id))
                {
                    var company = await _context.Company.FindAsync(user.user_company_id);

                    if (company != null)
                    {
                        if (!string.IsNullOrEmpty(model.CompanyName))
                            company.company_name = model.CompanyName;

                        if (!string.IsNullOrEmpty(model.CompanyIntro))
                            company.company_intro = model.CompanyIntro;

                        if (!string.IsNullOrEmpty(model.CompanyWebsite))
                            company.company_website = model.CompanyWebsite;

                        if (!string.IsNullOrEmpty(model.CompanyIndustry))
                            company.company_industry = model.CompanyIndustry;


                        if (model.CompanyAreaId.HasValue)
                            company.company_area_id = model.CompanyAreaId;

                        _context.Entry(company).State = EntityState.Modified;
                    }
                }

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Don't return password
                user.user_password = null;

                return Ok(new
                {
                    User = user,
                    Company = user.Company
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
       
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
        public string ContactNumber { get; set; }
        public string PrivacyStatus { get; set; }
        public int? UserAreaId { get; set; }

        // Company information
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyIntro { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyIndustry { get; set; }
        public string CompanyIcon { get; set; }
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
        public string ContactNumber { get; set; }
        public string UserIcon { get; set; }
        public string PrivacyStatus { get; set; }
        public int? UserAreaId { get; set; }

        // Company information
        public string CompanyName { get; set; }
        public string CompanyIntro { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyIndustry { get; set; }
        public int? CompanyAreaId { get; set; }
    }
}