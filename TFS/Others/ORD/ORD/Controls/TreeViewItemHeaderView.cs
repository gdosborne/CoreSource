namespace SNC.Applications.Developer.Controls
{
	using System;
	using System.ComponentModel;
	using System.Windows.Media;

	public class TreeViewItemHeaderView : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void UpdateInterface() {

		}
		public void InitView() {

		}
		private string _HeaderText;
		public string HeaderText {
			get { return _HeaderText; }
			set {
				_HeaderText = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HeaderText"));
			}
		}
		private ImageSource _HeaderImageSource;
		public ImageSource HeaderImageSource {
			get { return _HeaderImageSource; }
			set {
				_HeaderImageSource = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HeaderImageSource"));
			}
		}
	}
}
