using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FormatCode.Classes;
using MVVMFramework;
namespace FormatCode.Windows
{
	public class OptionsWindowView : OptionFlag, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public OptionsWindowView()
		{
			base.PropertyChanged += OptionsWindowView_PropertyChanged;
		}
		void OptionsWindowView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		private ICommand _OKCommand = null;
		public DelegateCommand OKCommand
		{
			get
			{
				if (_OKCommand == null)
					_OKCommand = new DelegateCommand(OK, ValidateOKState);
				return _OKCommand as DelegateCommand;
			}
		}
		private void OK(object state)
		{
			Registry.SetValue<long>("Options", (long)OptionFlags);
			if (CloseRequest != null)
				CloseRequest(this, new CloseEventArgs(true));
		}
		private bool ValidateOKState(object state)
		{
			return true;
		}
		public event CloseHandler CloseRequest;
		private ICommand _CancelCommand = null;
		public DelegateCommand CancelCommand
		{
			get
			{
				if (_CancelCommand == null)
					_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState);
				return _CancelCommand as DelegateCommand;
			}
		}
		private void Cancel(object state)
		{
			if (CloseRequest != null)
				CloseRequest(this, new CloseEventArgs(false));
		}
		private bool ValidateCancelState(object state)
		{
			return true;
		}
		public override void InitializeOptions()
		{
			if (OptionFlags == Classes.Enumerations.OptionFlags.None)
				return;
			var flags = Enum.GetNames(typeof(FormatCode.Classes.Enumerations.OptionFlags)).ToList();
			flags.Remove("None");
			var t = this.GetType();
			flags.ForEach(x =>
			{
				var val = (FormatCode.Classes.Enumerations.OptionFlags)Enum.Parse(typeof(FormatCode.Classes.Enumerations.OptionFlags), x, true);
				if ((OptionFlags & val) == val)
				{
					var p = t.GetProperty(x);
					if (p != null)
						p.SetValue(this, true);
				}
			});
		}
	}
}
