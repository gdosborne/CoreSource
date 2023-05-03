#include "fDS.h"
#include "..\..\common\DS\DS_Control.h"
#include "..\..\common\DS\DS_App_Status.h"
#include "..\..\common\Utils_Main.h"

#include <crtdbg.h>


namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      //
      // statics
      //
      private ref struct C_DS_App_Status abstract sealed
      {
        static int iInit_App_Status = 0;
        static aFW::I_DS_App_Status_Register ^ pDS_App = nullptr;

        //
        // On_Handle_Created
        //
        static void On_Handle_Created( System::Object ^, System::EventArgs ^ )
        {
          try
          {
            if ( iInit_App_Status == 0 )
            {
              _ASSERT( pDS_App == nullptr );
              pDS_App = aFW::DS_App_Status_Register( nullptr );
            } else {
              _ASSERT( pDS_App != nullptr );
            }
            ++iInit_App_Status;
          } catch ( System::Exception ^ pErr ) {
            aFW::Silent_Exception( pErr );
          }
        } // ........................... On_Handle_Created ...........................

        //
        // On_Handle_Destroyed
        //
        static void On_Handle_Destroyed( System::Object ^, System::EventArgs ^ )
        {
          try
          {
            --iInit_App_Status;
            if ( iInit_App_Status == 0 )
            {
              delete pDS_App; pDS_App = nullptr;
            } else {
              _ASSERT( iInit_App_Status > 0 );
            }
          } catch ( System::Exception ^ pErr ) {
            aFW::Silent_Exception( pErr );
          }
        } // ........................... On_Handle_Destroyed ....................
      };

      //
      // fDS
      //
      public ref struct fDS sealed : IDS
      {
        virtual System::Windows::Forms::Control ^ Create_DS_Control()
        {
          aFW::C_Control_DS ^ pRet = gcnew aFW::C_Control_DS;
          pRet->HandleCreated += gcnew System::EventHandler( &C_DS_App_Status::On_Handle_Created );
          pRet->HandleDestroyed += gcnew System::EventHandler( &C_DS_App_Status::On_Handle_Destroyed );
          return pRet;
        } // ......................... Create_DS_Control ................
      };

    } // namespace Services
  } // namespace OptiRamp
} // namespace SNC
