using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AIJobCareer.Models
{
    public class WorkExperience
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid experience_id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid user_id { get; set; }

        [Required]
        [StringLength(255)]
        public string job_title { get; set; }

        [Required]
        [StringLength(255)]
        public string company_name { get; set; }

        [StringLength(255)]
        public string location { get; set; }

        [Required]
        public DateTime start_date { get; set; }

        public DateTime? end_date { get; set; }

        public bool is_current { get; set; } = false;

        public string description { get; set; }

        public DateTime created_at { get; set; } = DateTime.UtcNow;

        public DateTime updated_at { get; set; } = DateTime.UtcNow;

        [ForeignKey("user_id")]
        public virtual User User { get; set; }

        public string experience_skill { get; set; }
    }
}
