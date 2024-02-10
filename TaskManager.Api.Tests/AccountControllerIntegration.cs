using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Tests
{
    public class AccountControllerIntegration : IntegrationTest
    {
        public AccountControllerIntegration(WebApplicationFactory<Program> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetToken_SendRequest_Should_IncorrectLoginPass()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "api/account");
            request.Headers.Add("Authorization", incorrectAuth);

            // Act
            var response = await apiClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetToken_SendRequest_Should_OK()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "api/account");
            request.Headers.Add("Authorization", adminAuth); // "Basic ZmlzdGFkbWluOmFkbWlu"

            // Act
            var response = await apiClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
