using Common.Application.Primitives;
using Newtonsoft.Json;
using OzDB.Management.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using IO = System.IO;

namespace OzDB.Management {
    public class OzDbDataTable : PropertyChangedBase {
        internal OzDbDataTable() { }

        public static string TableExtension => ".ozt";

        #region Filename Property
        private string _Filename = default;
        /// <summary>Gets/sets the Filename.</summary>
        /// <value>The Filename.</value>
        [JsonIgnore]
        public string Filename {
            get => _Filename;
            internal set {
                _Filename = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Fields Property
        private List<OzDBDataField> _Fields = default;
        /// <summary>Gets/sets the Fields.</summary>
        /// <value>The Fields.</value>
        [JsonProperty]
        public List<OzDBDataField> Fields {
            get => _Fields;
            set {
                _Fields = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public async override Task<bool> Save() {
            await Task.Yield();
            var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            var json = JsonConvert.SerializeObject(this, settings);
            try {
                IO.File.WriteAllText(IO.Path.Combine(Filename, "Database.ozdb"), json, Encoding.BigEndianUnicode);
            }
            catch (Exception) {
                throw;
            }
            return true;
        }

        public static OzDbDataTable FromFile(string filename) {
            var result = default(OzDbDataTable);
            try {
                var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                var json = IO.File.ReadAllText(filename, Encoding.BigEndianUnicode);
                result = JsonConvert.DeserializeObject(json, settings).As<OzDbDataTable>();
                result.Filename = filename;
            }
            catch (Exception) { throw; }
            return result;
        }

        public async override Task<bool> MoveAsync(string destination) {
            if (string.IsNullOrWhiteSpace(destination)) return false;

            if (!Directory.Exists(destination)) {
                Directory.CreateDirectory(destination);
            }
            var destFilename = Path.Combine(destination, Path.GetFileName(Filename));
            if (File.Exists(Filename)) {
                throw new TableExistsException($"Table \"{destFilename}\" already exists");
            }
            try {
                File.Move(Filename, destFilename);
                File.Delete(Filename);
                Filename = destFilename;
            }
            catch (System.Exception) {
                throw;
            }
            return true;
        }

        public async override Task<bool> DeleteAsync() {
            try {
                File.Delete(Filename);
            }
            catch (System.Exception) {
                throw;
            }
            return true;
        }
    }
}
