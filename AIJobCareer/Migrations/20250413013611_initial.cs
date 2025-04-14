using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AIJobCareer.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AREA",   
                columns: table => new
                {
                    area_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    area_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AREA", x => x.area_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SKILL",
                columns: table => new
                {
                    skill_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    skill_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    skill_level = table.Column<string>(type: "enum('beginner', 'intermediate', 'proficient', 'advanced', 'expert')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKILL", x => x.skill_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "COMPANY",
                columns: table => new
                {
                    company_id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_icon = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_intro = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_website = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_industry = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_area_id = table.Column<int>(type: "int", nullable: true),
                    company_founded = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY", x => x.company_id);
                    table.ForeignKey(
                        name: "FK_COMPANY_AREA_company_area_id",
                        column: x => x.company_area_id,
                        principalTable: "AREA",
                        principalColumn: "area_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JOB",
                columns: table => new
                {
                    job_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    job_company_id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_responsible = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_salary_min = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    job_salary_max = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    job_location = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_type = table.Column<string>(type: "enum('internship', 'freelance', 'full_time', 'part_time', 'contract')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_status = table.Column<string>(type: "enum('open', 'closed')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Posted_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    job_deadline = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    job_benefit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_requirement = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JOB", x => x.job_id);
                    table.ForeignKey(
                        name: "FK_JOB_COMPANY_job_company_id",
                        column: x => x.job_company_id,
                        principalTable: "COMPANY",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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
                    user_privacy_status = table.Column<string>(type: "enum('public', 'private')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_role = table.Column<string>(type: "enum('job_seeker', 'business')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_account_created_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_login_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    user_area_id = table.Column<int>(type: "int", nullable: true),
                    user_company_id = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_USERS_AREA_user_area_id",
                        column: x => x.user_area_id,
                        principalTable: "AREA",
                        principalColumn: "area_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USERS_COMPANY_user_company_id",
                        column: x => x.user_company_id,
                        principalTable: "COMPANY",
                        principalColumn: "company_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JOB_SKILL",
                columns: table => new
                {
                    JS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JS_JOB_ID = table.Column<int>(type: "int", nullable: false),
                    JS_SKILL_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JOB_SKILL", x => x.JS_ID);
                    table.ForeignKey(
                        name: "FK_JOB_SKILL_JOB_JS_JOB_ID",
                        column: x => x.JS_JOB_ID,
                        principalTable: "JOB",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JOB_SKILL_SKILL_JS_SKILL_ID",
                        column: x => x.JS_SKILL_ID,
                        principalTable: "SKILL",
                        principalColumn: "skill_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    education_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    degree_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    institution_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    start_year = table.Column<int>(type: "int", nullable: false),
                    end_year = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.education_id);
                    table.ForeignKey(
                        name: "FK_Education_USERS_user_id",
                        column: x => x.user_id,
                        principalTable: "USERS",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NOTIFICATION",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    notification_user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    notification_company_id = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    notification_text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    notification_timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    notification_status = table.Column<string>(type: "enum('read', 'unread')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICATION", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_NOTIFICATION_COMPANY_notification_company_id",
                        column: x => x.notification_company_id,
                        principalTable: "COMPANY",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NOTIFICATION_USERS_notification_user_id",
                        column: x => x.notification_user_id,
                        principalTable: "USERS",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    project_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    project_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    project_year = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    project_url = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.project_id);
                    table.ForeignKey(
                        name: "FK_Project_USERS_user_id",
                        column: x => x.user_id,
                        principalTable: "USERS",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Publication",
                columns: table => new
                {
                    publication_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    publication_title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    publisher = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    publication_year = table.Column<int>(type: "int", nullable: false),
                    publication_url = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publication", x => x.publication_id);
                    table.ForeignKey(
                        name: "FK_Publication_USERS_user_id",
                        column: x => x.user_id,
                        principalTable: "USERS",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Resume",
                columns: table => new
                {
                    resume_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    resume_user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    resume_url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resume_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_application_id = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resume", x => x.resume_id);
                    table.ForeignKey(
                        name: "FK_Resume_USERS_resume_user_id",
                        column: x => x.resume_user_id,
                        principalTable: "USERS",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USER_SKILL",
                columns: table => new
                {
                    US_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    US_USER_ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    US_SKILL_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_SKILL", x => x.US_ID);
                    table.ForeignKey(
                        name: "FK_USER_SKILL_SKILL_US_SKILL_ID",
                        column: x => x.US_SKILL_ID,
                        principalTable: "SKILL",
                        principalColumn: "skill_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_SKILL_USERS_US_USER_ID",
                        column: x => x.US_USER_ID,
                        principalTable: "USERS",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Work_Experience",
                columns: table => new
                {
                    experience_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    job_title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    location = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    start_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_current = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    experience_skill = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Work_Experience", x => x.experience_id);
                    table.ForeignKey(
                        name: "FK_Work_Experience_USERS_user_id",
                        column: x => x.user_id,
                        principalTable: "USERS",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JOB_APPLICATION",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LinkedIn = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Portfolio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Experience = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Education = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Skills = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Availability = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Relocate = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Salary = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CoverLetter = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resume_id = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JOB_APPLICATION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JOB_APPLICATION_JOB_JobId",
                        column: x => x.JobId,
                        principalTable: "JOB",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JOB_APPLICATION_Resume_resume_id",
                        column: x => x.resume_id,
                        principalTable: "Resume",
                        principalColumn: "resume_id");
                    table.ForeignKey(
                        name: "FK_JOB_APPLICATION_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AREA",
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
                table: "SKILL",
                columns: new[] { "skill_id", "skill_level", "skill_name" },
                values: new object[,]
                {
                    { 1, "Advanced", "C# Programming" },
                    { 2, "Intermediate", "Database Management" },
                    { 3, "Advanced", "Project Management" },
                    { 4, "Advanced", "Web Development" },
                    { 5, "Expert", "Petroleum Engineering" },
                    { 6, "Intermediate", "Digital Marketing" },
                    { 7, "Advanced", "Forestry Management" },
                    { 8, "Intermediate", "Tourism & Hospitality" },
                    { 9, "Expert", "Indigenous Culture Knowledge" },
                    { 10, "Advanced", "Agricultural Science" }
                });

            migrationBuilder.InsertData(
                table: "COMPANY",
                columns: new[] { "company_id", "company_area_id", "company_founded", "company_icon", "company_industry", "company_intro", "company_name", "company_website" },
                values: new object[,]
                {
                    { "petronas", 2, new DateTime(2017, 1, 13, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3441), "", "Oil & Gas", "Oil and gas exploration company operating in Sarawak's offshore regions.", "Petronas Carigali Sdn Bhd", "https://www.petronas.com" },
                    { "sarawakenergy", 1, new DateTime(2015, 4, 13, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3425), "", "Energy & Utilities", "Leading energy provider in Sarawak focusing on renewable energy sources.", "Sarawak Energy Berhad", "https://www.sarawakenergy.com" },
                    { "sarawakforestrycorporation", 1, new DateTime(2020, 1, 13, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3444), "", "Forestry & Environmental Services", "Responsible for sustainable management of Sarawak's forest resources.", "Sarawak Forestry Corporation", "https://www.sarawakforestry.com" },
                    { "sdec", 5, new DateTime(2018, 1, 13, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3446), "", "Technology & Digital Services", "Driving digital transformation and innovation across Sarawak.", "Sarawak Digital Economy Corporation", "https://www.sdec.com.my" }
                });

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "user_id", "last_login_at", "user_account_created_time", "user_age", "user_area_id", "user_company_id", "user_contact_number", "user_email", "user_first_name", "user_icon", "user_intro", "user_last_name", "user_password", "user_privacy_status", "user_role", "username" },
                values: new object[,]
                {
                    { new Guid("10daf21c-8cdf-4cba-8cf7-28859c4da0f7"), null, new DateTime(2024, 7, 13, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3483), 35, 2, null, "0167890123", "rajesh@example.com", "Rajesh", "rajesh_profile.jpg", "Petroleum engineer with 10 years experience in offshore drilling.", "Kumar", "hashed_password_3", "public", "job_seeker", "rajesh_kumar" },
                    { new Guid("77742591-68d6-40b3-81ab-1af77bce5c9d"), null, new DateTime(2025, 1, 13, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3478), 32, 3, null, "0123456789", "siti@example.com", "Siti", "siti_profile.jpg", "Environmental scientist with expertise in tropical forest conservation.", "Nur Aminah", "hashed_password_2", "private", "job_seeker", "siti_aminah" },
                    { new Guid("e284ddf2-049e-4321-95d5-891009a97a7d"), null, new DateTime(2024, 10, 13, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3471), 28, 1, null, "0198765432", "ahmad@example.com", "Ahmad", "ahmad_profile.jpg", "Software developer with 5 years of experience in web technologies.", "bin Ibrahim", "hashed_password_1", "public", "job_seeker", "ahmad_ibrahim" }
                });

            migrationBuilder.InsertData(
                table: "JOB",
                columns: new[] { "job_id", "Posted_Date", "job_benefit", "job_company_id", "job_deadline", "job_description", "job_location", "job_requirement", "job_responsible", "job_salary_max", "job_salary_min", "job_status", "job_title", "job_type" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 24, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3499), "Health insurance,\nPerformance bonus,\nProfessional development allowance.", "sarawakenergy", new DateTime(2025, 5, 13, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3501), "Develop and maintain enterprise software applications for energy management systems.", "Kuching, Sarawak", "Bachelor's degree in Computer Science,\n5+ years experience in software development.", "Lead software development projects,\nMentor junior developers,\nCollaborate with stakeholders.", 7500.00m, 5000.00m, "Open", "Senior Software Developer", "Full_Time" },
                    { 2, new DateTime(2025, 4, 3, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3531), "Housing allowance,\nTransportation,\nMedical coverage,\nAnnual bonus.", "petronas", new DateTime(2025, 5, 17, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3532), "Design and implement strategies for efficient oil and gas extraction. Collaborate with multidisciplinary teams to solve complex drilling challenges.", "Miri, Sarawak", "Bachelor's degree in Petroleum Engineering,\n5+ years field experience.", "Oversee drilling operations,\nOptimize oil extraction processes,\nCoordinate with field teams.", 9000.00m, 6500.00m, "Open", "Petroleum Engineer", "Contract" },
                    { 3, new DateTime(2025, 2, 13, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3535), "Field allowance,\nGovernment pension scheme,\nPaid study leave.", "sarawakforestrycorporation", new DateTime(2025, 5, 23, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3536), "Monitor forest health, implement conservation programs, and work with local communities to promote sustainable forest management practices.", "Kuching, Sarawak (with field work)", "Bachelor's degree in Forestry or Environmental Science,\nKnowledge of local ecosystems.", "Implement and monitor conservation programs,\nEngage with local communities,\nReport on ecosystem health.", 5500.00m, 4000.00m, "Open", "Forest Conservation Officer", "Part_Time" },
                    { 4, new DateTime(2024, 12, 13, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3539), "Performance bonuses,\nFlexible working arrangements,\nTraining opportunities.", "sdec", new DateTime(2025, 1, 12, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3539), "Create and execute digital marketing campaigns to promote Sarawak's digital initiatives across various platforms and channels.", "Samarahan, Sarawak", "Bachelor's degree in Marketing or Communications,\nExperience with digital marketing tools.", "Develop digital marketing strategies,\nManage social media and PPC campaigns,\nAnalyze performance metrics.", 5000.00m, 3500.00m, "Closed", "Digital Marketing Specialist", "Full_Time" },
                    { 5, new DateTime(2025, 3, 4, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3542), "Professional development fund,\nHealth insurance,\nPerformance bonus.", "sarawakenergy", new DateTime(2025, 4, 18, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3542), "Evaluate renewable energy projects, conduct feasibility studies, and provide recommendations for sustainable energy solutions.", "Kuching, Sarawak", "Bachelor's degree in Environmental Engineering or related field,\nKnowledge of renewable energy technologies.", "Analyze project feasibility,\nModel energy outputs,\nPrepare recommendation reports.", 6000.00m, 4500.00m, "Open", "Renewable Energy Analyst", "Internship" },
                    { 6, new DateTime(2025, 3, 10, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3544), "Remote work options,\nMedical coverage,\nProfessional development.", "sarawakforestrycorporation", new DateTime(2025, 4, 24, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3545), "Develop and maintain web applications that support Sarawak's digital economy initiatives, from database design to user interface implementation.", "Samarahan, Sarawak", "Bachelor's degree in Computer Science,\nProficiency in front‑end and back‑end technologies.", "Design and develop web applications,\nMaintain backend services,\nImplement user-friendly interfaces.", 7000.00m, 4800.00m, "Open", "Full Stack Developer", "Full_Time" },
                    { 7, new DateTime(2025, 3, 29, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3547), "Health insurance,\nStock options,\nTraining budget.", "sarawakenergy", new DateTime(2025, 5, 13, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3548), "Analyze large datasets to extract insights and build predictive models for energy demand forecasting.", "Kuching, Sarawak", "Bachelor's/Master's in Data Science or related,\n3+ years experience in data analytics,\nProficiency in Python and ML frameworks.", "Develop data pipelines,\nBuild and validate predictive models,\nPresent findings to stakeholders.", 8500.00m, 6000.00m, "Open", "Data Scientist", "Full_Time" },
                    { 8, new DateTime(2025, 3, 19, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3551), "Housing allowance,\nMedical coverage,\nAnnual bonus.", "petronas", new DateTime(2025, 4, 28, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3551), "Plan and supervise infrastructure projects including roads, bridges, and public facilities.", "Miri, Sarawak", "Bachelor's degree in Civil Engineering,\n4+ years site experience,\nKnowledge of local building codes.", "Design structural plans,\nOversee construction sites,\nEnsure compliance with safety standards.", 8000.00m, 5500.00m, "Open", "Civil Engineer", "Full_Time" },
                    { 9, new DateTime(2025, 4, 8, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3554), "Field allowance,\nFlexible schedule,\nEquipment provision.", "sarawakforestrycorporation", new DateTime(2025, 5, 18, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3554), "Manage GIS databases to support forestry conservation and land management.", "Samarahan, Sarawak", "Bachelor's in Geography/GIS,\nProficiency in ArcGIS/QGIS,\n2+ years GIS experience.", "Develop and maintain GIS maps,\nConduct spatial analysis,\nTrain staff on GIS tools.", 6500.00m, 4500.00m, "Open", "GIS Specialist", "Contract" },
                    { 10, new DateTime(2025, 4, 1, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3556), "Flexible working hours,\nHealth coverage,\nProfessional development.", "sdec", new DateTime(2025, 5, 16, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3557), "Design intuitive user interfaces for web and mobile applications to enhance user experience.", "Kuching, Sarawak", "Bachelor's in Design or HCI,\n3+ years UX/UI experience,\nProficiency in Figma and Sketch.", "Create wireframes and prototypes,\nConduct user research,\nCollaborate with developers.", 7000.00m, 5000.00m, "Open", "UX Designer", "Full_Time" }
                });

            migrationBuilder.InsertData(
                table: "NOTIFICATION",
                columns: new[] { "notification_id", "notification_company_id", "notification_status", "notification_text", "notification_timestamp", "notification_user_id" },
                values: new object[,]
                {
                    { 1, "sarawakenergy", "unread", "Your application for Senior Software Developer has been received. We will review it shortly.", new DateTime(2025, 4, 3, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3606), new Guid("e284ddf2-049e-4321-95d5-891009a97a7d") },
                    { 2, "sarawakforestrycorporation", "read", "You have been shortlisted for the Forest Conservation Officer position. Please prepare for an interview.", new DateTime(2025, 4, 5, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3608), new Guid("77742591-68d6-40b3-81ab-1af77bce5c9d") },
                    { 3, "petronas", "unread", "Thank you for your application to Petronas Carigali. Your application is under review.", new DateTime(2025, 4, 8, 9, 36, 11, 442, DateTimeKind.Local).AddTicks(3610), new Guid("10daf21c-8cdf-4cba-8cf7-28859c4da0f7") }
                });

            migrationBuilder.InsertData(
                table: "USER_SKILL",
                columns: new[] { "US_ID", "US_SKILL_ID", "US_USER_ID" },
                values: new object[,]
                {
                    { 1, 1, new Guid("e284ddf2-049e-4321-95d5-891009a97a7d") },
                    { 2, 7, new Guid("77742591-68d6-40b3-81ab-1af77bce5c9d") },
                    { 3, 5, new Guid("10daf21c-8cdf-4cba-8cf7-28859c4da0f7") }
                });

            migrationBuilder.InsertData(
                table: "JOB_SKILL",
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

            migrationBuilder.CreateIndex(
                name: "IX_COMPANY_company_area_id",
                table: "COMPANY",
                column: "company_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_Education_user_id",
                table: "Education",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_job_company_id",
                table: "JOB",
                column: "job_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_APPLICATION_JobId",
                table: "JOB_APPLICATION",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_APPLICATION_resume_id",
                table: "JOB_APPLICATION",
                column: "resume_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JOB_APPLICATION_UserId",
                table: "JOB_APPLICATION",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_SKILL_JS_JOB_ID",
                table: "JOB_SKILL",
                column: "JS_JOB_ID");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_SKILL_JS_SKILL_ID",
                table: "JOB_SKILL",
                column: "JS_SKILL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_notification_company_id",
                table: "NOTIFICATION",
                column: "notification_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATION_notification_user_id",
                table: "NOTIFICATION",
                column: "notification_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_user_id",
                table: "Project",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_user_id",
                table: "Publication",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_resume_user_id",
                table: "Resume",
                column: "resume_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SKILL_US_SKILL_ID",
                table: "USER_SKILL",
                column: "US_SKILL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SKILL_US_USER_ID",
                table: "USER_SKILL",
                column: "US_USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_user_area_id",
                table: "USERS",
                column: "user_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_user_company_id",
                table: "USERS",
                column: "user_company_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_username",
                table: "USERS",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Work_Experience_user_id",
                table: "Work_Experience",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "JOB_APPLICATION");

            migrationBuilder.DropTable(
                name: "JOB_SKILL");

            migrationBuilder.DropTable(
                name: "NOTIFICATION");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DropTable(
                name: "USER_SKILL");

            migrationBuilder.DropTable(
                name: "Work_Experience");

            migrationBuilder.DropTable(
                name: "Resume");

            migrationBuilder.DropTable(
                name: "JOB");

            migrationBuilder.DropTable(
                name: "SKILL");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "COMPANY");

            migrationBuilder.DropTable(
                name: "AREA");
        }
    }
}
