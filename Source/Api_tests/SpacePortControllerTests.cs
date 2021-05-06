using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SpacePark_API;
using SpacePark_API.Models;
using SpacePark_API.Models.APIModels;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SpacePark_API.Controllers;
using System.Linq;

namespace Api_tests
{
    public class SpacePortControllerTests
    {
        private DbContext _context;
        SpacePortsController _controller;
        public void Setup()
        {
            var dbContextOptions =
                new DbContextOptionsBuilder<MyContext>().UseInMemoryDatabase(databaseName: "TestDB");
            _context = new MyContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();

            _controller = new((MyContext)_context);
        }

        public void Close()
        {
            _context.Database.EnsureDeleted();
        }

        public void Seeding()
        {
            Setup();

            SpacePort newSpaceport = new() { Name = "testSpacePort", TotalCapacity = 10 };
            _context.Add(newSpaceport);

            newSpaceport = new() { Name = "testSpacePort2", TotalCapacity = 5 };
            _context.Add(newSpaceport);

            _context.SaveChanges();
        }

        [Fact]
        public async Task CreateSpacePortAndGetSpaceport()
        {
            Setup();

            SpacePort newSpaceport = new() { Name = "testSpacePort", TotalCapacity = 10 };

            await _controller.PostSpacePort(newSpaceport);

            var result = await _controller.GetSpacePorts();

            Assert.Single(result.Value);
            Close();
        }

        [Fact]
        public async Task GetAllSpacePorts()
        {
            Setup();
            Seeding();

            var result = await _controller.GetSpacePorts();

            Assert.Equal(2, result.Value.Count());
            Close();
        }
    }
}
