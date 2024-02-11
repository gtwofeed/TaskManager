using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using TaskManager.Common.Models;
using Xunit;

namespace TaskManager.Api.Tests
{
    public class ProjectsControllerIntegration : IntegrationTest
    {
        public readonly string editorAuth; // строка бозовой авторизации редактора
        public readonly string userAuth; // строка бозовой авторизации пользователя
        public ProjectsControllerIntegration(WebApplicationFactory<Program> fixture) : base(fixture)
        {

            editorAuth = GetAuth(UserStatus.Editor, db);
            userAuth = GetAuth(UserStatus.User, db);
        }

        [Fact]
        public async Task Create__SendRequest_ShouldId2()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
