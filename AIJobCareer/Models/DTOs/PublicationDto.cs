using System;

namespace AIJobCareer.DTOs.Publication
{
    public class PublicationDto
    {
        public Guid publication_id { get; set; }
        public Guid? user_id { get; set; }
        public string publication_title { get; set; }
        public string publisher { get; set; }
        public int publication_year { get; set; }
        public string publication_url { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class CreatePublicationDto
    {
        public Guid user_id { get; set; }
        public string publication_title { get; set; }
        public string publisher { get; set; }
        public int publication_year { get; set; }
        public string publication_url { get; set; }
        public string description { get; set; }
    }

    public class UpdatePublicationDto
    {
        public Guid publication_id { get; set; }
        public string publication_title { get; set; }
        public string publisher { get; set; }
        public int publication_year { get; set; }
        public string publication_url { get; set; }
        public string description { get; set; }
    }
}