using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Text;
using GregOsborne.Developers.Suite.Configuration;
using GregOsborne.MVVMFramework;

namespace GregOsborne.Developers.Suite {
	public class AboutWindowView : ViewModelBase {
		public AboutWindowView() {
			this.Title = $"About Window";
			this.Version = new Version(1, 0, 0, 0).ToString();
			this.AppName = "Some application";
			this.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec rutrum magna vitae tempor rhoncus. Curabitur tempor enim nec porttitor consectetur";
			this.Copyright = "Copyright © Greg Osborne 2020\nAll rights reserved";
		}

		public override void Initialize() {
			this.Title = $"About {App.Current.As<App>().ApplicationName}";
			this.Version = this.GetType().Assembly.GetName().Version.ToString();
			this.AppName = App.Current.As<App>().ApplicationName;
			this.Description = this.GetType().Assembly.GetAssemblyAttributeValue<AssemblyDescriptionAttribute>("Description");
			this.Copyright = this.GetType().Assembly.GetAssemblyAttributeValue<AssemblyCopyrightAttribute>("Copyright");

			this.Assemblies = new ObservableCollection<AssemblyInformation>(App.Current.As<App>().Assemblies.OrderBy(x => x.AssemblyName));
			this.Extensions = new ObservableCollection<AssemblyInformation>(App.Current.As<App>().Extensions.OrderBy(x => x.AssemblyName));
		}

		private string title = default;
		public string Title {
			get => this.title;
			set {
				this.title = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string version = default;
		public string Version {
			get => this.version;
			set {
				this.version = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string appName = default;
		public string AppName {
			get => this.appName;
			set {
				this.appName = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string description = default;
		public string Description {
			get => this.description;
			set {
				this.description = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string copyright = default;
		public string Copyright {
			get => this.copyright;
			set {
				this.copyright = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private ObservableCollection<AssemblyInformation> assemblies = default;
		public ObservableCollection<AssemblyInformation> Assemblies {
			get => this.assemblies;
			set {
				this.assemblies = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private ObservableCollection<AssemblyInformation> extensions = default;
		public ObservableCollection<AssemblyInformation> Extensions {
			get => this.extensions;
			set {
				this.extensions = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public event ExecuteUiActionHandler ExecuteUiAction;
		private DelegateCommand cancelCommand = default;
		public DelegateCommand CancelCommand => this.cancelCommand ?? (this.cancelCommand = new DelegateCommand(this.Cancel, this.ValidateCancelState));
		private bool ValidateCancelState(object state) => true;
		private void Cancel(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("CloseAboutBox"));

		private DelegateCommand sysInfoCommand = default;
		public DelegateCommand SysInfoCommand => this.sysInfoCommand ?? (this.sysInfoCommand = new DelegateCommand(this.SysInfo, this.ValidateSysInfoState));
		private bool ValidateSysInfoState(object state) => true;
		private void SysInfo(object state) {
			ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("DisplaySystemInfoBox"));
			ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("CloseAboutBox"));
		}

		private DelegateCommand copyToClipboardCommand = default;
		public DelegateCommand CopyToClipboardCommand => this.copyToClipboardCommand ?? (this.copyToClipboardCommand = new DelegateCommand(this.CopyToClipboard, this.ValidateCopyToClipboardState));
		private bool ValidateCopyToClipboardState(object state) => true;
		private void CopyToClipboard(object state) {
			var sb = new StringBuilder();
			sb.AppendLine(this.AppName);
			sb.AppendLine($"Version {this.Version}");
			sb.AppendLine(new string('-', 60));
			sb.AppendLine(this.Description.WrapLongString(60));
			sb.AppendLine();

			sb.AppendLine("Assemblies");
			this.Assemblies.ToList().ForEach(x => sb.AppendLine(x.ToString()));
			sb.AppendLine();

			sb.AppendLine("Extensions");
			this.Extensions.ToList().ForEach(x => sb.AppendLine(x.ToString()));
			sb.AppendLine();

			sb.AppendLine(this.Copyright);
			Clipboard.SetText(sb.ToString());
		}
	}
}
