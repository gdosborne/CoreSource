#include "fWeb_HMI_Proxy.h"
#include "fElement.h"
#include "fWebChannel.h"
#include "..\..\common\Config\Config_Set_Signals.h"

// .Net namespaces
using namespace System;
using namespace System::Collections::Generic;
using namespace System::Drawing;
using namespace System::Drawing::Drawing2D;
using namespace System::Drawing::Text;
using namespace System::IO;
using namespace System::Runtime::InteropServices;

// Cusotm namespaces
using namespace SNC::OptiRamp::Services::fDefs;
using namespace SNC::OptiRamp::Services::fWeb;

//
// fWebProxy
//
private ref struct fWebProxy sealed : IWebProxy
{

  aWEB::C_Web_Proxy ^ pWeb_Proxy;
private:
  //
  fWebChannel ^ Get_fWebChannel(IWebChannel ^ pChannel);

public:
  //
  virtual void Start() override
  {
    if ( pWeb_Proxy != nullptr )
    {
      pWeb_Proxy->Start();
    }
  }


  //
  virtual void Stop() override
  {
    if ( pWeb_Proxy != nullptr )
    {
      pWeb_Proxy->Stop();
    }
  }

  virtual void Fire_Value(IWebChannel^ pChannel, System::Nullable<double>  tValue) override;

  virtual bool Web_Channel_Is_InUse(IWebChannel^ pChannel) override;

  virtual void Fire_Picture(IWebChannel^ pChannel, IPictureBuilder^ pPictureBuilder) override;

protected:
  //
  !fWebProxy()
  {
    delete pWeb_Proxy; pWeb_Proxy = nullptr;
  }
  //
  ~fWebProxy() { this->!fWebProxy(); }
};

//
//------------------------- IWebProxy Implementation ---------------------------------------------
//


//
// Create_Ex
//
static IWebProxy ^ Create_Ex(
  fProject                        ^ pProject_f,
  aCONF::C_Project_Node::C_Kids   ^ pNodes,
  System::String                  ^ sRuntime_Tag,
  [ Out ] String                ^ % sError
  )
{
  fWebProxy ^ pRet = gcnew fWebProxy;
  try
  {
    bool bHas_DB_Objects;
    aWEB::C_Web_Proxy::C_Result tResult;

    pRet->pWeb_Proxy = aWEB::C_Web_Proxy::Create( pProject_f->sProjectFileName,
      pNodes,
      pProject_f->pProjectComputer,
      sRuntime_Tag,
      0,
      false, // has DB
      bHas_DB_Objects,
      tResult );

    if ( pRet->pWeb_Proxy == nullptr )
    {
      if ( tResult.bIs_Error )
      {
        sError = tResult.sError_of_Message;
      } else
      {
        sError = nullptr;
      }

      return nullptr;
    }

    return null_and_return( pRet );
  } finally
  {
    delete pRet; pRet = nullptr;
  }
} // ................................ Create_Ex .............................


//
//
IWebProxy ^ IWebProxy::Create(
  IProject                  ^ pProject, 
  IEnumerable< IElement^ >  ^ pPotentialElements, 
  [ Out ] String          ^ % sError
  )
{
  aFW::C_Start_Stop_Trace oTrace( "IWebProxy::Create", true );
  try
  {
    if ( pPotentialElements == nullptr )
    {
      sError = "empty list of potential elements"; return nullptr;
    }

    fProject ^ const pProject_f = safe_cast<fProject ^>(pProject);
    if ( pProject_f == nullptr || !pProject_f->IsOpen() )
    {
      sError = "project is not opened";  return nullptr;
    }


    aCONF::C_Project_Node::C_Kids oNodes;

    for each (IElement ^ pIter in pPotentialElements)
    {
      fElement ^ const pIter_f = dynamic_cast<fElement ^>(pIter);
      if ( pIter_f == nullptr || pIter_f->pNode == nullptr )
      {
        _ASSERT( false );
      } else
      {
        _ASSERT( !oNodes.Contains( pIter_f->pNode ) );
        oNodes.Add( pIter_f->pNode );
      }
    } // for each (IElement ^ pIter ....


    return Create_Ex( pProject_f, % oNodes, "Rod_Pump", sError );

  } catch ( Exception ^ pErr ) {
    aFW::Runtime_Exception( sError, pErr );
    return nullptr;
  }
} // ........................................ Create .................................


//
// Create_VTS
//
IWebProxy ^ IWebProxy::Create_VTS(
  IProject                                                        ^ pProject,
  [ System::Runtime::InteropServices::Out ] C_Tag_WebChannel    ^ % pTag_WebChannel,
  [ System::Runtime::InteropServices::Out ] System::String      ^ % sError
  )
{
  aFW::C_Start_Stop_Trace oTrace( "IWebProxy::Create_VTS", true );
  try
  {
    pTag_WebChannel = gcnew C_Tag_WebChannel;

    fProject ^ const pProject_f = safe_cast<fProject ^>(pProject);
    if ( pProject_f == nullptr || !pProject_f->IsOpen() )
    {
      sError = "project is not opened";  return nullptr;
    }

    auto pNode_Computer = pProject_f->pProjectComputer;
    auto pNode_VTS = aCONF::C_Project_Node_Type_Single_Instance::Find_Kid( 
                        pNode_Computer, aCONF::C_Constants_Signals::pTag_VTS_Server );

    if ( pNode_VTS == nullptr )
    {
      // not error
      System::Diagnostics::Trace::WriteLine( "VTS Server is not defined" );
      return nullptr;
    }

    aCONF::C_Project_Node::C_Kids oVTS_Folders;
    { // fill list of VTS folders
      auto pIter = pNode_Computer;
      while ( (pIter = aCONF::C_Project_Node::Traverse( pIter, pNode_Computer )) != nullptr )
      {
        if ( pIter->pType->sTag == aCONF::C_Constants_Signals::pTag_VTS_Folder )
        {
          oVTS_Folders.Add( pIter );
        }
      }
    } // fill list ....

    IWebProxy ^ pRet = Create_Ex( pProject_f, % oVTS_Folders, "Virtual_Tag_Service", sError );
    try
    {
      if (pRet != nullptr)
      { // fill dictionary 
        auto pIter = pNode_Computer;
        while ( (pIter = aCONF::C_Project_Node::Traverse( pIter, pNode_Computer )) != nullptr )
        {
          if ( pIter->pType->sTag == aCONF::C_Constants_Signals::pTag_VTS_Folder )
          {
            auto pIterE = pProject_f->pNode_Elements[pIter];
            for each (auto pIter_Data in pIterE->dataChannels)
            {
              auto pIter_Web = dynamic_cast<IWebChannel^>(pIter_Data);
              if ( pIter_Web == nullptr || !pRet->Web_Channel_Is_InUse( pIter_Web ) )
              {
                continue;
              }
              // name of Web Channel 
              System::String ^ sChannel_Name = pIter_Web->Name;

              // should correspond to name of VTS Tag
              aCONF::C_Project_Node_Signal ^ pFound_Signal = nullptr;
              for each (auto pIter_Tag in pIter->pKids)
              {
                if (   pIter_Tag->Get_UI_Name() == sChannel_Name 
                    && (pFound_Signal = dynamic_cast<aCONF::C_Project_Node_Signal ^>(pIter_Tag)) != nullptr  
                    )
                {
                  break;
                }
              }

              if ( pFound_Signal == nullptr )
              {
                _ASSERT( false ); // impossible
              } else {
                System::String ^ sFull_Tag = pFound_Signal->Full_Tag();
                if ( pTag_WebChannel->ContainsKey(sFull_Tag) )
                {
                  System::Diagnostics::Trace::WriteLine( "Detected duplicated VTS Tag \"" + sFull_Tag + "\"" );
                } else {
                  pTag_WebChannel->Add( sFull_Tag, pIter_Web );

                }
              } // if ( pFound_Signal == nullptr )




            } // for each (auto pIter_Data .......

          } // if ( pIter ....
        } // while ( (pIter = ....

        if ( pTag_WebChannel->Count == 0 )
        {
          System::Diagnostics::Trace::WriteLine( "No used VTS tags" );
          return nullptr;
        }

      } // if (pRet != nullptr) .......




      return null_and_return(pRet);
    } finally {
      delete pRet; pRet = nullptr;
    }

  } catch ( Exception ^ pErr ) {
    aFW::Runtime_Exception( sError, pErr );
    return nullptr;
  }
} // ............................ Create_VTS ................................

//
//------------------------ fWebProxy Implementation -------------------------------------
//
fWebChannel ^ fWebProxy::Get_fWebChannel(IWebChannel ^ pChannel)
{
  if (pWeb_Proxy == nullptr) 
  { 
    _ASSERT(false); 
    return nullptr; 
  }

  fWebChannel ^ const pRet = dynamic_cast<fWebChannel ^>(pChannel);

  if (pRet != nullptr && pRet->pNode != nullptr) 
    return pRet;

  _ASSERT(false); return nullptr;
} 



void fWebProxy::Fire_Value(IWebChannel^ pChannel, Nullable<double>  tValue) 
{
  try
  {
    fWebChannel ^ const pChannel_f = Get_fWebChannel(pChannel);

    if (pChannel_f == nullptr) 
    { 
      return; 
    }

    Type ^ const pType = pChannel_f->Type;

    if (pType == double::typeid || pType == bool::typeid)
    {
      pWeb_Proxy->Fire_Value(pChannel_f->pNode, pChannel_f->iIdx, tValue);
    }
    else 
    {
      _ASSERT(false);
    }
  }
  catch (Exception ^ pErr) 
  {
    SILENT_EXCEPTION(pErr);
  }
}

bool fWebProxy::Web_Channel_Is_InUse(IWebChannel^ pChannel)
{
  fWebChannel ^ const pChannel_f = Get_fWebChannel(pChannel);

  if (pChannel_f == nullptr) 
  { 
    return false; 
  }

  Type ^ const pType = pChannel_f->Type;

  if (pType == double::typeid || pType == bool::typeid)
  {
    return pWeb_Proxy->Is_Used_Channel(pChannel_f->pNode, pChannel_f->iIdx);
  }
  else if (pType == Bitmap::typeid) 
  {
    return pWeb_Proxy->oNodes_Picture.Find(pChannel_f->pNode, pChannel_f->Name) != nullptr;
  }
  else 
  {
    _ASSERT(false);
  }

  return false;
}

// 
// Fire_Picture
//
void fWebProxy::Fire_Picture(IWebChannel^ pChannel, IPictureBuilder^ pPictureBuilder) 
{
  try
  {
    auto pChannel_f = Get_fWebChannel(pChannel);

    if (pChannel_f == nullptr) 
    { 
      return; 
    }
    if (pChannel_f->Type != Bitmap::typeid) 
    { 
      _ASSERT(false); 
      return; 
    }

    auto pPict = pWeb_Proxy->oNodes_Picture.Find( pChannel_f->pNode, pChannel_f->Name );

    if (pPict == nullptr)
    {
      // means that we don't use this picture in web hmi 
      // use Web_Channel_Is_InUse to verify that we need to fire this picture
      _ASSERT(false); 
      return;
    }

    if (pPict->tSize.IsEmpty)
    {
      // paranoidal crap
      _ASSERT(false); 
      return;
    }

    if (pPictureBuilder == nullptr)
    {
      // assume that we fire empty stuff
      pWeb_Proxy->Fire_Picture(pPict, nullptr);
      return;
    }

    Parameters oParameters;

    Bitmap oBitmap(pPict->tSize.Width, pPict->tSize.Height);

    oParameters.tRect = Rectangle(Point::Empty, oBitmap.Size);
    oParameters.tAxisXDisplayStyle = AxisXDisplayStyle::GenericValues;

    Graphics ^ pG;

    try
    {
      pG = Graphics::FromImage(%oBitmap);

      try
      {
        Color tBackground_Color = Color::Black;

        if (pPict->pApp_Web_Properties != nullptr)
        {
          switch (pPict->pApp_Web_Properties->tRuntime_Picture_Style)
          {
          case aWEB::C_App_Web_Properties::E_Runtime_Picture_Style::White_Background:
            tBackground_Color = Color::White;
            oParameters.tRuntimePictureStyle = RuntimePictureStyle::White_Background;
            break;
          case aWEB::C_App_Web_Properties::E_Runtime_Picture_Style::Black_Background:
            tBackground_Color = Color::Black;
            oParameters.tRuntimePictureStyle = RuntimePictureStyle::Black_Background;
            break;
          default: _ASSERT(false);
          }
        }


        pG->Clear(tBackground_Color);

        // aUI::C_Control_Core::Set_High_Quality_Mode(pG) is replaced with the following 2 lines, 
        // don't wan't to include the module into the project
        pG->SmoothingMode = SmoothingMode::AntiAlias;
        pG->TextRenderingHint = TextRenderingHint::AntiAlias;

        ResponseStatus ^ pStatus;

        pPictureBuilder->BuildPicture(pG, %oParameters, pStatus);
      }
      catch (Exception ^ pErr) 
      {
        pG->DrawString(pErr->ToString(), SystemFonts::DefaultFont, Brushes::Tomato, oParameters.tRect);
      }

    }
    finally 
    {
      delete pG;      pG = nullptr;
    }

    MemoryStream  oMemory;
    oBitmap.Save( %oMemory, aWEB::C_Web_Proxy::ctOutput_Image_Format );
    pWeb_Proxy->Fire_Picture(pPict, oMemory.ToArray());
  }
  catch (Exception ^ pErr) 
  {
    SILENT_EXCEPTION(pErr);
  }
} // ......................................... Fire_Picture ...........................

//
//------------------------ End of fWebProxy Implementation -------------------------------
//