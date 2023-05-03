using MyMinistry.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MyMinistry.Views
{
	public class TimeActionPanelView : BindableBase
	{
		#region Private Fields

		private DelegateCommand _StartTimeCommand = null;
		private DelegateCommand _StopTimeCommand = null;

		#endregion Private Fields

		#region Public Properties

		public TimeSpan? CurrentTimeSpan {
			get { return CommonData.MinistrySessionTime; }
			set {
				SetProperty(ref CommonData.MinistrySessionTime, value, "CurrentTimeSpan");
				SetUITime();
			}
		}

		public string CurrentTimeSpanString {
			get { return CommonData.MinistryTimeDisplay; }
			set {
				SetProperty(ref CommonData.MinistryTimeDisplay, value, "CurrentTimeSpanString");
			}
		}

		public DateTime? EndDate {
			get { return CommonData.MinistrySessionEnd; }
			set {
				SetProperty(ref CommonData.MinistrySessionEnd, value, "EndDate");
				UpdateInterface();
			}
		}

		public DateTime? StartDate {
			get { return CommonData.MinistrySessionStart; }
			set {
				SetProperty(ref CommonData.MinistrySessionStart, value, "StartDate");
				UpdateInterface();
			}
		}

		public DelegateCommand StartTimeCommand {
			get {
				if (_StartTimeCommand == null)
					_StartTimeCommand = new DelegateCommand(StartTime, ValidateStartTimeState);
				return _StartTimeCommand as DelegateCommand;
			}
		}

		public DelegateCommand StopTimeCommand {
			get {
				if (_StopTimeCommand == null)
					_StopTimeCommand = new DelegateCommand(StopTime, ValidateStopTimeState);
				return _StopTimeCommand as DelegateCommand;
			}
		}

		#endregion Public Properties

		#region Public Methods

		public void SetUITime()
		{
			if (CommonData.MinistrySessionTime.HasValue)
				CurrentTimeSpanString = string.Format("{0}:{1}:{2}", CommonData.MinistrySessionTime.Value.Hours.ToString("#0"), CommonData.MinistrySessionTime.Value.Minutes.ToString("00"), CommonData.MinistrySessionTime.Value.Seconds.ToString("00"));
			else
				CurrentTimeSpanString = "0:00:00";
		}

		public void UpdateInterface()
		{
			StartTimeCommand.RaiseCanExecuteChanged();
			StopTimeCommand.RaiseCanExecuteChanged();
		}

		#endregion Public Methods

		#region Private Methods

		private void StartTime(object state)
		{
			CommonData.StartMinistryTime(this);
		}

		private void StopTime(object state)
		{
			CommonData.StopMinistryTime();
		}

		private bool ValidateStartTimeState(object state)
		{
			return !StartDate.HasValue;
		}

		private bool ValidateStopTimeState(object state)
		{
			return StartDate.HasValue && !EndDate.HasValue;
		}

		#endregion Private Methods
	}
}
