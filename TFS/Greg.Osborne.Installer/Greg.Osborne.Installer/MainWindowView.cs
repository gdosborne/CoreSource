namespace Greg.Osborne.Installer {
	using System.Collections.ObjectModel;
	using System.Windows;
	using Greg.Osborne.Installer.Support;
	using GregOsborne.Application.Primitives;
	using GregOsborne.MVVMFramework;

	public partial class MainWindowView : ViewModelBase {

		public MainWindowView() {
			this.InstallerName = "Osborne Installer";
			this.ProductsTabText = "Products";
			this.OptionsTabText = "Options";
			this.InstructionPanelWelcomeParagraph = "Quisque imperdiet fringilla tellus, at vestibulum nisl volutpat nec. Donec dapibus quis neque eu finibus. Lorem ipsum";
			this.HelpTextParagraph = "Maecenas vitae ornare metus. Proin sed enim nec mi elementum venenatis. Pellentesque fringilla turpis ac pulvinar lobortis. Aliquam sed purus nec erat ornare facilisis at a quam. Aliquam aliquet posuere sodales. Vestibulum ut sodales tellus. Nulla fringilla a quam et rhoncus.";

			this.OptionsVisibility = Visibility.Collapsed;
			this.InstallationItemsVisibility = Visibility.Visible;

			this.ControlIcons = new ObservableCollection<ControlIcon>();
			this.SideItems = new ObservableCollection<SideItem>();
			this.InstallationItems = new ObservableCollection<InstallItem>();
			this.OptionItems = new ObservableCollection<OptionItem>();

			var icon = new ControlIcon(this.ProvideFeedbackCommand) {
				Tooltip = "Provide Feedback",
				IconHexValue = "&#xED15;"
			};
			this.ControlIcons.Add(icon);

			this.AppVersionText = this.GetType().Assembly.GetName().Version.ToString();
		}

		public override void Initialize() {
		}

		public void AddInstallationItem(InstallItem item) {
			item.PropertyChanged += this.InstallItem_PropertyChanged;
			this.InstallationItems.Add(item);
			UpdateInterface();
		}

		private void InstallItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) => this.InvokePropertyChanged(e.PropertyName);

		public event ExecuteUiActionHandler ExecuteUiAction;

		private string installerName = default;
		private string productsTabText = default;
		private string optionsTabText = default;
		private ObservableCollection<ControlIcon> controlIcons = default;
		private double windowSizeRatio = default;
		private string instructionPanelWelconmeParagraph = default;
		private ObservableCollection<SideItem> sideItems = default;
		private string helpTextParagraph = default;
		private string appVersionText = default;
		private ObservableCollection<InstallItem> installationItems = default;
		private Visibility installationItemsVisibility = default;
		private Visibility optionsVisibility = default;
		private ObservableCollection<OptionItem> optionItems = default;

		public ObservableCollection<OptionItem> OptionItems {
			get => this.optionItems;
			set {
				this.optionItems = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public Visibility OptionsVisibility {
			get => this.optionsVisibility;
			set {
				this.optionsVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public Visibility InstallationItemsVisibility {
			get => this.installationItemsVisibility;
			set {
				this.installationItemsVisibility = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public ObservableCollection<InstallItem> InstallationItems {
			get => this.installationItems;
			set {
				this.installationItems = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
		public string AppVersionText {
			get => this.appVersionText;
			set {
				this.appVersionText = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string InstallerName {
			get => this.installerName;
			set {
				this.installerName = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string ProductsTabText {
			get => this.productsTabText;
			set {
				this.productsTabText = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string OptionsTabText {
			get => this.optionsTabText;
			set {
				this.optionsTabText = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public ObservableCollection<ControlIcon> ControlIcons {
			get => this.controlIcons;
			set {
				this.controlIcons = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public double WindowSizeRatio {
			get => this.windowSizeRatio;
			set {
				this.windowSizeRatio = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string InstructionPanelWelcomeParagraph {
			get => this.instructionPanelWelconmeParagraph;
			set {
				this.instructionPanelWelconmeParagraph = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public ObservableCollection<SideItem> SideItems {
			get => this.sideItems;
			set {
				this.sideItems = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string HelpTextParagraph {
			get => this.helpTextParagraph;
			set {
				this.helpTextParagraph = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}
	}
}