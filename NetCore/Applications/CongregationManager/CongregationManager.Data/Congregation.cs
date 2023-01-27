using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Applicationn;
using Newtonsoft.Json;

namespace CongregationManager.Data {
    [JsonObject("congregation")]
    public class Congregation {
        public Congregation() {
            Groups = new List<Group>();
        }
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonIgnore]
        public string Filename { get; set; }
        [JsonIgnore]
        public string DataPath { get; set; }
        [JsonProperty("groups")]
        public List<Group> Groups { get; set; }

        public void Save(string password) {
            var crypto = new Crypto(password);

            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            var json = JsonConvert.SerializeObject(this, settings);
            var enc = crypto.Encrypt<string>(json);
            File.WriteAllText(Path.Combine(DataPath, Filename), enc);
        }

        public static Congregation OpenFromFile(string filename, string password) {
            var crypto = new Crypto(password);
            var data = File.ReadAllText(filename);
            var json = crypto.Decrypt<string>(data);

            var settings = new JsonSerializerSettings {
                Formatting = Formatting.Indented
            };
            var result = JsonConvert.DeserializeObject<Congregation>(json, settings);
            return result;
        }

        override public string ToString() =>
            Name;
    }
}
