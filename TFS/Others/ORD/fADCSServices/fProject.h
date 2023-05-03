#pragma once
//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by: Ilya Markevich
//-------------------------------------------------------------------

//
// Implementation of the IElement, IProject, IElemType interfaces
//
#include "fElement.h"
#include "..\..\common\Config\Config_Project_Tree.h"
#include "..\..\common\Web\Config_Set_Web.h"
#include "..\..\common\Web\Config_Set_Web_Page.h"
#include "..\..\common\HMI\Items_HMI.h"
#include "..\..\common\Web\Web_HMI.h"
#include "..\..\common\Web\Web_HMI_dlg.h"

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace fDefs
      {
        private ref struct C_Image_Descriptor sealed
        {
        public:
          System::String ^ sImageXmlName;
          int iHmi_ID;
        };

        typedef System::Collections::Generic::List<IElement ^> C_All_Element_List;
        typedef System::Collections::Generic::Dictionary<System::String ^, C_Image_Descriptor ^> C_Image_Element_List;

        public ref class fProject /*sealed*/ : IProject
        {
          initonly static array<wchar_t> ^ oDotSeparator = gcnew array<wchar_t> {'.'};
          initonly static array<wchar_t> ^ oBrokenBarSeparator = gcnew array<wchar_t> {'|'};
          C_All_Element_List    ^ allElementList;
          C_Image_Element_List ^ pImageElements;
          System::Collections::Generic::List<System::String ^> oAssociationErrors;

        internal:
          System::String ^ sProjectFileName = System::String::Empty;
          
          C_Node_Element        ^ pNode_Elements;
          
          aCONF::C_Project_Node ^ pProjectRoot;
          aCONF::C_Project_Node ^ pProjectComputer;

        private:
          void InitializeModelObjects();
          //
          void SetProjectRoot( aCONF::C_Project_Node ^ pNewProjectRoot, aCONF::C_Project_Node ^ pNewProjectComputer, bool bEnhanced_Web_HMI );
          //
          void ResetProjectRoot(  );
          //
          void CreateProjectNodeTree( System::String ^ sFileName, System::String ^ sImpersonatedComputer, bool bEnhanced_Web_HMI );
          //
          void Parse_Page_Elements(fElement ^ pParentElement, System::Xml::XmlNode ^ const pXmlParentNode, System::String ^ sHMI_Type);
          //
          void Parse_Page_Elements(fElement ^ pParentElement, System::Xml::XmlNode ^ const pXmlNode);
          //
          void Parse_HMI_Items(aWEB::C_Project_Node_Web_Server ^ pWebServer);
          //
          fElement ^ Create_HMI_Child_Element(fElement ^ pParentElement);
          //
          void Set_HMI_Child_Properties(fElement ^ pChildElement, System::Xml::XmlNode ^ const pXmlHmiNode, System::String ^ const sType);
          //
          void Create_HMI_Children_Elements(fElement ^ pParentElement, System::Xml::XmlNode ^ const pXmlHmiNode, System::String ^ const sType);
          //
          void InjectErrorPage(System::String ^ sError);
          //
          void Add_Datasource_Peer(IElement ^ const pElement, System::String ^ const sDatasourcePropertyName, bool bHasFullNameValue);
          //
          void Add_Value_Peer(IElement ^ const pElement, bool bHasFullNameValue);
          //
          void Add_Property_Peer(IElement ^ const pElement, IProperty ^ const pPeerValue);
          //
          void Add_Reference_Peer(IElement ^ const pElement, bool bHasFullNameValue);
          //
          void Add_Popup_Peer(IElement ^ const pElement, bool bHasFullNameValue);
          //
          void Add_Image_Peer(IElement ^ const pElement, System::String ^ sImageName);
          //
          void Add_Multiple_Peers(fElement ^ pChildElement);
          //
          void Add_VTS_Peer(IElement ^ const pElement, System::String ^ const sPeerType);
          //
          void Add_VTS_Folder_Peer(IElement ^ const pElement);
          //
          void Add_VTS_Server_Peer(IElement ^ const pElement);

        public:
          fProject(fDiagnostics::IOptiRampLog^ logService_, System::String^ sAppName);

          property System::String ^ Path
          {
            virtual System::String ^ get();
          }

          property IElement ^ Root
          {
            virtual IElement ^ get();
          }

          virtual IElement ^ GetElemById( int elementId );

          property IElement ^ default[ int ]
          {
            virtual IElement ^ get(int index);
            virtual void set(int index, IElement ^ newElement);
          }

          virtual int GetElementsCount();

          virtual IElemType ^ GetElemType( System::String ^ TypeID );

          virtual System::Collections::Generic::IEnumerable<IElement^> ^ GetElemsOfType( IElemType ^ elemType );

          literal System::String ^ Option_Enhanced_Web_HMI = "Enhanced_Web_HMI";
          //
          // List of supported options (Key + Value)
          // c + Impersonated_computer_name
          // Enhanced_Web_HMI + nullptr
          //
          virtual IElement ^ Open(
            System::String ^ sProjectFileName_, 
            System::Collections::Generic::IDictionary<System::String ^, System::String ^> ^ options, 
            [ System::Runtime::InteropServices::Out ] ResponseStatus ^% status 
            );

          virtual bool Save( [ System::Runtime::InteropServices::Out ] ResponseStatus ^%  status );


          virtual bool IsOpen();

          property IElement ^ ActiveComputer
          {
            virtual IElement ^ get();
          }

          virtual event ProjectNotify ^ eventNotify;


          virtual int AddElement( 
            int               parentId, 
            System::String  ^ sName, 
            IElemType       ^ type,
            System::String  ^ sDescription,
            System::Collections::Generic::IDictionary<System::String^, IProperty^> ^ properties,
            [ System::Runtime::InteropServices::Out ]  System::String ^ % error 
            );


          virtual bool DeleteElement( 
            int elementId, 
            [ System::Runtime::InteropServices::Out ]  System::String ^ % error
            );


        protected:
          !fProject();

          ~fProject();

        };
      } // namespace fDefs
    } // namespace Services
  } // namespace OptiRamp
} // namespace SNC
