#include "fProjectEUFactory.h"

// .Net namespaces
using namespace System;
using namespace System::Collections::Generic;
using namespace System::Threading;

// Custom namespaces
using namespace aGAS;
using namespace SNC::OptiRamp::Services::fDefs;
using namespace SNC::OptiRamp::Services::ProjectEUFactory;

namespace ProjectEU
{
  ref class fProjectEU : IProjectEU
  {
  public:
    fProjectEU(C_EU_Conversion const* ceuc);

    property String^ Text
    {
      virtual String^ get();
    }

    property String^ Name
    {
      virtual String^ get();

      virtual void set(String^ name_);
    }

    property IProjectEU^ baseEU
    {
      virtual IProjectEU^ get();
    }

    virtual String^ GetFormattedValue(double value);

  internal:
    static Dictionary< IntPtr, IProjectEU^ > euDictionary;

  private:
    C_EU_Conversion const * ceuConversion;
  };
}

// Newly added namespaces
using namespace ProjectEU;

//
//-------------------- fProjectEUFactory Implementation ------------------
//
IProjectEU^ fProjectEUFactory::CreateInstance(const C_EU_Conversion * ceuc)
{
  if (ceuc != nullptr)
  { 
    IntPtr keyPointer(const_cast<C_EU_Conversion*>(ceuc));
    IProjectEU^ eu = nullptr;

    Monitor::Enter(%fProjectEU::euDictionary);

    try
    {
      if (!fProjectEU::euDictionary.TryGetValue(keyPointer, eu))
      {
        eu = gcnew fProjectEU(ceuc);
        fProjectEU::euDictionary.Add(keyPointer, eu);
      }
    }
    finally
    {
        Monitor::Exit(%fProjectEU::euDictionary);
    }

    return eu;
  }
  
  return nullptr;
}
//
//-------------------- End of fProjectEUFactory Implementation------------
//

//-------------------- fProjectEU Implemntation -------------------------------
ProjectEU::fProjectEU::fProjectEU(C_EU_Conversion const* ceuc) : ceuConversion(ceuc)
{
}

String^ ProjectEU::fProjectEU::Text::get()
{
  return gcnew String(ceuConversion->Get_EU().ExName);
}

String^ ProjectEU::fProjectEU::Name::get()
{
  return gcnew String(ceuConversion->Get_CEU());
}

void ProjectEU::fProjectEU::Name::set(String^ name_)
{
  throw gcnew NotImplementedException;
}

IProjectEU^ ProjectEU::fProjectEU::baseEU::get()
{
    return nullptr;
}

String^ ProjectEU::fProjectEU::GetFormattedValue(double value)
{
  return CValue(ceuConversion, value);
}
//--------------------- End of fProject Implementation -------------------------