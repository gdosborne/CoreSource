namespace SoundDesk
{
	using System;
	using System.ComponentModel;
	using System.Windows;
	using MVVMFramework;
	using GregOsborne.Application.Primitives;

	public class ProgressDialogBoxView : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void UpdateInterface()
		{

		}
		public void InitView()
		{

		}
		public void Initialize(Window window)
		{

		}
		public void Persist(Window window)
		{

		}
		private string _Prompt;
		public string Prompt
		{
			get { return _Prompt; }
			set
			{
				_Prompt = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		private bool? _DialogResult;
		public bool? DialogResult
		{
			get { return _DialogResult; }
			set
			{
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
	}
}
