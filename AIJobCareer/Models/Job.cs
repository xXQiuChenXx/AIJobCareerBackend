using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Job
    {
        [Key]
        public int job_id { get; set; }

        public int job_company_id { get; set; }

        [ForeignKey("job_company_id")]
        public virtual Company company { get; set; } = null;

        [StringLength(255)]
        public string job_title { get; set; } = string.Empty;

        public string job_responsible { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal? job_salary_min { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? job_salary_max { get; set; }

        [StringLength(255)]
        public string job_location { get; set; } = string.Empty;

        public string job_status { get; set; } = string.Empty;
        public string job_benefit { get; set; } = string.Empty;
        public string job_requirement { get; set; } = string.Empty;
        public virtual ICollection<JobSkill> job_skills { get; set; } = new List<JobSkill>();
        public virtual ICollection<JobApplication> job_application { get; set; } = new List<JobApplication>();
    }
}
