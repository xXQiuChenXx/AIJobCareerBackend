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

        public string resume_text { get; set; }

        [StringLength(255)]
        public string resume_file { get; set; }

        public DateTime resume_last_modify_time { get; set; }

        public virtual ICollection<JobApplicationTable> job_application_tables { get; set; }
    }
}
