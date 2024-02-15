using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text;
using System.Text.Json;
using TaskManager.Api.Data;
using TaskManager.Api.Data.Models;
using TaskManager.Api.Tests.Models;
using TaskManager.Common.Models;
using Xunit;

namespace TaskManager.Api.Tests.Abstractions
{
    /*
     * получаем апи приложение подменяя контекст базы данных на InMemory
     * добовляем пользователя со статусом Admin
     */
    public abstract class IntegrationTestsBase : IClassFixture<WebApplicationFactory<Program>>
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ApplicationContext DB { get; }

        /// <summary>
        /// единный контекст для всех тестов
        /// </summary>
        public HttpClient ApiClient { get; }

        /// <summary>
        /// строка бозовой авторизации админа
        /// </summary>
        public string AdminAuth { get; }

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
            DB = builder.Services.CreateScope().ServiceProvider.GetService<ApplicationContext>()!;

            List<User> users =
                [
                    new()
                    {
                        Email = "fistadmin",
                        Password = "admin",
                        Status = UserStatus.Admin,
                    },
                ];

            DB.Users.AddRange(users);
            DB.SaveChanges();
            #endregion

            AdminAuth = GetAuth(UserStatus.Admin, DB);

            ApiClient = builder.CreateClient();
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

            return await ApiClient.SendAsync(request);
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
