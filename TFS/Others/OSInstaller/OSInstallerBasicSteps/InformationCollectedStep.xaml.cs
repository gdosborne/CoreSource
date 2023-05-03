namespace OSInstallerBasicSteps
{
	using MVVMFramework;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Windows.Controls;
	using System.Windows.Media;

	public partial class InformationCollectedStep : UserControl, IInstallerWizardStep
	{
		#region Public Constructors
		public InformationCollectedStep()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Public Methods
		public void Initialize()
		{
			var finalData = Manager.Datum.Where(x => x.MustValidate).ToList();
			var finalText = new StringBuilder();
			finalData.ForEach(x => finalText.AppendLine(Manager.ExpandString(x.Value)));
			View.CompletedText = finalText.ToString();
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
				View.WindowText = new SolidColorBrush((Color)Manager.Properties.First(x => x.Name.Equals("WindowText")).Value);
			}
		}
		public int Sequence { get; set; }
		public InformationCollectedStepView View
		{
			get { return LayoutRoot.GetView<InformationCollectedStepView>(); }
		}
		#endregion Public Properties
	}
}
