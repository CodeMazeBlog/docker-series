using AccountOwnerServer;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Integration
{
    public class IntegrationTests
    {
        private readonly TestContext _context;

        public IntegrationTests()
        {
            _context = new TestContext();
        }

        [Fact]
        public async Task GetAllOwners_ReturnsOkResponse()
        {
            // Act
            var response = await _context.Client.GetAsync("/api/owner");
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllOwners_ReturnsAListOfOwners()
        {
            // Act
            var response = await _context.Client.GetAsync("/api/owner");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var owners = JsonConvert.DeserializeObject<List<Owner>>(responseString);

            // Assert
            Assert.NotEmpty(owners);
        }
    }
}
