using Azure.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using TaskManager.Common.Models;
using Xunit;


namespace TaskManager.Api.Tests
{
    public class UsersControllerIntegration : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly HttpClient client; // единный контекст для всех тестов
        readonly string adminAuth; // строка бозовой авторизации админа


        public UsersControllerIntegration(WebApplicationFactory<Program> application)
        {
            application.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddDbContext<ApplicationContext>(option =>
                    {
                        option.UseInMemoryDatabase("test_db");
                    });
                });
            });
            client = application.CreateClient();
            adminAuth = "Basic ZmlzdGFkbWluOmFkbWlu";
        }

        string GetAuth(ApplicationContext context, UserStatus status)
        {
            string username = "";
            string password = "";
            var user = context.Users.FirstOrDefault(u => u.Status == status) ?? context.Users.FirstOrDefault();
            return $"Basic {Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{username}:{password}"))}";
        }

        [Fact]
        public async Task CheckStatus_SendRequest_ShouldOk()
        {

            // Act
            var response = await client.GetAsync("api/users/check-status");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Create_SendRequest_ShouldCount1()
        {

            // Act
            var request = new HttpRequestMessage(HttpMethod.Post, "api/account/token");
            request.Headers.Add("Authorization", adminAuth);
            var content = new StringContent("", null, "text/plain");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var token = JsonSerializer.Deserialize<Token>(await response.Content.ReadAsStringAsync());

            request = new HttpRequestMessage(HttpMethod.Get, "api/users/all");
            request.Headers.Add("Authorization", $"Bearer {token}");
            response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var users = JsonSerializer.Deserialize<List<UserDTO>>(await response.Content.ReadAsStringAsync());

            users.Should().HaveCount(2);
            // Assert
        }
    }
}