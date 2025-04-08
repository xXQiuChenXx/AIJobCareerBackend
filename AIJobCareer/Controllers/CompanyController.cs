using AIJobCareer.Data;
using AIJobCareer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AIJobCareer.Models.DTOs;

namespace AIJobCareer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CompanyController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Company/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(string id)
        {
            var company = await _context.Company
                .Include(c => c.Area)
                .FirstOrDefaultAsync(c => c.company_id == id);

            if (company == null)
            {
                return NotFound("Company not found");
            }

            var companyDTO = new CompanyDTO
            {
                company_id = company.company_id,
                company_name = company.company_name,
                company_icon = company.company_icon,
                company_intro = company.company_intro,
                company_website = company.company_website,
                company_industry = company.company_industry,
                company_area_name = company.Area.area_name,
                company_founded = company.company_founded,
            };

            return Ok(companyDTO);
        }

        // GET: api/Company/5/jobs
        [HttpGet("{id}/jobs")]
        public async Task<IActionResult> GetCompanyWithJobs(string id)
        {
            var company = await _context.Company
                .Include(c => c.Area)
                .Include(c => c.jobs)
                .FirstOrDefaultAsync(c => c.company_id == id);

            if (company == null)
            {
                return NotFound("Company not found");
            }

            var companyWithJobsDTO = new CompanyWithJobsDTO
            {
                company_id = company.company_id,
                company_name = company.company_name,
                company_icon = company.company_icon,
                company_intro = company.company_intro,
                company_website = company.company_website,
                company_founded = company.company_founded,
                company_industry = company.company_industry,
                company_area_name = company?.Area?.area_name,
                Jobs = company.jobs.Select(j => new JobBasicDTO
                {
                    job_id = j.job_id,
                    job_title = j.job_title,
                    job_description = j.job_description,
                    job_type = j.job_type,
                    job_salary_min = j.job_salary_min,
                    job_salary_max = j.job_salary_max,
                    job_location = j.job_location,
                    job_posted_date = j.Posted_Date
                }).ToList()
            };

            return Ok(companyWithJobsDTO);
        }

        // POST: api/Company
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCompany(CreateCompanyDTO companyDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Validate area exists if provided
                if (companyDTO.company_area_id.HasValue)
                {
                    var areaExists = await _context.Area.AnyAsync(a => a.area_id == companyDTO.company_area_id);
                    if (!areaExists)
                    {
                        return BadRequest("The specified area does not exist");
                    }
                }

                var company = new Company
                {
                    company_id = companyDTO.company_id,
                    company_name = companyDTO.company_name,
                    company_founded = companyDTO.company_founded,
                    company_icon = companyDTO.company_icon,
                    company_intro = companyDTO.company_intro,
                    company_website = companyDTO.company_website,
                    company_industry = companyDTO.company_industry,
                    company_area_id = companyDTO.company_area_id
                };

                _context.Company.Add(company);
                await _context.SaveChangesAsync();

                var createdCompanyDTO = new CompanyDTO
                {
                    company_id = company.company_id,
                    company_name = company.company_name,
                    company_icon = company.company_icon,
                    company_intro = company.company_intro,
                    company_website = company.company_website,
                    company_founded = company.company_founded,
                    company_industry = company.company_industry,
                    company_area_name = company.Area.area_name
                };

                return CreatedAtAction(nameof(GetCompany), new { id = company.company_id }, createdCompanyDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCompany(string id, UpdateCompanyDTO companyDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Validate area exists if provided
                if (!string.IsNullOrEmpty(companyDTO.company_area_name))
                {
                    var area = await _context.Area.FirstOrDefaultAsync(a => a.area_name == companyDTO.company_area_name);
                    if (area == null)
                    {
                        return BadRequest("The specified area does not exist");
                    }
                    companyDTO.company_area_id = area.area_id;
                }

                // Ensure company exists
                var existingCompany = await _context.Company.FindAsync(id);
                if (existingCompany == null)
                {
                    return NotFound("Company not found");
                }

                // Check if user has permission (assuming business user associated with this company)
                var currentUserId = User.FindFirst("UserId")?.Value;
                if (currentUserId != null)
                {
                    var user = await _context.User
                        .FirstOrDefaultAsync(u => u.user_id.ToString() == currentUserId);

                    if (user == null || (user.user_company_id != id && user.user_role != "business"))
                    {
                        return Forbid("You don't have permission to update this company");
                    }
                }

                // Update properties
                existingCompany.company_name = companyDTO.company_name;
                existingCompany.company_icon = companyDTO.company_icon;
                existingCompany.company_intro = companyDTO.company_intro;
                existingCompany.company_website = companyDTO.company_website;
                existingCompany.company_founded = companyDTO.company_founded;
                existingCompany.company_industry = companyDTO.company_industry;
                existingCompany.company_area_id = companyDTO.company_area_id;

                await _context.SaveChangesAsync();

                var updatedCompanyDTO = new CompanyDTO
                {
                    company_id = existingCompany.company_id,
                    company_name = existingCompany.company_name,
                    company_icon = existingCompany.company_icon,
                    company_founded = existingCompany.company_founded,
                    company_intro = existingCompany.company_intro,
                    company_website = existingCompany.company_website,
                    company_industry = existingCompany.company_industry,
                    company_area_name = existingCompany.Area.area_name
                };

                return Ok(updatedCompanyDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound("Company not found");
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]  // Only admin can delete companies
        public async Task<IActionResult> DeleteCompany(string id)
        {
            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return NotFound("Company not found");
            }

            try
            {
                // Check if there are users associated with this company
                var associatedUsers = await _context.User
                    .Where(u => u.user_company_id == id)
                    .ToListAsync();

                if (associatedUsers.Any())
                {
                    return BadRequest("Cannot delete company with associated users");
                }

                _context.Company.Remove(company);
                await _context.SaveChangesAsync();

                return Ok("Company deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool CompanyExists(string id)
        {
            return _context.Company.Any(e => e.company_id == id);
        }
    }
}