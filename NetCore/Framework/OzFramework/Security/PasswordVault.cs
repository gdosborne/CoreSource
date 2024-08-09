/* File="PasswordVault"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Common.Xml.Linq;
using Common;
using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Xml.Linq;
using Common.Primitives;
using Common.Text;

namespace Common.Security {
    /// <summary>
    /// PasswordVault
    /// </summary>
    public sealed class PasswordVault : IVault {
        /// <summary>
        /// CredentialStatusValues
        /// </summary>
        public enum CredentialStatusValues {
            /// <summary>
            /// Credentials have not been set
            /// </summary>
            NotSet,
            /// <summary>
            /// Username/Password combination is valid
            /// </summary>
            Valid,
            /// <summary>
            /// Username/Password combination is invalid
            /// </summary>
            Invalid
        }

        private Guid vaultID = new("{D88CFB65-104C-4457-904E-F498A989F7C0}");
        /// <summary>
        /// PasswordVault constructor
        /// </summary>
        /// <param name="vaultLocation">Location (folder) of the vault</param>
        /// <param name="vaultPassword">Password to encrypt the vault</param>
        public PasswordVault(string vaultLocation, SecureString vaultPassword) {
            var g = vaultID.ToString()
                .Replace("{", string.Empty)
                .Replace("}", string.Empty)
                .Replace("-", string.Empty);
            var salt = new byte[32];
            for (int i = 0; i < g.ToCharArray().Length; i++) {
                salt[i] = Convert.ToByte(g.ToCharArray()[i]);
            }
            vaultSalt = salt;
            var dir = Path.Combine(vaultLocation, vaultID.ToString());
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }
            this.vaultPassword = vaultPassword;
            vaultFileName = Path.Combine(dir, "credentials.vault");
        }

        private readonly SecureString vaultPassword = default;
        private readonly string vaultFileName = default;
        private readonly byte[] vaultSalt = default;

        private XDocument GetDocument(Crypto crypto) {
            using var readerStream = new FileStream(vaultFileName, FileMode.Open, FileAccess.Read, FileShare.None);
            using var sr = new StreamReader(readerStream);
            var fileData = crypto.Decrypt(sr.ReadToEnd());
            return XDocument.Parse(fileData);
        }

        private void SaveDocument(Crypto crypto, XDocument doc) {
            var encrypted = crypto.Encrypt<string>(doc.ToString());
            using var finalStream = new FileStream(vaultFileName, FileMode.Create, FileAccess.Write, FileShare.None);
            using var writer = new StreamWriter(finalStream);
            writer.Write(encrypted);
        }

        /// <summary>
        /// Validates the supplied crtedentials to stored credentials for the application
        /// </summary>
        /// <param name="credential">The Username/Password combination</param>
        /// <returns></returns>
        public CredentialStatusValues ValidateCredential(VaultCredential credential) {
            var result = CredentialStatusValues.NotSet;
            var crypto = new Crypto(vaultSalt);
            if (!File.Exists(vaultFileName)) {
                return result;
            }
            var doc = GetDocument(crypto);
            var root = doc.Root;
            var appElement = root.Elements().FirstOrDefault(x =>
                x.LocalName().Equals("ApplicationCredentials") &&
                !x.Attribute("name").IsNull() &&
                x.Attribute("name").Value.Equals(credential.ApplicationID));
            if (!appElement.IsNull()) {
                var uName = appElement.Attribute("username").Value;
                var pWord = appElement.Attribute("password").Value;
                result = CredentialStatusValues.Invalid;
                if (uName.EqualsIgnoreCase(credential.Username)
                    && pWord.Equals(credential.Password.StringToCompare())) {
                    result = CredentialStatusValues.Valid;
                }
            }
            else {
                result = CredentialStatusValues.NotSet;
            }
            return result;
        }

        /// <summary>
        /// Stores credentials in the vault
        /// </summary>
        /// <param name="credential">The username/password combination</param>
        public void StoreCredentials(VaultCredential credential) {
            var crypto = new Crypto(vaultSalt);
            if (!File.Exists(vaultFileName)) {
                var xDoc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "true"),
                    new XElement("vault"));
                SaveDocument(crypto, xDoc);
            }
            var doc = GetDocument(crypto);
            var root = doc.Root;
            var appElement = root.Elements().FirstOrDefault(x =>
                x.LocalName().Equals("ApplicationCredentials") &&
                !x.Attribute("name").IsNull() &&
                x.Attribute("name").Value.Equals(credential.ApplicationID));
            if (!appElement.IsNull()) {
                appElement.Attribute("username").Value = credential.Username;
                appElement.Attribute("password").Value = credential.Password.StringToCompare();
            }
            else {
                appElement = new XElement("ApplicationCredentials",
                    new XAttribute("name", credential.ApplicationID),
                    new XAttribute("username", credential.Username),
                    new XAttribute("password", credential.Password.StringToCompare()));
                root.Add(appElement);
            }
            SaveDocument(crypto, doc);
        }

        /// <summary>Purges the specified application.</summary>
        /// <param name="application">The application.</param>
        public void Purge(string application) {
            var crypto = new Crypto(vaultSalt);
            if (!File.Exists(vaultFileName)) {
                var xDoc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "true"),
                    new XElement("vault"));
                SaveDocument(crypto, xDoc);
            }
            var doc = GetDocument(crypto);
            var root = doc.Root;
            var appElement = root.Elements().FirstOrDefault(x =>
                x.LocalName().Equals("ApplicationCredentials") &&
                !x.Attribute("name").IsNull() &&
                x.Attribute("name").Value.Equals(application));
            if (!appElement.IsNull()) {
                appElement.Remove();
            }
            SaveDocument(crypto, doc);
        }
    }
}
