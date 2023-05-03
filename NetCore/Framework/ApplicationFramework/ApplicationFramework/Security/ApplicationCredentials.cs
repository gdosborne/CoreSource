using Common.AppFramework.Primitives;
using Common.AppFramework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.AppFramework.Security {
    public class ApplicationCredentials : NetworkCredential {
        public ApplicationCredentials()
            : base() { }
        public ApplicationCredentials(bool areNewCredentials)
            : base() {
            AreNewCredentials = areNewCredentials;
        }
        public ApplicationCredentials(string? username, string? password, bool areNewCredentials)
           : base(username, password) {
            AreNewCredentials = areNewCredentials;
        }
        public ApplicationCredentials(string? username, SecureString? password, bool areNewCredentials)
           : base(username, password) {
            AreNewCredentials = areNewCredentials;
        }
        public ApplicationCredentials(string? username, string? password, string? domain, bool areNewCredentials)
           : base(username, password, domain) {
            AreNewCredentials = areNewCredentials;
        }
        public ApplicationCredentials(string? username, SecureString? password, string? domain, bool areNewCredentials)
           : base(username, password, domain) {
            AreNewCredentials = areNewCredentials;
        }

        public bool AreNewCredentials { get; private set; }

        public bool HasValidCredentials() =>
            !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);

        public override bool Equals(object? obj) => obj != null && (
                UserName == obj.As<ApplicationCredentials>().UserName
                && SecurePassword.IsEqualTo(obj.As<ApplicationCredentials>().SecurePassword));

        public override int GetHashCode() =>
            this.GetHashCode();
    }
}
