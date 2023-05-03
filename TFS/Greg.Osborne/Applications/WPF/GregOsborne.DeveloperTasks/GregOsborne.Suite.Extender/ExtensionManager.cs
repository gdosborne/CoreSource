namespace GregOsborne.Suite.Extender {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Windows.Controls;
	using GregOsborne.Application.Primitives;

	public sealed class ExtensionManager {
		public ExtensionManager() {
			this.extensions = new List<IExtender>();
			this.AssemblyFileNamesToDeleteOnShutdown = new List<string>();
		}

		public event ExtensionAddedHandler ExtensionAdded;

		public List<IExtender> RemoveExtension(IExtender extension, bool isPermanent) {
			if (extension == null) {
				throw new ArgumentException("Missing extension");
			}
			var toRemove = new List<IExtender>();
			foreach (var ext in this.extensions) {
				if (ext == extension) {
					var fName = ext.AssemblyFilename;
					var allExts = this.GetAllExtensions(fName).Select(x => x.Title).ToList();
					var exts = this.extensions.Where(x => allExts.Contains(x.Title));
					foreach (var e in exts) {
						if (e.TabItem != null) {
							if (e.TabItem.Parent is TabControl tc) {
								tc.Items.Remove(e.TabItem);
								toRemove.Add(e);
								break;
							}
						}
					}
				}
			}
			for (var i = 0; i < toRemove.Count; i++) {
				this.extensions.Remove(toRemove[i]);
				if (isPermanent) {
					this.AssemblyFileNamesToDeleteOnShutdown.Add(toRemove[i].AssemblyFilename);
				}
			}
			return toRemove;
		}

		public List<string> AssemblyFileNamesToDeleteOnShutdown { get; private set; }

		public void AddExtension(string assemblyFileName) => this.AddExtension(assemblyFileName, false);

		public void AddExtension(string assemblyFileName, bool throwExceptionOnExtensionNotFound) {
			if (!File.Exists(assemblyFileName)) {
				throw new FileNotFoundException("Assembly file not found", assemblyFileName);
			}

			var allExts = this.GetAllExtensions(assemblyFileName);
			this.extensions.AddRange(allExts);
			allExts.ForEach(x => {
				this.ExtensionAdded?.Invoke(this, new ExtensionAddedEventArgs(x));
			});
			if (!allExts.Any() && throwExceptionOnExtensionNotFound) {
				throw new ApplicationException($"No new extensions found in {assemblyFileName}.");
			}
		}

		private List<IExtender> GetAllExtensions(string assemblyFileName) {
			var result = new List<IExtender>();
			var assy = Assembly.LoadFile(assemblyFileName);
			var types = assy.GetTypes().Where(x => x.GetConstructors().Length > 0).ToArray();
			types = types.Where(x => x.GetConstructors().Any(y => !y.ContainsGenericParameters)).ToArray();
			types = types.Where(x => x.GetConstructors().Any(y => y.GetParameters().Count() == 0)).ToArray();
			types.ToList().ForEach(type => {
				var instance = Activator.CreateInstance(type);
				var extensions = type.GetInterfaces().Where(x => x.Name == "IExtender");
				if (extensions.Any()) {
					extensions.ToList().ForEach(ext => {
						if (!result.Any(x => x.Title == instance.As<IExtender>().Title)) {
							instance.As<IExtender>().AssemblyFilename = assemblyFileName;
							result.Add(instance.As<IExtender>());
						}
					});
				}
			});
			return result;
		}

		private int currentIndex = 0;
		public void BeginRead() => this.currentIndex = 0;
		public bool HasNextExtension {
			get {
				var result = this.extensions.Count > this.currentIndex;
				if (!result) {
					//end of extensions - reset
					this.BeginRead();
				}
				return result;
			}
		}

		public IExtender GetNextExtension() {
			if (this.HasNextExtension) {
				var result = this.extensions[this.currentIndex];
				this.currentIndex++;
				return result;
			}
			return null;
		}

		private readonly List<IExtender> extensions = default;
	}
}
