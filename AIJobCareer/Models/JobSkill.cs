using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class JobSkill
    {
        [Key]
        public int JS_ID { get; set; }
        public int JS_JOB_ID { get; set; }
        [ForeignKey("JS_JOB_ID")]
        public virtual Job Job { get; set; }
        public int JS_SKILL_ID { get; set; }
        [ForeignKey("JS_SKILL_ID")]
        public virtual Skill Skill { get; set; }
    }
}
