// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Xml data provider
//
namespace MoMoney.Data.Providers
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;

	public class XMLProvider : IDataProvider
	{
		#region Public Constructors
		public XMLProvider() {
			Accounts = new ObservableCollection<Account>();
			Accounts.CollectionChanged += Accounts_CollectionChanged;
		}
		#endregion Public Constructors

		#region Public Methods
		public virtual void Open(string fileName) {
			if (!File.Exists(fileName))
				throw new FileNotFoundException("Cannot find mmf file", fileName);
			FileName = fileName;
			var doc = XDocument.Load(fileName);
			ReadData(doc); if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}
		public virtual void Save() {
			var doc = new XDocument(new XDocument(new XElement(MMFKEY)));
			var xUsers = new XElement(USERSKEY);
			doc.Root.Add(xUsers);
			var xAccounts = new XElement(ACCOUNTSKEY);
			Accounts.ToList().ForEach(x => xAccounts.Add(x.ToXElement()));
			doc.Root.Add(xAccounts);
			doc.Save(FileName);
		}
		public virtual void Save(string fileName) {
			FileName = fileName;
			Save();
		}
		#endregion Public Methods

		#region Protected Methods
		protected void ReadData(XDocument doc) {
			if (!doc.Root.Name.LocalName.Equals(MMFKEY))
				throw new ApplicationException("Invalid mmf file");
			var xAccounts = doc.Root.Element(ACCOUNTSKEY);
			if (xAccounts == null)
				throw new ApplicationException("No accounts in mmf file");
			xAccounts.Elements().ToList().ForEach(x =>
			{
				var a = Account.FromXElement(x, this);
				if (!Accounts.Any(y => y.ID == a.ID))
					Accounts.Add(a);
			});
			HasChanges = false;
		}
		#endregion Protected Methods

		#region Private Methods
		private void Accounts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
			HasChanges = true;
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}
		#endregion Private Methods

		#region Public Events
		public event EventHandler DataChanged;
		#endregion Public Events

		#region Public Fields
		public static readonly string ACCOUNTKEY = "account";
		public static readonly string ACCOUNTSKEY = "accounts";
		public static readonly string ACCOUNTTYPEFIELD = "accounttype";
		public static readonly string AMOUNTFIELD = "amount";
		public static readonly string CHECKNUMBERFIELD = "checknumber";
		public static readonly string CREATEDBYFIELD = "createdby";
		public static readonly string CREATEDFIELD = "created";
		public static readonly string FIRSTNAMEFIELD = "firstname";
		public static readonly string IDFIELD = "id";
		public static readonly string LASTNAMEFIELD = "lastname";
		public static readonly string MMFKEY = "mmffile";
		public static readonly string NAMEFIELD = "name";
		public static readonly string PAYEEFIELD = "payee";
		public static readonly string POSTEDFIELD = "posted";
		public static readonly string STARTINGBALANCEFIELD = "startingbalance";
		public static readonly string TRANSACTIONKEY = "transaction";
		public static readonly string TRANSACTIONSKEY = "transactions";
		public static readonly string TRANSACTIONTYPEFIELD = "transactiontype";
		public static readonly string USERKEY = "user";
		public static readonly string USERNAMEFIELD = "username";
		public static readonly string USERSKEY = "users";
		#endregion Public Fields

		#region Public Properties
		public ObservableCollection<Account> Accounts { get; private set; }

		public string FileName { get; protected set; }
		public bool HasChanges { get; set; }

		public int MaxAccountNumber {
			get {
				if (!Accounts.Any())
					return 0;
				return Accounts.Max(x => x.ID);
			}
		}
		#endregion Public Properties
	}
}
