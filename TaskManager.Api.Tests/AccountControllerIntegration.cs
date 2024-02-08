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
    public class AccountControllerIntegration
    {

        /*[Fact]
        public async Task GetToken_SendRequest_Should_IncorrectLoginPass()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/token");
            request.Headers.Add("Authorization", IncorrectAuth);
            var content = new StringContent("", null, "text/plain");
            request.Content = content;

            // Act
            var response = await apiClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }*/

        [Fact]
        public async Task GetToken_SendRequest_Should_OK()
        {
            // Arrange

            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<ApplicationContext>));
                    services.AddDbContext<ApplicationContext>(option =>
                    {
                        option.UseInMemoryDatabase("test");
                    });
                });
            });

            ApplicationContext db = webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationContext>()!;

            List<User> users = [
                new()
                {
                    Email = "user",
                    Password = "User123",
                    Status = UserStatus.User,
                },
                new()
                {
                    Email = "editor",
                    Password = "Editor123",
                    Status = UserStatus.Editor,
                }];

            db.AddRange(users);
            db.SaveChanges();
            var client = webHost.CreateClient();



            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/token");
            request.Headers.Add("Authorization", "Basic ZmlzdGFkbWluOmFkbWlu");
            var content = new StringContent("", null, "text/plain");
            request.Content = content;

            // Act
            var response = await client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
