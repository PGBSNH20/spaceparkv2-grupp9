using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpacePark_API.Models;

namespace SpacePark_API.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Parking> Parking { get; set; }
        public DbSet<Pay> Pay { get; set; }
        public DbSet<Receipt> Receipts {get; set;}
        public DbSet<SpacePort> SpacePorts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost,41433; Database=SpacePortDB; User ID=SA; Password=verystrong!pass123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parking>().HasKey(x => x.ID);
            modelBuilder.Entity<SpacePort>().HasKey(x => x.ID);
        }
    }
}
