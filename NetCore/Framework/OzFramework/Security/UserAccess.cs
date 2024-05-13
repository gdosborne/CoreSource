/* File="UserAccess"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.DirectoryServices.AccountManagement;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace OzFramework.Security {
    internal enum TOKEN_INFORMATION_CLASS {
        TokenUser = 1,
        TokenGroups,
        TokenPrivileges,
        TokenOwner,
        TokenPrimaryGroup,
        TokenDefaultDacl,
        TokenSource,
        TokenType,
        TokenImpersonationLevel,
        TokenStatistics,
        TokenRestrictedSids,
        TokenSessionId,
        TokenGroupsAndPrivileges,
        TokenSessionReference,
        TokenSandBoxInert,
        TokenAuditPolicy,
        TokenOrigin,
        TokenElevationType,
        TokenLinkedToken,
        TokenElevation,
        TokenHasRestrictions,
        TokenAccessInformation,
        TokenVirtualizationAllowed,
        TokenVirtualizationEnabled,
        TokenIntegrityLevel,
        TokenUIAccess,
        TokenMandatoryPolicy,
        TokenLogonSid,
        MaxTokenInfoClass
    }

    internal enum WELL_KNOWN_SID_TYPE {
        WinNullSid = 0,
        WinWorldSid = 1,
        WinLocalSid = 2,
        WinCreatorOwnerSid = 3,
        WinCreatorGroupSid = 4,
        WinCreatorOwnerServerSid = 5,
        WinCreatorGroupServerSid = 6,
        WinNtAuthoritySid = 7,
        WinDialupSid = 8,
        WinNetworkSid = 9,
        WinBatchSid = 10,
        WinInteractiveSid = 11,
        WinServiceSid = 12,
        WinAnonymousSid = 13,
        WinProxySid = 14,
        WinEnterpriseControllersSid = 15,
        WinSelfSid = 16,
        WinAuthenticatedUserSid = 17,
        WinRestrictedCodeSid = 18,
        WinTerminalServerSid = 19,
        WinRemoteLogonIdSid = 20,
        WinLogonIdsSid = 21,
        WinLocalSystemSid = 22,
        WinLocalServiceSid = 23,
        WinNetworkServiceSid = 24,
        WinBuiltinDomainSid = 25,
        WinBuiltinAdministratorsSid = 26,
        WinBuiltinUsersSid = 27,
        WinBuiltinGuestsSid = 28,
        WinBuiltinPowerUsersSid = 29,
        WinBuiltinAccountOperatorsSid = 30,
        WinBuiltinSystemOperatorsSid = 31,
        WinBuiltinPrintOperatorsSid = 32,
        WinBuiltinBackupOperatorsSid = 33,
        WinBuiltinReplicatorSid = 34,
        WinBuiltinPreWindows2000CompatibleAccessSid = 35,
        WinBuiltinRemoteDesktopUsersSid = 36,
        WinBuiltinNetworkConfigurationOperatorsSid = 37,
        WinAccountAdministratorSid = 38,
        WinAccountGuestSid = 39,
        WinAccountKrbtgtSid = 40,
        WinAccountDomainAdminsSid = 41,
        WinAccountDomainUsersSid = 42,
        WinAccountDomainGuestsSid = 43,
        WinAccountComputersSid = 44,
        WinAccountControllersSid = 45,
        WinAccountCertAdminsSid = 46,
        WinAccountSchemaAdminsSid = 47,
        WinAccountEnterpriseAdminsSid = 48,
        WinAccountPolicyAdminsSid = 49,
        WinAccountRasAndIasServersSid = 50,
        WinNTLMAuthenticationSid = 51,
        WinDigestAuthenticationSid = 52,
        WinSChannelAuthenticationSid = 53,
        WinThisOrganizationSid = 54,
        WinOtherOrganizationSid = 55,
        WinBuiltinIncomingForestTrustBuildersSid = 56,
        WinBuiltinPerfMonitoringUsersSid = 57,
        WinBuiltinPerfLoggingUsersSid = 58,
        WinBuiltinAuthorizationAccessSid = 59,
        WinBuiltinTerminalServerLicenseServersSid = 60,
        WinBuiltinDCOMUsersSid = 61,
        WinBuiltinIUsersSid = 62,
        WinIUserSid = 63,
        WinBuiltinCryptoOperatorsSid = 64,
        WinUntrustedLabelSid = 65,
        WinLowLabelSid = 66,
        WinMediumLabelSid = 67,
        WinHighLabelSid = 68,
        WinSystemLabelSid = 69,
        WinWriteRestrictedCodeSid = 70,
        WinCreatorOwnerRightsSid = 71,
        WinCacheablePrincipalsGroupSid = 72,
        WinNonCacheablePrincipalsGroupSid = 73,
        WinEnterpriseReadonlyControllersSid = 74,
        WinAccountReadonlyControllersSid = 75,
        WinBuiltinEventLogReadersGroup = 76,
        WinNewEnterpriseReadonlyControllersSid = 77,
        WinBuiltinCertSvcDComAccessGroup = 78
    }

    internal enum SECURITY_IMPERSONATION_LEVEL {
        SecurityAnonymous,
        SecurityIdentification,
        SecurityImpersonation,
        SecurityDelegation
    }

    internal enum TOKEN_ELEVATION_TYPE {
        TokenElevationTypeDefault = 1,
        TokenElevationTypeFull,
        TokenElevationTypeLimited
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SID_AND_ATTRIBUTES {
        public IntPtr Sid;
        public uint Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TOKEN_ELEVATION {
        public int TokenIsElevated;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TOKEN_MANDATORY_LABEL {
        public SID_AND_ATTRIBUTES Label;
    }

    internal class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid {
        private SafeTokenHandle() : base(true) {
        }

        internal SafeTokenHandle(IntPtr handle) : base(true) => base.SetHandle(handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool CloseHandle(IntPtr handle);

        protected override bool ReleaseHandle() => CloseHandle(base.handle);
    }

    internal class NativeMethods {
        public const uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        public const uint STANDARD_RIGHTS_READ = 0x00020000;
        public const uint TOKEN_ASSIGN_PRIMARY = 0x0001;
        public const uint TOKEN_DUPLICATE = 0x0002;
        public const uint TOKEN_IMPERSONATE = 0x0004;
        public const uint TOKEN_QUERY = 0x0008;
        public const uint TOKEN_QUERY_SOURCE = 0x0010;
        public const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        public const uint TOKEN_ADJUST_GROUPS = 0x0040;
        public const uint TOKEN_ADJUST_DEFAULT = 0x0080;
        public const uint TOKEN_ADJUST_SESSIONID = 0x0100;
        public const uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
        public const uint TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED |
            TOKEN_ASSIGN_PRIMARY | TOKEN_DUPLICATE | TOKEN_IMPERSONATE |
            TOKEN_QUERY | TOKEN_QUERY_SOURCE | TOKEN_ADJUST_PRIVILEGES |
            TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT | TOKEN_ADJUST_SESSIONID);


        public const int ERROR_INSUFFICIENT_BUFFER = 122;

        public const int SECURITY_MANDATORY_UNTRUSTED_RID = 0x00000000;
        public const int SECURITY_MANDATORY_LOW_RID = 0x00001000;
        public const int SECURITY_MANDATORY_MEDIUM_RID = 0x00002000;
        public const int SECURITY_MANDATORY_HIGH_RID = 0x00003000;
        public const int SECURITY_MANDATORY_SYSTEM_RID = 0x00004000;

        [DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr Frameworkrocess, uint desiredAccess, out SafeTokenHandle hToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool DuplicateToken(SafeTokenHandle ExistingTokenHandle, SECURITY_IMPERSONATION_LEVEL ImpersonationLevel, out SafeTokenHandle DuplicateTokenHandle);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetTokenInformation(SafeTokenHandle hToken, TOKEN_INFORMATION_CLASS tokenInfoClass, IntPtr pTokenInfo, int tokenInfoLength, out int returnLength);

        public const uint BCM_SETSHIELD = 0x160C;

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, IntPtr lParam);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetSidSubAuthority(IntPtr pSid, uint nSubAuthority);
    }

    public static class UserAccess {
        public static bool IsUserInAdminGroup() {
            var result = false;
            var hToken = default(SafeTokenHandle);
            var hTokenToCheck = default(SafeTokenHandle);
            var pElevationType = IntPtr.Zero;
            var pLinkedToken = IntPtr.Zero;
            var cbSize = 0;

            try {
                if (!NativeMethods.OpenProcessToken(System.Diagnostics.Process.GetCurrentProcess().Handle, NativeMethods.TOKEN_QUERY | NativeMethods.TOKEN_DUPLICATE, out hToken)) {
                    throw new Win32Exception();
                }

                if (Environment.OSVersion.Version.Major >= 6) {
                    cbSize = sizeof(TOKEN_ELEVATION_TYPE);
                    pElevationType = Marshal.AllocHGlobal(cbSize);
                    if (pElevationType == IntPtr.Zero) {
                        throw new Win32Exception();
                    }

                    if (!NativeMethods.GetTokenInformation(hToken, TOKEN_INFORMATION_CLASS.TokenElevationType, pElevationType, cbSize, out cbSize)) {
                        throw new Win32Exception();
                    }

                    var elevType = (TOKEN_ELEVATION_TYPE)Marshal.ReadInt32(pElevationType);

                    if (elevType == TOKEN_ELEVATION_TYPE.TokenElevationTypeLimited) {
                        cbSize = IntPtr.Size;
                        pLinkedToken = Marshal.AllocHGlobal(cbSize);
                        if (pLinkedToken == IntPtr.Zero) {
                            throw new Win32Exception();
                        }

                        if (!NativeMethods.GetTokenInformation(hToken, TOKEN_INFORMATION_CLASS.TokenLinkedToken, pLinkedToken, cbSize, out cbSize)) {
                            throw new Win32Exception();
                        }

                        var hLinkedToken = Marshal.ReadIntPtr(pLinkedToken);
                        hTokenToCheck = new SafeTokenHandle(hLinkedToken);
                    }
                }

                if (hTokenToCheck.IsNull() && !NativeMethods.DuplicateToken(hToken, SECURITY_IMPERSONATION_LEVEL.SecurityIdentification, out hTokenToCheck)) {
                    throw new Win32Exception();
                }

                var id = new WindowsIdentity(hTokenToCheck.DangerousGetHandle());
                var principal = new WindowsPrincipal(id);
                result = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            finally {
                if (!hToken.IsNull()) {
                    hToken.Close();
                    hToken = null;
                }
                if (!hTokenToCheck.IsNull()) {
                    hTokenToCheck.Close();
                    hTokenToCheck = null;
                }
                if (pElevationType != IntPtr.Zero) {
                    Marshal.FreeHGlobal(pElevationType);
                    pElevationType = IntPtr.Zero;
                }
                if (pLinkedToken != IntPtr.Zero) {
                    Marshal.FreeHGlobal(pLinkedToken);
                    pLinkedToken = IntPtr.Zero;
                }
            }

            return result;
        }

        public static bool IsRunAsAdmin() {
            var id = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static bool IsProcessElevated() {
            var fIsElevated = false;
            var hToken = default(SafeTokenHandle);
            var cbTokenElevation = 0;
            var pTokenElevation = IntPtr.Zero;

            try {
                if (!NativeMethods.OpenProcessToken(System.Diagnostics.Process.GetCurrentProcess().Handle, NativeMethods.TOKEN_QUERY, out hToken)) {
                    throw new Win32Exception();
                }

                cbTokenElevation = Marshal.SizeOf(typeof(TOKEN_ELEVATION));
                pTokenElevation = Marshal.AllocHGlobal(cbTokenElevation);
                if (pTokenElevation == IntPtr.Zero) {
                    throw new Win32Exception();
                }

                if (!NativeMethods.GetTokenInformation(hToken, TOKEN_INFORMATION_CLASS.TokenElevation, pTokenElevation, cbTokenElevation, out cbTokenElevation)) {
                    throw new Win32Exception();
                }

                var elevation = (TOKEN_ELEVATION)Marshal.PtrToStructure(pTokenElevation, typeof(TOKEN_ELEVATION));

                fIsElevated = (elevation.TokenIsElevated != 0);
            }
            finally {
                if (!hToken.IsNull()) {
                    hToken.Close();
                    hToken = null;
                }
                if (pTokenElevation != IntPtr.Zero) {
                    Marshal.FreeHGlobal(pTokenElevation);
                    pTokenElevation = IntPtr.Zero;
                    cbTokenElevation = 0;
                }
            }

            return fIsElevated;
        }

        public static int GetProcessIntegrityLevel() {
            var result = -1;
            var hToken = default(SafeTokenHandle);
            var cbTokenIL = 0;
            var pTokenIL = IntPtr.Zero;

            try {
                if (!NativeMethods.OpenProcessToken(System.Diagnostics.Process.GetCurrentProcess().Handle, NativeMethods.TOKEN_QUERY, out hToken)) {
                    throw new Win32Exception();
                }

                if (!NativeMethods.GetTokenInformation(hToken, TOKEN_INFORMATION_CLASS.TokenIntegrityLevel, IntPtr.Zero, 0, out cbTokenIL)) {
                    var error = Marshal.GetLastWin32Error();
                    if (error != NativeMethods.ERROR_INSUFFICIENT_BUFFER) {
                        throw new Win32Exception(error);
                    }
                }

                pTokenIL = Marshal.AllocHGlobal(cbTokenIL);
                if (pTokenIL == IntPtr.Zero) {
                    throw new Win32Exception();
                }

                if (!NativeMethods.GetTokenInformation(hToken, TOKEN_INFORMATION_CLASS.TokenIntegrityLevel, pTokenIL, cbTokenIL, out cbTokenIL)) {
                    throw new Win32Exception();
                }

                var tokenIL = (TOKEN_MANDATORY_LABEL)Marshal.PtrToStructure(pTokenIL, typeof(TOKEN_MANDATORY_LABEL));
                var pIL = NativeMethods.GetSidSubAuthority(tokenIL.Label.Sid, 0);
                result = Marshal.ReadInt32(pIL);
            }
            finally {
                if (!hToken.IsNull()) {
                    hToken.Close();
                    hToken = null;
                }
                if (pTokenIL != IntPtr.Zero) {
                    Marshal.FreeHGlobal(pTokenIL);
                    pTokenIL = IntPtr.Zero;
                    cbTokenIL = 0;
                }
            }

            return result;
        }

        public static bool IsUserAuthenticated(string username, string password) => IsUserAuthenticated(username, password, "idotcentral");

        public static bool IsUserAuthenticated(string username, string password, string domainname) {
            var result = false;

            using (var pc = new PrincipalContext(ContextType.Domain, "idotcentral")) {
                result = pc.ValidateCredentials(username, password);
            }

            return result;
        }
    }
}
