using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Common.Models;
using Xunit;

namespace TaskManager.Api.Tests
{
    public class AccountControllerIntegration : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly HttpClient client; // единный контекст для всех тестов
        readonly string adminAuth; // строка бозовой авторизации админа


        public AccountControllerIntegration(WebApplicationFactory<Program> application)
        {
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
    }
}
