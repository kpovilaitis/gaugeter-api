﻿using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Data
{
    public class CarGaugesDbContext : DbContext
    {
        public CarGaugesDbContext(DbContextOptions<CarGaugesDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<Work> Work { get; set; }
        public DbSet<ActiveToken> ActiveToken { get; set; }

        //public DbSet<TelemData> TelemData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433; Database=CarGauges;User=SA; Password=yourStrong(!)Password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDevice>()
                .HasKey(t => new { t.UserId, t.DeviceAddress });

            modelBuilder.Entity<UserDevice>()
                .HasOne(ud => ud.User)
                .WithMany(u => u.UserDevices)
                .HasForeignKey(ud => ud.UserId);

            modelBuilder.Entity<UserDevice>()
                .HasOne(ud => ud.Device)
                .WithMany(d => d.DeviceUsers)
                .HasForeignKey(ud => ud.DeviceAddress);
        }
    }
}
