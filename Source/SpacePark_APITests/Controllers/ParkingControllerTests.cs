using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpacePark_API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using SpacePark_API.Models.APIModels;


namespace SpacePark_API.Controllers.Tests
{
    public class ParkingControllerTests
    {
        [TestMethod()]
        public void PostParkingTest()
        {
            var client = new RestClient("https://localhost:5001/api/");
            var request = new RestRequest("parking/register", DataFormat.Json);

            var parking = new ParkingRequest { ArrivalTime = DateTime.Now, PersonName = "Boba Fett", SpacePortID = 1, StarShip = "millennium falcon" };


            client.Execute(request);

            Assert.Fail();
        }

        [TestMethod()]
        public void GetId()
        {
            

        }
    }
}