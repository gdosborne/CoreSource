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
    [JsonObject]
    public class OzDBDatabase : PropertyChangedBase {
        internal OzDBDatabase(string name, string description, string filename) {
            Name = name;
            Description = description;
            Filename = filename;
            Tables = new List<OzDbDataTable>();
        }

        public static string DatabaseExtension => ".ozdb";

        #region Filename Property
        private string _Filename = default;
        /// <summary>Gets/sets the Filename.</summary>
        /// <value>The Filename.</value>
        [JsonIgnore]
        public string Filename {
            get => _Filename;
            set {
                _Filename = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Tables Property
        private List<OzDbDataTable> _Tables = default;
        /// <summary>Gets/sets the Tables.</summary>
        /// <value>The Tables.</value>
        [JsonIgnore]
        public List<OzDbDataTable> Tables {
            get => _Tables;
            set {
                _Tables = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public async override Task<bool> DeleteAsync() {
            try {
                foreach (var table in Tables) {
                    await table.DeleteAsync();
                }
                File.Delete(Filename);
            }
            catch (System.Exception) {
                throw;
            }
            return true;
        }

        public async override Task<bool> Save() {
            var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            var json = JsonConvert.SerializeObject(this, settings);
            try {
                foreach (var table in Tables) {
                    await table.Save();
                }
                File.WriteAllText(Filename, json, Encoding.BigEndianUnicode);
            }
            catch (Exception ex) {
                throw;
            }
            return true;
        }

        public static OzDBDatabase Create(string name, string folder) => Create(name, string.Empty, folder);

        public static OzDBDatabase Create(string name, string description, string folder) {
            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }
            return CreateLocal(name, description, folder);
        }

        private static OzDBDatabase CreateLocal(string name, string description, string folder) {
            var result = new OzDBDatabase(name, description, folder);
            var filename = Path.Combine(folder, $"{name}{DatabaseExtension}");
            result.Filename = filename;
            result.Save().Wait();
            return result;
        }

        private static OzDBDatabase OpenDatabase(string filename) {
            var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            OzDBDatabase? db;
            try {
                var json = File.ReadAllText(filename, Encoding.BigEndianUnicode);
                db = JsonConvert.DeserializeObject(json, settings).As<OzDBDatabase>();
                var dir = new IO.DirectoryInfo(Path.GetDirectoryName(filename));
                var tableFiles = dir.GetFiles($"*.{db.Name}{OzDbDataTable.TableExtension}");
                foreach (var tableFile in tableFiles) {
                    var t = OzDbDataTable.FromFile(tableFile.FullName);
                    db.Tables.Add(t);
                }
            }
            catch (Exception) {
                throw;
            }
            return db;
        }

        public static OzDBDatabase FromDatabaseFile(string dbFilename) {
            if (!File.Exists(dbFilename))
                throw new IO.FileNotFoundException($"Cannot find database file ({dbFilename})");

            return OpenDatabase(dbFilename);
        }

        public async override Task<bool> MoveAsync(string destination) {
            if (string.IsNullOrWhiteSpace(destination)) return false;

            if (!Directory.Exists(destination)) {
                Directory.CreateDirectory(destination);
            }
            var destFilename = Path.Combine(destination, Path.GetFileName(Filename));
            if (File.Exists(Filename)) {
                throw new DatabaseExistsException($"Folder \"{destination}\" already contains the file {Path.GetFileName(Filename)}");
            }
            try {
                File.Move(Filename, destFilename);
                foreach (var table in Tables) {
                    await table.MoveAsync(destination);
                }
                Filename = destFilename;
            }
            catch (System.Exception) {
                throw;
            }
            return true;
        }
    }
}
