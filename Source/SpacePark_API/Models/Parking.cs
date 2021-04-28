using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpacePark_API.Models;

namespace SpacePark_API.Models
{
    public class Parking
    {
        public int ID { get; set; }
        public string PersonName { get; set; }
        public string SpaceShip { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool Payed { get; set; }
    }

    public class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost,41433; Database=SpacePortDB; User ID=SA; Password=verystrong!pass123");
        }
        public DbSet<SpacePark_API.Models.Parking> Parking { get; set; }
    }
}
