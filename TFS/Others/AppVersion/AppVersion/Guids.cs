// Guids.cs
// MUST match guids.h
using System;

namespace GregOsborne.AppVersion
{
    static class GuidList
    {
        public const string guidAppVersionPkgString = "05951645-5750-45e4-a017-c4144ea5d0d0";
        public const string guidAppVersionCmdSetString = "e5fda461-873a-476c-b65e-4104e6264dd3";
        public const string guidToolWindowPersistanceString = "b1aaf479-8dbd-483a-988e-6bb253118271";

        public static readonly Guid guidAppVersionCmdSet = new Guid(guidAppVersionCmdSetString);
    };
}
