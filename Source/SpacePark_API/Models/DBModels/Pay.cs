using System;
using System.ComponentModel.DataAnnotations.Schema;
using SpacePark_API.Models;

namespace SpacePark_API.Models
{
    public class Pay
    {
        public int ID { get; set; }
        [ForeignKey("ParkID")]
        public int ParkID { get; set; }
        public Parking Park { get; set; }
        public int Invoice { get; set; }
        public DateTime DepartTime { get; set; }
    

    }
}