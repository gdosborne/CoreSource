using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Common.Application.Security {
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions {
        /// <summary>Strings to compare.</summary>
        /// <param name="ss">The ss.</param>
        /// <returns>
        ///   string
        /// </returns>
        public static string StringToCompare(this SecureString ss) {
            var unmanagedString = IntPtr.Zero;
            try {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(ss);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static bool IsEqualTo(this SecureString ss1, SecureString ss2) {
            var bstr1 = Marshal.SecureStringToBSTR(ss1);
            var bstr2 = Marshal.SecureStringToBSTR(ss2);
            return IsEqual(bstr1, bstr2);
        }

        private static bool IsEqual(IntPtr bstr1, IntPtr bstr2) {
            var length1 = Marshal.ReadInt32(bstr1, -4);
            var length2 = Marshal.ReadInt32(bstr2, -4);

            if (length1 != length2) return false;

            var equal = 0;
            for (var i = 0; i < length1; i++) {
                var c1 = Marshal.ReadByte(bstr1 + i);
                var c2 = Marshal.ReadByte(bstr2 + i);
                equal |= c1 ^ c2;
            }
            return equal == 0;
        }

        public static bool IsValid(this NetworkCredential creds) =>
            creds!=null && !string.IsNullOrEmpty(creds.UserName) && !string.IsNullOrEmpty(creds.Password);

        public static bool HasValidCredentials(this CredentialManagement.Credential cred) =>
            !string.IsNullOrEmpty(cred.Username) && !string.IsNullOrEmpty(cred.Password);

        public static string ToStandardString(this SecureString value) {
            if (value == null)
                return default(String);
            IntPtr valuePtr = IntPtr.Zero;
            try {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
