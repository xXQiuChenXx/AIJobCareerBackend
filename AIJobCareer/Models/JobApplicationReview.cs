using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class JobApplicationReview
    {
            [Key]
            public int REVIEW_ID { get; set; }
            public int REVIEW_APPLICATION_ID { get; set; }
            [ForeignKey("REVIEW_APPLICATION_ID")]
            public virtual JobApplication JobApplication { get; set; }
            public int REVIEW_COMPANY_ID { get; set; }
            [ForeignKey("REVIEW_COMPANY_ID")]
            public virtual Company Company { get; set; }
            public string REVIEW_STATUS { get; set; }
            public string REVIEW_CONTEXT { get; set; }
            public DateTime REVIEW_DATE { get; set; }
    }
}
