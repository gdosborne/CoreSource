namespace VersionMaster {
	public class AppSingleton {
		public AppSingleton() => triggerFileName = Path.Combine(Path.GetTempPath(), "trigger.tmp");

		private readonly string triggerFileName = default;
		public void WaitForPreviousTermination() {
			var st = Task.Factory.StartNew(() => {
				if (!File.Exists(triggerFileName)) {
					return;
				} else {
					var fil = new FileInfo(triggerFileName);
					if (DateTime.Now.Subtract(fil.LastWriteTime).TotalSeconds > 10)
						return;
				}
				System.Threading.Thread.Sleep(100);
			});
			st.Wait();
			CreateTriggerFile();
		}

		private void CreateTriggerFile() {
			if (File.Exists(triggerFileName)) {
				return;
			}

			using (var fs = new FileStream(triggerFileName, FileMode.Create, FileAccess.Write, FileShare.None))
			using (var sw = new StreamWriter(fs)) {
				sw.Write($"In use {DateTime.Now}");
			}
		}

		public void RemoveTriggerFile() {
			if (File.Exists(triggerFileName)) {
				File.Delete(triggerFileName);
			}
		}
	}
}
