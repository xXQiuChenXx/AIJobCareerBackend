using AIJobCareer.Data;
using AIJobCareer.DTOs.Publication;
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
    public class PublicationController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PublicationController(ApplicationDBContext context)
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

        // GET: api/Publication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicationDto>>> GetPublications()
        {
            Guid userId = GetCurrentUserId();
            List<Publication> publications = await _context.Publication
                .Where(p => p.user_id == userId)
                .ToListAsync();
            return publications.Select(p => new PublicationDto
            {
                publication_id = p.publication_id,
                user_id = p.user_id,
                publication_title = p.publication_title,
                publisher = p.publisher,
                publication_year = p.publication_year,
                publication_url = p.publication_url,
                description = p.description,
                created_at = p.created_at,
                updated_at = p.updated_at
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublicationDto>> GetPublication(Guid id)
        {
            var userId = GetCurrentUserId();
            var publication = await _context.Publication.FindAsync(id);

            if (publication == null)
            {
                return NotFound();
            }

            if(publication.user_id != userId)
            {
                return Unauthorized();
            }

            return new PublicationDto
            {
                publication_id = publication.publication_id,
                user_id = publication.user_id,
                publication_title = publication.publication_title,
                publisher = publication.publisher,
                publication_year = publication.publication_year,
                publication_url = publication.publication_url,
                description = publication.description,
                created_at = publication.created_at,
                updated_at = publication.updated_at
            };
        }

        // Update the POST method to accept CreatePublicationDto
        [HttpPost]
        public async Task<ActionResult<PublicationDto>> CreatePublication(CreatePublicationDto dto)
        {
            Guid userId = GetCurrentUserId();

            var publication = new Publication
            {
                user_id = userId,
                publication_title = dto.publication_title,
                publisher = dto.publisher,
                publication_year = dto.publication_year,
                publication_url = dto.publication_url,
                description = dto.description,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.Publication.Add(publication);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPublication), new { id = publication.publication_id },
                new PublicationDto
                {
                    publication_id = publication.publication_id,
                    user_id = publication.user_id,
                    publication_title = publication.publication_title,
                    publisher = publication.publisher,
                    publication_year = publication.publication_year,
                    publication_url = publication.publication_url,
                    description = publication.description,
                    created_at = publication.created_at,
                    updated_at = publication.updated_at
                });
        }

        // Update the PUT method to accept UpdatePublicationDto
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublication(Guid id, UpdatePublicationDto dto)
        {
            var userId = GetCurrentUserId();

            var publication = await _context.Publication.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }

            if(publication.user_id != userId)
            {
                return Unauthorized();
            }

            publication.publication_title = dto.publication_title;
            publication.publisher = dto.publisher;
            publication.publication_year = dto.publication_year;
            publication.publication_url = dto.publication_url;
            publication.description = dto.description;
            publication.updated_at = DateTime.UtcNow;

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

        // DELETE: /Publication/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublication(Guid id)
        {
            Guid userId = GetCurrentUserId();

            Publication? publication = await _context.Publication.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }

            // Verify the project belongs to the current user
            if (publication.user_id != userId)
            {
                return Forbid();
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
