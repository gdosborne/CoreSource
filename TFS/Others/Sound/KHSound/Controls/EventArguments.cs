using System;
using System.Linq;
namespace KHSound.Controls
{
	public delegate void RecordingCompleteHandler(object sender, RecordingCompleteEventArgs e);
	public class RecordingCompleteEventArgs : EventArgs
	{
		public RecordingCompleteEventArgs(int sequenceNumber)
		{
			SequenceNumber = sequenceNumber;
		}
		public int SequenceNumber { get; private set; }
	}
}
