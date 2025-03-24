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
    public class WorkExperienceController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public WorkExperienceController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/WorkExperience
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkExperience>>> GetWorkExperiences()
        {
            return await _context.Work_Experience.ToListAsync();
        }

        // GET: api/WorkExperience/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkExperience>> GetWorkExperience(Guid id)
        {
            var workExperience = await _context.Work_Experience.FindAsync(id);

            if (workExperience == null)
            {
                return NotFound();
            }

            return workExperience;
        }

        // GET: api/WorkExperience/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<WorkExperience>>> GetWorkExperiencesByUserId(Guid userId)
        {
            return await _context.Work_Experience
                .Where(e => e.user_id == userId)
                .ToListAsync();
        }

        // POST: api/WorkExperience
        [HttpPost]
        public async Task<ActionResult<WorkExperience>> CreateWorkExperience(WorkExperience workExperience)
        {
            workExperience.created_at = DateTime.UtcNow;
            workExperience.updated_at = DateTime.UtcNow;

            _context.Work_Experience.Add(workExperience);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkExperience), new { id = workExperience.experience_id }, workExperience);
        }

        // PUT: api/WorkExperience/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkExperience(Guid id, WorkExperience workExperience)
        {
            if (id != workExperience.experience_id)
            {
                return BadRequest();
            }

            workExperience.updated_at = DateTime.UtcNow;
            _context.Entry(workExperience).State = EntityState.Modified;
            _context.Entry(workExperience).Property(x => x.created_at).IsModified = false;

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

        // DELETE: api/WorkExperience/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkExperience(Guid id)
        {
            var workExperience = await _context.Work_Experience.FindAsync(id);
            if (workExperience == null)
            {
                return NotFound();
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
