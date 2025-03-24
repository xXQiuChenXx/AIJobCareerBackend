using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid project_id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid user_id { get; set; }

        [Required]
        [StringLength(255)]
        public string project_name { get; set; }

        public int project_year { get; set; }

        public string description { get; set; }

        [StringLength(512)]
        public string project_url { get; set; }

        public DateTime created_at { get; set; } = DateTime.UtcNow;

        public DateTime updated_at { get; set; } = DateTime.UtcNow;

        [ForeignKey("user_id")]
        public virtual User User { get; set; }
    }
}
