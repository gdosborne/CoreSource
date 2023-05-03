// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// IDataProvider interface
//
namespace MoMoney.Data.Providers
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	public interface IDataProvider
	{
		#region Public Methods
		void Open(string fileName);
		void Save();
		void Save(string fileName);
		#endregion Public Methods

		#region Public Events
		event EventHandler DataChanged;
		#endregion Public Events

		#region Public Properties
		ObservableCollection<Account> Accounts {
			get;
		}

		string FileName {
			get;
		}
		bool HasChanges { get; set; }

		int MaxAccountNumber { get; }
		#endregion Public Properties
	}
}
