using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class CareerAnalysis
    {
        [Key]
        public int analysis_id { get; set; }
        public Guid analysis_user_id { get; set; }
        [ForeignKey("analysis_user_id")]
        public virtual User User { get; set; }
        public string analysis_ai_direction { get; set; }
        public string analysis_ai_market_gap { get; set; }
        public string analysis_ai_career_prospects { get; set; }
    }
}
