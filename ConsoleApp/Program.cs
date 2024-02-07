
using System.Text.Json;
using System;
using System.Net;
using TaskManager.Common.Models;
using System.Text;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {


            await Temp1();



        }
        static async Task Temp()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:49914/api/account/token");
            request.Headers.Add("Authorization", "Basic ZmlzdGFkbWluOmFkbWlu");
            var content = new StringContent("", null, "text/plain");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var token = JsonSerializer.Deserialize<Token>(await response.Content.ReadAsStringAsync());

            request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:49914/api/users/all");
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
    }
}
