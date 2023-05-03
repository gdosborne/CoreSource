//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Written by:	Alex Novitskiy, Rex Gray
//-------------------------------------------------------------------

//
// Definitions of classes, structures, enums and interfaces for fDBO("Database Optiramp") components.
//
#pragma once

#include "fProject.h"
#include "..\common\fApp.h"
#include "..\..\common\DB\DB_Core.h"
#include "..\..\common\DB\DB_Main.h"
#include "..\..\common\DB\Utils_DB.h"
#include "..\..\common\DM\Config_Set_Data_Mining.h"
#include "..\..\common\DM\DM_Sequencer.h"
#include "..\..\common\Recorder\TV_Write_DB.h"
#include "..\..\common\Utils_Main_fC.h"
#include "..\..\common\Utils_FileTime.h"
#include "..\..\common\Utils_LocalTime.h"

#include <crtdbg.h>

using namespace SNC::OptiRamp::Services::fDefs;

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace fDatabase
      {
        //
        // fDboEventInfo
        //
        public ref class fDboEventInfo : IDboEventInfo
        {
        private:
          System::String ^ sMessage;
          bool     bError;
          Object ^ oData;
        
        public:
          virtual property System::String^ message
          {
            System::String^ get() { return sMessage; }
            void set(System::String^ value) { sMessage = value; }
          }
          virtual property bool error
          {
            bool get() { return bError; }
            void set(bool value) { bError = value; }
          }
          virtual property Object^ data
          {
            Object^ get() { return oData; }
            void set(Object^ value) { oData = value; }
          }
        }; // ............................... fDboEventInfo ........................
        
        //
        // fDboConnection
        //
        public ref class fDboConnection sealed : IDBConnection
        {
        private:
          IProject                    ^ pProject;
          IElement                    ^ pDatabase;
          aDB::C_DB_Main              ^ pDB_Main;
          aDB::C_DB_Main::C_Options     tDB_Options;
          System::String              ^ sActiveComputer;
          System::String              ^ sDatabasePath;

        public:
          virtual property IProject^ project
          {
            IProject ^ get() { return pProject; }
          }
          virtual property IElement^ database
          {
            IElement ^ get() { return pDatabase; }
          }
          property bool connectionOpen
          {
            bool get() { return pDB_Main != nullptr; }
          }
        internal:
          property aDB::C_DB_Main ^ DBmain
          {
            aDB::C_DB_Main ^ get() { return pDB_Main; }
          }
        public:
          //
          fDboConnection(
             IProject                         ^ project_, 
             IElement                         ^ database_, 
             fDiagnostics::OptiRampLog        ^ log_, 
             System::String                   ^ appName_,
             fDiagnostics::IThreadsWatchdog   ^ pThreadsWatchdog   // can be null
             );
          //
          fDboConnection(
            IProject                        ^ project_,
            IElement                        ^ database_,
            fDiagnostics::OptiRampLog       ^ log_,
            System::String                  ^ appName_,
            int                               iMaxCountDB_,
            fDiagnostics::IThreadsWatchdog  ^ pThreadsWatchdog // can be null
            );
          //
          fDboConnection(
            System::String                  ^ sActiveComputer_,
            System::String                  ^ sDatabasePath_,
            fDiagnostics::OptiRampLog       ^ log_,
            System::String                  ^ appName_,
            fDiagnostics::IThreadsWatchdog  ^ pThreadsWatchdog // can be null
            );

          // -------------------------  PUBLIC INTERFACE FUNCTIONS -----------------------
          virtual bool Open([System::Runtime::InteropServices::Out] System::String ^% sError); // can fire NotificationEvent
          //
          virtual void Close() { Close_ex(); }

        public:
          virtual event DboNotifyHandler^ notificationEvent;

        protected:
          //
          !fDboConnection() { Close_ex(); }
          //
          ~fDboConnection() { this->!fDboConnection(); }

        private:
          void Notify( fDboEventInfo^ ei );
          void ErrorNotification(System::String ^ const sError);
          void Close_ex();
          //
          void Construct(
            IProject                        ^ project_,
            IElement                        ^ database_,
            fDiagnostics::OptiRampLog       ^ log_,
            System::String                  ^ appName_,
            int                               iMaxCountDB_,
            fDiagnostics::IThreadsWatchdog  ^ pThreadsWatchdog 
            );
        }; // ............................... fDboConnection ........................

        //
        // fDboWriter
        //
        public ref class fDboWriter sealed :  SNC::OptiRamp::Services::fDatabase::IDBOWriter,
                                              SNC::OptiRamp::Services::fDatabase::ICADatabase
        {
        private:
          fDboConnection ^ pConnection;
          System::String ^ sTablePrefix;
          System::Collections::Generic::IEnumerable<IElement ^> ^ pElements;
          System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fDatabase::ICADataBuffer ^> ^ pBufferData;
          aTV_REC::I_DB_WriteChannels ^ pWriter;
          aTV_REC::C_DB_Channels oChannels;

        public:
          //
          // ctor
          //
          fDboWriter(
            fDboConnection                                          ^ connection_,
            System::String                                          ^ sRuntimeTag,
            System::Collections::Generic::IEnumerable<IElement ^>   ^ elements_
            );
          //
          // ctor
          //
          fDboWriter(
            fDboConnection                                             ^ connection_,
            System::String                                             ^ sRuntimeTag,
            System::Collections::Generic::IEnumerable<IElement ^>      ^ elements_,
            System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fDatabase::ICADataBuffer ^> ^ pBufferData_
            );
          //
          // ctor
          //
          fDboWriter(
            fDboConnection ^ connection_,
            System::String ^ sRuntimeTag
            );

          // -------------------------  PUBLIC INTERFACE FUNCTIONS -----------------------
          //
          virtual bool WriteWebChannel(
            IElement                                                ^ pElement, 
            SNC::OptiRamp::Services::fWeb::IWebChannel              ^ pWebChannel, 
            SNC::OptiRamp::Services::fRT::RTData                    ^ pData, 
            [System::Runtime::InteropServices::Out] System::String  ^% sError
            );
          //
          virtual bool WriteWebChannelBlocking(
            IElement                                                  ^ pElement,
            SNC::OptiRamp::Services::fWeb::IWebChannel                ^ pWebChannel,
            SNC::OptiRamp::Services::fRT::RTData                      ^ pData,
            bool                                                      % bStop,
            [ System::Runtime::InteropServices::Out ] System::String  ^% sError
            );
          //
          virtual bool WriteWebChannels(
            IElement                                                                          ^ pElement, 
            System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fRT::RTValue>  ^ pData, 
            [System::Runtime::InteropServices::Out] System::String                            ^% sError);
          //
          virtual bool WriteWebChannelsBlocking(
            IElement                                                                          ^ pElement,
            System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fRT::RTValue>  ^ pData,
            bool                                                                              % bStop,
            [ System::Runtime::InteropServices::Out ] System::String                          ^% sError );
          //
          virtual bool WriteNcWebChannel(
            IElement                                                ^ pElement,
            SNC::OptiRamp::Services::fWeb::IWebChannel              ^ pWebChannel,
            SNC::OptiRamp::Services::fRT::RTDataString              ^ pData,
            SNC::OptiRamp::Services::fRT::ApplicationInfo           ^ pAppInfo,
            [System::Runtime::InteropServices::Out] System::String ^% sError);
          //
          virtual bool WriteNotification(
            NotificationDescriptor                                 ^  pNdescriptor,
            [System::Runtime::InteropServices::Out] System::String ^% sError);
          //
          virtual System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fDatabase::IDboEventInfo^> ^ GetEvents(
            System::DateTime T1_UTC,
            System::DateTime T2_UTC,
            [System::Runtime::InteropServices::Out] System::String ^% sError)
          {
            throw gcnew System::NotImplementedException("fDboWriter does not handle GetEvents method of ICADatabase.");
          }
          //
          virtual System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fDatabase::CADescriptor^> ^ ReadCADescriptors(
            int channelID,
            System::DateTime T1_UTC,
            System::DateTime T2_UTC,
            [System::Runtime::InteropServices::Out] System::String ^% sError)
          {
            throw gcnew System::NotImplementedException("fDboWriter does not handle ReadCADescriptors method of ICADatabase.");
          }
          //
          virtual System::Data::DataSet ^ ReadCAData(
            SNC::OptiRamp::Services::fDatabase::CADescriptor ^pDescriptor,
            [System::Runtime::InteropServices::Out] System::String ^% sError)
          {
            throw gcnew System::NotImplementedException("fDboWriter does not handle ReadCAData method of ICADatabase.");
          }
          //
          virtual bool WriteCAData(
            SNC::OptiRamp::Services::fDatabase::CADescriptor ^pDescriptor,
            System::IO::MemoryStream ^pBlob,
            [System::Runtime::InteropServices::Out] System::String ^% sError);

          //
          // thread safe, blocking
          //
          virtual bool WriteStreamToBlob(
            System::String            ^ tableName,
            System::String            ^ dataName,
            System::DateTime            timeStampUTC,
            System::IO::MemoryStream  ^ pBlob,
            System::String          ^ % error
            );

        public:
          virtual event DboNotifyHandler ^ notificationEvent;
          void ErrorNotification(System::String ^ sError);

        protected:
          !fDboWriter();
          ~fDboWriter() { this->!fDboWriter(); }

        private:
          void Notify(fDboEventInfo^ ei);
          void CreateWriteChannels(System::String ^ const sTablePrefix, System::Collections::Generic::IEnumerable<IElement ^> ^ pElements);
          void OnDisposeWriteChannels(System::Object ^ sender, System::EventArgs ^ e);
          //
          bool WriteWebChannelEx(
            IElement                                    ^ pElement,
            SNC::OptiRamp::Services::fWeb::IWebChannel  ^ pWebChannel,
            SNC::OptiRamp::Services::fRT::RTData        ^ pData,
            const volatile bool                         * pCaller_Stop,
            [ System::Runtime::InteropServices::Out ] System::String ^% sError
            );
          //
          bool WriteWebChannelsEx(
            IElement                                    ^ pElement,
            System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fRT::RTValue> ^ pData,
            const volatile bool                         * pCaller_Stop,
            [ System::Runtime::InteropServices::Out ] System::String  ^% sError 
            );

        }; // ............................... fDboWriter ........................

        //
        // fDataMining
        //
        public ref class fDataMining sealed
        {
          fDboConnection ^ pConnection;
          __int64          iLast_Nudge;
          System::Collections::Generic::IEnumerable < IElement^ > ^ pData_Mining_Elements;

        internal:
          aDM::I_Data_Mining ^ pData_Mining;

          // ------------------------- PUBLIC INTERFACE FUNCTIONS -----------------------
        public:
          //
          // ctor
          // 
          fDataMining(
            fDboConnection                                          ^ connection_,
            System::Collections::Generic::IEnumerable < IElement^ > ^ pPotentialElements_,
            System::String                                          ^ sRuntimeTag_ // from ctSupported_Runtimes
            );
          //
          void Start();
          //
          property System::Collections::Generic::IEnumerable < IElement^ > ^ Data_Mining_Elements
          {
            System::Collections::Generic::IEnumerable < IElement^ > ^ get() { return pData_Mining_Elements; }
          }
        private:
          //
          void SignalNewValue(
            aCONF::C_Project_Node    ^ pNode,
            int                        iWebChannel,
            __int64                    iTime_UTC,
            System::Nullable<double>   tValue
            );

          //
          value struct C_Node_Channel sealed
          {
            aCONF::C_Project_Node ^ pNode; int iWebChannel;
          };
          //
          System::Collections::Generic::Dictionary<C_Node_Channel, fRT::RTValue> oTranslate_Nodes;
        public:
          //
          delegate void DMNotifyHandler( fRT::RTValue tValue );
          
          // 
          event DMNotifyHandler ^ notificationData;

        protected:
          !fDataMining();
          ~fDataMining() { this->!fDataMining(); }

        }; // ............................... fDataMining ........................
      } // namespace fDefs
    } // namespace Services
  } // namespace OptiRamp
} // namespace SNC
