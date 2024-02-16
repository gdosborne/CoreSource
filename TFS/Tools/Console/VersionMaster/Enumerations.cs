namespace VersionMaster {
	using System;
	using System.Collections.Generic;
	using System.IO;
    using System.Linq;
    using ConsoleUtilities;
	using GregOsborne.Application.Text;

	public static class Enumerations {

		public enum VersionParts {
			Assembly,
			Information,
			File
		}

		public enum ProjectTypes {
			CSProject,
			VBProject
		}

		public enum PartTypes {
			Major,
			Minor,
			Build,
			Revision
		}

		public enum TransformTypes {
			Ignore,
			Fixed,
			Increment,
			IncrementResetEachDay,
			IncrementEachDay,
			Random,
			DayValue,
			DayvalueFrom,
			SecondValue,
			SecondValueFrom,
			Year,
			TwoDigitYear,
			Month,
			Day,
			Second
		}

		public static IEnumerable<TransformTypes> GetTransformTypes() {
			var result = new List<TransformTypes>();
			Enum.GetNames(typeof(TransformTypes)).ToList().ForEach(t => {
				result.Add((TransformTypes)Enum.Parse(typeof(TransformTypes), t));
			});
			return result;
		}

		public static int ProcessMethod(this TransformTypes transformType, object[] paramValues, Dictionary<TransformTypes, Delegate> delegates) {
			if (!delegates.ContainsKey(transformType)) {
				throw new ApplicationException($"No delegate for method \"{transformType}\"");
			}

			var dgt = delegates[transformType];

			var result = default(int);
			var current = (int)paramValues[0];
			if (current < 0) {
				current = 0;
			}

			var lastUpdate = DateTime.Now.AddDays(-1);
			var minDate = new DateTime(1900, 1, 1);
			var hasParam = !string.IsNullOrEmpty((string)paramValues[2]);
			var hasLastDate = ((DateTime?)paramValues[1]).HasValue;
			if (hasLastDate) {
				lastUpdate = ((DateTime?)paramValues[1]).Value;
			}

			var parameters = new List<object>();
			switch (transformType) {
				case TransformTypes.Ignore:
					parameters.Add(current);
					break;

				case TransformTypes.Increment:
					parameters.Add(current);
					parameters.Add(false);
					parameters.Add(null);
					break;

				case TransformTypes.IncrementResetEachDay:
				case TransformTypes.IncrementEachDay:
					parameters.Add(current);
					if (hasLastDate) {
						parameters.Add(lastUpdate);
					}
					else {
						parameters.Clear();
						parameters.Add(hasParam ? int.Parse((string)paramValues[2]) : 0);
						dgt = delegates[TransformTypes.Fixed];
					}
					break;

				case TransformTypes.DayValue:
				case TransformTypes.SecondValue:
					parameters.Add(minDate);
					break;

				case TransformTypes.DayvalueFrom:
				case TransformTypes.SecondValueFrom:
					parameters.Add(hasParam ? DateTime.Parse((string)paramValues[2]) : minDate);
					break;

				case TransformTypes.Fixed:
					parameters.Add(hasParam ? int.Parse((string)paramValues[2]) : 0);
					break;

				case TransformTypes.Random:
				case TransformTypes.Year:
				case TransformTypes.TwoDigitYear:
				case TransformTypes.Month:
				case TransformTypes.Day:
					break;
			}
			result = dgt != null
				? (int)dgt.DynamicInvoke(parameters.ToArray())
				: (int)delegates[transformType].DynamicInvoke(parameters.ToArray());
			return result;
		}

		public static string Output(this VersionTrigger trigger, string line) => line.StartsWithIgnoreCase("// VersionUpdate")
			? string.Empty //remove version update from file if it exists
			: trigger == null
				? line
				: $"{trigger.ActualText}\"{trigger.Version}\")]";

		public static string TriggerText(this VersionParts value) {
			var result = value == VersionParts.Assembly
				? "[assembly: assemblyversion("
				: value == VersionParts.Information
					? "[assembly: assemblyinformationalversion("
					: "[assembly: assemblyfileversion(";
			return result;
		}

		public static string ActualText(this VersionParts value) => value == VersionParts.Assembly
			? "[assembly: AssemblyVersion("
			: value == VersionParts.Information
				? "[assembly: AssemblyInformationalVersion("
				: "[assembly: AssemblyFileVersion(";

		public static void UpdateAll(this VersionParts value, string assemblyInfoPath, Version oldVersion, Version newVersion, DateTime? lastUpdate = default) {
			if (string.IsNullOrEmpty(assemblyInfoPath)) {
				throw new ArgumentNullException(nameof(assemblyInfoPath));
			}

			if (!File.Exists(assemblyInfoPath)) {
				throw new FileNotFoundException("Cannot find assembly info file", assemblyInfoPath);
			}

			UpdateVersion(assemblyInfoPath, oldVersion, newVersion, !lastUpdate.HasValue ? DateTime.Now : lastUpdate.Value);
		}

		public static DateTime? GetLastUpdate(this VersionParts part, string assemblyInfoFileName) {
			var result = default(DateTime?);
			using (var fs = new FileStream(assemblyInfoFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (var sr = new StreamReader(fs)) {
				while (!sr.EndOfStream) {
					var line = sr.ReadLine();
					if (line.StartsWithIgnoreCase("// VersionUpdate")) {
						line = line.Replace("// VersionUpdate", string.Empty);
						line = line.TrimStart("(\"".ToCharArray()).TrimEnd("\")]".ToCharArray());
						result = DateTime.Parse(line);
						//("2019-12-20")
					}
				}
			}
			return result;
		}

		public static Version GetVersion(this VersionParts part, string assemblyInfoFileName) {
			var versionTrigger = part.TriggerText();
			var otherChar = "]";
			var result = new Version();
			var attrs = default(FileAttributes);

			try {
				attrs = File.GetAttributes(assemblyInfoFileName);
				if (attrs.HasFlag(FileAttributes.ReadOnly)) {
					attrs = attrs & ~FileAttributes.ReadOnly;
					File.SetAttributes(assemblyInfoFileName, attrs);
				}
			}
			catch { }

			try {
				using (var fs = new FileStream(assemblyInfoFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
				using (var sr = new StreamReader(fs)) {
					while (!sr.EndOfStream) {
						var line = sr.ReadLine();
						if (line.Trim().ToLower().StartsWith(versionTrigger)) {
							var v = line.Substring(versionTrigger.Length + 1);
							v = v.Replace(
								Helper.GetTuple(")", string.Empty),
								Helper.GetTuple(otherChar, string.Empty),
								Helper.GetTuple("\"", string.Empty),
								Helper.GetTuple("*", "0"));
							var verMaj = default(int);
							var verMin = default(int);
							var verBuild = default(int);
							var verRev = default(int);
							var testVer = new Version(v);
							if (testVer.Major >= 0) {
								verMaj = testVer.Major;
							}

							if (testVer.Minor >= 0) {
								verMin = testVer.Minor;
							}

							if (testVer.Build >= 0) {
								verBuild = testVer.Build;
							}

							if (testVer.Revision >= 0) {
								verRev = testVer.Revision;
							}

							result = new Version(verMaj, verMin, verBuild, verRev);
							break;
						}
					}
				}
			}
			catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
			return result;
		}

		public static void UpdateVersion(string assemblyInfoFileName, Version oldVersion, Version newVersion, DateTime lastUpdate) {
			var triggers = new Dictionary<string, bool> {
				{ VersionParts.Assembly.TriggerText(), false },
				{ VersionParts.File.TriggerText(), false },
				{ VersionParts.Information.TriggerText(), false },
				{ "// VersionUpdate(", false }
			};
			var attrs = default(FileAttributes);

			try {
				attrs = File.GetAttributes(assemblyInfoFileName);
				if (attrs.HasFlag(FileAttributes.ReadOnly)) {
					attrs = attrs & ~FileAttributes.ReadOnly;
					File.SetAttributes(assemblyInfoFileName, attrs);
				}
			}
			catch { }

			try {
				if (File.Exists($"{assemblyInfoFileName}.bak")) {
					File.Delete($"{assemblyInfoFileName}.bak");
				}
				if (!File.Exists($"{assemblyInfoFileName}.bak")) {
					File.Move(assemblyInfoFileName, $"{assemblyInfoFileName}.bak");
				}

				var lastUpdateActual = lastUpdate.ToString("yyyy-MM-dd");
				var lastUpdateReplace = DateTime.Now.ToString("yyyy-MM-dd");

				using (var fs = new FileStream($"{assemblyInfoFileName}.bak", FileMode.Open, FileAccess.Read, FileShare.Read))
				using (var sr = new StreamReader(fs))
				using (var strWriter = new StringWriter()) {
					var triggerFound = false;
					while (!sr.EndOfStream) {
						var line = sr.ReadLine();
						triggerFound = false;
						foreach (var x in triggers) {
							if (line.ContainsIgnoreCase(x.Key) && !x.Value) {
								triggerFound = true;
								triggers[x.Key] = true;

								if (x.Key == "// VersionUpdate(") {
									strWriter.WriteLine($"// VersionUpdate(\"{lastUpdateReplace}\")]");
								}
								else if (x.Key == VersionParts.Assembly.TriggerText()) {
									strWriter.WriteLine($"[assembly: AssemblyVersion(\"{newVersion}\")]");
								}
								else if (x.Key == VersionParts.File.TriggerText()) {
									strWriter.WriteLine($"[assembly: AssemblyFileVersion(\"{newVersion}\")]");
								}
								else if (x.Key == VersionParts.Information.TriggerText()) {
									strWriter.WriteLine($"[assembly: AssemblyInformationalVersion(\"{newVersion}\")]");
								}
								break;
							}
						}
						if (!triggerFound) {
							strWriter.WriteLine(line);
						}
					}

					using (var fs1 = new FileStream(assemblyInfoFileName, FileMode.Create, FileAccess.Write, FileShare.None))
					using (var sw = new StreamWriter(fs1)) {
						sw.Write(strWriter.ToString());
					}
				}
			}
			catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
		}
	}
}
