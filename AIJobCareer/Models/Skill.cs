using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Skill
    {
        [Key]
        public int SKILL_ID { get; set; }
        [StringLength(255)]
        public string SKILL_NAME { get; set; }
        public string SKILL_INFO { get; set; }
        [StringLength(100)]
        public string SKILL_TYPE { get; set; }
        public string SKILL_LEVEL { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
        public virtual ICollection<JobSkill> JobSkills { get; set; }
    }
}
