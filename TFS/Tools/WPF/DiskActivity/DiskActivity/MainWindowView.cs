using GregOsborne.MVVMFramework;
using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DiskActivity
{
	public class MainWindowView : INotifyPropertyChanged
	{
        public event EventHandler StopMonitor;
        public event EventHandler StartMonitor;
        public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler ExitApplication;
		public void UpdateInterface()
		{

		}
		private RelayCommand _ExitCommand;
		public RelayCommand ExitCommand
		{
			get
			{
				if (_ExitCommand == null)
					_ExitCommand = new RelayCommand(Exit) { IsEnabled = true };
				return _ExitCommand;
			}
		}
		private void Exit()
		{
			if (ExitApplication != null)
				ExitApplication(this, EventArgs.Empty);
		}
        private DelegateCommand _StopMonitorCommand;
        public DelegateCommand StopMonitorCommand => _StopMonitorCommand ?? (_StopMonitorCommand = new DelegateCommand(StopMonitor1, ValidateStopMonitorState));
        private void StopMonitor1(object state)
        {
            StopMonitor?.Invoke(this, EventArgs.Empty);
        }
        private bool ValidateStopMonitorState(object state) => true;
        private DelegateCommand _StartMonitorCommand;
        public DelegateCommand StartMonitorCommand => _StartMonitorCommand ?? (_StartMonitorCommand = new DelegateCommand(StartMonitor1, ValidateStartMonitorState));
        private void StartMonitor1(object state)
        {
            StartMonitor?.Invoke(this, EventArgs.Empty);
        }
        private bool ValidateStartMonitorState(object state) => true;

    }
}
