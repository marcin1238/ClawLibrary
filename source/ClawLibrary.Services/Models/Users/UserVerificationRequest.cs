namespace ClawLibrary.Services.Models.Users
{
    public class UserVerificationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }

        public override string ToString()
        {
            return $"Email: {Email}, VerificationCode: {VerificationCode}";
        }
    }
}