using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIJobCareer.Models
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int JobId { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Phone { get; set; }
        
        public string? LinkedIn { get; set; }
        
        public string? Portfolio { get; set; }
        
        [Required]
        public string Experience { get; set; }
        
        [Required]
        public string Education { get; set; }
        
        [Required]
        public string Skills { get; set; }
        
        [Required]
        public string Availability { get; set; }
        
        public bool Relocate { get; set; }
        
        [Required]
        public string Salary { get; set; }
        
        [Required]
        public string CoverLetter { get; set; }
        
        public int? resume_id { get; set; }
        [ForeignKey("resume_id")]
        
        public ApplicationStatus Status { get; set; } = ApplicationStatus.New;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }

        public virtual Resume Resume { get; set; }
    }

    public enum ApplicationStatus
    {
        New,
        UnderReview,
        Interview,
        Rejected,
        Accepted
    }
}