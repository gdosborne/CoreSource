namespace AppSystem.IO {
	using System;
	using System.Diagnostics;
	using System.Threading.Tasks;
	using AppSystem.Primitives;
	using Windows.Storage;

	public static class ExtensionMethods {
		public static async Task<ulong> Size(this StorageFile file) {
			var basicProperties = await file.GetBasicPropertiesAsync();
			return basicProperties.Size;
		}

		public static async Task<bool> IsItemPresentAsync(this StorageFolder folder, string name) {
			if (folder == null) {
				throw new ArgumentException("Folder is required", "folder");
			}
			if (string.IsNullOrWhiteSpace(name)) {
				throw new ArgumentException("Name is required", "name");
			}
			try {
				var item = await folder.TryGetItemAsync(name);
				return item != null;
			}
			catch (Exception ex) {
				Debug.WriteLine(ex);
				return false;
			}
		}

		public static async Task<StorageFolder> GetSubFolderAsync(this StorageFolder folder, string name, bool isCreateWhenMissing) {
			if (folder == null) {
				throw new ArgumentException("Folder is required", "folder");
			}
			if (string.IsNullOrWhiteSpace(name)) {
				throw new ArgumentException("Name is required", "name");
			}
			return !await folder.IsItemPresentAsync(name)
				? await folder.CreateFolderAsync(name)
				: await folder.GetFolderAsync(name);

		}
	}
}
