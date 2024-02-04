using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using Xunit;
using Microsoft.Extensions.Configuration;


namespace TaskManager.Api.Tests
{
    public class UsersControllerIntegration : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly HttpClient client; // единный контекст для всех тестов
        
        public UsersControllerIntegration(WebApplicationFactory<Program> application)
        {
            client = application.CreateClient();
        }

        [Fact]
        public async Task CheckStatus_SendRequest_ShouldOk()
        {

            // Act
            var response = await client.GetAsync("api/users/check-status");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}