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
        public DbSet<Project> Project { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Publication> Publication { get; set; }
        public DbSet<Certification> Certification { get; set; }
        public DbSet<WorkExperience> Work_Experience { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);

            // Area relationships
            modelBuilder.Entity<Area>()
                .HasMany(a => a.users)
                .WithOne(u => u.Area)
                .HasForeignKey(u => u.user_area_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Area>()
                .HasMany(a => a.companies)
                .WithOne(c => c.Area)
                .HasForeignKey(c => c.company_area_id)
                .OnDelete(DeleteBehavior.Restrict);

            // User relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserSkills)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.US_USER_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Resumes)
                .WithOne(r => r.user)
                .HasForeignKey(r => r.resume_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CareerAnalyses)
                .WithOne(ca => ca.User)
                .HasForeignKey(ca => ca.analysis_user_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserApplications)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UA_USER_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.user)
                .HasForeignKey(n => n.notification_user_id)
                .OnDelete(DeleteBehavior.Cascade);

            // WorkExperience relationships
            modelBuilder.Entity<WorkExperience>()
                .HasOne(we => we.User)
                .WithMany(u => u.WorkExperiences)
                .HasForeignKey(we => we.user_id);

            // Skill relationships
            modelBuilder.Entity<Skill>()
                .HasMany(s => s.user_skills)
                .WithOne(us => us.Skill)
                .HasForeignKey(us => us.US_SKILL_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Skill>()
                .HasMany(s => s.job_skills)
                .WithOne(js => js.Skill)
                .HasForeignKey(js => js.JS_SKILL_ID)
                .OnDelete(DeleteBehavior.Cascade);

            // Education relationships
            modelBuilder.Entity<Education>()
                .HasOne(e => e.User)
                .WithMany(u => u.Educations)
                .HasForeignKey(e => e.user_id);

            // Certification relationships
            modelBuilder.Entity<Certification>()
                .HasOne(c => c.User)
                .WithMany(u => u.Certifications)
                .HasForeignKey(c => c.user_id);

            // Project relationships
            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.user_id);

            // Company relationships
            modelBuilder.Entity<Company>()
                .HasMany(c => c.jobs)
                .WithOne(j => j.company)
                .HasForeignKey(j => j.job_company_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.job_application_reviews)
                .WithOne(jar => jar.company)
                .HasForeignKey(jar => jar.review_company_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.notifications)
                .WithOne(n => n.company)
                .HasForeignKey(n => n.notification_company_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Job relationships
            modelBuilder.Entity<Job>()
                .HasMany(j => j.job_skills)
                .WithOne(js => js.Job)
                .HasForeignKey(js => js.JS_JOB_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Job>()
                .HasMany(j => j.job_application)
                .WithOne(ja => ja.job)
                .HasForeignKey(ja => ja.application_job_id)
                .OnDelete(DeleteBehavior.Cascade);

            // JobApplication relationships
            modelBuilder.Entity<JobApplication>()
                .HasMany(ja => ja.job_application_table)
                .WithOne(jat => jat.JobApplication)
                .HasForeignKey(jat => jat.TABLE_APPLICATION_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobApplication>()
                .HasMany(ja => ja.user_application)
                .WithOne(ua => ua.job_application)
                .HasForeignKey(ua => ua.UA_APPLICATION_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobApplication>()
                .HasMany(ja => ja.job_application_reviews)
                .WithOne(jar => jar.job_application)
                .HasForeignKey(jar => jar.review_application_id)
                .OnDelete(DeleteBehavior.Cascade);

            // Resume relationships
            modelBuilder.Entity<Resume>()
                .HasMany(r => r.job_application_tables)
                .WithOne(jat => jat.Resume)
                .HasForeignKey(jat => jat.TABLE_RESUME_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Resume>()
               .HasOne(r => r.user)
               .WithMany(u => u.Resumes)
               .HasForeignKey(r => r.resume_user_id)
               .OnDelete(DeleteBehavior.Cascade);

            // Configure table names to match SQL
            modelBuilder.Entity<Area>().ToTable("AREA");
            modelBuilder.Entity<User>().ToTable("USERS");
            modelBuilder.Entity<Skill>().ToTable("SKILL");
            modelBuilder.Entity<UserSkill>().ToTable("USER_SKILL");
            modelBuilder.Entity<Resume>().ToTable("RESUME");
            modelBuilder.Entity<CareerAnalysis>().ToTable("CAREER_ANALYSIS");
            modelBuilder.Entity<Company>().ToTable("COMPANY");
            modelBuilder.Entity<Job>().ToTable("JOB");
            modelBuilder.Entity<JobSkill>().ToTable("JOB_SKILL");
            modelBuilder.Entity<JobApplication>().ToTable("JOB_APPLICATION");
            modelBuilder.Entity<JobApplicationTable>().ToTable("JOB_APPLICATION_TABLE");
            modelBuilder.Entity<UserApplication>().ToTable("USER_APPLICATION");
            modelBuilder.Entity<JobApplicationReview>().ToTable("JOB_APPLICATION_REVIEW");
            modelBuilder.Entity<Notification>().ToTable("NOTIFICATION");

            // Seed data

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
                 company_area_id = 1, // Kuching
                 company_industry = "Energy & Utilities"
             },
              new Company
              {
                  company_id = 2,
                  company_name = "Petronas Carigali Sdn Bhd",
                  company_icon = "petronas_icon.png",
                  company_intro = "Oil and gas exploration company operating in Sarawak's offshore regions.",
                  company_website = "https://www.petronas.com",
                  company_area_id = 2, // Miri
                  company_industry = "Oil & Gas"
              },
              new Company
              {
                  company_id = 3,
                  company_name = "Sarawak Forestry Corporation",
                  company_icon = "sfc_icon.png",
                  company_intro = "Responsible for sustainable management of Sarawak's forest resources.",
                  company_website = "https://www.sarawakforestry.com",
                  company_area_id = 1, // Kuching
                  company_industry = "Forestry & Environmental Services"
              },
              new Company
              {
                  company_id = 4,
                  company_name = "Sarawak Digital Economy Corporation",
                  company_icon = "sdec_icon.png",
                  company_intro = "Driving digital transformation and innovation across Sarawak.",
                  company_website = "https://www.sdec.com.my",
                  company_area_id = 5, // Samarahan
                  company_industry = "Technology & Digital Services"
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
                    user_privacy_status = "public",
                    user_role = "job_seeker",
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
                    user_privacy_status = "private",
                    user_role = "job_seeker",
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
                    user_privacy_status = "public",
                    user_role = "job_seeker",
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
                   job_description = "Develop and maintain enterprise software applications for energy management systems.",
                   job_responsible = "Lead software development projects, mentor junior developers, and collaborate with stakeholders.",
                   job_salary_min = 5000.00M,
                   job_salary_max = 7500.00M,
                   job_type = JobType.Full_Time,
                   job_location = "Kuching, Sarawak",
                   job_status = "Open",
                   Posted_Date = DateTime.Now.AddDays(-20),
                   job_deadline = DateTime.Now.AddDays(30), // 45 days from posting
                   job_benefit = "Health insurance, performance bonus, professional development allowance.",
                   job_requirement = "Bachelor's degree in Computer Science, 5+ years experience in software development."
               },
                new Job
                {
                    job_id = 2,
                    job_company_id = 2,
                    job_title = "Petroleum Engineer",
                    job_description = "Design and implement strategies for efficient oil and gas extraction. Collaborate with multidisciplinary teams to solve complex drilling challenges.",
                    job_responsible = "Oversee drilling operations and optimize oil extraction processes.",
                    job_salary_min = 6500.00M,
                    job_salary_max = 9000.00M,
                    job_type = JobType.Contract,
                    job_location = "Miri, Sarawak",
                    job_status = "Open",
                    Posted_Date = DateTime.Now.AddDays(-10),
                    job_deadline = DateTime.Now.AddDays(34), // 40 days from posting
                    job_benefit = "Housing allowance, transportation, medical coverage, annual bonus.",
                    job_requirement = "Bachelor's degree in Petroleum Engineering, 5+ years field experience."
                },
                new Job
                {
                    job_id = 3,
                    job_company_id = 3,
                    job_type = JobType.Part_Time,
                    job_title = "Forest Conservation Officer",
                    job_description = "Monitor forest health, implement conservation programs, and work with local communities to promote sustainable forest management practices.",
                    job_responsible = "Implement and monitor forest conservation programs across Sarawak.",
                    job_salary_min = 4000.00M,
                    job_salary_max = 5500.00M,
                    Posted_Date = DateTime.Now.AddMonths(-2),
                    job_deadline = DateTime.Now.AddMonths(1).AddDays(10), // 60 days from posting
                    job_location = "Kuching, Sarawak (with field work)",
                    job_status = "Open",
                    job_benefit = "Field allowance, government pension scheme, paid study leave.",
                    job_requirement = "Bachelor's degree in Forestry or Environmental Science, knowledge of local ecosystems."
                },
                new Job
                {
                    job_id = 4,
                    job_company_id = 4,
                    job_type = JobType.Full_Time,
                    job_title = "Digital Marketing Specialist",
                    job_description = "Create and execute digital marketing campaigns to promote Sarawak's digital initiatives across various platforms and channels.",
                    Posted_Date = DateTime.Now.AddMonths(-4),
                    job_deadline = DateTime.Now.AddMonths(-4).AddDays(30), // 30 days from posting
                    job_responsible = "Develop and implement digital marketing strategies for Sarawak's digital initiatives.",
                    job_salary_min = 3500.00M,
                    job_salary_max = 5000.00M,
                    job_location = "Samarahan, Sarawak",
                    job_status = "closed",
                    job_benefit = "Performance bonuses, flexible working arrangements, training opportunities.",
                    job_requirement = "Bachelor's degree in Marketing or Communications, experience with digital marketing tools."
                },
                new Job
                {
                    job_id = 5,
                    job_company_id = 1,
                    job_type = JobType.Internship,
                    job_title = "Renewable Energy Analyst",
                    job_description = "Evaluate renewable energy projects, conduct feasibility studies, and provide recommendations for sustainable energy solutions.",
                    job_responsible = "Analyze renewable energy projects and prepare feasibility reports.",
                    Posted_Date = DateTime.Now.AddDays(-40),
                    job_deadline = DateTime.Now.AddDays(5), // 45 days from posting
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
                    job_type = JobType.Full_Time,
                    job_title = "Full Stack Developer",
                    job_description = "Develop and maintain web applications that support Sarawak's digital economy initiatives, from database design to user interface implementation.",
                    Posted_Date = DateTime.Now.AddDays(-34),
                    job_deadline = DateTime.Now.AddDays(11), // 45 days from posting
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
                    application_type = "full_time",
                    application_status = "pending",
                    application_submission_date = DateTime.Now.AddDays(-10)
                },
                new JobApplication
                {
                    application_id = 2,
                    application_job_id = 3,
                    application_type = "full_time",
                    application_status = "interview_scheduled",
                    application_submission_date = DateTime.Now.AddDays(-15)
                },
                new JobApplication
                {
                    application_id = 3,
                    application_job_id = 2,
                    application_type = "contract",
                    application_status = "pending",
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
                    notification_user_id = userId1,
                    notification_company_id = 1,
                    notification_text = "Your application for Senior Software Developer has been received. We will review it shortly.",
                    notification_timestamp = DateTime.Now.AddDays(-10),
                    notification_status = "unread"
                },
                new Notification
                {
                    notification_id = 2,
                    notification_user_id = userId2,
                    notification_company_id = 3,
                    notification_text = "You have been shortlisted for the Forest Conservation Officer position. Please prepare for an interview.",
                    notification_timestamp = DateTime.Now.AddDays(-8),
                    notification_status = "read"
                },
                new Notification
                {
                    notification_id = 3,
                    notification_user_id = userId3,
                    notification_company_id = 2,
                    notification_text = "Thank you for your application to Petronas Carigali. Your application is under review.",
                    notification_timestamp = DateTime.Now.AddDays(-5),
                    notification_status = "unread"
                }
           );

            // Seed CareerAnalysis (3)
            modelBuilder.Entity<CareerAnalysis>().HasData(
                new CareerAnalysis
                {
                    analysis_id = 1,
                    analysis_user_id = userId1,
                    analysis_ai_direction = "Based on your skills and experience, you have strong potential in software development. Consider specializing in energy sector applications or cloud technologies.",
                    analysis_ai_market_gap = "There is growing demand for developers with expertise in renewable energy systems in Sarawak. Consider upskilling in this area.",
                    analysis_ai_career_prospects = "High potential for career growth in Sarawak's emerging digital economy. Projected salary increase of 15-20% in the next 3 years."
                },
                new CareerAnalysis
                {
                    analysis_id = 2,
                    analysis_user_id = userId2,
                    analysis_ai_direction = "Your environmental science background positions you well for conservation roles. Consider gaining project management certification.",
                    analysis_ai_market_gap = "Sarawak has increasing needs for environmental impact assessment specialists for sustainable development projects.",
                    analysis_ai_career_prospects = "Strong demand for conservation experts in both government and private sectors in Sarawak over the next 5 years."
                },
                new CareerAnalysis
                {
                    analysis_id = 3,
                    analysis_user_id = userId3,
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