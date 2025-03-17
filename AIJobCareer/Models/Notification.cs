using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Notification
    {
        [Key]
        public int NOTIFICATION_ID { get; set; }
        public int? NOTIFICATION_USER_ID { get; set; }
        [ForeignKey("NOTIFICATION_USER_ID")]
        public virtual User User { get; set; }
        public int? NOTIFICATION_COMPANY_ID { get; set; }
        [ForeignKey("NOTIFICATION_COMPANY_ID")]
        public virtual Company Company { get; set; }
        public string NOTIFICATION_TEXT { get; set; }
        public DateTime NOTIFICATION_TIMESTAMP { get; set; }
        public string NOTIFICATION_STATUS { get; set; }
    }
}
