using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class JobApplication
    {
        [Key]
        public int application_id { get; set; }
        public int application_job_id { get; set; }

        [Column(TypeName = "enum('internship', 'freelance', 'full_time', 'part_time', 'contract')")]
        public string application_type { get; set; }

        [Column(TypeName = "enum('pending','interview_scheduled', 'accepted', 'rejected')")]
        public string application_status { get; set; } = "pending";
        public DateTime application_submission_date { get; set; } = DateTime.UtcNow;

        [ForeignKey("application_job_id")]
        public virtual Job job { get; set; }
        public virtual ICollection<UserApplication> user_application { get; set; }
        public virtual ICollection<JobApplicationReview> job_application_reviews { get; set; }
        public virtual ICollection<JobApplicationTable> job_application_table { get; set; }
    }
}
