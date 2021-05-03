using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpacePark_API.Models;

namespace SpacePark_API.Models
{
    public class SpacePort
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TotalCapacity { get; set; }
    }

}
