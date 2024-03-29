// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg
// Created: 10/7/2016 4:06:50 PM
// Updated: 
// Updated by: 
// -----------------------------------------------------------------------
//
// MainWindowView
//
namespace MyMinistry
{
	using System;
	using System.ComponentModel;
	using System.Windows;
	using MVVMFramework;
	using GregOsborne.Application.Primitives;

	public class MainWindowView : INotifyPropertyChanged
	{
		public MainWindowView()
		{
			ApplicationTitle = App.ApplicationTitle;
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public event ExecuteUIActionHandler ExecuteUIAction;
		public void UpdateInterface()
		{

		}
		public void InitView()
		{

		}
		private string _ApplicationTitle;
		public string ApplicationTitle
		{
			get { return _ApplicationTitle; }
			set
			{
				_ApplicationTitle = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
	}
}
