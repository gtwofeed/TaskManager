using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text;
using System.Text.Json;
using TaskManager.Api.Data;
using TaskManager.Api.Data.Models;
using TaskManager.Common.Models;
using Xunit;

namespace TaskManager.Api.Tests
{
    /*
     * получаем апи приложение подменяя контекст базы данных на InMemory
     * добовляем пользователя со статусом Admin
     */
    public abstract class IntegrationTestsBase : IClassFixture<WebApplicationFactory<Program>>
    {
        public readonly JsonSerializerOptions options = new ()
        {
            PropertyNameCaseInsensitive = true
        };

        public readonly ApplicationContext db;
        public readonly HttpClient apiClient; // единный контекст для всех тестов
        public readonly string adminAuth; // строка бозовой авторизации админа

        public IntegrationTestsBase(WebApplicationFactory<Program> fixture)
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
            db = builder.Services.CreateScope().ServiceProvider.GetService<ApplicationContext>()!;

            List<User> users = 
                [
                    new()
                    {
                        Email = "fistadmin",
                        Password = "admin",
                        Status = UserStatus.Admin,
                    },
                ];

            db.Users.AddRange(users);
            db.SaveChanges();
            #endregion

            adminAuth = GetAuth(UserStatus.Admin, db);

            apiClient = builder.CreateClient();
        }

        public async Task<HttpResponseMessage> SendRequestAsync(
            HttpMethod metod,
            string url,
            string autString,
            StringContent? content = null)
        {
            var request = new HttpRequestMessage(metod, url);
            request.Headers.Add("Authorization", autString);
            request.Content = content;

            return await apiClient.SendAsync(request);
        }

        public async Task<string> GetBearerToken(string baseAutString)
        {
            var response = await SendRequestAsync(HttpMethod.Post, "api/account", baseAutString);

            var json = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<Token>(json);
            
            return $"Bearer {token?.ToString()}";
        }

        /// <summary>
        /// получение строки базовай авторизации для пользователя определённого статуса 
        /// из предоставленного кондекста базы данных
        /// </summary>
        /// <param name="context"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetAuth(UserStatus status, ApplicationContext? context = null)
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
