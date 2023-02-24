using System;
using System.Linq;

namespace Common.Application.IO {
    public static class OneDrive {
        public static string GetOneDriveDirectory() {
            var result = default(string);
            var oneDriveName = "OneDrive";
            var oneDriveConsumerName = "OneDriveConsumer";
            var skyDriveRegistryKey = @"Software\Microsoft\Windows\CurrentVersion\SkyDrive";
            var oneDriveRegistryKey = @"Software\Microsoft\OneDrive";
            var userFolderName = "UserFolder";

            //1 - Environment variables
            var variables = Environment.GetEnvironmentVariables();
            if (variables.Keys.Cast<string>().Any(x => x.Equals(oneDriveName, StringComparison.OrdinalIgnoreCase) || x.Equals(oneDriveConsumerName, StringComparison.OrdinalIgnoreCase))) {
                result = (string)variables[oneDriveName];
                if (string.IsNullOrEmpty(result)) {
                    result = (string)variables[oneDriveConsumerName];
                }
            }

            //2 - Registry
            if (string.IsNullOrEmpty(result)) {
                var regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(oneDriveRegistryKey);
                if (regKey != null && regKey.GetValueNames().Contains(userFolderName)) {
                    result = (string)regKey.GetValue(userFolderName);
                }

                if (string.IsNullOrEmpty(result)) {
                    regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(skyDriveRegistryKey);
                    if (regKey != null && regKey.GetValueNames().Contains(userFolderName)) {
                        result = (string)regKey.GetValue(userFolderName);
                    }
                }
            }

            //3 - Last try - there may be an ini file held over since the SkyDrive days
            if (string.IsNullOrEmpty(result)) {
                var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "OneDrive", "settings", "Personal");
                if (System.IO.Directory.Exists(path)) {
                    var d = new System.IO.DirectoryInfo(path);
                    var files = d.GetFiles().ToList();
                    var fileInfo = default(System.IO.FileInfo);
                    files.ForEach(file => {
                        if (fileInfo != null) {
                            return;
                        }

                        if (file.Extension.Equals(".dat", StringComparison.OrdinalIgnoreCase)) {
                            //there is an ini file with the same name as a dat file - this contains the Libray Path under the key "library"
                            var testFile = $"{System.IO.Path.GetFileNameWithoutExtension(file.FullName)}.ini";
                            fileInfo = files.FirstOrDefault(x => x.Name.Equals(testFile, StringComparison.OrdinalIgnoreCase));
                            if (fileInfo != null) {
                                return;
                            }
                        }
                    });
                    if (fileInfo != null) {
                        using (var fs = new System.IO.FileStream(fileInfo.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
                        using (var reader = new System.IO.StreamReader(fs)) {
                            while (reader.Peek() > -1) {
                                var line = reader.ReadLine();
                                if (line.StartsWith("library", StringComparison.OrdinalIgnoreCase)) {
                                    var parts = line.Split(" = ".ToCharArray());
                                    var data = parts[1].Split(' ');
                                    result = data[7].Replace("\"", string.Empty);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
