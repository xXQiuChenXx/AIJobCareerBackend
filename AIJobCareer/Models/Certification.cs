using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Certification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid certification_id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid user_id { get; set; }

        [Required]
        [StringLength(255)]
        public string certification_name { get; set; }

        [Required]
        [StringLength(255)]
        public string issuing_organization { get; set; }

        [Required]
        public DateTime issue_date { get; set; }

        public DateTime? expiry_date { get; set; }

        [StringLength(255)]
        public string credential_id { get; set; }

        [StringLength(512)]
        public string credential_url { get; set; }

        public DateTime created_at { get; set; } = DateTime.UtcNow;

        public DateTime updated_at { get; set; } = DateTime.UtcNow;

        [ForeignKey("user_id")]
        public virtual User User { get; set; }
    }
}
