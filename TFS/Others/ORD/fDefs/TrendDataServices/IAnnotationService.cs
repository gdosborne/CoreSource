//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Written by:	Greg Osborne and Alex Novitskiy
//-------------------------------------------------------------------

//
// This file contains definitions and descriptions of IAnnotationService interface
// Namespace: SNC.OptiRamp.Services.TrendDataProcessing
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using SNC.OptiRamp.Services.fDatabase;
using SNC.OptiRamp.Services.fDefs;

namespace SNC.OptiRamp.Services.TrendDataProcessing
{
    public class AnnotationItem
    {
        public string AnnotationText    { get; set; }
        public DateTime DateTimeUTC     { get; set; }

        /// <summary>
        /// The annnotation type strings obtained initially from project. 
        /// Every annotation type can be associated in project with other attributes like a color and so on.
        /// If there is no match between the type from database and project, the default attributes will be used.
        /// </summary>
        public string AnnotationType    { get; set; } 
    }
    public class AnnotationRequest : DatabaseDescriptor // DatabaseDescriptor defined in ITrendingDataService.cs file as part of ITrendingDataService interface
    {
        public DateTime StartDateUTC    { get; set; }
        public DateTime EndDateUTC      { get; set; }
        public string   PageName        { get; set; }
    }
    public class AnnotationResponse : DatabaseDescriptor
    {
        public AnnotationResponse(AnnotationRequest request)
        {
            this.ServerAddress = request.ServerAddress;
            this.Database = request.Database;
            this.DatabaseType = request.DatabaseType;
            this.PageName = request.PageName;
        }
        public string PageName { get; set; }
        public ObservableCollection<AnnotationItem> AnnotationItems { get; set; }
    }
    public class AnnotationSaveRequest : DatabaseDescriptor
    {
        public DateTime DateTimeUTC         { get; set; }
        public string PageName              { get; set; }
        public AnnotationItem Annotation    { get; set; }
    }
    public class AnnotationDeleteRequest : DatabaseDescriptor
    {
        public DateTime DateTimeUTC { get; set; }
        public string PageName      { get; set; }
    }

    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    public interface IAnnotationService
    {
        // Every request includes database info to perform operations on specific database

        //
        // Syncronous request to get all annotations between t1 and t2 for specific WA page.
        // Response includes all database info and collection of annotation items.
        // Returns the empty collection if there is no annotation found.
        //
        AnnotationResponse GetAnnotations(AnnotationRequest request, out ResponseStatus Status);
        
        //
        // Asyncronous request to save annotation item in database.
        // Annotation item contains timestamp, name of page and annotation string.
        //
        void SaveAnnotation(AnnotationSaveRequest request, out ResponseStatus Status);
        
        //
        // Asyncronous request to delete annotation item from database.
        // Request parameters are timestamp and name of page.
        //
        void DeleteAnnotation(AnnotationDeleteRequest request, out ResponseStatus Status);
    }
}

