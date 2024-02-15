using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using TaskManager.Common.Models;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Tests
{
    public class AccountControllerIntegrationTests : IntegrationTestsBase
    {
        readonly string incorrectAuth; // строка бозовой авторизации не существуещего пользователя
        public AccountControllerIntegrationTests(WebApplicationFactory<Program> fixture) : base(fixture)
        {
            incorrectAuth = GetAuth(UserStatus.User);
        }

        [Fact]
        public async Task GetToken_SendRequest_Should_IncorrectLoginPass()
        {
            // Act
            var response = await SendRequestAsync(HttpMethod.Post, "api/account", incorrectAuth);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetToken_SendRequest_Should_OK()
        {
            // Act
            var response = await SendRequestAsync(HttpMethod.Post, "api/account", adminAuth);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
