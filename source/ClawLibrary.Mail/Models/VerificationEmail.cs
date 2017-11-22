﻿namespace ClawLibrary.Mail.Models
{
    public class VerificationEmail
    {
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string CustomerKey { get; set; }
        public string ContactEmail { get; set; }
    }
}