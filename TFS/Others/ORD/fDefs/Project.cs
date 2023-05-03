//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by:	Alex Novitskiy
//-------------------------------------------------------------------
//
// Definitions of classes, structures, enums and interfaces for fProject
//

using System;
using System.Collections.Generic;
using SNC.OptiRamp.Services.fRT;
using SNC.OptiRamp.Services.fDatabase;

namespace SNC.OptiRamp.Services.fDefs
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectEventInfo
    {
        public bool IsError     { get; set; }
        public string Message   { get; set; }
        public int Progress     { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ProjectConstants
    {
      public static readonly int invalidHandle = -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ni"></param>
    public delegate void ProjectNotify(ProjectEventInfo ei);

    /// <summary>
    ///     Types of Input/Output channels
    /// </summary>
    public enum IOType { AnalogIn, AnalogOut, DigitalIn, DigitalOut };

    /// <summary>
    ///     Interface provides access to EU data
    /// </summary>
    public interface IProjectEU
    {
        string Text          { get; }
        string Name          { get; set; }
        IProjectEU baseEU    { get; }

        /// <summary>
        /// Returns value as formatted string for given project EU
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string GetFormattedValue(double value);
    }

    /// <summary>
    ///     Interface provides basic properties for data channel
    /// </summary>
    public interface IDataChannel
    {
        /// <summary>
        ///     Long description or comments for data channel, for example "water temperature at base of well etc..."
        /// </summary>
        string Description      { get; set; }
        /// <summary>
        ///     Name of data channel, for example "WeelHead Pressure"
        /// </summary>
        string Name             { get; set; }
        /// <summary>
        ///     Customer tag - unique customer id per plant(s)
        /// </summary>
        string CustomerTag      { get; set; }
        /// <summary>
        ///     Max value, nullabel
        /// </summary>
        double? Min             { get; set; }
        /// <summary>
        ///     Min value, nullable
        /// </summary>
        double? Max             { get; set; }
        /// <summary>
        ///     reference to EU interface providing EU info. 
        ///     CAN BE NULL if not availale for specific data channel
        /// </summary>
        IProjectEU EU           { get; set; }
        /// <summary>
        /// see \\192.168.0.100\Engineering\Projects\Development\Archive System Design\Docs\Archive System Design.pdf
        /// </summary>
        bool NCUType { get; set; }
        /// <summary>
        /// see \\192.168.0.100\Engineering\Projects\Development\Archive System Design\Docs\Archive System Design.pdf
        /// </summary>
        bool DiscreteType { get; set; }
    }

    /// <summary>
    ///     IO Channel interface
    ///     In addition to IDataChannel properties provides the IO Type and initial value.
    /// </summary>
    public interface IIOChannel : IDataChannel
    {
        IOType type             { get; set; }
        double? initValue       { get; set; }
    }

    //---------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    ///  Elemnet Type class
    /// </summary>
    public interface IElemType // C_Project_Node_Type
    {
        string TypeID { get; }
        string Name { get; } 
    }

    /// ------------------------------------- Property interfaces ---------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    public interface IProperty
    {
        string Name { get; }
        object rowValue { get; set; }
    }
    /// <summary>
    ///  
    /// </summary>
    public interface IPropertyInt : IProperty
    {
        int? Value { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IPropertyString : IProperty
    {
        string Value { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IPropertyBool : IProperty
    {
        bool Value { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IPropertyDouble : IProperty
    {
        double? Value { get; set; }
        IProjectEU EU { get; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IPropertyBitmap : IProperty
    {
        System.Drawing.Bitmap Value { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IPropertyDateTime : IProperty
    {
        System.DateTime Value { get; set; }
    }
    
    /// <summary>
    /// Peer structure
    /// </summary>
    public struct Peer
    {
        public int elemID;     // element ID
        public string peerID;  // meaning depends on type of connection
    }
    /// ------------------------------------- IElement interface ---------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    public interface IElement // C_Project_Node
    {
        /// <summary>
        /// 
        /// </summary>
        int Id { get;  } // index in the allElements project array
        
        /// <summary>
        /// 
        /// </summary>
        string Name { get;  } // short name: Ps
        
        /// <summary>
        /// 
        /// </summary>
        string Description { get; set; } // long name: Pressure on the top of main water tower
        
        /// <summary>
        /// 
        /// </summary>
        string Tag { get; } // customer (project specific) tag: P102XYZ
        
        /// <summary>
        /// 
        /// </summary>
        IElemType Type { get; }
        
        /// <summary>
        /// 
        /// </summary>
        IDictionary<string, IProperty> Properties { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        IElement Parent { get; }
        
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<int> Children { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<Peer> Peers { get; set; }

        /// <summary>
        ///     Returns collection of dataChannels (webChannels, IOChannels, etc...)
        /// </summary>
        IEnumerable<IDataChannel> dataChannels { get; set; }

        /// <summary>
        ///     Returns enmumerator interface for children 
        /// </summary>
        /// <returns>
        ///     Returns IElement[]
        /// </returns>
        IEnumerable<IElement> GetChildren();
        
        /// <summary>
        ///     Looking for parent in the elements tree hierarchy until it
        ///     finds the parent of specified type
        /// </summary>
        /// <param name="type">
        ///     Type of parent
        /// </param>
        /// <returns>
        ///     Returns the parent element of specified type or null
        /// </returns>
        IElement GetParentOfType(IElemType parentType);

    }

    public interface IVTSElement : IElement
    {
        new string Name
        {
            get;
            set;
        }
        IRTItemData rtItemData { get; set; }
    }

    /// ------------------------------------- IProject interface ---------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    public interface IProject
    {
        /// <summary>
        ///     Full path to the project including file name.
        ///     For example: c:\adcs\projects\rodpump\rodpump1.xml
        /// </summary>
        string Path { get; }

        /// <summary>
        ///     Root element of the project
        /// </summary>
        IElement Root { get; }

        /// <summary>
        ///     Computer designated as active in the project. Available when project is open. Otherwise = null.
        /// </summary>
        IElement ActiveComputer { get; }

        /// <summary>
        ///     Returns reference to the element that is saved in internal project storage
        /// </summary>
        /// <param name="ElemId">
        ///     Id of element that is index in the internal project array
        /// </param>
        /// <returns>
        ///     Reference to the element
        /// </returns>
        IElement GetElemById(int ElemId);       // can return null

        /// <summary>
        ///     Indexer 
        ///     Example of use:  Element myElem = Project[1034];  // etc...
        /// </summary>
        /// <param name="index">
        ///     Index in all elements array
        /// </param>
        /// <returns>
        ///     Can throw execption if index out of range
        /// </returns>
        IElement this[int Id] { get; set; }  // 
        
        /// <summary>
        ///     The function starts parsing the project
        /// </summary>
        /// <param name="file">
        ///     Project file name including path. Saved in the Path property
        /// </param>
        /// <param name="options">
        ///     Options is dictionary of pairs <option name, option value> 
        /// </param>
        /// <param name="status">
        ///     Status of operation
        /// </param>
        /// <returns>
        ///     Return null if there are errors or Main Computer
        /// </returns>
        IElement Open(string file, IDictionary<string,string> options, out ResponseStatus status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        bool Save(out ResponseStatus status);

        /// <summary>
        ///     Indicates if project is open.
        /// </summary>
        /// <returns>
        ///     Returns True if project open, False if not.
        /// </returns>
        bool IsOpen();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     returns Max of elements in allElements array
        /// </returns>
        int GetElementsCount();

        /// <summary>
        ///     Returns enumerator for collection of elements of specified type
        /// </summary>
        /// <param name="elemType">
        /// </param>
        /// <returns>
        /// </returns>
        IEnumerable<IElement> GetElemsOfType(IElemType elemType);

        /// <summary>
        ///     Returns Type of Element
        /// </summary>
        /// <param name="Tag">
        ///     TypeID - the inner name of elem type
        /// </param>
        /// <returns></returns>
        IElemType GetElemType(string TypeID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="properties"></param>
        /// <param name="error"></param>
        /// <returns>
        /// returns ProjectConstants.invalidHandle if AddElement fails and error with details
        /// </returns>
        int AddElement(int parentId, string name, IElemType type, string description, IDictionary<string, IProperty> properties, out string error);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectHandle"></param>
        /// <param name="elementId"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool DeleteElement(int elementId, out string error);

        /// <summary>
        ///     Event is fired when component needs to inform a client of progress or errors of parsing  
        /// </summary>
        event ProjectNotify eventNotify;
    }

    public interface IProject<T> where T : IElement
    {
        /// <summary>
        ///     Full path to the project including file name.
        ///     For example: c:\adcs\projects\rodpump\rodpump1.xml
        /// </summary>
        string Path { get; }

        /// <summary>
        ///     Root element of the project
        /// </summary>
        T Root { get; }

        /// <summary>
        ///     Computer designated as active in the project. Available when project is open. Otherwise = null.
        /// </summary>
        T ActiveComputer { get; }

        /// <summary>
        ///     Returns reference to the element that is saved in internal project storage
        /// </summary>
        /// <param name="ElemId">
        ///     Id of element that is index in the internal project array
        /// </param>
        /// <returns>
        ///     Reference to the element
        /// </returns>
        T GetElemById(int ElemId);       // can return null

        /// <summary>
        ///     Indexer 
        ///     Example of use:  Element myElem = Project[1034];  // etc...
        /// </summary>
        /// <param name="index">
        ///     Index in all elements array
        /// </param>
        /// <returns>
        ///     Can throw execption if index out of range
        /// </returns>
        T this[int Id] { get; set; }  // 

        /// <summary>
        ///     The function starts parsing the project
        /// </summary>
        /// <param name="file">
        ///     Project file name including path. Saved in the Path property
        /// </param>
        /// <param name="options">
        ///     Options is dictionary of pairs <option name, option value> 
        /// </param>
        /// <param name="status">
        ///     Status of operation
        /// </param>
        /// <returns>
        ///     Return null if there are errors or Main Computer
        /// </returns>
        T Open(string file, IDictionary<string, string> options, out ResponseStatus status);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        bool Save(out ResponseStatus status);

        /// <summary>
        ///     Indicates if project is open.
        /// </summary>
        /// <returns>
        ///     Returns True if project open, False if not.
        /// </returns>
        bool IsOpen();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     returns Max of elements in allElements array
        /// </returns>
        int GetElementsCount();

        /// <summary>
        ///     Returns enumerator for collection of elements of specified type
        /// </summary>
        /// <param name="elemType">
        /// </param>
        /// <returns>
        /// </returns>
        IEnumerable<T> GetElemsOfType(IElemType elemType);

        /// <summary>
        ///     Returns Type of Element
        /// </summary>
        /// <param name="Tag">
        ///     TypeID - the inner name of elem type
        /// </param>
        /// <returns></returns>
        IElemType GetElemType(string TypeID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="properties"></param>
        /// <param name="error"></param>
        /// <returns>
        /// returns ProjectConstants.invalidHandle if AddElement fails and error with details
        /// </returns>
        int AddElement(int parentId, string name, IElemType type, string description, IDictionary<string, IProperty> properties, out string error);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectHandle"></param>
        /// <param name="elementId"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool DeleteElement(int elementId, out string error);

        /// <summary>
        ///     Event is fired when component needs to inform a client of progress or errors of parsing  
        /// </summary>
        event ProjectNotify eventNotify;
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDeviation
    {
        int TagFolderID { get; set; } // folder to record
        string TagFolderPath { get; set; }
        double? DValuePercent { get; set; }
        bool State { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ConditionType { none, equal, higher, lower }

    /// <summary>
    /// 
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// Properties
        /// </summary>
        ConditionType Type      { get; }            // ConditionType: lower, higher, ...
        int[] ArchiveIdFolders  { get; set; }       // folders to record
        string[] ArchiveFolders { get; set; }       // folders to record
        int UId                 { get; }            // unique Id for condition element
        string Text             { get; set; }       // OR text by user: Pressure before inlet in turbine > 110 kPa.
                                                    // OR text by user with {} fileds: Pressure before inlet in turbine {Type} {Sp} {EU}.
                                                    // OR empty. In this case the text is created as: {tagDescription} {Type} {Sp} {EU}. For example: Wellhead Pressure > 100.00 kPa
        string TextOff          { get; }            // optional, if not empty is used for state = cleared.
        double? Sp              { get; }
        double? DeadBand        { get; }
        double? Delay           { get; }
        double? Priority        { get; }
        ConditionStatus State   { get; set; }      // ConditionState: active, cleared, ackn, ...
        DateTime LastChange     { get; set; }
        bool IsEvent            { get; set; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public interface IArchiveTriggersForCondition
    {
        int conditionID { get; set; }
        List<int> archiveTriggers { get; set; }
    }    
    
    // Needed to allow IRemoteProject to use more specialized version of IElement
    public interface IVtsProject<T> : IProject<T> where T : IElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="properties"></param>
        /// <param name="error"></param>
        void CreateTag(TagInfo info, TagProperties properties, out string error);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condPath"></param>
        /// <param name="atc"></param>
        /// <returns></returns>
        bool CheckConditionsForArchiveTriggers(string condPath, out IArchiveTriggersForCondition atc);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="dev"></param>
        /// <returns></returns>
        bool CheckDeviation(int folderId, out IDeviation dev);
    }

    public class TagInfo
    {
        public string ServerName;
        public string GroupName;
        public string FolderPath;
        public string Name;
    }

    public class TagProperties
    {
        public string Description;
        public string OpcItemName;
        public string EU;
        public double? Span;
        public double? Offset;
    }
    //----------------------------------------------------------------------------------------------------------
}
