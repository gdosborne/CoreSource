using System;
using System.Windows;
using Dbq.Views;
using GregOsborne.Application.Primitives;

namespace Dbq {
	public partial class AboutBoxWindow : Window {
		public AboutBoxWindow() {
			InitializeComponent();			
		}

		private void AboutBoxWindow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName.Equals("DialogResult"))
				this.DialogResult = sender.As<SplashWindowView>().DialogResult;
		}

		protected override void OnSourceInitialized(EventArgs e) {
			base.OnSourceInitialized(e);
			//this.HideControlBox();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			//this.DataContext.As<SplashWindowView>().PropertyChanged += AboutBoxWindow_PropertyChanged;
		}
	}
}
