using AIJobCareer.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;

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

            // Seed Areas (10)
            modelBuilder.Entity<Area>().HasData(
                new Area { AREA_ID = 1, AREA_NAME = "Kuching" },
                new Area { AREA_ID = 2, AREA_NAME = "Miri" },
                new Area { AREA_ID = 3, AREA_NAME = "Sibu" },
                new Area { AREA_ID = 4, AREA_NAME = "Bintulu" },
                new Area { AREA_ID = 5, AREA_NAME = "Samarahan" },
                new Area { AREA_ID = 6, AREA_NAME = "Sri Aman" },
                new Area { AREA_ID = 7, AREA_NAME = "Kapit" },
                new Area { AREA_ID = 8, AREA_NAME = "Limbang" },
                new Area { AREA_ID = 9, AREA_NAME = "Sarikei" },
                new Area { AREA_ID = 10, AREA_NAME = "Betong" }
            );

            // Seed Skills (10)
            modelBuilder.Entity<Skill>().HasData(
                new Skill { SKILL_ID = 1, SKILL_NAME = "C# Programming", SKILL_INFO = "Microsoft .NET framework development", SKILL_TYPE = "Technical", SKILL_LEVEL = "Advanced" },
                new Skill { SKILL_ID = 2, SKILL_NAME = "Database Management", SKILL_INFO = "SQL Server, MySQL, PostgreSQL", SKILL_TYPE = "Technical", SKILL_LEVEL = "Intermediate" },
                new Skill { SKILL_ID = 3, SKILL_NAME = "Project Management", SKILL_INFO = "Agile, Scrum, Kanban methodologies", SKILL_TYPE = "Management", SKILL_LEVEL = "Advanced" },
                new Skill { SKILL_ID = 4, SKILL_NAME = "Web Development", SKILL_INFO = "HTML, CSS, JavaScript", SKILL_TYPE = "Technical", SKILL_LEVEL = "Advanced" },
                new Skill { SKILL_ID = 5, SKILL_NAME = "Petroleum Engineering", SKILL_INFO = "Oil and gas extraction techniques", SKILL_TYPE = "Technical", SKILL_LEVEL = "Expert" },
                new Skill { SKILL_ID = 6, SKILL_NAME = "Digital Marketing", SKILL_INFO = "SEO, SEM, Social Media Marketing", SKILL_TYPE = "Marketing", SKILL_LEVEL = "Intermediate" },
                new Skill { SKILL_ID = 7, SKILL_NAME = "Forestry Management", SKILL_INFO = "Sustainable forest management practices", SKILL_TYPE = "Environmental", SKILL_LEVEL = "Advanced" },
                new Skill { SKILL_ID = 8, SKILL_NAME = "Tourism & Hospitality", SKILL_INFO = "Customer service and hospitality management", SKILL_TYPE = "Service", SKILL_LEVEL = "Intermediate" },
                new Skill { SKILL_ID = 9, SKILL_NAME = "Indigenous Culture Knowledge", SKILL_INFO = "Understanding of Sarawak's indigenous cultures", SKILL_TYPE = "Cultural", SKILL_LEVEL = "Expert" },
                new Skill { SKILL_ID = 10, SKILL_NAME = "Agricultural Science", SKILL_INFO = "Tropical agriculture techniques", SKILL_TYPE = "Agricultural", SKILL_LEVEL = "Advanced" }
            );

            // Seed Companies (4)
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    COMPANY_ID = 1,
                    COMPANY_NAME = "Sarawak Energy Berhad",
                    COMPANY_ICON = "sarawak_energy_icon.png",
                    COMPANY_INTRO = "Leading energy provider in Sarawak focusing on renewable energy sources.",
                    COMPANY_WEBSITE = "https://www.sarawakenergy.com",
                    COMPANY_AREA_ID = 1 // Kuching
                },
                new Company
                {
                    COMPANY_ID = 2,
                    COMPANY_NAME = "Petronas Carigali Sdn Bhd",
                    COMPANY_ICON = "petronas_icon.png",
                    COMPANY_INTRO = "Oil and gas exploration company operating in Sarawak's offshore regions.",
                    COMPANY_WEBSITE = "https://www.petronas.com",
                    COMPANY_AREA_ID = 2 // Miri
                },
                new Company
                {
                    COMPANY_ID = 3,
                    COMPANY_NAME = "Sarawak Forestry Corporation",
                    COMPANY_ICON = "sfc_icon.png",
                    COMPANY_INTRO = "Responsible for sustainable management of Sarawak's forest resources.",
                    COMPANY_WEBSITE = "https://www.sarawakforestry.com",
                    COMPANY_AREA_ID = 1 // Kuching
                },
                new Company
                {
                    COMPANY_ID = 4,
                    COMPANY_NAME = "Sarawak Digital Economy Corporation",
                    COMPANY_ICON = "sdec_icon.png",
                    COMPANY_INTRO = "Driving digital transformation and innovation across Sarawak.",
                    COMPANY_WEBSITE = "https://www.sdec.com.my",
                    COMPANY_AREA_ID = 5 // Samarahan
                }
            );    
            
            // Seed Users (3)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    USER_ID = 1,
                    USER_NAME = "Ahmad bin Ibrahim",
                    USER_AGE = 28,
                    USER_INTRO = "Software developer with 5 years of experience in web technologies.",
                    USER_CONTACT_NUMBER = "0198765432",
                    USER_EMAIL = "ahmad@example.com",
                    USER_PASSWORD = "hashed_password_1", // In reality, this should be properly hashed
                    USER_ICON = "ahmad_profile.jpg",
                    USER_PRIVACY_STATUS = "Public",
                    USER_ROLE = "JobSeeker",
                    USER_ACCOUNT_CREATED_TIME = DateTime.Now.AddMonths(-6),
                    USER_AREA_ID = 1 // Kuching
                },
                new User
                {
                    USER_ID = 2,
                    USER_NAME = "Siti Nur Aminah",
                    USER_AGE = 32,
                    USER_INTRO = "Environmental scientist with expertise in tropical forest conservation.",
                    USER_CONTACT_NUMBER = "0123456789",
                    USER_EMAIL = "siti@example.com",
                    USER_PASSWORD = "hashed_password_2", // In reality, this should be properly hashed
                    USER_ICON = "siti_profile.jpg",
                    USER_PRIVACY_STATUS = "Limited",
                    USER_ROLE = "JobSeeker",
                    USER_ACCOUNT_CREATED_TIME = DateTime.Now.AddMonths(-3),
                    USER_AREA_ID = 3 // Sibu
                },
                new User
                {
                    USER_ID = 3,
                    USER_NAME = "Rajesh Kumar",
                    USER_AGE = 35,
                    USER_INTRO = "Petroleum engineer with 10 years experience in offshore drilling.",
                    USER_CONTACT_NUMBER = "0167890123",
                    USER_EMAIL = "rajesh@example.com",
                    USER_PASSWORD = "hashed_password_3", // In reality, this should be properly hashed
                    USER_ICON = "rajesh_profile.jpg",
                    USER_PRIVACY_STATUS = "Public",
                    USER_ROLE = "JobSeeker",
                    USER_ACCOUNT_CREATED_TIME = DateTime.Now.AddMonths(-9),
                    USER_AREA_ID = 2 // Miri
                }
            );
            
            // Seed Jobs (6)
            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    JOB_ID = 1,
                    JOB_COMPANY_ID = 1,
                    JOB_TITLE = "Senior Software Developer",
                    JOB_RESPONSIBLE = "Develop and maintain enterprise software applications for energy management systems.",
                    JOB_SALARY_MIN = 5000.00M,
                    JOB_SALARY_MAX = 7500.00M,
                    JOB_LOCATION = "Kuching, Sarawak",
                    JOB_STATUS = "Open",
                    JOB_BENEFIT = "Health insurance, performance bonus, professional development allowance.",
                    JOB_REQUIREMENT = "Bachelor's degree in Computer Science, 5+ years experience in software development."
                },
                new Job
                {
                    JOB_ID = 2,
                    JOB_COMPANY_ID = 2,
                    JOB_TITLE = "Petroleum Engineer",
                    JOB_RESPONSIBLE = "Oversee drilling operations and optimize oil extraction processes.",
                    JOB_SALARY_MIN = 6500.00M,
                    JOB_SALARY_MAX = 9000.00M,
                    JOB_LOCATION = "Miri, Sarawak",
                    JOB_STATUS = "Open",
                    JOB_BENEFIT = "Housing allowance, transportation, medical coverage, annual bonus.",
                    JOB_REQUIREMENT = "Bachelor's degree in Petroleum Engineering, 5+ years field experience."
                },
                new Job
                {
                    JOB_ID = 3,
                    JOB_COMPANY_ID = 3,
                    JOB_TITLE = "Forest Conservation Officer",
                    JOB_RESPONSIBLE = "Implement and monitor forest conservation programs across Sarawak.",
                    JOB_SALARY_MIN = 4000.00M,
                    JOB_SALARY_MAX = 5500.00M,
                    JOB_LOCATION = "Kuching, Sarawak (with field work)",
                    JOB_STATUS = "Open",
                    JOB_BENEFIT = "Field allowance, government pension scheme, paid study leave.",
                    JOB_REQUIREMENT = "Bachelor's degree in Forestry or Environmental Science, knowledge of local ecosystems."
                },
                new Job
                {
                    JOB_ID = 4,
                    JOB_COMPANY_ID = 4,
                    JOB_TITLE = "Digital Marketing Specialist",
                    JOB_RESPONSIBLE = "Develop and implement digital marketing strategies for Sarawak's digital initiatives.",
                    JOB_SALARY_MIN = 3500.00M,
                    JOB_SALARY_MAX = 5000.00M,
                    JOB_LOCATION = "Samarahan, Sarawak",
                    JOB_STATUS = "Open",
                    JOB_BENEFIT = "Performance bonuses, flexible working arrangements, training opportunities.",
                    JOB_REQUIREMENT = "Bachelor's degree in Marketing or Communications, experience with digital marketing tools."
                },
                new Job
                {
                    JOB_ID = 5,
                    JOB_COMPANY_ID = 1,
                    JOB_TITLE = "Renewable Energy Analyst",
                    JOB_RESPONSIBLE = "Analyze renewable energy projects and prepare feasibility reports.",
                    JOB_SALARY_MIN = 4500.00M,
                    JOB_SALARY_MAX = 6000.00M,
                    JOB_LOCATION = "Kuching, Sarawak",
                    JOB_STATUS = "Open",
                    JOB_BENEFIT = "Professional development fund, health insurance, performance bonus.",
                    JOB_REQUIREMENT = "Bachelor's degree in Environmental Engineering or related field, knowledge of renewable energy technologies."
                },
                new Job
                {
                    JOB_ID = 6,
                    JOB_COMPANY_ID = 4,
                    JOB_TITLE = "Full Stack Developer",
                    JOB_RESPONSIBLE = "Design and develop web applications for Sarawak's digital economy initiatives.",
                    JOB_SALARY_MIN = 4800.00M,
                    JOB_SALARY_MAX = 7000.00M,
                    JOB_LOCATION = "Samarahan, Sarawak",
                    JOB_STATUS = "Open",
                    JOB_BENEFIT = "Remote work options, medical coverage, professional development.",
                    JOB_REQUIREMENT = "Bachelor's degree in Computer Science, proficiency in front-end and back-end technologies."
                }
            );

            // Seed Resume (3)
            modelBuilder.Entity<Resume>().HasData(
                new Resume
                {
                    RESUME_ID = 1,
                    RESUME_USER_ID = 1,
                    RESUME_TEXT = "Experienced software developer with expertise in .NET Core, React, and cloud technologies. Worked on enterprise applications for energy sector.",
                    RESUME_FILE = "ahmad_resume.pdf",
                    RESUME_LAST_MODIFY_TIME = DateTime.Now.AddDays(-15)
                },
                new Resume
                {
                    RESUME_ID = 2,
                    RESUME_USER_ID = 2,
                    RESUME_TEXT = "Environmental scientist focused on forest conservation. Experience in GIS mapping, biodiversity assessment, and sustainable forest management practices.",
                    RESUME_FILE = "siti_resume.pdf",
                    RESUME_LAST_MODIFY_TIME = DateTime.Now.AddDays(-7)
                },
                new Resume
                {
                    RESUME_ID = 3,
                    RESUME_USER_ID = 3,
                    RESUME_TEXT = "Petroleum engineer with extensive experience in offshore drilling. Skills include reservoir analysis, production optimization, and HSE compliance.",
                    RESUME_FILE = "rajesh_resume.pdf",
                    RESUME_LAST_MODIFY_TIME = DateTime.Now.AddDays(-21)
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
                    US_USER_ID = 1, // Ahmad bin Ibrahim
                    US_SKILL_ID = 1 // C# Programming
                },
                new UserSkill
                {
                    US_ID = 2,
                    US_USER_ID = 2, // Siti Nur Aminah
                    US_SKILL_ID = 7 // Forestry Management
                },
                new UserSkill
                {
                    US_ID = 3,
                    US_USER_ID = 3, // Rajesh Kumar
                    US_SKILL_ID = 5 // Petroleum Engineering
                }
            );

            // Seed JobApplication (3)
            modelBuilder.Entity<JobApplication>().HasData(
                new JobApplication
                {
                    APPLICATION_ID = 1,
                    APPLICATION_JOB_ID = 1,
                    APPLICATION_TYPE = "Full Time",
                    APPLICATION_STATUS = "Under Review",
                    APPLICATION_SUBMISSION_DATE = DateTime.Now.AddDays(-10)
                },
                new JobApplication
                {
                    APPLICATION_ID = 2,
                    APPLICATION_JOB_ID = 3,
                    APPLICATION_TYPE = "Full Time",
                    APPLICATION_STATUS = "Interview Scheduled",
                    APPLICATION_SUBMISSION_DATE = DateTime.Now.AddDays(-15)
                },
                new JobApplication
                {
                    APPLICATION_ID = 3,
                    APPLICATION_JOB_ID = 2,
                    APPLICATION_TYPE = "Contract",
                    APPLICATION_STATUS = "Under Review",
                    APPLICATION_SUBMISSION_DATE = DateTime.Now.AddDays(-5)
                }
            );

            // Seed UserApplication (3)
            modelBuilder.Entity<UserApplication>().HasData(
                new UserApplication
                {
                    UA_ID = 1,
                    UA_USER_ID = 1,
                    UA_APPLICATION_ID = 1 // Ahmad applied for Senior Software Developer
                },
                new UserApplication
                {
                    UA_ID = 2,
                    UA_USER_ID = 2,
                    UA_APPLICATION_ID = 2 // Siti applied for Forest Conservation Officer
                },
                new UserApplication
                {
                    UA_ID = 3,
                    UA_USER_ID = 3,
                    UA_APPLICATION_ID = 3 // Rajesh applied for Petroleum Engineer
                }
            );

            // Seed Notification (3)
            modelBuilder.Entity<Notification>().HasData(
                new Notification
                {
                    NOTIFICATION_ID = 1,
                    NOTIFICATION_USER_ID = 1,
                    NOTIFICATION_COMPANY_ID = 1,
                    NOTIFICATION_TEXT = "Your application for Senior Software Developer has been received. We will review it shortly.",
                    NOTIFICATION_TIMESTAMP = DateTime.Now.AddDays(-10),
                    NOTIFICATION_STATUS = "Unread"
                },
                new Notification
                {
                    NOTIFICATION_ID = 2,
                    NOTIFICATION_USER_ID = 2,
                    NOTIFICATION_COMPANY_ID = 3,
                    NOTIFICATION_TEXT = "You have been shortlisted for the Forest Conservation Officer position. Please prepare for an interview.",
                    NOTIFICATION_TIMESTAMP = DateTime.Now.AddDays(-8),
                    NOTIFICATION_STATUS = "Read"
                },
                new Notification
                {
                    NOTIFICATION_ID = 3,
                    NOTIFICATION_USER_ID = 3,
                    NOTIFICATION_COMPANY_ID = 2,
                    NOTIFICATION_TEXT = "Thank you for your application to Petronas Carigali. Your application is under review.",
                    NOTIFICATION_TIMESTAMP = DateTime.Now.AddDays(-5),
                    NOTIFICATION_STATUS = "Unread"
                }
            );

            // Seed CareerAnalysis (3)
            modelBuilder.Entity<CareerAnalysis>().HasData(
                new CareerAnalysis
                {
                    ANALYSIS_ID = 1,
                    ANALYSIS_USER_ID = 1,
                    ANALYSIS_AI_DIRECTION = "Based on your skills and experience, you have strong potential in software development. Consider specializing in energy sector applications or cloud technologies.",
                    ANALYSIS_AI_MARKET_GAP = "There is growing demand for developers with expertise in renewable energy systems in Sarawak. Consider upskilling in this area.",
                    ANALYSIS_AI_CAREER_PROSPECTS = "High potential for career growth in Sarawak's emerging digital economy. Projected salary increase of 15-20% in the next 3 years."
                },
                new CareerAnalysis
                {
                    ANALYSIS_ID = 2,
                    ANALYSIS_USER_ID = 2,
                    ANALYSIS_AI_DIRECTION = "Your environmental science background positions you well for conservation roles. Consider gaining project management certification.",
                    ANALYSIS_AI_MARKET_GAP = "Sarawak has increasing needs for environmental impact assessment specialists for sustainable development projects.",
                    ANALYSIS_AI_CAREER_PROSPECTS = "Strong demand for conservation experts in both government and private sectors in Sarawak over the next 5 years."
                },
                new CareerAnalysis
                {
                    ANALYSIS_ID = 3,
                    ANALYSIS_USER_ID = 3,
                    ANALYSIS_AI_DIRECTION = "Your petroleum engineering experience is valuable. Consider expanding into renewable energy transition projects.",
                    ANALYSIS_AI_MARKET_GAP = "There is growing need for engineers who can bridge traditional oil & gas with renewable energy projects in Sarawak.",
                    ANALYSIS_AI_CAREER_PROSPECTS = "Stable career prospects in Miri, with opportunities to transition to leadership roles in the next 2-3 years."
                }
            );

            // Seed JobApplicationReview (3)
            modelBuilder.Entity<JobApplicationReview>().HasData(
                new JobApplicationReview
                {
                    REVIEW_ID = 1,
                    REVIEW_APPLICATION_ID = 1,
                    REVIEW_COMPANY_ID = 1,
                    REVIEW_STATUS = "Positive",
                    REVIEW_CONTEXT = "Strong technical background and relevant experience. Recommended for interview.",
                    REVIEW_DATE = DateTime.Now.AddDays(-5)
                },
                new JobApplicationReview
                {
                    REVIEW_ID = 2,
                    REVIEW_APPLICATION_ID = 2,
                    REVIEW_COMPANY_ID = 3,
                    REVIEW_STATUS = "Very Positive",
                    REVIEW_CONTEXT = "Excellent match for the position. Scientific background and conservation experience are ideal.",
                    REVIEW_DATE = DateTime.Now.AddDays(-10)
                },
                new JobApplicationReview
                {
                    REVIEW_ID = 3,
                    REVIEW_APPLICATION_ID = 3,
                    REVIEW_COMPANY_ID = 2,
                    REVIEW_STATUS = "Neutral",
                    REVIEW_CONTEXT = "Good experience but may need additional training in offshore safety protocols.",
                    REVIEW_DATE = DateTime.Now.AddDays(-3)
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