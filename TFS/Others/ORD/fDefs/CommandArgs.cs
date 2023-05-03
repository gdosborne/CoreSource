//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by:	Rex Gray
//-------------------------------------------------------------------
//
// Definitions of command line argument objects.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using System.Xml;
using SNC.OptiRamp.Services.fDefs;

namespace SNC.OptiRamp.Services.fCommandArgs
{
    /// <summary>
    /// Definition of a command line argument.
    /// </summary>
    public struct CmdLineArgDef
    {
        public char arg;
        public string name;
        public bool isOptional;
        public bool hasSecondArg;
    }

    /// <summary>
    /// Interface for handling command line arguments.
    /// </summary>
    public interface ICommandArgs
    {
        CmdLineArgDef[] GetArgDefsFromAssembly(Assembly assem, out ResponseStatus status);
        string[] ParseCommandLineArguments(string[] args, CmdLineArgDef[] paramDefs, out ResponseStatus status);
    }
}
