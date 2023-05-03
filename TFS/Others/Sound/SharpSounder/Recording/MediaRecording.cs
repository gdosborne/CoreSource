using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
namespace SharpSounder.Recording
{
	public class MediaRecording : IRecordable
	{
		public event EventHandler LengthUpdated;
		public event EventHandler RecordingComplete;
		public event EventHandler RecordingPaused;
		public event EventHandler RecordingStarted;
		private int InternalLength = 0;
		private Timer LengthTimer = null;
		public MediaRecording()
		{
			LengthTimer = new Timer(1000);
			LengthTimer.Elapsed += new ElapsedEventHandler(LengthTimer_Elapsed);
		}
		public MediaRecording(string fileName)
			: this()
		{
			FileName = fileName;
		}
		public string FileName { get; set; }
		public bool IsComplete { get; private set; }
		public bool IsPaused { get; private set; }
		public bool IsRecording { get; private set; }
		public TimeSpan Length
		{
			get
			{
				return TimeSpan.FromSeconds(InternalLength);
			}
		}
		public void Pause()
		{
			SendCommand("pause recsound");
			if(RecordingPaused != null)
				RecordingPaused(this, EventArgs.Empty);
			IsPaused = true;
		}
		public void Start()
		{
			if(IsPaused)
			{
				SendCommand("resume recsound");
				IsPaused = false;
			}
			else
			{
				if(string.IsNullOrEmpty(FileName))
					throw new ApplicationException("FileName not specified");
				if(new FileInfo(FileName).Extension != ".wav")
					throw new ApplicationException("File must be a wav file");
				var n = 0;
				var baseName = Path.GetFileNameWithoutExtension(FileName);
				var dirName = Path.GetDirectoryName(FileName);
				while(File.Exists(FileName))
				{
					n++;
					FileName = Path.Combine(dirName, string.Format("{0}{1}.wav", baseName, n));
				}
				SendCommand(string.Format("open new Type waveaudio Alias recsound", FileName));
				SendCommand("record recsound");
				if(RecordingStarted != null)
					RecordingStarted(this, EventArgs.Empty);
				LengthTimer.Start();
				IsRecording = true;
			}
		}
		public void Stop()
		{
			LengthTimer.Stop();
			SendCommand("stop recsound");
			SendCommand(string.Format("save recsound \"{0}\"", FileName));
			SendCommand("close recsound");
			if(RecordingComplete != null)
				RecordingComplete(this, EventArgs.Empty);
			IsRecording = false;
			IsComplete = true;
		}
		[DllImport("winmm.dll", EntryPoint = "mciGetErrorStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		private static extern bool mciGetErrorString(int dwError, StringBuilder lpstrBuffer, int uLength);
		[DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
		private static void SendCommand(string command)
		{
			int result = mciSendString(command, string.Empty, 0, 0);
			if(result != 0)
				ThrowError(result, command);
		}
		private static void ThrowError(int result, string command)
		{
			StringBuilder buffer = new StringBuilder(128);
			mciGetErrorString(result, buffer, buffer.Capacity);
			var error = buffer.ToString();
			throw new ApplicationException(string.Format("{0} ({1})", error, command));
		}
		private void LengthTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if(!IsPaused)
			{
				InternalLength++;
				if(LengthUpdated != null)
					LengthUpdated(this, EventArgs.Empty);
			}
		}
	}
}
