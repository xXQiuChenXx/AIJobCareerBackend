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

        public DbSet<UserAccount> UserAccount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 
        }
    }
}
