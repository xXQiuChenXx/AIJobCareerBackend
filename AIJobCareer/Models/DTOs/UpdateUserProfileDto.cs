namespace AIJobCareer.Models.DTOs
{
    public class UpdateUserProfileDto
    {
        public string? user_first_name { get; set; }
        public string? user_last_name { get; set; }
        public int? user_age { get; set; }
        public string? user_intro { get; set; }
        public string? user_contact_number { get; set; }
        public string user_email { get; set; }
        public string? user_icon { get; set; }
        public string? area_name { get; set; } // Added for area updates
        public string? privacy_status { get; set; }
    }
}
