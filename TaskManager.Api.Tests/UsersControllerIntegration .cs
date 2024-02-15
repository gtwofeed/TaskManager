using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using TaskManager.Api.Data.Models;
using TaskManager.Api.Tests.Abstractions;
using TaskManager.Common.Models;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Tests
{
    public class UsersControllerIntegrationTests : IntegrationTestsBase
    {
        public UsersControllerIntegrationTests(WebApplicationFactory<Program> fixture) : base(fixture)
        {
            #region adding test data to the Database
            List<User> users =
                [
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
                    },
                ];

            DB.Users.AddRange(users);
            DB.SaveChanges();
            #endregion
        }

        [Fact]
        public async Task Check_SendRequest_ShouldOk()
        {
            // Arrange in CommonContext

            // Act
            var response = await ApiClient.GetAsync("api/users/check");

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

            StringContent content = new (JsonSerializer.Serialize(dto), null, "application/json");

            // Act
            var response = await SendRequestAsync(HttpMethod.Post, "api/users", await GetBearerToken(AdminAuth), content);

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

            // Act
            var response = await SendRequestAsync(HttpMethod.Get, $"api/users/{id}", await GetBearerToken(AdminAuth));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string responseJson = await response.Content.ReadAsStringAsync();
            UserDTO? user = JsonSerializer.Deserialize<UserDTO>(responseJson, JsonSerializerOptions);

            user?.Id.Should().Be(id);
        }

        [Fact]
        public async Task Update_SendRequest_ShouldEqueUsersDTO()
        {
            // Arrange
            int id = 2;
            UserDTO? dto, dtoMod;
            string bearerToken = await GetBearerToken(AdminAuth);

            // Act Get user
            var response = await SendRequestAsync(HttpMethod.Get, $"api/users/{id}", bearerToken);

            // Assert Get user
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string responseJson = await response.Content.ReadAsStringAsync();
            dto = JsonSerializer.Deserialize<UserDTO>(responseJson, JsonSerializerOptions);
            dto.Should().NotBeNull();
            dto!.Id.Should().Be(id);

            // Arrange Update user
            Random random = new Random();
            dto.Password += random.Next(10).ToString();

            StringContent content = new(JsonSerializer.Serialize(dto), null, "application/json");

            // Act Update user
            response = await SendRequestAsync(HttpMethod.Patch, $"api/users/{id}", bearerToken, content);

            // Assert Update user
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            response = await SendRequestAsync(HttpMethod.Get, $"api/users/{id}", bearerToken);

            responseJson = await response.Content.ReadAsStringAsync();
            dtoMod = JsonSerializer.Deserialize<UserDTO>(responseJson, JsonSerializerOptions);
            dtoMod.Should().NotBeNull();
            dtoMod.Should().Be(dto);
        }

        [Fact]
        public async Task Delete_SendRequest_ShouldOk()
        {
            // Arrange
            int id = 2;
            string baseToken = await GetBearerToken(AdminAuth);
            UserDTO? dto;

            // Act Get user
            var response = await SendRequestAsync(HttpMethod.Get, $"api/users/{id}", baseToken);

            // Assert Get user
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string responseJson = await response.Content.ReadAsStringAsync();
            dto = JsonSerializer.Deserialize<UserDTO>(responseJson, JsonSerializerOptions);
            dto.Should().NotBeNull();
            dto!.Id.Should().Be(id);

            // Act Delete user
            response = await SendRequestAsync(HttpMethod.Delete, $"api/users/{id}", baseToken);

            // Assert Delete user
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            response = await SendRequestAsync(HttpMethod.Get, $"api/users/{id}", baseToken);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task GetUsers_SendRequest_ShouldCount3()
        {
            // Arrange and Act
            var response = await SendRequestAsync(HttpMethod.Get, $"api/users", await GetBearerToken(AdminAuth));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseJson = await response.Content.ReadAsStreamAsync();
            var users = await JsonSerializer.DeserializeAsync<IEnumerable<UserDTO>>(responseJson, JsonSerializerOptions);

            users?.Count().Should().Be(3);
        } 
        
    }
}