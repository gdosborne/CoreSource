namespace OzDB.Application.IO {
	using System.IO;
	using Microsoft.WindowsAPICodePack.Shell;
	using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

	public static class MetaData {
		public static ShellProperties GetProperties(this FileInfo file) {
			if (!file.Exists) {
				return null;
			}

			var fileObject = ShellObject.FromParsingName(file.FullName);
			if (fileObject != null) {
				return fileObject.Properties;
			}

			return null;
		}
	}
}
