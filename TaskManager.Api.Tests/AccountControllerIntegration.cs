using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using TaskManager.Api.Models;
using TaskManager.Common.Models;
using Xunit;

namespace TaskManager.Api.Tests
{
    public class AccountControllerIntegration : ComonnContext
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
