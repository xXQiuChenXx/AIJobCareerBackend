using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Education
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid education_id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid user_id { get; set; }

        [Required]
        [StringLength(255)]
        public string degree_name { get; set; }

        [Required]
        [StringLength(255)]
        public string institution_name { get; set; }

        [Required]
        public int start_year { get; set; }

        public int? end_year { get; set; }

        public string description { get; set; }

        public DateTime created_at { get; set; } = DateTime.UtcNow;

        public DateTime updated_at { get; set; } = DateTime.UtcNow;

        [ForeignKey("user_id")]
        public virtual User User { get; set; }
    }
}
