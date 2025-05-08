
using AIJobCareer.Data;
using AIJobCareer.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AIJobCareer.Services
{
    public interface IJobService
    {
        Task<PaginatedResponse<JobResponseDto>> GetJobsAsync(JobFilterRequest filter);
        Task<JobResponseDto> GetJobByIdAsync(int id);
        Task<JobCreateDto> CreateJobAsync(JobCreateDto job);
        Task<bool> UpdateJobAsync(JobUpdateDto job);
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

            if (filter.JobType != null && filter.JobType.Length > 0)
            {
                query = query.Where(j => filter.JobType.Contains(j.job_type));
            }

            if (filter.Company != null)
            {
                query = query.Where(j => j.job_company_id == filter.Company);
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
                _ => filter.SortDescending ? query.OrderByDescending(j => j.posted_date) : query.OrderBy(j => j.posted_date)
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
                    posted_date = j.posted_date,
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

        public async Task<JobResponseDto?> GetJobByIdAsync(int id)
        {

            Job? result = await _dbContext.Job.Include(j => j.company).FirstOrDefaultAsync(j => j.job_id == id);
            if (result == null)
                return null;

            return new JobResponseDto
            {
                job_id = result.job_id,
                job_title = result.job_title,
                job_responsible = result.job_responsible,
                job_salary_min = result.job_salary_min,
                job_salary_max = result.job_salary_max,
                job_location = result.job_location,
                job_type = result.job_type,
                job_status = result.job_status,
                posted_date = result.posted_date,
                job_deadline = result.job_deadline,
                job_description = result.job_description,
                job_benefit = result.job_benefit,
                job_requirement = result.job_requirement,
                company = new CompanyDto
                {
                    company_id = result.company.company_id,
                    company_name = result.company.company_name,
                    company_icon = result.company.company_icon,
                    company_intro = result.company.company_intro,
                    company_industry = result.company.company_industry,
                    company_website = result.company.company_website
                },
            };
        }

        public async Task<JobCreateDto> CreateJobAsync(JobCreateDto job)
        {
            job.posted_date = DateTime.UtcNow;
            var formatted = new Job
            {
                job_id = job.job_id,
                job_title = job.job_title,
                job_description = job.job_description,
                job_company_id = job.job_company_id,
                job_responsible = job.job_responsible,
                job_salary_min = job.job_salary_min,
                job_salary_max = job.job_salary_max,
                job_location = job.job_location,
                job_type = job.job_type,
                job_status = job.job_status,
                posted_date = job.posted_date,
                job_deadline = job.job_deadline,
                job_benefit = job.job_benefit,
                job_requirement = job.job_requirement,
                company = _dbContext.Company.Find(job.job_company_id)
            };
            _dbContext.Job.Add(formatted);
            await _dbContext.SaveChangesAsync();
            return job;
            ;
        }

        public async Task<bool> UpdateJobAsync(JobUpdateDto job)
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
        public JobType[]? JobType { get; set; }
        public string? Location { get; set; }
        public string? Company { get; set; }
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
        public string job_description { get; set; }
        public string job_responsible { get; set; }
        public decimal? job_salary_min { get; set; }
        public decimal? job_salary_max { get; set; }
        public string job_location { get; set; }
        public JobType job_type { get; set; }
        public string job_status { get; set; }
        public DateTime posted_date { get; set; }
        public DateTime job_deadline { get; set; }
        public string job_benefit { get; set; }
        public string job_requirement { get; set; }

        // Company without jobs collection
        public CompanyDto company { get; set; }
    }

    public class JobCreateDto
    {
        public int job_id { get; set; }
        public string job_title { get; set; }
        public string job_description { get; set; }
        public string job_company_id { get; set; }
        public string job_responsible { get; set; }
        public decimal? job_salary_min { get; set; }
        public decimal? job_salary_max { get; set; }
        public string job_location { get; set; }
        public JobType job_type { get; set; }
        public string job_status { get; set; }
        public DateTime posted_date { get; set; }
        public DateTime job_deadline { get; set; }
        public string job_benefit { get; set; }
        public string job_requirement { get; set; }
    }

    public class JobUpdateDto
    {
        public int job_id { get; set; }
        public string job_title { get; set; }
        public string job_description { get; set; }
        public string job_responsible { get; set; }
        public decimal? job_salary_min { get; set; }
        public decimal? job_salary_max { get; set; }
        public string job_location { get; set; }
        public JobType job_type { get; set; }
        public string job_status { get; set; }
        public DateTime posted_date { get; set; }
        public DateTime job_deadline { get; set; }
        public string job_benefit { get; set; }
        public string job_requirement { get; set; }
    }

    public class CompanyDto
    {
        public string company_id { get; set; }
        public string company_name { get; set; }
        public string company_icon { get; set; }
        public string company_intro { get; set; }
        public string company_website { get; set; }
        public string company_industry { get; set; }
    }
}
