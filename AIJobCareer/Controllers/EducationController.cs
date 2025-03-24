using AIJobCareer.Data;
using AIJobCareer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIJobCareer.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public EducationController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Education
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Education>>> GetEducations()
        {
            return await _context.Education.ToListAsync();
        }

        // GET: api/Education/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Education>> GetEducation(Guid id)
        {
            var education = await _context.Education.FindAsync(id);

            if (education == null)
            {
                return NotFound();
            }

            return education;
        }

        // GET: api/Education/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Education>>> GetEducationsByUserId(Guid userId)
        {
            return await _context.Education
                .Where(e => e.user_id == userId)
                .ToListAsync();
        }

        // POST: api/Education
        [HttpPost]
        public async Task<ActionResult<Education>> CreateEducation(Education education)
        {
            education.created_at = DateTime.UtcNow;
            education.updated_at = DateTime.UtcNow;

            _context.Education.Add(education);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEducation), new { id = education.education_id }, education);
        }

        // PUT: api/Education/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEducation(Guid id, Education education)
        {
            if (id != education.education_id)
            {
                return BadRequest();
            }

            education.updated_at = DateTime.UtcNow;
            _context.Entry(education).State = EntityState.Modified;
            _context.Entry(education).Property(x => x.created_at).IsModified = false;

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

            _context.Education.Remove(education);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EducationExists(Guid id)
        {
            return _context.Education.Any(e => e.education_id == id);
        }
    }
}
