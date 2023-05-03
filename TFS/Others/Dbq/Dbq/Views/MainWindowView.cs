using DbqDatabase;
using GregOsborne.MVVMFramework;
using System.Collections.Generic;

namespace Dbq.Views {
	public class MainWindowView : ViewModelBase {
        public MainWindowView()
        {
            _manager = new DbqConnectionManager();
            DatabaseTypes = _manager.ConnectionTypes;
        }
        private DbqConnectionManager _manager = null;
        private IEnumerable<string> _DatabaseTypes = null;
        public IEnumerable<string> DatabaseTypes {
            get {
                return _DatabaseTypes;
            }
            set {
                _DatabaseTypes = null;
                InvokePropertyChanged(this, "DatabaseTypes");
            }
        }
		private DelegateCommand _NewCommand = null;
		public DelegateCommand NewCommand {
			get {
				if (_NewCommand == null)
					_NewCommand = new DelegateCommand(New, ValidateNewState);
				return _NewCommand as DelegateCommand;
			}
		}
		private void New(object state) {
		}
		private bool ValidateNewState(object state) {
			return true;
		}
		private DelegateCommand _OpenCommand = null;
		public DelegateCommand OpenCommand {
			get {
				if (_OpenCommand == null)
					_OpenCommand = new DelegateCommand(Open, ValidateOpenState);
				return _OpenCommand as DelegateCommand;
			}
		}
		private void Open(object state) {
		}
		private bool ValidateOpenState(object state) {
			return true;
		}
		private DelegateCommand _SaveCommand = null;
		public DelegateCommand SaveCommand {
			get {
				if (_SaveCommand == null)
					_SaveCommand = new DelegateCommand(Save, ValidateSaveState);
				return _SaveCommand as DelegateCommand;
			}
		}
		private void Save(object state) {
		}
		private bool ValidateSaveState(object state) {
			return false;
		}
		private DelegateCommand _SaveAsCommand = null;
		public DelegateCommand SaveAsCommand {
			get {
				if (_SaveAsCommand == null)
					_SaveAsCommand = new DelegateCommand(SaveAs, ValidateSaveAsState);
				return _SaveAsCommand as DelegateCommand;
			}
		}
		private void SaveAs(object state) {
		}
		private bool ValidateSaveAsState(object state) {
			return false;
		}
		private DelegateCommand _UndoCommand = null;
		public DelegateCommand UndoCommand {
			get {
				if (_UndoCommand == null)
					_UndoCommand = new DelegateCommand(Undo, ValidateUndoState);
				return _UndoCommand as DelegateCommand;
			}
		}
		private void Undo(object state) {
		}
		private bool ValidateUndoState(object state) {
			return false;
		}
		private DelegateCommand _RedoCommand = null;
		public DelegateCommand RedoCommand {
			get {
				if (_RedoCommand == null)
					_RedoCommand = new DelegateCommand(Redo, ValidateRedoState);
				return _RedoCommand as DelegateCommand;
			}
		}
		private void Redo(object state) {
		}
		private bool ValidateRedoState(object state) {
			return false;
		}
		private DelegateCommand _CutCommand = null;
		public DelegateCommand CutCommand {
			get {
				if (_CutCommand == null)
					_CutCommand = new DelegateCommand(Cut, ValidateCutState);
				return _CutCommand as DelegateCommand;
			}
		}
		private void Cut(object state) {
		}
		private bool ValidateCutState(object state) {
			return false;
		}
		private DelegateCommand _CopyCommand = null;
		public DelegateCommand CopyCommand {
			get {
				if (_CopyCommand == null)
					_CopyCommand = new DelegateCommand(Copy, ValidateCopyState);
				return _CopyCommand as DelegateCommand;
			}
		}
		private void Copy(object state) {
		}
		private bool ValidateCopyState(object state) {
			return false;
		}
		private DelegateCommand _PasteCommand = null;
		public DelegateCommand PasteCommand {
			get {
				if (_PasteCommand == null)
					_PasteCommand = new DelegateCommand(Paste, ValidatePasteState);
				return _PasteCommand as DelegateCommand;
			}
		}
		private void Paste(object state) {
		}
		private bool ValidatePasteState(object state) {
			return false;
		}
		private DelegateCommand _SettingsCommand = null;
		public DelegateCommand SettingsCommand {
			get {
				if (_SettingsCommand == null)
					_SettingsCommand = new DelegateCommand(Settings, ValidateSettingsState);
				return _SettingsCommand as DelegateCommand;
			}
		}
		private void Settings(object state) {
		}
		private bool ValidateSettingsState(object state) {
			return true;
		}
		private DelegateCommand _PrintCommand = null;
		public DelegateCommand PrintCommand {
			get {
				if (_PrintCommand == null)
					_PrintCommand = new DelegateCommand(Print, ValidatePrintState);
				return _PrintCommand as DelegateCommand;
			}
		}
		private void Print(object state) {
		}
		private bool ValidatePrintState(object state) {
			return false;
		}
		private DelegateCommand _ExitCommand = null;
		public DelegateCommand ExitCommand {
			get {
				if (_ExitCommand == null)
					_ExitCommand = new DelegateCommand(Exit, ValidateExitState);
				return _ExitCommand as DelegateCommand;
			}
		}
		private void Exit(object state) {
		}
		private bool ValidateExitState(object state) {
			return true;
		}
		private DelegateCommand _HelpAboutCommand = null;
		public DelegateCommand HelpAboutCommand {
			get {
				if (_HelpAboutCommand == null)
					_HelpAboutCommand = new DelegateCommand(HelpAbout, ValidateHelpAboutState);
				return _HelpAboutCommand as DelegateCommand;
			}
		}
		private void HelpAbout(object state) {
			//ShowNewView(this, new SplashWindowView());
		}
		private bool ValidateHelpAboutState(object state) {
			return true;
		}
	}
}
