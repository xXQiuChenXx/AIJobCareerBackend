using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class JobApplicationReview
    {
        [Key]
        public int review_id { get; set; }

        public int review_application_id { get; set; }

        [ForeignKey("review_application_id")]
        public virtual JobApplication job_application { get; set; }

        public int review_company_id { get; set; }

        [ForeignKey("review_company_id")]
        public virtual Company company { get; set; }

        public string review_status { get; set; }
        public string review_context { get; set; }
        public DateTime review_date { get; set; }
    }
}
