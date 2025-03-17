using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Resume
    {
        [Key]
        public int RESUME_ID { get; set; }
        public int RESUME_USER_ID { get; set; }
        [ForeignKey("RESUME_USER_ID")]
        public virtual User User { get; set; }
        public string RESUME_TEXT { get; set; }
        [StringLength(255)]
        public string RESUME_FILE { get; set; }
        public DateTime RESUME_LAST_MODIFY_TIME { get; set; }
        public virtual ICollection<JobApplicationTable> JobApplicationTables { get; set; }
    }
}
