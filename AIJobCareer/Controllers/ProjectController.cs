using AIJobCareer.Data;
using AIJobCareer.Models;
using AIJobCareer.Models.DTOs;
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
        public async Task<ActionResult<IEnumerable<ProjectResponseDto>>> GetProjects()
        {
            var userId = GetCurrentUserId();
            var projects = await _context.Project
                .Where(p => p.user_id == userId)
                .ToListAsync();

            return projects.Select(p => new ProjectResponseDto
            {
                ProjectId = p.project_id,
                ProjectName = p.project_name,
                ProjectYear = p.project_year,
                Description = p.description,
                ProjectUrl = p.project_url,
                CreatedAt = p.created_at,
                UpdatedAt = p.updated_at
            }).ToList();
        }

        // GET: /Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectResponseDto>> GetProject(Guid id)
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

            return new ProjectResponseDto
            {
                ProjectId = project.project_id,
                ProjectName = project.project_name,
                ProjectYear = project.project_year,
                Description = project.description,
                ProjectUrl = project.project_url,
                CreatedAt = project.created_at,
                UpdatedAt = project.updated_at
            };
        }

        // GET: /Project/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ProjectResponseDto>>> GetProjectsByUserId(Guid userId)
        {
            var projects = await _context.Project
                .Where(p => p.user_id == userId)
                .ToListAsync();

            return projects.Select(p => new ProjectResponseDto
            {
                ProjectId = p.project_id,
                ProjectName = p.project_name,
                ProjectYear = p.project_year,
                Description = p.description,
                ProjectUrl = p.project_url,
                CreatedAt = p.created_at,
                UpdatedAt = p.updated_at
            }).ToList();
        }

        // POST: /Project
        [HttpPost]
        public async Task<ActionResult<ProjectResponseDto>> CreateProject(ProjectRequestDto projectDto)
        {
            var userId = GetCurrentUserId();

            var project = new Project
            {
                user_id = userId,
                project_name = projectDto.ProjectName,
                project_year = projectDto.ProjectYear,
                description = projectDto.Description,
                project_url = projectDto.ProjectUrl,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            var responseDto = new ProjectResponseDto
            {
                ProjectId = project.project_id,
                ProjectName = project.project_name,
                ProjectYear = project.project_year,
                Description = project.description,
                ProjectUrl = project.project_url,
                CreatedAt = project.created_at,
                UpdatedAt = project.updated_at
            };

            return CreatedAtAction(nameof(GetProject), new { id = project.project_id }, responseDto);
        }

        // PUT: /Project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, ProjectRequestDto projectDto)
        {
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

            // Update the project with DTO values
            existingProject.project_name = projectDto.ProjectName;
            existingProject.project_year = projectDto.ProjectYear;
            existingProject.description = projectDto.Description;
            existingProject.project_url = projectDto.ProjectUrl;
            existingProject.updated_at = DateTime.UtcNow;

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