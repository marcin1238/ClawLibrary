using System;
using ClawLibrary.Core.Enums;

namespace ClawLibrary.Services.Models.Categories
{
    public class CategoryResponse
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public Status Status { get; set; }

        public override string ToString()
        {
            return $"Key: {Key}, Name: {Name}" +
                   $"CreatedDate: {CreatedDate}, ModifiedDate: {ModifiedDate}, Status: {Status}";
        }
    }
}