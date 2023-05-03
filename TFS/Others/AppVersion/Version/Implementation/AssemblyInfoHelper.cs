namespace VersionEngine.Implementation
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;

	public class AssemblyInfoHelper
	{
		#region Public Constructors
		public AssemblyInfoHelper(string fileName) {
			FileName = fileName;
		}
		#endregion Public Constructors

		#region Public Methods
		public Version GetVersion(ProjectTypes projectType, bool fileVersion) {
			Version result = new Version();
			string versionTrigger = null;
			string updateTrigger = null;
			string endChar = null;
			string startChar = null;
			string assyAttrName = "assembly:";
			switch (projectType) {
				case ProjectTypes.CPPProject:
				case ProjectTypes.CSProject:
					startChar = "[";
					//versionTrigger = fileVersion ? "[assembly: assemblyfileversion(" : "[assembly: assemblyversion(";
					updateTrigger = "// versionupdate(";
					endChar = "]";
					break;
				case ProjectTypes.VBProject:
					startChar = "<";
					//versionTrigger = fileVersion ? "<assembly: assemblyfileversion(" : "<assembly: assemblyversion(";
					updateTrigger = "' versionupdate(";
					endChar = ">";
					break;
			}
			LastUpdate = null;
			using (var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.None))
			using (var sr = new StreamReader(fs)) {
				while (!sr.EndOfStream) {
					var line = sr.ReadLine();
					if (line.Trim().StartsWith(startChar)) {
						if (line.Trim().Substring(1, assyAttrName.Length).Equals(assyAttrName, StringComparison.OrdinalIgnoreCase)) {
							var versionType = line.Split(':')[1];
							var pStart = versionType.IndexOf("(");
							var pEnd = versionType.IndexOf(")", pStart + 1);
							if (pEnd > -1) {
								var data = versionType.Substring(pStart + 1, pEnd - pStart - 1).Trim('\"');
								var verType = versionType.Substring(0, pStart).Trim();
								if (verType.Equals("AssemblyFileVersionAttribute", StringComparison.OrdinalIgnoreCase) || verType.Equals("AssemblyFileVersion", StringComparison.OrdinalIgnoreCase) || verType.Equals("AssemblyVersionAttribute", StringComparison.OrdinalIgnoreCase) || verType.Equals("AssemblyVersion", StringComparison.OrdinalIgnoreCase)) {
									if (data.EndsWith(".*"))
										data = data.Replace(".*", string.Empty);
									Version ver;
									if (Version.TryParse(data, out ver)) {
										result = ver;
									}
								}
							}
						}
					}
					else if (line.Trim().ToLower().StartsWith(updateTrigger)) {
						string d = line.Substring(updateTrigger.Length);
						d = d
							.Replace(")", String.Empty)
							.Replace(endChar, String.Empty)
							.Replace("\"", String.Empty)
							.Replace(">", String.Empty)
							.Replace(";", string.Empty);
						LastUpdate = DateTime.Parse(d);
					}
				}
				sr.Close();
				fs.Close();
			}
			return result;
		}
		public DateTime? LastUpdate { get; private set; }
		public void SetAssemblyVersion(string assyInfo, ProjectData p, ProjectTypes projectType) {
			SetVersion(assyInfo, p, projectType, false);
		}
		public void SetFileVersion(string assyInfo, ProjectData p, ProjectTypes projectType) {
			SetVersion(assyInfo, p, projectType, true);
		}
		#endregion Public Methods

		#region Private Methods
		private void SetVersion(string assyInfo, ProjectData p, ProjectTypes projectType, bool fileVersion) {
			StringBuilder sb = new StringBuilder();
			string updateTrigger = null;
			string endChar = null;
			string startChar = null;
			string assyAttrName = "assembly:";
			bool dateAdded = false;
			switch (projectType) {
				case ProjectTypes.CPPProject:
				case ProjectTypes.CSProject:
					startChar = "[";
					endChar = "]";
					updateTrigger = "// versionupdate(";
					break;
				case ProjectTypes.VBProject:
					startChar = "<";
					endChar = ">";
					updateTrigger = "' versionupdate(";
					break;
			}
			using (var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.None))
			using (var sr = new StreamReader(fs)) {
				while (!sr.EndOfStream) {
					var line = sr.ReadLine();
					if (line.Trim().StartsWith(startChar)) {
						if (line.Trim().Substring(1, assyAttrName.Length).Equals(assyAttrName, StringComparison.OrdinalIgnoreCase)) {
							var versionType = line.Split(':')[1];
							var pStart = versionType.IndexOf("(");
							var pEnd = versionType.IndexOf(")", pStart + 1);
							if (pEnd > -1) {
								var verType = versionType.Substring(0, pStart).Trim();
								if (verType.Equals("AssemblyFileVersionAttribute", StringComparison.OrdinalIgnoreCase) || verType.Equals("AssemblyFileVersion", StringComparison.OrdinalIgnoreCase) || verType.Equals("AssemblyVersionAttribute", StringComparison.OrdinalIgnoreCase) || verType.Equals("AssemblyVersion", StringComparison.OrdinalIgnoreCase)) {
									sb.AppendFormat("{0}{1}{2}(\"{3}\"){4}" + Environment.NewLine, new object[] { startChar, assyAttrName, verType, fileVersion ? p.ModifiedFileVersion : p.ModifiedAssemblyVersion, endChar });
								}
								else
									sb.AppendLine(line);
							}
							else
								sb.AppendLine(line);
						}
						else
							sb.AppendLine(line);
					}
					else if (line.Trim().ToLower().StartsWith(updateTrigger)) {
						sb.AppendLine(updateTrigger + "\"" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "\")" + (projectType == ProjectTypes.VBProject ? string.Empty : ";"));
						dateAdded = true;
					}
					else {
						sb.AppendLine(line);
					}
				}
				if (!dateAdded) {
					sb.AppendLine(updateTrigger + "\"" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "\")" + (projectType == ProjectTypes.VBProject ? string.Empty : ";"));
				}
				sr.Close();
				fs.Close();
			}
			using (var fs = new FileStream(assyInfo, FileMode.Create, FileAccess.Write, FileShare.None))
			using (var sw = new StreamWriter(fs)) {
				sw.Write(sb.ToString());
				sw.Close();
				fs.Close();
			}
		}
		#endregion Private Methods

		#region Public Properties
		public string FileName { get; private set; }
		#endregion Public Properties
	}
}
