using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AIJobCareerBackend.Models;

namespace AIJobCareer.Models
{
    public class UserApplication
    {
        [Key]
        public int UA_ID { get; set; }
        public Guid UA_USER_ID { get; set; }
        [ForeignKey("UA_USER_ID")]
        public virtual User User { get; set; }
        public int UA_APPLICATION_ID { get; set; }
        [ForeignKey("UA_APPLICATION_ID")]
        public virtual JobApplication job_application { get; set; }
    }
}
