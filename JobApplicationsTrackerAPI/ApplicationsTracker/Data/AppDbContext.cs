using ApplicationsTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    }
}
