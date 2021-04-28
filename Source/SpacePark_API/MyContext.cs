using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePark_API
{
    public class MyContext : DbContext
    {
        public virtual DbSet<Park> Park { get; set; }

        public MyContext() : base()
        {

        }

        //Byt connection string så det funkar för dig
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost,41433; Database=SpacePortDB; User ID=SA; Password=verystrong!pass123");
        }
    }
}
