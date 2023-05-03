namespace OSInstallerExtensibility.Interfaces
{
	using OSInstallerCommands;
	using OSInstallerExtensibility.Events;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Media;

	public interface IInstallerManager
	{
		#region Public Methods
		void AddData(IInstallerData data);
		void AddProperty(IInstallerProperty property);
		void AddStep(IInstallerWizardStep step);
		void CreateNew(string fileName);
		void DisplayStep(IInstallerWizardStep step);
		string ExpandString(string source);
		string ExpandVariable(string name);
		string ExpandVariable(string stepId, string name);
		IInstallerComplete GetCompleteStep();
		ImageSource GetImageSourceFromResource(string assemblyName, string resourceName);
		IInstallerWizardStep GetRevertStep();
		void GoToNextStep();
		void GoToPreviousStep();
		void Load(string fileName);
		void Save();
		void Save(string fileName);
		void StartInstallation(IInstallerWizardStep installStep);
		void StartWizard();
		void WriteToLog(string message);
		#endregion Public Methods

		#region Public Events
		event EventHandler InstallationStarted;
		event InstallCompleteHandler InstallComplete;
		event InstallerCommandExecutingHandler InstallerCommandExecuting;
		event EventHandler LoadComplete;
		event StepChangedHandler StepChanged;
		#endregion Public Events

		#region Public Properties
		bool AllowSilentInstall { get; set; }
		IList<BaseCommand> Commands { get; }
		IList<IInstallerData> Datum { get; }
		IList<IInstallerExtension> Extensions { get; }
		string FileName { get; }
		IList<string> GeneratedTempFiles { get; }
		Version InstallerFileNameVersion { get; set; }
		bool IsDirty { get; set; }
		bool IsInstallationInProgress { get; }
		IList<IInstallerItem> Items { get; }
		string LogFileName { get; }
		IList<IInstallerItem> PreprocessingItems { get; }
		IList<IInstallerProperty> Properties { get; }
		IList<string> RequiredDataNames { get; }
		CommandStatuses Status { get; set; }
		int StepNumber { get; }
		IList<IInstallerWizardStep> Steps { get; }
		char VariableTrigger { get; set; }
		#endregion Public Properties
	}
}
