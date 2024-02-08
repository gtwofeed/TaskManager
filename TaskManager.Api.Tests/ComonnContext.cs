using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Api.Models;
using TaskManager.Common.Models;

namespace TaskManager.Api.Tests
{
    /*
     * получаем апи приложение подменяя контекст базы данных на InMemory
     * добовляем пользователя со статусом Admin
     * добовляем пользователя со статусом Editor
     * добовляем пользователя со статусом User
     */
    public abstract class ComonnContext
    {
        public readonly HttpClient apiClient; // единный контекст для всех тестов
        public readonly string adminAuth; // строка бозовой авторизации админа
        public readonly string editorAuth; // строка бозовой авторизации редактора
        public readonly string userAuth; // строка бозовой авторизации пользователя
        public readonly string incorrectAuth; // строка бозовой авторизации не существуещего пользователя

        public ComonnContext()
        {
            // заменяем провайдера UseInMemoryDatabase
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

            // заполняем Database тестовыми данными
            ApplicationContext db = webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationContext>()!;

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

            db.AddRange(users);
            db.SaveChanges();

            adminAuth = GetAuth(UserStatus.Admin, db);
            editorAuth = GetAuth(UserStatus.Editor, db);
            userAuth = GetAuth(UserStatus.User, db);
            incorrectAuth = GetAuth(UserStatus.User);

            apiClient = webHost.CreateClient();
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
