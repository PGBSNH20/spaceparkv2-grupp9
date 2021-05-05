using SpacePark_API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using SpacePark_API.Models.APIModels;
using SpacePark_API.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace SpacePark_API.Controllers.Tests
{
    public class ParkingControllerTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        readonly HttpClient _client;
      
        public ParkingControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task GetParking()
        {
            var response = await _client.GetAsync("/Parking");
            response.StatusCode.Equals(HttpStatusCode.OK);

            var test = JsonConvert.DeserializeObject<Parking[]>(await response.Content.ReadAsStringAsync());
            Assert.Equal(16, test.Count());
            
        }
    }
}