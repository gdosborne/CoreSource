//-------------------------------------------------------------------
// (©) 2015 Statistics & Control Inc. All rights reserved.
// Created by:	Alex Novitskiy
//-------------------------------------------------------------------

// Summary:
//      Definition for IVTSItem interface
//      Namespace: SNC.OptiRamp.Applications.VTS
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using SNC.OptiRamp.Services.fRT;

namespace SNC.OptiRamp.Applications.VTS
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVTSItem
    {
        string ID                   { get; }
        IRTItemData rtItem          { get; }
        object Data                 { get; set; }
        RTDataDouble DataSpanOffset { get; }
    }

    /// <summary>
    /// Interface for producing unique IDs.
    /// </summary>
    public interface IVTSUniqueID
    {
        /// <summary>
        /// Initialize a unique ID generator for the project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="lastID">The last unique ID found in the project file when it is parsed.</param>
        void Init(string projectName, uint lastID);

        /// <summary>
        /// Generate a unique ID for the corresponding project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>A generated, unique ID value associated with the project.</returns>
        uint GetNewUID(string projectName);
    }
}
