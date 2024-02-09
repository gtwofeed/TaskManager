using FluentAssertions;
using System.Net;
using TaskManager.Common.Models;
using Xunit;


namespace TaskManager.Api.Tests
{
    public class UsersControllerIntegration : CommonContext
    {

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
        public async Task GetUsers_SendRequest_ShouldCount1()
        {
            // Arrange


            // Act

            // Assert
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