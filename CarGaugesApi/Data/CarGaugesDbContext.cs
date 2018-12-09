using System;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Data
{
    public class CarGaugesDbContext : DbContext
    {
        public CarGaugesDbContext(DbContextOptions<CarGaugesDbContext> options) : base(options)
        {
        }

        public DbSet<Models.User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433; Database=CarGauges;User=SA; Password=yourStrong(!)Password");
        }
    }
}
