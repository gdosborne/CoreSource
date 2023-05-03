#pragma once
//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by: Ilya Markevich
//-------------------------------------------------------------------

//
// Implementation of the IProperty interfaces
//

#include "fElementType.h"
#include "..\..\common\Config\Config_Project_Tree.h"
#include "..\..\common\Config\Parameters_Helper.h"

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace fDefs
      {
        //
        // C_Setter
        //
        private ref struct C_Setter abstract
        {
          //
          virtual void Set( System::String ^ sNewValue ) = 0;
        }; // .............................. C_Setter ........................

        //
        // C_Setter_Reflection_Prop
        //
        private ref struct C_Setter_Reflection_Prop sealed : C_Setter
        {
          aCONF::C_Project_Node ^ const pTarget;
          System::String        ^ const sTargetProperty;
          //
          C_Setter_Reflection_Prop(
            aCONF::C_Project_Node ^ pTarget_,
            System::String        ^ sTargetProperty_
            );
          //
          virtual void Set( System::String ^ sNewValue ) override;
        }; // ................................ C_Setter_Reflection_Prop .....................

        //
        // C_Setter_Tag_Value
        //
        private ref struct C_Setter_Tag_Value sealed : C_Setter
        {
          aCONF::C_TagValues    ^ const pTarget;
          System::String        ^ const sTag;
          //
          C_Setter_Tag_Value(
            aCONF::C_TagValues    ^ pTarget_,
            System::String        ^ sTag_
            );
          //
          virtual void Set( System::String ^ sNewValue ) override;
        }; // ................................ C_Setter_Tag_Value .....................


        //
        // fProperty
        //
        private ref struct fProperty abstract : IProperty
        {
          System::String  ^ const sName;
          C_Setter        ^ pSetter;
          initonly static array<wchar_t> ^ oMainSeparators = gcnew array<wchar_t> {'{', ',', '}'};
          initonly static array<wchar_t> ^ oEqualSeparator = gcnew array<wchar_t> {'='};
          initonly static array<System::String ^> ^ oIntegerProperties =
            gcnew array<System::String ^> {
            "countofaxisgridlines",
              "countofgridlines",
              "countoftimegridlines",
              "salt",
              "rowcount",
              "colcount"
          };
          initonly static array<System::String ^> ^ oDoubleTypeValueProperties =
            gcnew array<System::String ^> {
            "BrushState",
              "DataPoint",
              "ImageState",
              "PageState",
              "Unit"
          };
          initonly static array<System::String ^> ^ oDoubleProperties =
            gcnew array<System::String ^> {
            "angle",
              "blurradius",
              "bordersize",
              "borderthickness",
              "boxlinethickness",
              "boxsize",
              "circularbarthickness",
              "color2endoffset",
              "cornerradius",
              "diameter",
              "direction",
              "endvalue",
              "fontsize",
              "gridlinethickness",
              "highhigh",
              "high",
              "highhighvalue",
              "highvalue",
              "lowlow",
              "low",
              "lowlowvalue",
              "lowvalue",
              "maximum",
              "minimum",
              "needlelength",
              "numberofmajorticks",
              "numberofminorticks",
              "numberofpoints",
              "opacity",
              "operatingrangemaximum",
              "operatingrangeminimum",
              "scaletextsize",
              "shadowdepth",
              "sigma",
              "speedometerborderthickness",
              "speedometerdiameter",
              "startvalue",
              "thickness",
              "thumbborderthickness",
              "tickthickness",
              "value",
              "width"
          };
          initonly static array<System::String ^> ^ oConvertDoubleToIntegerProperties =
            gcnew array<System::String ^> {
            "bordersize"
          };
          initonly static array<System::String ^> ^ oMultiPeerProperties =
            gcnew array<System::String ^> {
            "highhighvalue",
              "highhigh",
              "highvalue",
              "high",
              "lowlowvalue",
              "lowlow",
              "lowvalue",
              "low",
              "maximum",
              "minimum"
          };
          initonly static array<System::String ^> ^ oBooleanProperties =
            gcnew array<System::String ^> {
            "autoscale",
              "canadjustbeyondoperatingrange",
              "canupdate",
              "excludemissing",
              "faceplatetickvisibility",
              "glasseffect",
              "highbrushvisibility",
              "highhighbrushvisibility",
              "isadjustable",
              "isdashed",
              "labelvisibility",
              "lowbrushvisibility",
              "lowlowbrushvisibility",
              "persist",
              "pivot",
              "scalevisibility",
              "show",
              "showbarvalues",
              "showborder",
              "showdatapoints",
              "showdropshadow",
              "showgridlines",
              "showhighhighvalue",
              "showhighvalue",
              "showlabel",
              "showlegendsbar",
              "showlowlowvalue",
              "showlowvalue",
              "showminorticks",
              "shownavigationbar",
              "showparametersbar",
              "showpointlabels",
              "showsetpoints",
              "showtoolbar",
              "showvalue",
              "showvalues",
              "speedometervalueontop",
              "titlevisibility",
              "total",
              "useactualdate",
              "valuevisibility"
          };
          initonly static array<System::String ^> ^ oDateTimeProperties =
            gcnew array<System::String ^> {
              "enddate",
              "sampleend",
              "samplestart",
              "startdate"
          };
          //
          fProperty( System::String ^ sName_ ) :sName( sName_ ) {}
          //
          property System::String ^ Name
          {
            virtual System::String ^ get() { return sName; }
          }
          property System::Object ^ rowValue
          {
            virtual System::Object ^ get() = 0;
            virtual void set( System::Object^ );
          }
          //
          static System::String ^ Property_to_String( aCONF::C_Project_Node ^ pNode, System::String ^ sPropertyName );

          public:
            static void Add_HMI_Property(System::Xml::XmlAttribute ^ pAttr, System::Collections::Generic::IDictionary<System::String^, IProperty^> ^ pProperties);
            static void Set_Element_Properties(IElement ^ pElement, System::Xml::XmlNode ^ const pXmlNode);
        }; // ....................................... fProperty .............................


        //
        // fPropertyString
        //
        private ref struct fPropertyString sealed : IPropertyString, fProperty
        {
          System::String ^ sValue;
          //
          fPropertyString( System::String ^ sName_, System::String ^ sValue_ ) :sValue( sValue_ ), fProperty( sName_ ) { }
          fPropertyString( System::String ^ sName_, aCONF::C_Project_Node ^ pNode, System::String ^ sPropertyName );
          //
          property System::String ^ Value
          {
            virtual System::String ^ get() { return sValue; }
            virtual void set( System::String^ sNew ) 
            { 
              rowValue = sNew; // this operation will throw if setter is not set up 
              sValue = sNew;  
            }
          }
          //
          property System::Object ^ rowValue
          {
            virtual System::Object ^ get() override { return sValue; }
          }


        }; // .............................................. fPropertyString .......................

        //
        // fPropertyBool
        //
        private ref struct fPropertyBool sealed : IPropertyBool, fProperty
        {
          bool bValue;
          //
          fPropertyBool(System::String ^ sName_) :fProperty(sName_) { }
          fPropertyBool(System::String ^ sName_, bool bValue_) : fProperty(sName_), bValue(bValue_) {}
          //
          property bool Value
          {
            virtual bool get() { return bValue; }
            virtual void set(bool bNew)
            {
              bValue = bNew;
            }
          }
          //
          property System::Object ^ rowValue
          {
            virtual System::Object ^ get() override { return bValue; }
          }


        }; // .............................................. fPropertyString .......................


        //
        // fPropertyInt
        //
        private ref struct fPropertyInt sealed : IPropertyInt, fProperty
        {
          System::Nullable<int>    oValue;
          //
          fPropertyInt( System::String ^ sName_ ) :fProperty( sName_ ) { }
          fPropertyInt(System::String ^ sName_, aCONF::C_Project_Node ^ pNode, System::String ^ sProperty);
          fPropertyInt(System::String ^ sName_, System::Nullable<int> oValue_) : fProperty(sName_), oValue(oValue_) {}
          //
          property System::Nullable<int> Value
          {
            virtual System::Nullable<int> get() { return oValue; }
            virtual void set( System::Nullable<int> oNew );
          }
          //
          property System::Object ^ rowValue
          {
            virtual System::Object ^ get() override;
          }

          //
          static System::Nullable<int> String_to_NInt( System::String ^ sIn );

        }; // .............................................. fPropertyInt .......................

        //
        // fPropertyDouble
        //
        private ref struct fPropertyDouble sealed : IPropertyDouble, fProperty
        {
          System::Nullable<double>    oValue;
          IProjectEU                ^ pEU;
          //
          fPropertyDouble( System::String ^ sName_) :fProperty( sName_ ) { }
          fPropertyDouble( aCONF::C_Project_Node ^ pNode, const aCONF::C_Param_Descr & oDescr );
          fPropertyDouble(System::String ^ sName_, System::Nullable<double> oValue_) : fProperty(sName_), oValue(oValue_) {}
          //
          property System::Nullable<double> Value
          {
            virtual System::Nullable<double> get() { return oValue; }
            virtual void set( System::Nullable<double> oNew );
          }
          //
          property System::Object ^ rowValue
          {
            virtual System::Object ^ get() override; 
          }
          //
          property IProjectEU ^ EU
          {
            virtual IProjectEU ^ get() { return pEU; }
          }
          //
          static System::Nullable<double> String_to_NDouble( System::String ^ sIn );

        }; // .............................................. fPropertyDouble .......................

        //
        // fPropertyBitmap
        //
        private ref struct fPropertyBitmap sealed : fProperty, IPropertyBitmap
        {
          System::Drawing::Bitmap ^ pValue;
          //
          fPropertyBitmap(System::String ^ sName_, System::Drawing::Bitmap ^ pValue_) :fProperty(sName_), pValue(pValue_) { }
          //
          property System::Drawing::Bitmap ^ Value
          {
            virtual System::Drawing::Bitmap ^ get() { return pValue; }
            virtual void set(System::Drawing::Bitmap ^ pNew) { pValue = pNew; }
          }
          //
          property System::Object ^ rowValue
          {
            virtual System::Object ^ get() override { return pValue; }
            virtual void set(System::Object^ pNewValue) override;
          }
          //
          System::String ^ Bitmap_to_String();

        }; // .............................................. fPropertyDouble .......................

        //
        // fPropertyDateTime
        //
        private ref struct fPropertyDateTime sealed : fProperty, IPropertyDateTime
        {
          System::DateTime oValue;
          System::DateTime ^ pValue;
          //
          fPropertyDateTime(System::String ^ sName_, System::DateTime oValue_) :fProperty(sName_), oValue(oValue_) { pValue = %oValue; }
          //
          property System::DateTime Value
          {
            virtual System::DateTime get() { return oValue; }
            virtual void set(System::DateTime oNew) { oValue = oNew; }
          }
          //
          property System::Object ^ rowValue
          {
            virtual System::Object ^ get() override { return pValue; }
            virtual void set(System::Object^ pNewValue) override;
          }

        }; // .............................................. fPropertyDateTime .......................


      } // namespace fDefs
    } // namespace Services
  } // namespace OptiRamp
} // namespace SNC
