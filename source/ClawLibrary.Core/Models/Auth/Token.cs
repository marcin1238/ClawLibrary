using System;
using Newtonsoft.Json;

namespace ClawLibrary.Core.Models.Auth
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("expires_at")]
        public DateTimeOffset ExpiresAt { get; set; }

        public override string ToString()
        {
            return $"AccessToken: {AccessToken}, TokenType: {TokenType}, ExpiresIn {ExpiresIn}, ExpiresAt {ExpiresAt}";
        }
    }

}