using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePark_API.Models.APIModels
{
    public class ParkingRequest
    {
        public string PersonName { get; set; }
        public string StarShip { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int SpacePortID { get; set; }
    }
}
