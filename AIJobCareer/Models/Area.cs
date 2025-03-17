using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Area
    {
        [Key]
        public int area_id { get; set; }

        [StringLength(255)]
        public string area_name { get; set; }

        public virtual ICollection<User> users { get; set; }
        public virtual ICollection<Company> companies { get; set; }
    }
}
