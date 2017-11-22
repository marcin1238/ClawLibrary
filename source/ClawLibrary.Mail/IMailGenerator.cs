using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClawLibrary.Mail.Models;
using MimeKit;

namespace ClawLibrary.Mail
{
    public interface IMailGenerator
    {
        MimeMessage PrepareEmailFromTemplate(Email email);
        MimeMessage PrepareEmail(UserResetPasswordEmail emailDetails);
        MimeMessage PrepareEmail(VerificationEmail emailDetails);
    }
}
