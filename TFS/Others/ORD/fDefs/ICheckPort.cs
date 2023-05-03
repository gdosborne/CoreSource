//-------------------------------------------------------------------
// © 2015 Statistics & Control Inc.  All rights reserved.
// Created by:	Rex Gray
//-------------------------------------------------------------------
//
// Interfaces for TCP/IP utility methods.
//
#region Using Directives (.NET)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
#endregion

#region Using Directives (SNC)
#endregion

namespace SNC.OptiRamp.Services.fTcpUtils
{
    /// <summary>
    /// Test TCP/IP communication by attempting to open a port.
    /// </summary>
    public interface ICheckPort
    {
        /// <summary>
        /// Simple check for communication with a remote computer.
        /// </summary>
        /// <param name="address">IP address or host computer name.</param>
        /// <param name="port">IP port number.</param>
        /// <param name="timeout">Time out period in milliseconds.</param>
        /// <param name="error">Error message.</param>
        /// <returns>True indicates that the remote computer can be communicated with on the default port.
        /// False indicates the remote computer CANNOT be communicated with.</returns>
        bool TestPort(string address, int port, int timeout, out string error);
    }
}
