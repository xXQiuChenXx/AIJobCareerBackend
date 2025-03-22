using AIJobCareer.Models;
using AIJobCareer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIJobCareer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        /// <summary>
        /// Gets a paginated list of job listings based on the provided filters
        /// </summary>
        /// <param name="filter">Filter and pagination parameters</param>
        /// <returns>Paginated list of job listings</returns>
        /// <response code="200">Returns the paginated list of job listings</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PaginatedResponse<Job>>> GetJobs([FromQuery] JobFilterRequest filter)
        {
            PaginatedResponse<Job>? result = await _jobService.GetJobsAsync(filter);
            return Ok(result);
        }

        /// <summary>
        /// Gets a specific job listing by id
        /// </summary>
        /// <param name="id">The id of the job listing</param>
        /// <returns>The job listing</returns>
        /// <response code="200">Returns the job listing</response>
        /// <response code="404">If the job listing is not found</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Job>> GetJobById(int id)
        {
            Job? job = await _jobService.GetJobByIdAsync(id);
            if (job == null)
                return NotFound();

            return Ok(job);
        }

        /// <summary>
        /// Creates a new job listing
        /// </summary>
        /// <param name="job">The job listing to create</param>
        /// <returns>The created job listing</returns>
        /// <response code="201">Returns the newly created job listing</response>
        /// <response code="400">If the job data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Job>> CreateJob(Job job)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Job? createdJob = await _jobService.CreateJobAsync(job);
            return CreatedAtAction(nameof(GetJobById), new { id = createdJob.job_id }, createdJob);
        }

        /// <summary>
        /// Updates an existing job listing
        /// </summary>
        /// <param name="id">The id of the job listing to update</param>
        /// <param name="job">The updated job data</param>
        /// <returns>No content</returns>
        /// <response code="204">If the job listing was updated successfully</response>
        /// <response code="400">If the job data is invalid</response>
        /// <response code="404">If the job listing is not found</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateJob(int id, Job job)
        {
            if (id != job.job_id)
                return BadRequest("ID in URL must match ID in request body");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool success = await _jobService.UpdateJobAsync(job);
            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a job listing
        /// </summary>
        /// <param name="id">The id of the job listing to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">If the job listing was deleted successfully</response>
        /// <response code="404">If the job listing is not found</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteJob(int id)
        {
            bool success = await _jobService.DeleteJobAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
