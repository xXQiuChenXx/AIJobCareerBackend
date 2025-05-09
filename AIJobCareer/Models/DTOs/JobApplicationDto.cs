using AIJobCareer.Models;
using System.ComponentModel.DataAnnotations;

namespace AIJobCareerBackend.Models.DTOs
{
    public class JobApplicationSubmitDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public string? LinkedIn { get; set; }

        public string? Portfolio { get; set; }

        [Required]
        public string Experience { get; set; }

        [Required]
        public string Education { get; set; }

        [Required]
        public string Skills { get; set; }

        [Required]
        public string Availability { get; set; }

        public bool Relocate { get; set; }

        [Required]
        public string Salary { get; set; }

        [Required]
        public string CoverLetter { get; set; }

        public IFormFile? Resume { get; set; } = null;

        [Required]
        public bool TermsAccepted { get; set; }

        [Required]
        public int JobId { get; set; }
    }

    public class JobApplicationResponseDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? LinkedIn { get; set; }
        public string? Portfolio { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public string Skills { get; set; }
        public string Availability { get; set; }
        public bool Relocate { get; set; }
        public string Salary { get; set; }
        public string CoverLetter { get; set; }
        public bool HasResume { get; set; }
        public string Status { get; set; }
        public DateTime SubmittedDate { get; set; }
        public JobDto JobPosition { get; set; }
    }

    public class JobDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public JobType EmploymentType { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}