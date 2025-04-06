using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models.DTOs
{
    public class CompanyDTO
    {
        public string company_id { get; set; }
        public string company_name { get; set; }
        public string? company_icon { get; set; }
        public string company_intro { get; set; }
        public string company_website { get; set; }
        public string company_industry { get; set; }
        public int? company_area_id { get; set; }
        public AreaDTO? Area { get; set; }
    }

    // DTO for creating a new company
    public class CreateCompanyDTO
    {
        [Required]
        public string company_id { get; set; }

        [Required]
        [StringLength(255)]
        public string company_name { get; set; }

        [StringLength(255)]
        public string? company_icon { get; set; }

        [Required]
        public string company_intro { get; set; }

        [Required]
        [StringLength(255)]
        public string company_website { get; set; }

        [Required]
        public string company_industry { get; set; }

        public int? company_area_id { get; set; }
    }

    // DTO for updating an existing company
    public class UpdateCompanyDTO
    {
        [Required]
        [StringLength(255)]
        public string company_name { get; set; }

        [StringLength(255)]
        public string? company_icon { get; set; }

        [Required]
        public string company_intro { get; set; }

        [Required]
        [StringLength(255)]
        public string company_website { get; set; }

        [Required]
        public string company_industry { get; set; }

        public int? company_area_id { get; set; }
    }

    // DTO for returning company with related jobs
    public class CompanyWithJobsDTO
    {
        public string company_id { get; set; }
        public string company_name { get; set; }
        public string? company_icon { get; set; }
        public string company_intro { get; set; }
        public string company_website { get; set; }
        public string company_industry { get; set; }
        public AreaDTO? Area { get; set; }
        public List<JobBasicDTO> Jobs { get; set; } = new List<JobBasicDTO>();
    }
    public class JobBasicDTO
    {
        public int job_id { get; set; }
        public string job_title { get; set; }
        public string job_description { get; set; }
        public string job_type { get; set; }
        public decimal? job_salary_min { get; set; }
        public decimal? job_salary_max { get; set; }
    }

    public class AreaDTO
    {
        public int area_id { get; set; }
        public string area_name { get; set; }
    }
}
