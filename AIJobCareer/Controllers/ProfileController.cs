using AIJobCareer.Data;
using AIJobCareer.DTOs.Publication;
using AIJobCareer.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AIJobCareer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ApplicationDBContext context, ILogger<ProfileController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Updates a user's profile information
        /// </summary>
        /// <param name="updateDto">The profile data to update</param>
        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDto updateDto)
        {
            Guid userId = GetCurrentUserId();

            var user = await _context.User.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Update only the allowed fields
            user.user_first_name = updateDto.user_first_name;
            user.user_last_name = updateDto.user_last_name;
            user.user_age = updateDto.user_age;
            user.user_intro = updateDto.user_intro;
            user.user_contact_number = updateDto.user_contact_number;
            user.user_email = updateDto.user_email;
            user.user_icon = updateDto.user_icon;
            user.user_privacy_status = updateDto.privacy_status;

            // Update area if area_name is provided
            if (!string.IsNullOrEmpty(updateDto.area_name))
            {
                // Look up area by name
                var area = await _context.Area.FirstOrDefaultAsync(a => a.area_name == updateDto.area_name);

                if (area == null)
                {
                    return BadRequest(new { message = $"Area '{updateDto.area_name}' not found. Please choose a valid area." });
                }

                user.user_area_id = area.area_id;
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Profile updated successfully" });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { message = "Failed to update profile", error = ex.InnerException?.Message ?? ex.Message });
            }
        }

        // POST: Profile/Complete
        [HttpPost("Complete/{username}")]
        public async Task<ActionResult> GetCompleteProfile(string username)
        {
            Guid current_user_id = GetCurrentUserId();

            // Get user and all related information in a single query
            var user = await _context.User
                .Include(u => u.Area)
                .Include(u => u.WorkExperiences)
                .Include(u => u.Educations)
                .Include(u => u.Projects)
                .Include(u => u.Publications)
                .Include(u => u.UserSkills)
                    .ThenInclude(us => us.Skill)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.username == username);

            if (user == null)
            {
                return NotFound();
            }

            if (user.user_privacy_status == "private" && user.user_id != current_user_id)
            {
                return Ok(new
                {
                    first_name = user.user_first_name,
                    last_name = user.user_last_name,
                    icon = user.user_icon,
                    privacy_status = user.user_privacy_status,
                    role = user.user_role,
                });
            }

            // Map to DTO to avoid password exposure and control the response structure
            var completeProfile = new UserProfileCompleteDto
            {
                BasicInfo = new UserBasicInfoDto
                {
                    user_id = user.user_id,
                    username = user.username,
                    first_name = user.user_first_name,
                    last_name = user.user_last_name,
                    email = user.user_email,
                    age = user.user_age,
                    intro = user.user_intro,
                    contact_number = user.user_contact_number,
                    icon = user.user_icon,
                    privacy_status = user.user_privacy_status,
                    role = user.user_role,
                    account_created_time = user.user_account_created_time,
                    last_login_at = user.last_login_at,
                    location = user.Area?.area_name,
                },

                // Add WorkExperiences mapping
                WorkExperiences = user.WorkExperiences.Select(w => new WorkExperienceDto
                {
                    experience_id = w.experience_id,
                    job_title = w.job_title,
                    company_name = w.company_name,
                    location = w.location,
                    start_date = w.start_date,
                    end_date = w.end_date,
                    is_current = w.is_current,
                    description = w.description,
                    experience_skill = w.experience_skill
                }).OrderByDescending(w => w.start_date).ToList(),

                Education = user.Educations.Select(e => new EducationDto
                {
                    education_id = e.education_id,
                    degree_name = e.degree_name,
                    institution_name = e.institution_name,
                    start_year = e.start_year,
                    end_year = e.end_year,
                    description = e.description,
                    user_id = user.user_id,
                    created_at = e.created_at,
                    updated_at = e.updated_at
                }).OrderByDescending(e => e.start_year).ToList(),

                Projects = user.Projects.Select(p => new ProjectDto
                {
                    project_id = p.project_id,
                    project_name = p.project_name,
                    project_year = p.project_year,
                    description = p.description,
                    project_url = p.project_url
                }).OrderByDescending(p => p.project_year).ToList(),

                Publications = user.Publications.Select(p => new PublicationDto
                {
                    publication_id = p.publication_id,
                    publication_title = p.publication_title,
                    publisher = p.publisher,
                    publication_year = p.publication_year,
                    publication_url = p.publication_url,
                    description = p.description
                }).OrderByDescending(p => p.publication_year).ToList(),

                Skills = user.UserSkills.Select(us => new SkillDto
                {
                    skill_id = us.US_SKILL_ID,
                    skill_name = us.Skill.skill_name,
                    skill_level = us.Skill.skill_level,
                }).OrderByDescending(s => s.skill_level).ToList(),
            };

            return Ok(completeProfile);
        }

        // Helper methods
        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }
            return Guid.Empty;
        }
    }

    // DTOs for complete profile data
    public class UserProfileCompleteDto
    {
        public UserBasicInfoDto BasicInfo { get; set; }
        public List<WorkExperienceDto> WorkExperiences { get; set; } = new List<WorkExperienceDto>();
        public List<EducationDto> Education { get; set; } = new List<EducationDto>();
        public List<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
        public List<PublicationDto> Publications { get; set; } = new List<PublicationDto>();
        public List<SkillDto> Skills { get; set; } = new List<SkillDto>();
        public List<CertificationDto> Certifications { get; set; } = new List<CertificationDto>();

    }

    public class UserBasicInfoDto
    {
        public Guid user_id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public int? age { get; set; }
        public string intro { get; set; }
        public string contact_number { get; set; }
        public string icon { get; set; }
        public string privacy_status { get; set; }
        public string role { get; set; }
        public DateTime account_created_time { get; set; }
        public DateTime? last_login_at { get; set; }
        public string location { get; set; }
    }

    public class WorkExperienceDto
    {
        public Guid experience_id { get; set; }
        public string job_title { get; set; }
        public string company_name { get; set; }
        public string location { get; set; }
        public DateTime start_date { get; set; }
        public DateTime? end_date { get; set; }
        public bool is_current { get; set; }
        public string description { get; set; }
        public string experience_skill { get; set; }
    }

    public class ProjectDto
    {
        public Guid project_id { get; set; }
        public string project_name { get; set; }
        public int project_year { get; set; }
        public string description { get; set; }
        public string project_url { get; set; }
    }

    public class SkillDto
    {
        public int skill_id { get; set; }
        public string skill_name { get; set; }
        public string skill_level { get; set; }
    }

    public class CertificationDto
    {
        public Guid certification_id { get; set; }
        public string certification_name { get; set; }
        public string issuing_organization { get; set; }
        public DateTime issue_date { get; set; }
        public DateTime? expiry_date { get; set; }
        public string credential_id { get; set; }
        public string credential_url { get; set; }
    }

    // DTOs for updating profile data
    public class UserProfileUpdateDto
    {
        public UserBasicInfoUpdateDto basic_info { get; set; }
        public List<WorkExperienceDto> work_experiences { get; set; }
        public List<EducationDto> education { get; set; }
        public List<ProjectDto> projects { get; set; }
        public List<PublicationDto> publications { get; set; }
    }

    public class UserBasicInfoUpdateDto
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string contact_number { get; set; }
        public string intro { get; set; }
        public int? age { get; set; }
        public string privacy_status { get; set; }
        public int? area_id { get; set; }
        public string icon { get; set; }
    }
}
