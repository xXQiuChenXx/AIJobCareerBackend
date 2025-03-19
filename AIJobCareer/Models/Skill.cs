using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIJobCareer.Models
{
    public class Skill
    {
        [Key]
        public int skill_id { get; set; }
        [StringLength(255)]
        public string skill_name { get; set; }
        public string skill_info { get; set; }
        [StringLength(100)]
        public string skill_type { get; set; }

        [Column(TypeName = "enum('beginner', 'intermediate', 'proficient', 'advanced', 'expert')")]
        public string skill_level { get; set; }
        public virtual ICollection<UserSkill> user_skills { get; set; } = new List<UserSkill>();
        public virtual ICollection<JobSkill> job_skills { get; set; } = new List<JobSkill>();
    }
}
