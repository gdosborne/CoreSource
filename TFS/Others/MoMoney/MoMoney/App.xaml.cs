// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
//
//
namespace MoMoney
{
	using System.Windows;

	public partial class App : Application
	{
		#region Public Constructors
		public App() {
			if (MoMoney.Properties.Settings.Default.RequiresUpdate) {
				MoMoney.Properties.Settings.Default.Upgrade();
				MoMoney.Properties.Settings.Default.RequiresUpdate = false;
				MoMoney.Properties.Settings.Default.Save();
			}
		}
		#endregion Public Constructors
	}
}
