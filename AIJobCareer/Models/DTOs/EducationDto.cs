namespace AIJobCareer.Models.DTOs
{
    public class EducationDto
    {
        public Guid education_id { get; set; }
        public Guid user_id { get; set; }
        public string degree_name { get; set; }
        public string institution_name { get; set; }
        public int start_year { get; set; }
        public int? end_year { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class EducationCreateDto
    {
        public string degree_name { get; set; }
        public string institution_name { get; set; }
        public int start_year { get; set; }
        public int? end_year { get; set; }
        public string description { get; set; }
    }

    public class EducationUpdateDto
    {
        public string degree_name { get; set; }
        public string institution_name { get; set; }
        public int start_year { get; set; }
        public int? end_year { get; set; }
        public string description { get; set; }
    }
}
