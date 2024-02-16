namespace OSInstallerBasicSteps
{
	using GregOsborne.MVVMFramework;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Controls;
	using System.Windows.Media;

	public partial class InstallingStep : UserControl, IInstallerWizardStep, IInstallerView
	{
		#region Public Constructors
		public InstallingStep()
		{
			InitializeComponent();
			IsInstallationStep = true;
		}
		#endregion Public Constructors

		#region Public Methods
		public void Initialize()
		{
		}
		#endregion Public Methods

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
				View.WindowText = new SolidColorBrush((Color)Manager.Properties.First(x => x.Name.Equals("WindowText")).Value);
			}
		}
		public double ProgressMaximum
		{
			get { return View.ProgressMaximum; }
			set { View.ProgressMaximum = value; }
		}
		public string ProgressMessage
		{
			get { return View.ProgressMessage; }
			set { View.ProgressMessage = value; }
		}
		public double ProgressValue
		{
			get { return View.ProgressValue; }
			set { View.ProgressValue = value; }
		}
		public int Sequence { get; set; }
		public InstallingStepView View
		{
			get { return LayoutRoot.GetView<InstallingStepView>(); }
		}
		#endregion Public Properties
	}
}
