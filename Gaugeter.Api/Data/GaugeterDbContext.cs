using Gaugeter.Api.Authentication.Models.Data;
using Gaugeter.Api.Devices.Models.Data;
using Gaugeter.Api.Jobs.Models.Data;
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
        public DbSet<UserDevice> UserDevice { get; set; }

        public DbSet<TelemData> TelemData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433; Database=Gaugeter;User=SA; Password=yourStrong(!)Password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDevice>()
                .HasKey(t => new { t.UserId, t.BluetoothAddress });

            modelBuilder.Entity<UserDevice>()
                .HasOne(ud => ud.User)
                .WithMany(u => u.Devices)
                .HasForeignKey(ud => ud.UserId);

            modelBuilder.Entity<UserDevice>()
                .HasOne(ud => ud.Device)
                .WithMany(d => d.Users)
                .HasForeignKey(ud => ud.BluetoothAddress);

            modelBuilder.Entity<Job>()
                .HasMany(j => j.Telem);
        }
    }
}
