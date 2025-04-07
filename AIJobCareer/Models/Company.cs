using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Company
    {
        [Key]
        public string company_id { get; set; }

        [StringLength(255)]
        public string company_name { get; set; } = string.Empty;


        [StringLength(255)]
        public string? company_icon { get; set; } = string.Empty;

        public string company_intro { get; set; } = string.Empty;

        [StringLength(255)]
        public string company_website { get; set; } = string.Empty;

        public string company_industry { get; set; } = string.Empty;

        public int? company_area_id { get; set; }

        public DateTime company_founded { get; set; }

        [ForeignKey("company_area_id")]
        public virtual Area Area { get; set; }
        public User? User { get; set; }
        public virtual ICollection<Notification> notifications { get; set; } = new List<Notification>();
        public virtual ICollection<Job> jobs { get; set; } = new List<Job>();
        public virtual ICollection<JobApplicationReview> job_application_reviews { get; set; } = new List<JobApplicationReview>();
    }
}
