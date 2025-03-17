using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class User
    {
        [Key]
        public int USER_ID { get; set; }
        [Required]
        [StringLength(255)]
        public string USER_NAME { get; set; }
        public int? USER_AGE { get; set; }
        public string USER_INTRO { get; set; }
        [StringLength(20)]
        public string USER_CONTACT_NUMBER { get; set; }
        [StringLength(255)]
        public string USER_EMAIL { get; set; }
        [StringLength(255)]
        public string USER_PASSWORD { get; set; }
        [StringLength(255)]
        public string USER_ICON { get; set; }
        public string USER_PRIVACY_STATUS { get; set; }
        public string USER_ROLE { get; set; }
        public DateTime USER_ACCOUNT_CREATED_TIME { get; set; }
        public int? USER_AREA_ID { get; set; }
        [ForeignKey("USER_AREA_ID")]
        public virtual Area Area { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
        public virtual ICollection<UserApplication> UserApplications { get; set; }
        public virtual ICollection<CareerAnalysis> CareerAnalyses { get; set; }
    }
}
