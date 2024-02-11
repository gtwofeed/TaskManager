using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManager.Api.Data;
using TaskManager.Api.Data.Models;
using TaskManager.Common.Models;
using Xunit;

namespace TaskManager.Api.Tests
{
    /*
     * получаем апи приложение подменяя контекст базы данных на InMemory
     * добовляем пользователя со статусом Admin
     * добовляем пользователя со статусом Editor
     * добовляем пользователя со статусом User
     */
    public abstract class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        public readonly JsonSerializerOptions options = new ()
        {
            PropertyNameCaseInsensitive = true
        };

        public readonly HttpClient apiClient; // единный контекст для всех тестов
        public readonly string adminAuth; // строка бозовой авторизации админа
        public readonly string editorAuth; // строка бозовой авторизации редактора
        public readonly string userAuth; // строка бозовой авторизации пользователя
        public readonly string incorrectAuth; // строка бозовой авторизации не существуещего пользователя

        public IntegrationTest(WebApplicationFactory<Program> fixture)
        {
            // заменяем провайдера UseInMemoryDatabase
            var builder = fixture.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    Guid idDB = Guid.NewGuid();
                    services.RemoveAll(typeof(DbContextOptions<ApplicationContext>));
                    services.AddDbContext<ApplicationContext>(option =>
                    {
                        option.UseInMemoryDatabase(idDB.ToString());
                    });
                });
            });

            #region заполняем Database тестовыми данными
            ApplicationContext db = builder.Services.CreateScope().ServiceProvider.GetService<ApplicationContext>()!;

            List<User> users = [
                new()
                {
                    Email = "fistadmin",
                    Password = "admin",
                    Status = UserStatus.Admin,
                },
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

            db.Users.AddRange(users);
            db.SaveChanges();
            #endregion

            adminAuth = GetAuth(UserStatus.Admin, db);
            editorAuth = GetAuth(UserStatus.Editor, db);
            userAuth = GetAuth(UserStatus.User, db);
            incorrectAuth = GetAuth(UserStatus.User);

            apiClient = builder.CreateClient();
        }

        public async Task<string> GetBearerToken(string baseAutString)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/account");
            request.Headers.Add("Authorization", baseAutString); // "Basic ZmlzdGFkbWluOmFkbWlu"

            var response = await apiClient.SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();
            Token? token = JsonSerializer.Deserialize<Token>(json);

            return $"Bearer {token?.ToString()}";
        }

        /// <summary>
        /// получение строки базовай авторизации для пользователя определённого статуса 
        /// из предоставленного кондекста базы данных
        /// </summary>
        /// <param name="context"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        string GetAuth(UserStatus status, ApplicationContext? context = null)
        {
            string username = "";
            string password = "";

            if (context != null)
            {
                var user = context.Users.FirstOrDefault(u => u.Status == status);
                if (user != null)
                {
                    username = user.Email;
                    password = user.Password;
                }
            }

            return $"Basic {Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{username}:{password}"))}";
        }
    }
}
