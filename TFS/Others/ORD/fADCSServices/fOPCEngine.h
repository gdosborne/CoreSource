#pragma once
//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by: Jeff Shafferman
//-------------------------------------------------------------------

//
// Implementation of fOPCEngine
//
#include "..\..\common\DA\DA_Client_I.h"

namespace Unmanaged
{
  struct C_OPC_Engine_Unmanaged;
  struct C_OPC_ItemCallBack_Unmanaged;
}

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace fOPC
      {
        ref struct fOPCClient;
        ref struct fOPCItem;

        //
        //
        //
        public ref struct fOPCEngine sealed : IOPCEngine
        {
          static fOPCEngine();
          fOPCEngine(
            SNC::OptiRamp::Services::fDiagnostics::IOptiRampLog       ^ logService_, 
            System::String                                            ^ sAppName,
            SNC::OptiRamp::Services::fDiagnostics::IThreadsWatchdog   ^ pThreadsWatchdog  // can be nullptr
            );

          // ----------------- IRTEngine ---------------------
          //
          virtual fRT::IRTConnection^ OpenClient(
            System::String                                            ^ machineName, 
            System::String                                            ^ serverName, 
            [System::Runtime::InteropServices::Out] System::String  ^ % error
            );
          //
          virtual void Start(bool bSyncRead, [System::Runtime::InteropServices::Out] System::String^% error);
          //
          virtual void Stop([System::Runtime::InteropServices::Out] System::String^% error);
          //
          virtual event fRT::RTItemEventHandler^ RTItemValue;
          //
          virtual event fRT::RTItemEventHandlerStr^ RTItemValueStr;

          // ------------------ IOPCEngine ----------------------------
          //
          virtual System::Collections::Generic::IEnumerable<System::String^> ^ GetOPCservers(
            System::String                                              ^ MachineName,
            [ System::Runtime::InteropServices::Out ] System::String  ^ % error
            );

        internal:
          typedef System::Collections::Generic::List<fOPCClient^> C_connectedClients;
          property C_connectedClients^ Clients
          {
            C_connectedClients ^ get();
          }

          aDA::C_DA_CallBack* GetItemCallBack();


        protected:
          !fOPCEngine();

          ~fOPCEngine();
        internal:
          System::Collections::Generic::Dictionary<int, fOPCItem^> opcItemDictionary;

          Unmanaged::C_OPC_Engine_Unmanaged* engine;
        private:
          Unmanaged::C_OPC_ItemCallBack_Unmanaged* itemCallBack;

          C_connectedClients ^ connectedClients;

          delegate void InteralItemDelegate(aDA::C_DA_Point_ID Point_ID, const VARIANT* pVar, __int64 timestamp, DWORD quality);
          InteralItemDelegate^ itemCallBackDelegate;

          void threadSafeDeleteClients();
          void InternalManagedItemCallBack(aDA::C_DA_Point_ID Point_ID, const VARIANT* pVar, __int64 timestamp, DWORD quality);
        };
      }
    }
  }
}