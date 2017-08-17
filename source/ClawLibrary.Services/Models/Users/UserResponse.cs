using System;

namespace ClawLibrary.Services.Models.Users
{
    public class UserResponse
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }


        public override string ToString()
        {
            return $"Email: {Email}, FirstName: {FirstName}, LastName: {LastName}, PhoneNumber: {PhoneNumber}";
        }
    }
}