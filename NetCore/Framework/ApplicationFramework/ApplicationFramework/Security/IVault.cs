using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Common.Application.Security.PasswordVault;

namespace Common.Application.Security {
    /// <summary>
    ///   <para>The vault interface</para>
    /// </summary>
    public interface IVault {
        /// <summary>Stores the credentials.</summary>
        /// <param name="credential">The credential.</param>
        void StoreCredentials(VaultCredential credential);
        /// <summary>Validates the credential.</summary>
        /// <param name="credential">The credential.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        CredentialStatusValues ValidateCredential(VaultCredential credential);

        /// <summary>Purges the specified application.</summary>
        /// <param name="application">The application.</param>
        void Purge(string application);
    }
}
