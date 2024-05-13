/* File="Crypto"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OzFramework {
    public sealed class Crypto : IDisposable {
        #region Public Constructors
        public Crypto(string password)
            : this(Encoding.ASCII.GetBytes(password)) {
        }
        public Crypto(byte[] salt) {
            _salt = ValidateSalt(salt);
        }
        #endregion Public Constructors

        #region Public Methods
        public string Decrypt<T>(T data) {
            if (data.IsNull())
                throw new ArgumentNullException("data");
            if (sharedSecret.IsNull())
                throw new ArgumentNullException("sharedSecret");
            if (data.GetType() != typeof(string) && data.GetType() != typeof(byte[]))
                throw new ArgumentException("Invalid data type - must be string or byte[]", "data");

            Aes aesAlg = null;

            try {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);
                byte[] bytes = null;
                if (typeof(T) == typeof(string))
                    bytes = Convert.FromBase64String((string)(object)data);
                else if (typeof(T) == typeof(byte[]))
                    bytes = (byte[])(object)data;
                using (MemoryStream msDecrypt = new MemoryStream(bytes)) {
                    aesAlg = Aes.Create();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                    using var srDecrypt = new StreamReader(csDecrypt);
                    return srDecrypt.ReadToEnd();
                }
            } finally {
                if (!aesAlg.IsNull())
                    aesAlg.Clear();
            }
        }
        public T Encrypt<T>(string data) {
            if (data.IsNull())
                throw new ArgumentNullException("plainText");
            if (sharedSecret.IsNull())
                throw new ArgumentNullException("sharedSecret");
            if (data.GetType() != typeof(string) && data.GetType() != typeof(byte[]))
                throw new ArgumentException("Invalid data type - must be string or byte[]", "data");

            Aes aesAlg = null;
            try {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);
                aesAlg = Aes.Create();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using MemoryStream msEncrypt = new MemoryStream();
                msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    swEncrypt.Write(data);

                if (typeof(T) == typeof(string))
                    return (T)(object)Convert.ToBase64String(msEncrypt.ToArray());
                else if (typeof(T) == typeof(byte[]))
                    return (T)(object)msEncrypt.ToArray();
            } finally {
                if (!aesAlg.IsNull())
                    aesAlg.Clear();
            }
            return (T)(object)null;
        }
        #endregion Public Methods

        #region Private Methods
        private byte[] ValidateSalt(byte[] data) {
            if (data.Length < 8) {
                var tmpSalt = new List<byte>(data);
                while (tmpSalt.Count < 9) {
                    var val = rnd.Next(0, 255).CastTo<byte>();
                    tmpSalt.Add(Convert.ToByte(val));
                }
                data = tmpSalt.ToArray();
            }
            return data;
        }

        private byte[] ReadByteArray(Stream s) {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length) {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length) {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
        #endregion Private Methods

        #region Private Fields
        private static System.Random rnd = new System.Random();
        private byte[]? _salt = null;
        private string? sharedSecret = "09lakshjfd";
        private bool isDisposed;

        private void Dispose(bool isDisposing) {
            if (!isDisposed) {
                if (isDisposing) {
                    _salt = null;
                    sharedSecret = null;
                }
                isDisposed = true;
                GC.Collect();
            }
        }
        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(isDisposing: true);
            //GC.SuppressFinalize(this);
        }
        #endregion Private Fields
    }
}
