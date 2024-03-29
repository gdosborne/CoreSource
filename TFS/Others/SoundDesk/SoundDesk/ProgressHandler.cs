namespace SoundDesk
{
	using System;
	using System.Windows.Controls;
	using System.Linq;
	using System.IO;

	public class DownloadProgressEventArgs : EventArgs
	{
		public DownloadProgressEventArgs(double value)
		{
			Value = value;
		}
		public double Value { get; private set; }
	}
	public class DownloadHandler
	{
		public delegate void DownloadProgressHandler(object sender, DownloadProgressEventArgs e);
		public event EventHandler DownloadCompleted;
		public event DownloadProgressHandler DownloadProgress;
		public void Start()
		{
			Download();
			if (DownloadCompleted != null)
				DownloadCompleted(this, EventArgs.Empty);
		}
		public string ExePath { get; set; }
		private void Download()
		{
			using (var client = App.GetClient())
			{
				var size = client.GetUpdateSize(App.ApplicationName);
				if (size > 0)
				{
					var lastBlock = false;
					var currentBlock = 0;
					long totalDownloaded = 0;
					var blockCount = client.GetBlockCount(App.ApplicationName);
					var updateDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
					Directory.CreateDirectory(updateDir);
					var downloadFileName = Path.Combine(updateDir, Path.GetFileName(Path.GetTempFileName()));
					using (var fs = new FileStream(downloadFileName, FileMode.CreateNew, FileAccess.Write, FileShare.None))
					using (var bw = new BinaryWriter(fs))
					{
						while (!lastBlock)
						{
							var data = client.GetUpdate(App.ApplicationName, out lastBlock);
							if (data == null)
								break;
							totalDownloaded += data.Length;
							if (DownloadProgress != null)
								DownloadProgress(this, new DownloadProgressEventArgs(totalDownloaded));
							bw.Write(data);
							currentBlock++;
						}
					}
					ExePath = Path.Combine(Path.GetDirectoryName(downloadFileName), App.ApplicationName + ".exe");
					File.Move(downloadFileName, ExePath);
				}
			}
		}
	}
}
