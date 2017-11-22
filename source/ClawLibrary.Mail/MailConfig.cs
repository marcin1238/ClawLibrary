namespace ClawLibrary.Mail
{
    public class MailConfig
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpLogin { get; set; }
        public string SmtpPassword { get; set; }

        public string BaseUrl { get; set; }
        public string BannerFileId { get; set; }
        public string FacebookLogoFileId { get; set; }
        public string TwitterLogoFileId { get; set; }

        public string AccountVerificationTemplate { get; set; }
        public string AccountVerificationEmailSubject { get; set; }
        

        public string PasswordResetTemplate { get; set; }
        public string PasswordResetEmailSubject { get; set; }

        public string OrganizationVerificationTemplate { get; set; }
        public string OrganizationVerificationEmailSubject { get; set; }
        

    }
}