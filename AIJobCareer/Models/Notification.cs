using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Notification
    {

        [Key]
        public int notification_id { get; set; }

        public Guid notification_user_id { get; set; }

        [ForeignKey("notification_user_id")]
        public virtual User user { get; set; }

        public string? notification_company_id { get; set; }

        [ForeignKey("notification_company_id")]
        public virtual Company company { get; set; }

        public string notification_text { get; set; }
        public DateTime notification_timestamp { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "enum('read', 'unread')")]
        public string notification_status { get; set; } = "unread";
    }
}
