namespace AppSystem.Security {
	using System;
	using Windows.Security.Cryptography;
	using Windows.Security.Cryptography.Core;

	public sealed class Crypto {
		public string Encrypt(string input, string pass) {
			var sap = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesEcbPkcs7);
			var aes = default(CryptographicKey);
			var hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
			var aesHash = hap.CreateHash();

			var encrypted = "";
			try {
				var hash = new byte[32];
				aesHash.Append(CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(pass)));
				CryptographicBuffer.CopyToByteArray(aesHash.GetValueAndReset(), out var temp);

				Array.Copy(temp, 0, hash, 0, 16);
				Array.Copy(temp, 0, hash, 15, 16);

				aes = sap.CreateSymmetricKey(CryptographicBuffer.CreateFromByteArray(hash));

				var buffer = CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(input));
				encrypted = CryptographicBuffer.EncodeToBase64String(CryptographicEngine.Encrypt(aes, buffer, null));

				return encrypted;
			}
			catch (Exception) {
				return null;
			}
		}


		public string Decrypt(string input, string pass) {
			var sap = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesEcbPkcs7);
			var aes = default(CryptographicKey);
			var hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
			var aesHash = hap.CreateHash();

			var decrypted = "";
			try {
				var hash = new byte[32];
				aesHash.Append(CryptographicBuffer.CreateFromByteArray(System.Text.Encoding.UTF8.GetBytes(pass)));
				CryptographicBuffer.CopyToByteArray(aesHash.GetValueAndReset(), out var temp);

				Array.Copy(temp, 0, hash, 0, 16);
				Array.Copy(temp, 0, hash, 15, 16);

				aes = sap.CreateSymmetricKey(CryptographicBuffer.CreateFromByteArray(hash));

				var buffer = CryptographicBuffer.DecodeFromBase64String(input);
				CryptographicBuffer.CopyToByteArray(CryptographicEngine.Decrypt(aes, buffer, null), out var Decrypted);
				decrypted = System.Text.Encoding.UTF8.GetString(Decrypted, 0, Decrypted.Length);

				return decrypted;
			}
			catch (Exception) {
				return null;
			}
		}
	}
}
