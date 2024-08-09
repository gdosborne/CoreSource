/* File="UpdateChecker"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Common.IO;
using Common.Primitives;
using Common.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using fx = Common.IO.File;

namespace Common.Install {
    public sealed class UpdateChecker {

        public Version GetPreviousVersion() {
            return AllVersions.OrderByDescending(x => x).Skip(1).First();
        }

        public string InstallerDirectory { get; set; }
        public string ApplicationName { get; set; }
        public Version CurrentVersion { get; set; }
        public string AppInstallExeNamePrefix { get; set; }

        #region TemporaryDirectory Property
        private string _TemporaryDirectory = default;
        public string TemporaryDirectory {
            get => _TemporaryDirectory;
            set {
                _TemporaryDirectory = value;
                if (!TemporaryDirectory.IsNull() && System.IO.Directory.Exists(TemporaryDirectory)) {
                    TempDirectory = new DirectoryInfo(TemporaryDirectory);
                }
            }
        }
        #endregion

        public DirectoryInfo TempDirectory { get; private set; }

        public IEnumerable<Version> AllVersions {
            get {
                var dir = new DirectoryInfo(InstallerDirectory);
                if (!dir.Exists) {
                    return new List<Version>();
                }

                var result = dir
                    .GetFiles($"{AppInstallExeNamePrefix}*.exe")
                    .Select(x => x.Name.Substring(9).Replace(".exe", string.Empty))
                    .Select(x => Version.Parse(x))
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

        private string CreateBatchFile(string installFilename) {
            TempDirectory = new DirectoryInfo(System.IO.Path.Combine(TemporaryDirectory, Guid.NewGuid().ToString())).CreateIfNotExist();
            var result = System.IO.Path.Combine(TempDirectory.FullName, "install.cmd");
            var sb = new StringBuilder();
            sb.AppendLine("@echo off");
            sb.AppendLine("@echo Please wait ... running update");
            sb.AppendLine("@echo If the update is located on a network drive, it may take awhile to start");
            sb.AppendLine($"\"{installFilename}\"");
            using (var fs = new FileStream(result, FileMode.CreateNew, FileAccess.Write, FileShare.None)) {
                using (var sw = new StreamWriter(fs)) {
                    sw.Write(sb.ToString());
                }
            }

            return result;
        }

        public async Task<bool> RunInstallation(Version version, string applicationTitle, bool isDownloadEnabled = false) {
            try {
                var runFile = System.IO.Path.Combine(InstallerDirectory, $"{AppInstallExeNamePrefix}{version}.exe");
                if (isDownloadEnabled) {
                    runFile = await DownloadVersionExeAsync(version);
                }
                if (runFile.IsNull()) {
                    return false;
                }
                if (System.IO.File.Exists(runFile)) {
                    var cmdFile = CreateBatchFile(runFile);
                    var p = new System.Diagnostics.Process {
                        StartInfo = new ProcessStartInfo {
                            FileName = cmdFile,
                            UseShellExecute = true,
                            WindowStyle = ProcessWindowStyle.Normal,
                        }
                    };
                    p.Start();
                }
                //UnloadAllInstances(applicationTitle);
            }
            catch (System.Exception ex) {
                MessageBox.Show($"An error occurred installing new version\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private async Task<string> DownloadVersionExeAsync(Version newVersion) {
            if (!AllVersions.Any(x => x == newVersion)) {
                return null;
            }
            var directory = new System.IO.DirectoryInfo(InstallerDirectory);
            var file = new FileInfo(System.IO.Path.Combine(directory.FullName, $"{ApplicationName} {newVersion}.exe"));
            var fileLen = file.Length;
            if (file.Exists) {
                TempDirectory = new DirectoryInfo(System.IO.Path.Combine(TemporaryDirectory, Guid.NewGuid().ToString())).CreateIfNotExist();
                var tempFileName = System.IO.Path.Combine(TempDirectory.FullName, file.Name);
                file.CopyTo(tempFileName);
                while (!System.IO.File.Exists(tempFileName) && fx.FileSize(tempFileName) < fileLen) {
                    await Task.Delay(1000);
                }
                return tempFileName;
            }
            return default;
        }
    }
}
