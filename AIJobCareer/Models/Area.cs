using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class Area
    {
            [Key]
            public int AREA_ID { get; set; }
            [StringLength(255)]
        public string AREA_NAME { get; set; }
            public virtual ICollection<User> Users { get; set; }
            public virtual ICollection<Company> Companies { get; set; }
    }
}
