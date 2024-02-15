using Microsoft.AspNetCore.Mvc.Testing;
using TaskManager.Api.Tests.Abstractions;
using TaskManager.Common.Models;
using Xunit;

namespace TaskManager.Api.Tests
{
    public class ProjectsControllerIntegration : IntegrationTestsBase
    {
        public readonly string editorAuth; // строка бозовой авторизации редактора
        public readonly string userAuth; // строка бозовой авторизации пользователя
        public ProjectsControllerIntegration(WebApplicationFactory<Program> fixture) : base(fixture)
        {

            editorAuth = GetAuth(UserStatus.Editor, DB);
            userAuth = GetAuth(UserStatus.User, DB);
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
