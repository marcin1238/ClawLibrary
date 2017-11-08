using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClawLibrary.Core.Models.Auth
{
    public class AuthConfig
    {
        public int TokenExpirationInMinutes { get; set; } = 60 * 24;
        public int PasswordResetKeyExpirationInMinutes { get; set; } = 60 * 24;
    }
}
