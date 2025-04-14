using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AIJobCareer.Models
{
    [Index(nameof(username), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid user_id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string username { get; set; }
        [Required]
        [StringLength(150)]
        public string user_first_name { get; set; }
        [Required]
        [StringLength(150)]
        public string user_last_name { get; set; }
        public int? user_age { get; set; }
        public string? user_intro { get; set; }
        [StringLength(20)]
        public string? user_contact_number { get; set; }
        [StringLength(255)]
        public string user_email { get; set; }
        [StringLength(255)]
        public string user_password { get; set; } = string.Empty;
        [StringLength(255)]
        public string? user_icon { get; set; }

        [Column(TypeName = "enum('public', 'private')")]
        public string user_privacy_status { get; set; } = "public";

        [Required]
        [Column(TypeName = "enum('job_seeker', 'business')")]
        public string user_role { get; set; } = string.Empty;
        public DateTime user_account_created_time { get; set; } = DateTime.UtcNow;
        public DateTime? last_login_at { get; set; }

        [ForeignKey("user_area_id")]
        public int? user_area_id { get; set; }

        [ForeignKey("user_company_id")]
        public string? user_company_id { get; set; }

        public virtual Area Area { get; set; } = null!;
        public Company? Company { get; set; } = null!;
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public virtual ICollection<Resume> Resumes { get; set; } = new List<Resume>();
        public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
        public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

        public virtual ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
        public virtual ICollection<Education> Educations { get; set; } = new List<Education>();
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
        public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();
    }
}
