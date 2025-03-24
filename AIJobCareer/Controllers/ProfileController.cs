using AIJobCareer.Data;
using AIJobCareer.Models;
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

        public ProfileController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Profile/Complete
        [HttpGet("Complete")]
        public async Task<ActionResult<UserProfileCompleteDto>> GetCompleteProfile()
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized();
            }

            // Get user and all related information in a single query
            var user = await _context.User
                .Include(u => u.Area)
                .Include(u => u.WorkExperiences)
                .Include(u => u.Educations)
                .Include(u => u.Projects)
                .Include(u => u.Publications)
                .Include(u => u.Certifications)
                .Include(u => u.UserSkills)
                    .ThenInclude(us => us.Skill)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.user_id == userId);

            if (user == null)
            {
                return NotFound();
            }

            // Map to DTO to avoid password exposure and control the response structure
            var completeProfile = new UserProfileCompleteDto
            {
                Education = user.Educations.Select(e => new EducationDto
                {
                    education_id = e.education_id,
                    degree_name = e.degree_name,
                    institution_name = e.institution_name,
                    start_year = e.start_year,
                    end_year = e.end_year,
                    description = e.description
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

                Certifications = user.Certifications.Select(c => new CertificationDto
                {
                    certification_id = c.certification_id,
                    certification_name = c.certification_name,
                    issuing_organization = c.issuing_organization,
                    issue_date = c.issue_date,
                    expiry_date = c.expiry_date,
                    credential_id = c.credential_id,
                    credential_url = c.credential_url
                }).OrderByDescending(c => c.issue_date).ToList(),
            };

            return completeProfile;
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
        public string full_name { get; set; }
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

    public class EducationDto
    {
        public Guid education_id { get; set; }
        public string degree_name { get; set; }
        public string institution_name { get; set; }
        public int start_year { get; set; }
        public int? end_year { get; set; }
        public string description { get; set; }
    }

    public class ProjectDto
    {
        public Guid project_id { get; set; }
        public string project_name { get; set; }
        public int project_year { get; set; }
        public string description { get; set; }
        public string project_url { get; set; }
    }

    public class PublicationDto
    {
        public Guid publication_id { get; set; }
        public string publication_title { get; set; }
        public string publisher { get; set; }
        public int publication_year { get; set; }
        public string publication_url { get; set; }
        public string description { get; set; }
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
