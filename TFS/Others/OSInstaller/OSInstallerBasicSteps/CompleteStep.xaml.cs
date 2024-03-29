namespace OSInstallerBasicSteps
{
	using GregOsborne.MVVMFramework;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Windows.Controls;
	using System.Windows.Media;

	public partial class CompleteStep : UserControl, IInstallerWizardStep, IInstallerComplete
	{
		#region Public Constructors
		public CompleteStep()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Public Methods
		public void Initialize()
		{
		}

		public void ModifyCompleteMessage(string message)
		{
			View.Paragraph1Text = message;
		}
		#endregion Public Methods

		#region Private Methods
		private void CompleteStepView_ExecuteUIAction(object sender, ExecuteUiActionEventArgs e)
		{
			if (e.CommandToExecute.Equals("DisplayLog", StringComparison.OrdinalIgnoreCase))
			{
				var fileName = Manager.LogFileName;
				var p = new Process
				{
					StartInfo = new ProcessStartInfo
					{
						FileName = fileName,
						WindowStyle = ProcessWindowStyle.Normal
					}
				};
				p.Start();
			}
		}
		#endregion Private Methods

		#region Private Fields
		private IInstallerManager _Manager = null;
		#endregion Private Fields

		#region Public Properties
		public string Id { get; set; }
		public bool IsInstallationStep { get; set; }
		public IInstallerManager Manager
		{
			get
			{
				return _Manager;
			}
			set
			{
				_Manager = value;
				View.Header = Manager.ExpandVariable(Id, "Header");
				View.Paragraph1Text = Manager.ExpandVariable(Id, "Paragraph1");
				View.LogFileName = _Manager.LogFileName;
				View.WindowText = new SolidColorBrush((Color)Manager.Properties.First(x => x.Name.Equals("WindowText")).Value);
			}
		}
		public int Sequence { get; set; }
		public CompleteStepView View
		{
			get { return LayoutRoot.GetView<CompleteStepView>(); }
		}
		#endregion Public Properties
	}
}
