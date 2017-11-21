namespace ClawLibrary.Services.Models.Users
{
    public class AuthorizeRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"Email: {Email}";
        }
    }
}
