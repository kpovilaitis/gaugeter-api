using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Data
{
    public class CarGaugesDbContext : DbContext
    {
        public CarGaugesDbContext(DbContextOptions<CarGaugesDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<Work> Work { get; set; }
        //public DbSet<TelemData> TelemData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433; Database=CarGauges;User=SA; Password=yourStrong(!)Password");
        }
    }
}
