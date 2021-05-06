using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpacePark_API.Controllers;
using SpacePark_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpacePark_API_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var data = new List<Parking>
            {
                new Parking
                {
                    PersonName = "Watto",
                    StarShip = "x-wing",
                    ArrivalTime = DateTime.Now,
                    Paid = false
                },
                new Parking
                {
                    PersonName = "R2-D2",
                    StarShip = "Y-wing",
                    ArrivalTime = DateTime.Now,
                    Paid = false
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Parking>>();
            mockSet.As<IQueryable<Parking>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Parking>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Parking>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Parking>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<MyContext>();
            mockContext.Setup(c => c.Parking).Returns(mockSet.Object);

            var service = new ParkingController(mockContext.Object);
            var blogs = service.GetParking();

            Assert.AreEqual(3, blogs.Result.Value.Count());
        }
    }
}
