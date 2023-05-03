#pragma once
#include "..\..\common\Config\Config_Project_Tree.h"
#include "fElement.h"

namespace aOIL
{
  //
  // Fill_Rod_Pump_Project_Properties
  //
  void Fill_Rod_Pump_Project_Properties(
    aCONF::C_Project_Node                                             ^ pSource_Node,
    SNC::OptiRamp::Services::fDefs::fElement::C_Element_Properties    ^ pTarget_Properties,
    bool                                                              % bHandled
    );
};