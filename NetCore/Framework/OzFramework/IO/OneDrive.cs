/* File="OneDrive"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using OzFramework.Text;
using System;
using System.Linq;

using w32 = Microsoft.Win32;

namespace OzFramework.IO {
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
            if (variables.Keys.Cast<string>().Any(x => x.ContainsIgnoreCase(oneDriveName) || x.ContainsIgnoreCase(oneDriveConsumerName))) {
                result = (string)variables[oneDriveName];
                if (result.IsNull()) {
                    result = (string)variables[oneDriveConsumerName];
                }
            }

            //2 - Registry
            if (result.IsNull()) {
                var regKey = w32.Registry.CurrentUser.OpenSubKey(oneDriveRegistryKey);
                if (!regKey.IsNull() && regKey.GetValueNames().Contains(userFolderName)) {
                    result = (string)regKey.GetValue(userFolderName);
                }

                if (result.IsNull()) {
                    regKey = w32.Registry.CurrentUser.OpenSubKey(skyDriveRegistryKey);
                    if (!regKey.IsNull() && regKey.GetValueNames().Contains(userFolderName)) {
                        result = (string)regKey.GetValue(userFolderName);
                    }
                }
            }

            //3 - Last try - there may be an ini file held over since the SkyDrive days
            if (result.IsNull()) {
                var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "OneDrive", "settings", "Personal");
                if (System.IO.Directory.Exists(path)) {
                    var d = new System.IO.DirectoryInfo(path);
                    var files = d.GetFiles().ToList();
                    var fileInfo = default(System.IO.FileInfo);
                    files.ForEach(file => {
                        if (!fileInfo.IsNull()) {
                            return;
                        }

                        if (file.Extension.ContainsIgnoreCase(".dat")) {
                            //there is an ini file with the same name as a dat file - this contains the Libray Path under the key "library"
                            var testFile = $"{System.IO.Path.GetFileNameWithoutExtension(file.FullName)}.ini";
                            fileInfo = files.FirstOrDefault(x => x.Name.ContainsIgnoreCase(testFile));
                            if (!fileInfo.IsNull()) {
                                return;
                            }
                        }
                    });
                    if (!fileInfo.IsNull()) {
                        using (var fs = new System.IO.FileStream(fileInfo.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
                        using (var reader = new System.IO.StreamReader(fs)) {
                            while (reader.Peek() > -1) {
                                var line = reader.ReadLine();
                                if (line.StartsWithIgnoreCase("library")) {
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
