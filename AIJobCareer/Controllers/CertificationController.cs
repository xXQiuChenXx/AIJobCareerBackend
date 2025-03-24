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
    public class CertificationController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CertificationController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Certification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certification>>> GetCertifications()
        {
            return await _context.Certification.ToListAsync();
        }

        // GET: api/Certification/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Certification>> GetCertification(Guid id)
        {
            var certification = await _context.Certification.FindAsync(id);

            if (certification == null)
            {
                return NotFound();
            }

            return certification;
        }

        // GET: api/Certification/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Certification>>> GetCertificationsByUserId(Guid userId)
        {
            return await _context.Certification
                .Where(c => c.user_id == userId)
                .ToListAsync();
        }

        // POST: api/Certification
        [HttpPost]
        public async Task<ActionResult<Certification>> CreateCertification(Certification certification)
        {
            certification.created_at = DateTime.UtcNow;
            certification.updated_at = DateTime.UtcNow;

            _context.Certification.Add(certification);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCertification), new { id = certification.certification_id }, certification);
        }

        // PUT: api/Certification/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCertification(Guid id, Certification certification)
        {
            if (id != certification.certification_id)
            {
                return BadRequest();
            }

            certification.updated_at = DateTime.UtcNow;
            _context.Entry(certification).State = EntityState.Modified;
            _context.Entry(certification).Property(x => x.created_at).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificationExists(id))
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

        // DELETE: api/Certification/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertification(Guid id)
        {
            var certification = await _context.Certification.FindAsync(id);
            if (certification == null)
            {
                return NotFound();
            }

            _context.Certification.Remove(certification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CertificationExists(Guid id)
        {
            return _context.Certification.Any(c => c.certification_id == id);
        }
    }
}
