using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MVVMFramework;

namespace OptiRampDesktop.Views
{
	public class PropertiesWindowView : INotifyPropertyChanged
	{
		#region Private Fields
		private ICommand _CancelCommand = null;

		private ImageSource _IconImageSource;

		private ICommand _OKCommand = null;

		private OptiRampControls.Classes.Properties _Properties;

		private string _TypeName;
		#endregion

		#region Public Constructors

		public PropertiesWindowView()
		{
			IconImageSource = new BitmapImage(new Uri("pack://application:,,,/OptiRampDesktop;component/Images/gear1.png"));
		}

		#endregion

		#region Public Events
		public event EventHandler CancelRequest;
		public event EventHandler OKRequest;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

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
		public ImageSource IconImageSource
		{
			get { return _IconImageSource; }
			set
			{
				_IconImageSource = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IconImageSource"));
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
		public OptiRampControls.Classes.Properties Properties
		{
			get { return _Properties; }
			set
			{
				_Properties = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Properties"));
			}
		}
		public string TypeName
		{
			get { return _TypeName; }
			set
			{
				_TypeName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TypeName"));
			}
		}
		#endregion

		#region Private Methods

		private void Cancel(object state)
		{
			if (CancelRequest != null)
				CancelRequest(this, EventArgs.Empty);
		}

		private void OK(object state)
		{
			if (OKRequest != null)
				OKRequest(this, EventArgs.Empty);
		}

		private bool ValidateCancelState(object state)
		{
			return true;
		}

		private bool ValidateOKState(object state)
		{
			return true;
		}

		#endregion
	}
}