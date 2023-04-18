using Common.OzApplication.IO;
using Common.OzApplication.Text;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Common.OzApplication.Install {
    public sealed class UpdateChecker {

        private void ProcessPath(bool isTest) {
            ApplicationDirectory = System.IO.Path.Combine(ApplicationDirectory, ApplicationName);
            var szTest = new DirectoryInfo(ApplicationDirectory).Parent.Size();
            if (isTest) {
                ApplicationDirectory += " Test";
            }

            new DirectoryInfo(ApplicationDirectory).CreateIfNotExist();
        }

        public Version GetPreviousVersion() {
            var maxVersion = AllVersions.OrderByDescending(x => x).First();
            return AllVersions.OrderByDescending(x => x).Skip(1).First();
        }

        public string ApplicationDirectory { get; set; }

        public string ApplicationName { get; set; }

        public Version CurrentVersion { get; set; }

        public Guid ApplicationGuid { get; set; }

        public string AppInstallExeName { get; set; }

        public IEnumerable<Version> AllVersions {
            get {
                var dir = new DirectoryInfo(ApplicationDirectory);
                if (!dir.Exists) {
                    return new List<Version>();
                }

                var result = dir
                    .GetDirectories()
                    .Where(x => Version.TryParse(x.Name, out var version) && System.IO.File.Exists(System.IO.Path.Combine(ApplicationDirectory, version.ToString(), AppInstallExeName)))
                    .Select(x => Version.Parse(x.Name))
                    .ToList();
                return result;
            }
        }

        public Version HighestVersion {
            get {
                var allVersions = AllVersions;
                if (!allVersions.Any()) {
                    return new Version();
                }

                return AllVersions.Max(x => x);
            }
        }

        public string SetupFileName { get; set; }

        public override string ToString() => $"{ApplicationName} : {CurrentVersion}";

        public bool UpdateExists(Version version) => HighestVersion > version;

        public bool UpdateExists() => UpdateExists(CurrentVersion);

        private void UnloadAllInstances(string applicationTitle) {
            var currentProcessId = System.Diagnostics.Process.GetCurrentProcess().Id;
            var allProcesses = System.Diagnostics.Process.GetProcesses();
            var procs = System.Diagnostics.Process.GetProcesses().Where(x => applicationTitle.ContainsIgnoreCase(x.ProcessName) || x.ProcessName.ContainsIgnoreCase(applicationTitle));
            if (procs.Any()) {
                procs.ToList().ForEach(x => {
                    if (currentProcessId != x.Id) {
                        x.Kill();
                    }
                });
            }
        }

        public bool RunInstallation(Version version, string applicationTitle) {
            UnloadAllInstances(applicationTitle);
            try {
                var runFile = DownloadVersionExe(version);
                if (runFile == null) {
                    return false;
                }
                var p = new System.Diagnostics.Process {
                    StartInfo = new ProcessStartInfo {
                        FileName = runFile
                    }
                };
                p.Start();
            }
            catch (System.Exception ex) {
                MessageBox.Show($"An error occurred installing new version\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private string DownloadVersionExe(Version newVersion) {
            if (!AllVersions.Any(x => x == newVersion)) {
                return null;
            }

            var directory = new System.IO.DirectoryInfo(ApplicationDirectory);
            foreach (var subDirectory in directory.GetDirectories()) {
                if (Version.TryParse(subDirectory.Name, out var version)) {
                    if (version == newVersion) {
                        var exeFileName = System.IO.Path.Combine(ApplicationDirectory, version.ToString(), AppInstallExeName);
                        if (System.IO.File.Exists(exeFileName)) {
                            var tempDir = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString());
                            if (!System.IO.Directory.Exists(tempDir)) {
                                System.IO.Directory.CreateDirectory(tempDir);
                            }

                            var tempFileName = System.IO.Path.Combine(tempDir, AppInstallExeName);
                            System.IO.File.Copy(exeFileName, tempFileName, true);
                            return tempFileName;
                        }
                        break;
                    }
                }
            }
            return default;
        }
    }
}