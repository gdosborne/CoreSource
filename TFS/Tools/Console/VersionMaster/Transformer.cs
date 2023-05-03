namespace VersionMaster {
    using ConsoleUtilities;
    using System;
    using System.IO;
    using static Enumerations;

    public static class Transformer {
        public static event ReportProgressHandler ReportProgress;

        public static Version Retrieve(this VersionParts part, string assemblyInfoFileName, out DateTime? lastUpdateDate) {
            if (string.IsNullOrEmpty(assemblyInfoFileName))
                throw new ArgumentNullException(nameof(assemblyInfoFileName));
            var result = UpdateVersion(assemblyInfoFileName, part, out var currentDate);
            lastUpdateDate = currentDate == default(DateTime) ? (DateTime?)null : currentDate;
            return result;
        }

        private static Version UpdateVersion(string assemblyInfoFileName, VersionParts part, out DateTime currentDate) {
            var versionTrigger = part == VersionParts.Assembly
                ? "[assembly: assemblyversion("
                : part == VersionParts.Information
                    ? "[assembly: assemblyinformationalversion("
                    : "[assembly: assemblyfileversion(";
            var updateTrigger = "// versionupdate(";
            var otherChar = "]";
            var result = new Version();
            currentDate = DateTime.Now;

            try {
                //turn off read-only attribute
                File.SetAttributes(assemblyInfoFileName, File.GetAttributes(assemblyInfoFileName) & ~FileAttributes.ReadOnly);
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
                            if (testVer.Major >= 0)
                                verMaj = testVer.Major;
                            if (testVer.Minor >= 0)
                                verMin = testVer.Minor;
                            if (testVer.Build >= 0)
                                verBuild = testVer.Build;
                            if (testVer.Revision >= 0)
                                verRev = testVer.Revision;
                            result = new Version(verMaj, verMin, verBuild, verRev);
                        }
                        else if (line.Trim().ToLower().StartsWith(updateTrigger)) {
                            var d = line.Substring(updateTrigger.Length + 1);
                            d = d.Replace(
                                Helper.GetTuple(")", string.Empty),
                                Helper.GetTuple(otherChar, string.Empty),
                                Helper.GetTuple("\"", string.Empty),
                                Helper.GetTuple(">", string.Empty));
                            currentDate = DateTime.Parse(d);
                        }
                    }
                }
            }
            catch { }
            return result;
        }

        private static string GetVersionItemSpecific(VersionParts part, string assembly, string information, string defaltValue) =>
            part == VersionParts.Assembly ? "assembly" : part == VersionParts.Information ? information : defaltValue;

        public static void Update(this VersionParts part, string assemblyInfoFileName, ProjectData projectData) {
            //UpdateVersion(assemblyInfoFileName, part, out var currentDate);
            var versionTrigger = part == VersionParts.Assembly
               ? "[assembly: assemblyversion("
               : part == VersionParts.Information
                   ? "[assembly: assemblyinformationalversion("
                   : "[assembly: assemblyfileversion(";
            var updateTrigger = "// versionupdate(";
            var otherChar = "]";
            var preVersion = new Version();
            var postVersion = new Version();
            var currentDate = DateTime.Now;

            try {
                //turn off read-only attribute
                File.SetAttributes(assemblyInfoFileName, File.GetAttributes(assemblyInfoFileName) & ~FileAttributes.ReadOnly);
            }
            catch { }

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
                        if (testVer.Major >= 0)
                            verMaj = testVer.Major;
                        if (testVer.Minor >= 0)
                            verMin = testVer.Minor;
                        if (testVer.Build >= 0)
                            verBuild = testVer.Build;
                        if (testVer.Revision >= 0)
                            verRev = testVer.Revision;
                        preVersion = new Version(verMaj, verMin, verBuild, verRev);
                    }
                    else if (line.Trim().ToLower().StartsWith(updateTrigger)) {
                        var d = line.Substring(updateTrigger.Length + 1);
                        d = d.Replace(
                            Helper.GetTuple(")", string.Empty),
                            Helper.GetTuple(otherChar, string.Empty),
                            Helper.GetTuple("\"", string.Empty),
                            Helper.GetTuple(">", string.Empty));
                        currentDate = DateTime.Parse(d);
                    }
                }
            }
            if(preVersion != new Version()) {

            }


            //var sb = new StringBuilder();
            //var hasUpdateDate = false;
            //var hasVersion = false;
            ////var versionTrigger = GetVersionItemSpecific(part, "[assembly: assemblyversion(", "[assembly: assemblyinformationalversion(", "[assembly: assemblyfileversion(");
            ////var versionData = GetVersionItemSpecific(part, "[assembly: AssemblyVersion(\"{0}\")]", "[assembly: AssemblyInformationalVersion(\"{0}\")]", "[assembly: AssemblyFileVersion(\"{0}\")]");
            ////var replacementVersion = GetVersionItemSpecific(part, projectData.ModifiedAssemblyVersion.ToString(4), projectData.ModifiedInformationVersion.ToString(4), projectData.ModifiedFileVersion.ToString(4));
            ////var updateTrigger = "// versionupdate(";
            ////var updateData = "// VersionUpdate(\"{0}\")]";

            //var assyTrigger = "assembly: AssemblyVersion";
            //var fileTrigger = "assembly: AssemblyVersion";
            //var infoTrigger = "assembly: AssemblyInformationalVersion";

            //using (var fs = new FileStream(assemblyInfoFileName, FileMode.Open, FileAccess.Read, FileShare.None))
            //using (var sr = new StreamReader(fs)) {
            //    while (!sr.EndOfStream) {
            //        var lineTrim = sr.ReadLine().Trim();

            //        if ((part == VersionParts.Assembly && lineTrim.ContainsIgnoreCase(assyTrigger)) 
            //                || (part == VersionParts.File && lineTrim.ContainsIgnoreCase(fileTrigger)) 
            //                || (part == VersionParts.Information && lineTrim.ContainsIgnoreCase(infoTrigger)))
            //            lineTrim = ProcessLine(part, lineTrim, projectData, assemblyInfoFileName);
            //        sb.AppendLine(lineTrim);



            //        //var line = sr.ReadLine();
            //        //if (line.Trim().ToLower().StartsWith(versionTrigger)) {
            //        //    sb.AppendLine(string.Format(versionData, replacementVersion));
            //        //    hasVersion = true;
            //        //}
            //        //else if (line.Trim().ToLower().StartsWith(updateTrigger)) {
            //        //    hasUpdateDate = true;
            //        //    sb.AppendLine(string.Format(updateData, DateTime.Now.ToString("yyyy-MM-dd")));
            //        //}
            //        //else
            //        //    sb.AppendLine(line);
            //    }
            //    //if (part == VersionParts.Information && !hasVersion)
            //    //    sb.AppendLine(string.Format(versionData, replacementVersion));
            //    //if (!hasUpdateDate)
            //    //    sb.AppendLine(string.Format(updateData, DateTime.Now.ToString("yyyy-MM-dd")));
            //    sr.Close();
            //    fs.Close();
            //}
            //using (var fs = new FileStream(assemblyInfoFileName, FileMode.Create, FileAccess.Write, FileShare.Read))
            //using (var sw = new StreamWriter(fs)) {
            //    sw.Write(sb.ToString());
            //}
        }

        //private static string ProcessLine(this VersionParts part, string line, ProjectData projectData, string assemblyInfoFileName) {
        //    var result = line;
        //    var ver = UpdateVersion(assemblyInfoFileName, part, out var currentDateTime);

        //    return result;
        //}        
    }
}
