namespace OSCrypto {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class Crypto
	{
		public Crypto(string password)
			: this(Encoding.ASCII.GetBytes(password)) {
		}
		public Crypto(byte[] salt) {
			_salt = ValidateSalt(salt);
		}

        public string Decrypt<T>(T data) {
			if (data == null)
				throw new ArgumentNullException("data");
			if (string.IsNullOrEmpty(_sharedSecret))
				throw new ArgumentNullException("sharedSecret");
			if (data.GetType() != typeof(string) && data.GetType() != typeof(byte[]))
				throw new ArgumentException("Invalid data type - must be string or byte[]", "data");

			var aesAlg = default(RijndaelManaged);

			try {
				var key = new Rfc2898DeriveBytes(_sharedSecret, _salt);
				byte[] bytes = null;
				if (typeof(T) == typeof(string))
					bytes = Convert.FromBase64String((string)(object)data);
				else if (typeof(T) == typeof(byte[]))
					bytes = (byte[])(object)data;
				using (var msDecrypt = new MemoryStream(bytes)) {
					aesAlg = new RijndaelManaged();
					aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
					aesAlg.IV = ReadByteArray(msDecrypt);
					var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
					using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					using (var srDecrypt = new StreamReader(csDecrypt)) {
						return srDecrypt.ReadToEnd();
					}
				}
			}
			finally {
                if (aesAlg != null) {
                    aesAlg.Clear();
                    aesAlg.Dispose();
                }
			}
		}
		public T Encrypt<T>(string data) {
			if (data == null)
				throw new ArgumentNullException("plainText");
			if (string.IsNullOrEmpty(_sharedSecret))
				throw new ArgumentNullException("sharedSecret");
			if (data.GetType() != typeof(string) && data.GetType() != typeof(byte[]))
				throw new ArgumentException("Invalid data type - must be string or byte[]", "data");

			var aesAlg = default(RijndaelManaged);
			try {
				var key = new Rfc2898DeriveBytes(_sharedSecret, _salt);
				aesAlg = new RijndaelManaged();
				aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
				var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
				using (var msEncrypt = new MemoryStream()) {
					msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
					msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
					using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					using (var swEncrypt = new StreamWriter(csEncrypt)) {
						swEncrypt.Write(data);
					}
					if (typeof(T) == typeof(string))
						return (T)(object)Convert.ToBase64String(msEncrypt.ToArray());
					else if (typeof(T) == typeof(byte[]))
						return (T)(object)msEncrypt.ToArray();
				}
			}
			finally {
                if (aesAlg != null) {
                    aesAlg.Clear();
                    aesAlg.Dispose();
                }
			}
			return (T)(object)null;
		}

        private byte[] ValidateSalt(byte[] data) {
			if (data.Length < 8) {
				var tmpSalt = new List<byte>(data);
				while (tmpSalt.Count < 9) {
					tmpSalt.Add(Convert.ToByte('@'));
				}
				data = tmpSalt.ToArray();
			}
			return data;
		}

        private byte[] ReadByteArray(Stream s) {
			var rawLength = new byte[sizeof(int)];
			if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length) {
				throw new SystemException("Stream did not contain properly formatted byte array");
			}

			var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
			if (s.Read(buffer, 0, buffer.Length) != buffer.Length) {
				throw new SystemException("Did not read byte array properly");
			}

			return buffer;
		}

        private byte[] _salt = null;
		private string _sharedSecret = "jsdhbdcjsbcv";
	}
}
