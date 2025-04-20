using AIJobCareer.Data;
using AIJobCareer.Models;
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
    public class EducationController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public EducationController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Education/user
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<EducationDto>>> GetEducations()
        {
            Guid userId = GetCurrentUserId();

            List<EducationDto> educations = await _context.Education
                .Where(e => e.user_id == userId)
                .Select(e => MapToDto(e))
                .ToListAsync();


            return educations;
        }

        // GET: api/Education/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<EducationDto>>> GetEducations(Guid userId)
        {
            var educations = await _context.Education
                .Where(e => e.user_id == userId)
                .Select(e => MapToDto(e))
                .ToListAsync();

            return educations;
        }

        // GET: api/Education/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationDto>> GetEducation(Guid id)
        {
            var education = await _context.Education.FindAsync(id);

            if (education == null)
            {
                return NotFound();
            }

            return MapToDto(education);
        }

        // POST: api/Education
        [HttpPost]
        public async Task<ActionResult<EducationDto>> CreateEducation(EducationCreateDto educationDto)
        {
            var userId = GetCurrentUserId();

            var education = new Education
            {
                user_id = userId,
                degree_name = educationDto.degree_name,
                institution_name = educationDto.institution_name,
                start_year = educationDto.start_year,
                end_year = educationDto.end_year,
                description = educationDto.description,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.Education.Add(education);
            await _context.SaveChangesAsync();

            var resultDto = MapToDto(education);

            return CreatedAtAction(nameof(GetEducation), new { id = resultDto.education_id }, resultDto);
        }

        // PUT: api/Education/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEducation(Guid id, EducationUpdateDto educationDto)
        {
            var education = await _context.Education.FindAsync(id);

            if (education == null)
            {
                return NotFound();
            }

            // Verify ownership
            var currentUserId = GetCurrentUserId();
            if (education.user_id != currentUserId)
            {
                return Forbid();
            }

            // Update fields
            education.degree_name = educationDto.degree_name;
            education.institution_name = educationDto.institution_name;
            education.start_year = educationDto.start_year;
            education.end_year = educationDto.end_year;
            education.description = educationDto.description;
            education.updated_at = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationExists(id))
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

        // DELETE: api/Education/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(Guid id)
        {
            var education = await _context.Education.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }

            // Verify ownership
            var currentUserId = GetCurrentUserId();
            if (education.user_id != currentUserId)
            {
                return Forbid();
            }

            _context.Education.Remove(education);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EducationExists(Guid id)
        {
            return _context.Education.Any(e => e.education_id == id);
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userIdClaim, out Guid userId))
            {
                return userId;
            }
            return Guid.Empty;
        }

        private static EducationDto MapToDto(Education education)
        {
            return new EducationDto
            {
                education_id = education.education_id,
                user_id = education.user_id,
                degree_name = education.degree_name,
                institution_name = education.institution_name,
                start_year = education.start_year,
                end_year = education.end_year,
                description = education.description,
                created_at = education.created_at,
                updated_at = education.updated_at
            };
        }
    }
}
