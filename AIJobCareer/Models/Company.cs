using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Company
    {
        [Key]
        public int COMPANY_ID { get; set; }
        [StringLength(255)]
        public string COMPANY_NAME { get; set; }
        [StringLength(255)]
        public string COMPANY_ICON { get; set; }
        public string COMPANY_INTRO { get; set; }
        [StringLength(255)]
        public string COMPANY_WEBSITE { get; set; }
        public int? COMPANY_AREA_ID { get; set; }
        [ForeignKey("COMPANY_AREA_ID")]
        public virtual Area Area { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<JobApplicationReview> JobApplicationReviews { get; set; }
    }
}
