#include "fOPCItem.h"
#include "fOPCClient.h"
#include "..\..\common\Utils_Monitor.h"

using namespace System;
using namespace System::Runtime::InteropServices;

using namespace SNC::OptiRamp::Services::fOPC;

fOPCItem::fOPCItem( 
  fOPCClient^ client_, bool bIsString_ 
  ) :client( client_ ), ID( ++iNextItemID ), bIsString( bIsString_ ), bActive(true)
{
}

void fOPCItem::Write(double value, [Out] String^% error)
{
  error = nullptr;

  client->writeValue(ID, value, error);
}

void fOPCItem::Write(String^ value, [Out] String^% error)
{
  error = nullptr;

  client->writeValue(ID, value, error);
}

//
// LastValue
//
bool fOPCItem::LastValue([Out] System::String ^ % value, [Out] System::DateTime % time, [Out] __int64 % quality)
{
  C_Monitor oLock( % oProtect );
  if (bHas_Value)
  {
    value = sLast_Value;
    time = tLast_Time;
    quality = iLast_Quality;
    return true;
  } else {
    return false;
  }
} // ................................ LastValue .........................

//
// Active::get
//
bool fOPCItem::Active::get()
{
  return bActive;
}

//
// Active::set
//
void fOPCItem::Active::set( bool bNew )
{
  bActive = bNew;

}
