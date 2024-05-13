/* File="Identity"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="4/25/2024" */

using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using System.Threading;

namespace OzFramework.User {
    public static class Identity {
        public static (string FullName, string Email) GetIdentityInformation() {
            var result = (FullName: default(string), Email: default(string));

            var up = UserPrincipal.Current;
            result.FullName = up.DisplayName;
            result.Email = up.EmailAddress;
            return result;
        }
    }
}
