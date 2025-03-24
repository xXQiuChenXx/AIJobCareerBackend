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
    public class PublicationController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PublicationController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Publication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publication>>> GetPublications()
        {
            return await _context.Publication.ToListAsync();
        }

        // GET: api/Publication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publication>> GetPublication(Guid id)
        {
            var publication = await _context.Publication.FindAsync(id);

            if (publication == null)
            {
                return NotFound();
            }

            return publication;
        }

        // GET: api/Publication/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Publication>>> GetPublicationsByUserId(Guid userId)
        {
            return await _context.Publication
                .Where(p => p.user_id == userId)
                .ToListAsync();
        }

        // POST: api/Publication
        [HttpPost]
        public async Task<ActionResult<Publication>> CreatePublication(Publication publication)
        {
            publication.created_at = DateTime.UtcNow;
            publication.updated_at = DateTime.UtcNow;

            _context.Publication.Add(publication);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPublication), new { id = publication.publication_id }, publication);
        }

        // PUT: api/Publication/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublication(Guid id, Publication publication)
        {
            if (id != publication.publication_id)
            {
                return BadRequest();
            }

            publication.updated_at = DateTime.UtcNow;
            _context.Entry(publication).State = EntityState.Modified;
            _context.Entry(publication).Property(x => x.created_at).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicationExists(id))
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

        // DELETE: api/Publication/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublication(Guid id)
        {
            var publication = await _context.Publication.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }

            _context.Publication.Remove(publication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublicationExists(Guid id)
        {
            return _context.Publication.Any(p => p.publication_id == id);
        }
    }
}
