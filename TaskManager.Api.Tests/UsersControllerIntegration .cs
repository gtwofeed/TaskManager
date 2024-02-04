using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;


namespace TaskManager.Api.Tests
{
    public class UsersControllerIntegration
    {
        [Fact]
        public async Task CheckStatus_SendRequest_ShouldOk()
        {

            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("api/users/check-status");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}