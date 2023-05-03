namespace GregOsborne.ApplicationData.ContinuousDeliverer {
    using GregOsborne.ApplicationData.ContinuousDeliverer.Model;
    using GregOsborne.ApplicationData.Exceptions.ContinuousDeliverer;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public sealed class Repository : IRepository, IDisposable {
        public enum ConnectionParameterIndex {
            DataDirectory
        }

        private string _dataDirectory = default(string);
        private bool _disposedValue = false;
        private bool _isOpen = false;

        public Repository(string dataDirectory) => ConnectionParameters = new string[] {
                dataDirectory
            };

        public Repository() { }

        public bool Connect(params string[] connectionParameters) {
            var result = false;
            ConnectionParameters = connectionParameters;
            if (connectionParameters != null && connectionParameters.Length > 0) {
                DataDirectory = connectionParameters[(int)ConnectionParameterIndex.DataDirectory];
            }
            return result;
        }

        public string[] ConnectionParameters { get; private set; }

        public string DataDirectory {
            get => _dataDirectory;
            private set {
                _dataDirectory = value;
                if (string.IsNullOrEmpty(_dataDirectory) && !Directory.Exists(_dataDirectory))
                    Directory.CreateDirectory(_dataDirectory);
            }
        }

        public IEnumerable<Application> GetApplications() {
            var result = new List<Application>();
            var dInfo = new DirectoryInfo(DataDirectory);
            if (!dInfo.Exists)
                throw new DataDirectoryMissingException("Data directory missing", DataDirectory);
            var index = 0;
            dInfo.GetFiles("*.accdb").ToList().ForEach(x => {
                var app = new Application {
                    DatabaseFileName = x.FullName,
                    ID = index,
                    Name = Path.GetFileNameWithoutExtension(x.Name)
                };
                result.Add(app);
                index++;
            });           
            return result;
        }

        public void Open() {
            if (!_isOpen) {
                _isOpen = true;
            }
            else {
                throw new DatabaseAlreadyOpenException();
            }
        }

        public void Close() {
            if (!_isOpen)
                return;

        }

        private void Dispose(bool disposing) {
            if (!_disposedValue) {
                if (disposing) {

                }
                _disposedValue = true;
            }
        }

        public void Dispose() => Dispose(true);

        public static void Create(params string[] parameters) {
            var fullName = Path.Combine(parameters[0], $"{parameters[1]}.accdb");
            if (File.Exists(fullName))
                throw new DatabaseAlreadyExistsException();
            var sourceFileName = Path.Combine(Path.GetDirectoryName(typeof(Repository).Assembly.Location), "ContinuousDeliverer", "DatabaseTemplate", "_Template.accdb");
            if(!File.Exists(sourceFileName))
                throw new DatabaseTemplateFileMissingException();
            File.Copy(sourceFileName, fullName);
        }
    }
}
