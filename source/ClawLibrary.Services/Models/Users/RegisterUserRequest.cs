namespace ClawLibrary.Services.Models.Users
{
    public class RegisterUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Language { get; set; }

        public override string ToString()
        {
            return $"Email: {Email}, PhoneNumber: {PhoneNumber}, FirstName: {FirstName}, LastName: {LastName}, Language: {Language}";
        }
    }
}