using System;
using System.Collections.Generic;
using System.Linq;
namespace ProcessSourceFiles.Classes
{
	public class ProcessThread
	{
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
		public event EventHandler Complete;
		public event ReportProgressEventHandler ReportProgress;
	}
}
