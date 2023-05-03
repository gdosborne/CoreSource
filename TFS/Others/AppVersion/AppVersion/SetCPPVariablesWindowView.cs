namespace GregOsborne.AppVersion
{
	using MVVMFramework;
	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Text;

	public class SetCPPVariablesWindowView : INotifyPropertyChanged
	{
		public SetCPPVariablesWindowView() {
			AllLines = new ObservableCollection<LineSelector>();
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public void UpdateInterface() {
			OKCommand.RaiseCanExecuteChanged();
		}
		public void InitView() {
			UpdateInterface();
		}
		private bool? _DialogResult;
		public bool? DialogResult {
			get { return _DialogResult; }
			set {
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DialogResult"));
			}
		}
		private DelegateCommand _OKCommand = null;
		public DelegateCommand OKCommand {
			get {
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		private void OK(object state) {
			var result = new StringBuilder("(");
			var first = true;
			AllLines.Where(x => x.IsSelected).ToList().ForEach(x =>
			{
				if (!first)
					result.Append(",");
				var varStart = "#define ".Length;
				var varEnd = x.Value.IndexOf(" ", varStart);
				var variableName = x.Value.Substring(varStart, varEnd - varStart);
				var isString = x.Value.IndexOfAny(new char[] { '\"' }) > -1;
				result.AppendFormat("{0}[{1}]", variableName, isString);
				first = false;
			});
			result.Append(")");
			SelectedVariableIdentifier = result.ToString();
			DialogResult = true;
		}
		private bool ValidateOKState(object state) {
			return AllLines.Any(x => x.IsSelected);
		}
		private DelegateCommand _CancelCommand = null;
		public DelegateCommand CancelCommand {
			get {
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		private void Cancel(object state) {
			DialogResult = false;
		}
		private bool ValidateCancelState(object state) {
			return true;
		}
		private string _SelectedVariableIdentifier;
		public string SelectedVariableIdentifier {
			get { return _SelectedVariableIdentifier; }
			set {
				_SelectedVariableIdentifier = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedVariableIdentifier"));
			}
		}

		private string _FileName;
		public string FileName {
			get { return _FileName; }
			set {
				_FileName = value;
				if (!string.IsNullOrEmpty(value)) {
					if (!File.Exists(value))
						throw new FileNotFoundException("Cannot find file", value);
					string dataHolder = null;
					using (var fs = new FileStream(value, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					using (var sr = new StreamReader(fs)) {
						while (sr.Peek() > -1) {
							var line = sr.ReadLine();
							if (line.StartsWith("// VersionData")) {
								dataHolder = line.Replace("// VersionData", string.Empty);
							}
							else {
								var ls = new LineSelector { IsSelected = false, Value = line };
								ls.PropertyChanged += ls_PropertyChanged;
								AllLines.Add(ls);
							}
						}
					}
				}
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
			}
		}

		void ls_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			UpdateInterface();
		}
		private ObservableCollection<LineSelector> _AllLines;
		public ObservableCollection<LineSelector> AllLines {
			get { return _AllLines; }
			set {
				_AllLines = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AllLines"));
			}
		}
	}
}
