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

        public MyContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
