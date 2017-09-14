namespace ClawLibrary.Services.Models.Authors
{
    public class AuthorRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}, Description: {Description}";
        }
    }
}