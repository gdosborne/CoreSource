namespace OSInstallerBuilder.Windows
{
	using GregOsborne.MVVMFramework;
	using GregOsborne.Application.Primitives;
	using OSInstallerBuilder.Classes.Options;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows.Controls;

	public class OptionsWindowView : ViewModelBase, INotifyPropertyChanged
	{
		#region Public Methods
		public override void UpdateInterface()
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
			OptionCategories.ToList().ForEach(x =>
			{
				x.Pages.ToList().ForEach(y =>
				{
					y.Groups.ToList().ForEach(z =>
					{
						z.Items.ToList().ForEach(item =>
						{
							if (item.Value != null)
								item.UpdateValue();
						});
					});
				});
			});
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
		public event ExecuteUiActionHandler ExecuteUIAction;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _CancelCommand = null;
		private bool? _DialogResult;
		private IList<GroupBox> _GroupsList;
		private DelegateCommand _OKCommand = null;
		private OptionList _OptionCategories;
		private NamedItem _SelectedTreeItem;
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
					PropertyChanged(this, new PropertyChangedEventArgs("DialogResult"));
			}
		}
		public IList<GroupBox> GroupsList
		{
			get
			{
				return _GroupsList;
			}
			set
			{
				_GroupsList = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("GroupsList"));
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
		public OptionList OptionCategories
		{
			get
			{
				return _OptionCategories;
			}
			set
			{
				_OptionCategories = value;
				if (SelectedTreeItem == null && _OptionCategories.Any())
					SelectedTreeItem = _OptionCategories.FirstOrDefault();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("OptionCategories"));
			}
		}

		public NamedItem SelectedTreeItem
		{
			get
			{
				return _SelectedTreeItem;
			}
			set
			{
				_SelectedTreeItem = value;
				OSInstallerBuilder.Classes.Options.Page thePage = null;
				if (_SelectedTreeItem is Category)
					thePage = _SelectedTreeItem.As<Category>().Pages.ToList().FirstOrDefault(x => x.Name.Equals(_SelectedTreeItem.Name));
				else if (_SelectedTreeItem is OSInstallerBuilder.Classes.Options.Page)
					thePage = _SelectedTreeItem.As<OSInstallerBuilder.Classes.Options.Page>();
				GroupsList = null;
				if (thePage != null)
				{
					var temp = new List<GroupBox>();
					thePage.Groups.ToList().ForEach(x =>
					{
						temp.Add(x.GetControl());
					});
					GroupsList = temp;
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SelectedTreeItem"));
			}
		}
		#endregion Public Properties
	}
}
