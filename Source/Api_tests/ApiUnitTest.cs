using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using SpacePark_API;
using SpacePark_API.Models;
using SpacePark_API.Models.APIModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api_tests
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
            var response = await _client.GetAsync("api/Parking");
            response.StatusCode.Equals(HttpStatusCode.OK);

            var test = JsonConvert.DeserializeObject<Parking[]>(await response.Content.ReadAsStringAsync());
            Assert.Equal(18, test.Length);
        }
    }

    public class ParkingControllerTests2
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ParkingControllerTests2()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetAllParkings()
        {
            // Act
            var response = await _client.GetAsync("api/parking");
            response.EnsureSuccessStatusCode();
            var responseString = JsonConvert.DeserializeObject<Parking[]>(await response.Content.ReadAsStringAsync());
            // Assert
            Assert.Equal(18, responseString.Length);
        }

        [Fact]
        public async Task GetCurrentParkingForBobaFett()
        {
            // Act
            var response = await _client.GetAsync("api/parking/Boba_Fett/current");
            response.EnsureSuccessStatusCode();
            var responseString = JsonConvert.DeserializeObject<Parking>(await response.Content.ReadAsStringAsync());

            Parking correctParking = new Parking { PersonName = "boba fett", Paid = false, StarShip = "Death Star" };
            // Assert
            Assert.Equal(correctParking.PersonName, responseString.PersonName);
            Assert.Equal(correctParking.Paid, responseString.Paid);
            Assert.Equal(correctParking.StarShip, responseString.StarShip);
        }

        [Fact]
        public async Task GetAllParkingsForBobaFett()
        {
            // Act
            var response = await _client.GetAsync("api/parking/Boba_Fett/all");
            response.EnsureSuccessStatusCode();
            var responseString = JsonConvert.DeserializeObject<Parking[]>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(3, responseString.Length);
        }

        [Fact]
        public async Task ESH()
        {
            ParkingRequest newParking = new ParkingRequest { PersonName = "Watto", SpacePortID = 3, StarShip = "Death Star", ArrivalTime = DateTime.Now };
            var e = new StringContent(newParking.ToString(), Encoding.UTF8, "application/json");
            //WebRequest test = WebRequest.Create("https://localhost:5001/api/Parking/register");
            //HttpRequestMessage request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Post,
            //    RequestUri = new Uri("https://localhost:5001/api/Parking/register"),
            //    Content = e
            //};
            //Uri testUri = new Uri("https://localhost:5001/api/Parking/register");
            // Act
            var response = await _client.PostAsync("https://localhost:5001/api/Parking/register", e);
            var t = response.Content;
            response.EnsureSuccessStatusCode();

            // Assert
            //Assert.Equal(3, responseString.Length);
            Assert.NotNull(response.EnsureSuccessStatusCode());
        }
    }
}
