namespace OSInstallerBasicSteps
{
	using GregOsborne.MVVMFramework;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Controls;
	using System.Windows.Media;

	public partial class StartStep : UserControl, IInstallerWizardStep
	{
		#region Public Constructors
		public StartStep()
		{
			InitializeComponent();
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
				View.Paragraph1Text = Manager.ExpandVariable(Id, "Paragraph1");
				View.Paragraph2Text = Manager.ExpandVariable(Id, "Paragraph2");
				View.WindowText = new SolidColorBrush((Color)Manager.Properties.First(x => x.Name.Equals("WindowText")).Value);
			}
		}
		public int Sequence { get; set; }
		public StartStepView View
		{
			get { return LayoutRoot.GetView<StartStepView>(); }
		}
		#endregion Public Properties
	}
}
