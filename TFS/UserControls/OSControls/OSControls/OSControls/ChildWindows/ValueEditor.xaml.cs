namespace OSControls.ChildWindows {
	using GregOsborne.Application.Windows;
	using GregOsborne.MVVMFramework;
	using System;
	using System.Windows;
	using System.Windows.Controls;

	public partial class ValueEditor : Window
	{
		#region Public Constructors
		public ValueEditor()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e)
		{
			this.HideControlBox();
		}
		#endregion Protected Methods

		#region Private Methods
		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if ((sender as ComboBox).SelectedItem == null)
				return;
			var selectedIndex = (sender as ComboBox).SelectedIndex;
			ValueTextBox.SelectedText = string.Format("{0}{1}{0}", View.VariableTrigger, View.AllVariableNames[selectedIndex]);
			(sender as ComboBox).SelectedItem = null;
		}
		private void ValueEditorView_ExecuteUIAction(object sender, ExecuteUiActionEventArgs e)
		{
			if (e.CommandToExecute.Equals("CloseWindow"))
				DialogResult = (bool)e.Parameters["DialogResult"];
		}
		#endregion Private Methods

		#region Public Properties
		public ValueEditorView View
		{
			get { return LayoutRoot.GetView<ValueEditorView>(); }
		}
		#endregion Public Properties
	}
}
