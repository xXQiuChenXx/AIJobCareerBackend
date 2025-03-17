using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AIJobCareer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    area_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    area_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.area_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    skill_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    skill_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    skill_info = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    skill_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    skill_level = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.skill_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    company_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_icon = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_intro = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_website = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_area_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.company_id);
                    table.ForeignKey(
                        name: "FK_Company_Area_company_area_id",
                        column: x => x.company_area_id,
                        principalTable: "Area",
                        principalColumn: "area_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_first_name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_last_name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_age = table.Column<int>(type: "int", nullable: true),
                    user_intro = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_contact_number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_icon = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_privacy_status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_account_created_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_login_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    user_area_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_User_Area_user_area_id",
                        column: x => x.user_area_id,
                        principalTable: "Area",
                        principalColumn: "area_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    job_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    job_company_id = table.Column<int>(type: "int", nullable: false),
                    job_title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_responsible = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_salary_min = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    job_salary_max = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    job_location = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_benefit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_requirement = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.job_id);
                    table.ForeignKey(
                        name: "FK_Job_Company_job_company_id",
                        column: x => x.job_company_id,
                        principalTable: "Company",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Career_Analysis",
                columns: table => new
                {
                    analysis_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    analysis_user_id = table.Column<int>(type: "int", nullable: false),
                    analysis_ai_direction = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    analysis_ai_market_gap = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    analysis_ai_career_prospects = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Career_Analysis", x => x.analysis_id);
                    table.ForeignKey(
                        name: "FK_Career_Analysis_User_analysis_user_id",
                        column: x => x.analysis_user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    notification_user_id = table.Column<int>(type: "int", nullable: true),
                    notification_company_id = table.Column<int>(type: "int", nullable: true),
                    notification_text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    notification_timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    notification_status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_Notification_Company_notification_company_id",
                        column: x => x.notification_company_id,
                        principalTable: "Company",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "FK_Notification_User_notification_user_id",
                        column: x => x.notification_user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Resume",
                columns: table => new
                {
                    resume_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    resume_user_id = table.Column<int>(type: "int", nullable: false),
                    resume_text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resume_file = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resume_last_modify_time = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resume", x => x.resume_id);
                    table.ForeignKey(
                        name: "FK_Resume_User_resume_user_id",
                        column: x => x.resume_user_id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USer_Skill",
                columns: table => new
                {
                    US_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    US_USER_ID = table.Column<int>(type: "int", nullable: false),
                    US_SKILL_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USer_Skill", x => x.US_ID);
                    table.ForeignKey(
                        name: "FK_USer_Skill_Skill_US_SKILL_ID",
                        column: x => x.US_SKILL_ID,
                        principalTable: "Skill",
                        principalColumn: "skill_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USer_Skill_User_US_USER_ID",
                        column: x => x.US_USER_ID,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Job_Application",
                columns: table => new
                {
                    application_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    application_job_id = table.Column<int>(type: "int", nullable: false),
                    application_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    application_status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    application_submission_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Application", x => x.application_id);
                    table.ForeignKey(
                        name: "FK_Job_Application_Job_application_job_id",
                        column: x => x.application_job_id,
                        principalTable: "Job",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Job_Skill",
                columns: table => new
                {
                    JS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JS_JOB_ID = table.Column<int>(type: "int", nullable: false),
                    JS_SKILL_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Skill", x => x.JS_ID);
                    table.ForeignKey(
                        name: "FK_Job_Skill_Job_JS_JOB_ID",
                        column: x => x.JS_JOB_ID,
                        principalTable: "Job",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Skill_Skill_JS_SKILL_ID",
                        column: x => x.JS_SKILL_ID,
                        principalTable: "Skill",
                        principalColumn: "skill_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Job_Application_Review",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    review_application_id = table.Column<int>(type: "int", nullable: false),
                    review_company_id = table.Column<int>(type: "int", nullable: false),
                    review_status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    review_context = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    review_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Application_Review", x => x.review_id);
                    table.ForeignKey(
                        name: "FK_Job_Application_Review_Company_review_company_id",
                        column: x => x.review_company_id,
                        principalTable: "Company",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Application_Review_Job_Application_review_application_id",
                        column: x => x.review_application_id,
                        principalTable: "Job_Application",
                        principalColumn: "application_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Job_Application_Table",
                columns: table => new
                {
                    TABLE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TABLE_APPLICATION_ID = table.Column<int>(type: "int", nullable: false),
                    TABLE_RESUME_ID = table.Column<int>(type: "int", nullable: false),
                    TABLE_COVER_LETTER = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Application_Table", x => x.TABLE_ID);
                    table.ForeignKey(
                        name: "FK_Job_Application_Table_Job_Application_TABLE_APPLICATION_ID",
                        column: x => x.TABLE_APPLICATION_ID,
                        principalTable: "Job_Application",
                        principalColumn: "application_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Application_Table_Resume_TABLE_RESUME_ID",
                        column: x => x.TABLE_RESUME_ID,
                        principalTable: "Resume",
                        principalColumn: "resume_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User_Application",
                columns: table => new
                {
                    UA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UA_USER_ID = table.Column<int>(type: "int", nullable: false),
                    UA_APPLICATION_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Application", x => x.UA_ID);
                    table.ForeignKey(
                        name: "FK_User_Application_Job_Application_UA_APPLICATION_ID",
                        column: x => x.UA_APPLICATION_ID,
                        principalTable: "Job_Application",
                        principalColumn: "application_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Application_User_UA_USER_ID",
                        column: x => x.UA_USER_ID,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "area_id", "area_name" },
                values: new object[,]
                {
                    { 1, "Kuching" },
                    { 2, "Miri" },
                    { 3, "Sibu" },
                    { 4, "Bintulu" },
                    { 5, "Samarahan" },
                    { 6, "Sri Aman" },
                    { 7, "Kapit" },
                    { 8, "Limbang" },
                    { 9, "Sarikei" },
                    { 10, "Betong" }
                });

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "skill_id", "skill_info", "skill_level", "skill_name", "skill_type" },
                values: new object[,]
                {
                    { 1, "Microsoft .NET framework development", "Advanced", "C# Programming", "Technical" },
                    { 2, "SQL Server, MySQL, PostgreSQL", "Intermediate", "Database Management", "Technical" },
                    { 3, "Agile, Scrum, Kanban methodologies", "Advanced", "Project Management", "Management" },
                    { 4, "HTML, CSS, JavaScript", "Advanced", "Web Development", "Technical" },
                    { 5, "Oil and gas extraction techniques", "Expert", "Petroleum Engineering", "Technical" },
                    { 6, "SEO, SEM, Social Media Marketing", "Intermediate", "Digital Marketing", "Marketing" },
                    { 7, "Sustainable forest management practices", "Advanced", "Forestry Management", "Environmental" },
                    { 8, "Customer service and hospitality management", "Intermediate", "Tourism & Hospitality", "Service" },
                    { 9, "Understanding of Sarawak's indigenous cultures", "Expert", "Indigenous Culture Knowledge", "Cultural" },
                    { 10, "Tropical agriculture techniques", "Advanced", "Agricultural Science", "Agricultural" }
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "company_id", "company_area_id", "company_icon", "company_intro", "company_name", "company_website" },
                values: new object[,]
                {
                    { 1, 1, "sarawak_energy_icon.png", "Leading energy provider in Sarawak focusing on renewable energy sources.", "Sarawak Energy Berhad", "https://www.sarawakenergy.com" },
                    { 2, 2, "petronas_icon.png", "Oil and gas exploration company operating in Sarawak's offshore regions.", "Petronas Carigali Sdn Bhd", "https://www.petronas.com" },
                    { 3, 1, "sfc_icon.png", "Responsible for sustainable management of Sarawak's forest resources.", "Sarawak Forestry Corporation", "https://www.sarawakforestry.com" },
                    { 4, 5, "sdec_icon.png", "Driving digital transformation and innovation across Sarawak.", "Sarawak Digital Economy Corporation", "https://www.sdec.com.my" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "user_id", "last_login_at", "user_account_created_time", "user_age", "user_area_id", "user_contact_number", "user_email", "user_first_name", "user_icon", "user_intro", "user_last_name", "user_password", "user_privacy_status", "user_role", "username" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 9, 18, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8244), 28, 1, "0198765432", "ahmad@example.com", "Ahmad", "ahmad_profile.jpg", "Software developer with 5 years of experience in web technologies.", "bin Ibrahim", "hashed_password_1", "Public", "JobSeeker", "ahmad_ibrahim" },
                    { 2, null, new DateTime(2024, 12, 18, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8262), 32, 3, "0123456789", "siti@example.com", "Siti", "siti_profile.jpg", "Environmental scientist with expertise in tropical forest conservation.", "Nur Aminah", "hashed_password_2", "Limited", "JobSeeker", "siti_aminah" },
                    { 3, null, new DateTime(2024, 6, 18, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8265), 35, 2, "0167890123", "rajesh@example.com", "Rajesh", "rajesh_profile.jpg", "Petroleum engineer with 10 years experience in offshore drilling.", "Kumar", "hashed_password_3", "Public", "JobSeeker", "rajesh_kumar" }
                });

            migrationBuilder.InsertData(
                table: "Career_Analysis",
                columns: new[] { "analysis_id", "analysis_ai_career_prospects", "analysis_ai_direction", "analysis_ai_market_gap", "analysis_user_id" },
                values: new object[,]
                {
                    { 1, "High potential for career growth in Sarawak's emerging digital economy. Projected salary increase of 15-20% in the next 3 years.", "Based on your skills and experience, you have strong potential in software development. Consider specializing in energy sector applications or cloud technologies.", "There is growing demand for developers with expertise in renewable energy systems in Sarawak. Consider upskilling in this area.", 1 },
                    { 2, "Strong demand for conservation experts in both government and private sectors in Sarawak over the next 5 years.", "Your environmental science background positions you well for conservation roles. Consider gaining project management certification.", "Sarawak has increasing needs for environmental impact assessment specialists for sustainable development projects.", 2 },
                    { 3, "Stable career prospects in Miri, with opportunities to transition to leadership roles in the next 2-3 years.", "Your petroleum engineering experience is valuable. Consider expanding into renewable energy transition projects.", "There is growing need for engineers who can bridge traditional oil & gas with renewable energy projects in Sarawak.", 3 }
                });

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "job_id", "job_benefit", "job_company_id", "job_location", "job_requirement", "job_responsible", "job_salary_max", "job_salary_min", "job_status", "job_title" },
                values: new object[,]
                {
                    { 1, "Health insurance, performance bonus, professional development allowance.", 1, "Kuching, Sarawak", "Bachelor's degree in Computer Science, 5+ years experience in software development.", "Develop and maintain enterprise software applications for energy management systems.", 7500.00m, 5000.00m, "Open", "Senior Software Developer" },
                    { 2, "Housing allowance, transportation, medical coverage, annual bonus.", 2, "Miri, Sarawak", "Bachelor's degree in Petroleum Engineering, 5+ years field experience.", "Oversee drilling operations and optimize oil extraction processes.", 9000.00m, 6500.00m, "Open", "Petroleum Engineer" },
                    { 3, "Field allowance, government pension scheme, paid study leave.", 3, "Kuching, Sarawak (with field work)", "Bachelor's degree in Forestry or Environmental Science, knowledge of local ecosystems.", "Implement and monitor forest conservation programs across Sarawak.", 5500.00m, 4000.00m, "Open", "Forest Conservation Officer" },
                    { 4, "Performance bonuses, flexible working arrangements, training opportunities.", 4, "Samarahan, Sarawak", "Bachelor's degree in Marketing or Communications, experience with digital marketing tools.", "Develop and implement digital marketing strategies for Sarawak's digital initiatives.", 5000.00m, 3500.00m, "Open", "Digital Marketing Specialist" },
                    { 5, "Professional development fund, health insurance, performance bonus.", 1, "Kuching, Sarawak", "Bachelor's degree in Environmental Engineering or related field, knowledge of renewable energy technologies.", "Analyze renewable energy projects and prepare feasibility reports.", 6000.00m, 4500.00m, "Open", "Renewable Energy Analyst" },
                    { 6, "Remote work options, medical coverage, professional development.", 4, "Samarahan, Sarawak", "Bachelor's degree in Computer Science, proficiency in front-end and back-end technologies.", "Design and develop web applications for Sarawak's digital economy initiatives.", 7000.00m, 4800.00m, "Open", "Full Stack Developer" }
                });

            migrationBuilder.InsertData(
                table: "Notification",
                columns: new[] { "notification_id", "notification_company_id", "notification_status", "notification_text", "notification_timestamp", "notification_user_id" },
                values: new object[,]
                {
                    { 1, 1, "Unread", "Your application for Senior Software Developer has been received. We will review it shortly.", new DateTime(2025, 3, 8, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8392), 1 },
                    { 2, 3, "Read", "You have been shortlisted for the Forest Conservation Officer position. Please prepare for an interview.", new DateTime(2025, 3, 10, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8394), 2 },
                    { 3, 2, "Unread", "Thank you for your application to Petronas Carigali. Your application is under review.", new DateTime(2025, 3, 13, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8395), 3 }
                });

            migrationBuilder.InsertData(
                table: "Resume",
                columns: new[] { "resume_id", "resume_file", "resume_last_modify_time", "resume_text", "resume_user_id" },
                values: new object[,]
                {
                    { 1, "ahmad_resume.pdf", new DateTime(2025, 3, 3, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8309), "Experienced software developer with expertise in .NET Core, React, and cloud technologies. Worked on enterprise applications for energy sector.", 1 },
                    { 2, "siti_resume.pdf", new DateTime(2025, 3, 11, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8312), "Environmental scientist focused on forest conservation. Experience in GIS mapping, biodiversity assessment, and sustainable forest management practices.", 2 },
                    { 3, "rajesh_resume.pdf", new DateTime(2025, 2, 25, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8313), "Petroleum engineer with extensive experience in offshore drilling. Skills include reservoir analysis, production optimization, and HSE compliance.", 3 }
                });

            migrationBuilder.InsertData(
                table: "USer_Skill",
                columns: new[] { "US_ID", "US_SKILL_ID", "US_USER_ID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 7, 2 },
                    { 3, 5, 3 }
                });

            migrationBuilder.InsertData(
                table: "Job_Application",
                columns: new[] { "application_id", "application_job_id", "application_status", "application_submission_date", "application_type" },
                values: new object[,]
                {
                    { 1, 1, "Under Review", new DateTime(2025, 3, 8, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8358), "Full Time" },
                    { 2, 3, "Interview Scheduled", new DateTime(2025, 3, 3, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8360), "Full Time" },
                    { 3, 2, "Under Review", new DateTime(2025, 3, 13, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8361), "Contract" }
                });

            migrationBuilder.InsertData(
                table: "Job_Skill",
                columns: new[] { "JS_ID", "JS_JOB_ID", "JS_SKILL_ID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 4 },
                    { 3, 2, 5 },
                    { 4, 3, 7 },
                    { 5, 4, 6 },
                    { 6, 6, 4 }
                });

            migrationBuilder.InsertData(
                table: "Job_Application_Review",
                columns: new[] { "review_id", "review_application_id", "review_company_id", "review_context", "review_date", "review_status" },
                values: new object[,]
                {
                    { 1, 1, 1, "Strong technical background and relevant experience. Recommended for interview.", new DateTime(2025, 3, 13, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8423), "Positive" },
                    { 2, 2, 3, "Excellent match for the position. Scientific background and conservation experience are ideal.", new DateTime(2025, 3, 8, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8424), "Very Positive" },
                    { 3, 3, 2, "Good experience but may need additional training in offshore safety protocols.", new DateTime(2025, 3, 15, 4, 26, 54, 306, DateTimeKind.Local).AddTicks(8426), "Neutral" }
                });

            migrationBuilder.InsertData(
                table: "Job_Application_Table",
                columns: new[] { "TABLE_ID", "TABLE_APPLICATION_ID", "TABLE_COVER_LETTER", "TABLE_RESUME_ID" },
                values: new object[,]
                {
                    { 1, 1, "I am excited to apply for the Senior Software Developer position at Sarawak Energy. My experience developing enterprise applications aligns perfectly with your requirements.", 1 },
                    { 2, 2, "As an environmental scientist passionate about forest conservation, I am eager to contribute to Sarawak Forestry Corporation's mission of sustainable forest management.", 2 },
                    { 3, 3, "With my decade of experience in petroleum engineering, I am confident in my ability to contribute to Petronas Carigali's operations in Sarawak's offshore fields.", 3 }
                });

            migrationBuilder.InsertData(
                table: "User_Application",
                columns: new[] { "UA_ID", "UA_APPLICATION_ID", "UA_USER_ID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Career_Analysis_analysis_user_id",
                table: "Career_Analysis",
                column: "analysis_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_company_area_id",
                table: "Company",
                column: "company_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_job_company_id",
                table: "Job",
                column: "job_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Application_application_job_id",
                table: "Job_Application",
                column: "application_job_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Application_Review_review_application_id",
                table: "Job_Application_Review",
                column: "review_application_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Application_Review_review_company_id",
                table: "Job_Application_Review",
                column: "review_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Application_Table_TABLE_APPLICATION_ID",
                table: "Job_Application_Table",
                column: "TABLE_APPLICATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Application_Table_TABLE_RESUME_ID",
                table: "Job_Application_Table",
                column: "TABLE_RESUME_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Skill_JS_JOB_ID",
                table: "Job_Skill",
                column: "JS_JOB_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Skill_JS_SKILL_ID",
                table: "Job_Skill",
                column: "JS_SKILL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_notification_company_id",
                table: "Notification",
                column: "notification_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_notification_user_id",
                table: "Notification",
                column: "notification_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_resume_user_id",
                table: "Resume",
                column: "resume_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_user_area_id",
                table: "User",
                column: "user_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Application_UA_APPLICATION_ID",
                table: "User_Application",
                column: "UA_APPLICATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Application_UA_USER_ID",
                table: "User_Application",
                column: "UA_USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USer_Skill_US_SKILL_ID",
                table: "USer_Skill",
                column: "US_SKILL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USer_Skill_US_USER_ID",
                table: "USer_Skill",
                column: "US_USER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Career_Analysis");

            migrationBuilder.DropTable(
                name: "Job_Application_Review");

            migrationBuilder.DropTable(
                name: "Job_Application_Table");

            migrationBuilder.DropTable(
                name: "Job_Skill");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "User_Application");

            migrationBuilder.DropTable(
                name: "USer_Skill");

            migrationBuilder.DropTable(
                name: "Resume");

            migrationBuilder.DropTable(
                name: "Job_Application");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Area");
        }
    }
}
