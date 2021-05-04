using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark_API.Models
{
    public class Starships
    {
        public int Count { get; set; }
        public List<Starship> Results { get; set; }
        public string Next { get; set; }
    }
    public class Starship
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Length { get; set; }
    }
}
