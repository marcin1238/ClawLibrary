namespace ClawLibrary.Core.Models.Auth
{
    public class ResetUserPasswordRequest
    {
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Email: {Email}";
        }
    }
}