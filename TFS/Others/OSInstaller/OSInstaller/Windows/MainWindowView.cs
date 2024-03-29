namespace OSInstaller.Windows
{
	using Microsoft.WindowsAPICodePack.Dialogs;
	using GregOsborne.MVVMFramework;
	using OSInstallerExtensibility.Events;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Reflection;
	using System.Windows.Interop;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;

	public class MainWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Constructors
		public MainWindowView()
		{
			TitleForegroundBrush = new SolidColorBrush(Colors.WhiteSmoke);
			TitleBackgroundBrush = new SolidColorBrush(Colors.DarkBlue);
			Title = "Application Title";
			CancelButtonText = "Cancel";
		}
		#endregion Public Constructors

		#region Public Methods
		public void Initialize()
		{
			if (!string.IsNullOrEmpty(App.InstallerFileName))
			{
				Manager = new OSInstallerExtensibility.Classes.Managers.Manager();
				Manager.StepChanged += Manager_StepChanged;
				Manager.InstallationStarted += Manager_InstallationStarted;
				Manager.InstallComplete += Manager_InstallComplete;
				Manager.InstallerCommandExecuting += Manager_InstallerCommandExecuting;

				try
				{
					Manager.Load(App.InstallerFileName);

					TitleBackgroundBrush = new SolidColorBrush((Color)Manager.Properties.First(x => x.Name.Equals("TitleBackground")).Value);
					TitleForegroundBrush = new SolidColorBrush((Color)Manager.Properties.First(x => x.Name.Equals("TitleForeground")).Value);
					WindowBackground = new SolidColorBrush((Color)Manager.Properties.First(x => x.Name.Equals("WindowBackground")).Value);
					WindowText = new SolidColorBrush((Color)Manager.Properties.First(x => x.Name.Equals("WindowText")).Value);
					AreaSeparator = new SolidColorBrush((Color)Manager.Properties.First(x => x.Name.Equals("AreaSeparator")).Value);
					ImagePath = (string)Manager.Properties.First(x => x.Name.Equals("ImagePath")).Value;

					Manager.Steps.ToList().ForEach(x => x.Manager = Manager);
					Title = Manager.ExpandString(Manager.Datum.First(x => x.Name.Equals("InstallationTitle")).Value);
					Manager.StartWizard();
				}
				catch (ReflectionTypeLoadException ex1)
				{
					var ex = ((ReflectionTypeLoadException)ex1).LoaderExceptions[0];
					App.GetTaskDialog("Error", string.Format("The installer file \"{0}\" could not be loaded.\rError Code: {1}", App.InstallerFileName, App.INSTALLER_TYPE_LOAD_ERROR), ex.Message, string.Empty, TaskDialogStandardIcon.Error, new WindowInteropHelper(App.Current.MainWindow).Handle, TaskDialogStandardButtons.Ok).Show();
					App.Current.Shutdown(App.INSTALLER_TYPE_LOAD_ERROR);
					return;
				}
				catch (Exception ex2)
				{
					App.GetTaskDialog("Error", string.Format("The installer file \"{0}\" could not be loaded.\rError Code: {1}", App.InstallerFileName, App.INSTALLER_FILE_LOAD_ERROR), ex2.Message, string.Empty, TaskDialogStandardIcon.Error, new WindowInteropHelper(App.Current.MainWindow).Handle, TaskDialogStandardButtons.Ok).Show();
					App.Current.Shutdown(App.INSTALLER_FILE_LOAD_ERROR);
					return;
				}
			}
			else
				App.Current.Shutdown(0);
		}

		public override void UpdateInterface()
		{
			var isFinalStep = CurrentStepNumber == Manager.Steps.Max(x => x.Sequence);
			CancelCommand.RaiseCanExecuteChanged();
			CancelButtonText = isFinalStep ? "Finish" : "Cancel";
			NextCommand.RaiseCanExecuteChanged();
			PreviousCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Private Methods
		private void Cancel(object state)
		{
			if (ExecuteAction != null)
				ExecuteAction(this, new ExecuteUiActionEventArgs("CloseCancel", new Dictionary<string, object> { { "PromptFirst", !CancelButtonText.Equals("Finish") } }));
		}
		private void Manager_InstallationStarted(object sender, EventArgs e)
		{
			if (InstallationStarted != null)
				InstallationStarted(this, e);
		}
		private void Manager_InstallComplete(object sender, InstallCompleteEventArgs e)
		{
			if (InstallComplete != null)
				InstallComplete(this, e);
		}
		private void Manager_InstallerCommandExecuting(object sender, InstallerCommandExecutingEventArgs e)
		{
			if (InstallerCommandExecuting != null)
				InstallerCommandExecuting(this, e);
		}
		private void Manager_StepChanged(object sender, OSInstallerExtensibility.Events.StepChangedEventArgs e)
		{
			CurrentStepNumber = e.Step.Sequence;
			CurrentWizardStep = e.Step;
			UpdateInterface();
		}
		private void Next(object state)
		{
			Manager.GoToNextStep();
			UpdateInterface();
		}
		private void Previous(object state)
		{
			Manager.GoToPreviousStep();
			UpdateInterface();
		}
		private bool ValidateCancelState(object state)
		{
			return Manager == null || (Manager != null && !Manager.IsInstallationInProgress);
		}
		private bool ValidateNextState(object state)
		{
			return Manager != null && !Manager.IsInstallationInProgress && CurrentStepNumber < Manager.Steps.Max(x => x.Sequence);
		}
		private bool ValidatePreviousState(object state)
		{
			return Manager != null && !Manager.IsInstallationInProgress && CurrentStepNumber > Manager.Steps.Min(x => x.Sequence);
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUiActionHandler ExecuteAction;
		public event EventHandler InstallationStarted;
		public event InstallCompleteHandler InstallComplete;
		public event InstallerCommandExecutingHandler InstallerCommandExecuting;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private Brush _AreaSeparator;
		private string _CancelButtonText;
		private DelegateCommand _CancelCommand = null;
		private int _CurrentStepNumber;
		private IInstallerWizardStep _CurrentWizardStep;
		private string _ImagePath;
		private bool _IsFinalStep;
		private IInstallerManager _Manager;
		private DelegateCommand _NextCommand = null;
		private DelegateCommand _PreviousCommand = null;
		private string _Title;
		private Brush _TitleBackgroundBrush;
		private Brush _TitleForegroundBrush;
		private Brush _WindowBackground;
		private Brush _WindowText;
		private ImageSource _WizardImageSource;
		#endregion Private Fields

		#region Public Properties
		public Brush AreaSeparator
		{
			get
			{
				return _AreaSeparator;
			}
			set
			{
				_AreaSeparator = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AreaSeparator"));
			}
		}
		public string CancelButtonText
		{
			get
			{
				return _CancelButtonText;
			}
			set
			{
				_CancelButtonText = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CancelButtonText"));
			}
		}
		public DelegateCommand CancelCommand
		{
			get
			{
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		public int CurrentStepNumber
		{
			get
			{
				return _CurrentStepNumber;
			}
			set
			{
				_CurrentStepNumber = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CurrentStepNumber"));
			}
		}
		public IInstallerWizardStep CurrentWizardStep
		{
			get
			{
				return _CurrentWizardStep;
			}
			set
			{
				_CurrentWizardStep = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CurrentWizardStep"));
			}
		}
		public string ImagePath
		{
			get
			{
				return _ImagePath;
			}
			set
			{
				_ImagePath = value;
				if (!string.IsNullOrEmpty(value))
					WizardImageSource = new BitmapImage(new Uri(value));
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ImagePath"));
			}
		}
		public bool IsFinalStep
		{
			get
			{
				return _IsFinalStep;
			}
			set
			{
				_IsFinalStep = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsFinalStep"));
			}
		}
		public IInstallerManager Manager
		{
			get
			{
				return _Manager;
			}
			set
			{
				_Manager = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Manager"));
			}
		}
		public DelegateCommand NextCommand
		{
			get
			{
				if (_NextCommand == null)
					_NextCommand = new DelegateCommand(Next, ValidateNextState);
				return _NextCommand as DelegateCommand;
			}
		}
		public DelegateCommand PreviousCommand
		{
			get
			{
				if (_PreviousCommand == null)
					_PreviousCommand = new DelegateCommand(Previous, ValidatePreviousState);
				return _PreviousCommand as DelegateCommand;
			}
		}
		public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				_Title = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Title"));
			}
		}
		public Brush TitleBackgroundBrush
		{
			get
			{
				return _TitleBackgroundBrush;
			}
			set
			{
				_TitleBackgroundBrush = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TitleBackgroundBrush"));
			}
		}
		public Brush TitleForegroundBrush
		{
			get
			{
				return _TitleForegroundBrush;
			}
			set
			{
				_TitleForegroundBrush = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TitleForegroundBrush"));
			}
		}
		public Brush WindowBackground
		{
			get
			{
				return _WindowBackground;
			}
			set
			{
				_WindowBackground = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("WindowBackground"));
			}
		}
		public Brush WindowText
		{
			get
			{
				return _WindowText;
			}
			set
			{
				_WindowText = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("WindowText"));
			}
		}
		public ImageSource WizardImageSource
		{
			get
			{
				return _WizardImageSource;
			}
			set
			{
				_WizardImageSource = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("WizardImageSource"));
			}
		}
		#endregion Public Properties
	}
}
