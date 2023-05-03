#include "fOPCGroup.h"
#include "fOPCClient.h"
#include "fOPCItem.h"
#include "..\..\common\Utils_String.h"

using namespace System;
using namespace System::Runtime::InteropServices;

using namespace SNC::OptiRamp::Services::fRT;
using namespace SNC::OptiRamp::Services::fOPC;

//
// ctor
//
fOPCGroup::fOPCGroup(fOPCClient^ client_) : client(client_)
{
}

//
// AddItem
//
IRTItem^ fOPCGroup::AddItem(
  System::String                                                ^ itemName,
  [ System::Runtime::InteropServices::Out ] System::String    ^ % error
  )
{
  return AddItemEx( itemName, false, error );
} // .............................. AddItem ....................

//
// AddItemStr
//
IRTItem^ fOPCGroup::AddItemStr(
  System::String                                                ^ itemName,
  [ System::Runtime::InteropServices::Out ] System::String    ^ % error
  )
{
  return AddItemEx( itemName, true, error );
} // .............................. AddItemStr ....................


//
// AddItemEx
//
IRTItem^ fOPCGroup::AddItemEx(
  System::String                                                ^ itemName,
  bool                                                            bIsString,
  [ System::Runtime::InteropServices::Out ] System::String    ^ % error
  )
{
  fOPCItem^ item;
  error = nullptr;
  try
  {
    item = gcnew fOPCItem( client, bIsString );
    client->addItem( itemName, item );
  } catch ( Exception^ ex )
  {
    delete item; item = nullptr;
    error = ex->Message;
  }

  return item;
} // .............................. AddItem ....................