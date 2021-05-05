using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpacePark_API.Controllers;
using SpacePark_API.Models;
using System.Collections.Generic;

namespace SpacePark_APItest
{
    [TestClass]
    public class UnitTest1
    {
        private MyContext context;

        [TestMethod]
        public void TestMethod1()
        {
            var controller = new ParkingController(context);

            var result = controller.GetParking().Result;
            Assert.AreEqual("16", result);
        }
    }
}
