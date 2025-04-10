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
                name: "CAREER_ANALYSIS",
                columns: table => new
                {
                    analysis_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    analysis_user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    analysis_ai_direction = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    analysis_ai_market_gap = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    analysis_ai_career_prospects = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAREER_ANALYSIS", x => x.analysis_id);
                    table.ForeignKey(
                        name: "FK_CAREER_ANALYSIS_USERS_analysis_user_id",
                        column: x => x.analysis_user_id,
                        principalTable: "USERS",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Certification",
                columns: table => new
                {
                    certification_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    certification_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    issuing_organization = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    issue_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    expiry_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    credential_id = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    credential_url = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certification", x => x.certification_id);
                    table.ForeignKey(
                        name: "FK_Certification_USERS_user_id",
                        column: x => x.user_id,
                        principalTable: "USERS",
                        principalColumn: "user_id",
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
                    ResumeUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                        name: "FK_JOB_APPLICATION_USERS_UserId",
                        column: x => x.UserId,
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
                name: "RESUME",
                columns: table => new
                {
                    resume_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    resume_user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    resume_text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resume_file = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    resume_last_modify_time = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESUME", x => x.resume_id);
                    table.ForeignKey(
                        name: "FK_RESUME_USERS_resume_user_id",
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
                name: "JOB_APPLICATION_REVIEW",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    review_application_id = table.Column<int>(type: "int", nullable: false),
                    review_company_id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    review_status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    review_context = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    review_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JOB_APPLICATION_REVIEW", x => x.review_id);
                    table.ForeignKey(
                        name: "FK_JOB_APPLICATION_REVIEW_COMPANY_review_company_id",
                        column: x => x.review_company_id,
                        principalTable: "COMPANY",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JOB_APPLICATION_REVIEW_JOB_APPLICATION_review_application_id",
                        column: x => x.review_application_id,
                        principalTable: "JOB_APPLICATION",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USER_APPLICATION",
                columns: table => new
                {
                    UA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UA_USER_ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UA_APPLICATION_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_APPLICATION", x => x.UA_ID);
                    table.ForeignKey(
                        name: "FK_USER_APPLICATION_JOB_APPLICATION_UA_APPLICATION_ID",
                        column: x => x.UA_APPLICATION_ID,
                        principalTable: "JOB_APPLICATION",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_APPLICATION_USERS_UA_USER_ID",
                        column: x => x.UA_USER_ID,
                        principalTable: "USERS",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JOB_APPLICATION_TABLE",
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
                    table.PrimaryKey("PK_JOB_APPLICATION_TABLE", x => x.TABLE_ID);
                    table.ForeignKey(
                        name: "FK_JOB_APPLICATION_TABLE_JOB_APPLICATION_TABLE_APPLICATION_ID",
                        column: x => x.TABLE_APPLICATION_ID,
                        principalTable: "JOB_APPLICATION",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JOB_APPLICATION_TABLE_RESUME_TABLE_RESUME_ID",
                        column: x => x.TABLE_RESUME_ID,
                        principalTable: "RESUME",
                        principalColumn: "resume_id",
                        onDelete: ReferentialAction.Restrict);
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
                    { "petronas", 2, new DateTime(2017, 1, 9, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6108), "petronas_icon.png", "Oil & Gas", "Oil and gas exploration company operating in Sarawak's offshore regions.", "Petronas Carigali Sdn Bhd", "https://www.petronas.com" },
                    { "sarawakenergy", 1, new DateTime(2015, 4, 9, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6094), "sarawak_energy_icon.png", "Energy & Utilities", "Leading energy provider in Sarawak focusing on renewable energy sources.", "Sarawak Energy Berhad", "https://www.sarawakenergy.com" },
                    { "sarawakforestrycorporation", 1, new DateTime(2020, 1, 9, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6112), "sfc_icon.png", "Forestry & Environmental Services", "Responsible for sustainable management of Sarawak's forest resources.", "Sarawak Forestry Corporation", "https://www.sarawakforestry.com" },
                    { "sdec", 5, new DateTime(2018, 1, 9, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6114), "sdec_icon.png", "Technology & Digital Services", "Driving digital transformation and innovation across Sarawak.", "Sarawak Digital Economy Corporation", "https://www.sdec.com.my" }
                });

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "user_id", "last_login_at", "user_account_created_time", "user_age", "user_area_id", "user_company_id", "user_contact_number", "user_email", "user_first_name", "user_icon", "user_intro", "user_last_name", "user_password", "user_privacy_status", "user_role", "username" },
                values: new object[,]
                {
                    { new Guid("61cb59c6-7a41-414d-9138-ea78ff4299b7"), null, new DateTime(2024, 10, 9, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6139), 28, 1, null, "0198765432", "ahmad@example.com", "Ahmad", "ahmad_profile.jpg", "Software developer with 5 years of experience in web technologies.", "bin Ibrahim", "hashed_password_1", "public", "job_seeker", "ahmad_ibrahim" },
                    { new Guid("640c6a74-3b8c-4fa0-840c-50f782b20367"), null, new DateTime(2024, 7, 9, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6177), 35, 2, null, "0167890123", "rajesh@example.com", "Rajesh", "rajesh_profile.jpg", "Petroleum engineer with 10 years experience in offshore drilling.", "Kumar", "hashed_password_3", "public", "job_seeker", "rajesh_kumar" },
                    { new Guid("ede7a34c-8871-424a-8d4d-91d38b8b95c6"), null, new DateTime(2025, 1, 9, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6144), 32, 3, null, "0123456789", "siti@example.com", "Siti", "siti_profile.jpg", "Environmental scientist with expertise in tropical forest conservation.", "Nur Aminah", "hashed_password_2", "private", "job_seeker", "siti_aminah" }
                });

            migrationBuilder.InsertData(
                table: "CAREER_ANALYSIS",
                columns: new[] { "analysis_id", "analysis_ai_career_prospects", "analysis_ai_direction", "analysis_ai_market_gap", "analysis_user_id" },
                values: new object[,]
                {
                    { 1, "High potential for career growth in Sarawak's emerging digital economy. Projected salary increase of 15-20% in the next 3 years.", "Based on your skills and experience, you have strong potential in software development. Consider specializing in energy sector applications or cloud technologies.", "There is growing demand for developers with expertise in renewable energy systems in Sarawak. Consider upskilling in this area.", new Guid("61cb59c6-7a41-414d-9138-ea78ff4299b7") },
                    { 2, "Strong demand for conservation experts in both government and private sectors in Sarawak over the next 5 years.", "Your environmental science background positions you well for conservation roles. Consider gaining project management certification.", "Sarawak has increasing needs for environmental impact assessment specialists for sustainable development projects.", new Guid("ede7a34c-8871-424a-8d4d-91d38b8b95c6") },
                    { 3, "Stable career prospects in Miri, with opportunities to transition to leadership roles in the next 2-3 years.", "Your petroleum engineering experience is valuable. Consider expanding into renewable energy transition projects.", "There is growing need for engineers who can bridge traditional oil & gas with renewable energy projects in Sarawak.", new Guid("640c6a74-3b8c-4fa0-840c-50f782b20367") }
                });

            migrationBuilder.InsertData(
                table: "JOB",
                columns: new[] { "job_id", "Posted_Date", "job_benefit", "job_company_id", "job_deadline", "job_description", "job_location", "job_requirement", "job_responsible", "job_salary_max", "job_salary_min", "job_status", "job_title", "job_type" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 20, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6195), "Health insurance, performance bonus, professional development allowance.", "sarawakenergy", new DateTime(2025, 5, 9, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6198), "Develop and maintain enterprise software applications for energy management systems.", "Kuching, Sarawak", "Bachelor's degree in Computer Science, 5+ years experience in software development.", "Lead software development projects, mentor junior developers, and collaborate with stakeholders.", 7500.00m, 5000.00m, "Open", "Senior Software Developer", "Full_Time" },
                    { 2, new DateTime(2025, 3, 30, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6201), "Housing allowance, transportation, medical coverage, annual bonus.", "petronas", new DateTime(2025, 5, 13, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6201), "Design and implement strategies for efficient oil and gas extraction. Collaborate with multidisciplinary teams to solve complex drilling challenges.", "Miri, Sarawak", "Bachelor's degree in Petroleum Engineering, 5+ years field experience.", "Oversee drilling operations and optimize oil extraction processes.", 9000.00m, 6500.00m, "Open", "Petroleum Engineer", "Contract" },
                    { 3, new DateTime(2025, 2, 9, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6204), "Field allowance, government pension scheme, paid study leave.", "sarawakforestrycorporation", new DateTime(2025, 5, 19, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6204), "Monitor forest health, implement conservation programs, and work with local communities to promote sustainable forest management practices.", "Kuching, Sarawak (with field work)", "Bachelor's degree in Forestry or Environmental Science, knowledge of local ecosystems.", "Implement and monitor forest conservation programs across Sarawak.", 5500.00m, 4000.00m, "Open", "Forest Conservation Officer", "Part_Time" },
                    { 4, new DateTime(2024, 12, 9, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6206), "Performance bonuses, flexible working arrangements, training opportunities.", "sdec", new DateTime(2025, 1, 8, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6207), "Create and execute digital marketing campaigns to promote Sarawak's digital initiatives across various platforms and channels.", "Samarahan, Sarawak", "Bachelor's degree in Marketing or Communications, experience with digital marketing tools.", "Develop and implement digital marketing strategies for Sarawak's digital initiatives.", 5000.00m, 3500.00m, "closed", "Digital Marketing Specialist", "Full_Time" },
                    { 5, new DateTime(2025, 2, 28, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6209), "Professional development fund, health insurance, performance bonus.", "sarawakenergy", new DateTime(2025, 4, 14, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6210), "Evaluate renewable energy projects, conduct feasibility studies, and provide recommendations for sustainable energy solutions.", "Kuching, Sarawak", "Bachelor's degree in Environmental Engineering or related field, knowledge of renewable energy technologies.", "Analyze renewable energy projects and prepare feasibility reports.", 6000.00m, 4500.00m, "Open", "Renewable Energy Analyst", "Internship" },
                    { 6, new DateTime(2025, 3, 6, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6212), "Remote work options, medical coverage, professional development.", "sarawakforestrycorporation", new DateTime(2025, 4, 20, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6213), "Develop and maintain web applications that support Sarawak's digital economy initiatives, from database design to user interface implementation.", "Samarahan, Sarawak", "Bachelor's degree in Computer Science, proficiency in front-end and back-end technologies.", "Design and develop web applications for Sarawak's digital economy initiatives.", 7000.00m, 4800.00m, "Open", "Full Stack Developer", "Full_Time" }
                });

            migrationBuilder.InsertData(
                table: "JOB_APPLICATION_REVIEW",
                columns: new[] { "review_id", "review_application_id", "review_company_id", "review_context", "review_date", "review_status" },
                values: new object[,]
                {
                    { 1, 1, "sarawakenergy", "Strong technical background and relevant experience. Recommended for interview.", new DateTime(2025, 4, 4, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6325), "Positive" },
                    { 2, 2, "petronas", "Excellent match for the position. Scientific background and conservation experience are ideal.", new DateTime(2025, 3, 30, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6327), "Very Positive" },
                    { 3, 3, "sdec", "Good experience but may need additional training in offshore safety protocols.", new DateTime(2025, 4, 6, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6328), "Neutral" }
                });

            migrationBuilder.InsertData(
                table: "NOTIFICATION",
                columns: new[] { "notification_id", "notification_company_id", "notification_status", "notification_text", "notification_timestamp", "notification_user_id" },
                values: new object[,]
                {
                    { 1, "sarawakenergy", "unread", "Your application for Senior Software Developer has been received. We will review it shortly.", new DateTime(2025, 3, 30, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6296), new Guid("61cb59c6-7a41-414d-9138-ea78ff4299b7") },
                    { 2, "sarawakforestrycorporation", "read", "You have been shortlisted for the Forest Conservation Officer position. Please prepare for an interview.", new DateTime(2025, 4, 1, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6297), new Guid("ede7a34c-8871-424a-8d4d-91d38b8b95c6") },
                    { 3, "petronas", "unread", "Thank you for your application to Petronas Carigali. Your application is under review.", new DateTime(2025, 4, 4, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6299), new Guid("640c6a74-3b8c-4fa0-840c-50f782b20367") }
                });

            migrationBuilder.InsertData(
                table: "RESUME",
                columns: new[] { "resume_id", "resume_file", "resume_last_modify_time", "resume_text", "resume_user_id" },
                values: new object[,]
                {
                    { 1, "ahmad_resume.pdf", new DateTime(2025, 3, 25, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6231), "Experienced software developer with expertise in .NET Core, React, and cloud technologies. Worked on enterprise applications for energy sector.", new Guid("61cb59c6-7a41-414d-9138-ea78ff4299b7") },
                    { 2, "siti_resume.pdf", new DateTime(2025, 4, 2, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6234), "Environmental scientist focused on forest conservation. Experience in GIS mapping, biodiversity assessment, and sustainable forest management practices.", new Guid("ede7a34c-8871-424a-8d4d-91d38b8b95c6") },
                    { 3, "rajesh_resume.pdf", new DateTime(2025, 3, 19, 21, 13, 31, 994, DateTimeKind.Local).AddTicks(6237), "Petroleum engineer with extensive experience in offshore drilling. Skills include reservoir analysis, production optimization, and HSE compliance.", new Guid("640c6a74-3b8c-4fa0-840c-50f782b20367") }
                });

            migrationBuilder.InsertData(
                table: "USER_APPLICATION",
                columns: new[] { "UA_ID", "UA_APPLICATION_ID", "UA_USER_ID" },
                values: new object[,]
                {
                    { 1, 1, new Guid("61cb59c6-7a41-414d-9138-ea78ff4299b7") },
                    { 2, 2, new Guid("ede7a34c-8871-424a-8d4d-91d38b8b95c6") },
                    { 3, 3, new Guid("640c6a74-3b8c-4fa0-840c-50f782b20367") }
                });

            migrationBuilder.InsertData(
                table: "USER_SKILL",
                columns: new[] { "US_ID", "US_SKILL_ID", "US_USER_ID" },
                values: new object[,]
                {
                    { 1, 1, new Guid("61cb59c6-7a41-414d-9138-ea78ff4299b7") },
                    { 2, 7, new Guid("ede7a34c-8871-424a-8d4d-91d38b8b95c6") },
                    { 3, 5, new Guid("640c6a74-3b8c-4fa0-840c-50f782b20367") }
                });

            migrationBuilder.InsertData(
                table: "JOB_APPLICATION_TABLE",
                columns: new[] { "TABLE_ID", "TABLE_APPLICATION_ID", "TABLE_COVER_LETTER", "TABLE_RESUME_ID" },
                values: new object[,]
                {
                    { 1, 1, "I am excited to apply for the Senior Software Developer position at Sarawak Energy. My experience developing enterprise applications aligns perfectly with your requirements.", 1 },
                    { 2, 2, "As an environmental scientist passionate about forest conservation, I am eager to contribute to Sarawak Forestry Corporation's mission of sustainable forest management.", 2 },
                    { 3, 3, "With my decade of experience in petroleum engineering, I am confident in my ability to contribute to Petronas Carigali's operations in Sarawak's offshore fields.", 3 }
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
                name: "IX_CAREER_ANALYSIS_analysis_user_id",
                table: "CAREER_ANALYSIS",
                column: "analysis_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Certification_user_id",
                table: "Certification",
                column: "user_id");

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
                name: "IX_JOB_APPLICATION_UserId",
                table: "JOB_APPLICATION",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_APPLICATION_REVIEW_review_application_id",
                table: "JOB_APPLICATION_REVIEW",
                column: "review_application_id");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_APPLICATION_REVIEW_review_company_id",
                table: "JOB_APPLICATION_REVIEW",
                column: "review_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_APPLICATION_TABLE_TABLE_APPLICATION_ID",
                table: "JOB_APPLICATION_TABLE",
                column: "TABLE_APPLICATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_JOB_APPLICATION_TABLE_TABLE_RESUME_ID",
                table: "JOB_APPLICATION_TABLE",
                column: "TABLE_RESUME_ID");

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
                name: "IX_RESUME_resume_user_id",
                table: "RESUME",
                column: "resume_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_USER_APPLICATION_UA_APPLICATION_ID",
                table: "USER_APPLICATION",
                column: "UA_APPLICATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_APPLICATION_UA_USER_ID",
                table: "USER_APPLICATION",
                column: "UA_USER_ID");

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
                name: "CAREER_ANALYSIS");

            migrationBuilder.DropTable(
                name: "Certification");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "JOB_APPLICATION_REVIEW");

            migrationBuilder.DropTable(
                name: "JOB_APPLICATION_TABLE");

            migrationBuilder.DropTable(
                name: "JOB_SKILL");

            migrationBuilder.DropTable(
                name: "NOTIFICATION");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Publication");

            migrationBuilder.DropTable(
                name: "USER_APPLICATION");

            migrationBuilder.DropTable(
                name: "USER_SKILL");

            migrationBuilder.DropTable(
                name: "Work_Experience");

            migrationBuilder.DropTable(
                name: "RESUME");

            migrationBuilder.DropTable(
                name: "JOB_APPLICATION");

            migrationBuilder.DropTable(
                name: "SKILL");

            migrationBuilder.DropTable(
                name: "JOB");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "COMPANY");

            migrationBuilder.DropTable(
                name: "AREA");
        }
    }
}
