using AIJobCareer.Data;
using AIJobCareer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Project.ToListAsync();
        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(Guid id)
        {
            var project = await _context.Project.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // GET: api/Project/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByUserId(Guid userId)
        {
            return await _context.Project
                .Where(p => p.user_id == userId)
                .ToListAsync();
        }

        // POST: api/Project
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            project.created_at = DateTime.UtcNow;
            project.updated_at = DateTime.UtcNow;

            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.project_id }, project);
        }

        // PUT: api/Project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, Project project)
        {
            if (id != project.project_id)
            {
                return BadRequest();
            }

            project.updated_at = DateTime.UtcNow;
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

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
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
