//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
//-------------------------------------------------------------------
//
// Implementation of IRPProject interface.
//

#include "fRPProject.h"
#include "fElement.h"
#include "fProject.h"
#include "fProperty.h"
#include "fProjectEUFactory.h"
#include "..\..\common\Oil\Config_Set_Rod_Pump.h"
#include "..\..\common\Config\Config_Project_Tree_Utils.h"
#include "..\..\common\Config\Config_Set_EU.h"
#include "..\..\common\Utils_File.h"
#include "..\..\common\Utils_String.h"


using namespace System::Runtime::InteropServices; 
using namespace SNC::OptiRamp::Services::fDefs;

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace RodPump
      {


        //
        // *** fRPProject ****
        //
        public ref class fRPProject sealed : public fProject, IRPProject 
        {
        public:
          //
          static fRPProject()
          {
            const int I = 0;
          }

          //
          //
          fRPProject(fDiagnostics::IOptiRampLog^ logService_, System::String^ sAppName) : fProject( logService_, sAppName )
          {
          } // .............................. ctor .............


          // ------------------- IRPProject --------------------------
          //
          //
          virtual System::Collections::Generic::IEnumerable<IElement^> ^ GetRPElems( [ System::Runtime::InteropServices::Out ] System::String ^ %  sError )
          {
            try
            {
              if ( pProjectRoot == nullptr )
              {
                sError = "project is not parsed";
                return nullptr;
              } 
              else 
              {
                _ASSERT( pProjectComputer != nullptr );
              }

              //find EU object
              aCONF::C_Project_Node_EU ^ const pEU = aCONF::C_Project_Node_EU::Find_EU_Node_throw( pProjectRoot );
              pEU->Dump_Me();

              //extract EU project settings
              pEU->Write_Project_Settings( false );
              aGAS::Dump_Project_Settings();


              System::Collections::Generic::List<IElement ^> ^ pRet = gcnew System::Collections::Generic::List < IElement ^ > ;

              if ( pProjectComputer != nullptr )
              {
                aCONF::C_Nodes_Names  oCheck_Names;

                aCONF::C_Project_Node ^ pIter = pProjectComputer;
                while ( (pIter = aCONF::C_Project_Node::Traverse( pIter, pProjectComputer )) != nullptr )
                {
                  aOIL::C_Project_Node_Rod_Pump ^ const pRod_Pump = dynamic_cast<aOIL::C_Project_Node_Rod_Pump ^>(pIter);

                  if ( pRod_Pump == nullptr ) 
                  { 
                    continue; 
                  }

                  aCONF::Check_For_Duplicated_Names_In_Items_List( %oCheck_Names, pIter );

                  System::String ^ sCheck = aFW::Check_File_Name( pIter->Get_UI_Name() );
                  if ( sCheck != nullptr )
                  {
                    sError = System::String::Format( "[{0}] : {1}", pIter->Get_UI_Name(), sCheck );
                    return nullptr;
                  }

                  pRod_Pump->Get_Library();


                  pRet->Add( pNode_Elements[ pIter ] );
                } // while ( (pIter  ......


              } // if ( pMain_Computer != nullptr ) .....
              return pRet;
            } 
            catch ( System::Exception ^ pErr ) 
            {
              sError = pErr->Message;
              return nullptr;
            }
          } // .................................. GetRPElems .......................
        }; // ............................... fRPProject ........................

      } // namespace RodPump

    } // namespace Services
  } // namespace OptiRamp
} // namespace SNC



namespace aOIL
{
  


  //
  // Fill_Rod_Pump_Project_Properties
  //
  void Fill_Rod_Pump_Project_Properties(
    aCONF::C_Project_Node                                             ^ pSource_Node,
    SNC::OptiRamp::Services::fDefs::fElement::C_Element_Properties    ^ pTarget_Properties,
    bool                                                              % bHandled
    )
  {
    _ASSERT( pTarget_Properties != nullptr );
    C_Project_Node_Rod_Pump ^ const pNode_Rod_Pump = dynamic_cast<aOIL::C_Project_Node_Rod_Pump ^>(pSource_Node);
    if ( pNode_Rod_Pump == nullptr ) { return; }

    C_Project_Node_Rod_Pump_Lib ^ const pNode_Rod_Pump_Lib = pNode_Rod_Pump->Get_Assoc_Lib();


    int iParams_Count;
    const aCONF::C_Param_Descr * pParams = C_Project_Node_Rod_Pump_Lib::Get_Param_Descr( iParams_Count );
    if ( iParams_Count <= 0 ) { _ASSERT( false ); return; }


    for ( int iP = 0; iP < iParams_Count; ++iP )
    {
      System::String ^ const sIter_Key = string_to_String( pParams[ iP ].sProperty );
      _ASSERT( !pTarget_Properties->ContainsKey( sIter_Key ) );
      
      fPropertyDouble ^ const pIter_Prop = gcnew fPropertyDouble( string_to_String(pParams[ iP ].sName) );
      pIter_Prop->pEU = SNC::OptiRamp::Services::ProjectEUFactory::fProjectEUFactory::CreateInstance( pParams[ iP ].pConv );

      System::String ^ sValue = nullptr;
      if ( pNode_Rod_Pump->oTagValue.TryGetValue( sIter_Key, sValue )
        && System::String::IsNullOrEmpty( sValue )
        )
      {
        sValue = nullptr;
      }
      if ( sValue == nullptr
        && pNode_Rod_Pump_Lib != nullptr
        && pNode_Rod_Pump_Lib->oTagValue.TryGetValue( sIter_Key, sValue )
        && System::String::IsNullOrEmpty( sValue )
        )
      {
        sValue = nullptr;
      }

      pIter_Prop->oValue = fPropertyDouble::String_to_NDouble( sValue );
      pIter_Prop->pSetter = gcnew C_Setter_Tag_Value( % pNode_Rod_Pump->oTagValue, sIter_Key );
        
      pTarget_Properties->Add( sIter_Key, pIter_Prop );

    } // for ( int iP .....

    bHandled = true;
  } // ................................................. Fill_Rod_Pump_Project_Properties ...................................

} // .......................... namespace aOIL ...................................



