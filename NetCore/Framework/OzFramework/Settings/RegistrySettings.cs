/* File="RegistrySettings"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using w32 = Microsoft.Win32;

namespace OzFramework.Settings {
    public class RegistrySettings {
        private w32.RegistryHive Hive = w32.RegistryHive.CurrentUser;
        private w32.RegistryKey registryKey = default;
        public void SetValue<T>(string key, string valueName, T value) {
            if (key.IsNull()) {
                throw new ArgumentNullException(nameof(key));
            }
            if (valueName.IsNull()) {
                throw new ArgumentNullException(nameof(value));
            }
            if(registryKey.IsNull()) {
                registryKey = w32.Registry.CurrentUser.CreateSubKey(@"\Software\CCC");
                if (!registryKey.IsNull()) {
                    registryKey = registryKey.CreateSubKey(@"PEAT");
                    if(registryKey.IsNull()) {
                        throw new System.ApplicationException("Cannot create PEAT subkey");
                    }
                }
                var regKey = registryKey.CreateSubKey(key);
                if (regKey.IsNull()) {
                    throw new System.ApplicationException($"Cannot create {key} subkey");
                }
                regKey.SetValue(valueName, value);
            }
        }

        public T GetValue<T>(string key, string valueName, T defaultvalue = default) {
            if (key.IsNull()) {
                throw new ArgumentNullException(nameof(key));
            }
            if (valueName.IsNull()) {
                throw new ArgumentNullException(nameof(defaultvalue));
            }
            if (registryKey.IsNull()) {
                registryKey = w32.Registry.CurrentUser.OpenSubKey(@"Software");
                if (!registryKey.IsNull()) {
                    registryKey = registryKey.CreateSubKey(@"CCC");
                    if (registryKey.IsNull()) {
                        throw new System.ApplicationException("Cannot create CCC subkey");
                    }
                    registryKey = registryKey.CreateSubKey(@"PEAT");
                    if (registryKey.IsNull()) {
                        throw new System.ApplicationException("Cannot create PEAT subkey");
                    }
                }
                var regKey = registryKey.CreateSubKey(key);
                if (regKey.IsNull()) {
                    return defaultvalue;
                }
                if(!regKey.GetSubKeyNames().Contains(valueName)) {
                    return defaultvalue;
                }
                return (T)regKey.GetValue(valueName);
            }
            return defaultvalue;
        }
    }
}
