// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Transaction
//
namespace MoMoney.Data
{
	using MoMoney.Data.Providers;
	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Xml.Linq;

	public enum TransactionTypes
	{
		Credit,
		Debit
	}

	public class Transaction : INotifyPropertyChanged
	{
		#region Public Constructors
		public Transaction() {
			TransactionTypes = new ObservableCollection<Data.TransactionTypes>
			{
				Data.TransactionTypes.Credit,
				Data.TransactionTypes.Debit
			};
			TransactionType = Data.TransactionTypes.Debit;
			Created = DateTime.Now;
			Posted = DateTime.Now;
		}
		#endregion Public Constructors

		#region Public Methods
		public static Transaction FromXElement(XElement element, XMLProvider provider) {
			var result = new Transaction
			{
				ID = int.Parse(element.Attribute(XMLProvider.IDFIELD).Value),
				Created = DateTime.Parse(element.Attribute(XMLProvider.CREATEDFIELD).Value),
				Amount = decimal.Parse(element.Attribute(XMLProvider.AMOUNTFIELD).Value),
				Posted = DateTime.Parse(element.Attribute(XMLProvider.POSTEDFIELD).Value),
				CheckNumber = int.Parse(element.Attribute(XMLProvider.CHECKNUMBERFIELD).Value),
				TransactionType = (TransactionTypes)Enum.Parse(typeof(TransactionTypes), element.Attribute(XMLProvider.TRANSACTIONTYPEFIELD).Value, true),
				Payee = element.Attribute(XMLProvider.PAYEEFIELD).Value
			};
			return result;
		}
		public XElement ToXElement() {
			return new XElement(XMLProvider.TRANSACTIONKEY,
				new XAttribute(XMLProvider.IDFIELD, ID),
				new XAttribute(XMLProvider.CREATEDFIELD, Created),
				new XAttribute(XMLProvider.AMOUNTFIELD, Amount),
				new XAttribute(XMLProvider.POSTEDFIELD, Posted),
				new XAttribute(XMLProvider.PAYEEFIELD, Payee),
				new XAttribute(XMLProvider.CHECKNUMBERFIELD, CheckNumber),
				new XAttribute(XMLProvider.TRANSACTIONTYPEFIELD, TransactionType));
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private decimal _Amount;
		private int _CheckNumber;
		private DateTime _Created;
		private int _ID;
		private string _Payee;
		private DateTime _Posted;
		private TransactionTypes _TransactionType;
		private ObservableCollection<TransactionTypes> _TransactionTypes;
		#endregion Private Fields

		#region Public Properties
		public decimal Amount {
			get {
				return _Amount;
			}
			set {
				_Amount = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
			}
		}
		public int CheckNumber {
			get {
				return _CheckNumber;
			}
			set {
				_CheckNumber = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CheckNumber"));
			}
		}
		public DateTime Created {
			get {
				return _Created;
			}
			set {
				_Created = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Created"));
			}
		}
		public int ID {
			get {
				return _ID;
			}
			set {
				_ID = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ID"));
			}
		}
		public string Payee {
			get {
				return _Payee;
			}
			set {
				_Payee = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Payee"));
			}
		}
		public DateTime Posted {
			get {
				return _Posted;
			}
			set {
				_Posted = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Posted"));
			}
		}
		public TransactionTypes TransactionType {
			get {
				return _TransactionType;
			}
			set {
				_TransactionType = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TransactionType"));
			}
		}
		public ObservableCollection<TransactionTypes> TransactionTypes {
			get {
				return _TransactionTypes;
			}
			set {
				_TransactionTypes = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TransactionTypes"));
			}
		}
		#endregion Public Properties
	}
}
