namespace AIJobCareer.DTOs
{
    // DTO for responses
    public class WorkExperienceResponseDto
    {
        public Guid ExperienceId { get; set; }
        public Guid UserId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public string Description { get; set; }
        public string ExperienceSkill { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    // DTO for creating new work experience
    public class WorkExperienceCreateDto
    {
        public Guid UserId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public string Description { get; set; }
        public string ExperienceSkill { get; set; }
    }

    // DTO for updating existing work experience
    public class WorkExperienceUpdateDto
    {
        public Guid ExperienceId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public string Description { get; set; }
        public string ExperienceSkill { get; set; }
    }
}