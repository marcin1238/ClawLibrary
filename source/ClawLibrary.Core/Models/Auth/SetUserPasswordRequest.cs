namespace ClawLibrary.Core.Models.Auth
{
    public class SetUserPasswordRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string VerificationCode { get; set; }

        public override string ToString()
        {
            return $"Email: {Email}, VerificationCode: {VerificationCode}";
        }
    }
}