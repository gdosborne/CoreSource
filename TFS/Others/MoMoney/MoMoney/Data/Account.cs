// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Account
//
namespace MoMoney.Data
{
	using MoMoney.Data.Providers;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Xml.Linq;

	public enum AccountTypes
	{
		Checking,
		Savings,
		Investment
	}

	public class Account
	{
		#region Public Constructors
		public Account() {
			Transactions = new ObservableCollection<Transaction>();
		}
		#endregion Public Constructors

		#region Public Methods
		public static Account FromXElement(XElement element, XMLProvider provider) {
			var result = new Account
			{
				ID = int.Parse(element.Attribute(XMLProvider.IDFIELD).Value),
				Created = DateTime.Parse(element.Attribute(XMLProvider.CREATEDFIELD).Value),
				Name = element.Attribute(XMLProvider.NAMEFIELD).Value,
				StartingBalance = decimal.Parse(element.Attribute(XMLProvider.STARTINGBALANCEFIELD).Value),
				AccountType = (AccountTypes)Enum.Parse(typeof(AccountTypes), element.Attribute(XMLProvider.ACCOUNTTYPEFIELD).Value, true)
			};
			var xTransactions = element.Element(XMLProvider.TRANSACTIONSKEY);
			if (xTransactions == null)
				return result;
			xTransactions.Elements().ToList().ForEach(x => result.Transactions.Add(Transaction.FromXElement(x, provider)));
			return result;
		}
		public XElement ToXElement() {
			var result = new XElement(XMLProvider.ACCOUNTKEY,
				new XAttribute(XMLProvider.IDFIELD, ID),
				new XAttribute(XMLProvider.NAMEFIELD, Name),
				new XAttribute(XMLProvider.CREATEDFIELD, Created),
				new XAttribute(XMLProvider.STARTINGBALANCEFIELD, StartingBalance),
				new XAttribute(XMLProvider.ACCOUNTTYPEFIELD, AccountType));
			var xTransactions = new XElement(XMLProvider.TRANSACTIONSKEY);
			Transactions.ToList().ForEach(x => xTransactions.Add(x.ToXElement()));
			result.Add(xTransactions);
			return result;
		}
		#endregion Public Methods

		#region Public Properties
		public AccountTypes AccountType { get; set; }

		public DateTime Created { get; set; }

		public int ID { get; set; }

		public string Name { get; set; }

		public decimal StartingBalance { get; set; }

		public decimal TodaysBalance {
			get {
				var result = StartingBalance;
				Transactions.Where(x => x.Posted <= DateTime.Now).ToList().ForEach(x =>
				{
					if (x.TransactionType == TransactionTypes.Credit)
						result += x.Amount;
					else
						result -= x.Amount;
				});
				return result;
			}
			set {
				throw new NotImplementedException();
			}
		}

		public ObservableCollection<Transaction> Transactions { get; private set; }
		#endregion Public Properties
	}
}
