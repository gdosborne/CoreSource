//-------------------------------------------------------------------
// (©) 2016 Statistics & Control Inc. All rights reserved.
// Created by:	Alex Novitskiy
//-------------------------------------------------------------------

// Summary: Version Control Interface

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SNC.OptiRamp.Services.fVersionControl
{
    /// <summary>
    /// Revision info data
    /// </summary>
    public class RevisionInfo
    {                                           // examples
        public string User { get; set; }        // "Alex"
        public string Changset { get; set; }    // <revision:changset> for example "7:e5aaa1920bbf"
        public string Description { get; set; } // "some bug fixed"
        public string Date { get; set; }        // "Web Jul 13 13:45:58 2016 -0500"
        public string Tag { get; set; }         //  
    }

    /// <summary>
    /// Version Control interface functions
    /// </summary>
    public interface IVersionControl
    {
        /// <summary>
        /// Adds file to repository
        /// </summary>
        /// <param name="file">File name in project folder</param>
        /// <param name="error"></param>
        /// <returns>true for success</returns>
        bool AddFile(string file, out string error);
        /// <summary>
        /// Removes file from repository
        /// </summary>
        /// <param name="file">file name in repository</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool ForgetFile(string file, out string error);
        /// <summary>
        /// Commits changes to repository
        /// </summary>
        /// <param name="description">summary text</param>
        /// <param name="user">user name</param>
        /// <param name="error"></param>
        /// <returns>"revision:changset"</returns>
        string Commit(string description, string user, out string error); // user
        /// <summary>
        /// Retrieves repository data to working folder
        /// </summary>
        /// <param name="revision">revision to retrieve</param>
        /// <param name="file">if file is not empty - retrieves only the file</param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool Update(RevisionInfo revision, string file, out string error);
        /// <summary>
        /// Gets the history of revisions
        /// </summary>
        /// <param name="file">if file is not empty it returns a revision history for the file</param>
        /// <param name="startTime">if start time != DateTime.MinValue it is used to get revisions after this time</param>
        /// <param name="counter">how many last revisions to return. If counter=1 it returns last revision. If counter=0 it returns all revisions</param>
        /// <param name="error"></param>
        /// <returns>Collection of revision info</returns>
        IEnumerable<RevisionInfo> GetRevisions(string file, DateTime startTime, int counter, out string error); // filter by file name
    }
}
