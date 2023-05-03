//
// Implementation of the IElement, IProject, IElemType interfaces
//
#include "fProject.h"
#include "fElementType.h"
#include "fElement.h"
#include "fProperty.h"
#include "fWebChannel.h"
#include "fWeb_HMI_Proxy.h"
#include "fProject_Rod_Pump.h"

#include "..\..\common\Config\Config_Project_Tree_Utils.h"
#include "..\..\common\Config\Config_Project_File.h"
#include "..\..\common\Config\Config_Set_Project.h"
#include "..\..\common\Config\Config_Set_HMI_ID.h"
#include "..\..\common\Config\Config_Set_Signals_OPC.h"
#include "..\..\common\CC\Config_Set_CC_App.h"
#include "..\..\common\DB\Config_Set_DB.h"
#include "..\..\common\DM\Config_Set_Data_Mining.h"
#include "..\..\common\ODBC\Config_Set_ODBC.h"
#include "..\..\common\Oil\Config_Set_Rod_Pump.h"
#include "..\..\common\Utils_Configfile.h"
#include "..\..\common\Utils_Delegate.h"
#include "..\..\common\Utils_Main.h"
#include "..\..\common\Utils_Main_fC.h"
#include "..\..\common\Utils_Net_XML.h"
#include "..\..\common\VTS\Config_Set_Signals_VTS.h"
#include "..\..\common\Web\Config_Set_Web_Style.h"
#include "..\..\common\Web\Web_Export_HMI.h"

#include <crtdbg.h>

// .Net namespaces
using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

using namespace SNC::OptiRamp::Services::fDiagnostics;

//
// Fill_Project_Specific_Properties
//
static void Fill_Project_Specific_Properties( 
  aCONF::C_Project_Node           ^ pSource_Node, 
  fElement::C_Element_Properties  ^ pTarget_Properties, 
  bool                            % bHandled )
{
  if (bHandled)
  {
    return;
  }

  { // ODBC Connection
    aODBC::C_Project_Node_ODBC_Connection ^ const pODBC_Connection 
      = dynamic_cast<aODBC::C_Project_Node_ODBC_Connection ^>(pSource_Node);

    if (pODBC_Connection != nullptr)
    {
      pTarget_Properties->Add("ConnectionString",
        gcnew fPropertyString( aODBC::C_Project_Node_ODBC_Connection::ctConnectionString,
        pODBC_Connection->sConnectionString));

      bHandled = true;
      return;
    }    
  } // ODBC Connection...

  { // ODBC Source
    aOIL::C_Project_Node_Rod_Pump_ODBC ^ const pRodPump_ODBC 
            = dynamic_cast<aOIL::C_Project_Node_Rod_Pump_ODBC ^>(pSource_Node);

    if (pRodPump_ODBC != nullptr)
    {
      pTarget_Properties->Add("MonitorQuery",
        gcnew fPropertyString( aOIL::C_Project_Node_Rod_Pump_ODBC::ctMonitorQuery, pRodPump_ODBC->sMonitorQuery ) );

      pTarget_Properties->Add("ReadQuery",
        gcnew fPropertyString( aOIL::C_Project_Node_Rod_Pump_ODBC::ctReadQuery, pRodPump_ODBC->sReadQuery ) );

      pTarget_Properties->Add("WellHeadPressureQuery",
        gcnew fPropertyString( aOIL::C_Project_Node_Rod_Pump_ODBC::ctWellHeadPressure, pRodPump_ODBC->sWellHeadPressureQuery ) );

      bHandled = true;  
      return;
    }
  } // ODBC Source...

  { // ODBC Group
    aODBC::C_Project_Node_ODBC_Group ^ const pODBC_Group = dynamic_cast<aODBC::C_Project_Node_ODBC_Group ^>(pSource_Node);

    if (pODBC_Group != nullptr)
    {
      pTarget_Properties->Add( "Default_SQL", gcnew fPropertyString( aODBC::C_Project_Node_ODBC_Group::ctDefaultSQL, pODBC_Group->sDefault_SQL ) );

      bHandled = true;
      return;
    }

  } // ODBC Group...

  { // ODBC Item
    aODBC::C_Project_Node_ODBC_Item^ const pODBC_Item = dynamic_cast<aODBC::C_Project_Node_ODBC_Item^>(pSource_Node);

    if (pODBC_Item != nullptr)
    {
      bool bTime_SQL_Params = false;
      System::String^ sqlQuery;

      pODBC_Item->Create_SQL(DateTime::MinValue, false, DateTime::MaxValue, bTime_SQL_Params, sqlQuery);

      pTarget_Properties->Add( "SQL_TVS", gcnew fPropertyString( aODBC::C_Project_Node_ODBC_Item::ctSQL_TVS, sqlQuery ) );

      bHandled = true;
      return;
    }
  } // ODBC Item...

  { // Computer
    aCONF::C_Project_Node_Computer ^ const pNode_Computer = dynamic_cast<aCONF::C_Project_Node_Computer^>(pSource_Node);
    if (pNode_Computer != nullptr)
    {
      pTarget_Properties->Add(
        aCONF::C_Project_Node_Computer::ctNetworkAddress,
        gcnew fPropertyString( aCONF::C_Project_Node_Computer::ctNetworkAddress, pSource_Node, "NetworkAddress" ) );

      bHandled = true;  return;
    }
  } // Computer ......

  { // OPC Server
    aCONF::C_Project_Node_OPC_Server ^ const pNode_Server = dynamic_cast<aCONF::C_Project_Node_OPC_Server ^>(pSource_Node);

    if ( pNode_Server != nullptr )
    {
      pTarget_Properties->Add( "OPC_Name", gcnew fPropertyString( "OPC Name", pSource_Node, "OPC_Name" ) );

      bHandled = true;  
      return;
    }
  } // OPC Server ......

  { // OPC Group
    aCONF::C_Project_Node_OPC_Group ^ const pNode_Group = dynamic_cast<aCONF::C_Project_Node_OPC_Group ^>(pSource_Node);

    if ( pNode_Group != nullptr )
    {
      pTarget_Properties->Add( "Prefix", gcnew fPropertyString( aCONF::C_Project_Node_OPC_Group::ctPrefix, pSource_Node, "Prefix" ) );
      pTarget_Properties->Add( "RefreshRate", gcnew fPropertyInt( aCONF::C_Project_Node_OPC_Group::ctRefreshRate, pSource_Node, "RefreshRate" ) );

      bHandled = true;  
      return;
    }
  } // OPC Group ......

  { // OPC Signal
    auto pNode_Signal = dynamic_cast<aCONF::C_Project_Node_OPC_Signal ^>(pSource_Node);
    if ( pNode_Signal != nullptr )
    {
      pTarget_Properties->Add( "Tag", gcnew fPropertyString( "Tag", pSource_Node, "Tag" ) );

      bHandled = true;  return;
    }
  } // OPC Signal ......


} // ................................................... Fill_Project_Specific_Properties ...................................

void Init_Project_Specific_Definitions()
{
  aCONF::Init_Set_Signals_OPC( false );
  aODBC::Init_Set_ODBC( false );
  aVTS::Init_Set_Signals_VTS( false );

  aOIL::Init_Set_Rod_Pump( false, nullptr );
  fElement::dlg_Fill_Properties += gcnew fElement::DLG_Fill_Properties( aOIL::Fill_Rod_Pump_Project_Properties );
  aOIL::Init_Set_Rod_Pump_ODBC( aCONF::C_Project_Node_Type::Find_Item( aODBC::C_Constants_ODBC::pTag_Connection ), false, nullptr );

  aCC::Init_Set_CC_App( false );
  aWEB::Init_Set_Web_Page(false);

  aWEB::Init_Set_Web_Style(false);
  aWIKI::Init_Set_Wiki(false);

  fElement::dlg_Fill_Properties += gcnew fElement::DLG_Fill_Properties(Fill_Project_Specific_Properties);
}

void fProject::InitializeModelObjects()
{
  if ( aCONF::C_Project_Node_Type::Types->Count > 0 )
  {
    // we do initialization only once
    _ASSERT(fElementType::oElementTypes.Count > 0); // paranoidal
    return;
  }

  aCONF::Init_Set_Basic( false );
  aDB::Init_Set_Database( false );
  aDM::Init_Set_Data_Mining( false, nullptr );

  Init_Project_Specific_Definitions();

  aHMI::C_HMI_Item::bSkipUnknownItemsOnCreation = true;

  aCONF::Strip_Config_Tree_For_Runtime();

  for each (aCONF::C_Project_Node_Type ^ pIter in aCONF::C_Project_Node_Type::Types)
  {
    fElementType::Add(gcnew fElementType(pIter));
  }
} 

//
// ResetProjectRoot
//
void fProject::ResetProjectRoot()
{
  if ( pProjectRoot != nullptr )
  {
    pProjectRoot->Delete_Me();
    delete pProjectRoot; //paranoidal, root doesn't have Dispose
    pProjectRoot = nullptr;
    pProjectComputer = nullptr;
  } else {
    _ASSERT( pProjectComputer == nullptr );
  }
} // .................................... ResetProjectRoot() ...................

void fProject::SetProjectRoot( aCONF::C_Project_Node ^ pNewProjectRoot, aCONF::C_Project_Node ^ pNewProjectComputer, bool bEnhanced_Web_HMI )
{
  ResetProjectRoot();

  pProjectRoot = pNewProjectRoot;
  pProjectComputer = pNewProjectComputer;

  if (pProjectRoot != nullptr)
  {
    allElementList = gcnew C_All_Element_List;
    pNode_Elements = gcnew C_Node_Element;
    pImageElements = gcnew C_Image_Element_List;

    aCONF::C_Project_Node ^ pIter = pProjectRoot;
    allElementList->Add(gcnew fElement(allElementList->Count, pIter, pNode_Elements));

    aWEB::C_Project_Node_Web_Server ^ pWebServer;

    while ((pIter = aCONF::C_Project_Node::Traverse(pIter, pProjectRoot)) != nullptr)
    {
      // Skip web styles. They should not be elements, because their properties are already applied to HMI objects.
      // Skip chart styles. They will be parsed as objecttemplates in Parse_HMI_Items().
      // Skip Unknown nodes.
      if (dynamic_cast<aWEB::C_Project_Node_Styles ^>(pIter) != nullptr ||
        dynamic_cast<aWEB::C_Project_Node_Style ^>(pIter) != nullptr ||
        dynamic_cast<aWEB::C_Trend_Chart_Style ^>(pIter) != nullptr ||
        dynamic_cast<aWEB::C_Report_Trend_Chart_Style ^>(pIter) != nullptr ||
        dynamic_cast<aCONF::C_Project_Node_Unknown ^>(pIter) != nullptr)
      {
        continue;
      }

      fElement ^ pNewElement = gcnew fElement(allElementList->Count, pIter, pNode_Elements);
      allElementList->Add(pNewElement);

      aWEB::C_Project_Node_Web_Server ^ const pWebServerTemp = dynamic_cast<aWEB::C_Project_Node_Web_Server ^>(pIter);
      if (pWebServerTemp != nullptr)
      {
        pWebServer = pWebServerTemp;
      }
    }
    if ( bEnhanced_Web_HMI )
    {
      Parse_HMI_Items(pWebServer);
    }
  }
  else 
  {
    // paranoidal
    allElementList = nullptr;
    sProjectFileName = nullptr;
  }
}

void fProject::CreateProjectNodeTree( String ^ sFileName, String ^ sImpersonatedComputer, bool bEnhanced_Web_HMI )
{
  aFW::Write_Log_Line();
  aFW::C_Start_Stop_Trace oTrace( "open project" );

  aCONF::C_HMI_ID::Reset_NextUniqueID();

  aCONF::C_Project_Node ^ pRoot = aCONF::Project_Open( sFileName );

  try
  {
    aCONF::C_Project_Node_Root ^ const pProjectRoot = safe_cast<aCONF::C_Project_Node_Root ^>(pRoot);
    pProjectRoot->sSourceProjectFile = sFileName;

    Strip_Config_Tree_For_Runtime(pProjectRoot);

    aCONF::C_Project_Node ^ const pComputer = Find_Impersonated_Computer( pProjectRoot, sImpersonatedComputer );

    SetProjectRoot( null_and_return( pRoot ), pComputer, bEnhanced_Web_HMI );
  } catch ( System::Exception ^ pErr ) {
    System::Diagnostics::Trace::WriteLine( pErr->ToString() );
    throw;
  } finally {
    delete pRoot;
  }
}

//
//
fProject::fProject(IOptiRampLog^ logService_, String^ sAppName )
{
  aFW::Log::Register( logService_, sAppName );
}

String ^ fProject::Path::get()
{
  return sProjectFileName;
}

IElement ^ fProject::Root::get()
{
  return Safe_Length( allElementList ) == 0 ? nullptr : allElementList[ 0 ];
}

IElement ^ fProject::GetElemById(int elementId)
{
  if (allElementList != nullptr)
  {
    for each(IElement ^ pElement in allElementList)
    {
      if (pElement->Id == elementId) 
      { 
        return pElement; 
      }
    }
  }

  return nullptr;
}

IElement ^ fProject::default::get(int index)
{
  return (allElementList == nullptr) ? nullptr : allElementList[index];
}

void fProject::default::set(int index, IElement ^ newElement)
{
  throw gcnew NotImplementedException;
}

int fProject::GetElementsCount()
{
  return Safe_Length(allElementList);
}

IElemType ^ fProject::GetElemType(String ^ TypeID)
{
  _ASSERT(fElementType::oElementTypes.Count > 0);

  for each (fElementType ^ pElementType in fElementType::oElementTypes)
  {
    if (System::String::CompareOrdinal(pElementType->TypeID, TypeID) == 0)
      return pElementType;
  }
  return nullptr;
}

IEnumerable<IElement^> ^ fProject::GetElemsOfType(IElemType ^ elemType)
{
  C_All_Element_List  ^ const pRet = gcnew C_All_Element_List;

  if (allElementList == nullptr || elemType == nullptr)
  {
    _ASSERT(false); return pRet;
  }
  for each (IElement  ^ pIter in  allElementList)
  {
    if (System::String::CompareOrdinal(pIter->Type->Name, elemType->Name) == 0 &&
        System::String::CompareOrdinal(pIter->Type->TypeID, elemType->TypeID) == 0)
    {
      pRet->Add(pIter);
    }
  }

  return pRet;
}

//
// Open
//
IElement ^ fProject::Open(
  String ^ sProjectFileName_, 
  IDictionary<String ^, String ^> ^ options, 
  [Out] ResponseStatus ^% status)
{
  status = gcnew ResponseStatus;
  DateTime tStart = DateTime::Now;
  IElement ^ pRet = nullptr;
  bool bEnhanced_Web_HMI = false;

  try
  {
    if (String::IsNullOrEmpty(sProjectFileName_))
    {
      status->Message = "empty ProjectFileName";
    }
    else 
    {
      InitializeModelObjects();

      sProjectFileName = sProjectFileName_;

      String ^ sImpersonatedComputer;

      if (options != nullptr)
      {
        if ( !options->TryGetValue( "c", sImpersonatedComputer ) && String::IsNullOrEmpty( sImpersonatedComputer ) )
        {
          sImpersonatedComputer = nullptr;
        }
        bEnhanced_Web_HMI = options->ContainsKey( Option_Enhanced_Web_HMI );
      }

      if (bEnhanced_Web_HMI)
      {
        aWEB::Init_Web_HMI();
      } else {
        aWEB::Init_Web_HMI_Runtime();
      }


      CreateProjectNodeTree( sProjectFileName, sImpersonatedComputer, bEnhanced_Web_HMI );

      pRet = pNode_Elements[pProjectComputer];
      _ASSERT(IsOpen());
    } // if ( System::String::IsNullOrEmpty( sProjectFileName_ ) ) .......

  }
  catch (Exception ^ pErr) 
  {
    status->IsError = true;
    status->Message = pErr->Message;
    pRet = nullptr;
  }
  finally 
  {
    status->SpentTime = DateTime::Now - tStart;
  }

#ifdef _DEBUG
  if (fElementType::pUnknowns->Count > 0)
  {
    aFW::Log::WriteRecord(">>>>> Found Unknown Types !!!!");
    for each (System::String ^ sUnknown in fElementType::pUnknowns)
    {
      aFW::Log::WriteRecord("      " + sUnknown);
    }
  }
#endif // _DEBUG

  if (status->IsError)
  {
    System::String ^ sError = "Error while parsing the project file: \"" + status->Message + "\"";
    aFW::Log::WriteRecord(sError);
    if (bEnhanced_Web_HMI)
    {
      InjectErrorPage(sError);
    }
  }

  return pRet;
} // ..................................... Open .................................

//
// Save
//
bool fProject::Save( [ System::Runtime::InteropServices::Out ] ResponseStatus ^%  status )
{
  status = gcnew ResponseStatus;
  status->Message = "is not implemented";
  status->IsError = true;
  return false;
} // ...................................... Save ....................................

//
// AddElement
//
int fProject::AddElement(
  int               parentId,
  System::String  ^ sName,
  IElemType       ^ type,
  System::String  ^ sDescription,
  System::Collections::Generic::IDictionary<System::String^, IProperty^> ^ properties,
  [ System::Runtime::InteropServices::Out ]  System::String ^ % error
  )
{
  error = System::String::Format( "AddElement(parentId={0}) is not implemented", parentId );
  return ProjectConstants::invalidHandle;
}


bool fProject::DeleteElement(
  int elementId,
  [ System::Runtime::InteropServices::Out ]  System::String ^ % error
  )
{
  error = System::String::Format( "DeleteElement(elementId={0}) is not implemented", elementId );
  return false;
}


bool fProject::IsOpen()  
{ 
  return (pProjectRoot != nullptr && pProjectComputer != nullptr); 
}

IElement ^ fProject::ActiveComputer::get()
{
  return (pProjectComputer == nullptr) ? nullptr : pNode_Elements[ pProjectComputer ];
}

fProject::!fProject()
{
  ResetProjectRoot();
}

fProject::~fProject() {  this->!fProject(); }

//
// Add_Property_Peer
//
void fProject::Add_Property_Peer(IElement ^ const pElement, IProperty ^ const pPeerValue)
{
  System::String ^ sPeerConnection = dynamic_cast<System::String ^>(pPeerValue->rowValue);

  if (System::String::IsNullOrEmpty(sPeerConnection))
    return;

  double fVal;
  if (double::TryParse(sPeerConnection, fVal))
    return;

  // Find VTS Tag.
  array<System::String ^> ^ pPaths = sPeerConnection->Split(fProject::oDotSeparator);
  sPeerConnection = pPaths[pPaths->Length - 1];

  System::Collections::Generic::List<Peer> ^ pCurrentPeers = gcnew System::Collections::Generic::List<Peer>();
  pCurrentPeers->AddRange(pElement->Peers);

  for each (IElement ^ pPeerElement in allElementList)
  {
    if (System::String::CompareOrdinal(pPeerElement->Name, sPeerConnection) == 0)
    {
      Peer oPeer;
      oPeer.elemID = pPeerElement->Id;
      oPeer.peerID = System::String::Empty;
      pCurrentPeers->Add(oPeer);
      pElement->Peers = pCurrentPeers;
      break;
    }
  }
}

//
// Add_Multiple_Peers
//
void fProject::Add_Multiple_Peers(fElement ^ pChildElement)
{
  IProperty ^ pPeerValue;
  if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::MINIMUM, pPeerValue))
  {
    Add_Property_Peer(pChildElement, pPeerValue);
  }
  if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::LOW, pPeerValue))
  {
    Add_Property_Peer(pChildElement, pPeerValue);
  }
  if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::LOWVALUE, pPeerValue))
  {
    Add_Property_Peer(pChildElement, pPeerValue);
  }
  if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::LOWLOW, pPeerValue))
  {
    Add_Property_Peer(pChildElement, pPeerValue);
  }
  if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::LOWLOWVALUE, pPeerValue))
  {
    Add_Property_Peer(pChildElement, pPeerValue);
  }
  if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::HIGH, pPeerValue))
  {
    Add_Property_Peer(pChildElement, pPeerValue);
  }
  if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::HIGHVALUE, pPeerValue))
  {
    Add_Property_Peer(pChildElement, pPeerValue);
  }
  if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::HIGHHIGH, pPeerValue))
  {
    Add_Property_Peer(pChildElement, pPeerValue);
  }
  if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::HIGHHIGHVALUE, pPeerValue))
  {
    Add_Property_Peer(pChildElement, pPeerValue);
  }
  if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::MAXIMUM, pPeerValue))
  {
    Add_Property_Peer(pChildElement, pPeerValue);
  }
}

//
// Add_Value_Peer
//
void fProject::Add_Value_Peer(IElement ^ const pElement, bool bHasFullNameValue)
{
  Add_Datasource_Peer(pElement, fRT::TypeIDs::VALUE, bHasFullNameValue);
}

//
// Add_Reference_Peer
//
void fProject::Add_Reference_Peer(IElement ^ const pElement, bool bHasFullNameValue)
{
  Add_Datasource_Peer(pElement, fRT::TypeIDs::REFERENCE, bHasFullNameValue);
}

//
// Add_Popup_Peer
//
void fProject::Add_Popup_Peer(IElement ^ const pElement, bool bHasFullNameValue)
{
  Add_Datasource_Peer(pElement, fRT::TypeIDs::POPUPNAME, bHasFullNameValue);
}

//
// Add_Datasource_Peer
//
void fProject::Add_Datasource_Peer(IElement ^ const pElement, System::String ^ const sDatasourcePropertyName, bool bHasFullNameValue)
{
  _ASSERT(pElement->Properties != nullptr);

  IProperty ^ pValue;
  if (pElement->Properties->TryGetValue(sDatasourcePropertyName, pValue))
  {
    System::String ^ sPeerConnection = dynamic_cast<System::String ^>(pValue->rowValue);

    if (System::String::IsNullOrEmpty(sPeerConnection))
      return;

    if (bHasFullNameValue)
    {
      // Find VTS Tag.
      array<System::String ^> ^ pPaths = sPeerConnection->Split(fProject::oDotSeparator);
      sPeerConnection = pPaths[pPaths->Length - 1];
    }

    if (System::String::CompareOrdinal(fRT::TypeIDs::REFERENCE, sDatasourcePropertyName) == 0 ||
      System::String::CompareOrdinal(fRT::TypeIDs::POPUPNAME, sDatasourcePropertyName) == 0)
    {
      // Find Web Page.
      array<System::String ^> ^ pPaths = sPeerConnection->Split(fProject::oBrokenBarSeparator);
      sPeerConnection = pPaths[pPaths->Length - 1];
    }

    System::Collections::Generic::List<Peer> ^ pCurrentPeers = gcnew System::Collections::Generic::List<Peer>();
    pCurrentPeers->AddRange(pElement->Peers);

    for each (IElement ^ pPeerElement in allElementList)
    {
      if (System::String::CompareOrdinal(pPeerElement->Name, sPeerConnection) == 0)
      {
        Peer oPeer;
        oPeer.elemID = pPeerElement->Id;
        oPeer.peerID = System::String::Empty;
        pCurrentPeers->Add(oPeer);
        pElement->Peers = pCurrentPeers;
        break;
      }
    }
  }
} // ................................. Add_Datasource_Peer ..................................

//
// Add_Image_Peer
//
void fProject::Add_Image_Peer(IElement ^ const pElement, System::String ^ sImageName)
{
  for each (KeyValuePair<System::String ^, C_Image_Descriptor ^> ^ pKV in pImageElements)
  {
    if (System::String::CompareOrdinal(pKV->Key, sImageName) == 0)
    {
      for each (IElement ^ pImageElement in allElementList)
      {
        if (System::String::CompareOrdinal(pImageElement->Type->TypeID, fRT::TypeIDs::WEBPICTURE) == 0)
        {
          bool bConvertedID = false;
          int iImageID;

          IProperty ^ pID;
          if (pImageElement->Properties->TryGetValue(fRT::TypeIDs::ID, pID))
          {
            bConvertedID = int::TryParse(dynamic_cast<System::String ^>(pID->rowValue), iImageID);
          }

          if (bConvertedID && pKV->Value->iHmi_ID == iImageID)
          {
            System::Collections::Generic::List<Peer> ^ pCurrentPeers = gcnew System::Collections::Generic::List<Peer>();
            pCurrentPeers->AddRange(pElement->Peers);

            Peer oPeer;
            oPeer.elemID = pImageElement->Id;
            oPeer.peerID = System::String::Empty;
            pCurrentPeers->Add(oPeer);
            pElement->Peers = pCurrentPeers;
          }
        }
      }
    }
  }
} // ................................. Add_Image_Peer ..................................

//
// Add_VTS_Folder_Peer
//
void fProject::Add_VTS_Folder_Peer(IElement ^ const pElement)
{
  IProperty ^ pHmiID;
  if (pElement->Properties->TryGetValue(fRT::TypeIDs::HMIID, pHmiID))
  {
    int iHmiID;
    if (int::TryParse(dynamic_cast<System::String ^>(pHmiID->rowValue), iHmiID))
    {
      for each (IElement ^ pel in allElementList)
      {
        if (System::String::CompareOrdinal(pel->Type->TypeID, fRT::TypeIDs::VTSFOLDER) != 0)
          continue;

        bool bConvertedID = false;
        int iSearchID;

        IProperty ^ pID;
        if (pel->Properties->TryGetValue(fRT::TypeIDs::HMIID, pID))
        {
          bConvertedID = int::TryParse(dynamic_cast<System::String ^>(pID->rowValue), iSearchID);
        }

        if (bConvertedID && iHmiID == iSearchID)
        {
          System::Collections::Generic::List<Peer> ^ pCurrentPeers = gcnew System::Collections::Generic::List<Peer>();
          pCurrentPeers->AddRange(pElement->Peers);

          Peer oPeer;
          oPeer.elemID = pel->Id;
          oPeer.peerID = System::String::Empty;
          pCurrentPeers->Add(oPeer);
          pElement->Peers = pCurrentPeers;
        }
      }
    }
  }
} // ................................. Add_VTS_Folder_Peer ..................................

//
// Parse_Page_Elements (1)
//
void fProject::Parse_Page_Elements(fElement ^ pParentElement, System::Xml::XmlNode ^ const pXmlParentNode, System::String ^ sHMI_Type)
{
  _ASSERT(pParentElement != nullptr);
  _ASSERT(pXmlParentNode != nullptr);
  _ASSERT(sHMI_Type != nullptr);

  // Handle HMI children.
  if (System::String::CompareOrdinal(pParentElement->Type->TypeID, fRT::TypeIDs::DYNAMICTEXT) == 0 ||
    System::String::CompareOrdinal(pParentElement->Type->TypeID, fRT::TypeIDs::DYNAMICCONNECTOR) == 0)
  {
    Add_Value_Peer(pParentElement, false);
    Add_Reference_Peer(pParentElement, false);
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "PopupLink") == 0)
  {
    Add_VTS_Folder_Peer(pParentElement);
    Add_Popup_Peer(pParentElement, false);
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "UpdatableText") == 0)
  {
    Add_Value_Peer(pParentElement, false);
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "StaticLine") == 0)
  {
    System::Xml::XmlNode ^ const pSegmentsNode = Find_Child_Node(pXmlParentNode, "segments");
    if (pSegmentsNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pSegmentsNode, "segment");
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "StaticImage") == 0)
  {
    IProperty ^ pImage;
    if (pParentElement->Properties->TryGetValue(fRT::TypeIDs::IMAGE, pImage))
      Add_Image_Peer(pParentElement, dynamic_cast<System::String ^>(pImage->rowValue));

    Add_Reference_Peer(pParentElement, false);
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "DynamicImage") == 0)
  {
    System::Xml::XmlNode ^ const pImagestatesNode = Find_Child_Node(pXmlParentNode, "imagestates");
    if (pImagestatesNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pImagestatesNode, "imagestate");

    Add_Value_Peer(pParentElement, false);
    Add_Reference_Peer(pParentElement, false);
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "CircularGauge") == 0 || System::String::CompareOrdinal(sHMI_Type, "SpeedGauge") == 0)
  {
    System::Xml::XmlNode ^ const pGaugeNode = Find_Child_Node(pXmlParentNode, "gauge");
    if (pGaugeNode != nullptr)
    {
      fElement ^ const pGaugeElement = Create_HMI_Child_Element(pParentElement);
      Set_HMI_Child_Properties(pGaugeElement, pGaugeNode, "gauge");
    }

    Add_Value_Peer(pParentElement, true);
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "Meter") == 0)
  {
    System::Xml::XmlNode ^ const pTitlesNode = Find_Child_Node(pXmlParentNode, "titles");
    if (pTitlesNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pTitlesNode, "title");

    System::Xml::XmlNode ^ const pMeterNode = Find_Child_Node(pXmlParentNode, "meter");
    if (pMeterNode != nullptr)
    {
      fElement ^ const pMeterElement = Create_HMI_Child_Element(pParentElement);
      Set_HMI_Child_Properties(pMeterElement, pMeterNode, "meter");
    }

    Add_Value_Peer(pParentElement, true);
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "SpiderPlot") == 0)
  {
    System::Xml::XmlNode ^ const pDatapointsNode = Find_Child_Node(pXmlParentNode, "datapoints");
    if (pDatapointsNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pDatapointsNode, "datapoint");

    Add_Multiple_Peers(pParentElement);
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "PieChart") == 0 ||
    System::String::CompareOrdinal(sHMI_Type, "FixedPieChart") == 0)
  {
    System::Xml::XmlNode ^ const pTitlesNode = Find_Child_Node(pXmlParentNode, "titles");
    if (pTitlesNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pTitlesNode, "title");

    System::Xml::XmlNode ^ const pDatapointsNode = Find_Child_Node(pXmlParentNode, "datapoints");
    if (pDatapointsNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pDatapointsNode, "datapoint");
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "BarChart") == 0)
  {
    System::Xml::XmlNode ^ const pTitlesNode = Find_Child_Node(pXmlParentNode, "titles");
    if (pTitlesNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pTitlesNode, "title");

    System::String ^ sDataMode = Attribute_to_String(pXmlParentNode, "datamode");
    if (System::String::CompareOrdinal(sDataMode, "Trending") == 0)
    {
      System::Xml::XmlNode ^ const pHistoricalNode = Find_Child_Node(pXmlParentNode, "historical");
      if (pHistoricalNode != nullptr && pHistoricalNode->HasChildNodes)
      {
        fElement ^ const pHistoricalElement = Create_HMI_Child_Element(pParentElement);
        Set_HMI_Child_Properties(pHistoricalElement, pHistoricalNode, "historical");

        System::Xml::XmlNode ^ const pChannelsNode = Find_Child_Node(pHistoricalNode, "channels");
        if (pChannelsNode != nullptr)
          Create_HMI_Children_Elements(pHistoricalElement, pChannelsNode, "channel");
      }
    }
    else if (System::String::CompareOrdinal(sDataMode, "Realtime") == 0)
    {
      System::Xml::XmlNode ^ const pDatapointsNode = Find_Child_Node(pXmlParentNode, "datapoints");
      if (pDatapointsNode != nullptr && pDatapointsNode->HasChildNodes)
      {
        for each (System::Xml::XmlNode ^ pDataPointNode in pDatapointsNode->ChildNodes)
        {
          fElement ^ const pDataPointElement = Create_HMI_Child_Element(pParentElement);
          Set_HMI_Child_Properties(pDataPointElement, pDataPointNode, "datapoint");

          System::Xml::XmlNode ^ const pSetPointsNode = Find_Child_Node(pDataPointNode, "setpoints");
          if (pSetPointsNode != nullptr)
            Create_HMI_Children_Elements(pDataPointElement, pSetPointsNode, "setpoint");
        }
      }
    }
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "TrendChart") == 0 ||
    System::String::CompareOrdinal(sHMI_Type, "REPORT_TREND_CHART_STYLE") == 0 ||
    System::String::CompareOrdinal(sHMI_Type, "TREND_CHART_STYLE") == 0)
  {
    System::Xml::XmlNode ^ const pChartNode = Find_Child_Node(pXmlParentNode, "chart");
    if (pChartNode != nullptr)
    {
      fElement ^ const pChartElement = Create_HMI_Child_Element(pParentElement);
      Set_HMI_Child_Properties(pChartElement, pChartNode, "chart");
    }

    System::Xml::XmlNode ^ const pTitlesNode = Find_Child_Node(pXmlParentNode, "titles");
    if (pTitlesNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pTitlesNode, "title");

    System::Xml::XmlNode ^ const pRequestNode = Find_Child_Node(pXmlParentNode, "request");
    if (pRequestNode != nullptr)
    {
      fElement ^ const pRequestElement = Create_HMI_Child_Element(pParentElement);
      Set_HMI_Child_Properties(pRequestElement, pRequestNode, "request");

      System::Xml::XmlNode ^ const pAxesNode = Find_Child_Node(pRequestNode, "axes");
      if (pAxesNode != nullptr && pAxesNode->HasChildNodes)
      {
        for each (System::Xml::XmlNode ^ pAxisNode in pAxesNode->ChildNodes)
        {
          fElement ^ const pAxisElement = Create_HMI_Child_Element(pRequestElement);
          Set_HMI_Child_Properties(pAxisElement, pAxisNode, "axis");

          System::Xml::XmlNode ^ const pChannelsNode = Find_Child_Node(pAxisNode, "channels");
          if (pChannelsNode != nullptr)
            Create_HMI_Children_Elements(pAxisElement, pChannelsNode, "channel");
        }
      }
    }
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "NormalDistributionPlot") == 0)
  {
    System::Xml::XmlNode ^ const pTitlesNode = Find_Child_Node(pXmlParentNode, "titles");
    if (pTitlesNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pTitlesNode, "title");

    System::Xml::XmlNode ^ const pNdPlotNode = Find_Child_Node(pXmlParentNode, "ndplot");
    if (pNdPlotNode != nullptr)
    {
      fElement ^ const pNdPlotElement = Create_HMI_Child_Element(pParentElement);
      Set_HMI_Child_Properties(pNdPlotElement, pNdPlotNode, "ndplot");

      System::Xml::XmlNode ^ const pLegendNode = Find_Child_Node(pNdPlotNode, "legend");
      if (pLegendNode != nullptr)
      {
        fElement ^ const pLegendElement = Create_HMI_Child_Element(pNdPlotElement);
        Set_HMI_Child_Properties(pLegendElement, pLegendNode, "legend");

        System::Xml::XmlNode ^ const pTitleNode1 = Find_Child_Node(pLegendNode, "title");
        if (pTitleNode1 != nullptr)
        {
          fElement ^ const pTitleElement1 = Create_HMI_Child_Element(pLegendElement);
          Set_HMI_Child_Properties(pTitleElement1, pTitleNode1, "title");
        }

        System::Xml::XmlNode ^ const pPointsNode = Find_Child_Node(pLegendNode, "points");
        if (pPointsNode != nullptr)
          Create_HMI_Children_Elements(pLegendElement, pPointsNode, "point");
      } // if (pLegendNode != nullptr)

      fElement ^ pDistanceElement = nullptr;
      System::Xml::XmlNode ^ const pDistanceNode = Find_Child_Node(pNdPlotNode, "distance");
      if (pDistanceNode != nullptr)
      {
        pDistanceElement = Create_HMI_Child_Element(pNdPlotElement);
        Set_HMI_Child_Properties(pDistanceElement, pDistanceNode, "distance");

        System::Xml::XmlNode ^ const pTitleNode2 = Find_Child_Node(pDistanceNode, "title");
        if (pTitleNode2 != nullptr)
        {
          fElement ^ const pTitleElement2 = Create_HMI_Child_Element(pDistanceElement);
          Set_HMI_Child_Properties(pTitleElement2, pTitleNode2, "title");
        }
      } // if (pDistanceNode != nullptr)

      System::Xml::XmlNode ^ const pGridNode = Find_Child_Node(pNdPlotNode, "grid");
      if (pGridNode != nullptr)
      {
        fElement ^ pGridElement = Create_HMI_Child_Element(pNdPlotElement);
        Set_HMI_Child_Properties(pGridElement, pGridNode, "grid");
      }

      System::String ^ sPlotType = Attribute_to_String(pXmlParentNode, "plottype");
      if (System::String::CompareOrdinal(sPlotType, "Channels") == 0)
      {
        System::Xml::XmlNode ^ const pChannelsNode = Find_Child_Node(pNdPlotNode, "channels");
        if (pChannelsNode != nullptr)
          Create_HMI_Children_Elements(pNdPlotElement, pChannelsNode, "channel");
      }
      else if (System::String::CompareOrdinal(sPlotType, "Targets") == 0)
      {
        System::Xml::XmlNode ^ const pTargetValuesNode = Find_Child_Node(pNdPlotNode, "targetvalues");
        if (pTargetValuesNode != nullptr)
        {
          fElement ^ const pTargetValuesElement = Create_HMI_Child_Element(pDistanceElement);
          Set_HMI_Child_Properties(pTargetValuesElement, pTargetValuesNode, "targetvalues");

          Create_HMI_Children_Elements(pTargetValuesElement, pTargetValuesNode, "targetvalue");
        }
      }
    } // if (pNdPlotNode != nullptr)
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "BoxAndWhiskerPlot") == 0)
  {
    System::Xml::XmlNode ^ const pTitlesNode = Find_Child_Node(pXmlParentNode, "titles");
    if (pTitlesNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pTitlesNode, "title");

    System::Xml::XmlNode ^ const pPlotNode = Find_Child_Node(pXmlParentNode, "plot");
    if (pPlotNode != nullptr)
    {
      fElement ^ const pPlotElement = Create_HMI_Child_Element(pParentElement);
      Set_HMI_Child_Properties(pPlotElement, pPlotNode, "plot");

      System::Xml::XmlNode ^ const pLegendNode = Find_Child_Node(pPlotNode, "legend");
      if (pLegendNode != nullptr)
      {
        fElement ^ const pLegendElement = Create_HMI_Child_Element(pPlotElement);
        Set_HMI_Child_Properties(pLegendElement, pLegendNode, "legend");

        System::Xml::XmlNode ^ const pTitleNode1 = Find_Child_Node(pLegendNode, "title");
        if (pTitleNode1 != nullptr)
        {
          fElement ^ const pTitleElement1 = Create_HMI_Child_Element(pLegendElement);
          Set_HMI_Child_Properties(pTitleElement1, pTitleNode1, "title");
        }

        System::Xml::XmlNode ^ const pPointsNode = Find_Child_Node(pLegendNode, "points");
        if (pPointsNode != nullptr)
          Create_HMI_Children_Elements(pLegendElement, pPointsNode, "point");
      }

      System::Xml::XmlNode ^ const pDistanceNode = Find_Child_Node(pPlotNode, "distance");
      if (pDistanceNode != nullptr)
      {
        fElement ^ const pDistanceElement = Create_HMI_Child_Element(pPlotElement);
        Set_HMI_Child_Properties(pDistanceElement, pDistanceNode, "distance");

        System::Xml::XmlNode ^ const pTitleNode2 = Find_Child_Node(pDistanceNode, "title");
        if (pTitleNode2 != nullptr)
        {
          fElement ^ pTitleElement2 = Create_HMI_Child_Element(pDistanceElement);
          Set_HMI_Child_Properties(pTitleElement2, pTitleNode2, "title");
        }
      }

      fElement ^ pGridElement = nullptr;
      System::Xml::XmlNode ^ const pGridNode = Find_Child_Node(pPlotNode, "grid");
      if (pGridNode != nullptr)
      {
        pGridElement = Create_HMI_Child_Element(pPlotElement);
        Set_HMI_Child_Properties(pGridElement, pGridNode, "grid");

        System::Xml::XmlNode ^ const pScaleNode = Find_Child_Node(pGridNode, "scale");
        if (pScaleNode != nullptr)
        {
          fElement ^ const pScaleElement = Create_HMI_Child_Element(pGridElement);
          Set_HMI_Child_Properties(pScaleElement, pScaleNode, "scale");
        }
      }

      System::String ^ sPlotType = Attribute_to_String(pXmlParentNode, "plottype");
      if (System::String::CompareOrdinal(sPlotType, "Channels") == 0)
      {
        System::Xml::XmlNode ^ const pChannelNode = Find_Child_Node(pGridNode, "channel");
        if (pChannelNode != nullptr)
        {
          fElement ^ const pChannelElement = Create_HMI_Child_Element(pGridElement);
          Set_HMI_Child_Properties(pChannelElement, pChannelNode, "channel");
        }

        System::Xml::XmlNode ^ const pChannelsNode = Find_Child_Node(pPlotNode, "channels");
        if (pChannelsNode != nullptr)
        {
          fElement ^ const pChannelsElement = Create_HMI_Child_Element(pPlotElement);
          Set_HMI_Child_Properties(pChannelsElement, pChannelsNode, "channels");
          Create_HMI_Children_Elements(pChannelsElement, pChannelsNode, "channel");
        }
      }
      else if (System::String::CompareOrdinal(sPlotType, "Targets") == 0)
      {
        System::Xml::XmlNode ^ const pTargetValuesNode = Find_Child_Node(pPlotNode, "targetvalues");
        if (pTargetValuesNode != nullptr)
        {
          fElement ^ const pTargetValuesElement = Create_HMI_Child_Element(pPlotElement);
          Set_HMI_Child_Properties(pTargetValuesElement, pTargetValuesNode, "targetvalues");

          Create_HMI_Children_Elements(pTargetValuesElement, pTargetValuesNode, "targetvalue");
        }
      }
    }
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "Table") == 0)
  {
    System::Xml::XmlNode ^ const pTitlesNode = Find_Child_Node(pXmlParentNode, "titles");
    if (pTitlesNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pTitlesNode, "title");

    System::Xml::XmlNode ^ const pRowsNode = Find_Child_Node(pXmlParentNode, "rows");
    if (pRowsNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pRowsNode, "row");

    System::Xml::XmlNode ^ const pColumnsNode = Find_Child_Node(pXmlParentNode, "columns");
    if (pColumnsNode != nullptr)
    {
      fElement ^ const pColumnsElement = Create_HMI_Child_Element(pParentElement);
      Set_HMI_Child_Properties(pColumnsElement, pColumnsNode, "columns");
      Create_HMI_Children_Elements(pColumnsElement, pColumnsNode, "column");
    }

    System::Xml::XmlNode ^ const pValuesNode = Find_Child_Node(pXmlParentNode, "values");
    if (pValuesNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pValuesNode, "value");

    System::String ^ sTableMode = Attribute_to_String(pXmlParentNode, "tablemode");
    if (System::String::CompareOrdinal(sTableMode, "Snapshot") == 0)
    {
      System::Xml::XmlNode ^ const pFormatNode = Find_Child_Node(pXmlParentNode, "format");
      if (pFormatNode != nullptr)
      {
        fElement ^ const pFormatElement = Create_HMI_Child_Element(pParentElement);
        Set_HMI_Child_Properties(pFormatElement, pFormatNode, "format");
      }
    }
  }
  else if (System::String::CompareOrdinal(sHMI_Type, "ReferenceTable") == 0)
  {
    System::Xml::XmlNode ^ const pTitleNode = Find_Child_Node(pXmlParentNode, "title");
    if (pTitleNode != nullptr)
    {
      fElement ^ pTitleElement = Create_HMI_Child_Element(pParentElement);
      Set_HMI_Child_Properties(pTitleElement, pTitleNode, "title");
    }

    Add_VTS_Folder_Peer(pParentElement);
    Add_Popup_Peer(pParentElement, false);

    System::Xml::XmlNode ^ const pColumnsNode = Find_Child_Node(pXmlParentNode, "reftablecolumns");
    if (pColumnsNode != nullptr)
    {
      fElement ^ pColumnElement = Create_HMI_Child_Element(pParentElement);
      Set_HMI_Child_Properties(pColumnElement, pColumnsNode, "reftablecolumns");
    }

    System::Xml::XmlNode ^ const pUnitsNode = Find_Child_Node(pXmlParentNode, "reftableunits");
    if (pUnitsNode != nullptr)
    {
      fElement ^ pUnitsElement = Create_HMI_Child_Element(pParentElement);
      Set_HMI_Child_Properties(pUnitsElement, pUnitsNode, "reftableunits");
    }

    System::Xml::XmlNode ^ const pRowsNode = Find_Child_Node(pXmlParentNode, "rows");
    if (pRowsNode != nullptr)
      Create_HMI_Children_Elements(pParentElement, pRowsNode, "reftablerow");
  }
} // ................................. Parse_Page_Elements (1) ..................................

//
// Parse_Page_Elements (2)
//
void fProject::Parse_Page_Elements(fElement ^ pParentElement, System::Xml::XmlNode ^ const pXmlNode)
{
  System::String ^ sParsed_HMI_Type = nullptr;

  if (pXmlNode->Attributes != nullptr)
  {
    for each (System::Xml::XmlAttribute ^ pAttr in pXmlNode->Attributes)
    {
      if (System::String::CompareOrdinal(pAttr->Name, "type") == 0)
      {
        sParsed_HMI_Type = pAttr->Value;
        break;
      }
    }
  }

  if (System::String::CompareOrdinal(sParsed_HMI_Type, "TrendChart") == 0)
  {
    System::String ^ sName = Attribute_to_String(pXmlNode, "name");
    if (sName != nullptr)
    {
      if (System::String::CompareOrdinal(sName, "~ReportingServiceTrendTemplate~") == 0)
      {
        sParsed_HMI_Type = "REPORT_TREND_CHART_STYLE";
      }
      else
      {
        sParsed_HMI_Type = "TREND_CHART_STYLE";
      }
    }
  }

  if (!System::String::IsNullOrEmpty(sParsed_HMI_Type))
  {
    fElement ^ pChildElement = Create_HMI_Child_Element(pParentElement);
    Set_HMI_Child_Properties(pChildElement, pXmlNode, sParsed_HMI_Type);

    Parse_Page_Elements(pChildElement, pXmlNode, sParsed_HMI_Type);
  }
} // ................................. Parse_Page_Elements (2) ..................................

//
// Create_HMI_Child_Element
//
fElement ^ fProject::Create_HMI_Child_Element(fElement ^ pParentElement)
{
  _ASSERT(pParentElement != nullptr);

  fElement ^ pChildElement = gcnew fElement(allElementList->Count, pParentElement);
  allElementList->Add(pChildElement);
  fElement::AddHmiChildElement(pParentElement, pChildElement);

  return pChildElement;
} // ................................. Create_HMI_Child_Element ..................................

//
// Set_HMI_Child_Properties
//
void fProject::Set_HMI_Child_Properties(fElement ^ pChildElement, System::Xml::XmlNode ^ const pHmiNode, System::String ^ const sType)
{
  _ASSERT(pChildElement != nullptr);
  _ASSERT(pHmiNode != nullptr);
  _ASSERT(!System::String::IsNullOrEmpty(sType));

  fElement::Set_HMI_Element_Type(pChildElement, pHmiNode, sType);

  fProperty::Set_Element_Properties(pChildElement, pHmiNode);

  // Set Peer element.
  if (System::String::CompareOrdinal(pChildElement->Type->TypeID, fRT::TypeIDs::DATAPOINT) == 0 ||
    System::String::CompareOrdinal(pChildElement->Type->TypeID, fRT::TypeIDs::CHANNEL) == 0 ||
    System::String::CompareOrdinal(pChildElement->Type->TypeID, fRT::TypeIDs::TARGETVALUE) == 0)
  {
    Add_Value_Peer(pChildElement, true);
  }
  else if (System::String::CompareOrdinal(pChildElement->Type->TypeID, fRT::TypeIDs::IMAGESTATE) == 0)
  {
    IProperty ^ pImage;
    if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::IMAGE, pImage))
    {
      Add_Image_Peer(pChildElement, dynamic_cast<System::String ^>(pImage->rowValue));
    }
  }
  else if ((System::String::CompareOrdinal(pChildElement->Parent->Type->TypeID, fRT::TypeIDs::METER) == 0 &&
            System::String::CompareOrdinal(pChildElement->Type->TypeID, fRT::TypeIDs::METER) == 0) ||
            System::String::CompareOrdinal(pChildElement->Type->TypeID, fRT::TypeIDs::GAUGE) == 0)
  {
    Add_Multiple_Peers(pChildElement);
  }
  else if (System::String::CompareOrdinal(pChildElement->Parent->Type->TypeID, fRT::TypeIDs::TABLE) == 0)
  {
    if (System::String::CompareOrdinal(pChildElement->Type->TypeID, fRT::TypeIDs::VALUE) == 0)
    {
      IProperty ^ pHMIID;
      if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::HMIID, pHMIID))
        Add_VTS_Folder_Peer(pChildElement);
      else
        Add_Value_Peer(pChildElement, false);
    }
  }
  else if (System::String::CompareOrdinal(pChildElement->Parent->Type->TypeID, fRT::TypeIDs::COLUMNS) == 0)
  {
    if (System::String::CompareOrdinal(pChildElement->Parent->Parent->Type->TypeID, fRT::TypeIDs::TABLE) == 0)
    {
      IProperty ^ pHMIID;
      if (pChildElement->Properties->TryGetValue(fRT::TypeIDs::HMIID, pHMIID))
        Add_VTS_Folder_Peer(pChildElement);
      else
        Add_Value_Peer(pChildElement, false);
    }
  }
} // ................................. Set_HMI_Child_Properties ..................................

//
// Create_HMI_Children_Elements
//
void fProject::Create_HMI_Children_Elements(fElement ^ pParentElement, System::Xml::XmlNode ^ const pXmlHmiNode, System::String ^ const sType)
{
  _ASSERT(pParentElement != nullptr);
  _ASSERT(!System::String::IsNullOrEmpty(sType));

  if (pXmlHmiNode != nullptr && pXmlHmiNode->HasChildNodes)
  {
    for each (System::Xml::XmlNode ^ pHmiNode in pXmlHmiNode->ChildNodes)
    {
      if (pHmiNode->NodeType == System::Xml::XmlNodeType::Comment)
        continue;

      fElement ^ pChildElement = Create_HMI_Child_Element(pParentElement);

      Set_HMI_Child_Properties(pChildElement, pHmiNode, sType);
    }
  }
} // ................................. Create_HMI_Children_Elements ..................................

//
// Parse_HMI_Items
//
void fProject::Parse_HMI_Items(aWEB::C_Project_Node_Web_Server ^ pWebServer)
{
  if (pWebServer == nullptr)
  {
    System::String ^ sError = ">>>>> This project has no web analytics server.";
    aFW::Log::WriteRecord(sError);
    throw gcnew System::Exception(sError);
  }

  System::Xml::XmlDocument ^ pDoc;

  try
  {
    pDoc = aWEB::Web_Export_HMI_Document(pWebServer, true, % oAssociationErrors);

    int errorCount = oAssociationErrors.Count;

    if (errorCount > 0)
    {
      int tenErrors = 10;
      aFW::Log::WriteRecord(System::String::Format(">>>>> {0} HMI Parse Errors !!!!!", errorCount));
      if (errorCount > 10)
        aFW::Log::WriteRecord(">>>>> Listing first ten errors.");
      for each (System::String ^ sErr in oAssociationErrors)
      {
        aFW::Log::WriteRecord("      " + sErr);
        if (--tenErrors == 0)
          break;
      }
    }

#ifdef _DEBUG
    //pDoc->Save("c:\\ADCS\\projects\\vts\\IProject_Testing.xml");
#endif

    System::Xml::XmlNode ^ const pXmlBody = Find_Child_Node(pDoc, "web_project");
    //System::Xml::XmlNode ^ const pXmlAnnot = Find_Child_Node(pXmlBody, "annotationtypes");
    //System::Xml::XmlNode ^ const pXmlSecurity = Find_Child_Node(pXmlBody, "security");

    // This selection puts the Xml nodes for all pages and sub-pages into a flat list.
    System::Xml::XmlNodeList ^ const pXmlPageList = pDoc->DocumentElement->SelectNodes("descendant::page");

    // Find project root element, web server element and all web page elements.
    fElement ^ pProjectElement = nullptr;
    fElement ^ pWebServerElement = nullptr;
    System::Collections::Generic::List<fElement ^> ^ pAllWebPages = gcnew System::Collections::Generic::List<fElement ^>();
    for each (fElement ^ pElement in allElementList)
    {
      if (pElement->Type == nullptr)
      {
        System::String ^ sMessage = System::String::Format("Element \"{0}\" has unknown type.", pElement->Name);
        throw gcnew System::Exception(sMessage);
      }
      if (System::String::CompareOrdinal(pElement->Type->TypeID, fRT::TypeIDs::ROOT) == 0)
      {
        pProjectElement = pElement;
        continue;
      }
      if (System::String::CompareOrdinal(pElement->Type->TypeID, fRT::TypeIDs::WEBSERVER) == 0)
      {
        pWebServerElement = pElement;
        continue;
      }
      if (System::String::CompareOrdinal(pElement->Type->TypeID, fRT::TypeIDs::PAGE) == 0)
      {
        pAllWebPages->Add(pElement);
      }
    }

    if (pProjectElement == nullptr)
    {
      // throw exception???
      return;
    }

    // Create list of web picture descriptors for later mapping to web picture elements.
    System::Xml::XmlNode ^ const pXmlImages = Find_Child_Node(pXmlBody, "images");
    if (pXmlImages != nullptr && pXmlImages->HasChildNodes)
      for each (System::Xml::XmlNode ^ const pXmlImage in pXmlImages->ChildNodes)
      {
        C_Image_Descriptor ^ pDescriptor = gcnew C_Image_Descriptor();

        pDescriptor->sImageXmlName = Attribute_to_String(pXmlImage, "name");

        int iValue;
        bool bParsedOK = int::TryParse(Attribute_to_String(pXmlImage, "id"), iValue);
        pDescriptor->iHmi_ID = bParsedOK ? iValue : 0;

        pImageElements->Add(pDescriptor->sImageXmlName, pDescriptor);
      }

    System::Xml::XmlNode ^ const pXmlFonts = Find_Child_Node(pXmlBody, "fonts");
    Create_HMI_Children_Elements(pProjectElement, pXmlFonts, "font");

    System::Xml::XmlNode ^ const pXmlBrushes = Find_Child_Node(pXmlBody, "brushes");
    Create_HMI_Children_Elements(pProjectElement, pXmlBrushes, "brush");

    System::Xml::XmlNode ^ const pXmlFlags = Find_Child_Node(pXmlBody, "dynamictextflags");
    Create_HMI_Children_Elements(pProjectElement, pXmlFlags, "flag");

    System::Xml::XmlNode ^ const pXmlUnits = Find_Child_Node(pXmlBody, "units");
    Create_HMI_Children_Elements(pProjectElement, pXmlUnits, "EU");

    System::Xml::XmlNode ^ const pXmlDatasources = Find_Child_Node(pXmlBody, "datasources");
    Create_HMI_Children_Elements(pProjectElement, pXmlDatasources, "datasource");

    // Web templates.
    System::Xml::XmlNode ^ const pXmlTemplates = Find_Child_Node(pXmlBody, "objecttemplates");
    if (pXmlTemplates != nullptr  && pXmlTemplates->HasChildNodes)
      for each (System::Xml::XmlNode ^ const pXmlTemplate in pXmlTemplates->ChildNodes)
      {
        Parse_Page_Elements(pProjectElement, pXmlTemplate);
      }

    // Handle web pages.
    // We are matching elements with xml nodes in order to parse HMI children.
    // At the "pages" level, the only xml child nodes are "objects", "pagestates", and recursively "pages" (sub-pages).
    // Web page elements and their parent-child relationships will have been created during the initial parsing of project nodes.
    // Therefore, the flat xml node list allows us to avoid a recursive call for the "pages" child node.
    if (pXmlPageList != nullptr)
      for each (System::Xml::XmlNode ^ const pXmlPage in pXmlPageList)
      {
        System::String ^ sXmlNodePageName = Attribute_to_String(pXmlPage, "name");
        if (System::String::IsNullOrEmpty(sXmlNodePageName))
        {
          continue;
        }

        for each (fElement ^ const pWebPageElement in pAllWebPages)
        {
          System::String ^ sPageName = pWebPageElement->Name;

          if (System::String::CompareOrdinal(sXmlNodePageName, sPageName) != 0)
          {
            continue;
          }

          fProperty::Set_Element_Properties(pWebPageElement, pXmlPage);

          if (pXmlPage->HasChildNodes)
          {
            System::Xml::XmlNode ^ const pXmlPageObjects = Find_Child_Node(pXmlPage, "objects");

            if (pXmlPageObjects != nullptr && pXmlPageObjects->HasChildNodes)
            {
              for each (System::Xml::XmlNode ^ const pXmlPageObject in pXmlPageObjects->ChildNodes)
              {
                Parse_Page_Elements(pWebPageElement, pXmlPageObject);
              }
            } // if (pXmlPageObjects != nullptr && pXmlPageObjects->HasChildNodes)

            System::Xml::XmlNode ^ const pXmlPageStates = Find_Child_Node(pXmlPage, "pagestates");
            if (pXmlPageStates != nullptr)
              Create_HMI_Children_Elements(pWebPageElement, pXmlPageStates, "pagestate");
          }
        } // for each (fElement ^ pWebPageElement in pAllWebPages)
      } // for each (System::Xml::XmlNode ^ const pXmlPage in pXmlPageList)
  }
  //catch (System::Exception ^ pErr)
  //{
  //  // Using the handler in Open() which reports errors through the ResponseStatus object and calls InjectErrorPage().
  //}
  finally
  {
    delete pDoc;
  }

} // ................................. Parse_HMI_Items ..................................

//
// InjectErrorPage
//
void fProject::InjectErrorPage(System::String ^ sError)
{
  if (allElementList == nullptr || allElementList->Count == 0)
    return;

  // Find the web server element and the first web page element.
  //fElement ^ pWebServerElement = nullptr;
  fElement ^ pErrorPageElement = nullptr;
  for each (fElement ^ pElement in allElementList)
  {
    if (pElement->Type == nullptr)
      continue;

    if (System::String::CompareOrdinal(pElement->Type->TypeID, fRT::TypeIDs::PAGE) == 0)
    {
      pErrorPageElement = pElement;
      //pWebServerElement = dynamic_cast<fElement ^>(pElement->Parent);
      break;
    }
  }

  if (/* pWebServerElement != nullptr && */ pErrorPageElement != nullptr)
  {
    //// Remove existing page elements from the web server, and add back the error page.
    //System::Collections::Generic::List<int> ^ pNewChildren = gcnew System::Collections::Generic::List<int>();
    //pNewChildren->Add(pErrorPageElement->Id);
    //pWebServerElement->Children = pNewChildren->ToArray();

    // Remove existing HMI elements from the error page.
    System::Collections::Generic::List<int> ^ pNewChildren = gcnew System::Collections::Generic::List<int>();
    pErrorPageElement->Children = pNewChildren->ToArray();

    System::Xml::XmlDocument ^ pDoc;
    try
    {
      pDoc = gcnew System::Xml::XmlDocument();
      pDoc->LoadXml("<object />");

      System::Xml::XmlNode ^ const pXmlPageObject = Find_Child_Node(pDoc, "object");
      Add_Attribute(pXmlPageObject, "location")->Value = "{X = 40, Y = 40}";
      Add_Attribute(pXmlPageObject, "size")->Value = "{Width = 400, Height = 400}";
      Add_Attribute(pXmlPageObject, "type")->Value = "StaticText";
      Add_Attribute(pXmlPageObject, "bordersize")->Value = "0";
      Add_Attribute(pXmlPageObject, "text")->Value = sError;

      Parse_Page_Elements(pErrorPageElement, pXmlPageObject);
    }
    finally
    {
      delete pDoc;
    }
  }
} // ................................. InjectErrorPage ..................................
