using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sistema_Login.Data.Requests
{
    public class CaptchaGoogle
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("error-codes")]
        public IEnumerable<string> Error { get; set; }

        [JsonPropertyName("challenge_ts")]
        public DateTime ChallengeTs { get; set; }

        [JsonPropertyName("hostname")]
        public string hostname { get; set; }
    }
}
