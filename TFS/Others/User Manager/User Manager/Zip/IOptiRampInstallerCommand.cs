using System;
using System.Collections.Generic;

namespace SNC.OptiRamp.Services.fSncZip
{
	public delegate void CommandCompleteHandler(object sender, EventArgs e);
	public delegate void CommandStartedHandler(object sender, EventArgs e);
	public delegate void CommandStatusChangedEventHandler(object sender, CommandStatusChangedEventArgs e);
	public delegate void InitializeProgressHandler(object sender, InitializeProgressEventArgs e);
	public delegate void UpdateProgressHandler(object sender, UpdateProgressEventArgs e);
	public enum CommandResults
	{
		Success,
		Failure
	}
	public interface IOptiRampInstallerCommand
	{
		event CommandCompleteHandler CommandComplete;
		event CommandStartedHandler CommandStarted;
		event CommandStatusChangedEventHandler CommandStatusChanged;
		event InitializeProgressHandler InitializeProgress;
		event UpdateProgressHandler UpdateProgress;
		CommandResults Result { get; }
		object Value { get; }
		void Execute(Dictionary<string, object> parameters);
	}
	public class CommandStatusChangedEventArgs : EventArgs
	{
		public CommandStatusChangedEventArgs(string message)
		{
			Message = message;
		}
		public string Message { get; private set; }
	}
	public class InitializeProgressEventArgs : EventArgs
	{
		public InitializeProgressEventArgs(double maximum)
			: this(maximum, 0)
		{
		}
		public InitializeProgressEventArgs(double maximum, double minimum)
			: this(maximum, minimum, 0)
		{
		}
		public InitializeProgressEventArgs(double maximum, double minimum, double value)
		{
			Maximum = maximum;
			Minimum = minimum;
			Value = value;
		}
		public double Maximum { get; set; }
		public double Minimum { get; set; }
		public double Value { get; set; }
	}
	public class UpdateProgressEventArgs : EventArgs
	{
		public UpdateProgressEventArgs(double value)
		{
			Value = value;
		}
		public double Value { get; set; }
	}
}
