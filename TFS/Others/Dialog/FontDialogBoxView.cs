namespace GregOsborne.Dialog
{
	using MVVMFramework;
	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Media;
	using GregOsborne.Application.Primitives;

	public class FontDialogBoxView : INotifyPropertyChanged
	{
		#region Public Constructors
		public FontDialogBoxView()
		{
			FontSizes = new ObservableCollection<double>();
			FontStyles = new ObservableCollection<FontStyle>();
			FontWeights = new ObservableCollection<FontWeight>();
			Fonts = new ObservableCollection<FontFamily>();
		}
		#endregion Public Constructors

		#region Public Methods
		public void Initialize(Window window)
		{
		}
		public void InitView()
		{
			foreach (var f in System.Windows.Media.Fonts.SystemFontFamilies.OrderBy(x => x.Source))
			{
				Fonts.Add(f);
			}
			for (double x = 6; x < 73; x++)
			{
				FontSizes.Add(x);
			}
			FontStyles.Add(System.Windows.FontStyles.Normal);
			FontStyles.Add(System.Windows.FontStyles.Italic);
			FontStyles.Add(System.Windows.FontStyles.Oblique);
			FontWeights.Add(System.Windows.FontWeights.Black);
			FontWeights.Add(System.Windows.FontWeights.Bold);
			FontWeights.Add(System.Windows.FontWeights.DemiBold);
			FontWeights.Add(System.Windows.FontWeights.ExtraBlack);
			FontWeights.Add(System.Windows.FontWeights.ExtraBold);
			FontWeights.Add(System.Windows.FontWeights.ExtraLight);
			FontWeights.Add(System.Windows.FontWeights.Heavy);
			FontWeights.Add(System.Windows.FontWeights.Light);
			FontWeights.Add(System.Windows.FontWeights.Medium);
			FontWeights.Add(System.Windows.FontWeights.Normal);
			FontWeights.Add(System.Windows.FontWeights.Regular);
			FontWeights.Add(System.Windows.FontWeights.SemiBold);
			FontWeights.Add(System.Windows.FontWeights.Thin);
			FontWeights.Add(System.Windows.FontWeights.UltraBlack);
			FontWeights.Add(System.Windows.FontWeights.UltraBold);
			FontWeights.Add(System.Windows.FontWeights.UltraLight);
		}
		public void Persist(Window window)
		{
		}
		public void UpdateInterface()
		{
			OKCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private bool? _DialogResult;
		private ObservableCollection<FontFamily> _Fonts;
		private ObservableCollection<double> _FontSizes;
		private ObservableCollection<FontStyle> _FontStyles;
		private ObservableCollection<FontWeight> _FontWeights;
		private FontFamily _SelectedFontFamily;
		private double _SelectedFontSize;
		private FontStyle _SelectedFontStyle;
		private FontWeight _SelectedFontWeight;
		private Visibility _SizeVisibility;
		private Visibility _StyleVisibility;
		private Visibility _WeightVisibility;
		#endregion Private Fields

		#region Public Properties
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
		public ObservableCollection<FontFamily> Fonts
		{
			get
			{
				return _Fonts;
			}
			set
			{
				_Fonts = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public ObservableCollection<double> FontSizes
		{
			get
			{
				return _FontSizes;
			}
			set
			{
				_FontSizes = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public ObservableCollection<FontStyle> FontStyles
		{
			get
			{
				return _FontStyles;
			}
			set
			{
				_FontStyles = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public ObservableCollection<FontWeight> FontWeights
		{
			get
			{
				return _FontWeights;
			}
			set
			{
				_FontWeights = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public FontFamily SelectedFontFamily
		{
			get
			{
				return _SelectedFontFamily;
			}
			set
			{
				_SelectedFontFamily = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public double SelectedFontSize
		{
			get
			{
				return _SelectedFontSize;
			}
			set
			{
				_SelectedFontSize = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public FontStyle SelectedFontStyle
		{
			get
			{
				return _SelectedFontStyle;
			}
			set
			{
				_SelectedFontStyle = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public FontWeight SelectedFontWeight
		{
			get
			{
				return _SelectedFontWeight;
			}
			set
			{
				_SelectedFontWeight = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Visibility SizeVisibility
		{
			get
			{
				return _SizeVisibility;
			}
			set
			{
				_SizeVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Visibility StyleVisibility
		{
			get
			{
				return _StyleVisibility;
			}
			set
			{
				_StyleVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Visibility WeightVisibility
		{
			get
			{
				return _WeightVisibility;
			}
			set
			{
				_WeightVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		#endregion Public Properties
		private DelegateCommand _OKCommand = null;
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
			DialogResult = true;
		}
		private bool ValidateOKState(object state)
		{
			return SelectedFontFamily != null && SelectedFontSize > 0;
		}
		private DelegateCommand _CancelCommand = null;
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
			DialogResult = false;
		}
		private bool ValidateCancelState(object state)
		{
			return true;
		}

	}
}
