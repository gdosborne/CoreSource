//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by:	Alex Novitskiy
//-------------------------------------------------------------------
//
// Definitions of general classes, structures, enums and interfaces for OptiRamp ADCS framework
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNC.OptiRamp.Services.fDefs
{
    /// <summary>
    /// The standard response object that contains the final result
    /// of interactions with a component.
    /// </summary>
    public class ResponseStatus
    {
        public ResponseStatus()
        {
            SpentTime = new TimeSpan(0);
            Message = string.Empty;
            IsError = false;
        }
        public TimeSpan SpentTime { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
    }
    
}
