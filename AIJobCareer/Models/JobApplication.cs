using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class JobApplication
    {
        [Key]
        public int APPLICATION_ID { get; set; }
        public int APPLICATION_JOB_ID { get; set; }
        [ForeignKey("APPLICATION_JOB_ID")]
        public virtual Job Job { get; set; }
        public string APPLICATION_TYPE { get; set; }
        public string APPLICATION_STATUS { get; set; }
        public DateTime APPLICATION_SUBMISSION_DATE { get; set; }
        public virtual ICollection<UserApplication> UserApplications { get; set; }
        public virtual ICollection<JobApplicationReview> JobApplicationReviews { get; set; }
        public virtual ICollection<JobApplicationTable> JobApplicationTables { get; set; }
    }
}
