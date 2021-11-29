using System.Data.Entity;
using TimeSheet.Entities;

namespace TimeSheet.Repository
{
    public class TimeSheetDbContext : DbContext
    {
        public TimeSheetDbContext() :base (@"Server=DESKTOP-R0IITTI;Database=TimeSheet;Trusted_Connection=True;")
        {

        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<UserOnProject> UsersOnProjects { get; set; }

        public DbSet<TimeSheetEntry> TimeSheetEntries { get; set; }

        public DbSet<TeamLeader> TeamLeaders { get; set; }

    }
}
