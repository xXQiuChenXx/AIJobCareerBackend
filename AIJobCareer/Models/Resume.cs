using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Resume
    {
        [Key]
        public int resume_id { get; set; }

        public Guid resume_user_id { get; set; }

        [ForeignKey("resume_user_id")]
        public virtual User user { get; set; }

        public string resume_url { get; set; }
        public string resume_name { get; set; }

        public string? job_application_id { get; set; }
        [ForeignKey("job_application_id")]

        public virtual JobApplication JobApplication { get; set; }
    }
}
