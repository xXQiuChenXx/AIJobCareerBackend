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
            // 
        }
    }
}
