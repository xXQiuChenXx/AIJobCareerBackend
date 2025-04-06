namespace AIJobCareer.Models.DTOs
{
    public class CreateUserSkillDTO
    {
        public string skill_level { get; set; }
        public string skill_name { get; set; }
    }
    public class UserSkillDTO
    {
        public int skill_id { get; set; }
        public string skill_name { get; set; }
        public string skill_level { get; set; }
    }
}
