using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models
{
    public class JobApplicationTable
    {
            [Key]
            public int TABLE_ID { get; set; }
            public int TABLE_APPLICATION_ID { get; set; }
            [ForeignKey("TABLE_APPLICATION_ID")]
            public virtual JobApplication JobApplication { get; set; }
            public int TABLE_RESUME_ID { get; set; }
            [ForeignKey("TABLE_RESUME_ID")]
            public virtual Resume Resume { get; set; }
            public string TABLE_COVER_LETTER { get; set; }
    }
}
