using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Job
    {
        [Key]
        public int JOB_ID { get; set; }
        public int JOB_COMPANY_ID { get; set; }
        [ForeignKey("JOB_COMPANY_ID")]
        public virtual Company Company { get; set; }
        [StringLength(255)]
        public string JOB_TITLE { get; set; }
        public string JOB_RESPONSIBLE { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? JOB_SALARY_MIN { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? JOB_SALARY_MAX { get; set; }
        [StringLength(255)]
        public string JOB_LOCATION { get; set; }
        public string JOB_STATUS { get; set; }
        public string JOB_BENEFIT { get; set; }
        public string JOB_REQUIREMENT { get; set; }
        public virtual ICollection<JobSkill> JobSkills { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
}
