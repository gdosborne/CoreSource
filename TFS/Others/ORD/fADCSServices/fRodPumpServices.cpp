//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Written by:	Alex Novitskiy
//-------------------------------------------------------------------

//
// implementing the IRodPumpMath interface in RodPumpCalculation class
//
#pragma once

#include "fRodPumpServices.h"
#include "fElement.h"
#include "fProject.h"
#include "..\..\common\Oil\Config_Set_Rod_Pump.h"
#include "..\..\common\Oil\Signals_Rod_Pump.h"
#include "..\..\common\Config\Config_Set_Computer.h"

#include "..\..\common\Utils_Main_fC.h"
#include "..\..\common\Utils_Main.h"
#include "..\..\common\Utils_String.h"
#include "..\..\common\Utils_Monitor.h"
#include "..\..\common\Utils_Array.h"
#include "..\..\common\DS\DS_Engine.h"
#include "..\..\common\Utils_Alloca.h"
#include "..\..\common\Utils_LocalTime.h"
#include "..\..\common\Utils_File.h"
#include "..\..\common\Utils_Native.h"
#include "..\..\common\Model\Log_Proxy.h"
#include "..\..\common\Math\cobyla_last_best.h"
#include "..\..\common\Math\cobyla.h"
#include "..\..\common\Gas\Gas_Computation_Constants.h"
#include "..\common\RP\RP_Position_Comp.h"

#include <crtdbg.h>

using namespace SNC::OptiRamp::Services::fDefs;
using namespace SNC::OptiRamp::Services::fRT;

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      using namespace fDefs;
      using namespace fDiagnostics;
      using namespace fWeb;

      namespace RodPump
      {

        //
        // C_RodPump_Singleton
        //
        private ref struct C_RodPump_Singleton abstract
        {
          static System::Object  oProtect;
          IElement      ^ const pElement;
          int                   iSingleton;
          //
          C_RodPump_Singleton( IElement  ^ pElement_ ) :pElement( pElement_ ), iSingleton( 1 ) 
          { 
          }
          //
          bool First_Instance() { return iSingleton == 1;  }
        }; // ......................... C_RodPump_Singleton ....................


        //
        // C_RodPump_Reservoir_U
        //
        struct C_RodPump_Reservoir_U sealed
        {
          C_RodPump_Reservoir_U( const std::wstring & sName )
          {
            aDS::DS_Add( NULL, this, sName );
          }
          ~C_RodPump_Reservoir_U()
          {
            aDS::DS_Remove( this );
          }
        }; // ............................. C_RodPump_Reservoir_U ..................

        //
        // C_RodPump_Reservoir
        //
        private ref class C_RodPump_Reservoir sealed : C_RodPump_Singleton
        {
          //
          static System::Collections::Generic::List<C_RodPump_Reservoir^> oRodPump_Reservoirs;
        public:
          //
          C_RodPump_Reservoir_U * pInternal_R;
        private:
          //
          C_RodPump_Reservoir( IElement  ^ padElement_ ) :C_RodPump_Singleton( padElement_ ), pInternal_R(NULL) 
          { 
            oRodPump_Reservoirs.Add( this ); 
            pInternal_R = new C_RodPump_Reservoir_U( String_to_wstring(pElement->Name) );
          }
        protected:
          !C_RodPump_Reservoir()
          {
            delete pInternal_R; pInternal_R = NULL;
          }
          ~C_RodPump_Reservoir() { this->!C_RodPump_Reservoir();  }
        public:
          //
          //
          static C_RodPump_Reservoir ^ Create( IElement^ pElement_ )
          {
            for each (C_RodPump_Reservoir ^ pIter in oRodPump_Reservoirs)
            {
              if ( pIter->pElement == pElement_ )
              {
                ++pIter->iSingleton; return pIter;
              }
            }
            return gcnew C_RodPump_Reservoir( pElement_ );
          } // ............................ Create ...............................

          //
          //
          static void Remove( C_RodPump_Reservoir ^ pReservoir )
          {
            if ( pReservoir == nullptr ) { return; }
            --pReservoir->iSingleton;
            if ( pReservoir->iSingleton <= 0 )
            {
              _ASSERT( pReservoir->iSingleton == 0 );
              oRodPump_Reservoirs.Remove( pReservoir );
              delete pReservoir;
            }
          } // ........................ Remove ...................


        }; // ......................... C_RodPump_Reservoir .......................


        //
        // C_RodPump_Pad_U
        //
        struct C_RodPump_Pad_U sealed
        {
          C_RodPump_Pad_U( const std::wstring & sName, const C_RodPump_Reservoir_U * pReservoir )
          {
            aDS::DS_Add( pReservoir, this, sName );
          }
          ~C_RodPump_Pad_U()
          {
            aDS::DS_Remove( this );
          }
        }; // ............................. C_RodPump_Pad_U ..................

        //
        // C_RodPump_Pad
        //
        private ref class C_RodPump_Pad sealed : C_RodPump_Singleton
        {
          static System::Collections::Generic::List<C_RodPump_Pad^> oRodPump_Pads;
        public:
          //
          C_RodPump_Pad_U           * pInternal_P;
          C_RodPump_Reservoir ^ const pReservoir;
        private:
          //
          C_RodPump_Pad( IElement  ^ padElement_, C_RodPump_Reservoir ^ pReservoir_ 
            ) :C_RodPump_Singleton(padElement_), pReservoir(pReservoir_), pInternal_P(NULL)
          { 
            oRodPump_Pads.Add( this ); 
            pInternal_P = new C_RodPump_Pad_U( String_to_wstring( pElement->Name ), pReservoir->pInternal_R );
          }
        public:
          //
          static C_RodPump_Pad ^ Create( IElement^ pElement_Pad, C_RodPump_Reservoir ^ pReservoir_)
          {
            _ASSERT( pReservoir_ != nullptr );
            for each (C_RodPump_Pad ^ pIter in oRodPump_Pads)
            {
              if ( pIter->pElement == pElement_Pad )
              {
                ++pIter->iSingleton; 
                _ASSERT( pIter->pReservoir == pReservoir_ );
                return pIter;
              }
            }
            return gcnew C_RodPump_Pad( pElement_Pad, pReservoir_ );
          }

          //
          //
          static void Remove( C_RodPump_Pad ^ pPad )
          {
            if ( pPad == nullptr ) { return; }

            --pPad->iSingleton;
            if ( pPad->iSingleton <= 0 )
            {
              _ASSERT( pPad->iSingleton == 0 );
              oRodPump_Pads.Remove( pPad );
              delete pPad;
            }
          }

        protected:
          !C_RodPump_Pad()
          {
            delete pInternal_P; pInternal_P = NULL;
          }
          ~C_RodPump_Pad() { this->!C_RodPump_Pad();  }
        }; // ....................... C_RodPump_Pad ............................


        //
        // C_RodPump_U
        //
        struct C_RodPump_U sealed
        {
          double fLn, f_tn, fAp, fVfn, f_rho_n, fMfn, fMr, fPi, fLp, fVavg, fPwhn, fDci, fAan;
          double fAr; // rod area
          //
          C_RodPump_U(const std::wstring & sName, const C_RodPump_Pad_U * pPad
            ) :
            fLn(0), f_tn(0), fAp(0), fVfn(0), f_rho_n(0), fMfn(0), fMr(0), fPi(0), fLp(0), fVavg(0), fPwhn(0),
            fAr(0), fDci(0), fAan(0)
          {
            aDS::DS_Add( pPad, this, sName );
          }
          ~C_RodPump_U()
          {
            aDS::DS_Remove( this );
          }
        private:
          // copy/assign prohibited
          C_RodPump_U( const C_RodPump_U & );
          void operator = (const C_RodPump_U &);
        }; // ...................... C_RodPump_U ................

        //
        // C_Data_Item
        //
        struct C_Data_Item sealed
        {
          double fL, fFe, f_t, f_v, f_a;
          //
          C_Data_Item() :fL( 0 ), fFe( 0 ), f_t( 0 ), f_v(0), f_a(0) { }
        }; // ................................ C_Data_Item ................................

        //
        // C_Computational_Data
        //
        struct C_Computational_Data sealed
        {
          std::vector<C_Data_Item>  vData_Items;
          size_t                    iDown_Idx; // index of the first downstroke
          double                    fL_max;
          //
          C_Computational_Data() :iDown_Idx(0), fL_max(0)
          {}
        private:
          // copy/assign prohibited
          C_Computational_Data( const C_Computational_Data & );
          void operator = (const C_Computational_Data &);
        }; // ................................. C_Computational_Data .......................


        //
        // C_Cobyla
        //
        enum { eX_Kf, eX_Lx, eX_rho, eX_Lap, eX_Sl, /*eX_Fv,*/ eX_ka, eX_Ffp, eX_dLx, eX_Size };
        struct C_Cobyla sealed : aMATH::C_cobyla_Core
        {
          const C_Computational_Data  & oData;
          const C_RodPump_U           & oRodPump;
          double                        fFe_avg;
          double                        vX_Min[ eX_Size ];
          double                        vX_Max[ eX_Size ];
          double                        vX_denorm[ eX_Size ];
          //
          double                        fPwh, fPi, fFf_sum;
          double                        fY_normalizer;
          //
          struct C_Data_Item_Comp sealed
          {
            double fFe, travel, fFpm, fFi;
            //
            C_Data_Item_Comp() :fFe(0), travel(1), fFpm(0), fFi(0)
            {}
          };
          std::vector<C_Data_Item_Comp> vData_Items_Comp;
          //
          C_Cobyla(
            const C_Computational_Data  & oData_,
            const C_RodPump_U           & oRodPump_,
            volatile const bool         & bStop_
            ) :oData(oData_), oRodPump(oRodPump_), 
               aMATH::C_cobyla_Core(bStop_, &oRodPump_),
               vData_Items_Comp( oData.vData_Items.size() ),
               fY_normalizer( 0 ), fPi( 0 ), fPwh( 0 ), fFf_sum( 0 )
          {
            oBest.Start( eX_Size );
            for ( int I = 0; I < eX_Size; ++I )
            {
              #ifdef _DEBUG
              vX_denorm[I] = vX_Min[ I ] = vX_Max[ I ] = HUGE_VAL;
              #else
              vX_denorm[I] = vX_Min[ I ] = vX_Max[ I ] = 0;
              #endif
            }
            double fSum = 0;
            for ( size_t I = 0; I < oData.vData_Items.size(); ++I )
            {
              fSum += oData.vData_Items[ I ].fFe;
            }
            fFe_avg = fSum / oData.vData_Items.size();
          }  // ........................... ctor .....................................

          //
          //
          int callback( const double *x, double *f, double *con )
          {
            if ( Check_Stop() ) { return 1; }

            for ( int I = 0; I < eX_Size; ++I )
            {
              con[ I ] = aMATH::cobyla_constrain_norm_min_max( x[ I ] );
              if ( !Check_Is_Number( con[ I ], sInternalError ) )
              {
                _ASSERT( false );
                Insert_Error_Prefix( "con[" + atos( I ) + "]", sInternalError );
                return 1;
              }
              vX_denorm[ I ] = aMATH::cobyla_denormalize( x[ I ], vX_Min[ I ], vX_Max[ I ] );
              if ( !Check_Is_Number( vX_denorm[ I ], sInternalError ) )
              {
                _ASSERT( false );
                Insert_Error_Prefix( "X[" + atos( I ) + "]", sInternalError );
                return 1;
              }
            }

            const double f_rho = vX_denorm[ eX_rho ];
            const double fLap  = vX_denorm[ eX_Lap ];

            fPi = fLap * f_rho * GRAVITY;
            const double fFp_mul = fPi * oRodPump.fAp;

            const double fAp_rho_g = oRodPump.fAp * f_rho * GRAVITY;
            const double fSl = vX_denorm[ eX_Sl ];

            
            double fLx = vX_denorm[ eX_Lx ];
            fLx = __max( 0, fLx );

            { // set up travel
              double f_dLx = vX_denorm[ eX_dLx ];
              f_dLx = __max( 0, f_dLx );

              const double fHalf_dLx = f_dLx / 2; 
              const double fLx_min = fLx - fHalf_dLx;
              const double fLx_max = fLx + fHalf_dLx;


              for ( size_t I = oData.iDown_Idx; I < oData.vData_Items.size(); ++I )
              {
                const double fL = oData.vData_Items[ I ].fL;
                if ( fL <= fLx_min )
                {
                  vData_Items_Comp[ I ].travel = 1;
                } else if ( fL >= fLx_max ) {
                  vData_Items_Comp[ I ].travel = 0;
                } else {
                  vData_Items_Comp[ I ].travel = (fLx_max - fL) / f_dLx;
                  _ASSERT( vData_Items_Comp[ I ].travel > 0 );
                  _ASSERT( vData_Items_Comp[ I ].travel < 1 );
                }
              } // for ( size_t I .......
              

            } // set up travel ......



            const double f_mc       = f_rho * oRodPump.fLp * oRodPump.fAp;
            const double fFi_mul_up = f_rho * oRodPump.fLp * oRodPump.fAp + oRodPump.fMr;
            const double fFi_mul_dn = oRodPump.fMr;
            const double fVavg = oRodPump.fVavg;
            const double fVavg2     = fVavg * fVavg;
            const double fFo        = fFi_mul_up * GRAVITY;
            const double fFb_mul    = f_rho * GRAVITY * oRodPump.fAr;
            const double fKf        = vX_denorm[ eX_Kf ];
            //const double fFv        = vX_denorm[ eX_Fv ];
            const double f_ka       = vX_denorm[ eX_ka ];
            const double fFfp       = vX_denorm[ eX_Ffp ];


            for ( size_t iIdx = 0; iIdx < oData.vData_Items.size(); ++iIdx )
            {
              const double f_v = oData.vData_Items[ iIdx ].f_v;
              const bool bDown_Stroke = (iIdx >= oData.iDown_Idx);
              const double fSign = (bDown_Stroke ? -1. : 1.);
              const double f_v_2 = f_v * f_v;

              const double fL = oData.vData_Items[ iIdx ].fL;


              double & fComp_Fe = vData_Items_Comp[ iIdx ].fFe; 
              fComp_Fe = 0;


              // Ff
              if ( !bDown_Stroke )
              {
                const double fFf = fKf * fL / oRodPump.fDci * (f_v_2 / fVavg2) * f_rho * oRodPump.fAan;
                fComp_Fe += fFf * fSign;
              }

              // Fo
              fComp_Fe += fFo;

              const double f_a = oData.vData_Items[ iIdx ].f_a;
              const double fLn_minus_L = oData.fL_max - fL;

              double & fFpm = vData_Items_Comp[ iIdx ].fFpm;
              fFpm = fLn_minus_L * fAp_rho_g /** fSK_mul*/;

              // Fa
              const double fPa = fPwh * (1 + f_ka * fL / oData.fL_max);
              const double fFa = oRodPump.fAp * fPa;
              fComp_Fe += fFa;

              if ( bDown_Stroke )
              {
                // Fp
                fComp_Fe -= fFp_mul * vData_Items_Comp[ iIdx ].travel;
              } else {
                // Fpm
                fComp_Fe += fFpm * fSl;
                // Inertia of oil in pump
                fComp_Fe += fLn_minus_L *  f_mc * f_a;
              }

              // Fi
              double & fFi = vData_Items_Comp[ iIdx ].fFi;

              fFi = f_a * (bDown_Stroke ? fFi_mul_dn : fFi_mul_up);

              fComp_Fe += fFi;

              // Fb
              const double fFb = (oRodPump.fLp - fL) * fFb_mul;
              fComp_Fe -= fFb;

              // Fv
              //fComp_Fe += fFv;

              // Ffp
              fComp_Fe += fFfp * fSign;

              if ( !Check_Is_Number( fComp_Fe, sInternalError ) )
              {
                _ASSERT( false );
                Insert_Error_Prefix( "Fe[" + atos( iIdx ) + "]", sInternalError );
                return 1;
              }

            } // for ( size_t iIdx ......

            const double fFf = fKf * oData.fL_max / oRodPump.fDci * f_rho * oRodPump.fAan;
            fFf_sum = fFf + fFfp;



            double fSum = 0;
            for ( size_t iIdx = 0; iIdx < oData.vData_Items.size(); ++iIdx )
            {
              const double fDelta = oData.vData_Items[ iIdx ].fFe - vData_Items_Comp[ iIdx ].fFe;
              fSum += fDelta * fDelta;
            } // for ( size_t iIdx  ....

            if ( !Check_Is_Number( fSum, sInternalError ) )
            {
              _ASSERT( false );
              Insert_Error_Prefix( "Y", sInternalError );
              return 1;
            }

            fSum /= oData.vData_Items.size();
            if ( fY_normalizer > 0 )
            {
              *f = aMATH::cobyla_normalize( fSum, 0, fY_normalizer );
              oBest.Step( *f, x, eX_Size, con );
            } else {
              *f = fSum;
            }

            if ( Tick_DS() )
            {
              Show_Progress( *f );
            }
            return 0;
          } // ......................................... callback ..........................

          //
          static int __stdcall cobyla_callback( int n, int m, const double *x, double *f, double *con, void *state )
          {
            _ASSERT( n == eX_Size );
            _ASSERT( m == eX_Size );
            return static_cast<C_Cobyla*>(state)->callback( x, f, con );
          }

        }; // ................................. C_Cobyla ...........................

        //
        // *** RodPumpCalculation *****
        //
        public ref class RodPumpCalculation sealed : public IRodPumpMath
        {
          static IProject           ^ pumpProject;
          initonly IElement         ^ pumpElement;
          C_RodPump_U               * pRodPump;
          C_RodPump_Pad             ^ pRodPump_Pad;
          initonly System::String   ^ sPump_Name;
          initonly System::String   ^ sDebug_File_Name, ^ sDebug_Bak_File_Name;
          int                         iStat_Comp_All, iStat_Comp_Success;
          int                         iDebug_Dump_Count = 0;
          literal int                 ctMax_Debug_Dump_Count = 5;
          //
          initonly static array<System::String ^> ^ ctX_Names = gcnew array<System::String ^>(eX_Size)
          {
            "Kf", "Lx", "rho", 
            "Lap", "Sl", /*"Fv",*/
            "ka", "Ffp", "dLx"
          };
          initonly static array<const aGAS::C_EU_Conversion*> ^ ctX_Conv = gcnew array<const aGAS::C_EU_Conversion*>( eX_Size )
          {
            NULL, &aGAS::EU_Conversion_Diameter, &aGAS::EU_Conversion_Density, 
            &aGAS::EU_Conversion_Length, NULL, /*&aGAS::EU_Conversion_Force,*/
            NULL, &aGAS::EU_Conversion_Force, &aGAS::EU_Conversion_Diameter
          };
          //
          aFW::C_Debug_Text_File     ^ pLog_History;
          System::Nullable<System::DateTime> oPrev_Log_Time;
          //
          initonly static array<aOIL::C_Rod_Pump_Signals::E_Web> ^ vLog_History_Columns =
          {
            aOIL::C_Rod_Pump_Signals::E_Web::Ff,  aOIL::C_Rod_Pump_Signals::E_Web::Lf,
            aOIL::C_Rod_Pump_Signals::E_Web::Sl,  aOIL::C_Rod_Pump_Signals::E_Web::Prod,
            aOIL::C_Rod_Pump_Signals::E_Web::Eff, aOIL::C_Rod_Pump_Signals::E_Web::rho,
            aOIL::C_Rod_Pump_Signals::E_Web::Lfs, aOIL::C_Rod_Pump_Signals::E_Web::Lap,
            aOIL::C_Rod_Pump_Signals::E_Web::Pi,  aOIL::C_Rod_Pump_Signals::E_Web::Fv,
            aOIL::C_Rod_Pump_Signals::E_Web::Pwh
          };
          array<double> ^ vLog_History_Values;
          //
          // RT input
          //
          enum struct                               E_RT_Data_Input       { Pwh };
          static initonly array<System::String^>  ^ vRT_Data_Input_Tags = { "Pwh" };
          array<System::Nullable<double> >        ^ vRT_Data_Input_Values;
          //
          // RT output
          //
          static initonly array<aOIL::C_Rod_Pump_Signals::E_Web> ^ vRT_Data_Output_Web =
          {
            aOIL::C_Rod_Pump_Signals::E_Web::Ff,
            aOIL::C_Rod_Pump_Signals::E_Web::Lf,
            aOIL::C_Rod_Pump_Signals::E_Web::Sl,
            aOIL::C_Rod_Pump_Signals::E_Web::Prod,
            aOIL::C_Rod_Pump_Signals::E_Web::Eff,
            aOIL::C_Rod_Pump_Signals::E_Web::rho,
            aOIL::C_Rod_Pump_Signals::E_Web::Lfs,
            aOIL::C_Rod_Pump_Signals::E_Web::Lap,
            aOIL::C_Rod_Pump_Signals::E_Web::Pi,
            aOIL::C_Rod_Pump_Signals::E_Web::Pwh
          };
          initonly array<IDataChannel^> ^ vRT_Data_Output_Channels;

        public:
          #pragma region RodPumpCalculation::ctor
          //
          //
          RodPumpCalculation(
            IProject        ^ _pumpProject,  
            IElement        ^ _pumpElement, 
            OptiRampLog     ^ _logService, 
            System::String  ^ _logName 
            ) :pumpElement(_pumpElement), pRodPump(NULL)
          {
            try
            {
              _ASSERT( _pumpProject != nullptr );
              _ASSERT( _pumpElement != nullptr );
              aFW::Log::Register( _logService, _logName );
              if ( pumpProject == nullptr )
              {
                pumpProject = _pumpProject;
              } else {
                _ASSERT( pumpProject == _pumpProject );
              }
              sPump_Name = _pumpElement->Name;
              aFW::Write_Log_Line();
              aFW::Write_Log_Line( aOIL::C_Project_Node_Rod_Pump::pName, sPump_Name );


              // retrieve parameters
              fElement                            ^ const pElement      = safe_cast<fElement ^>(_pumpElement);
              aOIL::C_Project_Node_Rod_Pump       ^ const pSet_Rod_Pump = safe_cast<aOIL::C_Project_Node_Rod_Pump ^>(pElement->pNode);
              aOIL::C_Project_Node_Rod_Pump_Lib   ^ const pSet_Lib      = pSet_Rod_Pump->Get_Library();

              int iParam_Count;
              const aCONF::C_Param_Descr * pParam_Descr = aOIL::C_Project_Node_Rod_Pump_Lib::Get_Param_Descr( iParam_Count );
              double fLn, f_tn, fDp, f_rho_n, fDci, fDpe, fLp, f_mpu, fPwhn;

              try
              {
                static const wchar_t * vTags[ 9 ] = { L"Ln", L"tn", L"Dp", L"rhon", L"Dci", L"Dpe", L"Lp", L"mpu", L"Pwhn" };
                double * vTgt[ dim( vTags ) ] = { &fLn, &f_tn, &fDp, &f_rho_n, &fDci, &fDpe, &fLp, &f_mpu, &fPwhn };
                for ( int I = 0; I < dim( vTags ); ++I )
                {
                  System::String ^ const sTag = string_to_String( vTags[ I ] );

                  aCONF::C_TagValues ^ const pSrc 
                    = pSet_Rod_Pump->oTagValue.ContainsKey( sTag )
                    ? % pSet_Rod_Pump->oTagValue
                    : % pSet_Lib->oTagValue;

                  System::String ^ const sParam_Error = pSrc->Tag_to_double( vTags[ I ], pParam_Descr, iParam_Count, *vTgt[ I ] );
                  if ( sParam_Error != nullptr )
                  {
                    throw gcnew System::Exception( sParam_Error );
                  } else {
                    aFW::Write_Log_Line( sTag, (*vTgt[ I ]).ToString( "G6" ) );
                  }
                } // for ( int I  .....

                if ( f_tn <= 1 ) { // check reasonable time 
                  throw gcnew System::Exception( "tn<1" );
                } else if ( f_rho_n > 1100 ) {
                  throw gcnew System::Exception( Name_Value("rhon", f_rho_n) + ">1100" );
                } else if ( f_rho_n < 700 ) {
                  throw gcnew System::Exception( Name_Value( "rhon", f_rho_n ) + "<700" );
                } else if ( fDci < fDpe ) {
                  throw gcnew System::Exception( Name_Value( "Dci", fDci) + "<" + Name_Value("Dpe", fDpe) );
                }
              } catch ( System::Exception ^ pErr ) {
                throw gcnew System::Exception( System::String::Format(
                  "[{0}] {1}", pSet_Lib->Get_UI_Name(), pErr->Message ), pErr );
              }

              // manage debug file data
              aCONF::C_Project_Node_Computer ^ const pSet_Computer = aCONF::C_Project_Node_Computer::Find_Parent_Computer( pSet_Rod_Pump );
              System::String ^ const sDebug_Folder = System::IO::Path::Combine(
                                            pSet_Computer->Verify_Project_Path(), "Rod_Pump_Debug" );
              sDebug_File_Name = System::IO::Path::Combine( sDebug_Folder, sPump_Name + ".log" );
              sDebug_Bak_File_Name = System::IO::Path::Combine( sDebug_Folder, sPump_Name + ".bak" );



              IElement ^ const pElement_Pad = _pumpElement->Parent;
              IElement ^ const pElement_Reservoir = pElement_Pad->Parent;


              C_RodPump_Reservoir ^ pReservoir;

              {
                C_Monitor oLock( % C_RodPump_Singleton::oProtect );
                pReservoir = C_RodPump_Reservoir::Create( pElement_Reservoir );
                pRodPump_Pad = C_RodPump_Pad::Create( pElement_Pad, pReservoir );
              }

              if ( pReservoir->First_Instance() )
              {
                aFW::Create_Dir_If_Not_Exist( sDebug_Folder );
              }

              pLog_History = gcnew aFW::C_Debug_Text_File(
                System::IO::Path::Combine( sDebug_Folder, sPump_Name + "_comp.log" )
                );
              { // header
                vLog_History_Values = gcnew array<double>( vLog_History_Columns->Length );
                System::Text::StringBuilder oText;
                oText.Append( "Time\tFile" );
                for each (aOIL::C_Rod_Pump_Signals::E_Web tIter in vLog_History_Columns)
                {
                  oText.Append( "\t" );
                  oText.Append( tIter.ToString() );
                  const aGAS::C_EU_Conversion * pConv = aOIL::C_Rod_Pump_Signals::ctWeb[ static_cast<int>(tIter) ].pConv;
                  if ( pConv != NULL )
                  {
                    oText.Append( "(" + string_to_String( pConv->Get_CEU() ) + ")" );
                  }
                }
                oText.Append( "\tRun(sec)\tMessage" );
                pLog_History->pWrite->WriteLine( oText.ToString() );
              } // header .....

              const double fFourt_of_PI = System::Math::PI / 4;

              pRodPump = new C_RodPump_U( String_to_wstring(pumpElement->Name), pRodPump_Pad->pInternal_P);
              pRodPump->fLn  = fLn;
              pRodPump->fLp  = fLp;
              pRodPump->f_tn = f_tn;
              pRodPump->f_rho_n = f_rho_n;
              pRodPump->fAp = fFourt_of_PI * fDp * fDp;
              aFW::Write_Log_Line( "Ap", pRodPump->fAp.ToString( "G6" ) );

              pRodPump->fVfn = pRodPump->fAp * fLn;
              aFW::Write_Log_Line( "Vfn", pRodPump->fVfn.ToString( "G6" ) );

              pRodPump->fAr = fFourt_of_PI * fDpe * fDpe;
              aFW::Write_Log_Line( "Ar", pRodPump->fAr.ToString( "G6" ) );

              pRodPump->fAan = fFourt_of_PI * fDci * fDci - pRodPump->fAr;
              pRodPump->fMfn = pRodPump->fAan * fLp * f_rho_n;
              aFW::Write_Log_Line( "Mfn", pRodPump->fMfn.ToString( "G6" ) );

              pRodPump->fMr = aGAS::EU_Conversion_Mass.CtoM(
                aGAS::EU_Conversion_Length.MtoC( fLp ) * aGAS::EU_Conversion_Mass.MtoC( f_mpu ) );
              aFW::Write_Log_Line( "Mr", pRodPump->fMr.ToString( "G6" ) );

              pRodPump->fDci  = fDci;
              pRodPump->fVavg = 2 * fLn / f_tn;
              pRodPump->fPwhn = fPwhn;

              // RT Input
              vRT_Data_Input_Values = gcnew array<System::Nullable<double> >( vRT_Data_Input_Tags->Length );

              // RT Output
              vRT_Data_Output_Channels = gcnew array<IDataChannel^>( vRT_Data_Output_Web->Length );
              { // fill vRT_Data_Output_Channels
                for each (IDataChannel ^ pIterCh in _pumpElement->dataChannels)
                {
                  if ( dynamic_cast<IWebChannel ^>(pIterCh) == nullptr ) { continue; }
                  System::String ^ const sName = pIterCh->Name;
                  for ( int I = 0; I < vRT_Data_Output_Web->Length; ++I )
                  {
                    if ( aOIL::C_Rod_Pump_Signals::ctWeb[ static_cast<int>(vRT_Data_Output_Web[ I ]) ].sName == sName )
                    {
                      _ASSERT( vRT_Data_Output_Channels[ I ] == nullptr );
                      vRT_Data_Output_Channels[ I ] = pIterCh;
                      break;
                    }
                  }
                }
                #ifdef _DEBUG
                for ( int I = 0; I < vRT_Data_Output_Channels->Length; ++I )
                {
                  _ASSERT( vRT_Data_Output_Channels[I] != nullptr );
                }
                #endif
              } // fill vRT_Data_Output_Channels .....

            } catch ( ... ) {
              delete this;
              throw;
            }
          } // ................................. ctor .........................
          #pragma endregion
          //
          //
          virtual event MathNotificationHandler ^ NotificationEvent;

        private:
          //
          // string constants
          //
          static initonly System::String ^ ctMessage_Interrupted = "Interrupted";
          static initonly System::String ^ ctMessage_Wrong_Data_Item = "Data Item Line[{0}]: {1}";
          static initonly System::String ^ ctMessage_Up_Down_Detection = "upstroke/downstroke change detection error. Suspect, the data are not sorted";


          //
          // Dump_Cobyla_Values
          //
          void Dump_Cobyla_Values(
            aMODEL::C_Log_Proxy_File  % oLog,
            const C_Cobyla            & oCobyla,
            const double                vX_constr[]
            )
          {
            for ( int I = 0; I < eX_Size; ++I )
            {
              if ( vX_constr[ I ] < 0 )
              {
                oLog.WriteLine( "Constr " + ctX_Names[ I ], vX_constr[ I ].ToString( "G6" ) + " Viloated!" );
              }
            }
            //oLog.WriteLine( "Pa", oCobyla.fPa.ToString("G6") );
            //oLog.WriteLine( "Fp", oCobyla.fFp.ToString( "G6" ) );
          } // ......................................... Dump_Cobyla_Values ..........................


          //
          //
          void Set_Web_CValue( 
            aMODEL::C_Log_Proxy_File        % oLog,
            array<fRT::RTValue>        ^ vRT_Data,
            System::DateTime                  tTimeStamp,
            aOIL::C_Rod_Pump_Signals::E_Web   tSignal, 
            double                            fValue 
            )
          {
            aWEB::C_Web_Channel tChannel = aOIL::C_Rod_Pump_Signals::ctWeb[ static_cast<int>(tSignal) ];
            //_ASSERT( tChannel.pType == double::typeid );
            const double fCValue = (tChannel.pConv == NULL) ? fValue : tChannel.pConv->MtoC( fValue );

            if ( tChannel.pConv == NULL )
            {
              oLog.WriteLine( tChannel.sName, fCValue.ToString("G6") );
            } else {
              oLog.WriteLine( tChannel.sName, aGAS::CValue_and_CEU( *tChannel.pConv, fCValue ) );
            }
            {
              const int iIdx = IndexOf( vLog_History_Columns, tSignal );
              if ( iIdx >= 0 )
              {
                vLog_History_Values[ iIdx ] = fCValue;
              } else {
                _ASSERT( false );
              }
            }
            {
              const int iIdx = IndexOf( vRT_Data_Output_Web, tSignal );
              if ( iIdx < 0 )
              {
                _ASSERT( false );
              } else {
                vRT_Data[ iIdx ].Value      = fCValue;
                vRT_Data[ iIdx ].Timestamp  = tTimeStamp;
              }
            }
          } // ........................................ Set_Web_CValue ............................

          //
          //
          enum struct E_Calculate_Result { eOK, eWarning, eError };



          #pragma region RodPumpCalculation::Calculate_Ex
          //
          //
          System::String ^ Calculate_Ex(
            bool                        bDo_Verbose_Logging,
            const bool                % stop,
            DynoCardRTData            ^ inDynoCard,
            aMODEL::C_Log_Proxy_File  % oLog,
            DynoCardRTData            ^ outResult,
            E_Calculate_Result        % tResult   
            )
          {
            _ASSERT( outResult != nullptr );
            tResult = E_Calculate_Result::eError;

            oLog.WriteLine( "TimeStamp(UTC)", inDynoCard->timeStampUTC.ToString(aFW::C_FW_Info::ctNow_for_Log_Mask) );
            if ( inDynoCard->timeStampUTC == System::DateTime::MinValue )
            {
              if ( bDo_Verbose_Logging )
              {
                System::Diagnostics::Trace::WriteLine( System::String::Format(
                  "Warning: \"{0}\" time stamp is empty", sPump_Name ) );
              }
            } else {
              oLog.WriteLine( "TimeStamp(Local)", 
                inDynoCard->timeStampUTC.ToLocalTime().ToString( aFW::C_FW_Info::ctNow_for_Log_Mask ) );
            }

            if ( inDynoCard->pumpID != sPump_Name && bDo_Verbose_Logging  )
            {

              System::Diagnostics::Trace::WriteLine( System::String::Format(
                "Warning: pumpID \"{0}\" is different from name \"{1}\"", inDynoCard->pumpID, sPump_Name ) );
            }
            oLog.WriteLine( "Data File", inDynoCard->dataFile );


            oLog.WriteLine( 
                System::Environment::NewLine 
              + "\t" + aGAS::Name_and_CEU( aGAS::EU_Conversion_Diameter, "L" )
              + "\t" + aGAS::Name_and_CEU( aGAS::EU_Conversion_Force, "Fe" )
              );

            // check data items
            auto pDataItems = inDynoCard->DataItems;
            int iCount_Items = 0;
            double fL_max = 0;
            if ( pDataItems != nullptr )
            {
              System::String ^ sCheck_Error = nullptr;
              std::string sBuf;
              for each (auto tIter in pDataItems)
              {
                oLog.WriteLine( System::String::Format(
                  "{0:D3}\t{1:G6}\t{2:G6}", iCount_Items, tIter.X, tIter.Y) );

                const double fL = tIter.X;
                if ( !aGAS::EU_Conversion_Diameter.Check_Range_C( fL, "L", sBuf )
                  || !aGAS::EU_Conversion_Force.Check_Range_C( tIter.Y, "Fe", sBuf )
                  )
                {
                  return System::String::Format( ctMessage_Wrong_Data_Item, iCount_Items, string_to_String( sBuf ));
                } 
                ++iCount_Items;
                fL_max = __max( fL_max, fL );
              }
            }
            oLog.WriteLine();
            const int iMin_Count_Items = 4;
            if ( iCount_Items < iMin_Count_Items )
            {
              return Name_Value( "Count of Data Items", iCount_Items ) + "<" + iMin_Count_Items.ToString(); 
            }

            const double fLn = pRodPump->fLn;

            double fFe_min = 1e10, fFe_max = 0;

            C_Computational_Data oData;
            oData.fL_max = fL_max = aGAS::EU_Conversion_Diameter.CtoM( fL_max );

            { //pass 2, convert to Model EU 
              oData.vData_Items.resize( iCount_Items );
              int iIdx = -1;
              for each (auto tIter in pDataItems)
              {
                ++iIdx;
                C_Data_Item & tTgt = oData.vData_Items[ iIdx ];
                tTgt.fFe = aGAS::EU_Conversion_Force.CtoM( tIter.Y );
                tTgt.fL = aGAS::EU_Conversion_Diameter.CtoM( tIter.X );

                fFe_min = __min( fFe_min, tTgt.fFe );
                fFe_max = __max( fFe_max, tTgt.fFe );

              }
            } //pass 2 ....

            if ( fFe_min >= fFe_max )
            {
              // last line of defence, when we have all Fe constants
              return "Fe is not changing";
            }


            if ( stop ) { return ctMessage_Interrupted; }

            { // pass 3, detect downstroke
              const double fMin_Change_Detection = fL_max / iCount_Items / 5;
              int iFirst = 0, iLast = 0;
              double fMax_L = oData.vData_Items[ 0 ].fL;
              for ( int iIdx = 1; iIdx < iCount_Items; ++iIdx )
              {
                const C_Data_Item & tSrc = oData.vData_Items[ iIdx ];
                if ( tSrc.fL > fMax_L )
                {
                  iFirst = iLast = iIdx;
                  fMax_L = tSrc.fL;
                } else if ( fabs( fMax_L - tSrc.fL ) < fMin_Change_Detection ) {
                  iLast = iIdx;
                }
              }
              oLog.WriteLine( "Downstroke Range", 
                System::String::Format("[{0:D3}...{1:D3}]", iFirst, iLast) );

              const int iDetected_First_Downstroke = (iFirst + iLast) / 2;
              oLog.WriteLine( "First Downstroke", iDetected_First_Downstroke.ToString("D3") );

              int iMin_Limit, iMax_Limit;
              if ( iDetected_First_Downstroke < (iMin_Limit = iMin_Count_Items / 2) )
              {
                return System::String::Format( ctMessage_Wrong_Data_Item, iDetected_First_Downstroke, 
                  System::String::Format("upstroke/downstroke position ({0}) is too small (<{1})",
                      iDetected_First_Downstroke, iMin_Limit 
                      ));
              } else if ( iDetected_First_Downstroke >( iMax_Limit = iCount_Items - iMin_Count_Items / 2) ) {
                return System::String::Format( ctMessage_Wrong_Data_Item, iDetected_First_Downstroke, 
                  System::String::Format( "upstroke/downstroke position ({0}) is too big (>{1})",
                  iDetected_First_Downstroke, iMax_Limit
                  ));
              }

              oData.iDown_Idx = iDetected_First_Downstroke;

            } // pass 3 ......


            if ( stop ) { return ctMessage_Interrupted; }

            { // pass 4, detect Lmax and Lmax corrected, compute time
              oLog.WriteLine( "Ln", fLn.ToString( "G6" ) );



              const double f_dt_angle = System::Math::PI * 2 / iCount_Items;

              const double fLmax_add = fL_max * (1 - System::Math::Cos( f_dt_angle / 2 ));
              oLog.WriteLine( "Lmax_add", fLmax_add.ToString( "G6" ) );


              fL_max += fLmax_add;
              oData.fL_max = fL_max;
              oLog.WriteLine( "Lmax", fL_max.ToString( "G6" ) );

              const double fL_radius = fL_max / 2;
              oLog.WriteLine( "L_radius", fL_radius.ToString( "G6" ) );
              Check_Is_Number( fL_radius );

              const double f_tn = pRodPump->f_tn;
              oLog.WriteLine( "tn", aGAS::CValue_and_CEU(& aGAS::EU_Conversion_Time, f_tn, true) );
              oLog.WriteLine();
              oLog.WriteLine( 
                "\t" + aGAS::Name_and_EU( &aGAS::EU_Conversion_Diameter, "L" )
                + "\t" + aGAS::Name_and_EU( &aGAS::EU_Conversion_Time, "Time" )
                + "\tSpeed (m/s)\tAccel (m/s2)" );

              const double fHalf_tn = f_tn / 2;
              const double fPI_tn = 2 * System::Math::PI / f_tn;
              System::Text::StringBuilder oText;

              for ( size_t I = 0, cnt = oData.vData_Items.size(); I < cnt; ++I )
              {
                try
                {
                  oText.Clear();
                  oText.Append( I.ToString( "D3" ) );
                  oText.Append( "\t" );

                  const double fL = oData.vData_Items[ I ].fL;
                  oText.Append( fL.ToString( "G6" ) );

                  const double fAngle = System::Math::Acos( fL / fL_radius - 1 );
                  Check_Is_Number( fAngle );


                  double & f_t = oData.vData_Items[ I ].f_t;
                  if ( I < oData.iDown_Idx )
                  {
                    f_t = (1 - fAngle / System::Math::PI) * fHalf_tn;
                  } else {
                    f_t = (1 + fAngle / System::Math::PI) * fHalf_tn;
                  }
                  oText.Append( "\t" );
                  oText.Append( f_t.ToString( "G6" ) );
                  Check_Is_Number( f_t );

                  const double fAngle_t = 2 * System::Math::PI * f_t / f_tn;

                  double & f_v = oData.vData_Items[ I ].f_v;
                  double & f_a = oData.vData_Items[ I ].f_a;

                  oText.Append( "\t" );
                  //f_v = fLn / 2 * fPI_tn * System::Math::Sin( fPI_tn * f_t );
                  f_v = fL_radius * fPI_tn * System::Math::Sin( fPI_tn * f_t );
                  oText.Append( f_v.ToString( "G6" ) );
                  Check_Is_Number( f_v );

                  oText.Append( "\t" );
                  //f_a = fLn / 2 * fPI_tn * fPI_tn * System::Math::Cos( fPI_tn * f_t );
                  f_a = fL_radius * fPI_tn * fPI_tn * System::Math::Cos( fPI_tn * f_t );
                  oText.Append( f_a.ToString( "G6" ) );
                  Check_Is_Number( f_a );


                } finally {
                  oLog.WriteLine( oText.ToString() );
                }


              } // for ( size_t I ......

              oLog.WriteLine();

            } // pass 4 .....

            if ( stop ) { return ctMessage_Interrupted; }

            pin_ptr<const bool> oStop( & stop );
            C_Cobyla oCobyla( oData, *pRodPump, *oStop );

            { // check input data
              const int iSrc_Idx = static_cast<int>(E_RT_Data_Input::Pwh);
              if ( vRT_Data_Input_Values[ iSrc_Idx ].HasValue )
              {
                std::wstring sName = String_to_wstring( vRT_Data_Input_Tags[ iSrc_Idx ] );
                std::wstring sBuf;
                const double fValue = vRT_Data_Input_Values[ iSrc_Idx ].Value;
                if ( !aGAS::EU_Conversion_Pressure.Check_Range_C( fValue, sName, sBuf ) )
                {
                  return string_to_String( sBuf );
                }
                oCobyla.fPwh = aGAS::EU_Conversion_Pressure.CtoM( fValue );
              } else {
                oCobyla.fPwh = pRodPump->fPwhn;
              }
              oLog.WriteLine( "Pwh", oCobyla.fPwh.ToString( "G6" ) );
            } // check input data .....
            //compute derived parameters


            // set up initial data
            oCobyla.vX_Min[ eX_Kf ] = 0;
            oCobyla.vX_Max[ eX_Kf ] = 10;
            oCobyla.vX_Min[ eX_Lx ] = fL_max * 0.01;
            oCobyla.vX_Max[ eX_Lx ] = fL_max * 0.99;
            oCobyla.vX_Min[ eX_rho ] = pRodPump->f_rho_n * 0.2;
            oCobyla.vX_Max[ eX_rho ] = pRodPump->f_rho_n * 1.2;
            oCobyla.vX_Min[ eX_Lap ] = 0;
            oCobyla.vX_Max[ eX_Lap ] = pRodPump->fLp * 0.8; 
            oCobyla.vX_Min[ eX_Sl ] = 0.6;
            oCobyla.vX_Max[ eX_Sl ] = 1;
            //oCobyla.vX_Min[ eX_Fv ] = 0;
            //oCobyla.vX_Max[ eX_Fv ] = oCobyla.fFe_avg * 0.75;
            oCobyla.vX_Min[ eX_ka ] = 0;
            oCobyla.vX_Max[ eX_ka ] = 1;
            oCobyla.vX_Min[ eX_Ffp ] = 0;
            oCobyla.vX_Max[ eX_Ffp ] = oCobyla.fFe_avg * 0.75;
            oCobyla.vX_Min[ eX_dLx ] = fL_max / iCount_Items * 8.;
            oCobyla.vX_Max[ eX_dLx ] = fL_max / 2;

            const double rhobeg = 0.2;
            _ALLOCA( double, vX, eX_Size );
            vX[ eX_Kf ] = aMATH::cobyla_normalize( 0.001, oCobyla.vX_Min[ eX_Kf ], oCobyla.vX_Max[ eX_Kf ] );
            vX[ eX_Lx ]   = 1 - rhobeg * 1.1; // closer to 1
            vX[ eX_rho ]  = aMATH::cobyla_normalize( pRodPump->f_rho_n, oCobyla.vX_Min[ eX_rho ], oCobyla.vX_Max[ eX_rho ] );
            vX[ eX_Lap ]  = aMATH::cobyla_normalize( pRodPump->fLp * 0.05, oCobyla.vX_Min[ eX_Lap ], oCobyla.vX_Max[ eX_Lap ] );
            vX[ eX_Sl ]   = aMATH::cobyla_normalize( 0.99, oCobyla.vX_Min[ eX_Sl ], oCobyla.vX_Max[ eX_Sl ] );
            //vX[ eX_Fv ]   = rhobeg * 1.1; // closer to 0
            vX[ eX_ka ]   = rhobeg * 1.1; // closer to 0
            vX[ eX_Ffp ]  = rhobeg * 1.1; // closer to 0
            vX[ eX_dLx ]  = rhobeg * 1.1; // closer to 0

            { // dump COBYLA starting data
              oLog.WriteLine( "COBYLA starts with:" + System::Environment::NewLine + "\tMin\tMax\tValue");
              for ( int I = 0; I < eX_Size; ++I )
              {
                const double fValue_MEU = aMATH::cobyla_denormalize( vX[ I ], oCobyla.vX_Min[ I ], oCobyla.vX_Max[ I ] );
                System::String ^ sCEU = System::String::Empty;
                if ( ctX_Conv[ I ] != NULL )
                {
                  sCEU = "\t(" + aGAS::CValue_and_CEU( ctX_Conv[ I ], fValue_MEU, true ) + ")";
                }

                oLog.WriteLine( System::String::Format(
                  "{0}\t{1:G6}\t{2:G6}\t{3:G6}{4}",
                  ctX_Names[I],
                  oCobyla.vX_Min[ I ], oCobyla.vX_Max[ I ],
                  fValue_MEU, sCEU
                  ));
              }
              oLog.WriteLine();
              oLog.WriteLine( "Fe_avg", oCobyla.fFe_avg.ToString( "G6" ) );

            } // dump COBYLA starting data .....

            _ALLOCA( double, vX_constr, eX_Size );

            { // check initial computation
              System::String ^ sErr = nullptr;
              try
              {
                double fResult;
                if ( oCobyla.callback( vX, &fResult, vX_constr ) )
                {
                  _ASSERT( !oCobyla.sInternalError.empty() );
                  sErr = string_to_String( oCobyla.sInternalError );
                } else {
                  oCobyla.fY_normalizer = fResult;
                  oLog.WriteLine( "Initial Y", "1" );
                  Dump_Cobyla_Values( oLog, oCobyla, vX_constr );
                } // if ( oCobyla.callback .....


              } catch ( const std::exception & oErr ) {
                sErr = string_to_String(oErr.what());
              } catch ( System::Exception ^ pErr ){
                oLog.WriteLine( pErr->ToString() );
                sErr = pErr->Message;
              }
              if ( sErr != nullptr )
              {
                return "Initial computation failure: " + sErr;
              }
            } // check initial computation .....

            if ( stop ) { return ctMessage_Interrupted; }

            { // main computation
              System::String ^ sErr = nullptr;
              try
              {
                int iMax_Fun = 1000 * eX_Size;
                const int iRes = aMATH::cobyla( eX_Size, eX_Size, vX, rhobeg, rhobeg / 100,
                    aMATH::COBYLA_MSG_NONE, &iMax_Fun, &C_Cobyla::cobyla_callback, & oCobyla );

                std::string sBuf;
                double fResult;

                if ( iRes == aMATH::COBYLA_USERABORT && stop )
                {
                  _ASSERT( !oCobyla.sInternalError.empty() );
                  sErr = string_to_String( oCobyla.sInternalError );
                } else if ( !oCobyla.oBest.Handle_Cobyla_Result_Good_Constrains( iRes, vX, sBuf ) )
                {
                  _ASSERT( !sBuf.empty() );
                  sErr = string_to_String( sBuf );
                } else if ( oCobyla.callback( vX, &fResult, vX_constr ) ) {
                  _ASSERT( !oCobyla.sInternalError.empty() );
                  sErr = string_to_String( oCobyla.sInternalError );
                } else {
                  // handling correct case
                  oLog.WriteLine();
                  oLog.WriteLine( "Iterations", iMax_Fun.ToString( "N0" ) );
                  oLog.WriteLine( "Final Y", fResult.ToString( "G6" ) );
                  Dump_Cobyla_Values( oLog, oCobyla, vX_constr );

                  System::Text::StringBuilder oText;
                  System::String ^ sWarning = nullptr;
                  for ( int I = 0; I < eX_Size; ++I )
                  {
                    const double fValue_MEU = oCobyla.vX_denorm[ I ];
                    oText.Clear();
                    oText.Append( fValue_MEU.ToString( "G6" ) );

                    if ( ctX_Conv[ I ] != NULL )
                    {
                      oText.Append(" (" + aGAS::CValue_and_CEU( ctX_Conv[ I ], fValue_MEU, true ) + ")");
                    }

                    const double f_dConstrain = oCobyla.vX_Max[ I ] - oCobyla.vX_Min[ I ];
                    _ASSERT( f_dConstrain > 0 );
                    const double fHit_Margin = 0.01 * f_dConstrain;

                    if ( fValue_MEU <= oCobyla.vX_Min[ I ] + fHit_Margin )
                    {
                      oText.Append( " Hit lower constrain!" );
                      if ( sWarning != nullptr ) { sWarning += ", "; }
                      sWarning += ctX_Names[ I ] + "(Lo)";

                    } else if ( fValue_MEU >= oCobyla.vX_Max[ I ] - fHit_Margin ) {

                      oText.Append( " Hit upper constrain!" );
                      if ( sWarning != nullptr ) { sWarning += ", "; }
                      sWarning += ctX_Names[ I ] + "(Hi)";
                    }
                    oLog.WriteLine( ctX_Names[ I ], oText.ToString() );
                  }

                  oLog.WriteLine();
                  oLog.WriteLine(
                             aGAS::Name_and_CEU( aGAS::EU_Conversion_Diameter, "L" )
                    + "\t" + aGAS::Name_and_CEU( aGAS::EU_Conversion_Force, "Fe" )
                    + "\t" + aGAS::Name_and_CEU( aGAS::EU_Conversion_Force, "Fe_orig" )
                    );

                  const int iData_Count = oData.vData_Items.size();
                  array<System::Windows::Point> ^ vBuf = gcnew array<System::Windows::Point>( iData_Count );
                  for ( int I = 0; I < iData_Count; ++I )
                  {
                    const double fL = aGAS::EU_Conversion_Diameter.MtoC( oData.vData_Items[ I ].fL );
                    vBuf[ I ].X = fL;
                    const double fFe = aGAS::EU_Conversion_Force.MtoC( oCobyla.vData_Items_Comp[ I ].fFe );
                    vBuf[ I ].Y = fFe;
                    const double fFe_orig = aGAS::EU_Conversion_Force.MtoC( oData.vData_Items[ I ].fFe );
                    oLog.WriteLine( System::String::Format( "{0:G6}\t{1:G6}\t{2:G6}", fL, fFe, fFe_orig ) );
                  }
                  oLog.WriteLine();

                  // compute output values
                  oLog.WriteLine( aFW::C_FW_Info::sSeparator );
                  const double fLf = oCobyla.vX_denorm[ eX_Lx ] / fL_max; 
                  const double fSl = oCobyla.vX_denorm[ eX_Sl ];
                  const double fVf = pRodPump->fAp * fLf * fLn * fSl;
                  oLog.WriteLine( "Vf", aGAS::CValue_and_CEU( &aGAS::EU_Conversion_Volume, fVf, true ));
                  const double fProd = fVf / pRodPump->f_tn;
                  const double fEff = fVf / pRodPump->fVfn;
                  const double frho = oCobyla.vX_denorm[ eX_rho ];
                  const double fLap = oCobyla.vX_denorm[ eX_Lap ];
                  double fLfs = pRodPump->fLp - fLap;
                  fLfs = __max( 0., fLfs );

                  const double fdLx = oCobyla.vX_denorm[ eX_dLx ];
                  oLog.WriteLine( ctX_Names[ eX_dLx ], fdLx.ToString( "G6" ) );


                  // fill in computed values
                  outResult->DataItems = vBuf;


                  array<fRT::RTValue> ^ vRT_Data = gcnew array<fRT::RTValue>( vRT_Data_Output_Channels->Length );
                  for ( int I = 0; I < vRT_Data_Output_Channels->Length; ++I )
                  {
                    vRT_Data[ I ].DataChannel = vRT_Data_Output_Channels[ I ];
                  }
                  outResult->RTData = vRT_Data;
                  auto tTS = inDynoCard->timeStampUTC;

                  Set_Web_CValue( oLog, vRT_Data, tTS, aOIL::C_Rod_Pump_Signals::E_Web::Ff, oCobyla.fFf_sum );
                  Set_Web_CValue( oLog, vRT_Data, tTS, aOIL::C_Rod_Pump_Signals::E_Web::Lf, fLf * 100 );
                  Set_Web_CValue( oLog, vRT_Data, tTS, aOIL::C_Rod_Pump_Signals::E_Web::Sl, fSl * 100 );
                  Set_Web_CValue( oLog, vRT_Data, tTS, aOIL::C_Rod_Pump_Signals::E_Web::Prod, fProd );
                  Set_Web_CValue( oLog, vRT_Data, tTS, aOIL::C_Rod_Pump_Signals::E_Web::Eff, fEff * 100 );
                  Set_Web_CValue( oLog, vRT_Data, tTS, aOIL::C_Rod_Pump_Signals::E_Web::rho, frho );
                  Set_Web_CValue( oLog, vRT_Data, tTS, aOIL::C_Rod_Pump_Signals::E_Web::Lfs, fLfs );
                  Set_Web_CValue( oLog, vRT_Data, tTS, aOIL::C_Rod_Pump_Signals::E_Web::Lap, fLap );
                  Set_Web_CValue( oLog, vRT_Data, tTS, aOIL::C_Rod_Pump_Signals::E_Web::Pi, oCobyla.fPi );
                  Set_Web_CValue( oLog, vRT_Data, tTS, aOIL::C_Rod_Pump_Signals::E_Web::Pwh, oCobyla.fPwh );


                  oLog.WriteLine( aFW::C_FW_Info::sSeparator );

                  if ( sWarning == nullptr )
                  {
                    _ASSERT( sErr == nullptr );
                    tResult = E_Calculate_Result::eOK;
                    return nullptr;
                  } else {
                    tResult = E_Calculate_Result::eWarning;
                    return "Warning: hit constrain " + sWarning;
                  }

                } // if ( iRes == .... else ..... 



              } catch ( const std::exception & oErr ) {
                sErr = string_to_String(oErr);
              } catch ( System::Exception ^ pErr ) {
                oLog.WriteLine( pErr->ToString() );
                sErr = pErr->Message;
              }
              if ( sErr != nullptr )
              {
                return "Computation failure: " + sErr;
              }

            } // main computation .....


            _ASSERT( false ); // should not get here
            return nullptr;
          } // .................................. Calculate_Ex .................................
          #pragma endregion

        public:
          #pragma region RodPumpCalculation::Calculate
          //
          // input - array of dynocard
          // output - result of math algorithm
          //
          virtual RodPumpResult^ Calculate( const bool % stop, DynoCardRTData^ inDynoCard )
          {
            ++iDebug_Dump_Count;
            const bool bDo_Verbose_Logging = (iDebug_Dump_Count < ctMax_Debug_Dump_Count);
            if ( bDo_Verbose_Logging )
            {
              System::Diagnostics::Trace::WriteLine( System::String::Format( "{0} Calculate Pump {1} (begin",
                aFW::Now_for_Log(), sPump_Name ) );
              if ( iDebug_Dump_Count + 1 == ctMax_Debug_Dump_Count )
              {
                System::Diagnostics::Trace::WriteLine( aFW::C_FW_Info::ctNow_for_Log_Prefix + " stop verbose logging next calculation" );
              }
            } // if ( bDo_Verbose_Logging ) .....

            RodPumpResult ^ const rpResult = gcnew RodPumpResult;
            ResponseStatus ^ const status = rpResult->status = gcnew ResponseStatus;
            try
            {
              const int iStart = System::Environment::TickCount;

              rpResult->dataCalculated = gcnew DynoCardRTData;

              aDS::DS_Update( pRodPump, LogTime_to_str() + " Start" );


              aFW::Move_File( sDebug_File_Name, sDebug_Bak_File_Name );
              aMODEL::C_Log_Proxy_File oDebug_File( sDebug_File_Name );
              oDebug_File.WriteLine( "Computations", iStat_Comp_All.ToString() );
              ++iStat_Comp_All;
              oDebug_File.WriteLine( "Successful", iStat_Comp_Success.ToString() );

              E_Calculate_Result tResult;
              bool bDo_Log_History = false;


              if ( inDynoCard == nullptr )
              {
                status->Message = "input data <null>";
                tResult = E_Calculate_Result::eError;
              } else { 

                bDo_Log_History = (pLog_History != nullptr);
                if ( bDo_Log_History )
                {
                  if ( !oPrev_Log_Time.HasValue || oPrev_Log_Time.Value < inDynoCard->timeStampUTC )
                  {
                    oPrev_Log_Time = inDynoCard->timeStampUTC;
                  } else {
                    System::Diagnostics::Trace::WriteLine("The current dyna card file time is older than the previous one, closing history file");
                    delete pLog_History; pLog_History = nullptr;
                    bDo_Log_History = false;
                  }
                } // if ( bDo_Log_History ) .....

                if ( bDo_Log_History )
                {
                  pLog_History->pWrite->Write(
                    inDynoCard->timeStampUTC.ToLocalTime().ToString("MM/dd/yy HH:mm")
                    + "\t" + inDynoCard->dataFile
                    );
                }

                { // retrieve input data
                  _ASSERT( vRT_Data_Input_Values->Length == vRT_Data_Input_Tags->Length );
                  for ( int I = 0; I < vRT_Data_Input_Values->Length; ++I )
                  {
                    vRT_Data_Input_Values[ I ] = System::Nullable<double>();
                  }
                  auto pRTData = inDynoCard->RTData;
                  if ( pRTData != nullptr ) 
                  {
                    bool bDumpHeader = true;
                    for each (fRT::RTValue ^ pIter in pRTData)
                    {
                      if ( pIter == nullptr || pIter->DataChannel == nullptr )
                      {
                        _ASSERT( false ); continue;
                      }
                      System::String ^ const sName = pIter->DataChannel->Name;
                      if ( System::String::IsNullOrEmpty( sName ) ) { _ASSERT( false ); continue; }

                      for ( int I = 0; I < vRT_Data_Input_Tags->Length; ++I )
                      {
                        if ( vRT_Data_Input_Tags[ I ] == sName )
                        {
                          System::Nullable<double> % tTgt = vRT_Data_Input_Values[ I ];
                          if ( pIter->IsValueGood() )
                          {
                            tTgt = pIter->Value;
                          }
                          oDebug_File.WriteLine( sName, tTgt.HasValue ? tTgt.Value.ToString( "G6" ) : "na" );
                          break;
                        }
                      }
                    } // for each (fRT::RTValue ^ pIter  ......
                  } // if ( pRTData != nullptr )  .....
                } // retrieve input data .....


                if ( bDo_Verbose_Logging )
                {
                  aFW::Write_Log_Line( "Data File", inDynoCard->dataFile );
                }
                status->Message = Calculate_Ex( bDo_Verbose_Logging,  stop, inDynoCard, oDebug_File, rpResult->dataCalculated, tResult );
              } // if ( inDynoCard == nullptr ) ......

              status->IsError = (tResult == E_Calculate_Result::eError);

              const double fRun_Seconds = (System::Environment::TickCount - iStart) / 1000.;

              if ( !status->IsError )
              {
                ++iStat_Comp_Success;
                if ( tResult == E_Calculate_Result::eWarning )
                {
                  _ASSERT( !System::String::IsNullOrEmpty( status->Message ) );
                  aDS::DS_Update( pRodPump, LogTime_to_str() + " Warning" );
                } else {
                  _ASSERT( System::String::IsNullOrEmpty( status->Message ) );
                  aDS::DS_Update( pRodPump, LogTime_to_str() + " OK" );
                }
                // very paranoidla assignments, Alex didn't ask for them
                rpResult->dataCalculated->timeStampUTC = inDynoCard->timeStampUTC;
                rpResult->dataCalculated->pumpID = System::String::IsNullOrEmpty( inDynoCard->pumpID ) ? sPump_Name : inDynoCard->pumpID;
                rpResult->dataCalculated->dataFile = inDynoCard->dataFile;

                if ( bDo_Log_History )
                {
                  System::Text::StringBuilder oText;
                  for each (double fCValue in vLog_History_Values)
                  {
                    oText.Append( "\t" );
                    oText.Append( fCValue.ToString( "G6" ) );
                  }
                  oText.Append( "\t" );
                  oText.Append( fRun_Seconds.ToString( "#0.000" ) );
                  oText.Append( "\t" );
                  oText.Append( status->Message );
                  pLog_History->pWrite->WriteLine( oText.ToString() );
                }

              } else {
                _ASSERT( !System::String::IsNullOrEmpty( status->Message ) );
                oDebug_File.WriteLine( status->Message );
                aDS::DS_Update( pRodPump, LogTime_to_str() + " Error: " + String_to_string( status->Message ) );

                if ( bDo_Log_History )
                {
                  System::Text::StringBuilder oText;
                  for each (double fCValue in vLog_History_Values)
                  {
                    oText.Append( "\t " );
                  }
                  oText.Append( "\t" );
                  oText.Append( fRun_Seconds.ToString( "#0.000" ) );
                  oText.Append( "\t" );
                  oText.Append( status->Message );
                  pLog_History->pWrite->WriteLine( oText.ToString() );
                }

              } // if ( !status->IsError ) .......


            } catch ( System::Exception ^ pErr ) {
              status->IsError = true;
              status->Message = pErr->Message;
              aFW::Silent_Exception( pErr );
            } finally {
              if ( status->IsError )
              {
                System::Diagnostics::Trace::WriteLine( System::String::Format(
                  "{0} fRodPumpServices.Calculate Error: \"{1}\", Data File: \"{2}\"", aFW::Now_for_Log(), status->Message, inDynoCard->dataFile
                  ) );
              }
              if ( bDo_Verbose_Logging )
              {
                System::Diagnostics::Trace::WriteLine( System::String::Format( "{0} Calculate Pump {1} end)",
                  aFW::Now_for_Log(), sPump_Name ) );
              } // if ( bDo_Verbose_Logging ) ......
            }
            NotificationEvent( status->Message, status->IsError );
            return rpResult;
          } // ................................... Calculate ...........................
          #pragma endregion


        protected:
          #pragma region RodPumpCalculation::!
          //
          //
          !RodPumpCalculation()
          {
            _ASSERT(!aFW::Is_GC_Finalizer_Thread());
            System::GC::SuppressFinalize( this );

            {
              C_Monitor oLock( % C_RodPump_Singleton::oProtect );
              if ( pRodPump_Pad != nullptr )
              {
                C_RodPump_Reservoir::Remove( pRodPump_Pad->pReservoir );
              }
              C_RodPump_Pad::Remove( pRodPump_Pad ); pRodPump_Pad = nullptr;
            }

            delete pRodPump; pRodPump = NULL;
            delete pLog_History; pLog_History = nullptr;
          } // .................................. ! ......................
          //
          ~RodPumpCalculation() { this->!RodPumpCalculation(); }
          #pragma endregion
        }; // ............................... RodPumpCalculation  ................................

        //
        // **** fMotorDynamicsComputer ****
        //
        private ref struct fMotorDynamicsComputer sealed : MotorDynamicsComputer
        {
          array<double> ^ vCoeff;
          const __int64 iStart_Time;

          //
          fMotorDynamicsComputer( 
            const aRP::C_RP_Position_Comp::C_Result & tRes 
            ) :iStart_Time(tRes.iStart_Time),
               vCoeff(gcnew array<double>(aRP::aCOEFF::eX_Size))
          {
            for ( int I = 0; I < aRP::aCOEFF::eX_Size; ++I )
            {
              vCoeff[ I ] = tRes.vCoeff[ I ];
            }
          } // ........................... ctor ............................

          //
          virtual double Angle( System::DateTime timeUTC )
          {
            const __int64 iTime = DateTime_to_UTC( timeUTC );
            const __int64 iS_Time = iStart_Time;
            pin_ptr<const double> vPin = &vCoeff[ 0 ];
            return aRP::C_RP_Position_Comp::C_Result::Angle( iTime, iS_Time, vPin );
          }

          //
          virtual void Position( System::DateTime timeUTC, double % position, bool % upstroke )
          {
            const double fAngle = Angle( timeUTC );

            double fBuf_position;
            bool bBuf_upstroke;

            aRP::C_RP_Position_Comp::C_Result::Position( fAngle, fBuf_position, bBuf_upstroke );

            position = fBuf_position;
            upstroke = bBuf_upstroke;

          }
        }; // .................................. fMotorDynamicsComputer .........................

        

        //
        // OPCDataDouble_to_C_RP_Point
        //
        static bool OPCDataDouble_to_C_RP_Point(
          System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fRT::RTDataDouble^> ^ data,
          aRP::C_RP_Points              & vPoints,
          const aGAS::C_EU_Conversion   * pConv,
          const char                    * sName,
          System::String              ^ % sError
          )
        {
          enum { eReject_Value_NA, eReject_quality_Bad, eReject_Not_Trusted_Value, eReject_Size };
          static const char * ctReject_Name[eReject_Size] =
          {
            "Value is not defined", "OPC quality is bad", "Value is not trusted"
          };
          
          int vReject_Count[ eReject_Size ] = { 0 };
          


          if ( data != nullptr )
          {
            aRP::C_RP_Point tBuf;
            std::string sBuf;
            for each (auto pIter in data)
            {
              if ( pIter == nullptr || !pIter->Value.HasValue
                )
              {
                ++vReject_Count[ eReject_Value_NA ];
              } else if ( RTData::IsQualityBad( pIter->Quality ) ) {
                ++vReject_Count[ eReject_quality_Bad ];
              } else {
                tBuf.fValue = pIter->Value.Value;
                if ( pConv != NULL && !pConv->Check_Range_C( tBuf.fValue, sName, sBuf ) )
                {
                  ++vReject_Count[ eReject_Not_Trusted_Value ];
                  sBuf.clear();
                } else {
                  tBuf.iTime = DateTime_to_UTC( pIter->Timestamp );
                  vPoints.push_back( tBuf );
                }
              }
            }
          } // if ( data != nullptr ) .....
          if ( vPoints.empty() )
          {
            System::Text::StringBuilder oText( "No data to process" );
            for ( int I = 0; I < eReject_Size; ++I )
            {
              if ( vReject_Count[ I ] > 0 )
              {
                oText.AppendFormat( ". {0} ({1})", string_to_String( ctReject_Name[ I ] ), vReject_Count[ I ] );
              }
            }
            sError = oText.ToString(); return false;
          }
          return true;
        } // ..................................... OPCDataDouble_to_C_RP_Point ................................


        //
        // *** fDynocardComputation ****
        //
        System::String ^ fDynocardComputation::MotorDynamicsMinTimeRange( double tn, double % MinTimeRangeSec )
        {
          try
          {
            aRP::C_RP_Position_Comp tComp( tn );
            std::string sError;
            if (!tComp.Check_Tn(sError))
            {
              _ASSERT( !sError.empty() );
              return string_to_String( sError );
            }

            MinTimeRangeSec = tn * aRP::C_RP_Position_Comp::iMin_Windows_Count;
            return nullptr;

          } catch ( const std::exception & oErr) {
            return string_to_String( oErr );
          } catch ( System::Exception ^ pErr ) {
            return pErr->Message;
          }
        } // ................................................ MotorDynamicsMinTimeRange ...............................


        // MotorDynamics
        //
        MotorDynamicsComputer ^ fDynocardComputation::MotorDynamics(
          double tn,
          System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fRT::RTDataDouble^> ^ data,
          bool % bStop,
          System::String ^ % sError
          )
        {
          try
          {
            aRP::C_RP_Position_Comp tComp(tn);

            aRP::C_RP_Points vPoints;
            if ( !OPCDataDouble_to_C_RP_Point( data, vPoints, & aGAS::EU_Conversion_Length, "Position",  sError ) )
            {
              return nullptr;
            }
            

            pin_ptr<volatile const bool> pPinStop = &bStop;

            std::string sErr;
            aRP::C_RP_Position_Comp::C_Result tResult = { 0 };
            if ( tComp.Compute(
              vPoints, *pPinStop, tResult, sErr
              ) )
            {
              return gcnew fMotorDynamicsComputer( tResult );
            } else {
              _ASSERT( !sErr.empty() );
            }

            sError = string_to_String( sErr );

          } catch ( const std::exception & oErr ) {
            SILENT_EXCEPTION_U( oErr.what() );
            sError = string_to_String( oErr );
          } catch ( System::Exception ^ pErr ) {
            SILENT_EXCEPTION( pErr );
            sError = pErr->Message;
          }
          return nullptr;
        } // .................................... MotorDynamics ...........................

        //
        // MotorDynaCard
        //
        System::Collections::Generic::IEnumerable<System::Windows::Point> ^ fDynocardComputation::MotorDynaCard(
          double tn,
          System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fRT::RTDataDouble ^> ^ dataPosition,
          System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fRT::RTDataDouble ^> ^ dataTorque,
          bool % bStop,
          System::String ^ % sError
          )
        {
          try
          {
            aRP::C_RP_Points vPoints_Position;
            if ( !OPCDataDouble_to_C_RP_Point( dataPosition, vPoints_Position, &aGAS::EU_Conversion_Length, "Position", sError ) )
            {
              return nullptr;
            }

            aRP::C_RP_Points vPoints_Torque;
            if ( !OPCDataDouble_to_C_RP_Point( dataTorque, vPoints_Torque, &aGAS::EU_Conversion_Torque, "Torque", sError ) )
            {
              return nullptr;
            }

            aRP::C_RP_Position_Comp tComp( tn );
            pin_ptr<volatile const bool> pPinStop = &bStop;

            std::string sErr;
            aRP::C_RP_Position_Comp::C_Dyna_Points vPoints_Result;
            if ( !tComp.Compute_DynaCard(
              vPoints_Position, vPoints_Torque, *pPinStop, vPoints_Result, sErr
              ) )
            {
              sError = string_to_String( sErr );
              return nullptr;
            }

            
            auto pRet = gcnew System::Collections::Generic::List < System::Windows::Point > ;

            for ( size_t I = 0; I < vPoints_Result.size(); ++I )
            {
              System::Windows::Point tBuf;
              tBuf.X = vPoints_Result[ I ].fLoad;
              tBuf.Y = vPoints_Result[ I ].fTorque;
              pRet->Add( tBuf );
            }

            return pRet;


          } catch ( const std::exception & oErr ) {
            SILENT_EXCEPTION_U( oErr.what() );
            sError = string_to_String( oErr );
          } catch ( System::Exception ^ pErr ) {
            SILENT_EXCEPTION( pErr );
            sError = pErr->Message;
          }
          return nullptr;
        } // .............................................. MotorDynaCard .............................


        
      } // .................................. namespace RodPump ................................
    } // ................................... namespace Services .......................
  } // .................................... namespace OptiRamp .......................
} // ..................................... namespace SNC .........................