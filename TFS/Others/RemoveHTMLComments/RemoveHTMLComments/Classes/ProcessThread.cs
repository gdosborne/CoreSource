namespace ProcessSourceFiles.Classes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class ProcessThread
	{
		#region Public Methods
		public void Start(object p)
		{
			ProcessParameters px = (ProcessParameters)p;
			int progress = 0;
			if (px.Files != null)
			{
				px.Files.ForEach(x =>
				{
					progress++;
					if (ReportProgress != null)
						ReportProgress(this, new ReportProgressEventArgs(progress, x.FileName));
					x.Process(px);
				});
			}
			if (Complete != null)
				Complete(this, EventArgs.Empty);
		}
		#endregion Public Methods

		#region Public Events
		public event EventHandler Complete;
		public event ReportProgressEventHandler ReportProgress;
		#endregion Public Events
	}
}
