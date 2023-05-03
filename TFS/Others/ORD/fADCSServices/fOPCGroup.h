#pragma once
//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by: Jeff Shafferman
//-------------------------------------------------------------------

//
// Implementation of fOPCGroup
//

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace fOPC
      {
        ref struct fOPCClient;

        //
        // *** fOPCGroup ***
        //
        private ref struct fOPCGroup sealed : IOPCGroup
        {
          fOPCGroup(fOPCClient ^ client_);
          //
          virtual SNC::OptiRamp::Services::fRT::IRTItem ^ AddItem(
            System::String                                            ^   itemName, 
            [System::Runtime::InteropServices::Out] System::String    ^ % error
            );
          //
          virtual SNC::OptiRamp::Services::fRT::IRTItem ^ AddItemStr(
            System::String                                                ^ itemName,
            [ System::Runtime::InteropServices::Out ] System::String    ^ % error
            );
        private:
          fOPCClient^ const client;
          //
          SNC::OptiRamp::Services::fRT::IRTItem ^ AddItemEx(
            System::String                                                ^ itemName,
            bool                                                            bIsString,
            [ System::Runtime::InteropServices::Out ] System::String    ^ % error
            );
        }; // ...................................... fOPCGroup ..............................
      }
    }
  }
}


