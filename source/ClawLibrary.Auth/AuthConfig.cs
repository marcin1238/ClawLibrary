namespace ClawLibrary.Auth
{
    public class AuthConfig
    {
        public int TokenExpirationInMinutes { get; set; } = 60 * 24;
        public int PasswordResetKeyExpirationInMinutes { get; set; } = 60 * 24;
    }
}