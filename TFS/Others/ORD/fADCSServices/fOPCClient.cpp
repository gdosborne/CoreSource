#include "fOPCClient.h"
#include "fOPCGroup.h"
#include "fOPCItem.h"
#include "..\..\common\Utils_Main.h"
#include "..\..\common\Utils_Main_fC.h"
#include "..\..\common\Utils_String.h"
#include "..\..\common\Utils_String_Ex.h"
#include "..\..\common\Utils_Monitor.h"
#include "..\..\common\DA\DA_Check_Tag.h"

#include <comutil.h>

// .Net namespaces
using namespace System;
using namespace System::Diagnostics;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

// Custom namespaces
using namespace aDA;
using namespace aDS;
using namespace aFW;
using namespace aOPC;
using namespace SNC::OptiRamp::Services::fRT;
using namespace SNC::OptiRamp::Services::fOPC;



// C++ namespace
using namespace std;


fOPCClient::fOPCClient(fOPCEngine^ engine_, String^ MachineName, String^ ServerName) : engine(engine_)
{
  opcItemList = gcnew C_opcItemList;

  wstringstream logFile;
  try
  {
    client = new C_DA_Client(logFile, engine->engine, String_to_wstring(MachineName), String_to_wstring(ServerName));

    Dump_stringstream(logFile);

    Trace::WriteLine(Now_for_Log() + " OPC Client Initialized");

    addToEngineClientList();
  }
  catch (Exception^ ex)
  {
    Dump_stringstream(logFile);

    String^ error = Now_for_Log() + String::Format(" fOPCClient::fOPCClient Failed{0}{1}", Environment::NewLine, ex->Message);

    Trace::WriteLine(error);

    delete this;
    throw;
  }
}

fOPCClient::!fOPCClient()
{
  aFW::C_Start_Stop_Trace oTrace(__FUNCTION__);

  _ASSERT(!aFW::Is_GC_Finalizer_Thread());
  removeOPCItemFromEngineDictionary();

  removeFromEngineClientList();

  delete client;
  client = nullptr;
}

fOPCClient::~fOPCClient() {  this->!fOPCClient(); }

IRTGroup^ fOPCClient::OpenGroup(String^ OPCgroupName, int msecRefreshRate, [Out] String^% error)
{
  wstringstream logFile;
  String^ errorMessage = nullptr;

  try
  {
    groupID = client->Add_Group(logFile, String_to_string(OPCgroupName), msecRefreshRate, String_to_wstring(OPCgroupName));

    Dump_stringstream(logFile);

    Trace::WriteLine(Now_for_Log() + " OPC Group Added");

    return gcnew fOPCGroup(this);
  }
  catch (const exception & ex)
  {
    errorMessage = gcnew String(ex.what());
  }
  catch (Exception^ ex)
  {
    errorMessage = ex->Message;
  }

  Dump_stringstream(logFile);

  String^ fileError = Now_for_Log() + String::Format(" fOPCClient::openGroup Failed with Exception{0}{1}", Environment::NewLine, errorMessage);

  Trace::WriteLine(fileError);

  error = errorMessage;

  delete this;
  return nullptr;
} // ........................................... OpenGroup ..................................


void fOPCClient::addItem(String^ OPCItemName, fOPCItem^ OPCItem)
{

  const int itemID = OPCItem->ID;


  try
  {
    const std::string sName = String_to_string( OPCItemName );
    std::string sBuf;
    if (!aDA::Check_Tag_For_Incorrect_Symbols(sName, sBuf))
    {
      Trace::WriteLine( System::String::Format( "!!! Warning: OPC tag {0} !!!", string_to_String(sBuf)) );

    }

    client->Add_Item(groupID, reinterpret_cast<void*>(OPCItem->ID), sName, engine->GetItemCallBack());

    {
      C_Monitor oLock(% engine->opcItemDictionary);
      engine->opcItemDictionary.Add(itemID, OPCItem);
    }

  }
  catch (const exception & e)
  {
    String^ errorMessage = gcnew String(e.what());

    String^ error = Now_for_Log() + String::Format(" fOPCClient::addItem Failed with std::exception{0}{1}", Environment::NewLine, errorMessage);

    Trace::WriteLine(error);

    throw gcnew Exception(errorMessage);
  }
  catch (Exception^ ex)
  {
    String^ error = Now_for_Log() + String::Format(" fOPCClient::addItem Failed with Exception{0}{1}", Environment::NewLine, ex->Message);

    Trace::WriteLine(error);

    throw gcnew Exception(ex->Message);
  }
}

void fOPCClient::writeValue(int id, double value, String^% error)
{
  VARIANT var;
  var.dblVal = value;
  var.vt = VT_R8;

  string stdErrorStr;

  if (!client->Item_Write_Value_Synch(reinterpret_cast<void*>(id), var, stdErrorStr))
  {
    error = gcnew String(stdErrorStr.c_str());
  }
}

//
// writeValue
//
void fOPCClient::writeValue(int id, String^ value, String^% error)
{
  _variant_t tVar;
  tVar = String_to_wstring( value ).c_str();

  string stdErrorStr;

  if ( !client->Item_Write_Value_Synch( reinterpret_cast<void*>(id), tVar, stdErrorStr ) )
  {
    error = gcnew String(stdErrorStr.c_str());
  }
}

void fOPCClient::addToEngineClientList()
{
  C_Monitor oLock(engine->Clients);
  engine->Clients->Add(this);
}

void fOPCClient::removeOPCItemFromEngineDictionary()
{
  if (opcItemList != nullptr)
  {
    for each (fOPCItem^ item in opcItemList)
    {
      C_Monitor oLock(% engine->opcItemDictionary);
      engine->opcItemDictionary.Remove(item->ID);
    }

    opcItemList->Clear();
  }
}

void fOPCClient::removeFromEngineClientList()
{
  C_Monitor oLock(engine->Clients);
  engine->Clients->Remove(this);
}