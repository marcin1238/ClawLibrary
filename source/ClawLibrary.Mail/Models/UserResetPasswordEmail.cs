namespace ClawLibrary.Mail.Models
{
    public class UserResetPasswordEmail
    {
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string PasswordResetKey { get; set; }
        public string ContactEmail { get; set; }
    }
}