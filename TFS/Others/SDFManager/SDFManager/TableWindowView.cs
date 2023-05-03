namespace SDFManager
{
	using GregOsborne.Application.Primitives;
	using MVVMFramework;
	using SDFManagerSupport;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Data;
	using System.Linq;
	using System.Windows;

	public class TableWindowView : INotifyPropertyChanged
	{
		public TableWindowView()
		{
			
		}
		#region Public Methods
		public void Initialize(Window window)
		{
			
		}
		private bool _IsNewTable;
		public bool IsNewTable
		{
			get { return _IsNewTable; }
			set
			{
				_IsNewTable = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public void InitView()
		{
			if (Definition == null)
			{
				Definition = new TableDefinition("Unknown");
				IsNewTable = true;
			}
		}
		public void Persist(Window window)
		{
		}
		public void UpdateInterface()
		{
		}
		#endregion Public Methods
		
		#region Private Methods
		private void Cancel(object state)
		{
			DialogResult = false;
		}
		private void OK(object state)
		{
			DialogResult = true;
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
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _CancelCommand = null;
		private TableDefinition _Definition;
		private bool? _DialogResult;

		private DelegateCommand _OKCommand = null;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand CancelCommand
		{
			get
			{
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		public TableDefinition Definition
		{
			get
			{
				return _Definition;
			}
			set
			{
				_Definition = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool? DialogResult
		{
			get
			{
				return _DialogResult;
			}
			set
			{
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
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
		#endregion Public Properties
	}
}
