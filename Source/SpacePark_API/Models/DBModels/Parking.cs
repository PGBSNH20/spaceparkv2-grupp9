using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpacePark_API.Models;
using System.ComponentModel.DataAnnotations;

namespace SpacePark_API.Models
{
    public class Parking
    {
        public int ID { get; set; }
        public string PersonName { get; set; }
        public string StarShip { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool Paid { get; set; }
        [Required]
        public SpacePort SpacePort { get; set; }
    }

}
