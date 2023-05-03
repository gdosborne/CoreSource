namespace OSControls.ChildWindows
{
	using GregOsborne.MVVMFramework;
	using OSControls.Classes;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	public class ValueEditorView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Methods
		public override void UpdateInterface()
		{
		}
		#endregion Public Methods

		#region Private Methods
		private void Cancel(object state)
		{
			ExecuteUIAction?.Invoke(this, new ExecuteUiActionEventArgs("CloseWindow", new Dictionary<string, object> { { "DialogResult", false } }));
		}
		private void OK(object state)
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUiActionEventArgs("CloseWindow", new Dictionary<string, object> { { "DialogResult", true } }));
		}
		private bool ValidateCancelState(object state)
		{
			return true;
		}
		private bool ValidateOKState(object state)
		{
			return true;
		}
		#endregion Private Methods

		#region Public Events
		public event ExecuteUiActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private List<string> _AllVariableNames;
		private List<string> _AllVariableValues;
		private DelegateCommand _CancelCommand = null;
		private DelegateCommand _OKCommand = null;
		private string _TheExpandedValue;
		private string _TheValue;
		private string _VariableTrigger;
		#endregion Private Fields

		#region Public Properties
		public List<string> AllVariableNames
		{
			get
			{
				return _AllVariableNames;
			}
			set
			{
				_AllVariableNames = value;
				if (AllVariableValues != null && AllVariableNames != null && !string.IsNullOrEmpty(VariableTrigger))
					TheExpandedValue = TheValue.ExpandString(AllVariableNames, AllVariableValues, VariableTrigger.ToCharArray()[0]);
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AllVariableNames"));
			}
		}
		public List<string> AllVariableValues
		{
			get
			{
				return _AllVariableValues;
			}
			set
			{
				_AllVariableValues = value;
				if (AllVariableValues != null && AllVariableNames != null && !string.IsNullOrEmpty(VariableTrigger))
					TheExpandedValue = TheValue.ExpandString(AllVariableNames, AllVariableValues, VariableTrigger.ToCharArray()[0]);
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("AllVariableValues"));
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
		public DelegateCommand OKCommand
		{
			get
			{
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		public string TheExpandedValue
		{
			get
			{
				return _TheExpandedValue;
			}
			set
			{
				_TheExpandedValue = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TheExpandedValue"));
			}
		}
		public string TheValue
		{
			get
			{
				return _TheValue;
			}
			set
			{
				_TheValue = value;
				if (AllVariableValues != null && AllVariableNames != null && !string.IsNullOrEmpty(VariableTrigger))
					TheExpandedValue = value.ExpandString(AllVariableNames, AllVariableValues, VariableTrigger.ToCharArray()[0]);

				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TheValue"));
			}
		}
		public string VariableTrigger
		{
			get
			{
				return _VariableTrigger;
			}
			set
			{
				_VariableTrigger = value;
				if (AllVariableValues != null && AllVariableNames != null && !string.IsNullOrEmpty(VariableTrigger))
					TheExpandedValue = TheValue.ExpandString(AllVariableNames, AllVariableValues, VariableTrigger.ToCharArray()[0]);
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("VariableTrigger"));
			}
		}
		#endregion Public Properties
	}
}
