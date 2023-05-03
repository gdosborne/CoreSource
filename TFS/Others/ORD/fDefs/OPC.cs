//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by:	Ilya Markevich
//-------------------------------------------------------------------
//
// Definitions of classes, structures, enums and interfaces for fOPC
//

using System;
using System.Collections.Generic;

using SNC.OptiRamp.Services.fRT;

namespace SNC.OptiRamp.Services.fOPC
{
   /// <summary>
   /// 
   /// </summary>
   public interface IOPCItem : IRTItem
   {
       void Write(double value, out string error);
       void Write(string value, out string error);
   }

   /// <summary>
   ///     OPC group
   ///     
   /// 
   /// </summary>
   public interface IOPCGroup : IRTGroup
   {
      IRTItem AddItemStr(string itemTag, out string error);
   }

   /// <summary>
   ///     OPC client
   /// </summary>
   public interface IOPCConnection : IRTConnection
   {
   }

   /// <summary>
   ///     OPC engine, created and destroyed once
   /// </summary>
   public interface IOPCEngine : IRTEngine
   {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="MashineName">  can be null for local computer </param>
       /// <param name="error"> </param>
       /// <returns>
       /// returns list of OPC servers (using registry for local computer) and
       /// OPCenum for remote.
       /// </returns>
       IEnumerable<string> GetOPCservers(string MachineName, out string error);
   }
}