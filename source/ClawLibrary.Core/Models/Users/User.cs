﻿using System;
using ClawLibrary.Core.Enums;

namespace ClawLibrary.Core.Models.Users
{
    public class User
    {
        public long Id { get; set; }
        public string Key { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Language { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }

        public string PasswordResetKey { get; set; }

        public DateTimeOffset? PasswordResetKeyCreatedDate { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return $"Key: {Key}, Email: {Email}, FirstName: {FirstName}, LastName: {LastName}," +
                   $" Status: {Status}, CreatedDate: {CreatedDate}, ModifiedDate: {ModifiedDate}";
        }
    }
}