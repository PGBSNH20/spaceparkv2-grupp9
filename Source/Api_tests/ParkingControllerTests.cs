using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpacePark_API.Controllers;
using SpacePark_API.Models;
using SpacePark_API.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api_tests
{
    public class ParkingControllerTests
    {
        private DbContext _context;
        private ParkingController _controller;
        public void Setup()
        {
            var dbContextOptions =
                new DbContextOptionsBuilder<MyContext>().UseInMemoryDatabase(databaseName: "TestDB");
            _context = new MyContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();

            _controller = new ParkingController((MyContext)_context);

        }

        public void Close()
        {
            _context.Database.EnsureDeleted();
        }

        public void Seeding()
        {
            Setup();
            SpacePort newSpaceport = new() { Name = "testSpacePort", TotalCapacity = 10 };

            Parking newParking = new() { PersonName = "Watto", StarShip = "x-wing", ArrivalTime = DateTime.Now, Paid = false, SpacePort = newSpaceport };
            _context.Add(newParking);
            newParking = new() { PersonName = "Watto", StarShip = "x-wing", ArrivalTime = new DateTime(2021, 04, 06, 12,00,00), Paid = true, SpacePort = newSpaceport };
            _context.Add(newParking);
            newParking = new() { PersonName = "Watto", StarShip = "x-wing", ArrivalTime = new DateTime(2021, 01, 06, 12, 00, 00), Paid = true, SpacePort = newSpaceport };
            _context.Add(newParking);
            newParking = new() { PersonName = "R2-D2", StarShip = "Y-wing", ArrivalTime = DateTime.Now, Paid = false, SpacePort = newSpaceport };
            _context.Add(newParking);

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllParkings()
        {
            Setup();
            Seeding();

            var result = await _controller.GetParking();
            Close();

            Assert.Equal(4, result.Value.Count());
        }

        [Fact]
        public async Task GetCurrentParking()
        {
            Setup();
            Seeding();

            var result = await _controller.GetCurrentParking("Watto");
            Close();

            Assert.False(result.Value.Paid);
            Assert.Equal("Watto", result.Value.PersonName);
        }

        [Fact]
        public async Task GetAllParkingsForOneCharacter()
        {
            Setup();
            Seeding();

            var result = await _controller.GetAllParkingForCharacter("Watto");

            Close();
            Assert.Equal(3, result.Value.Count());
        }

        [Fact]
        public async Task PostNewParking()
        {
            Setup();
            Seeding();
            ParkingRequest parkingRequest = new() { PersonName = "Watto", StarShip = "Death Star", ArrivalTime = DateTime.Now, SpacePortID = 1 };

            var result =  _controller.PostParking(parkingRequest, "Watto", "Death Star");
            Close();

            Assert.True(result.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task PutPayParking()
        {
            Setup();
            Seeding();

            PayParkingRequest payParking = new() { ID = 4, SpacePortID = 1 };

            var result = await _controller.PutParking(4, payParking);

            var statusCode = result.GetType().GetProperty("StatusCode").GetValue(result, null);

            Assert.Equal(200, statusCode);
        }
    }
}
