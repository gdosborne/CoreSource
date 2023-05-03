// <copyright file="LogMessageEventArgs.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/17/2020</date>

namespace Toolbar.Controller {
    using System;

    using GregOsborne.Application.Logging;

    public delegate void LogMessageHandler(object sender, LogMessageEventArgs e);
    public class LogMessageEventArgs : EventArgs {
        public LogMessageEventArgs(string message, ApplicationLogger.EntryTypes entryType, bool isCritcal) {
            Message = message;
            EntryType = entryType;
            IsCritical = isCritcal;
        }

        public ApplicationLogger.EntryTypes EntryType { get; private set; } = default;

        public bool IsCritical { get; private set; } = default;

        public string Message { get; private set; } = null;
    }
}
