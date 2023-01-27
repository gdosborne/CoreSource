using System.Security;

namespace Common.Applicationn.Security {
    /// <summary>
    /// The Vault Credentials
    /// </summary>
    public class VaultCredential {
        /// <summary>
        /// Initializes a new instance of the <see cref="VaultCredential"/> class.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public VaultCredential(string applicationId, string username, SecureString password) {
            ApplicationID = applicationId;
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; private set; } = null;
        /// <summary>
        /// Gets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public string ApplicationID { get; private set; }
        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public SecureString Password { get; private set; }
    }
}
