using Microsoft.AspNetCore.Mvc.Testing;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string api = "api/users/check-status"; //"api/users/all"
            WebApplicationFactory<Program> application = new();
            HttpClient client = application.CreateClient();

            var response = await client.GetAsync(api);
            /*response.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZmlzdGFkbWluIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJuYmYiOjE3MDcyOTQwNzcsImV4cCI6MTcwNzI5NDEzNywiaXNzIjoiTXlBdXRoU2VydmVyIiwiYXVkIjoiTXlBdXRoQ2xpZW50In0.c4lAwo5qeRh8PCii4BVNLVsw6BApbKoRUXbBiERmvnA");*/
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
