#pragma once
//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by: Jeff Shafferman
//-------------------------------------------------------------------

//
// Implementation of IProjectEU
//
#include "..\..\common\Gas\Gas_EU.h"

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace ProjectEUFactory
      {
        ref class fProjectEUFactory sealed abstract
        {
        public:
          static SNC::OptiRamp::Services::fDefs::IProjectEU^ CreateInstance(const aGAS::C_EU_Conversion * ceuc);
        };
      }
    }
  }
}