#pragma once
//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by: Jeff Shafferman
//-------------------------------------------------------------------

//
// Implementation of fOPCItem
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
        //  fOPCItem
        //
        private ref struct fOPCItem sealed : IOPCItem
        {
          fOPCItem(fOPCClient^ client_, bool bIsString_);
          const int ID;
          const bool bIsString;

          virtual void Write(double value, [System::Runtime::InteropServices::Out] System::String^% error);
          virtual void Write(System::String^ value, [System::Runtime::InteropServices::Out] System::String^% error);

          virtual bool LastValue(
            [System::Runtime::InteropServices::Out] System::String ^ % value, 
            [System::Runtime::InteropServices::Out] System::DateTime % time, 
            [System::Runtime::InteropServices::Out] __int64 % quality);
          
          property bool Active
          {
            virtual bool get();
            virtual void set( bool bNew );
          }

        internal:
          // >>>
          static System::Object  oProtect;
          bool                bHas_Value;
          System::String    ^ sLast_Value;
          System::DateTime    tLast_Time;
          __int64             iLast_Quality;
          // <<<
          bool                bActive;
        private:
          static int          iNextItemID = 0;
          fOPCClient  ^ const client;
        }; // ....................................... fOPCItem ................................
      }
    }
  }
}