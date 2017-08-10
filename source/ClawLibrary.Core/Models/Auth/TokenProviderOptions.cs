using System;
using Microsoft.IdentityModel.Tokens;

namespace ClawLibrary.Core.Models.Auth
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/api/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(60);

        public SigningCredentials SigningCredentials { get; set; }
    }
}