// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Encrypted xml data provider
//
namespace MoMoney.Data.Providers
{
	using OSCrypto;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;

	public class EncryptedXMLProvider : XMLProvider
	{
		#region Public Constructors
		public EncryptedXMLProvider(string password)
			: base() {
			_Password = password;
		}
		#endregion Public Constructors

		#region Public Methods
		public override void Open(string fileName) {
			using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
			using (var br = new BinaryReader(fs)) {
				var data = br.ReadBytes(Convert.ToInt32(fs.Length));
				var crypto = new Crypto(_Password);
				var xmlData = crypto.Decrypt<byte[]>(data);
				ReadData(XDocument.Parse(xmlData));
			}
			HasChanges = false;
		}
		public override void Save() {
			var doc = new XDocument(new XDocument(new XElement(MMFKEY)));
			var xAccounts = new XElement(ACCOUNTSKEY);
			Accounts.ToList().ForEach(x => xAccounts.Add(x.ToXElement()));
			doc.Root.Add(xAccounts);
			var crypto = new Crypto(_Password);
			var data = crypto.Encrypt<byte[]>(doc.ToString());
			using (var fs = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
			using (var br = new BinaryWriter(fs)) {
				br.Write(data);
			}
			HasChanges = false;
		}
		public override void Save(string fileName) {
			FileName = fileName;
			Save();
		}
		#endregion Public Methods

		#region Private Fields
		private string _Password = null;
		#endregion Private Fields
	}
}
