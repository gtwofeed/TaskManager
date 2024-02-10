using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using TaskManager.Common.Models;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Tests
{
    public class UsersControllerIntegration : IntegrationTest
    {
        public UsersControllerIntegration(WebApplicationFactory<Program> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Check_SendRequest_ShouldOk()
        {
            // Arrange in CommonContext

            // Act
            var response = await apiClient.GetAsync("api/users/check");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_SendRequest_ShouldId4()
        {
            // Arrange
            var dto = new UserDTO
            {
                Email = "createTest",
                Password = "CreateTest123",
                Status = UserStatus.User,
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "api/users");
            request.Headers.Add("Authorization", $"Bearer {await GetToken(adminAuth)}");

            StringContent content = new (JsonSerializer.Serialize(dto), null, "application/json");
            request.Content = content;

            // Act
            var response = await apiClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Be("4");
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
        public async Task GetUsers_SendRequest_ShouldCount1()
        {
            // Arrange


            // Act

            // Assert
        }

        async Task<string?> GetToken(string baseAutString)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/account");
            request.Headers.Add("Authorization", baseAutString); // "Basic ZmlzdGFkbWluOmFkbWlu"


            var response = await apiClient.SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();
            Token? token = JsonSerializer.Deserialize<Token>(json);

            return token?.ToString();
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
            else if (oldUser.Photo != null && modUser.Photo == null) return false;
            else if (oldUser.Photo == null && modUser.Photo != null) return false;
            else if (oldUser.Photo != null && modUser.Photo != null && !oldUser.Photo.SequenceEqual(modUser.Photo)) return false;
            return true;
        }
    }
}