// Guids.cs
// MUST match guids.h
using System;

namespace GregOsborne.AppDocumentation
{
    static class GuidList
    {
        public const string guidAppDocumentationPkgString = "5be12f71-5129-4a04-9bc5-bf82a4b8b858";
        public const string guidAppDocumentationCmdSetString = "acc56d04-7630-4e48-b525-71e252ba81de";

        public static readonly Guid guidAppDocumentationCmdSet = new Guid(guidAppDocumentationCmdSetString);
    };
}
