namespace ClawLibrary.Services.Models.Users
{
    public class UserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}";
        }
    }
}