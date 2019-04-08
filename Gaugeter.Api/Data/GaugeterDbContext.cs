using Gaugeter.Api.Authentication.Models.Data;
using Gaugeter.Api.Devices.Models.Data;
using Gaugeter.Api.Jobs.Models;
using Gaugeter.Api.Users.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Data
{
    public class GaugeterDbContext : DbContext
    {
        public GaugeterDbContext(DbContextOptions<GaugeterDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<ActiveToken> ActiveToken { get; set; }

        //public DbSet<TelemData> TelemData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433; Database=Gaugeter;User=SA; Password=yourStrong(!)Password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Devices);
        }
    }
}
