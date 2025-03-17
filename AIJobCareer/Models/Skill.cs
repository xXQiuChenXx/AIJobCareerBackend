using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Skill
    {
        [Key]
        public int skill_id { get; set; }
        [StringLength(255)]
        public string skill_name { get; set; } = string.Empty;
        public string skill_info { get; set; } = string.Empty;
        [StringLength(100)]
        public string skill_type { get; set; } = string.Empty;
        public string skill_level { get; set; } = string.Empty;
        public virtual ICollection<UserSkill> user_skills { get; set; } = new List<UserSkill>();
        public virtual ICollection<JobSkill> job_skills { get; set; } = new List<JobSkill>();
    }
}
