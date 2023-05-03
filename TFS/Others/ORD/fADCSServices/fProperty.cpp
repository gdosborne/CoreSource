#include "fProperty.h"
#include "fProjectEUFactory.h"
#include "..\..\common\Utils_Array.h"
#include "..\..\common\Utils_NET_XML.h"
#include "..\..\common\Utils_Nullable.h"
#include "..\..\common\Utils_String.h"
#include <crtdbg.h>

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace fDefs
      {

        //
        // *** C_Setter_Reflection_Prop ***
        //
        // ctor
        //
        C_Setter_Reflection_Prop::C_Setter_Reflection_Prop(
          aCONF::C_Project_Node ^ pTarget_,
          System::String        ^ sTargetProperty_
          ) :pTarget( pTarget_ ), sTargetProperty( sTargetProperty_ )
        { 
          _ASSERT( !System::String::IsNullOrEmpty( sTargetProperty ) );
        }

        //
        // Set
        //
        void C_Setter_Reflection_Prop::Set( System::String ^ sNewValue )
        {
          // to do !!!
          throw gcnew System::NotImplementedException;
        } // ................................. Set ..................................


        //
        // *** C_Setter_Tag_Value ****
        //
        // ctor
        //
        C_Setter_Tag_Value::C_Setter_Tag_Value(
          aCONF::C_TagValues    ^ pTarget_,
          System::String        ^ sTag_
          ) :pTarget( pTarget_ ), sTag( sTag_ )
        {
          _ASSERT( !System::String::IsNullOrEmpty( sTag ) );
          _ASSERT( pTarget != nullptr );
        }

        //
        //
        //
        void C_Setter_Tag_Value::Set( System::String ^ sNewValue )
        {
          if ( System::String::IsNullOrEmpty( sNewValue ) )
          {
            pTarget->Remove( sTag );
          } else {
            pTarget[ sTag ] = sNewValue;
          }
        }


        //
        // *** fProperty ***
        //
        // rowValue::set
        //
        void fProperty::rowValue::set( System::Object ^ sObj ) 
        { 
          if ( pSetter == nullptr )
          {
            throw gcnew System::NotImplementedException;
          } else {
            System::String ^ sValue = (sObj == nullptr) ? nullptr : sObj->ToString();
            pSetter->Set( sValue );
          }
        } // .................................... rowValue::set ............................

        //
        // Property_to_String
        //
        System::String ^ fProperty::Property_to_String( aCONF::C_Project_Node ^ pNode, System::String ^ sPropertyName )
        {
          _ASSERT( pNode != nullptr );
          _ASSERT( !System::String::IsNullOrEmpty( sPropertyName ) );

          System::Object ^ const pBuf = pNode->GetType()->InvokeMember( Property_Name( sPropertyName ),
            System::Reflection::BindingFlags::Public |
            System::Reflection::BindingFlags::NonPublic |
            System::Reflection::BindingFlags::Instance |
            System::Reflection::BindingFlags::GetProperty,
            nullptr,
            pNode,
            nullptr );
          return (pBuf == nullptr) ? nullptr : pBuf->ToString();
        } // ........................................... Property_to_String .........................


        //
        // *** fPropertyString ****
        //
        // ctor
        //
        fPropertyString::fPropertyString( 
          System::String ^ sName_, aCONF::C_Project_Node ^ pNode, System::String ^ sPropertyName
          ) :fProperty( sName_ )
        {
          sValue = Property_to_String( pNode, sPropertyName );
        }

        //
        // *** fPropertyInt ****
        //
        fPropertyInt::fPropertyInt( 
          System::String ^ sName_, aCONF::C_Project_Node ^ pNode, System::String ^ sProperty 
          ) :fProperty( sName_ )
        {
          oValue = String_to_NInt( Property_to_String( pNode, sProperty ) );
        }

        //
        // String_to_NInt
        //
        System::Nullable<int> fPropertyInt::String_to_NInt( System::String ^ sIn )
        {
          System::String ^ sError;
          System::Nullable<int> oRet;
          if ( !System::String::IsNullOrEmpty( sIn ) && !String_to_Nullable( oRet, sError, sIn ) )
          {
            _ASSERT( false );
          }
          return oRet;
        }

        // Value::set
        //
        void fPropertyInt::Value::set( System::Nullable<int> oNew )
        {
          rowValue = Nullable_to_String( oNew );
          oValue = oNew;
        }

        //
        // rowValue::get
        //
        System::Object ^ fPropertyInt::rowValue::get()
        {
          return Nullable_to_String( oValue );
        }


        //
        // *** fPropertyDouble ***
        //
        // ctor
        //
        fPropertyDouble::fPropertyDouble( 
          aCONF::C_Project_Node ^ pNode, const aCONF::C_Param_Descr & oDescr
          ):fProperty( string_to_String( oDescr.sName ) )
        {
          pEU = SNC::OptiRamp::Services::ProjectEUFactory::fProjectEUFactory::CreateInstance( oDescr.pConv );

          oValue = String_to_NDouble( Property_to_String( pNode, string_to_String( oDescr.sProperty ) ) );
        }

        //
        // String_to_NDouble
        //
        System::Nullable<double> fPropertyDouble::String_to_NDouble( System::String ^ sIn )
        {
          System::String ^ sError;
          System::Nullable<double> oRet;
          if ( !System::String::IsNullOrEmpty(sIn) && !String_to_Nullable( oRet, sError, sIn ) )
          {
            _ASSERT( false );
          }
          return oRet;
        }

        // Value::set
        //
        void fPropertyDouble::Value::set( System::Nullable<double> oNew )
        {
          rowValue = Nullable_to_String( oNew );
          oValue = oNew;
        }

        //
        // rowValue::get
        //
        System::Object ^ fPropertyDouble::rowValue::get()
        {
          return Nullable_to_String( oValue );
        }


        //
        // *** fPropertyBitmap ***
        //
        // Bitmap_to_String
        //
        System::String ^ fPropertyBitmap::Bitmap_to_String()
        {
          if (pValue == nullptr) return nullptr;
          System::IO::MemoryStream oStream;
          pValue->Save(% oStream, System::Drawing::Imaging::ImageFormat::Png);
          return System::Convert::ToBase64String(oStream.ToArray(), System::Base64FormattingOptions::InsertLineBreaks);
        }

        //
        // rowValue::set
        //
        void fPropertyBitmap::rowValue::set(System::Object ^ pNewValue)
        {
          System::Drawing::Bitmap ^ pNew = dynamic_cast<System::Drawing::Bitmap ^>(pNewValue);
          pValue = (pNew != nullptr) ? pNew : nullptr;
        }


        //
        // *** fPropertyDateTime ***
        //
        // rowValue::set
        //
        void fPropertyDateTime::rowValue::set(System::Object ^ pNewValue)
        {
          System::DateTime ^ pNew = dynamic_cast<System::DateTime ^>(pNewValue);
          pValue = (pNew != nullptr) ? pNew : nullptr;
        }

        //
        // Add_HMI_Property
        //
        void fProperty::Add_HMI_Property(System::Xml::XmlAttribute ^ pAttr, System::Collections::Generic::IDictionary<System::String^, IProperty^> ^ pProperties)
        {
          _ASSERT(pAttr != nullptr);

          System::String ^ sAttrName = pAttr->Name;
          System::String ^ sAttrValue = pAttr->Value;
          System::String ^ sXlatName = fElementType::XlatTypeID(sAttrName);

          if (System::String::CompareOrdinal(sAttrName, "location") == 0)
          {
            //  location = "{X=140,Y=125}"
            array<System::String ^> ^ sTemp = sAttrValue->Split(oMainSeparators);
            _ASSERT(Safe_Length(sTemp) == 4);

            array<System::String ^> ^ sX = sTemp[1]->Split(oEqualSeparator);
            _ASSERT(Safe_Length(sX) == 2);

            int iValue;
            bool bResult = int::TryParse(sX[1], iValue);
            System::Nullable<int> iX = bResult ? iValue : System::Nullable<int>();
            pProperties->Add(fRT::TypeIDs::X, gcnew fPropertyInt(fRT::TypeIDs::X, iX));

            array<System::String ^> ^ sY = sTemp[2]->Split(oEqualSeparator);
            _ASSERT(Safe_Length(sY) == 2);

            bResult = int::TryParse(sY[1], iValue);
            System::Nullable<int> iY = bResult ? iValue : System::Nullable<int>();
            pProperties->Add(fRT::TypeIDs::Y, gcnew fPropertyInt(fRT::TypeIDs::Y, iY));
          }
          else if (System::String::CompareOrdinal(sAttrName, "size") == 0)
          {
            //  size = "{Width=512, Height=24}"
            array<System::String ^> ^ sTemp = sAttrValue->Split(oMainSeparators);
            _ASSERT(Safe_Length(sTemp) == 4);

            array<System::String ^> ^ sWidth = sTemp[1]->Split(oEqualSeparator);
            _ASSERT(Safe_Length(sWidth) == 2);

            int iValue;
            bool bResult = int::TryParse(sWidth[1], iValue);
            System::Nullable<int> iWidth = bResult ? iValue : System::Nullable<int>();
            pProperties->Add(fRT::TypeIDs::WIDTH, gcnew fPropertyInt(fRT::TypeIDs::WIDTH, iWidth));

            array<System::String ^> ^ sHeight = sTemp[2]->Split(oEqualSeparator);
            _ASSERT(Safe_Length(sHeight) == 2);
            System::String ^ sHeightName = sHeight[0]->Trim();

            bResult = int::TryParse(sHeight[1], iValue);
            System::Nullable<int> iHeight = bResult ? iValue : System::Nullable<int>();
            pProperties->Add(fRT::TypeIDs::HEIGHT, gcnew fPropertyInt(fRT::TypeIDs::HEIGHT, iHeight));
          }
          else if (System::String::CompareOrdinal(sAttrName, "data") == 0)
          {
            int x = 0;
          }
          else if (System::String::CompareOrdinal(sAttrName, "startendcap") == 0 ||
            System::String::CompareOrdinal(sAttrName, "endendcap") == 0)
          {
            if (System::String::CompareOrdinal(sAttrValue, "Triangle") == 0)
              pProperties->Add(sXlatName, gcnew fPropertyString(sXlatName, "Miter"));
            else if (System::String::CompareOrdinal(sAttrValue, "Flat") == 0)
              pProperties->Add(sXlatName, gcnew fPropertyString(sXlatName, "Butt"));
            else
              pProperties->Add(sXlatName, gcnew fPropertyString(sXlatName, sAttrValue));
          }
          else if (System::String::CompareOrdinal(sAttrName, "value") == 0)
          {
            //if (::Find_String(fProperty::oDoubleTypeValueProperties, sType) < 0)
            //{
            //  pProperties->Add(sXlatName, gcnew fPropertyString(sXlatName, sAttrValue));
            //}
            //else
            {
              double fValue;
              bool bResult = double::TryParse(sAttrValue, fValue);
              //System::Nullable<double> fSize = bResult ? fValue : System::Nullable<double>();
              //pProperties->Add(sXlatName, gcnew fPropertyDouble(sXlatName, fSize));
              if (!bResult)
              {
                pProperties->Add(sXlatName, gcnew fPropertyString(sXlatName, sAttrValue));
              }
              else
              {
                System::Nullable<double> fSize = bResult ? fValue : System::Nullable<double>();
                pProperties->Add(sXlatName, gcnew fPropertyDouble(sXlatName, fSize));
              }
            }
          }
          else if (System::String::CompareOrdinal(sAttrName, "fixedtotal") == 0)
          {
            double fValue;
            bool bResult = double::TryParse(sAttrValue, fValue);

            if (!bResult)
            {
              pProperties->Add(sXlatName, gcnew fPropertyString(sXlatName, sAttrValue));
            }
            else
            {
              pProperties->Add(sXlatName, gcnew fPropertyDouble(sXlatName, System::Nullable<double>(fValue)));
            }
          }
          else if (::Find_String(fProperty::oIntegerProperties, sAttrName) >= 0)
          {
            int iVal;
            bool bResult = int::TryParse(sAttrValue, iVal);
            System::Nullable<int> iValue = bResult ? iVal : System::Nullable<int>();
            pProperties->Add(sXlatName, gcnew fPropertyInt(sXlatName, iValue));
          }
          else if (::Find_String(fProperty::oDoubleProperties, sAttrName) >= 0)
          {
            double fVal;
            bool bResult = double::TryParse(sAttrValue, fVal);
            System::Nullable<double> fValue = bResult ? fVal : System::Nullable<double>();

            if (!bResult && ::Find_String(fProperty::oMultiPeerProperties, sAttrName) >= 0)
            {
              pProperties->Add(sXlatName, gcnew fPropertyString(sXlatName, sAttrValue));
            }
            else if (::Find_String(fProperty::oConvertDoubleToIntegerProperties, sAttrName) < 0)
            {
              pProperties->Add(sXlatName, gcnew fPropertyDouble(sXlatName, fValue));
            }
            else
            {
              int iVal;

              if (!fValue.HasValue)
                iVal = 0;
              else
                iVal = static_cast<int>(System::Math::Round(fVal));

              System::Nullable<int> iValue = iVal;
              pProperties->Add(sXlatName, gcnew fPropertyInt(sXlatName, iValue));
            }
          }
          else if (::Find_String(fProperty::oBooleanProperties, sAttrName) >= 0)
          {
            bool bVal;
            bool bResult = bool::TryParse(sAttrValue, bVal);
            bool bValue = bResult ? bVal : false;
            pProperties->Add(sXlatName, gcnew fPropertyBool(sXlatName, bValue));
          }
          else if (::Find_String(fProperty::oDateTimeProperties, sAttrName) >= 0)
          {
            System::DateTime oVal;
            bool bResult = System::DateTime::TryParse(sAttrValue, oVal);
            System::DateTime oValue = bResult ? oVal : System::DateTime::Now;
            pProperties->Add(sXlatName, gcnew fPropertyDateTime(sXlatName, oValue));
          }
          else // String property
          {
            pProperties->Add(sXlatName, gcnew fPropertyString(sXlatName, sAttrValue));
          }
        }

        void fProperty::Set_Element_Properties(IElement ^ pElement, System::Xml::XmlNode ^ const pXmlNode)
        {
          _ASSERT(pElement != nullptr);
          _ASSERT(pXmlNode != nullptr);

          if (pXmlNode->Attributes != nullptr)
          {
            for each (System::Xml::XmlAttribute ^ pAttr in pXmlNode->Attributes)
            {
              fProperty::Add_HMI_Property(pAttr, pElement->Properties);
            }
          }
        }

      } // namespace fDefs
    } // namespace Services
  } // namespace OptiRamp
} // namespace SNC
