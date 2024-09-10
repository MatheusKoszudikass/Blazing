using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Identity.Entities
{
    public record Login
    {
        public string LoginIdentifier { get; protected set; } = string.Empty;
        public string Password { get; protected set; } = string.Empty;
        public string TwoFactorCode { get; protected set; }
        public string TwoFactorRecoveryCode { get; protected set; }
        public bool RememberMe { get; set; }

        public Login(string LoginIdentifier, string password, string twoFactorCode = "", string twoFactorRecoveryCode = "", bool rememberMe = false)
        {
            SetLoginIdentifier(LoginIdentifier);
            SetPassword(password);
            TwoFactorCode = twoFactorCode;
            TwoFactorRecoveryCode = twoFactorRecoveryCode;
            RememberMe = rememberMe;
        }

        public void SetLoginIdentifier(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty", nameof(email));
            }
            LoginIdentifier = email;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be empty", nameof(password));
            }
            Password = password;
        }
    }

}
