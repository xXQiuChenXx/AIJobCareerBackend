using AIJobCareer.Models;
using Microsoft.EntityFrameworkCore;
namespace AIJobCareer.Data
{
    public class ApplicationDBContext : DbContext

    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)

          : base(options)

        {

        }

        public DbSet<Area> Area { get; set; }
        public DbSet<CareerAnalysis> Career_Analysis { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<JobApplication> Job_Application { get; set; }
        public DbSet<JobApplicationReview> Job_Application_Review { get; set; }
        public DbSet<JobApplicationTable> Job_Application_Table { get; set; }
        public DbSet<JobSkill> Job_Skill { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Resume> Resume { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserApplication> User_Application { get; set; }
        public DbSet<UserSkill> USer_Skill { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);

            Guid userId1 = Guid.NewGuid();
            Guid userId2 = Guid.NewGuid();
            Guid userId3 = Guid.NewGuid();

            // Seed Areas (10)
            modelBuilder.Entity<Area>().HasData(
                new Area { area_id = 1, area_name = "Kuching" },
                new Area { area_id = 2, area_name = "Miri" },
                new Area { area_id = 3, area_name = "Sibu" },
                new Area { area_id = 4, area_name = "Bintulu" },
                new Area { area_id = 5, area_name = "Samarahan" },
                new Area { area_id = 6, area_name = "Sri Aman" },
                new Area { area_id = 7, area_name = "Kapit" },
                new Area { area_id = 8, area_name = "Limbang" },
                new Area { area_id = 9, area_name = "Sarikei" },
                new Area { area_id = 10, area_name = "Betong" }
            );

            // Seed Skills (10)
            modelBuilder.Entity<Skill>().HasData(
                new Skill { skill_id = 1, skill_name = "C# Programming", skill_info = "Microsoft .NET framework development", skill_type = "Technical", skill_level = "Advanced" },
                new Skill { skill_id = 2, skill_name = "Database Management", skill_info = "SQL Server, MySQL, PostgreSQL", skill_type = "Technical", skill_level = "Intermediate" },
                new Skill { skill_id = 3, skill_name = "Project Management", skill_info = "Agile, Scrum, Kanban methodologies", skill_type = "Management", skill_level = "Advanced" },
                new Skill { skill_id = 4, skill_name = "Web Development", skill_info = "HTML, CSS, JavaScript", skill_type = "Technical", skill_level = "Advanced" },
                new Skill { skill_id = 5, skill_name = "Petroleum Engineering", skill_info = "Oil and gas extraction techniques", skill_type = "Technical", skill_level = "Expert" },
                new Skill { skill_id = 6, skill_name = "Digital Marketing", skill_info = "SEO, SEM, Social Media Marketing", skill_type = "Marketing", skill_level = "Intermediate" },
                new Skill { skill_id = 7, skill_name = "Forestry Management", skill_info = "Sustainable forest management practices", skill_type = "Environmental", skill_level = "Advanced" },
                new Skill { skill_id = 8, skill_name = "Tourism & Hospitality", skill_info = "Customer service and hospitality management", skill_type = "Service", skill_level = "Intermediate" },
                new Skill { skill_id = 9, skill_name = "Indigenous Culture Knowledge", skill_info = "Understanding of Sarawak's indigenous cultures", skill_type = "Cultural", skill_level = "Expert" },
                new Skill { skill_id = 10, skill_name = "Agricultural Science", skill_info = "Tropical agriculture techniques", skill_type = "Agricultural", skill_level = "Advanced" }
            );

            // Seed Companies (4)
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    company_id = 1,
                    company_name = "Sarawak Energy Berhad",
                    company_icon = "sarawak_energy_icon.png",
                    company_intro = "Leading energy provider in Sarawak focusing on renewable energy sources.",
                    company_website = "https://www.sarawakenergy.com",
                    company_area_id = 1 // Kuching
                },
                new Company
                {
                    company_id = 2,
                    company_name = "Petronas Carigali Sdn Bhd",
                    company_icon = "petronas_icon.png",
                    company_intro = "Oil and gas exploration company operating in Sarawak's offshore regions.",
                    company_website = "https://www.petronas.com",
                    company_area_id = 2 // Miri
                },
                new Company
                {
                    company_id = 3,
                    company_name = "Sarawak Forestry Corporation",
                    company_icon = "sfc_icon.png",
                    company_intro = "Responsible for sustainable management of Sarawak's forest resources.",
                    company_website = "https://www.sarawakforestry.com",
                    company_area_id = 1 // Kuching
                },
                new Company
                {
                    company_id = 4,
                    company_name = "Sarawak Digital Economy Corporation",
                    company_icon = "sdec_icon.png",
                    company_intro = "Driving digital transformation and innovation across Sarawak.",
                    company_website = "https://www.sdec.com.my",
                    company_area_id = 5 // Samarahan
                }
            );

            // Seed Users (3)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    user_id = userId1,
                    user_first_name = "Ahmad",
                    user_last_name = "bin Ibrahim",
                    username = "ahmad_ibrahim",
                    user_age = 28,
                    user_intro = "Software developer with 5 years of experience in web technologies.",
                    user_contact_number = "0198765432",
                    user_email = "ahmad@example.com",
                    user_password = "hashed_password_1", // In reality, this should be properly hashed
                    user_icon = "ahmad_profile.jpg",
                    user_privacy_status = "Public",
                    user_role = "JobSeeker",
                    user_account_created_time = DateTime.Now.AddMonths(-6),
                    user_area_id = 1 // Kuching
                },
                new User
                {
                    user_id = userId2,
                    user_first_name = "Siti",
                    user_last_name = "Nur Aminah",
                    username = "siti_aminah",
                    user_age = 32,
                    user_intro = "Environmental scientist with expertise in tropical forest conservation.",
                    user_contact_number = "0123456789",
                    user_email = "siti@example.com",
                    user_password = "hashed_password_2", // In reality, this should be properly hashed
                    user_icon = "siti_profile.jpg",
                    user_privacy_status = "Limited",
                    user_role = "JobSeeker",
                    user_account_created_time = DateTime.Now.AddMonths(-3),
                    user_area_id = 3 // Sibu
                },
                new User
                {
                    user_id = userId3,
                    user_first_name = "Rajesh",
                    user_last_name = "Kumar",
                    username = "rajesh_kumar",
                    user_age = 35,
                    user_intro = "Petroleum engineer with 10 years experience in offshore drilling.",
                    user_contact_number = "0167890123",
                    user_email = "rajesh@example.com",
                    user_password = "hashed_password_3", // In reality, this should be properly hashed
                    user_icon = "rajesh_profile.jpg",
                    user_privacy_status = "Public",
                    user_role = "JobSeeker",
                    user_account_created_time = DateTime.Now.AddMonths(-9),
                    user_area_id = 2 // Miri
                }
            );

            // Seed Jobs (6)
            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    job_id = 1,
                    job_company_id = 1,
                    job_title = "Senior Software Developer",
                    job_responsible = "Develop and maintain enterprise software applications for energy management systems.",
                    job_salary_min = 5000.00M,
                    job_salary_max = 7500.00M,
                    job_location = "Kuching, Sarawak",
                    job_status = "Open",
                    job_benefit = "Health insurance, performance bonus, professional development allowance.",
                    job_requirement = "Bachelor's degree in Computer Science, 5+ years experience in software development."
                },
                new Job
                {
                    job_id = 2,
                    job_company_id = 2,
                    job_title = "Petroleum Engineer",
                    job_responsible = "Oversee drilling operations and optimize oil extraction processes.",
                    job_salary_min = 6500.00M,
                    job_salary_max = 9000.00M,
                    job_location = "Miri, Sarawak",
                    job_status = "Open",
                    job_benefit = "Housing allowance, transportation, medical coverage, annual bonus.",
                    job_requirement = "Bachelor's degree in Petroleum Engineering, 5+ years field experience."
                },
                new Job
                {
                    job_id = 3,
                    job_company_id = 3,
                    job_title = "Forest Conservation Officer",
                    job_responsible = "Implement and monitor forest conservation programs across Sarawak.",
                    job_salary_min = 4000.00M,
                    job_salary_max = 5500.00M,
                    job_location = "Kuching, Sarawak (with field work)",
                    job_status = "Open",
                    job_benefit = "Field allowance, government pension scheme, paid study leave.",
                    job_requirement = "Bachelor's degree in Forestry or Environmental Science, knowledge of local ecosystems."
                },
                new Job
                {
                    job_id = 4,
                    job_company_id = 4,
                    job_title = "Digital Marketing Specialist",
                    job_responsible = "Develop and implement digital marketing strategies for Sarawak's digital initiatives.",
                    job_salary_min = 3500.00M,
                    job_salary_max = 5000.00M,
                    job_location = "Samarahan, Sarawak",
                    job_status = "Open",
                    job_benefit = "Performance bonuses, flexible working arrangements, training opportunities.",
                    job_requirement = "Bachelor's degree in Marketing or Communications, experience with digital marketing tools."
                },
                new Job
                {
                    job_id = 5,
                    job_company_id = 1,
                    job_title = "Renewable Energy Analyst",
                    job_responsible = "Analyze renewable energy projects and prepare feasibility reports.",
                    job_salary_min = 4500.00M,
                    job_salary_max = 6000.00M,
                    job_location = "Kuching, Sarawak",
                    job_status = "Open",
                    job_benefit = "Professional development fund, health insurance, performance bonus.",
                    job_requirement = "Bachelor's degree in Environmental Engineering or related field, knowledge of renewable energy technologies."
                },
                new Job
                {
                    job_id = 6,
                    job_company_id = 4,
                    job_title = "Full Stack Developer",
                    job_responsible = "Design and develop web applications for Sarawak's digital economy initiatives.",
                    job_salary_min = 4800.00M,
                    job_salary_max = 7000.00M,
                    job_location = "Samarahan, Sarawak",
                    job_status = "Open",
                    job_benefit = "Remote work options, medical coverage, professional development.",
                    job_requirement = "Bachelor's degree in Computer Science, proficiency in front-end and back-end technologies."
                }
            );

            // Seed Resume (3)
            modelBuilder.Entity<Resume>().HasData(
                new Resume
                {
                    resume_id = 1,
                    resume_user_id = userId1,
                    resume_text = "Experienced software developer with expertise in .NET Core, React, and cloud technologies. Worked on enterprise applications for energy sector.",
                    resume_file = "ahmad_resume.pdf",
                    resume_last_modify_time = DateTime.Now.AddDays(-15)
                },
                new Resume
                {
                    resume_id = 2,
                    resume_user_id = userId2,
                    resume_text = "Environmental scientist focused on forest conservation. Experience in GIS mapping, biodiversity assessment, and sustainable forest management practices.",
                    resume_file = "siti_resume.pdf",
                    resume_last_modify_time = DateTime.Now.AddDays(-7)
                },
                new Resume
                {
                    resume_id = 3,
                    resume_user_id = userId3,
                    resume_text = "Petroleum engineer with extensive experience in offshore drilling. Skills include reservoir analysis, production optimization, and HSE compliance.",
                    resume_file = "rajesh_resume.pdf",
                    resume_last_modify_time = DateTime.Now.AddDays(-21)
                }
            );

            // Seed JobSkill (6)
            modelBuilder.Entity<JobSkill>().HasData(
                new JobSkill
                {
                    JS_ID = 1,
                    JS_JOB_ID = 1, // Senior Software Developer at Sarawak Energy
                    JS_SKILL_ID = 1 // C# Programming
                },
                new JobSkill
                {
                    JS_ID = 2,
                    JS_JOB_ID = 1, // Senior Software Developer at Sarawak Energy
                    JS_SKILL_ID = 4 // Web Development
                },
                new JobSkill
                {
                    JS_ID = 3,
                    JS_JOB_ID = 2, // Petroleum Engineer at Petronas Carigali
                    JS_SKILL_ID = 5 // Petroleum Engineering
                },
                new JobSkill
                {
                    JS_ID = 4,
                    JS_JOB_ID = 3, // Forest Conservation Officer at Sarawak Forestry
                    JS_SKILL_ID = 7 // Forestry Management
                },
                new JobSkill
                {
                    JS_ID = 5,
                    JS_JOB_ID = 4, // Digital Marketing Specialist at SDEC
                    JS_SKILL_ID = 6 // Digital Marketing
                },
                new JobSkill
                {
                    JS_ID = 6,
                    JS_JOB_ID = 6, // Full Stack Developer at SDEC
                    JS_SKILL_ID = 4 // Web Development
                }
            );

            // Seed UserSkill (3)
            modelBuilder.Entity<UserSkill>().HasData(
                new UserSkill
                {
                    US_ID = 1,
                    US_USER_ID = userId1, // Ahmad bin Ibrahim
                    US_SKILL_ID = 1 // C# Programming
                },
                new UserSkill
                {
                    US_ID = 2,
                    US_USER_ID = userId2, // Siti Nur Aminah
                    US_SKILL_ID = 7 // Forestry Management
                },
                new UserSkill
                {
                    US_ID = 3,
                    US_USER_ID = userId3, // Rajesh Kumar
                    US_SKILL_ID = 5 // Petroleum Engineering
                }
            );

            // Seed JobApplication (3)
            modelBuilder.Entity<JobApplication>().HasData(
                new JobApplication
                {
                    application_id = 1,
                    application_job_id = 1,
                    application_type = "Full Time",
                    application_status = "Under Review",
                    application_submission_date = DateTime.Now.AddDays(-10)
                },
                new JobApplication
                {
                    application_id = 2,
                    application_job_id = 3,
                    application_type = "Full Time",
                    application_status = "Interview Scheduled",
                    application_submission_date = DateTime.Now.AddDays(-15)
                },
                new JobApplication
                {
                    application_id = 3,
                    application_job_id = 2,
                    application_type = "Contract",
                    application_status = "Under Review",
                    application_submission_date = DateTime.Now.AddDays(-5)
                }
            );

            // Seed UserApplication (3)
            modelBuilder.Entity<UserApplication>().HasData(
                new UserApplication
                {
                    UA_ID = 1,
                    UA_USER_ID = userId1,
                    UA_APPLICATION_ID = 1 // Ahmad applied for Senior Software Developer
                },
                new UserApplication
                {
                    UA_ID = 2,
                    UA_USER_ID = userId2,
                    UA_APPLICATION_ID = 2 // Siti applied for Forest Conservation Officer
                },
                new UserApplication
                {
                    UA_ID = 3,
                    UA_USER_ID = userId3,
                    UA_APPLICATION_ID = 3 // Rajesh applied for Petroleum Engineer
                }
            );

            modelBuilder.Entity<Notification>().HasData(
                new Notification
                {
                    notification_id = 1,
                    notification_user_id = 1,
                    notification_company_id = 1,
                    notification_text = "Your application for Senior Software Developer has been received. We will review it shortly.",
                    notification_timestamp = DateTime.Now.AddDays(-10),
                    notification_status = "Unread"
                },
                new Notification
                {
                    notification_id = 2,
                    notification_user_id = 2,
                    notification_company_id = 3,
                    notification_text = "You have been shortlisted for the Forest Conservation Officer position. Please prepare for an interview.",
                    notification_timestamp = DateTime.Now.AddDays(-8),
                    notification_status = "Read"
                },
                new Notification
                {
                    notification_id = 3,
                    notification_user_id = 3,
                    notification_company_id = 2,
                    notification_text = "Thank you for your application to Petronas Carigali. Your application is under review.",
                    notification_timestamp = DateTime.Now.AddDays(-5),
                    notification_status = "Unread"
                }
           );

            // Seed CareerAnalysis (3)
            modelBuilder.Entity<CareerAnalysis>().HasData(
                new CareerAnalysis
                {
                    analysis_id = 1,
                    analysis_user_id = 1,
                    analysis_ai_direction = "Based on your skills and experience, you have strong potential in software development. Consider specializing in energy sector applications or cloud technologies.",
                    analysis_ai_market_gap = "There is growing demand for developers with expertise in renewable energy systems in Sarawak. Consider upskilling in this area.",
                    analysis_ai_career_prospects = "High potential for career growth in Sarawak's emerging digital economy. Projected salary increase of 15-20% in the next 3 years."
                },
                new CareerAnalysis
                {
                    analysis_id = 2,
                    analysis_user_id = 2,
                    analysis_ai_direction = "Your environmental science background positions you well for conservation roles. Consider gaining project management certification.",
                    analysis_ai_market_gap = "Sarawak has increasing needs for environmental impact assessment specialists for sustainable development projects.",
                    analysis_ai_career_prospects = "Strong demand for conservation experts in both government and private sectors in Sarawak over the next 5 years."
                },
                new CareerAnalysis
                {
                    analysis_id = 3,
                    analysis_user_id = 3,
                    analysis_ai_direction = "Your petroleum engineering experience is valuable. Consider expanding into renewable energy transition projects.",
                    analysis_ai_market_gap = "There is growing need for engineers who can bridge traditional oil & gas with renewable energy projects in Sarawak.",
                    analysis_ai_career_prospects = "Stable career prospects in Miri, with opportunities to transition to leadership roles in the next 2-3 years."
                }
            );

            // Seed JobApplicationReview (3)
            modelBuilder.Entity<JobApplicationReview>().HasData(
                new JobApplicationReview
                {
                    review_id = 1,
                    review_application_id = 1,
                    review_company_id = 1,
                    review_status = "Positive",
                    review_context = "Strong technical background and relevant experience. Recommended for interview.",
                    review_date = DateTime.Now.AddDays(-5)
                },
                new JobApplicationReview
                {
                    review_id = 2,
                    review_application_id = 2,
                    review_company_id = 3,
                    review_status = "Very Positive",
                    review_context = "Excellent match for the position. Scientific background and conservation experience are ideal.",
                    review_date = DateTime.Now.AddDays(-10)
                },
                new JobApplicationReview
                {
                    review_id = 3,
                    review_application_id = 3,
                    review_company_id = 2,
                    review_status = "Neutral",
                    review_context = "Good experience but may need additional training in offshore safety protocols.",
                    review_date = DateTime.Now.AddDays(-3)
                }
            );

            // Seed JobApplicationTable (3)
            modelBuilder.Entity<JobApplicationTable>().HasData(
                new JobApplicationTable
                {
                    TABLE_ID = 1,
                    TABLE_APPLICATION_ID = 1,
                    TABLE_RESUME_ID = 1,
                    TABLE_COVER_LETTER = "I am excited to apply for the Senior Software Developer position at Sarawak Energy. My experience developing enterprise applications aligns perfectly with your requirements."
                },
                new JobApplicationTable
                {
                    TABLE_ID = 2,
                    TABLE_APPLICATION_ID = 2,
                    TABLE_RESUME_ID = 2,
                    TABLE_COVER_LETTER = "As an environmental scientist passionate about forest conservation, I am eager to contribute to Sarawak Forestry Corporation's mission of sustainable forest management."
                },
                new JobApplicationTable
                {
                    TABLE_ID = 3,
                    TABLE_APPLICATION_ID = 3,
                    TABLE_RESUME_ID = 3,
                    TABLE_COVER_LETTER = "With my decade of experience in petroleum engineering, I am confident in my ability to contribute to Petronas Carigali's operations in Sarawak's offshore fields."
                }
            );
        }
    }
}