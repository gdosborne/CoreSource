namespace GregOsborne.Dialog
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Windows;

	public sealed class AddressDialog
	{
		public AddressDialog()
		{
			Title = "Address";
		}
		public string Address { get; set; }
		public string Title { get; set; }
		public bool? ShowDialog(Window owner)
		{
            //var dlg = new SetAddressDialog();
            //if (owner != null)
            //{
            //	dlg.Owner = owner;
            //	dlg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //}
            //else
            //	dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //dlg.Title = Title;
            //dlg.View.Address = Address;
            //var result = dlg.ShowDialog();
            //if (!result.GetValueOrDefault())
            //	return result;
            //Address = dlg.View.Address;
            //dlg.Close();
            //return result;
            return null;
		}
		public void Show()
		{
			//var dlg = new SetAddressDialog();
			//dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			//dlg.Title = Title;
			//dlg.View.Address = Address;
			//dlg.ShowDialog();
		}
	}
}
