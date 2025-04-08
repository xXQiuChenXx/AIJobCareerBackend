using AIJobCareer.Data;
using AIJobCareer.Models;
using AIJobCareer.Services;
using AIJobCareerBackend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AIJobCareer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class JobApplicationsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<JobApplicationsController> _logger;
        private readonly IR2FileService _fileService;

        public JobApplicationsController(
            ApplicationDBContext context,
            IR2FileService fileService,
            ILogger<JobApplicationsController> logger)
        {
            _context = context;
            _logger = logger;
            _fileService = fileService;
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

        // POST: api/JobApplications
        [HttpPost]
        public async Task<ActionResult<JobApplicationResponseDto>> SubmitApplication([FromForm] JobApplicationSubmitDto applicationDto)
        {
            var userId = GetCurrentUserId();
            try
            {
                // Verify the job position exists
                var jobPosition = await _context.Job.FindAsync(applicationDto.JobId);
                if (jobPosition == null)
                {
                    return BadRequest("Invalid job position");
                }

                // Verify the user exists
                var user = await _context.User.FindAsync(applicationDto.UserId);
                if (user == null)
                {
                    return BadRequest("Invalid user");
                }

                // Check if user has already applied for this position
                var existingApplication = await _context.Job_Application
                    .FirstOrDefaultAsync(a => a.UserId == applicationDto.UserId && a.JobId == applicationDto.JobId);

                if (existingApplication != null)
                {
                    return BadRequest("You have already applied for this position");
                }

                // Verify terms were accepted
                if (!applicationDto.TermsAccepted)
                {
                    return BadRequest("Terms and conditions must be accepted");
                }

                // Create new job application
                var application = new JobApplication
                {
                    UserId = userId,
                    FirstName = applicationDto.FirstName,
                    LastName = applicationDto.LastName,
                    Email = applicationDto.Email,
                    Phone = applicationDto.Phone,
                    LinkedIn = applicationDto.LinkedIn,
                    Portfolio = applicationDto.Portfolio,
                    Experience = applicationDto.Experience,
                    Education = applicationDto.Education,
                    Skills = applicationDto.Skills,
                    Availability = applicationDto.Availability,
                    Relocate = applicationDto.Relocate,
                    Salary = applicationDto.Salary,
                    CoverLetter = applicationDto.CoverLetter,
                    Status = ApplicationStatus.New,
                    CreatedAt = DateTime.UtcNow,
                    JobId = applicationDto.JobId
                };

                // Handle resume upload if provided
                if (applicationDto.Resume != null)
                {
                    // Validate file
                    var allowedExtensions = new[] { ".pdf", ".docx", ".doc", ".txt" };
                    var extension = Path.GetExtension(applicationDto.Resume.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(extension))
                    {
                        return BadRequest("Invalid file type. Allowed types: PDF, DOCX, DOC, TXT");
                    }

                    if (applicationDto.Resume.Length > 5 * 1024 * 1024) // 5MB max
                    {
                        return BadRequest("File size exceeds the 5MB limit");
                    }
                    try
                    {
                        var fileKey = await _fileService.UploadFileAsync(applicationDto.Resume, "resumes");
                        application.ResumeUrl = "https://store.myitscm.com/" + fileKey;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error during file upload: {ex.Message}");
                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading the file");
                    }
                   
                }

                _context.Job_Application.Add(application);
                await _context.SaveChangesAsync();

                // Return response
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting job application");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your application");
            }
        }

        // GET: api/JobApplications/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<JobApplicationResponseDto>>> GetUserApplications(Guid userId)
        {
            // Check if user exists
            var user = await _context.User.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Get all applications by user
            var applications = await _context.Job_Application
                .Include(a => a.Job)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return applications.Select(a => MapToResponseDto(a, a.Job)).ToList();
        }

        // GET: api/JobApplications/job/{jobId}
        [HttpGet("job/{jobId}")]
        public async Task<ActionResult<IEnumerable<JobApplicationResponseDto>>> GetJobApplications(int jobId)
        {
            // Check if job position exists
            var jobPosition = await _context.Job.FindAsync(jobId);
            if (jobPosition == null)
            {
                return NotFound("Job position not found");
            }

            // Get all applications for this position
            var applications = await _context.Job_Application
                .Include(a => a.User)
                .Where(a => a.JobId == jobId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return applications.Select(a => MapToResponseDto(a, jobPosition)).ToList();
        }

        // GET: api/JobApplications/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplicationResponseDto>> GetApplication(int id)
        {
            var application = await _context.Job_Application
                .Include(a => a.Job)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
            {
                return NotFound();
            }

            return MapToResponseDto(application, application.Job);
        }

        // PUT: api/JobApplications/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateApplicationStatus(int id, [FromBody] string status)
        {
            var application = await _context.Job_Application.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            // Parse and validate status
            if (!Enum.TryParse<ApplicationStatus>(status, true, out var newStatus))
            {
                return BadRequest("Invalid application status");
            }

            application.Status = newStatus;
            application.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating application status");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update application status");
            }
        }

        private JobApplicationResponseDto MapToResponseDto(JobApplication application, Job jobPosition)
        {
            return new JobApplicationResponseDto
            {
                Id = application.Id,
                UserId = application.UserId,
                FirstName = application.FirstName,
                LastName = application.LastName,
                Email = application.Email,
                Phone = application.Phone,
                LinkedIn = application.LinkedIn,
                Portfolio = application.Portfolio,
                Experience = application.Experience,
                Education = application.Education,
                Skills = application.Skills,
                Availability = application.Availability,
                Relocate = application.Relocate,
                Salary = application.Salary,
                CoverLetter = application.CoverLetter,
                HasResume = !string.IsNullOrEmpty(application.ResumeUrl),
                Status = application.Status.ToString(),
                SubmittedDate = application.CreatedAt,
                JobPosition = new JobDto
                {
                    Id = jobPosition.job_id,
                    Title = jobPosition.job_title,
                    Company = jobPosition.company.company_name,
                    Location = jobPosition.job_location,
                    EmploymentType = jobPosition.job_type
                }
            };
        }
    }
}
