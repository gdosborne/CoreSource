#include "fElement.h"
#include "fElementType.h"
#include "fProperty.h"
#include "fWebChannel.h"
#include "fProjectEUFactory.h"
#include "..\..\common\Config\Config_Set_Project.h"
#include "..\..\common\Config\Config_Set_Application.h"
#include "..\..\common\Config\Config_Set_EU.h"
#include "..\..\common\Config\Config_Set_Signals_OPC.h"
#include "..\..\common\Config\Config_Set_Computer.h"
#include "..\..\common\Config\Revisions_List.h"
#include "..\..\common\VTS\Config_Set_Signals_VTS.h"
#include "..\..\common\Oil\Config_Set_Rod_Pump.h"
#include "..\..\common\DB\Config_Set_DB.h"
#include "..\..\common\Utils_Array.h"
#include "..\..\common\Utils_Delegate.h"
#include "..\..\common\Utils_Net_XML.h"
#include "..\..\common\VTS\Config_Set_Signals_VTS.h"
#include "..\..\common\Web\Web_HMI_I.h"
#include "..\..\common\web\Config_Set_Web.h"
#include "..\..\common\web\Config_Set_Web_Page.h"
#include "..\..\common\web\Config_Set_Wiki.h"

#include <crtdbg.h>

// .Net namespaces
using namespace System;
using namespace System::Collections::Generic;

// Custom namespaces
using namespace aCONF;
using namespace aDB;
using namespace aWEB;
using namespace SNC::OptiRamp::Services;
using namespace SNC::OptiRamp::Services::fWeb;

// forward declared functions
static void Fill_Basic_Properties(C_Project_Node^ pSource_Node, fElement::C_Element_Properties^ pTarget_Properties, bool% bHandled)
{
  _ASSERT(!bHandled); // we pray that MultiDelegate preserves calling order of added members
  _ASSERT(pTarget_Properties != nullptr);

  { // Note
    C_Project_Node_Note ^ const pNode_Note = dynamic_cast<C_Project_Node_Note ^>(pSource_Node);
    if (pNode_Note != nullptr)
    {
      pTarget_Properties->Add(fRT::TypeIDs::NOTE, gcnew fPropertyString(fRT::TypeIDs::NOTE, pNode_Note->Prop_Note));
    }
    // Do not return. This property belongs to a base, project node class, so several derived project nodes have it, too.
  } // Note....

  { // Root
    C_Project_Node_Root ^ const pNode_Root = dynamic_cast<C_Project_Node_Root ^>(pSource_Node);
    if (pNode_Root != nullptr)
    {
      aCONF::C_Revisions_List ^ pProjectRevisions = %(pNode_Root->oRevList);
      if (pProjectRevisions != nullptr && pProjectRevisions->Get_Current_Revision_Idx() == 0)
      {
        aCONF::C_Revisions_List::C_Revision oRevision = pProjectRevisions->Revisions[0];

        pTarget_Properties->Add(fRT::TypeIDs::REVISION, gcnew fPropertyString(fRT::TypeIDs::REVISION, oRevision.Get_Revision()));
        pTarget_Properties->Add(fRT::TypeIDs::REVISIONMAJORNUMBER, gcnew fPropertyString(fRT::TypeIDs::REVISIONMAJORNUMBER, oRevision.iMajor.ToString()));
        pTarget_Properties->Add(fRT::TypeIDs::REVSIONMINORNUMBER, gcnew fPropertyString(fRT::TypeIDs::REVSIONMINORNUMBER, oRevision.iMinor.ToString()));
        pTarget_Properties->Add(fRT::TypeIDs::REVISIONDATE, gcnew fPropertyString(fRT::TypeIDs::REVISIONDATE, oRevision.tDate.ToString("MM/dd/yyyy HH:mm:ss")));
        pTarget_Properties->Add(fRT::TypeIDs::REVISIONUSER, gcnew fPropertyString(fRT::TypeIDs::REVISIONUSER, oRevision.sUser));
        pTarget_Properties->Add(fRT::TypeIDs::REVISIONDESCRIPTION, gcnew fPropertyString(fRT::TypeIDs::REVISIONDESCRIPTION, oRevision.sDescription));
      }
      else
      {
        pTarget_Properties->Add(fRT::TypeIDs::REVISION, gcnew fPropertyString(fRT::TypeIDs::REVISION, System::String::Empty));
        pTarget_Properties->Add(fRT::TypeIDs::REVISIONMAJORNUMBER, gcnew fPropertyString(fRT::TypeIDs::REVISIONMAJORNUMBER, System::String::Empty));
        pTarget_Properties->Add(fRT::TypeIDs::REVSIONMINORNUMBER, gcnew fPropertyString(fRT::TypeIDs::REVSIONMINORNUMBER, System::String::Empty));
        pTarget_Properties->Add(fRT::TypeIDs::REVISIONDATE, gcnew fPropertyString(fRT::TypeIDs::REVISIONDATE, System::String::Empty));
        pTarget_Properties->Add(fRT::TypeIDs::REVISIONUSER, gcnew fPropertyString(fRT::TypeIDs::REVISIONUSER, System::String::Empty));
        pTarget_Properties->Add(fRT::TypeIDs::REVISIONDESCRIPTION, gcnew fPropertyString(fRT::TypeIDs::REVISIONDESCRIPTION, System::String::Empty));
      }

      pTarget_Properties->Add(fRT::TypeIDs::NAME, gcnew fPropertyString(fRT::TypeIDs::NAME, pNode_Root->Get_UI_Name(0)));
      pTarget_Properties->Add(fRT::TypeIDs::PROJECTNOTES, gcnew fPropertyString(fRT::TypeIDs::PROJECTNOTES, pNode_Root->Prop_ProjectNotes));
      for (int I = 1; I < C_Project_Node_Root::vProperties->Length; ++I)
      {
        String ^ sProp = C_Project_Node_Root::vProperties[I];

        pTarget_Properties->Add(sProp,
          gcnew fPropertyString(fElementType::XlatTypeID(C_Project_Node_Root::vPropNames[I]),
          pNode_Root,
          sProp));
      }
      pTarget_Properties->Add(fRT::TypeIDs::WINDOWLIMIT, gcnew fPropertyString(fRT::TypeIDs::WINDOWLIMIT, pNode_Root->sWindowLimit));

      bHandled = true;
      return;
    } // if ( pNode_Root != nullptr ) ......
  } // Root....

  { // Computer
    aCONF::C_Project_Node_Computer ^ const pComputer = dynamic_cast<aCONF::C_Project_Node_Computer ^>(pSource_Node);
    if (pComputer != nullptr)
    {
      pTarget_Properties->Add(fRT::TypeIDs::NETWORKADDRESS, gcnew fPropertyString(fRT::TypeIDs::NETWORKADDRESS, pComputer->sNetworkAddress));
      pTarget_Properties->Add(fRT::TypeIDs::PROJECTPATH, gcnew fPropertyString(fRT::TypeIDs::PROJECTPATH, pComputer->sProjectPath));
      bHandled = true;
      return;
    }
  } // Computer ...

  { // VTS Server
    aVTS::C_Project_Node_VTS_Server ^ const pVtsServer = dynamic_cast<aVTS::C_Project_Node_VTS_Server ^>(pSource_Node);
    if (pVtsServer != nullptr)
    {
      pTarget_Properties->Add(fRT::TypeIDs::PORT, gcnew fPropertyInt(fRT::TypeIDs::PORT, pVtsServer->iPort));
      pTarget_Properties->Add(fRT::TypeIDs::CONTROLLERPORT, gcnew fPropertyInt(fRT::TypeIDs::CONTROLLERPORT, pVtsServer->iPortWebController));
      pTarget_Properties->Add(fRT::TypeIDs::LASTIMPORT, gcnew fPropertyString(fRT::TypeIDs::LASTIMPORT, pVtsServer->sLast_Import_Source_File));
      bHandled = true;
      return;
    }
  } // VTS Server ...

  { // OptiRamp OPC Server
    aCONF::C_Project_Node_OptiRamp_OPC_Server ^ const pOptiRampOpcServer = dynamic_cast<aCONF::C_Project_Node_OptiRamp_OPC_Server ^>(pSource_Node);
    if (pOptiRampOpcServer != nullptr)
    {
      pTarget_Properties->Add(fRT::TypeIDs::PORT, gcnew fPropertyInt(fRT::TypeIDs::PORT, pOptiRampOpcServer->iPort));
      bHandled = true;
      return;
    }
  } // OptiRamp OPC Server ...

  { // OPC Server
    aCONF::C_Project_Node_OPC_Server ^ const pOpcServer = dynamic_cast<aCONF::C_Project_Node_OPC_Server ^>(pSource_Node);
    if (pOpcServer != nullptr)
    {
      pTarget_Properties->Add("OpcName", gcnew fPropertyString("OpcName", pOpcServer->sOPC_Name));
      bHandled = true;
      return;
    }
  } // OPC Server ...

  { // Wiki Server
    aWIKI::C_Project_Node_Wiki ^ const pWikiServer = dynamic_cast<aWIKI::C_Project_Node_Wiki ^>(pSource_Node);
    if (pWikiServer != nullptr)
    {
      pTarget_Properties->Add("WikiUrlSuffix", gcnew fPropertyString("WikiUrlSuffix", pWikiServer->sUrl));

      bHandled = true;
      return;
    }
  } // Wiki Server...

  { // Database
    C_Project_Node_Database ^ const pDatabase = dynamic_cast<C_Project_Node_Database ^>(pSource_Node);
    if (pDatabase != nullptr)
    {
      pTarget_Properties->Add("DATABASEPATH", gcnew fPropertyString("DATABASEPATH", pDatabase->sDatabasePath));

      bHandled = true;
      return;
    }
  } // Database...

  { // Web Configuration Server
    C_Project_Node_Web_Config ^ const pWebConfig = dynamic_cast<C_Project_Node_Web_Config ^>(pSource_Node);
    if (pWebConfig != nullptr)
    {
      pTarget_Properties->Add(fRT::TypeIDs::NAME, gcnew fPropertyString(fRT::TypeIDs::NAME, pWebConfig->pName));
      pTarget_Properties->Add(fRT::TypeIDs::WEBAPPLICATIONNAME, gcnew fPropertyString(fRT::TypeIDs::WEBAPPLICATIONNAME, pWebConfig->Prop_WebAppName));

      bHandled = true;
      return;
    }
  } // Web Configuration Server ...

  { // Web Analytic Server
    aWEB::C_Project_Node_Web_Server ^ const pWebServer = dynamic_cast<aWEB::C_Project_Node_Web_Server ^>(pSource_Node);
    if (pWebServer != nullptr)
    {
      pTarget_Properties->Add(fRT::TypeIDs::DEFAULTDATASOURCE, gcnew fPropertyString(fRT::TypeIDs::DEFAULTDATASOURCE, pWebServer->Prop_DefaultRuntime));
      pTarget_Properties->Add(fRT::TypeIDs::DYNAMICTEXTDATASOURCE, gcnew fPropertyString(fRT::TypeIDs::DYNAMICTEXTDATASOURCE, pWebServer->Prop_DynamicTextRuntime));
      pTarget_Properties->Add(fRT::TypeIDs::DYNAMICTEXTNOTDEFINEDCOLOR, gcnew fPropertyString(fRT::TypeIDs::DYNAMICTEXTNOTDEFINEDCOLOR, pWebServer->sNotDefinedColor));
      pTarget_Properties->Add(fRT::TypeIDs::DYNAMICTEXTSIMULATEDCOLOR, gcnew fPropertyString(fRT::TypeIDs::DYNAMICTEXTSIMULATEDCOLOR, pWebServer->sSimulatedColor));
      pTarget_Properties->Add(fRT::TypeIDs::DYNAMICTEXTFIELDCOLOR, gcnew fPropertyString(fRT::TypeIDs::DYNAMICTEXTFIELDCOLOR, pWebServer->sFieldColor));
      pTarget_Properties->Add(fRT::TypeIDs::DYNAMICTEXTALARMEDCOLOR, gcnew fPropertyString(fRT::TypeIDs::DYNAMICTEXTALARMEDCOLOR, pWebServer->sAlarmedColor));
      for each(aWEB::C_Project_Node_Web_Server::C_AnnotationType ^ pAnn in pWebServer->pAnnotations)
      {
        System::String ^ sName = "annot_" + pAnn->sName;
        System::String ^ sValue = "annot_" + pAnn->sName + "_Value";
        System::String ^ sBrush = "annot_" + pAnn->sName + "_Brush";
        pTarget_Properties->Add(sName, gcnew fPropertyString(sName, pAnn->sName));
        pTarget_Properties->Add(sValue, gcnew fPropertyString(sValue, pAnn->sValue));
        pTarget_Properties->Add(sBrush, gcnew fPropertyString(sBrush, pAnn->sBrushColor));
      }

      // Logo Image
      if (pWebServer->Has_Peers())
      {
        // There should be only one logo connection to the web server. If there is more than 1, only the first will be used.
        aCONF::C_Project_Connection_Peer ^ pConnection = pWebServer->pPeers[0];
        if (pConnection != nullptr)
        {
          aWEB::C_Project_Node_Picture ^ const pLogo = dynamic_cast<aWEB::C_Project_Node_Picture ^>(pConnection->pPeerNode);
          if (pLogo != nullptr)
          {
            pTarget_Properties->Add(fRT::TypeIDs::LOGO, gcnew fPropertyBitmap(fRT::TypeIDs::LOGO, pLogo->pPicture));
          }
        }
      }

      bHandled = true;
      return;
    } // if (pWebServer != nullptr)
  } // Web Analytic Server...

  { // Web Page
    aWEB::C_Project_Node_Web_Page ^ const pWebPage = dynamic_cast<aWEB::C_Project_Node_Web_Page ^>(pSource_Node);
    if (pWebPage != nullptr)
    {
      int iValue;
      bool bResult = int::TryParse(pWebPage->HMI_Height, iValue);
      int iHeight = bResult ? iValue : 0;
      pTarget_Properties->Add(fRT::TypeIDs::HEIGHT, gcnew fPropertyInt(fRT::TypeIDs::HEIGHT, iHeight));

      bResult = int::TryParse(pWebPage->HMI_Width, iValue);
      int iWidth = bResult ? iValue : 0;
      pTarget_Properties->Add(fRT::TypeIDs::WIDTH, gcnew fPropertyInt(fRT::TypeIDs::WIDTH, iWidth));

      pTarget_Properties->Add(fRT::TypeIDs::PAGESTATE, gcnew fPropertyString(fRT::TypeIDs::PAGESTATE, pWebPage->bPageStateChecked.ToString()));

      bHandled = true;
      return;
    }
  } // Web Page...

  { // Engineering Units
    aCONF::C_Project_Node_EU ^ const pUnits = dynamic_cast<aCONF::C_Project_Node_EU ^>(pSource_Node);
    if (pUnits != nullptr)
    {
      //if (pUnits->EU_Lines != nullptr)
      //{
      //  for each (aEU::C_EU_Line ^ pEuLine in pUnits->EU_Lines)
      //  {
      //    System::String ^ sName0 = pEuLine->GetName();
      //    System::String ^ sName1 = pEuLine->Span.ToString();
      //    System::String ^ sName2 = pEuLine->Offset.ToString();
      //    System::String ^ sName3 = pEuLine->oDigitsAfterPoint.ToString();
      //    System::String ^ sName4 = string_to_String(pEuLine->pConverter->Get_CEU());
      //  }
      //}

      bHandled = true;
      return;
    }
  } // Engineering Units ...

  { // Image Collection
    aWEB::C_Project_Node_Pictures ^ const pImages = dynamic_cast<aWEB::C_Project_Node_Pictures ^>(pSource_Node);
    if (pImages != nullptr)
    {
      pTarget_Properties->Add(fRT::TypeIDs::NAME, gcnew fPropertyString(fRT::TypeIDs::NAME, pImages->Get_UI_Name()));
      bHandled = true;
      return;
    }
  } // Image Collection ...

  { // Image
    aWEB::C_Project_Node_Picture ^ const pImage = dynamic_cast<aWEB::C_Project_Node_Picture ^>(pSource_Node);
    if (pImage != nullptr)
    {
      pTarget_Properties->Add(fRT::TypeIDs::ID, gcnew fPropertyInt(fRT::TypeIDs::ID, pImage->Get_HMI_ID()));
      pTarget_Properties->Add(fRT::TypeIDs::NAME, gcnew fPropertyString(fRT::TypeIDs::NAME, pImage->Get_UI_Name()));

      if (pImage->sPictureStr == nullptr)
      {
        pTarget_Properties->Add(fRT::TypeIDs::BITMAP, gcnew fPropertyBitmap(fRT::TypeIDs::BITMAP, pImage->pPicture));
        pTarget_Properties->Add(fRT::TypeIDs::SVG, gcnew fPropertyString(fRT::TypeIDs::SVG, nullptr));
      }
      else
      {
        pTarget_Properties->Add(fRT::TypeIDs::BITMAP, gcnew fPropertyBitmap(fRT::TypeIDs::BITMAP, nullptr));
        pTarget_Properties->Add(fRT::TypeIDs::SVG, gcnew fPropertyString(fRT::TypeIDs::SVG, pImage->sPictureStr));
      }

      bHandled = true;
      return;
    }
  } // Image ...

  { // Web Library
    aWEB::C_Project_Node_Web_Library ^ const pLibrary = dynamic_cast<aWEB::C_Project_Node_Web_Library ^>(pSource_Node);
    if (pLibrary != nullptr)
    {
      int iValue;
      bool bResult = int::TryParse(pLibrary->HMI_Height, iValue);
      int iHeight = bResult ? iValue : 0;
      pTarget_Properties->Add(fRT::TypeIDs::HEIGHT, gcnew fPropertyInt(fRT::TypeIDs::HEIGHT, iHeight));

      bResult = int::TryParse(pLibrary->HMI_Width, iValue);
      int iWidth = bResult ? iValue : 0;
      pTarget_Properties->Add(fRT::TypeIDs::WIDTH, gcnew fPropertyInt(fRT::TypeIDs::WIDTH, iWidth));
      bHandled = true;
      return;
    }
  } // Web Library ...

  { // Library Channel
    aWEB::C_Project_Node_Lib_Channel ^ const pLibChannel = dynamic_cast<aWEB::C_Project_Node_Lib_Channel ^>(pSource_Node);
    if (pLibChannel != nullptr)
    {
      bHandled = true;
      return;
    }
  } // Library Channel ...

  { // VTS Folder
    aVTS::C_Project_Node_VTS_Folder ^ const pVtsFolder = dynamic_cast<aVTS::C_Project_Node_VTS_Folder ^>(pSource_Node);
    if (pVtsFolder != nullptr)
    {
      System::String ^ sUID = System::String::IsNullOrEmpty(pVtsFolder->sUID) ? System::String::Empty : pVtsFolder->sUID;
      pTarget_Properties->Add(fRT::TypeIDs::UID, gcnew fPropertyString(fRT::TypeIDs::UID, sUID));
      pTarget_Properties->Add(fRT::TypeIDs::HMIID, gcnew fPropertyInt(fRT::TypeIDs::HMIID, pVtsFolder->Get_HMI_ID()));
      pTarget_Properties->Add(fRT::TypeIDs::VTSFOLDERFULLNAME, gcnew fPropertyString(fRT::TypeIDs::VTSFOLDERFULLNAME, pVtsFolder->Full_Prefix_No_Trailing_Dot()));

      bHandled = true;
      return;
    }
  } // VTS Folder ...

  { // VTS Tag
    aVTS::C_Project_Node_VTS_Signal ^ const pVtsSignal = dynamic_cast<aVTS::C_Project_Node_VTS_Signal ^>(pSource_Node);
    if (pVtsSignal != nullptr)
    {
      System::String ^ sUID = System::String::IsNullOrEmpty(pVtsSignal->sUID) ? System::String::Empty : pVtsSignal->sUID;
      pTarget_Properties->Add(fRT::TypeIDs::UID, gcnew fPropertyString(fRT::TypeIDs::UID, sUID));

      System::String ^ sEU = System::String::IsNullOrEmpty(pVtsSignal->sEU) ? System::String::Empty : pVtsSignal->sEU;
      pTarget_Properties->Add(fRT::TypeIDs::EU, gcnew fPropertyString(fRT::TypeIDs::EU, sEU));

      pTarget_Properties->Add(fRT::TypeIDs::VTSTAGFULLNAME, gcnew fPropertyString(fRT::TypeIDs::VTSTAGFULLNAME, pVtsSignal->Full_Tag()));

      bHandled = true;
      return;
    }
  } // VTS Tag ...

  { // Rod Pump
    aOIL::C_Project_Node_Rod_Pump ^ const pRodPump = dynamic_cast<aOIL::C_Project_Node_Rod_Pump ^>(pSource_Node);
    if (pRodPump != nullptr)
    {
      pTarget_Properties->Add("ReplacePumpName", gcnew fPropertyString("ReplacePumpName", pRodPump->bReplacePumpName.ToString()));
      pTarget_Properties->Add("UseLibraryPattern", gcnew fPropertyString("UseLibraryPattern", pRodPump->bUseLibraryPattern.ToString()));
      //pTarget_Properties->Add("UseAltName", gcnew fPropertyString("UseAltName", pRodPump->bUseAltName.ToString()));
      pTarget_Properties->Add("AltName", gcnew fPropertyString("AltName", pRodPump->sAltName));
      pTarget_Properties->Add("LibraryPattern", gcnew fPropertyString("LibraryPattern", pRodPump->sLibraryPattern));

      bHandled = true;
      return;
    }
  } // Rod Pump...

}

static fElement::fElement()
{
  dlg_Fill_Properties += gcnew DLG_Fill_Properties(Fill_Basic_Properties);
}

fElement::fElement(int iNodeID_, C_Project_Node ^ pNode_, C_Node_Element  ^ pNode_Element_)
  : iNodeID(iNodeID_), 
    pNode(pNode_),
    pNode_Element(pNode_Element_),
    iElementID(iNodeID_),
    pParentElement(nullptr)
{
  _ASSERT(pNode != nullptr);
  _ASSERT(!pNode_Element->ContainsKey(pNode));

  pNode_Element->Add(pNode, this);
  sName = pNode->Get_UI_Name();
  sType = fElementType::XlatTypeID(pNode->pType->sTag);
}

fElement::fElement(int iElementID_, IElement ^ pWebPageElement)
  : iElementID(iElementID_), pParentElement(pWebPageElement),
    iNodeID(iElementID_),
    pNode(nullptr),
    pNode_Element(nullptr)
{
  pProperties = gcnew C_Element_Properties;
}

array<IWebChannel ^> ^ fElement::WebChannels::get()
{
  if (pWebChannels == nullptr)
  {
    array<C_Web_Channel> ^ pChannels;
    if (pNode != nullptr)
      pChannels = Find_Web_Channels_Safe(this->pNode);

    const int iCount = Safe_Length(pChannels);

    if (iCount == 0) 
    {
      pWebChannels = fWebChannel::oEmpty;
    } else {
      array<IWebChannel ^> ^ pBuf = gcnew array<IWebChannel ^>( iCount );

      if ( iCount == 0 )
      {
        return fWebChannel::oEmpty;
      }

      for ( int I = 0; I < iCount; ++I )
      {
        pBuf[ I ] = gcnew fWebChannel( this->pNode, pChannels, I );
      }

      pWebChannels = pBuf;
    }
  }

  return pWebChannels;
}


int fElement::Id::get()
{
  return iNodeID;
}

String ^ fElement::Name::get()
{
  return sName;
}

String ^ fElement::Description::get()
{
  return nullptr;
}

void fElement::Description::set(String ^ sDescription_)
{
  throw gcnew NotImplementedException;
}

String ^ fElement::Tag::get()
{
  throw gcnew NotImplementedException;
  //return pNode->pType->sTag; 
}

IElemType ^ fElement::Type::get()
{
  for each (fElementType ^ pElementType in fElementType::oElementTypes)
  {
    if (System::String::CompareOrdinal(pElementType->TypeID, sType) == 0)
      return pElementType;
  }
  return nullptr;
}

IDictionary<String^, IProperty^> ^ fElement::Properties::get()
{
  if (pProperties == nullptr && pNode != nullptr)
  {
    C_Element_Properties ^ pBuf = gcnew C_Element_Properties;

    if (pNode != nullptr && !Delegate_Is_Empty(dlg_Fill_Properties))
    {
      bool bIsHandled = false;
      dlg_Fill_Properties(pNode, pBuf, bIsHandled);
    }

    pProperties = pBuf;
  }

  return pProperties;
}

void fElement::Properties::set(IDictionary<String^, IProperty^> ^ pProperties_)
{
  throw gcnew NotImplementedException;
}

IElement ^ fElement::Parent::get()
{
  if (pNode != nullptr && pNode->pParent != nullptr)
  {
    fElement ^ pElement;
    if (pNode_Element->TryGetValue(pNode->pParent, pElement))
      pParentElement = pElement;
    else
      pParentElement = nullptr;
  }

  return pParentElement;
}

void fElement::Parent::set(IElement ^ pParent_)
{
  throw gcnew NotImplementedException;
}

IEnumerable<int> ^ fElement::Children::get()
{
  if (pChildren == nullptr)
  {
    // this pBuf trick makes call of Children::get() thread safe
    List<int> ^ pBuf = gcnew List<int>;

    if (pNode != nullptr && pNode->Has_Kids())
    {
      for each (C_Project_Node ^ pIter in pNode->pKids)
      {
        fElement ^ pElement;
        if (pNode_Element->TryGetValue(pIter, pElement))
          pBuf->Add(pElement->Id);
      }
    }

    pChildren = pBuf;
  }

  return pChildren;
}

void fElement::Children::set(IEnumerable<int> ^ pChildren_)
{
  if (pChildren != nullptr)
  {
    pChildren = pChildren_;
  }
  else
  {
    // this pBuf trick makes call of Children::get() thread safe
    List<int> ^ pBuf = gcnew List<int>;

    if (pNode != nullptr && pNode->Has_Kids())
    {
      for each (C_Project_Node ^ pIter in pNode->pKids)
      {
        fElement ^ pElement;
        if (pNode_Element->TryGetValue(pIter, pElement))
          pBuf->Add(pElement->Id);
      }
    }

    pChildren = pBuf;
  }
}

IEnumerable<Peer> ^ fElement::Peers::get()
{
  if (pPeers == nullptr)
  {
    List<Peer> ^ pBuf = gcnew List<Peer>;

    if (pNode != nullptr && pNode->Has_Peers())
    {
      for each (C_Project_Connection_Peer ^ pIter in pNode->pPeers)
      {
        if (dynamic_cast<C_Project_Node_Unknown ^>(pIter->pPeerNode) != nullptr)
          continue;

        Peer tBuf;
        tBuf.elemID = pNode_Element[pIter->pPeerNode]->Id;
        tBuf.peerID = pIter->pTag;
        pBuf->Add( tBuf );
      }
      pPeers = pBuf;
    } else {
      pPeers = oEmptyPeers;
    }

  }

  return pPeers;
}

void fElement::Peers::set(IEnumerable<Peer> ^ pPeers_)
{
  pPeers = pPeers_ == nullptr ? oEmptyPeers : pPeers_;
}

//
// fIOChannel
//
private ref struct fIOChannel sealed : IIOChannel
{
  aCONF::C_Project_Node_App::C_Parameter  ^ const pPar;
  const IOType                                    tType;
  //

  // ------------------ IDataChannel ------------------
  property System::String ^ Description
  {
    virtual System::String ^ get() { return System::String::Empty;  }
    virtual void set( System::String^ descriptoin_ )
    {
      throw gcnew System::NotImplementedException;
    }
  }

  property System::String ^ Name
  {
    virtual System::String ^ get() { return pPar->sName; }
    virtual void set( System::String ^ name_ )
    {
      throw gcnew System::NotImplementedException;
    }
  }

  property System::String ^ CustomerTag
  {
    virtual System::String ^ get()
    {
      throw gcnew System::NotImplementedException;
    }
    virtual void set( System::String^ customerTag_ )
    {
      throw gcnew System::NotImplementedException;
    }

  }

  //
  property System::Nullable<double> Min
  {
    virtual System::Nullable<double> get() { return System::Nullable<double>();  }
    virtual void set( System::Nullable<double> val )
    {
      throw gcnew System::NotImplementedException;
    }
  }

  property System::Nullable<double> Max
  {
    virtual System::Nullable<double> get() { return System::Nullable<double>(); }
    virtual void set( System::Nullable<double> val )
    {
      throw gcnew System::NotImplementedException;
    }

  }

  property IProjectEU^ EU
  {
    virtual IProjectEU^ get()
    {
      switch ( tType )
      {
      case IOType::AnalogIn:
      case IOType::AnalogOut:
        return SNC::OptiRamp::Services::ProjectEUFactory::fProjectEUFactory::CreateInstance( safe_cast<aCONF::C_Project_Node_App::C_Analog^>(pPar)->pConv );
      case IOType::DigitalIn:
      case IOType::DigitalOut:
        return nullptr;
      default:
        _ASSERT( false );
        return nullptr;
      } 
    }
    virtual void set( IProjectEU^ projectEU_ )
    {
      throw gcnew System::NotImplementedException;
    }
  }

  property bool NCUType
  {
    virtual bool get() { return false;  }
    virtual void set( bool )
    { throw gcnew System::NotImplementedException;  }
  }

  property bool DiscreteType
  {
    virtual bool get() { return false; }
    virtual void set( bool )
    { throw gcnew System::NotImplementedException; }
  }

  property IOType type 
  { 
    virtual IOType get() { return tType;  }
    virtual void set( IOType) 
    {
      throw gcnew System::NotImplementedException;
    }
  }

  property System::Nullable<double> initValue 
  { 
    virtual System::Nullable<double> get() { return System::Nullable<double>();  }
    virtual void set( System::Nullable<double> )
    {
      throw gcnew System::NotImplementedException;
    }
  }


  //
  fIOChannel(
    aCONF::C_Project_Node_App::C_Parameter  ^ pPar_,
    const IOType                              tType_
    ) :pPar( pPar_ ), tType( tType_ )
  {
    _ASSERT( pPar != nullptr );
  }
}; // ............................... fIOChannel ..................................


//
// dataChannels::get
//
IEnumerable<IDataChannel^> ^ fElement::dataChannels::get()
{
  if (pDataChannels == nullptr)
  {
    List<IDataChannel^> ^ pBuf;

    // web channels
    for each (IDataChannel^ data in WebChannels)
    {
      Create_if_null(pBuf)->Add(data);
    }

    // AI, DI, DO
    aCONF::C_Project_Node_App ^ const pNode_App = dynamic_cast<aCONF::C_Project_Node_App ^>(pNode);
    if ( pNode_App != nullptr )
    {

      auto pAIs = pNode_App->Get_Analog_Inputs();
      if ( Safe_Length( pAIs ) > 0 )
      {
        Create_if_null( pBuf );
        for each (auto pIter in pAIs)
        {
          pBuf->Add( gcnew fIOChannel( pIter, IOType::AnalogIn ) );
        }
      }
      
      auto pDIs = pNode_App->Get_Discrete_Inputs();
      if ( Safe_Length( pDIs ) > 0 )
      {
        Create_if_null( pBuf );
        for each (auto pIter in pDIs)
        {
          pBuf->Add( gcnew fIOChannel( pIter, IOType::DigitalIn ) );
        }
      }

      auto pDOs = pNode_App->Get_Discrete_Outputs();
      if ( Safe_Length( pDOs ) > 0 )
      {
        Create_if_null( pBuf );
        for each (auto pIter in pDOs)
        {
          pBuf->Add( gcnew fIOChannel( pIter, IOType::DigitalOut ) );
        }
      }

      auto pAOs = pNode_App->Get_Analog_Outputs();
      if ( Safe_Length( pAOs ) > 0 )
      {
        Create_if_null( pBuf );
        for each (auto pIter in pAOs)
        {
          pBuf->Add( gcnew fIOChannel( pIter, IOType::AnalogOut ) );
        }
      }

    } // if ( pNode_App != nullptr ) ......



    pDataChannels = (pBuf == nullptr) ? fWebChannel::oEmpty_Data_Channels : pBuf->ToArray();
  }

  return pDataChannels;
} // ................................... dataChannels::get ............................................

void fElement::dataChannels::set(IEnumerable<IDataChannel^> ^ pDataChannels_)
{
  throw gcnew NotImplementedException;
}

IEnumerable<IElement^> ^ fElement::GetChildren()
{
  C_All_Element_List ^ pRet = gcnew C_All_Element_List;

  if (pNode != nullptr && pNode->Has_Kids())
  {
    for each (C_Project_Node ^ pIter in pNode->pKids)
    {
      fElement ^ pElement;
      if (pNode_Element->TryGetValue(pIter, pElement))
      {
        if (pElement->Type != nullptr)
          pRet->Add(pElement);
      }

      //pRet->Add(pNode_Element[pIter]);
    }
  }

  for (int i = 0; i < pHMI_Elements->Count; ++i)
  {
    pRet->Add(pHMI_Elements[i]);
  }

  return pRet;
}

IElement^ fElement::GetParentOfType(IElemType ^ parentType)
{
  throw gcnew NotImplementedException;
}

void fElement::AddHmiChildElement(IElement ^ pParentElement, IElement ^ pChildHmiElement)
{
  _ASSERT(pParentElement != nullptr);
  _ASSERT(pChildHmiElement != nullptr);

  System::Collections::Generic::List<int> ^ pChildrenIDs = gcnew System::Collections::Generic::List<int>();
  pChildrenIDs->AddRange(pParentElement->Children);
  pChildrenIDs->Add(pChildHmiElement->Id);
  pParentElement->Children = pChildrenIDs->ToArray();

  System::Collections::Generic::List<IElement ^> ^ pChildrenElements = gcnew System::Collections::Generic::List<IElement ^>();
  pChildrenElements->AddRange(pParentElement->GetChildren());
  pChildrenElements->Add(pChildHmiElement);

  fElement ^ pElement = dynamic_cast<fElement ^>(pParentElement);

  pElement->pHMI_Elements->Add(pChildHmiElement);
}

void fElement::Set_HMI_Element_Type(fElement ^ pElement, System::Xml::XmlNode ^ pXmlNode, System::String ^ sType_)
{
  _ASSERT(pElement != nullptr);
  _ASSERT(pXmlNode != nullptr);
  _ASSERT(sType_ != nullptr);

  pElement->sType = fElementType::XlatTypeID(sType_);
  fElementType::Add(gcnew fElementType(pElement->sType));

  pElement->sName = Attribute_to_String(pXmlNode, "name");
  if (System::String::IsNullOrEmpty(pElement->sName))
  {
    if (System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::STATICSHAPE) == 0)
    {
      pElement->sName = Attribute_to_String(pXmlNode, "shapetype");
    }
    else if (System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::STATICIMAGE) == 0)
    {
      pElement->sName = Attribute_to_String(pXmlNode, "image");
    }
    else if (System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::FLAG) == 0)
    {
      pElement->sName = "DynamicTextFlag";
    }
    else if (System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::STATICTEXT) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::STATICCONNECTOR) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::STATICLINE) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::TITLE) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::DATAPOINT) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::SETPOINT) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::BARCHART) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::CHANNEL) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::HISTORICAL) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::CHART) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::REQUEST) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::TRENDCHART) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::GAUGE) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::METER) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::NORMALDISTRIBUTIONPLOT) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::NDPLOT) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::GRID) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::DISTANCE) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::LEGEND) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::TARGETVALUE) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::TARGETVALUES) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::BOXANDWHISKERPLOT) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::PLOT) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::SCALE) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::CHANNELS) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::VALUE) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::COLUMNS) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::DELIMITER) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::FORMAT) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::POPUPLINK) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::REFTABLEROW) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::REFTABLECOLUMNS) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::REFTABLEUNITS) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::POPUPNAME) == 0 ||
      System::String::CompareOrdinal(pElement->sType, fRT::TypeIDs::SEGMENT) == 0)
    {
      pElement->sName = pElement->sType;
    }
    else
    {
      pElement->sName = "UnNamed";
    }
  }
}
