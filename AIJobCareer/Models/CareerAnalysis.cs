using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class CareerAnalysis
    {
        [Key]
        public int ANALYSIS_ID { get; set; }
        public int ANALYSIS_USER_ID { get; set; }
        [ForeignKey("ANALYSIS_USER_ID")]
        public virtual User User { get; set; }
        public string ANALYSIS_AI_DIRECTION { get; set; }
        public string ANALYSIS_AI_MARKET_GAP { get; set; }
        public string ANALYSIS_AI_CAREER_PROSPECTS { get; set; }
    }
}
