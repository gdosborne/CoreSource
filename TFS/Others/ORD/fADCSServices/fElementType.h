#pragma once
//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by: Jeff Shafferman
//-------------------------------------------------------------------

//
// Implementation of IElemType
//
#include "..\..\common\Config\Config_Project_Tree.h"
#include "..\..\common\HMI\Items_HMI.h"

using namespace SNC::OptiRamp::Services::fDefs;

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace fDefs
      {
        private ref class fElementType : IElemType
        {
        private:
          System::String ^ sTypeName;
          System::String ^ sTypeID;
          typedef System::Collections::Generic::Dictionary<System::String ^, System::String ^> C_XlatTags;
          static C_XlatTags ^ pXlatTags = gcnew C_XlatTags;

        internal:
          fElementType(aCONF::C_Project_Node_Type ^ pNode_Type);
          fElementType(System::String ^ sType);

#ifdef _DEBUG
        public:
          static System::Collections::Generic::List<System::String ^> ^ pUnknowns = gcnew System::Collections::Generic::List<System::String ^>();
#endif // _DEBUG

        public:
          static fElementType();

          property System::String ^ Name
          {
            virtual System::String ^ get();
          }

          property System::String ^ TypeID
          {
            virtual System::String ^ get();
          }

          static void Add(fElementType ^ pElementType);

          static System::String ^ XlatTypeID(System::String ^ sNodeTypeID);

        internal:
          static System::Collections::Generic::List<fElementType^> oElementTypes;
        };
      }
    }
  }
}