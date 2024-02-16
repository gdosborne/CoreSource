namespace OSInstallerBuilder.Controls
{
	using GregOsborne.MVVMFramework;
	using GregOsborne.Application.Primitives;
	using OSInstallerExtensibility.Classes.Data;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;

	public class InstallerUIView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Constructors
		public InstallerUIView()
		{
			WindowBackground = new SolidColorBrush(Colors.White);
			TitleBackgroundBrush = new SolidColorBrush(Colors.LightBlue);
			TitleForegroundBrush = new SolidColorBrush(Colors.DarkBlue);
			AreaSeparator = new SolidColorBrush(Colors.DarkGray);
			WindowText = new SolidColorBrush(Colors.Black);
			WizardImageSource = DefaultSource();
			Title = "Application Installer";
			Header = "Welcome to the Application Installer";
			Paragraph1Text = "This wizard will guide you through the steps of installing the application on your machine";
			Paragraph2Text = "Click the Next button to continue.";
			ColorPickerVisibility = Visibility.Collapsed;
			OverlayVisibility = Visibility.Collapsed;
			ImagePathVisibility = Visibility.Collapsed;
			UpdateInterface();
		}
		#endregion Public Constructors

		#region Public Methods
		public override void UpdateInterface()
		{
			ColorPickerVisibility = SelectedItemType != SelectedItemTypes.None && SelectedItemType != SelectedItemTypes.Image ? Visibility.Visible : Visibility.Collapsed;
			ImagePathVisibility = SelectedItemType == SelectedItemTypes.Image ? Visibility.Visible : Visibility.Collapsed;
			ClearImageCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Private Methods
		private void ClearImage(object state)
		{
			ImagePath = string.Empty;
		}
		private ImageSource DefaultSource()
		{
			BitmapImage bi = new BitmapImage();
			bi.BeginInit();
			bi.UriSource = new Uri("/Assets/wizard.png", UriKind.RelativeOrAbsolute);
			bi.EndInit();
			return bi;
		}
		private void SelectImage(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUiActionEventArgs("SelectImageFile"));
		}
		private bool ValidateClearImageState(object state)
		{
			return !string.IsNullOrEmpty(ImagePath);
		}
		private bool ValidateSelectImageState(object state)
		{
			return true;
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUiActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private Brush _AreaSeparator;
		private DelegateCommand _ClearImageCommand = null;
		private Visibility _ColorPickerVisibility;
		private Color _CurrentColor;
		private string _Header;
		private string _ImagePath;
		private Visibility _ImagePathVisibility;
		private bool _IsInitializing;
		private IInstallerManager _Manager;
		private double _OverlayHeight;
		private double _OverlayLeft;
		private Thickness _OverlayMargin;
		private double _OverlayTop;
		private Visibility _OverlayVisibility;
		private double _OverlayWidth;
		private string _Paragraph1Text;
		private string _Paragraph2Text;
		private SelectedItemTypes _SelectedItemType;
		private DelegateCommand _SelectImageCommand = null;
		private string _Title;
		private Brush _TitleBackgroundBrush;
		private Brush _TitleForegroundBrush;
		private Brush _WindowBackground;
		private Brush _WindowText;
		private ImageSource _WizardImageSource;
		#endregion Private Fields

		#region Public Enums
		public enum SelectedItemTypes
		{
			None,
			HeaderBackground,
			HeaderTextForeground,
			Separator,
			Image,
			Background,
			WindowText
		}
		#endregion Public Enums

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
				if (Manager != null)
					Manager.Properties.First(x => x.Name.Equals("AreaSeparator")).Value = (value.As<SolidColorBrush>()).Color;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AreaSeparator"));
			}
		}
		public DelegateCommand ClearImageCommand
		{
			get
			{
				if (_ClearImageCommand == null)
					_ClearImageCommand = new DelegateCommand(ClearImage, ValidateClearImageState);
				return _ClearImageCommand.As<DelegateCommand>();
			}
		}
		public Visibility ColorPickerVisibility
		{
			get
			{
				return _ColorPickerVisibility;
			}
			set
			{
				_ColorPickerVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ColorPickerVisibility"));
			}
		}
		public Color CurrentColor
		{
			get
			{
				return _CurrentColor;
			}
			set
			{
				_CurrentColor = value;
				switch (SelectedItemType)
				{
					case SelectedItemTypes.None:
						break;
					case SelectedItemTypes.HeaderBackground:
						TitleBackgroundBrush = new SolidColorBrush(value);
						break;
					case SelectedItemTypes.HeaderTextForeground:
						TitleForegroundBrush = new SolidColorBrush(value);
						break;
					case SelectedItemTypes.Separator:
						AreaSeparator = new SolidColorBrush(value);
						break;
					case SelectedItemTypes.Image:
						break;
					case SelectedItemTypes.Background:
						WindowBackground = new SolidColorBrush(value);
						break;
					case SelectedItemTypes.WindowText:
						WindowText = new SolidColorBrush(value);
						break;
					default:
						break;
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CurrentColor"));
			}
		}
		public string Header
		{
			get
			{
				return _Header;
			}
			set
			{
				_Header = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Header"));
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
				if (Manager != null)
				{
					var item = Manager.PreprocessingItems.FirstOrDefault(x => x.Name.Equals(OSInstallerExtensibility.Classes.Managers.Manager.PREPROCESS_IMAGE));
					if (item == null)
						Manager.PreprocessingItems.Add(new InstallerItem(OSInstallerExtensibility.Classes.Managers.Manager.PREPROCESS_IMAGE)
						{
							IncludeSubFolders = false,
							ItemType = ItemTypes.File,
							Path = value,
							TypeSource = null
						});
					else
						item.Path = value;
					var imgPathProperty = Manager.Properties.FirstOrDefault(x => x.Name.Equals("ImagePath"));
					if (imgPathProperty == null)
						Manager.Properties.Add(new InstallerProperty("ImagePath") { Value = OSInstallerExtensibility.Classes.Managers.Manager.PREPROCESS_IMAGE });
					else
						imgPathProperty.Value = OSInstallerExtensibility.Classes.Managers.Manager.PREPROCESS_IMAGE;
				}
				if (!string.IsNullOrEmpty(value))
					WizardImageSource = new BitmapImage(new Uri(value));
				else
					WizardImageSource = DefaultSource();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ImagePath"));
			}
		}
		public Visibility ImagePathVisibility
		{
			get
			{
				return _ImagePathVisibility;
			}
			set
			{
				_ImagePathVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ImagePathVisibility"));
			}
		}
		public bool IsInitializing
		{
			get
			{
				return _IsInitializing;
			}
			set
			{
				_IsInitializing = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsInitializing"));
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
				IsInitializing = true;
				_Manager = value;
				if (_Manager == null)
					return;

				WindowBackground = new SolidColorBrush((Color)_Manager.Properties.First(x => x.Name.Equals("WindowBackground")).Value);
				TitleBackgroundBrush = new SolidColorBrush((Color)_Manager.Properties.First(x => x.Name.Equals("TitleBackground")).Value);
				TitleForegroundBrush = new SolidColorBrush((Color)_Manager.Properties.First(x => x.Name.Equals("TitleForeground")).Value);
				AreaSeparator = new SolidColorBrush((Color)_Manager.Properties.First(x => x.Name.Equals("AreaSeparator")).Value);
				WindowText = new SolidColorBrush((Color)_Manager.Properties.First(x => x.Name.Equals("WindowText")).Value);
				if (Manager.PreprocessingItems.Any(x => x.Name.Equals(OSInstallerExtensibility.Classes.Managers.Manager.PREPROCESS_IMAGE)))
					ImagePath = (string)Manager.PreprocessingItems.FirstOrDefault(x => x.Name.Equals(OSInstallerExtensibility.Classes.Managers.Manager.PREPROCESS_IMAGE)).Path;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Manager"));
				IsInitializing = false;
			}
		}
		public double OverlayHeight
		{
			get
			{
				return _OverlayHeight;
			}
			set
			{
				_OverlayHeight = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OverlayHeight"));
			}
		}
		public double OverlayLeft
		{
			get
			{
				return _OverlayLeft;
			}
			set
			{
				_OverlayLeft = value;
				OverlayMargin = new Thickness(value, OverlayTop, 0, 0);
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OverlayLeft"));
			}
		}
		public Thickness OverlayMargin
		{
			get
			{
				return _OverlayMargin;
			}
			set
			{
				_OverlayMargin = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OverlayMargin"));
			}
		}
		public double OverlayTop
		{
			get
			{
				return _OverlayTop;
			}
			set
			{
				_OverlayTop = value;
				OverlayMargin = new Thickness(OverlayLeft, value, 0, 0);
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OverlayTop"));
			}
		}
		public Visibility OverlayVisibility
		{
			get
			{
				return _OverlayVisibility;
			}
			set
			{
				_OverlayVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OverlayVisibility"));
			}
		}
		public double OverlayWidth
		{
			get
			{
				return _OverlayWidth;
			}
			set
			{
				_OverlayWidth = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OverlayWidth"));
			}
		}
		public string Paragraph1Text
		{
			get
			{
				return _Paragraph1Text;
			}
			set
			{
				_Paragraph1Text = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Paragraph1Text"));
			}
		}
		public string Paragraph2Text
		{
			get
			{
				return _Paragraph2Text;
			}
			set
			{
				_Paragraph2Text = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Paragraph2Text"));
			}
		}
		public SelectedItemTypes SelectedItemType
		{
			get
			{
				return _SelectedItemType;
			}
			set
			{
				_SelectedItemType = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedItemType"));
			}
		}
		public DelegateCommand SelectImageCommand
		{
			get
			{
				if (_SelectImageCommand == null)
					_SelectImageCommand = new DelegateCommand(SelectImage, ValidateSelectImageState);
				return _SelectImageCommand as DelegateCommand;
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
				if (Manager != null)
					Manager.Properties.First(x => x.Name.Equals("TitleBackground")).Value = (value.As<SolidColorBrush>()).Color;
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
				if (Manager != null)
					Manager.Properties.First(x => x.Name.Equals("TitleForeground")).Value = (value.As<SolidColorBrush>()).Color;
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
				if (Manager != null)
					Manager.Properties.First(x => x.Name.Equals("WindowBackground")).Value = (value.As<SolidColorBrush>()).Color;
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
				if (Manager != null)
					Manager.Properties.First(x => x.Name.Equals("WindowText")).Value = (value.As<SolidColorBrush>()).Color;
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
