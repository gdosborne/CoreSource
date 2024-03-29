namespace GregOsborne.AppVersion
{
	using GregOsborne.Application.Windows;
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;

	public partial class SetCPPVariablesWindow : Window
	{
		#region Public Constructors
		public SetCPPVariablesWindow() {
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e) {
			base.OnSourceInitialized(e);
			this.HideControlBox();
			this.HideMinimizeAndMaximizeButtons();
		}
		#endregion Protected Methods

		#region Public Properties
		public SetCPPVariablesWindowView View {
			get {
				return LayoutRoot.GetView<SetCPPVariablesWindowView>();
			}
		}
		#endregion Public Properties

		private void SetCPPVariablesWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "DialogResult":
					DialogResult = View.DialogResult;
					break;
			}
		}
	}
}
