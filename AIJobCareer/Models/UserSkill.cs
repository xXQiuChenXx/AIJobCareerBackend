using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class UserSkill
    {
        [Key]
        public int US_ID { get; set; }
        public Guid US_USER_ID { get; set; }
        [ForeignKey("US_USER_ID")]
        public virtual User User { get; set; } = null;
        public int US_SKILL_ID { get; set; }
        [ForeignKey("US_SKILL_ID")]
        public virtual Skill Skill { get; set; } = null;
    }
}
