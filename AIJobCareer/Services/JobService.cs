
using AIJobCareer.Data;
using AIJobCareer.Models;
using Microsoft.EntityFrameworkCore;

namespace AIJobCareer.Services
{
    public interface IJobService
    {
        Task<PaginatedResponse<JobResponseDto>> GetJobsAsync(JobFilterRequest filter);
        Task<Job> GetJobByIdAsync(int id);
        Task<Job> CreateJobAsync(Job job);
        Task<bool> UpdateJobAsync(Job job);
        Task<bool> DeleteJobAsync(int id);
    }

    public class JobService : IJobService
    {
        private readonly ApplicationDBContext _dbContext;

        public JobService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResponse<JobResponseDto>> GetJobsAsync(JobFilterRequest filter)
        {
            var query = _dbContext.Job.Include(j => j.company).AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                var searchTerm = filter.SearchTerm.ToLower();
                query = query.Where(j => j.job_title.ToLower().Contains(searchTerm) ||
                                         j.company.company_name.ToLower().Contains(searchTerm));
            }

            if (filter.JobType.HasValue)
            {
                query = query.Where(j => j.job_type == filter.JobType.Value);
            }

            if (!string.IsNullOrEmpty(filter.Location))
            {
                query = query.Where(j => j.job_location.Contains(filter.Location));
            }

            if (filter.MinSalary.HasValue)
            {
                query = query.Where(j => j.job_salary_max >= filter.MinSalary.Value);
            }

            if (filter.MaxSalary.HasValue)
            {
                query = query.Where(j => j.job_salary_min <= filter.MaxSalary.Value);
            }

            // Apply sorting
            query = filter.SortBy?.ToLower() switch
            {
                "title" => filter.SortDescending ? query.OrderByDescending(j => j.job_title) : query.OrderBy(j => j.job_title),
                "company" => filter.SortDescending ? query.OrderByDescending(j => j.company.company_name) : query.OrderBy(j => j.company.company_name),
                "salary" => filter.SortDescending ? query.OrderByDescending(j => j.job_salary_max) : query.OrderBy(j => j.job_salary_max),
                _ => filter.SortDescending ? query.OrderByDescending(j => j.Posted_Date) : query.OrderBy(j => j.Posted_Date)
            };

            // Get total count for pagination
            int totalCount = await query.CountAsync();

            // Apply pagination
            List<JobResponseDto> items = await query
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(j => new JobResponseDto
                {
                    job_id = j.job_id,
                    job_title = j.job_title,
                    job_responsible = j.job_responsible,
                    job_salary_min = j.job_salary_min,
                    job_salary_max = j.job_salary_max,
                    job_location = j.job_location,
                    job_type = j.job_type,
                    job_status = j.job_status,
                    Posted_Date = j.Posted_Date,
                    job_benefit = j.job_benefit,
                    job_requirement = j.job_requirement,
                    company = new CompanyDto
                    {
                        company_id = j.company.company_id,
                        company_name = j.company.company_name,
                        company_icon = j.company.company_icon,
                        company_intro = j.company.company_intro,
                        company_website = j.company.company_website
                    },

                })
                .ToListAsync();

            return new PaginatedResponse<JobResponseDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageIndex = filter.PageIndex,
                PageSize = filter.PageSize
            };
        }

        public async Task<Job> GetJobByIdAsync(int id)
        {
            return await _dbContext.Job.FindAsync(id);
        }

        public async Task<Job> CreateJobAsync(Job job)
        {
            job.Posted_Date = DateTime.UtcNow;
            _dbContext.Job.Add(job);
            await _dbContext.SaveChangesAsync();
            return job;
        }

        public async Task<bool> UpdateJobAsync(Job job)
        {
            var existingJob = await _dbContext.Job.FindAsync(job.job_id);
            if (existingJob == null)
                return false;

            // Update properties of existing job
            _dbContext.Entry(existingJob).CurrentValues.SetValues(job);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteJobAsync(int id)
        {
            var job = await _dbContext.Job.FindAsync(id);
            if (job == null)
                return false;

            _dbContext.Job.Remove(job);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }

    public class JobFilterRequest
    {
        public string? SearchTerm { get; set; }
        public JobType? JobType { get; set; }
        public string? Location { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string? SortBy { get; set; } = "PostedDate"; // Default sort by posted date
        public bool SortDescending { get; set; } = true; // Default newest first
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class JobResponseDto
    {
        public int job_id { get; set; }
        public string job_title { get; set; }
        public string job_responsible { get; set; }
        public decimal? job_salary_min { get; set; }
        public decimal? job_salary_max { get; set; }
        public string job_location { get; set; }
        public JobType job_type { get; set; }
        public string job_status { get; set; }
        public DateTime Posted_Date { get; set; }
        public string job_benefit { get; set; }
        public string job_requirement { get; set; }

        // Company without jobs collection
        public CompanyDto company { get; set; }
    }

    public class CompanyDto
    {
        public int company_id { get; set; }
        public string company_name { get; set; }
        public string company_icon { get; set; }
        public string company_intro { get; set; }
        public string company_website { get; set; }
        // No jobs collection here
    }
}
