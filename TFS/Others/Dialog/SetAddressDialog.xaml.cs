namespace GregOsborne.Dialog
{
	using MVVMFramework;
	using GregOsborne.Application.Windows;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;

	internal partial class SetAddressDialog : Window
	{
		#region Public Constructors
		public SetAddressDialog()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideControlBox();
			this.HideMinimizeAndMaximizeButtons();
		}
		#endregion Protected Methods

		#region Private Methods
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		private void SetAddressDialogView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals("DialogResult"))
			{
				this.DialogResult = View.DialogResult;
			}
		}
		#endregion Private Methods

		#region Public Properties
		public SetAddressDialogView View { get { return LayoutRoot.GetView<SetAddressDialogView>(); } }
		#endregion Public Properties
	}
}
