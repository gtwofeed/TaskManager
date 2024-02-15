using System.Text.Json.Serialization;

namespace TaskManager.Api.Tests
{
    public class Token
    {
        [JsonPropertyName("access_token")]
        public string? AccessTokenen { get; set; }
        public string? UserName { get; set; }

        public override string ToString() =>
            AccessTokenen ?? string.Empty;
    }
}
