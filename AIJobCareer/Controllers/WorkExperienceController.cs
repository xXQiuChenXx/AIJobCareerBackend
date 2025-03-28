using AIJobCareer.Data;
using AIJobCareer.DTOs;
using AIJobCareer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AIJobCareer.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class WorkExperienceController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public WorkExperienceController(ApplicationDBContext context)
        {
            _context = context;
        }

        // Helper method to get current user's ID from token
        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                throw new UnauthorizedAccessException("Invalid user token");
            }
            return userId;
        }

        // GET: WorkExperience
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkExperienceResponseDto>>> GetWorkExperiences()
        {
            var userId = GetCurrentUserId();
            var workExperiences = await _context.Work_Experience.Where(p => p.user_id == userId).ToListAsync();

            var workExperienceDtos = workExperiences.Select(w => new WorkExperienceResponseDto
            {
                ExperienceId = w.experience_id,
                UserId = w.user_id,
                JobTitle = w.job_title,
                CompanyName = w.company_name,
                Location = w.location,
                StartDate = w.start_date,
                EndDate = w.end_date,
                IsCurrent = w.is_current,
                Description = w.description,
                ExperienceSkill = w.experience_skill,
                CreatedAt = w.created_at,
                UpdatedAt = w.updated_at
            }).ToList();

            return workExperienceDtos;
        }

        // GET: WorkExperience/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkExperienceResponseDto>> GetWorkExperience(Guid id)
        {
            var workExperience = await _context.Work_Experience.FindAsync(id);

            if (workExperience == null)
            {
                return NotFound();
            }

            var workExperienceDto = new WorkExperienceResponseDto
            {
                ExperienceId = workExperience.experience_id,
                UserId = workExperience.user_id,
                JobTitle = workExperience.job_title,
                CompanyName = workExperience.company_name,
                Location = workExperience.location,
                StartDate = workExperience.start_date,
                EndDate = workExperience.end_date,
                IsCurrent = workExperience.is_current,
                Description = workExperience.description,
                ExperienceSkill = workExperience.experience_skill,
                CreatedAt = workExperience.created_at,
                UpdatedAt = workExperience.updated_at
            };

            return workExperienceDto;
        }

        // GET: WorkExperience/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<WorkExperienceResponseDto>>> GetWorkExperiencesByUserId(Guid userId)
        {
            var workExperiences = await _context.Work_Experience
                .Where(e => e.user_id == userId)
                .ToListAsync();

            var workExperienceDtos = workExperiences.Select(w => new WorkExperienceResponseDto
            {
                ExperienceId = w.experience_id,
                UserId = w.user_id,
                JobTitle = w.job_title,
                CompanyName = w.company_name,
                Location = w.location,
                StartDate = w.start_date,
                EndDate = w.end_date,
                IsCurrent = w.is_current,
                Description = w.description,
                ExperienceSkill = w.experience_skill,
                CreatedAt = w.created_at,
                UpdatedAt = w.updated_at
            }).ToList();

            return workExperienceDtos;
        }

        // POST: WorkExperience
        [HttpPost]
        public async Task<ActionResult<WorkExperienceResponseDto>> CreateWorkExperience(WorkExperienceCreateDto workExperienceDto)
        {
            var userId = GetCurrentUserId();

            var workExperience = new WorkExperience
            {
                user_id = userId,
                job_title = workExperienceDto.JobTitle,
                company_name = workExperienceDto.CompanyName,
                location = workExperienceDto.Location,
                start_date = workExperienceDto.StartDate,
                end_date = workExperienceDto.EndDate,
                is_current = workExperienceDto.IsCurrent,
                description = workExperienceDto.Description,
                experience_skill = workExperienceDto.ExperienceSkill,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.Work_Experience.Add(workExperience);
            await _context.SaveChangesAsync();

            var responseDto = new WorkExperienceResponseDto
            {
                ExperienceId = workExperience.experience_id,
                UserId = workExperience.user_id,
                JobTitle = workExperience.job_title,
                CompanyName = workExperience.company_name,
                Location = workExperience.location,
                StartDate = workExperience.start_date,
                EndDate = workExperience.end_date,
                IsCurrent = workExperience.is_current,
                Description = workExperience.description,
                ExperienceSkill = workExperience.experience_skill,
                CreatedAt = workExperience.created_at,
                UpdatedAt = workExperience.updated_at
            };

            return CreatedAtAction(nameof(GetWorkExperience), new { id = workExperience.experience_id }, responseDto);
        }

        // PUT: WorkExperience/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkExperience(Guid id, WorkExperienceUpdateDto workExperienceDto)
        {

            var userId = GetCurrentUserId();

            var workExperience = await _context.Work_Experience.FindAsync(id);
            if (workExperience == null)
            {
                return NotFound();
            }

            if(workExperience.user_id != userId)
            {
                return Unauthorized();
            }

            // Update the entity with DTO values
            workExperience.job_title = workExperienceDto.JobTitle;
            workExperience.company_name = workExperienceDto.CompanyName;
            workExperience.location = workExperienceDto.Location;
            workExperience.start_date = workExperienceDto.StartDate;
            workExperience.end_date = workExperienceDto.EndDate;
            workExperience.is_current = workExperienceDto.IsCurrent;
            workExperience.description = workExperienceDto.Description;
            workExperience.experience_skill = workExperienceDto.ExperienceSkill;
            workExperience.updated_at = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkExperienceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: WorkExperience/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkExperience(Guid id)
        {

            Guid userId = GetCurrentUserId();

            var workExperience = await _context.Work_Experience.FindAsync(id);
            if (workExperience == null)
            {
                return NotFound();
            }

            if(workExperience.user_id != userId)
            {
                return Unauthorized();
            }

            _context.Work_Experience.Remove(workExperience);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkExperienceExists(Guid id)
        {
            return _context.Work_Experience.Any(e => e.experience_id == id);
        }
    }
}