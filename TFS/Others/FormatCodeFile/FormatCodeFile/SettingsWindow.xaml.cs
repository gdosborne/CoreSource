using System;
using System.Collections.Generic;
using System.Windows;
using GregOsborne.Application.Primitives;
using GregOsborne.MVVMFramework;

namespace FormatCodeFile
{
	public partial class SettingsWindow : Window
	{
		public SettingsWindow()
		{
			InitializeComponent();

            this.DataContext = new SettingsWindowView();

            this.View.PropertyChanged += SettingsWindowView_PropertyChanged;

        }
        public SettingsWindowView View => this.DataContext.As<SettingsWindowView>();

		private void SettingsWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Result")
			{
				DialogResult = View.Result;
			}
		}
	}
}
