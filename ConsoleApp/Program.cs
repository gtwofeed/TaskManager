
using System.Text.Json;
using TaskManager.Common.Models;
using System.Text;
using Task = System.Threading.Tasks.Task;
using TaskManager.Api.Data;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Temp();
            
        }
        static async Task Temp()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/api/account/token");
            request.Headers.Add("Authorization", "Basic ZmlzdGFkbWluOmFkbWlu");
            var content = new StringContent("", null, "text/plain");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var token = JsonSerializer.Deserialize<Token>(await response.Content.ReadAsStringAsync());
            await Console.Out.WriteLineAsync(token.ToString());
            request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5000/api/users/all");
            request.Headers.Add("Authorization", $"Bearer {token}");
            response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var users = JsonSerializer.Deserialize<List<UserDTO>>(await response.Content.ReadAsStringAsync());
            ;
        }
        static async Task Temp1()
        {
            string uAuth =
            Convert.ToBase64String(
            Encoding.UTF8.GetBytes(":")
            );
            Console.WriteLine(uAuth);
        }
        static void Temp2()
        {

        }
        static string GetAuth(UserStatus status, ApplicationContext? context = null)
        {
            string username = "";
            string password = "";
            if (context is null) return $"Basic {Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{username}:{password}"))}";

            var user = context.Users.FirstOrDefault(u => u.Status == status) ?? context.Users.FirstOrDefault();

            return $"Basic {Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{username}:{password}"))}";
        }
    }
}
