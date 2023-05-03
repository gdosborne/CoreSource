//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc. All rights reserved.
// Written by:	Omar Chughtai
//-------------------------------------------------------------------
//
// Summary: Channel Enumerator Service is used for getting Channels from historical database by enumerator object.
// Namespace: SNC.OptiRamp.Services.TrendDataProcessing 
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using SNC.OptiRamp.Services.fDatabase;
using SNC.OptiRamp.Services.fDefs;

namespace SNC.OptiRamp.Services.TrendDataProcessing
{
    public class CreateChannelsEnumRequest : DatabaseDescriptor
    {        
        public string Filter { get; set; }
    }
    
    public class CreateChannelsEnumResponse : DatabaseDescriptor
    {
        public CreateChannelsEnumResponse(CreateChannelsEnumRequest request)
        {
            this.ServerAddress = request.ServerAddress;
            this.Database = request.Database;
            this.DatabaseType = request.DatabaseType;
            this.Filter = request.Filter;
            this.ceHandle = -1;
        }
        public string Filter { get; private set; }
        public int ceHandle { get; set; }
    }

    public class GetChannelsEnumRequest : DatabaseDescriptor
    {
        public int ceHandle { get; set; }
        public int numberOfRecords { get; set; }
    }
    
    public class GetChannelsEnumResponse : DatabaseDescriptor
    {
        public GetChannelsEnumResponse(GetChannelsEnumRequest request)
        {
            this.ServerAddress = request.ServerAddress;
            this.Database = request.Database;
            this.DatabaseType = request.DatabaseType;
            this.ceHandle = request.ceHandle;
        }
        public int ceHandle { get; private set; }
        public IEnumerable<ChannelItem> ChannelItems { get; set; }
    }  

    public class ChannelsEnumRequest : DatabaseDescriptor
    {
        public int ceHandle {get; set;}
    }
    
    public class SortChannelEnumRequest : DatabaseDescriptor
    {
        public int ceHandle { get; set; }
        public bool direction {get; set;}
    }
       
    public interface IChannelsEnumerator
    {   
        /// Every request includes database info to perform operations on specific database
       
        /// <summary>
        /// Creates Enumerator object
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Status"></param>
        /// <returns>CreateChannelEnumResponse object
        /// </returns>
        CreateChannelsEnumResponse CreateChannelsEnum(CreateChannelsEnumRequest request, out ResponseStatus Status); 
        
        /// <summary>
        /// Returns GetRecordsChannelResponse object with array of channel names.
        /// The channels are collected from the enumerator array starting with 
        /// current enumerator postion for the requested number of records.
        /// The enumerator is moved to next position after request.
        /// If number of requested records starting from current enumerator position is exceeding the enumerator array length
        /// the only remain number of channels returned. Enumerator will be positioned to next position after array. 
        /// The Reset function can be used to move the enumerator to the initial position.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        GetChannelsEnumResponse GetChannelsEnum(GetChannelsEnumRequest request, out ResponseStatus Status);

        /// <summary>
        /// Resets the enumerator to the zero position.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Status"></param>
        void ResetChannelsEnum(ChannelsEnumRequest request, out ResponseStatus Status);

        /// <summary>
        /// Sorts the enumerator array in asc(false) or desc(true) order based on request.direction parameter. 
        /// Resets the enumerator to the zero position.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Status"></param>
        void SortChannelsEnum(SortChannelEnumRequest request, out ResponseStatus Status);
        
        /// <summary>
        /// Deletes the enumerator object.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Status"></param>
        void DeleteChannelsEnum(ChannelsEnumRequest request, out ResponseStatus Status);
    }

}
