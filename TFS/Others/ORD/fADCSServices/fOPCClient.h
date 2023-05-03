#pragma once
//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by: Jeff Shafferman
//-------------------------------------------------------------------

//
// Implementation of fOPCClient
//

#include "..\..\common\OPC_DA_Client_WT\OPC_Client.h"
#include "fOPCEngine.h"

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace fOPC
      {
        ref struct fOPCItem;

        private ref struct fOPCClient sealed : IOPCConnection
        {
          fOPCClient(fOPCEngine^ engine_, System::String^ MachineName, System::String^ ServerName);

          virtual SNC::OptiRamp::Services::fRT::IRTGroup^ OpenGroup(System::String^ OPCgroupName, int msecRefreshRate, [System::Runtime::InteropServices::Out] System::String^% error);

        internal:
          aOPC::C_DA_Client* client;

          void addItem(System::String^ OPCItemName, fOPCItem^ OPCItem);
          void writeValue(int ID, double value, System::String^% error);
          void writeValue(int ID, System::String^ value, System::String^% error);

        protected:
          !fOPCClient();
          ~fOPCClient();

        private:
          aOPC::C_DA_Client::C_Group_ID groupID;
          fOPCEngine^ const engine;
          typedef System::Collections::Generic::List<fOPCItem^> C_opcItemList;
          C_opcItemList ^ opcItemList;

          void addToEngineClientList();
          void removeOPCItemFromEngineDictionary();
          void removeFromEngineClientList();
        };
      }
    }
  }
}