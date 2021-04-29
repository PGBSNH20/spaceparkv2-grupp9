using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark_API.Models
{
    public class Receipt
    {
        public int ID { get; set; }
        public int PayID { get; set; }
        public string PersonName { get; set; }
        public string StarShip { get; set; }
        public Double Price { get; set; }
    }
}
