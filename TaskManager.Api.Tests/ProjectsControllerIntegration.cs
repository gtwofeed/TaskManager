using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using TaskManager.Common.Models;
using Xunit;

namespace TaskManager.Api.Tests
{
    public class ProjectsControllerIntegration : IntegrationTest
    {
        public ProjectsControllerIntegration(WebApplicationFactory<Program> fixture) : base(fixture) { }

        [Fact]
        public async Task Create__SendRequest_ShouldId2()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
