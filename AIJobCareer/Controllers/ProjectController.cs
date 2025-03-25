using AIJobCareer.Data;
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
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ProjectController(ApplicationDBContext context)
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

        // GET: /Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var userId = GetCurrentUserId();
            return await _context.Project
                .Where(p => p.user_id == userId)
                .ToListAsync();
        }

        // GET: /Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(Guid id)
        {
            var userId = GetCurrentUserId();

            var project = await _context.Project.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            // Verify the project belongs to the current user
            if (project.user_id != userId)
            {
                return Forbid();
            }

            return project;
        }

        // GET: /Project/user/{userId} - Admin only endpoint or for profile viewing
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByUserId(Guid userId)
        {
            // If viewing own projects or implement additional permission check here
            return await _context.Project
                .Where(p => p.user_id == userId)
                .ToListAsync();
        }

        // POST: /Project
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            var userId = GetCurrentUserId();

            // Override any user_id in the request with the authenticated user's ID
            project.user_id = userId;
            project.created_at = DateTime.UtcNow;
            project.updated_at = DateTime.UtcNow;

            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.project_id }, project);
        }

        // PUT: /Project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, Project project)
        {
            if (id != project.project_id)
            {
                return BadRequest();
            }

            var userId = GetCurrentUserId();

            // Verify the project exists and belongs to the current user
            var existingProject = await _context.Project.FindAsync(id);
            if (existingProject == null)
            {
                return NotFound();
            }

            if (existingProject.user_id != userId)
            {
                return Forbid();
            }

            // Ensure the user can't change the user_id
            project.user_id = userId;
            project.updated_at = DateTime.UtcNow;

            _context.Entry(existingProject).State = EntityState.Detached;
            _context.Entry(project).State = EntityState.Modified;
            _context.Entry(project).Property(x => x.created_at).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // DELETE: /Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            Guid userId = GetCurrentUserId();

            Project? project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            // Verify the project belongs to the current user
            if (project.user_id != userId)
            {
                return Forbid();
            }

            _context.Project.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(Guid id)
        {
            return _context.Project.Any(p => p.project_id == id);
        }
    }
}