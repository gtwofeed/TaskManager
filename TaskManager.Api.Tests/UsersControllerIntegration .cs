using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using System.Text;
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
            string connection = "Server=(localdb)\\mssqllocaldb;Database=TaskManagerTestDb;Trusted_Connection=True";
            var webHost = application.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<ApplicationContext>));
                    services.AddDbContext<ApplicationContext>(option =>
                    {
                        option.UseSqlServer(connection);
                    });
                });
            });

            client = webHost.CreateClient();


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
        bool EqueUsersDTO(UserDTO oldUser, UserDTO modUser)
        {
            if (oldUser.Id != modUser.Id) return false;
            else if (oldUser.Email != modUser.Email) return false;
            else if (oldUser.Password != modUser.Password) return false;
            else if (oldUser.Status != modUser.Status) return false;
            else if (oldUser.RegistrationDate != modUser.RegistrationDate) return false;
            else if (oldUser.FirstName != modUser.FirstName) return false;
            else if (oldUser.LastName != modUser.LastName) return false;
            else if (oldUser.Phone != modUser.Phone) return false;
            else if (!oldUser.Photo.SequenceEqual(modUser.Photo)) return false;
            return true;
        }

        [Fact]
        public async Task Check_SendRequest_ShouldOk()
        {
            // Arrange

            // Act
            var response = await client.GetAsync("api/users/check");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_SendRequest_ShouldId4()
        {
            // Arrange


            // Act


            // Assert
        }

        [Fact]
        public async Task Cet_SendRequest_ShouldUserDTOId2()
        {
            // Arrange


            // Act


            // Assert
        }

        [Fact]
        public async Task Update_SendRequest_ShouldEqueUsersDTO()
        {
            // Arrange


            // Act


            // Assert
        }

        [Fact]
        public async Task Delete_SendRequest_ShouldOk()
        {
            // Arrange


            // Act


            // Assert
        }

        [Fact]
        public async Task GetUsers_SendRequest_ShouldCount3()
        {
            // Arrange


            // Act


            // Assert
        }
    }
}