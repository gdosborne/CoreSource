using Common.Application.Media;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace CongregationManager.Data {
    [JsonObject]
    public class ApplicationTheme : INotifyPropertyChanged {
        public ApplicationTheme() { 
            Values = new Dictionary<string, string>();
        }

        #region Name Property
        private string _Name = default;
        /// <summary>Gets/sets the Name.</summary>
        /// <value>The Name.</value>
        [JsonProperty("name")]
        public string Name {
            get => _Name;
            set {
                _Name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Values Property
        private Dictionary<string, string> _Values = default;
        /// <summary>Gets/sets the Values.</summary>
        /// <value>The Values.</value>
        [JsonProperty("values")]
        public Dictionary<string, string> Values {
            get => _Values;
            set {
                _Values = value;
                OnPropertyChanged();
            }
        }
        #endregion

        [JsonIgnore]
        public string FullPath { get; set; }

        public static ApplicationTheme FromFile(string filename) {
            var json = default(string);
            var data = File.ReadAllText(filename);
            
            json = data;

            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            var result = JsonConvert.DeserializeObject<ApplicationTheme>(json, settings);
            result.FullPath = filename;
            return result;
        }

        public void Save(string filename) {
            FullPath= filename;
            Save();
        }

        public void Save() {
            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(this, settings);
            try {
                File.WriteAllText(FullPath, json);
            }
            catch (Exception) {
                throw;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(propertyName)));

        public void Apply(ResourceDictionary resources) {
            Values.ToList().ForEach(x => {
                resources.MergedDictionaries.ToList().ForEach(d => {
                    if(d.Contains(x.Key)) {
                        d[x.Key] = new SolidColorBrush(x.Value.ToColor());
                    }
                });
            });
        }
    }
}
