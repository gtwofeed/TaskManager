using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
