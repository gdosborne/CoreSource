//-------------------------------------------------------------------
// © 2015 Statistics & Control Inc.  All rights reserved.
// Created by:	Rex Gray
//-------------------------------------------------------------------

#include "..\..\common\web\Config_Set_Web_Page.h"
#include "..\..\common\Utils_Enum.h"
#include "..\..\common\Utils_Net_Xml.h"

namespace aWEB
{
  //
  // C_Web_Page_Core::Get_Property_Control
  //
  aCONF::C_Project_Node_Property ^ C_Web_Page_Core::Get_Property_Control(System::Drawing::Size oInitialSize)
  {
    return nullptr;
  } // ................................... Get_Property_Control ...........................

  //
  // Get_Property_Control
  //
  aCONF::C_Project_Node_Property ^ C_Project_Node_Web_Page::Get_Property_Control(System::Drawing::Size oInitialSize)
  {
    return nullptr;
  } // ................................... Get_Property_Control ...........................

  //
  // Pasted
  //
  void C_Project_Node_Web_Page::Pasted()
  {
  } // ................................... Pasted ...........................

  //
  // C_Project_Node_Lib_Channel::Get_Property_Control
  //
  aCONF::C_Project_Node_Property ^ C_Project_Node_Lib_Channel::Get_Property_Control(System::Drawing::Size oInitialSize)
  {
    return nullptr;
  } // ................................... Get_Property_Control ...........................

  //
  // *** C_Project_Node_Picture ****
  //
  // Save
  //
  void C_Project_Node_Picture::Save(System::Xml::XmlNode ^ pTarget)
  {
    __super::Save(pTarget);

    //System::String ^ sPicture = Picture_to_String();
    //if (sPicture != nullptr)
    //{
    //  System::Xml::XmlNode ^ const pXmlImage = Add_Child_Node(pTarget, "image");
    //  pXmlImage->InnerText = sPicture;
    //}
    //else if (!System::String::IsNullOrEmpty(sPictureStr)) {
    //  Add_Attribute(pTarget, "PictureStr")->Value = tPictureStr_Type.ToString();
    //  System::Xml::XmlNode ^ const pXmlStr = Add_Child_Node(pTarget, "str");

    //  pXmlStr->InnerText = System::Convert::ToBase64String(
    //    System::Text::Encoding::UTF8->GetBytes(sPictureStr),
    //    System::Base64FormattingOptions::InsertLineBreaks);
    //}
  } // ............................ Save ............................

  //
  // Load
  //
  void C_Project_Node_Picture::Load(System::Xml::XmlNode ^ pSource)
  {
    __super::Load(pSource);

    System::Xml::XmlNode ^ pXmlImage = Find_Child_Node(pSource, "image");
    if (pXmlImage != nullptr)
    {
      System::String ^ sImage = pXmlImage->InnerText;
      if (sImage != nullptr)
      {
        try
        {
          System::IO::MemoryStream oStream(System::Convert::FromBase64String(sImage));
          System::Drawing::Bitmap ^ pBuf = gcnew System::Drawing::Bitmap(%oStream);
          delete pPicture; pPicture = pBuf;
        }
        catch (...) {
          _ASSERT(false);
        }
      } // if (sImage != nullptr) ...
    }
    else if ((pXmlImage = Find_Child_Node(pSource, "str")) != nullptr) {
      if (Enum_Try_Parse(Attribute_to_String(pSource, "PictureStr"), tPictureStr_Type))
      {
        sPictureStr = System::Text::Encoding::UTF8->GetString(
          System::Convert::FromBase64String(pXmlImage->InnerText));
      }
    }
  } // ................................... Load .............................

  //
  // Get_Property_Control
  //
  aCONF::C_Project_Node_Property ^ C_Project_Node_Picture::Get_Property_Control(System::Drawing::Size oInitialSize)
  {
    return nullptr;
  } // ................................... Get_Property_Control ...........................

}
