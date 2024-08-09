/* File="ApplicationCredentials"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Common.Primitives;
using System.Net;
using System.Security;

namespace Common.Security {
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
            !UserName.IsNull() && !Password.IsNull();

        public override bool Equals(object? obj) => !obj.IsNull() && (
                UserName == obj.As<ApplicationCredentials>().UserName
                && SecurePassword.IsEqualTo(obj.As<ApplicationCredentials>().SecurePassword));

        public override int GetHashCode() =>
            this.GetHashCode();
    }
}
