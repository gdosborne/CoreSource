/* File="WriteToLogEventArgs"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Events {
    public delegate void WriteToLogHandler(object sender, WriteToLogEventArgs e);
    public class WriteToLogEventArgs : EventArgs {
        public WriteToLogEventArgs(string message, ApplicationLogger.EntryTypes entryType, int tabIndent = 0) { 
            Message = message;
            EntryType = entryType;
            TabIndent = tabIndent;
        }

        public string Message { get; private set; }
        public ApplicationLogger.EntryTypes EntryType { get; private set; }
        public int TabIndent { get; private set; }
    }
}
