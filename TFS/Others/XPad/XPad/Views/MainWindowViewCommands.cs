namespace XPad.Views
{
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Xml.Linq;

	public partial class MainWindowView
	{
		private DelegateCommand _SaveAsCommand = null;
		private DelegateCommand _NewCommand = null;
		private DelegateCommand _PropertiesCommand = null;
		private DelegateCommand _CloseErrorCommand = null;
		private DelegateCommand _RenameCommand = null;
		private DelegateCommand _OpenCommand = null;
		private DelegateCommand _SaveCommand = null;
		private DelegateCommand _ExitCommand = null;
		private DelegateCommand _RedoCommand = null;
		private DelegateCommand _UndoCommand = null;
		private DelegateCommand _CutCommand = null;
		private DelegateCommand _CopyCommand = null;
		private DelegateCommand _PasteCommand = null;
		private DelegateCommand _HelpCommand = null;
		private DelegateCommand _AboutCommand = null;
		private DelegateCommand _OptionsCommand = null;
		private DelegateCommand _AddElementCommand = null;
		private DelegateCommand _RemoveElementCommand = null;
		private DelegateCommand _AddAttributeCommand = null;
		private DelegateCommand _RemoveAttributeCommand = null;

		public DelegateCommand CloseErrorCommand
		{
			get
			{
				if (_CloseErrorCommand == null)
					_CloseErrorCommand = new DelegateCommand(CloseError, ValidateCloseErrorState);
				return _CloseErrorCommand as DelegateCommand;
			}
		}
		private void CloseError(object state)
		{
			ErrorVisibility = Visibility.Collapsed;
		}
		private bool ValidateCloseErrorState(object state)
		{
			return true;
		}

		public DelegateCommand PropertiesCommand
		{
			get
			{
				if (_PropertiesCommand == null)
					_PropertiesCommand = new DelegateCommand(Properties, ValidatePropertiesState);
				return _PropertiesCommand as DelegateCommand;
			}
		}
		private void Properties(object state)
		{

		}
		private bool ValidatePropertiesState(object state)
		{
			return true;
		}
		public DelegateCommand RenameCommand
		{
			get
			{
				if (_RenameCommand == null)
					_RenameCommand = new DelegateCommand(Rename, ValidateRenameState);
				return _RenameCommand as DelegateCommand;
			}
		}
		private void Rename(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("RenameElement", null));
		}
		private bool ValidateRenameState(object state)
		{
			return Document != null && SelectedElement != null;
		}

		public DelegateCommand NewCommand
		{
			get
			{
				if (_NewCommand == null)
					_NewCommand = new DelegateCommand(New, ValidateNewState);
				return _NewCommand as DelegateCommand;
			}
		}
		private void New(object state)
		{
			if (Document != null && DocumentHasChanges)
			{

			}
			Document = new XDocument(new XElement("data"));
			Document.Declaration = new XDeclaration("1.0", "utf-8", "yes");
			RefreshTree();
		}
		private bool ValidateNewState(object state)
		{
			return true;
		}
		public DelegateCommand OpenCommand
		{
			get
			{
				if (_OpenCommand == null)
					_OpenCommand = new DelegateCommand(Open, ValidateOpenState);
				return _OpenCommand as DelegateCommand;
			}
		}
		private void Open(object state)
		{
			if (ExecuteUIAction != null)
			{
				var p = new Dictionary<string, object>
				{
					{ "result", false },
					{ "filename", string.Empty },
					{ "lastdirectory", XPad.App.GetSetting<string>("XPad", "Settings", "LastDirectory", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) }
				};
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("OpenXmlFile", p));
				if (!(bool)p["result"])
					return;
				try
				{
					Document = XDocument.Load((string)p["filename"]);
					FileName = (string)p["filename"];
					XPad.App.SetSetting<string>("XPad", "Settings", "LastDirectory", System.IO.Path.GetDirectoryName(FileName));
					RefreshTree();
				}
				catch (Exception ex)
				{
					DisplayError(ex.Message);
					Document = null;
					FileName = null;
				}
			}
		}
		
		private bool ValidateOpenState(object state)
		{
			return true;
		}
		public DelegateCommand SaveCommand
		{
			get
			{
				if (_SaveCommand == null)
					_SaveCommand = new DelegateCommand(Save, ValidateSaveState);
				return _SaveCommand as DelegateCommand;
			}
		}
		private void Save(object state)
		{

		}
		private bool ValidateSaveState(object state)
		{
			return Document != null && DocumentHasChanges;
		}
		public DelegateCommand SaveAsCommand
		{
			get
			{
				if (_SaveAsCommand == null)
					_SaveAsCommand = new DelegateCommand(SaveAs, ValidateSaveAsState);
				return _SaveAsCommand as DelegateCommand;
			}
		}
		private void SaveAs(object state)
		{

		}
		private bool ValidateSaveAsState(object state)
		{
			return Document != null;
		}
		public DelegateCommand ExitCommand
		{
			get
			{
				if (_ExitCommand == null)
					_ExitCommand = new DelegateCommand(Exit, ValidateExitState);
				return _ExitCommand as DelegateCommand;
			}
		}
		private void Exit(object state)
		{

		}
		private bool ValidateExitState(object state)
		{
			return true;
		}
		public DelegateCommand RedoCommand
		{
			get
			{
				if (_RedoCommand == null)
					_RedoCommand = new DelegateCommand(Redo, ValidateRedoState);
				return _RedoCommand as DelegateCommand;
			}
		}
		private void Redo(object state)
		{

		}
		private bool ValidateRedoState(object state)
		{
			return false;
		}
		public DelegateCommand UndoCommand
		{
			get
			{
				if (_UndoCommand == null)
					_UndoCommand = new DelegateCommand(Undo, ValidateUndoState);
				return _UndoCommand as DelegateCommand;
			}
		}
		private void Undo(object state)
		{

		}
		private bool ValidateUndoState(object state)
		{
			return false;
		}
		public DelegateCommand CutCommand
		{
			get
			{
				if (_CutCommand == null)
					_CutCommand = new DelegateCommand(Cut, ValidateCutState);
				return _CutCommand as DelegateCommand;
			}
		}
		private void Cut(object state)
		{

		}
		private bool ValidateCutState(object state)
		{
			return false;
		}
		public DelegateCommand CopyCommand
		{
			get
			{
				if (_CopyCommand == null)
					_CopyCommand = new DelegateCommand(Copy, ValidateCopyState);
				return _CopyCommand as DelegateCommand;
			}
		}
		private void Copy(object state)
		{

		}
		private bool ValidateCopyState(object state)
		{
			return false;
		}
		public DelegateCommand PasteCommand
		{
			get
			{
				if (_PasteCommand == null)
					_PasteCommand = new DelegateCommand(Paste, ValidatePasteState);
				return _PasteCommand as DelegateCommand;
			}
		}
		private void Paste(object state)
		{

		}
		private bool ValidatePasteState(object state)
		{
			return false;
		}
		public DelegateCommand HelpCommand
		{
			get
			{
				if (_HelpCommand == null)
					_HelpCommand = new DelegateCommand(Help, ValidateHelpState);
				return _HelpCommand as DelegateCommand;
			}
		}
		private void Help(object state)
		{

		}
		private bool ValidateHelpState(object state)
		{
			return false;
		}
		public DelegateCommand AboutCommand
		{
			get
			{
				if (_AboutCommand == null)
					_AboutCommand = new DelegateCommand(About, ValidateAboutState);
				return _AboutCommand as DelegateCommand;
			}
		}
		private void About(object state)
		{

		}
		private bool ValidateAboutState(object state)
		{
			return false;
		}
		public DelegateCommand OptionsCommand
		{
			get
			{
				if (_OptionsCommand == null)
					_OptionsCommand = new DelegateCommand(Options, ValidateOptionsState);
				return _OptionsCommand as DelegateCommand;
			}
		}
		private void Options(object state)
		{
			if (ExecuteUIAction != null)
			{
				var p = new Dictionary<string, object>
				{
					{ "result", false }
				};
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowOptions", p));
				if (!(bool)p["result"])
					return;
				try
				{

				}
				catch (Exception ex)
				{
					Document = null;
					FileName = null;
				}
			}
		}
		private bool ValidateOptionsState(object state)
		{
			return true;
		}
		public DelegateCommand AddElementCommand
		{
			get
			{
				if (_AddElementCommand == null)
					_AddElementCommand = new DelegateCommand(AddElement, ValidateAddElementState);
				return _AddElementCommand as DelegateCommand;
			}
		}
		private void AddElement(object state)
		{

		}
		private bool ValidateAddElementState(object state)
		{
			return Document != null && SelectedElement != null;
		}
		public DelegateCommand RemoveElementCommand
		{
			get
			{
				if (_RemoveElementCommand == null)
					_RemoveElementCommand = new DelegateCommand(RemoveElement, ValidateRemoveElementState);
				return _RemoveElementCommand as DelegateCommand;
			}
		}
		private void RemoveElement(object state)
		{

		}
		private bool ValidateRemoveElementState(object state)
		{
			return Document != null && SelectedElement != null;
		}
		public DelegateCommand AddAttributeCommand
		{
			get
			{
				if (_AddAttributeCommand == null)
					_AddAttributeCommand = new DelegateCommand(AddAttribute, ValidateAddAttributeState);
				return _AddAttributeCommand as DelegateCommand;
			}
		}
		private void AddAttribute(object state)
		{

		}
		private bool ValidateAddAttributeState(object state)
		{
			return Document != null && SelectedElement != null;
		}
		public DelegateCommand RemoveAttributeCommand
		{
			get
			{
				if (_RemoveAttributeCommand == null)
					_RemoveAttributeCommand = new DelegateCommand(RemoveAttribute, ValidateRemoveAttributeState);
				return _RemoveAttributeCommand as DelegateCommand;
			}
		}
		private void RemoveAttribute(object state)
		{

		}
		private bool ValidateRemoveAttributeState(object state)
		{
			return Document != null && SelectedAttribute != null;
		}
		public void UpdateInterface()
		{
			NewCommand.RaiseCanExecuteChanged();
			OpenCommand.RaiseCanExecuteChanged();
			SaveCommand.RaiseCanExecuteChanged();
			SaveAsCommand.RaiseCanExecuteChanged();
			ExitCommand.RaiseCanExecuteChanged();
			UndoCommand.RaiseCanExecuteChanged();
			RedoCommand.RaiseCanExecuteChanged();
			CutCommand.RaiseCanExecuteChanged();
			CopyCommand.RaiseCanExecuteChanged();
			PasteCommand.RaiseCanExecuteChanged();
			OptionsCommand.RaiseCanExecuteChanged();
			AddElementCommand.RaiseCanExecuteChanged();
			AddAttributeCommand.RaiseCanExecuteChanged();
			RemoveElementCommand.RaiseCanExecuteChanged();
			RemoveAttributeCommand.RaiseCanExecuteChanged();
			AboutCommand.RaiseCanExecuteChanged();
			HelpCommand.RaiseCanExecuteChanged();
			PropertiesCommand.RaiseCanExecuteChanged();
			RenameCommand.RaiseCanExecuteChanged();
			CloseErrorCommand.RaiseCanExecuteChanged();
		}
	}
}
