using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazing.Identity.Entities
{
    public record class Login
    {
        public string Email { get; protected set; } = string.Empty;
        public string Password { get; protected set; } = string.Empty;
        public string TwoFactorCode { get; protected set; }
        public string TwoFactorRecoveryCode { get; protected set; }
        public bool RememberMe { get; set; }

        public Login(string email, string password, string twoFactorCode = "", string twoFactorRecoveryCode = "", bool rememberMe = false)
        {
            SetEmail(email);
            SetPassword(password);
            TwoFactorCode = twoFactorCode;
            TwoFactorRecoveryCode = twoFactorRecoveryCode;
            RememberMe = rememberMe;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty", nameof(email));
            }
            Email = email;
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
