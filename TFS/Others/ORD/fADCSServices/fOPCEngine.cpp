#include "fOPCEngine.h"
#include "fOPCClient.h"
#include "fOPCItem.h"
#include "..\..\common\Utils_Main.h"
#include "..\..\common\Utils_Main_fC.h"
#include "..\..\common\Utils_String.h"
#include "..\..\common\Utils_String_Ex.h"
#include "..\..\common\OPC_DA_Client_WT\OPC_Client.h"
#include "..\..\common\OPC\Utils_OPC.h"
#include "..\..\common\Utils_Array.h"
#include "..\..\common\Utils_Monitor.h"
#include "..\..\common\DS\DS_Engine.h"
#include "..\..\common\Threading\ThreadsWatchdogU_fC.h"

#ifdef _DEBUG
#include "..\..\common\Utils.h"
#endif

// .Net namespace
using namespace System;
using namespace System::Diagnostics;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

// Custom interfaces
using namespace aDA;
using namespace aFW;
using namespace aOPC;
using namespace SNC::OptiRamp::Services::fRT;
using namespace SNC::OptiRamp::Services::fOPC;
using namespace SNC::OptiRamp::Services::fDiagnostics;

// C++ standard namespace
using namespace std;

namespace Unmanaged
{
  struct C_OPC_Engine_Unmanaged sealed : DA_Init
  {
    //
    C_OPC_Engine_Unmanaged(wostream &oLogFile) : DA_Init(oLogFile)
    {
      aDS::DS_Add(NULL, this, "OPC");
    }
    //
    ~C_OPC_Engine_Unmanaged()
    {
      aDS::DS_Remove(this);
    }
  };

  struct C_OPC_ItemCallBack_Unmanaged sealed : C_DA_CallBack
  {
    typedef void(__stdcall *F_Call_Back_Managed)(C_DA_Point_ID Point_ID, const VARIANT* pVar, __int64 timestamp, DWORD quality);
    F_Call_Back_Managed pCall_Back_Managed;

    C_OPC_ItemCallBack_Unmanaged() : pCall_Back_Managed(NULL)
    {
    }

    ~C_OPC_ItemCallBack_Unmanaged()
    {
    }

    virtual void Call_Back_Value(C_DA_Point_ID Point_ID, const VARIANT* pVar, __int64 timestamp, DWORD quality)
    {
      F_Call_Back_Managed pBuf = pCall_Back_Managed;

      if (pBuf != NULL)
      {
        pBuf(Point_ID, pVar, timestamp, quality);
      }
      else 
      {
        #ifdef _DEBUG
                const int I = 0;
        #endif
      }
    }
  };
}

using namespace Unmanaged;

//
// static ctor
//
static fOPCEngine::fOPCEngine()
{
  aOPC::DA_Init::iServer_Time_Behaviour = aOPC::DA_Init::eTime_Use_Server;
}

//
// ctor
//
fOPCEngine::fOPCEngine(
  SNC::OptiRamp::Services::fDiagnostics::IOptiRampLog       ^ logService_,
  System::String                                            ^ sAppName,
  SNC::OptiRamp::Services::fDiagnostics::IThreadsWatchdog   ^ pThreadsWatchdog  // can be nullptr
  ) : engine( NULL ), itemCallBack( NULL )
{
  wstringstream logFile;

  try
  {
    connectedClients = gcnew C_connectedClients;

    Log::Register(logService_, sAppName);
    Register_Threads_Watchdog_Engine( pThreadsWatchdog );

    engine = new C_OPC_Engine_Unmanaged(logFile);
    itemCallBack = new C_OPC_ItemCallBack_Unmanaged();
    itemCallBackDelegate = gcnew InteralItemDelegate(this, &fOPCEngine::InternalManagedItemCallBack);

    Dump_stringstream(logFile);

    Trace::WriteLine(Now_for_Log() + " OPC initialized");
  }
  catch (Exception^ ex)
  {
    Dump_stringstream(logFile);

    String^ error = Now_for_Log() + String::Format(" fOPCEngine::fOPCEngine Failed{0}{1}", Environment::NewLine, ex->Message);
    Trace::WriteLine(error);

    delete this;
    throw;
  }
}

IRTConnection^ fOPCEngine::OpenClient(String^ MachineName, String^ OPCserverName, [Out] String^% error)
{
  IOPCConnection^ client = nullptr;
  error = nullptr;

  try
  {
    client = gcnew fOPCClient(this, MachineName, OPCserverName);
  }
  catch (Exception^ ex)
  {
    error = ex->Message;
  }

  return client;
}

void fOPCEngine::Start(bool bSyncRead, [Out] String^% error)
{
  IntPtr IPtr = System::Runtime::InteropServices::Marshal::GetFunctionPointerForDelegate(itemCallBackDelegate);
  itemCallBack->pCall_Back_Managed = static_cast<C_OPC_ItemCallBack_Unmanaged::F_Call_Back_Managed>(IPtr.ToPointer());
  error = nullptr;

  try
  {
    C_OPC_Engine_Unmanaged::Start(bSyncRead);
  }
  catch (Exception ^ ex)
  {
    error = ex->Message;
    aFW::Silent_Exception( ex );
  }
}

void fOPCEngine::Stop([Out] String^% error)
{
  try
  {
    error = nullptr;

    if (itemCallBack != NULL)
    {
      itemCallBack->pCall_Back_Managed = NULL;
    }

    C_OPC_Engine_Unmanaged::Stop();
  }
  catch (Exception^ ex)
  {
    error = Now_for_Log() + String::Format(" fOPCEngine::Stop Failed{0}{1}", Environment::NewLine, ex->Message);

    Trace::WriteLine(error);
  }
}

fOPCEngine::C_connectedClients^ fOPCEngine::Clients::get()
{
  return connectedClients;
}


C_DA_CallBack* fOPCEngine::GetItemCallBack()
{
  return itemCallBack;
}


//
// InternalManagedItemCallBack
//
void fOPCEngine::InternalManagedItemCallBack(C_DA_Point_ID Point_ID, const VARIANT* pVar, __int64 timestamp, DWORD quality)
{
  try
  {
    fOPCItem ^ item;
    {
      C_Monitor oLock( % opcItemDictionary );
      if ( !opcItemDictionary.TryGetValue( reinterpret_cast<int>(Point_ID), item ) )
      {
        item = nullptr;
      }
    }
    if ( item == nullptr)
    {
      _ASSERT( false ); // do we have racing here ?
    }
    DateTime date;
    try
    {
      date = DateTime::FromFileTimeUtc( timestamp );
    } catch ( ... ) {
      date = DateTime::UtcNow;  // fall back
    }

    System::String ^ sNew_Value = nullptr;

    if ( item->bIsString )
    {
      aFW::Set_Thread_Culture();
      const std::wstring sBuf = VARIANT_to_wstring( pVar );
      sNew_Value = string_to_String( sBuf );

      if ( item->bActive)
      {
        RTItemValueStr( item, sNew_Value, date, quality );
      }
    } else {
      double value = 0;
      System::Nullable<double> tValue;
      if (VARIANT_to_double(pVar, value))
      {
        tValue = value;
        sNew_Value = value.ToString();
      }
      if (item->bActive)
      {
        RTItemValue( item, tValue, date, quality );
      }
    }

    { // remember the last value
      C_Monitor oLock( % fOPCItem::oProtect );
      item->bHas_Value = true;
      item->sLast_Value = sNew_Value;
      item->iLast_Quality = quality;
      item->tLast_Time = date;
    }


  } catch ( System::Exception ^ pErr ) {
    SILENT_EXCEPTION( pErr );
  }
} // ........................... InternalManagedItemCallBack ......................

//
// !
// 
fOPCEngine::!fOPCEngine()
{
  try
  {
    aFW::C_Start_Stop_Trace oTrace(__FUNCTION__);
    _ASSERT(!aFW::Is_GC_Finalizer_Thread());

    if (itemCallBack != NULL)
    {
      itemCallBack->pCall_Back_Managed = NULL;
    }


    threadSafeDeleteClients();

    delete itemCallBack;
    itemCallBack = NULL;

    delete engine;
    engine = NULL;
  }
  catch (System::Exception ^ pErr) 
  {
    aFW::Silent_Exception(pErr);
  }
} // .................................... ! .......................................

//
// dtor
//
fOPCEngine::~fOPCEngine() {   this->!fOPCEngine(); }

void fOPCEngine::threadSafeDeleteClients()
{
  while (true)
  {
    fOPCClient ^ pToDelete = nullptr;
    {
      C_Monitor oLock(connectedClients);

      if (connectedClients->Count > 0)
      {
        pToDelete = connectedClients[0];
      }
    }

    if (pToDelete == nullptr) 
    { 
      break; 
    }

    delete pToDelete;
  }
}

//
// GetOPCservers
//
IEnumerable<System::String^> ^ fOPCEngine::GetOPCservers(
  System::String                                              ^ MachineName,
  [ System::Runtime::InteropServices::Out ] System::String  ^ % error
  )
{
  try
  {
    std::string sMachineName;
    if (!System::String::IsNullOrEmpty(MachineName))
    {
      sMachineName = String_to_string( MachineName );
    }

    std::vector<std::string> vList;
    std::string sError;
    if ( !DA_Init::Get_OPC_Servers_List(sMachineName, vList, sError ) )
    {
      _ASSERT( !sError.empty() );
      error = string_to_String( sError );
      return nullptr;
    }

    auto pRet = gcnew System::Collections::Generic::List<System::String^>;
    for ( size_t I = 0; I < vList.size(); ++I )
    {
      pRet->Add( string_to_String( vList[ I ] ) );
    }

    error = nullptr;
    return pRet;

  } catch ( const std::exception & oErr ) {
    error = string_to_String(oErr);
    return nullptr;
  } catch ( System::Exception ^ pErr ) {
    error = pErr->Message;
    return nullptr;
  }
} // ..................................... GetOPCservers ..........................................

