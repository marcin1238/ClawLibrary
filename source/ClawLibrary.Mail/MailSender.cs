using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace ClawLibrary.Mail
{
    /// <summary>
    /// Implementation of <see cref="IMailSender" />.
    /// </summary>
    public class MailSender : IMailSender
    {
        private readonly MailConfig _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailSender" /> class.
        /// </summary>
        /// <param name="config"></param>
        public MailSender(MailConfig config)
        {
            _config = config;
        }

        public Task SendAsync(MimeMessage message)
        {
            return Task.Factory.StartNew(async () =>
            {
                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(_config.SmtpServer, _config.SmtpPort, true);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(_config.SmtpLogin, _config.SmtpPassword);

                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
            });
        }
    }
}
