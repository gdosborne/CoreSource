using Common.Application.Primitives;
using Newtonsoft.Json;
using OzDB.Management.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CommonFile = Common.Application.IO.File;
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
        [JsonProperty]
        public List<OzDbDataTable> Tables {
            get => _Tables;
            set {
                _Tables = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ID Property
        private Guid _ID = default;
        /// <summary>Gets/sets the ID.</summary>
        /// <value>The ID.</value>
        [JsonProperty]
        public Guid ID {
            get => _ID;
            set {
                _ID = value;
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

        public async Task<bool> SaveAsync() {
            await Task.Yield();
            var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            var json = JsonConvert.SerializeObject(this, settings);
            var newFilename = default(string);
            try {
                if (HasNameChange) {
                    newFilename = Path.Combine(Path.GetDirectoryName(Filename), $"{Name}{CommonFile.Extension(Filename)}");
                    if (!Filename.Equals(newFilename, StringComparison.OrdinalIgnoreCase)) {
                        File.Move(Filename, newFilename);
                        Filename = newFilename;
                    }
                    HasNameChange = false;
                }
                File.WriteAllText(Filename, json, Encoding.BigEndianUnicode);
                HasChanges = false;
            }
            catch (Exception ex) {
                throw new InvalidFilenameException($"{newFilename} is an invalid file name", ex, newFilename);
            }
            return true;
        }

        public async Task<bool> SaveAsAsync(string newFilename) {
            try {
                if (!File.Exists(newFilename)) {
                    var fnwo = Path.GetFileNameWithoutExtension(newFilename);
                    Name = fnwo;
                    if (await SaveAsync()) File.Move(Filename, newFilename);
                    HasChanges = false;
                }
                else
                    await SaveAsync();
            }
            catch (Exception ex) {
                throw new InvalidFilenameException($"{newFilename} is an invalid file name", ex, newFilename);
            }
            return true;
        }

        public async static Task<OzDBDatabase> Create(string name, string folder, Guid id) =>
            await Create(name, string.Empty, folder, id);

        public async static Task<OzDBDatabase> Create(string name, string description, string folder, Guid id) {
            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }
            var result = await CreateLocal(name, description, folder, id);
            result.HasChanges = false;
            return result;
        }

        private async static Task<OzDBDatabase> CreateLocal(string name, string description, string folder, Guid id) {
            var result = new OzDBDatabase(name, description, folder);
            var filename = Path.Combine(folder, $"{name}{DatabaseExtension}");
            result.Filename = filename;
            result.ID = id;
            await result.SaveAsync();
            return result;
        }

        private static OzDBDatabase OpenDatabase(string filename) {
            var settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            OzDBDatabase? db;
            try {
                var json = File.ReadAllText(filename, Encoding.BigEndianUnicode);
                db = JsonConvert.DeserializeObject(json, settings).As<OzDBDatabase>();
                var dir = new IO.DirectoryInfo(Path.GetDirectoryName(filename));
                //var tableFiles = dir.GetFiles($"*.{db.Name}{OzDbDataTable.TableExtension}");
                //foreach (var tableFile in tableFiles) {
                //    var t = OzDbDataTable.FromFile(tableFile.FullName);
                //    db.Tables.Add(t);
                //}
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

        public async Task<bool> MoveAsync(string destination) {
            if (string.IsNullOrWhiteSpace(destination)) return false;

            await Task.Yield();
            if (!Directory.Exists(destination)) {
                Directory.CreateDirectory(destination);
            }
            var destFilename = Path.Combine(destination, Path.GetFileName(Filename));
            if (File.Exists(Filename)) {
                throw new DatabaseExistsException($"Folder \"{destination}\" already contains the file {Path.GetFileName(Filename)}", null);
            }
            try {
                File.Move(Filename, destFilename);
                Filename = destFilename;
            }
            catch (System.Exception) {
                throw;
            }
            return true;
        }

        public void Close() {

        }
    }
}
