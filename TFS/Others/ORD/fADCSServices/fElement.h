#pragma once
//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by: Jeff Shafferman
//-------------------------------------------------------------------

//
// Implementation of IElement
//

#include "..\..\common\Config\Config_Project_Tree.h"

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace fDefs
      {
        // predefinitions
        ref class fElement;
        typedef System::Collections::Generic::Dictionary<aCONF::C_Project_Node^, fElement^> C_Node_Element;

        //
        // fElement
        //
        private ref class fElement : IElement
        {
        private:
          System::Collections::Generic::IEnumerable<int> ^ pChildren;
          System::Collections::Generic::IEnumerable<IDataChannel^> ^ pDataChannels;
          System::Collections::Generic::IEnumerable<Peer> ^ pPeers;
          initonly static array<Peer> ^ oEmptyPeers = gcnew array<Peer>( 0 );
          System::Collections::Generic::List<IElement^> ^ pHMI_Elements = gcnew System::Collections::Generic::List<IElement ^>(0);

        internal:
          const int               iNodeID;
          C_Node_Element        ^ const pNode_Element;
          aCONF::C_Project_Node ^ const pNode;

          const int               iElementID;
          IElement              ^ pParentElement;
          System::String        ^ sName;
          System::String        ^ sType;

          typedef System::Collections::Generic::Dictionary<System::String^, IProperty^> C_Element_Properties;

          delegate void DLG_Fill_Properties(aCONF::C_Project_Node ^ pSource_Node, C_Element_Properties  ^ pTarget_Properties, bool% bHandled);

          static DLG_Fill_Properties ^ dlg_Fill_Properties;

          System::Collections::Generic::IDictionary<System::String^, IProperty^> ^ pProperties;
          array<SNC::OptiRamp::Services::fWeb::IWebChannel ^> ^ pWebChannels;
          //
          property array<SNC::OptiRamp::Services::fWeb::IWebChannel ^> ^ WebChannels
          {
             virtual array<SNC::OptiRamp::Services::fWeb::IWebChannel ^> ^ get();
          }
        public:
          static fElement();

          fElement(int iNodeID_, aCONF::C_Project_Node ^ pNode_, C_Node_Element  ^ pNode_Element_);

          fElement(int iElementID_, IElement ^ pWebPageElement);

          static void Set_HMI_Element_Type(fElement ^ pElement, System::Xml::XmlNode ^ pXmlNode, System::String ^ sType_);

          static void AddHmiChildElement(IElement ^ pParentElement, IElement ^ pChildHmiElement);

          // ----- fDefs:IElement -------------------
          property int Id
          {
            virtual int get();
          }

          property System::String ^ Name
          {
            virtual System::String ^ get();
          }

          property System::String ^ Description
          {
            virtual System::String ^ get();
            virtual void set(System::String ^ sDescription_);
          }

          property System::String ^ Tag
          {
            virtual System::String ^ get();
          }

          property IElemType ^ Type
          {
            virtual IElemType ^ get();
          }

          property System::Collections::Generic::IDictionary<System::String^, IProperty^> ^ Properties
          {
            virtual System::Collections::Generic::IDictionary<System::String^, IProperty^> ^ get();
            virtual void set(System::Collections::Generic::IDictionary<System::String^, IProperty^> ^ pProperties_);
          }

          property IElement ^ Parent
          {
            virtual IElement ^ get();
            virtual void set(IElement ^ pParent_);
          }

          property System::Collections::Generic::IEnumerable<int> ^ Children
          {
            virtual System::Collections::Generic::IEnumerable<int> ^ get();
            virtual void set(System::Collections::Generic::IEnumerable<int> ^ pChildren_);
          }

          property System::Collections::Generic::IEnumerable<Peer> ^ Peers
          {
            virtual System::Collections::Generic::IEnumerable<Peer> ^ get();
            virtual void set(System::Collections::Generic::IEnumerable<Peer> ^ pPeers_);
          }

          property System::Collections::Generic::IEnumerable<IDataChannel^> ^ dataChannels
          {
            virtual System::Collections::Generic::IEnumerable<IDataChannel^> ^ get();
            virtual void set(System::Collections::Generic::IEnumerable<IDataChannel^> ^ pDataChannels_);
          }

          virtual System::Collections::Generic::IEnumerable<IElement^> ^ GetChildren();

          virtual IElement ^ GetParentOfType(IElemType ^ parentType);
        };
      }
    }
  }
}