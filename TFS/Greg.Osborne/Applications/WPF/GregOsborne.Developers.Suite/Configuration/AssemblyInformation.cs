namespace GregOsborne.Developers.Suite.Configuration {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using GregOsborne.Suite.Extender;
	using SysIO = System.IO;

	public class AssemblyInformation {
		public AssemblyInformation() => this.ExtensionNames = new ObservableCollection<string>();

		public override string ToString() {
			var extNames = default(string);
			if (this.ExtensionNames.Any()) {
				extNames = " " + this.ExtNames;
			}
			return $"{this.AssemblyFileName} ({this.Version}){extNames}";
		}
		public string ExtNames {
			get {
				if (!this.ExtensionNames.Any()) {
					return string.Empty;
				}

				var extNames = "\"";
				extNames += string.Join("\",\"", this.ExtensionNames);
				if (!extNames.EndsWith("\"")) {
					extNames += "\"";
				}

				return extNames;
			}
		}
		public ObservableCollection<string> ExtensionNames { get; set; } = default;
		public string AssemblyFileName { get; set; } = default;
		public string Path { get; set; } = default;
		public string Version { get; set; } = default;
		public DateTime BuildDate { get; set; } = default;
		public string AssemblyName { get; set; }
		public static AssemblyInformation FromExtension(IExtender extension) {
			var result = new AssemblyInformation {
				AssemblyFileName = extension.AssemblyFilename,
				Path = SysIO.Path.GetDirectoryName(extension.AssemblyFilename),
				Version = extension.GetType().Assembly.GetName().Version.ToString(),
				BuildDate = new FileInfo(extension.AssemblyFilename).LastWriteTime,
				AssemblyName = Assembly.LoadFile(extension.AssemblyFilename).GetName().Name
			};
			result.ExtensionNames.Add(extension.Title);
			return result;
		}

		public static List<AssemblyInformation> LoadAssemblies(string assemblyDir) {
			var result = new List<AssemblyInformation>();
			if (!Directory.Exists(assemblyDir)) {
				throw new ApplicationException($"Cannot find directory {assemblyDir}");
			}

			var assyExtensions = new string[] { "dll", "exe" };
			assyExtensions.ToList().ForEach(x => {
				var filter = $"*.{x}";
				GetAssembliesFromDirectory(assemblyDir, filter, result);
			});
			return result;
		}

		private static void LoadAssembly(FileInfo file, List<AssemblyInformation> extensions) {
			var assy = Assembly.LoadFile(file.FullName);
			var ai = new AssemblyInformation {
				Path = file.FullName,
				AssemblyFileName = file.Name,
				Version = assy.GetName().Version.ToString(),
				BuildDate = file.LastWriteTime,
				AssemblyName = assy.GetName().Name
			};
			var types = assy.GetTypes().Where(x => x.GetConstructors().Length > 0).ToArray();
			types = types.Where(x => x.GetConstructors().Any(y => !y.ContainsGenericParameters)).ToArray();
			types = types.Where(x => x.GetConstructors().Any(y => y.GetParameters().Count() == 0)).ToArray();
			types.ToList().ForEach(type => {
				var exts = type.GetInterfaces().Where(x => x.Name == "IExtender");
				if (exts.Any()) {
					var instance = Activator.CreateInstance(type);
					exts.ToList().ForEach(ext => {
						var name = ((IExtender)instance).Title;
						ai.ExtensionNames.Add(name);
					});
				}
			});
			extensions.Add(ai);
		}

		private static void GetAssembliesFromDirectory(string directoryName, string filter, List<AssemblyInformation> assemblies) {
			var dir = new DirectoryInfo(directoryName);
			var assys = dir.GetFiles(filter);
			assys.ToList().ForEach(dll => {
				LoadAssembly(dll, assemblies);
			});
			var dirs = dir.GetDirectories();
			dirs.ToList().ForEach(x => GetAssembliesFromDirectory(x.FullName, filter, assemblies));
		}
	}
}
