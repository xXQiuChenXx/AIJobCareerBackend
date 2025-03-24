using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Publication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid publication_id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid user_id { get; set; }

        [Required]
        [StringLength(255)]
        public string publication_title { get; set; }

        [Required]
        [StringLength(255)]
        public string publisher { get; set; }

        public int publication_year { get; set; }

        [StringLength(512)]
        public string publication_url { get; set; }

        public string description { get; set; }

        public DateTime created_at { get; set; } = DateTime.UtcNow;

        public DateTime updated_at { get; set; } = DateTime.UtcNow;

        [ForeignKey("user_id")]
        public virtual User User { get; set; }
    }
}
