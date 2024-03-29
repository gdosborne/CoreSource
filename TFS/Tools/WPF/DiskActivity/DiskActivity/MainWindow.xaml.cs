// ***********************************************************************
// Assembly         : DiskActivity
// Author           : Greg
// Created          : 08-21-2015
//
// Last Modified By : Greg
// Last Modified On : 08-26-2015
// ***********************************************************************
// <copyright file="MainWindow.xaml.cs" company="OSoft">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using GregOsborne.MVVMFramework;
using OSoftComponents;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DiskActivity
{
	public partial class MainWindow : Window
	{
		private OSoftComponents.DiskActivity activity = null;
		public MainWindow()
		{
			InitializeComponent();
			activity = new OSoftComponents.DiskActivity();
			activity.ActivitySensed += activity_ActivitySensed;
		}

		void activity_ActivitySensed(object sender, ActivityEventArgs e)
		{
			OnEllipse.Fill = e.IsOn ? Brushes.Red : Brushes.Black;
		}

		public MainWindowView View { get { return LayoutRoot.GetView<MainWindowView>(); } }

		private void Window_Unloaded(object sender, RoutedEventArgs e)
		{
			activity.Dispose();
		}

		private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}

		private void MainWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{

		}

		private void MainWindowView_ExitApplication(object sender, EventArgs e)
		{
			App.Current.Shutdown();
		}

        private void MainWindowView_StopMonitor(object sender, EventArgs e)
        {
            activity.Stop();
        }

        private void MainWindowView_StartMonitor(object sender, EventArgs e)
        {
            activity.Start();
        }
    }
}
