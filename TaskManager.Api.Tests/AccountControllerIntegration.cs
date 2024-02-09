using FluentAssertions;
using System.Net;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Tests
{
    public class AccountControllerIntegration : CommonContext
    {

        [Fact]
        public async Task GetToken_SendRequest_Should_IncorrectLoginPass()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/token");
            request.Headers.Add("Authorization", incorrectAuth);
            var content = new StringContent("", null, "text/plain");
            request.Content = content;

            // Act
            var response = await apiClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetToken_SendRequest_Should_OK()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/token");
            request.Headers.Add("Authorization", adminAuth); // "Basic ZmlzdGFkbWluOmFkbWlu"
            var content = new StringContent("", null, "text/plain");
            request.Content = content;

            // Act
            var response = await apiClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
