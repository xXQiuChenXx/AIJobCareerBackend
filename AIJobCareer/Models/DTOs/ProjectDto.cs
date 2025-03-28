using System.ComponentModel.DataAnnotations;

namespace AIJobCareer.Models.DTOs
{
    // DTO for project creation and update requests
    public class ProjectRequestDto
    {
        [Required]
        [StringLength(255)]
        public string ProjectName { get; set; }

        [Required]
        public int ProjectYear { get; set; }

        public string Description { get; set; }

        [StringLength(512)]
        [Url]
        public string ProjectUrl { get; set; }
    }

    // DTO for project responses
    public class ProjectResponseDto
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int ProjectYear { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}