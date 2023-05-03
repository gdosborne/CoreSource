using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GregOsborne.Controls.Configuration {
    public delegate void CodeChangedHandler(object sender, CodeChangedEventArgs e);

    public class CodeChangedEventArgs : EventArgs {
        public CodeChangedEventArgs(string code, Enumerations.CodeLanguage language, string rawText) {
            Code = code;
            Language = language;
            RawText = rawText;
        }
        public string Code { get; }
        public Enumerations.CodeLanguage Language { get; }
        public string RawText { get; }
    }
}
