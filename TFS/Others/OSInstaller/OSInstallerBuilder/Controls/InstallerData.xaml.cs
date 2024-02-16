namespace OSInstallerBuilder.Controls
{
	using GregOsborne.MVVMFramework;
	using OSInstallerBuilder.Classes.Events;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;

	public partial class InstallerData : UserControl, IInstallerSettingsController
	{
		#region Public Constructors
		public InstallerData()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Public Methods
		public void Reset()
		{
			TheNameValueEditor.ItemsSource = null;
			Manager = null;
			View.UpdateInterface();
		}
		#endregion Public Methods

		#region Private Methods
		private static void onManagerChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (InstallerData)source;
			if (src == null)
				return;
			var value = (IInstallerManager)e.NewValue;
			src.View.Manager = value;
		}
		private void InstallerDataView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals("IndividualItem"))
			{
				if (SettingsChanged != null)
					SettingsChanged(this, EventArgs.Empty);
			}
		}
		private void NamedValueEditor_QueryAddNewItem(object sender, OSControls.Classes.Events.QueryAddNewItemEventArgs e)
		{
			var item = new OSInstallerExtensibility.Classes.Data.InstallerData("[NEWITEM]") { Value = "[VALUE]", IsEditable = true, IsRequired = false, IsStepData = false, MustValidate = true };
			View.Data.Add(item);
			e.TheNewItem = item;
			View.Manager.IsDirty = true;
			View.Manager.Datum.Add(item);
			if (DataItemAdded != null)
				DataItemAdded(this, new DataItemAddedEventArgs(item));
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}
		#endregion Private Methods

		#region Public Events
		public event DataItemAddedHandler DataItemAdded;
		public event EventHandler SettingsChanged;
		#endregion Public Events

		#region Public Fields
		public static readonly DependencyProperty ManagerProperty = DependencyProperty.Register("Manager", typeof(IInstallerManager), typeof(InstallerData), new PropertyMetadata(null, onManagerChanged));
		#endregion Public Fields

		#region Public Properties
		public IInstallerManager Manager
		{
			get { return (IInstallerManager)GetValue(ManagerProperty); }
			set { SetValue(ManagerProperty, value); }
		}
		public InstallerDataView View
		{
			get { return LayoutRoot.GetView<InstallerDataView>(); }
		}
		#endregion Public Properties
	}
}
