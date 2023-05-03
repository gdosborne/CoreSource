#pragma once
//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by: Jeff Shafferman
//-------------------------------------------------------------------

//
// Implementation of fWebChannel
//
#include "fProject.h"
#include "..\..\common\web\Web_HMI_I.h"

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace fWeb
      {
        //
        // fWebChannel
        //
        private ref class fWebChannel sealed : IWebChannel
        {
        public:
          initonly static array<fDefs::IDataChannel^> ^ oEmpty_Data_Channels = gcnew array<fDefs::IDataChannel^>(0);
          initonly static array<IWebChannel^> ^ oEmpty = gcnew array<IWebChannel^>( 0 );
          //
          aCONF::C_Project_Node         ^ const pNode;
          initonly array<aWEB::C_Web_Channel> ^ pChannels;
          const int                             iIdx;

          fWebChannel(aCONF::C_Project_Node^ pNode_, array<aWEB::C_Web_Channel> ^ pChannels_, int iIdx_);

          // ------------------ IDataChannel ------------------
          property System::String ^ Description
          {
            virtual System::String ^ get();
            virtual void set(System::String^ descriptoin_);
          }

          property System::String ^ Name
          {
            virtual System::String ^ get();
            virtual void set(System::String ^ name_);
          }

          property System::String ^ CustomerTag
          {
            virtual System::String ^ get();
            virtual void set(System::String^ customerTag_);
          }

          //
          property System::Nullable<double> Min
          {
            virtual System::Nullable<double> get();
            virtual void set(System::Nullable<double> val);
          }

          property System::Nullable<double> Max
          {
            virtual System::Nullable<double> get();
            virtual void set(System::Nullable<double> val);
          }

          property SNC::OptiRamp::Services::fDefs::IProjectEU^ EU
          {
            virtual SNC::OptiRamp::Services::fDefs::IProjectEU^ get();
            virtual void set(SNC::OptiRamp::Services::fDefs::IProjectEU^ projectEU_);
          }

          property bool NCUType
          {
            virtual bool get();
            virtual void set( bool );
          }

          property bool DiscreteType
          {
            virtual bool get();
            virtual void set( bool );
          }

          // ------------------- IWebChannel ------------------------
          property System::Type   ^ Type
          {
            virtual System::Type ^ get();
          }
        };
      }
    }
  }
}