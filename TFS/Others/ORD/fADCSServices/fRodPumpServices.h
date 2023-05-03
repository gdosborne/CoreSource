//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Written by:	Alex Novitskiy
//-------------------------------------------------------------------

//
// Definitions of the interfaces for SNC::OptiRamp::Services::RodPump
//
#pragma once

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace RodPump
      {
        ///
        /// callback function signature to notify a client about problems during calculation
        ///
        public delegate void MathNotificationHandler(System::String^ message, bool error);
        
      
        ///
        /// dynocard, variables and status
        ///
        public ref struct RodPumpResult sealed
        {
          SNC::OptiRamp::Services::fDefs::ResponseStatus^ status;
          DynoCardRTData^ dataCalculated;
        };
        

        /// ---------------------------------------------------------------------------------------
        ///
        /// interface IRodPumpMath definition
        ///
        public interface class IRodPumpMath
        {
          RodPumpResult^ Calculate(const bool % stop, DynoCardRTData^ inDynoCard);
          //
          event MathNotificationHandler^ NotificationEvent;
        };
        /// ---------------------------------------------------------------------------------------

        //
        //  fDynocardComputation
        //
        public ref struct fDynocardComputation sealed : DynocardComputation
        {
          virtual System::String ^ MotorDynamicsMinTimeRange( double tn, double % MinTimeRangeSec );

          //
          virtual MotorDynamicsComputer ^ MotorDynamics(
              double tn, 
              System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fRT::RTDataDouble^> ^ data, 
              bool % bStop,
              System::String ^ % sError
              );
          //
          virtual System::Collections::Generic::IEnumerable<System::Windows::Point> ^ MotorDynaCard(
            double tn,
            System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fRT::RTDataDouble ^> ^ dataPosition,
            System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fRT::RTDataDouble ^> ^ dataTorque,
            bool % bStop,
            System::String ^ % sError
            );

        }; // .................................... fDynocardComputation ............................

      } // .................................. namespace RodPump ................................
    } // ................................... namespace Services .......................
  } // .................................... namespace OptiRamp .......................
} // ..................................... namespace SNC .........................
