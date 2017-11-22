using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;

namespace ClawLibrary.Mail
{
    /// <summary>
    /// Mail sender
    /// </summary>
    public interface IMailSender
    {
        /// <summary>
        /// Sends MimeMessage via SMTP.
        /// </summary>
        /// <param name="message">Email message</param>
        Task SendAsync(MimeMessage message);
    }
}
