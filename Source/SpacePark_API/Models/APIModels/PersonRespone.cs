using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark_API.Models
{
    class PersonsResponse
    {
        public int Count { get; set; }
        public List<Person> Results { get; set; }
        public string Next { get; set; }
    }


    class Person
    {
        public string Name { get; set; }
        public string BirthYear { get; set; }
    }
}
