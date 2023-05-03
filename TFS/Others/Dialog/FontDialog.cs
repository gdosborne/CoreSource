namespace GregOsborne.Dialog
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Windows;
	using System.Windows.Media;

	public sealed class FontDialog
	{
		public FontDialog()
		{
			StyleVisibility = Visibility.Visible;
			SizeVisibility = Visibility.Visible;
			WeightVisibility = Visibility.Visible;
		}

		public void Show()
		{
			var dlg = new FontDialogBox
			{
				WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
				Title = Title,
				StyleVisibility = StyleVisibility,
				WeightVisibility = WeightVisibility,
				SizeVisibility = SizeVisibility
			};
			dlg.CurrentFontFamily = CurrentFontFamily;
			dlg.CurrentFontSize = CurrentFontSize;
			dlg.CurrentFontWeight = CurrentFontWeight;
			dlg.CurrentFontStyle = CurrentFontStyle;
			dlg.Show();
		}

		public bool? ShowDialog(System.Windows.Window owner)
		{
			var dlg = new FontDialogBox
			{
				WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
				Owner = owner,
				Title = Title,
				StyleVisibility = StyleVisibility,
				WeightVisibility = WeightVisibility,
				SizeVisibility = SizeVisibility								
			};
			dlg.CurrentFontFamily = CurrentFontFamily;
			dlg.CurrentFontSize = CurrentFontSize;
			dlg.CurrentFontWeight = CurrentFontWeight;
			dlg.CurrentFontStyle = CurrentFontStyle;
			var result = dlg.ShowDialog();
			CurrentFontFamily = dlg.CurrentFontFamily;
			CurrentFontSize = dlg.CurrentFontSize;
			CurrentFontWeight = dlg.CurrentFontWeight;
			CurrentFontStyle = dlg.CurrentFontStyle;
			return result;
		}
		public FontStyle CurrentFontStyle { get; set; }
		public FontWeight CurrentFontWeight { get; set; }
		public double CurrentFontSize { get; set; }
		public FontFamily CurrentFontFamily { get; set; }
		public Visibility StyleVisibility { get; set; }
		public Visibility WeightVisibility { get; set; }
		public Visibility SizeVisibility { get; set; }
		public string Title { get; set; }
	}
}
