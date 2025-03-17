using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIJobCareer.Migrations
{
    /// <inheritdoc />
    public partial class Datas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    AREA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AREA_NAME = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.AREA_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SKILL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SKILL_NAME = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SKILL_INFO = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SKILL_TYPE = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SKILL_LEVEL = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SKILL_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    COMPANY_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    COMPANY_NAME = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    COMPANY_ICON = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    COMPANY_INTRO = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    COMPANY_WEBSITE = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    COMPANY_AREA_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.COMPANY_ID);
                    table.ForeignKey(
                        name: "FK_Company_Area_COMPANY_AREA_ID",
                        column: x => x.COMPANY_AREA_ID,
                        principalTable: "Area",
                        principalColumn: "AREA_ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    USER_NAME = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USER_AGE = table.Column<int>(type: "int", nullable: true),
                    USER_INTRO = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USER_CONTACT_NUMBER = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USER_EMAIL = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USER_PASSWORD = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USER_ICON = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USER_PRIVACY_STATUS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USER_ROLE = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USER_ACCOUNT_CREATED_TIME = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    USER_AREA_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.USER_ID);
                    table.ForeignKey(
                        name: "FK_User_Area_USER_AREA_ID",
                        column: x => x.USER_AREA_ID,
                        principalTable: "Area",
                        principalColumn: "AREA_ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JOB_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JOB_COMPANY_ID = table.Column<int>(type: "int", nullable: false),
                    JOB_TITLE = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JOB_RESPONSIBLE = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JOB_SALARY_MIN = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    JOB_SALARY_MAX = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    JOB_LOCATION = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JOB_STATUS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JOB_BENEFIT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JOB_REQUIREMENT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JOB_ID);
                    table.ForeignKey(
                        name: "FK_Job_Company_JOB_COMPANY_ID",
                        column: x => x.JOB_COMPANY_ID,
                        principalTable: "Company",
                        principalColumn: "COMPANY_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Career_Analysis",
                columns: table => new
                {
                    ANALYSIS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ANALYSIS_USER_ID = table.Column<int>(type: "int", nullable: false),
                    ANALYSIS_AI_DIRECTION = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ANALYSIS_AI_MARKET_GAP = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ANALYSIS_AI_CAREER_PROSPECTS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Career_Analysis", x => x.ANALYSIS_ID);
                    table.ForeignKey(
                        name: "FK_Career_Analysis_User_ANALYSIS_USER_ID",
                        column: x => x.ANALYSIS_USER_ID,
                        principalTable: "User",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NOTIFICATION_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOTIFICATION_USER_ID = table.Column<int>(type: "int", nullable: true),
                    NOTIFICATION_COMPANY_ID = table.Column<int>(type: "int", nullable: true),
                    NOTIFICATION_TEXT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NOTIFICATION_TIMESTAMP = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NOTIFICATION_STATUS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NOTIFICATION_ID);
                    table.ForeignKey(
                        name: "FK_Notification_Company_NOTIFICATION_COMPANY_ID",
                        column: x => x.NOTIFICATION_COMPANY_ID,
                        principalTable: "Company",
                        principalColumn: "COMPANY_ID");
                    table.ForeignKey(
                        name: "FK_Notification_User_NOTIFICATION_USER_ID",
                        column: x => x.NOTIFICATION_USER_ID,
                        principalTable: "User",
                        principalColumn: "USER_ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Resume",
                columns: table => new
                {
                    RESUME_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RESUME_USER_ID = table.Column<int>(type: "int", nullable: false),
                    RESUME_TEXT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RESUME_FILE = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RESUME_LAST_MODIFY_TIME = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resume", x => x.RESUME_ID);
                    table.ForeignKey(
                        name: "FK_Resume_User_RESUME_USER_ID",
                        column: x => x.RESUME_USER_ID,
                        principalTable: "User",
                        principalColumn: "USER_ID",
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
                        principalColumn: "SKILL_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USer_Skill_User_US_USER_ID",
                        column: x => x.US_USER_ID,
                        principalTable: "User",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Job_Application",
                columns: table => new
                {
                    APPLICATION_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    APPLICATION_JOB_ID = table.Column<int>(type: "int", nullable: false),
                    APPLICATION_TYPE = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    APPLICATION_STATUS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    APPLICATION_SUBMISSION_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Application", x => x.APPLICATION_ID);
                    table.ForeignKey(
                        name: "FK_Job_Application_Job_APPLICATION_JOB_ID",
                        column: x => x.APPLICATION_JOB_ID,
                        principalTable: "Job",
                        principalColumn: "JOB_ID",
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
                        principalColumn: "JOB_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Skill_Skill_JS_SKILL_ID",
                        column: x => x.JS_SKILL_ID,
                        principalTable: "Skill",
                        principalColumn: "SKILL_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Job_Application_Review",
                columns: table => new
                {
                    REVIEW_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    REVIEW_APPLICATION_ID = table.Column<int>(type: "int", nullable: false),
                    REVIEW_COMPANY_ID = table.Column<int>(type: "int", nullable: false),
                    REVIEW_STATUS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    REVIEW_CONTEXT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    REVIEW_DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Application_Review", x => x.REVIEW_ID);
                    table.ForeignKey(
                        name: "FK_Job_Application_Review_Company_REVIEW_COMPANY_ID",
                        column: x => x.REVIEW_COMPANY_ID,
                        principalTable: "Company",
                        principalColumn: "COMPANY_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Application_Review_Job_Application_REVIEW_APPLICATION_ID",
                        column: x => x.REVIEW_APPLICATION_ID,
                        principalTable: "Job_Application",
                        principalColumn: "APPLICATION_ID",
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
                        principalColumn: "APPLICATION_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Application_Table_Resume_TABLE_RESUME_ID",
                        column: x => x.TABLE_RESUME_ID,
                        principalTable: "Resume",
                        principalColumn: "RESUME_ID",
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
                        principalColumn: "APPLICATION_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Application_User_UA_USER_ID",
                        column: x => x.UA_USER_ID,
                        principalTable: "User",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Career_Analysis_ANALYSIS_USER_ID",
                table: "Career_Analysis",
                column: "ANALYSIS_USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_COMPANY_AREA_ID",
                table: "Company",
                column: "COMPANY_AREA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JOB_COMPANY_ID",
                table: "Job",
                column: "JOB_COMPANY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Application_APPLICATION_JOB_ID",
                table: "Job_Application",
                column: "APPLICATION_JOB_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Application_Review_REVIEW_APPLICATION_ID",
                table: "Job_Application_Review",
                column: "REVIEW_APPLICATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Application_Review_REVIEW_COMPANY_ID",
                table: "Job_Application_Review",
                column: "REVIEW_COMPANY_ID");

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
                name: "IX_Notification_NOTIFICATION_COMPANY_ID",
                table: "Notification",
                column: "NOTIFICATION_COMPANY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_NOTIFICATION_USER_ID",
                table: "Notification",
                column: "NOTIFICATION_USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_RESUME_USER_ID",
                table: "Resume",
                column: "RESUME_USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_User_USER_AREA_ID",
                table: "User",
                column: "USER_AREA_ID");

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

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastLoginAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.AccountId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
