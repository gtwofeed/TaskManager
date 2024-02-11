using Azure.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;
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
            request.Headers.Add("Authorization", await GetBearerToken(adminAuth));

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
            int id = 2;

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/users/{id}");
            request.Headers.Add("Authorization", await GetBearerToken(adminAuth));

            // Act
            var response = await apiClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string responseJson = await response.Content.ReadAsStringAsync();
            UserDTO? user = JsonSerializer.Deserialize<UserDTO>(responseJson, options);

            user?.Id.Should().Be(id);
        }

        [Fact]
        public async Task Update_SendRequest_ShouldEqueUsersDTO()
        {
            // Arrange
            int id = 2;
            var dto = new UserDTO();
            var dtoMod = new UserDTO();
            string baseToken = await GetBearerToken(adminAuth);

            var requestGet = new HttpRequestMessage(HttpMethod.Get, $"api/users/{id}");
            requestGet.Headers.Add("Authorization", baseToken);

            // Act Get user
            var response = await apiClient.SendAsync(requestGet);

            // Assert Get user
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string responseJson = await response.Content.ReadAsStringAsync();
            dto = JsonSerializer.Deserialize<UserDTO>(responseJson, options);
            dto.Should().NotBeNull();
            dto!.Id.Should().Be(id);

            // Arrange Update user
            Random random = new Random();
            dto.Password += random.Next(10).ToString();

            var requestUpdate = new HttpRequestMessage(HttpMethod.Patch, $"api/users/{id}");
            requestUpdate.Headers.Add("Authorization", baseToken);

            StringContent content = new(JsonSerializer.Serialize(dto), null, "application/json");
            requestUpdate.Content = content;

            // Act Update user
            response = await apiClient.SendAsync(requestUpdate);

            // Assert Update user
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            requestGet = new HttpRequestMessage(HttpMethod.Get, $"api/users/{id}");
            requestGet.Headers.Add("Authorization", baseToken);

            response = await apiClient.SendAsync(requestGet);

            responseJson = await response.Content.ReadAsStringAsync();
            dtoMod = JsonSerializer.Deserialize<UserDTO>(responseJson, options);
            dtoMod.Should().NotBeNull();
            dtoMod.Should().Be(dto);
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

        async Task<string> GetBearerToken(string baseAutString)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/account");
            request.Headers.Add("Authorization", baseAutString); // "Basic ZmlzdGFkbWluOmFkbWlu"


            var response = await apiClient.SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();
            Token? token = JsonSerializer.Deserialize<Token>(json);

            return $"Bearer {token?.ToString()}";
        }
    }
}