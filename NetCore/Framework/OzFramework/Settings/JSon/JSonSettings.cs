/* File="JSonSettings"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Common.Primitives;
using Common.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common.Settings.JSon {
    public class JSonSettings {
        private JSonSettings() {
            Sections = new List<JSonSection>();
        }

        private string settingsFileName;
        public List<JSonSection> Sections { get; private set; }
        public static JSonSettings? OpenFromFile(string filename) {
            var json = default(string);
            var data = "{" + Environment.NewLine + "}";
            if(File.Exists(filename))
                data = File.ReadAllText(filename);
            json = data;

            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            var file = JsonConvert.DeserializeObject<JSonSettings>(json, settings);
            file.settingsFileName = filename;
            return file;
        }

        public T GetValue<T>(string section, string key, T defaultValue = default) {
            var tmpSection = Sections.FirstOrDefault(x => x.Name.EqualsIgnoreCase(section));
            if (tmpSection.IsNull()) {
                tmpSection = new JSonSection(section);
                Sections.Add(tmpSection);
            }
            var tmpValue = tmpSection.Values.FirstOrDefault(x => x.Name.EqualsIgnoreCase(key));
            if (tmpValue.IsNull()) {
                tmpValue = new JSonValue(key, defaultValue);
                tmpSection.Values.Add(tmpValue);
            }
            return (T)tmpValue.Value;
        }

        public void SetValue<T>(string section, string key, T value) {
            var tmpSection = Sections.FirstOrDefault(x => x.Name.EqualsIgnoreCase(section));
            if (tmpSection.IsNull()) {
                tmpSection = new JSonSection(section);
                Sections.Add(tmpSection);
            }
            var tmpValue = tmpSection.Values.FirstOrDefault(x => x.Name.EqualsIgnoreCase(key));
            if (tmpValue.IsNull()) {
                tmpValue = new JSonValue(key, value);
                tmpSection.Values.Add(tmpValue);
            } else
                tmpValue.Value = value;
            Save();
        }

        internal void Save() {
            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(this, settings);
            File.WriteAllText(settingsFileName, json);

        }
    }
}
