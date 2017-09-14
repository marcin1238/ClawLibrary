using System;
using ClawLibrary.Core.Enums;

namespace ClawLibrary.Services.Models.Authors
{
    public class AuthorResponse
    {
        public string Key { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public Status Status { get; set; }

        public override string ToString()
        {
            return $"Key: {Key}, FirstName: {FirstName}, LastName: {LastName}" +
                   $"Description: {Description}, CreatedDate: {CreatedDate}, ModifiedDate: {ModifiedDate}, Status: {Status}";
        }
    }
}