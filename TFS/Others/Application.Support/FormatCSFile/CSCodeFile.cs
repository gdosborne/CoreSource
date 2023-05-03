namespace Application.Support.FormatCSFile
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CSCodeFile : IDisposable
    {
        public CSCodeFile(string fileName)
        {
            FileName = fileName;
            CodeLines = new List<CodeLine>();
            LoadFile();
        }

        private void LoadFile()
        {
            _fileStream = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            using (var sr = new StreamReader(_fileStream)) {
                RawText = sr.ReadToEnd();
            }

            using (var sr = new StringReader(RawText)) {
                var lineNumber = 0;
                while (sr.Peek() > -1) {
                    lineNumber++;
                    var line = sr.ReadLine();
                    var cl = new CodeLine(line, lineNumber);
                    CodeLines.Add(cl);
                }
            }
        }

        private FileStream _fileStream = null;

        public string FileName {
            get; private set;
        }

        private bool _isDisposed = false;

        protected virtual void Dispose(bool isDisposing)
        {
            if (!_isDisposed) {
                if (isDisposing) {
                    if (_fileStream != null) {
                        _fileStream.Close();
                        _fileStream.Dispose();
                    }
                    CodeLines.Clear();
                    CodeLines.TrimExcess();
                    RawText = null;
                }
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public List<CodeLine> CodeLines {
            get; private set;
        }

        public string RawText {
            get; private set;
        }

        public void Reformat()
        {

        }
    }
}
