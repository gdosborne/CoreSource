//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Written by:	Rex Gray
//-------------------------------------------------------------------

#include "fDatabase.h"
#include "fElement.h"
#include "fWebChannel.h"
#include "fWeb_HMI_Proxy.h"
#include "..\..\common\CC\Config_Set_CC_App.h"
#include "..\..\common\Threading\ThreadsWatchdogU_fC.h"

using namespace System;
using namespace System::Runtime::InteropServices;

using namespace SNC::OptiRamp::Services::fDefs;
using namespace SNC::OptiRamp::Services::fDatabase;
using namespace SNC::OptiRamp::Services::fOPC;
using namespace SNC::OptiRamp::Services::fRT;
using namespace SNC::OptiRamp::Services::fWeb;
using namespace SNC::OptiRamp::Services::fDiagnostics;

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      using namespace fRT;

      namespace fDatabase
      {
        //
        // C_Read_Runtime_Seconds
        //
        ref struct C_Read_Runtime_Seconds sealed : aTV_REC::I_DB_WriteChannels::I_Read_Seconds
        {
          //
          // C_Read_Runtime_Seconds_Iterator
          //
          ref struct C_Read_Runtime_Seconds_Iterator : aTV_REC::I_DB_WriteChannels::I_Read_Seconds_Iterator
          {
            fDboWriter                         ^ pLog;
            System::String                     ^ sChannelName;
            ICADataBuffer                      ^ pRuntimeBuffer;
            System::Nullable<System::DateTime>   oFromTimeUTC;
            System::Nullable<System::DateTime>   oToTimeUTC;
            __int64                              iToTime;
            __int64                              iCurrentTime;
            int                                  iToIndex;
            int                                  iCurrentIndex;
            System::Collections::Generic::List<RTValueDouble> ^ pData;
            //
            // ctor
            //
            C_Read_Runtime_Seconds_Iterator(
              fDboWriter ^ pLog_, 
              System::String ^ sChannelName_, 
              ICADataBuffer ^ pRuntimeBuffer_, 
              System::Nullable<System::DateTime> oFromTimeUTC_, 
              System::Nullable<System::DateTime> oToTimeUTC_
              ): pLog(pLog_), sChannelName(sChannelName_), pRuntimeBuffer(pRuntimeBuffer_), 
                 oFromTimeUTC(oFromTimeUTC_), oToTimeUTC(oToTimeUTC_), 
                 iToTime(0), iCurrentTime(0), iToIndex(0), iCurrentIndex(0), pData(nullptr)
            {
              #ifdef _DEBUG
              _ASSERT( pLog != nullptr );
              _ASSERT(pRuntimeBuffer != nullptr);
              _ASSERT(oToTimeUTC.HasValue);
              if (oFromTimeUTC.HasValue) 
              {
                _ASSERT(oToTimeUTC.Value > oFromTimeUTC.Value);
              }
              #endif // _DEBUG

              iToTime = pRuntimeBuffer->LastBufferTime.Ticks;
              if (oToTimeUTC.HasValue)
              {
                iToTime = __min(iToTime, oToTimeUTC.Value.Ticks);
              }

              iCurrentTime = pRuntimeBuffer->FirstBufferTime.Ticks;
              if (oFromTimeUTC.HasValue)
              {
                iCurrentTime = __max(iCurrentTime, oFromTimeUTC.Value.Ticks);
              }

              System::String ^ sError = System::String::Empty;
              System::Collections::Generic::IEnumerable<RTValueDouble> ^ pData_ = pRuntimeBuffer->GetBuffer(sError);
              if (!System::String::IsNullOrEmpty(sError))
              {
                // do nothing
              }
              else
              {
                pData = gcnew System::Collections::Generic::List<RTValueDouble>( pData_ );
                FindClosestIndex(iCurrentTime, iCurrentIndex);
                FindClosestIndex(iToTime, iToIndex);
              }
              pLog->ErrorNotification(
                System::String::Format("Channel \"{0}\": Total Buffer points {1}."
                , sChannelName
                , Safe_Length( pData )
                ));
              pLog->ErrorNotification(
                System::String::Format("First Buffer Time {0}."
                , pRuntimeBuffer->FirstBufferTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff")
                ));
              pLog->ErrorNotification(
                System::String::Format("Last Buffer Time {0}."
                , pRuntimeBuffer->LastBufferTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff")
                ));
            } // ............................... ctor ........................
            //
            // FindClosestIndex
            // 
            void FindClosestIndex(__int64 ticks, int % index)
            {
              // Find runtime point index closest to ticks.
              if ( pData != nullptr ) for ( int i = 0, cnt = pData->Count; i < cnt; ++i )
              {
                if (pData[i].Timestamp.Ticks >= ticks)
                  break;
                else
                  index = i;
              }
            } // ............................... FindClosestIndex ........................
            //
            // Fetch
            //
            virtual bool C_Read_Runtime_Seconds_Iterator::Fetch(
              const volatile bool % bStop,
              System::DateTime % tTime,
              System::Nullable<double> % tValue)
            {
              if (bStop) { return false; }

              if (   pData == nullptr 
                  || iCurrentIndex < 0
                  || iCurrentIndex >= pData->Count
                  || iCurrentIndex > iToIndex 
                  )
              {
                return false;
              }
              tTime = pData[iCurrentIndex].Timestamp;
              tValue = pData[iCurrentIndex].Value;
              pLog->ErrorNotification(
                System::String::Format("Fetched point for \"{0}\": Timestamp {1}; Value {2}."
                , sChannelName
                , pData[iCurrentIndex].Timestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff")
                , (pData[iCurrentIndex].Value.HasValue ? static_cast<double>(pData[iCurrentIndex].Value).ToString("G6") : "null")
                ));
              iCurrentIndex++;
              return true;
            } // ............................... Fetch ........................
          }; // ............................... C_Read_Runtime_Seconds_Iterator ........................

          fDboWriter ^ pLog;
          System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fDatabase::ICADataBuffer ^> ^ pBufferData;
          //System::Collections::Generic::Dictionary<System::String ^, C_Read_Runtime_Seconds_Iterator ^>  ^ pIterators;

          //
          // ctor
          //
          C_Read_Runtime_Seconds(fDboWriter ^ pLog_,
            System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fDatabase::ICADataBuffer ^> ^ pBufferData_)
            : pLog(pLog_), pBufferData(pBufferData_)
          {
            _ASSERT(pLog != nullptr);
            _ASSERT(pBufferData != nullptr);
          } // ............................... ctor ........................
          //
          // Get_Last_Time
          //
          // finds time of the younger point (if iBorder_Time_UTC !=0 then point time <= than iBorder_Time_UTC)
          //
          virtual bool Get_Last_Time(
            aTV_REC::I_DB_WriteChannels::C_Name_ID tName_ID,
            System::Nullable<System::DateTime>     tBorder_Time_UTC,
            const volatile bool                  % bStop,
            System::DateTime                     % tLast_Time_UTC
            )
          {
            if (bStop)
              return false;

            for each (ICADataBuffer ^ pBuffer in pBufferData)
            {
              if (pBuffer->Channel->Name == tName_ID.sName)
              {
                if (!tBorder_Time_UTC.HasValue)
                {
                  if (pBuffer->LastBufferTime.Year == 1)
                  {
                    pLog->ErrorNotification(
                      System::String::Format("Get_Last_Time for \"{0}\": has a default LastBufferTime {1}."
                      , tName_ID.sName
                      , pBuffer->LastBufferTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff")
                      ));
                    return false;
                  }
                  else
                  {
                    tLast_Time_UTC = pBuffer->LastBufferTime;
                    pLog->ErrorNotification(
                      System::String::Format("Get_Last_Time for \"{0}\": using LastBufferTime {1}."
                      , tName_ID.sName
                      , pBuffer->LastBufferTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff")
                      ));
                    return true;
                  }
                }
                else
                {
                  System::String ^ sError = System::String::Empty;
                  System::Collections::Generic::IEnumerable<RTValueDouble> ^ pData_ = pBuffer->GetBuffer(sError);
                  if (!System::String::IsNullOrEmpty(sError))
                    return false;
                  else
                  {
                    System::Collections::Generic::List<RTValueDouble> ^ pData 
                      = gcnew System::Collections::Generic::List<RTValueDouble>( pData_ );

                    tLast_Time_UTC = tBorder_Time_UTC.Value;
                    for ( int i = 0, cnt = pData->Count; i < cnt; ++i )
                    {
                      if (pData[i].Timestamp.Ticks > tBorder_Time_UTC.Value.Ticks)
                        break;
                      else
                        tLast_Time_UTC = pData[i].Timestamp;
                    }
                    pLog->ErrorNotification(
                      System::String::Format("Get_Last_Time for \"{0}\": using nearest last buffer time {1}."
                      , tName_ID.sName
                      , tLast_Time_UTC.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff")
                      ));
                    return true;
                  }
                } // else if (tBorder_Time_UTC.HasValue)
              } // if (pBuffer->Channel->Name == tName_ID.sName)
            } // for each (ICADataBuffer ^ pBuffer in pBufferData)
            pLog->ErrorNotification(System::String::Format("Get_Last_Time for \"{0}\": did NOT find a last buffer time.", tName_ID.sName));
            return false;
          } // ............................... Get_Last_Time ........................
          //
          // Get_Iterator
          //
          virtual aTV_REC::I_DB_WriteChannels::I_Read_Seconds_Iterator ^ Get_Iterator(
            aTV_REC::I_DB_WriteChannels::C_Name_ID tName_ID,
            System::Nullable<System::DateTime>     tFrom_Time_UTC,
            System::Nullable<System::DateTime>     tTo_Time_UTC
            )
          {
            for each (ICADataBuffer ^ pBuffer in pBufferData)
            {
              if (pBuffer->Channel->Name == tName_ID.sName)
              {
                pLog->ErrorNotification(
                  System::String::Format("Get_Iterator for \"{0}\": From {1} To {2}"
                  , tName_ID.sName
                  , (tFrom_Time_UTC.HasValue ? tFrom_Time_UTC.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff") : "null")
                  , (tTo_Time_UTC.HasValue ? tTo_Time_UTC.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss:fff") : "null")
                  ));
                return gcnew C_Read_Runtime_Seconds_Iterator(pLog, tName_ID.sName, pBuffer, tFrom_Time_UTC, tTo_Time_UTC);
              }
            }
            return nullptr;
          } // ............................... Get_Iterator ........................
        }; // ............................... C_Read_Runtime_Seconds ........................

        //  
        // fDboConnection
        //
        // Construct
        //
        void fDboConnection::Construct(
          IProject                        ^ project_,
          IElement                        ^ database_,
          fDiagnostics::OptiRampLog       ^ log_,
          System::String                  ^ appName_,
          int                               iMaxCountDB_,
          fDiagnostics::IThreadsWatchdog  ^ pThreadsWatchdog
          )
        {
          _ASSERT( project_ != nullptr );
          _ASSERT( database_ != nullptr );
          _ASSERT( log_ != nullptr );
          _ASSERT( !System::String::IsNullOrEmpty( appName_ ) );
          aFW::Log::Register( log_, appName_ );
          fDiagnostics::Register_Threads_Watchdog_Engine( pThreadsWatchdog );

          pProject = project_;
          pDatabase = database_;
          pDB_Main = nullptr;

          tDB_Options.tMode = aDB::C_DB_Connection::eMode_Spin_Month;
          tDB_Options.iMaxCountDB = System::Math::Max( 0, iMaxCountDB_ );

        } // ............................. Construct ............................

        //
        // ctor
        //
        fDboConnection::fDboConnection(
          IProject                        ^ project_,
          IElement                        ^ database_,
          fDiagnostics::OptiRampLog       ^ log_,
          System::String                  ^ appName_,
          int                               iMaxCountDB_,
          fDiagnostics::IThreadsWatchdog  ^ pThreadsWatchdog
          )
        {
          Construct( project_, database_, log_, appName_, iMaxCountDB_, pThreadsWatchdog );
        } // ...................................... ctor .........................

        //
        // ctor
        //
        fDboConnection::fDboConnection(
          IProject                        ^ project_,
          IElement                        ^ database_,
          fDiagnostics::OptiRampLog       ^ log_,
          System::String                  ^ appName_,
          fDiagnostics::IThreadsWatchdog  ^ pThreadsWatchdog
          )
        {
          Construct( project_, database_, log_, appName_, 0, pThreadsWatchdog );
        } // ...................................... ctor .........................

        //
        // ctor
        //
        fDboConnection::fDboConnection(
          System::String                  ^ sActiveComputer_,
          System::String                  ^ sDatabasePath_,
          fDiagnostics::OptiRampLog       ^ log_,
          System::String                  ^ appName_,
          fDiagnostics::IThreadsWatchdog  ^ pThreadsWatchdog
          )
        {
          _ASSERT(sActiveComputer_ != nullptr);
          _ASSERT(sDatabasePath_ != nullptr);
          _ASSERT(log_ != nullptr);
          _ASSERT(!System::String::IsNullOrEmpty(appName_));

          aFW::Log::Register(log_, appName_);
          fDiagnostics::Register_Threads_Watchdog_Engine( pThreadsWatchdog );

          pProject = nullptr;
          pDatabase = nullptr;
          pDB_Main = nullptr;

          sActiveComputer = sActiveComputer_;

          // Require path only. Remove any database file name.
          sDatabasePath = System::IO::Path::HasExtension(sDatabasePath_) ?
            System::IO::Path::GetDirectoryName(sDatabasePath_) :
            sDatabasePath_;

          tDB_Options.tMode = aDB::C_DB_Connection::eMode_Spin_Month;
          tDB_Options.iMaxCountDB = 0;
        } // ...................................... ctor .........................

        //
        // Open
        //
        bool fDboConnection::Open([Out] System::String ^% sError)
        {
          sError = System::String::Empty;

          try
          {
            if (pDB_Main == nullptr)
            {
              if (pProject == nullptr)
              {
                pDB_Main = gcnew aDB::C_DB_Main(sActiveComputer, sDatabasePath, false, nullptr, tDB_Options);
              }
              else
              {
                fElement ^ pComputer = dynamic_cast<fElement ^>(pProject->ActiveComputer);
                if (pComputer == nullptr)
                {
                  sError = System::String::Format("No active computer found. Unable to open connection to {0}", pDatabase->Name);
                }
                else
                {
                  aCONF::C_Project_Node_Computer ^ pActiveComputer = dynamic_cast<aCONF::C_Project_Node_Computer ^>(pComputer->pNode);
                  if (pActiveComputer == nullptr)
                  {
                    sError = System::String::Format("No active computer found. Unable to open connection to {0}", pDatabase->Name);
                  }
                  else
                  {
                    pDB_Main = gcnew aDB::C_DB_Main(pActiveComputer, false, tDB_Options);
                  }
                } // else if (pComputer != nullptr)
              } // else if (pProject != nullptr)
            } // if (pDB_Main == nullptr)
          }
          catch (const std::exception & oErr) {
            sError = string_to_String(oErr.what());
          }
          catch (System::Exception ^ pErr) {
            sError = pErr->Message;
          }

          if (System::String::IsNullOrEmpty(sError))
          {
            aFW::Log::WriteRecord(System::String::Format("Open connection to {0}", (pDatabase == nullptr ? sDatabasePath : pDatabase->Name)));
            return true;
          }
          else
          {
            aFW::Log::WriteRecord(sError);
            ErrorNotification(sError);
            return false;
          }
        } // ...................................... Open .........................

        //
        // Close_ex
        //
        void fDboConnection::Close_ex()
        {
          System::String ^ sError = System::String::Empty;
          try
          {
            if (pDB_Main != nullptr)
            {
              // Associated I_DB_WriteChannels in the pDB_Main container will also be deleted.
              delete pDB_Main;
              pDB_Main = nullptr;
            }
          }
          catch (const std::exception & oErr) {
            sError = string_to_String(oErr.what());
          }
          catch (System::Exception ^ pErr) {
            sError = pErr->Message;
          }

          if (System::String::IsNullOrEmpty(sError))
          {
            aFW::Log::WriteRecord(System::String::Format("Closed connection to {0}", pDatabase == nullptr ? sDatabasePath : pDatabase->Name));
          }
          else
          {
            aFW::Log::WriteRecord(System::String::Format("Failed to close connection to {0} --> {1}", pDatabase == nullptr ? sDatabasePath : pDatabase->Name, sError));
            ErrorNotification(sError);
          }
        } // ...................................... Close_ex .........................

        //
        // ErrorNotification
        //
        void fDboConnection::ErrorNotification(System::String ^ const sError)
        {
          _ASSERT( !System::String::IsNullOrEmpty( sError ) );
          fDboEventInfo ^ pInfo = gcnew fDboEventInfo();
          pInfo->error = true;
          pInfo->message = sError;
          pInfo->data = nullptr;
          Notify(pInfo);
        } // ...................................... ErrorNotification .........................

        //
        // Notify
        //
        void fDboConnection::Notify(fDboEventInfo^ ei)
        {
          notificationEvent(ei);
        } // ...................................... DboConnectionNotify .........................

        //
        // fDboWriter
        //
        // ctor
        //
        fDboWriter::fDboWriter(
          fDboConnection ^ connection_, System::String ^ sRuntimeTag,
          System::Collections::Generic::IEnumerable<IElement ^> ^ elements_)
          : pConnection(connection_), sTablePrefix(sRuntimeTag), pElements(elements_), pBufferData(nullptr), pWriter(nullptr)
        {
          _ASSERT(pConnection != nullptr);
          _ASSERT(pConnection->DBmain != nullptr);
          _ASSERT(!System::String::IsNullOrEmpty(sRuntimeTag));
          _ASSERT(pElements != nullptr);

          CreateWriteChannels(sTablePrefix, pElements);
        } // ...................................... ctor .........................
        //
        // ctor
        //
        fDboWriter::fDboWriter(
          fDboConnection                                             ^ connection_,
          System::String                                             ^ sRuntimeTag,
          System::Collections::Generic::IEnumerable<IElement ^>      ^ elements_,
          System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fDatabase::ICADataBuffer ^> ^ pBufferData_)
          : pConnection(connection_), sTablePrefix(sRuntimeTag), pElements(elements_), pBufferData(pBufferData_), pWriter(nullptr)
        {
          _ASSERT(pConnection != nullptr);
          _ASSERT(pConnection->DBmain != nullptr);
          _ASSERT(!System::String::IsNullOrEmpty(sRuntimeTag));
          _ASSERT(pElements != nullptr);
          _ASSERT(pBufferData != nullptr);

          CreateWriteChannels(sTablePrefix, pElements);
        } // ...................................... ctor .........................
        //
        // ctor
        //
        fDboWriter::fDboWriter(
          fDboConnection ^ connection_,
          System::String ^ sRuntimeTag
          )
          : pConnection(connection_), sTablePrefix(sRuntimeTag), pElements(nullptr), pBufferData(nullptr), pWriter(nullptr)
        {
          _ASSERT(pConnection != nullptr);
          _ASSERT(pConnection->DBmain != nullptr);
          _ASSERT(!System::String::IsNullOrEmpty(sRuntimeTag));

          CreateWriteChannels(sTablePrefix, pElements);
        } // ...................................... ctor .........................

        //
        // finalizer
        //
        fDboWriter::!fDboWriter()
        {
          if (pConnection->DBmain != nullptr)
          {
            pConnection->DBmain->Get_Container()->Remove(pWriter);
            delete pWriter;
            pWriter = nullptr;
          }
        } // ...................................... finalizer .........................

        //
        // OnDisposeWriteChannels
        //
        void fDboWriter::OnDisposeWriteChannels(System::Object ^ sender, System::EventArgs ^ e)
        {
          pWriter = nullptr;
        } // ...................................... OnDisposeWriteChannels .........................

        //
        // CreateWriteChannels
        //
        void fDboWriter::CreateWriteChannels(System::String ^ const sRuntimeTag, System::Collections::Generic::IEnumerable<IElement ^> ^ pElements)
        {
          if (pConnection != nullptr && pConnection->DBmain != nullptr)
          {
            _ASSERT(!System::String::IsNullOrEmpty(sRuntimeTag));
            _ASSERT(sRuntimeTag[sRuntimeTag->Length - 1] != L'_');
            _ASSERT(pConnection->DBmain->Get_Container() != nullptr);

            aTV_REC::I_DB_WriteChannels::C_DB_WriteChannels_Create tCreate;
            tCreate.bTrim_Future_Time = true;
            tCreate.bDo_M_H_Tables = true;
            tCreate.bM_H_Tables_In_Segments = true;
            tCreate.bDo_CA_Table = true;
            tCreate.bDo_ENotification_Table = true;
            tCreate.pTotalizer = nullptr;
            tCreate.pTarget_Compliances = nullptr;
            tCreate.sPrefix = sRuntimeTag;
            tCreate.pDB_Wrapper = pConnection->DBmain;
            tCreate.pRead_Seconds = (pBufferData == nullptr ? nullptr : gcnew C_Read_Runtime_Seconds(this, pBufferData));
            tCreate.bVerbose_Logging = true;
            tCreate.bDo_VTSValuesNC_Table = true;

            if (pElements != nullptr)
            {
              for each (IElement ^ pel in pElements)
              {
                System::Collections::Generic::IEnumerable<IDataChannel ^> ^ data = pel->dataChannels;

                if (data != nullptr)
                {
                  for each(IDataChannel ^ pCh in data)
                  {
                    IWebChannel ^ pWebCh = dynamic_cast<IWebChannel ^>(pCh);
                    if (pWebCh != nullptr)
                    {
                      if (pWebCh->Type == System::Drawing::Bitmap::typeid)
                      {
                        continue;
                      }
                    }

                    aTV_REC::C_DB_Channel oCh;
                    oCh.ID = pCh;
                    oCh.Name  = (!System::String::IsNullOrEmpty(pel->Name) && (pel->Type->Name != SNC::OptiRamp::Applications::VTS::VTSTypes::ARCHIVE)) 
                              ? (pel->Name + "." + pCh->Name)
                              : pCh->Name;
                    oCh.sCEU = System::String::Empty;
                    oCh.bDiscreteType = pCh->DiscreteType;
                    oCh.bNCUType = pCh->NCUType;

                    oChannels.Add(oCh);
                  } // for each(IDataChannel ^ pCh in data)
                } // if (data != nullptr)
              } // for each (IElement ^ pel in pElements)

              tCreate.channels = %oChannels;
            } // if (pElements != nullptr)

            pWriter = aTV_REC::I_DB_WriteChannels::Create( tCreate );

            if (pWriter != nullptr)
            {
              // If the fDboConnection is closed before fDboWriter, then I_DB_WriteChannels will be disposed.
              pWriter->Disposed += gcnew System::EventHandler(this, &fDboWriter::OnDisposeWriteChannels);
            }
          } // if (pConnection != nullptr && pConnection->DBmain != nullptr)
        } // ...................................... CreateWriteChannels .........................

        //
        // WriteWebChannelEx
        //
        bool fDboWriter::WriteWebChannelEx(
          IElement                                    ^ pElement,
          SNC::OptiRamp::Services::fWeb::IWebChannel  ^ pWebChannel,
          SNC::OptiRamp::Services::fRT::RTData        ^ pData,
          const volatile bool                         * pCaller_Stop,
          [ System::Runtime::InteropServices::Out ] System::String ^% sError
          )
        {
          _ASSERT( pElement != nullptr );
          _ASSERT( pWebChannel != nullptr );
          _ASSERT( pData != nullptr );

          sError = System::String::Empty;

          try
          {
            if ( pWriter == nullptr )
            {
              // This could occur if the fDboConnection object is disposed before this fDboWriter.
              sError = "The database channels have not been registered. Has the fDboConnection been closed?";
            } else if ( pWebChannel->Type == System::Drawing::Bitmap::typeid )
            {
              sError = System::String::Format(
                "Abort. Detected System::Drawing::Bitmap for web channel {0}, element {1}.",
                pElement->Name, pWebChannel->Name );
            } else
            {
              System::Nullable<double> data;
              if (RTData::IsQualityGood(pData->Quality))
              {
                if ( pWebChannel->Type == System::Boolean::typeid )
                {
                  data = (safe_cast<bool>(pData->Value) ? 1.0 : 0.0);
                } else if ( pWebChannel->Type == System::Double::typeid )
                {
                  data = safe_cast<double>(pData->Value);
                }
              }

              pWriter->Write(
                pWebChannel,
                pData->Timestamp.ToFileTimeUtc(),
                data,
                pCaller_Stop );
            }
          } catch ( const std::exception & oErr ) {
            sError = string_to_String( oErr.what() );
          } catch ( System::Exception ^ pErr ) {
            sError = pErr->Message;
          }

          if ( System::String::IsNullOrEmpty( sError ) )
          {
            return true;
          } else
          {
            ErrorNotification( sError );
            return false;
          }
        } // ................................................ WriteWebChannelEx .........................

        //
        // WriteWebChannel
        //
        bool fDboWriter::WriteWebChannel(IElement ^ pElement, IWebChannel ^ pWebChannel, RTData ^ pData, [Out] System::String ^% sError)
        {
          return WriteWebChannelEx( pElement, pWebChannel, pData, NULL, sError );
        } // ...................................... WriteWebChannel .........................

        //
        // WriteWebChannelBlocking
        //
        bool fDboWriter::WriteWebChannelBlocking(
          IElement                                    ^ pElement,
          SNC::OptiRamp::Services::fWeb::IWebChannel  ^ pWebChannel,
          SNC::OptiRamp::Services::fRT::RTData        ^ pData,
          bool                                        % bStop,
          [ System::Runtime::InteropServices::Out ] System::String ^% sError
          )
        {
          pin_ptr<volatile const bool> oStop( &bStop );
          return WriteWebChannelEx( pElement, pWebChannel, pData, oStop, sError );
        }

        //
        // WriteWebChannelsEx
        //
        bool fDboWriter::WriteWebChannelsEx(
          IElement                                    ^ pElement,
          System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fRT::RTValue> ^ pData,
          const volatile bool                         * pCaller_Stop,
          [ System::Runtime::InteropServices::Out ] System::String  ^% sError
          )
        {
          _ASSERT( pElement != nullptr );
          _ASSERT( pData != nullptr );

          sError = System::String::Empty;

          try
          {
            if ( pData == nullptr )
            {
              sError = System::String::Format( "Abort. No data calculated for web channel {0}.", pElement->Name );
            } else
            {
              for each(RTValue rtv in pData)
              {
                if ( pCaller_Stop != NULL && *pCaller_Stop )
                {
                  sError = "interrupted";
                  break;
                }
                IWebChannel ^ const pWebChannel = dynamic_cast<IWebChannel ^>(rtv.DataChannel);
                _ASSERT( pWebChannel != nullptr );
                _ASSERT( pWebChannel->Type != System::Drawing::Bitmap::typeid );
                _ASSERT( pWebChannel->Type != System::Boolean::typeid );

                if ( pWriter == nullptr )
                {
                  // This could occur if the fDboConnection object is disposed before this fDboWriter.
                  sError = "The database channels have not been registered. Has the fDboConnection been closed?";
                } else if ( pWebChannel->Type == System::Drawing::Bitmap::typeid )
                {
                  sError = System::String::Format(
                    "Abort. Detected System::Drawing::Bitmap for web channel {0}, element {1}.",
                    pElement->Name, pWebChannel->Name );
                } else if ( pWebChannel->Type == System::Boolean::typeid )
                {
                  sError = System::String::Format(
                    "Abort. Detected System::Boolean for web channel {0}, element {1}.",
                    pElement->Name, pWebChannel->Name );
                } else
                {
                  rtv.Quality = (UINT)(rtv.Value.HasValue ? RTQuality::Good : RTQuality::Bad);

                  DateTime timestamp = (rtv.Timestamp == DateTime::MinValue) ? DateTime::Now : rtv.Timestamp;

                  pWriter->Write(
                    pWebChannel,
                    timestamp.ToFileTimeUtc(),
                    rtv.Value,
                    pCaller_Stop );
                }
              } // for each(RTValue rtv in pData)
            }
          } catch ( const std::exception & oErr ) {
            sError = string_to_String( oErr.what() );
          } catch ( System::Exception ^ pErr ) {
            sError = pErr->Message;
          }

          if ( System::String::IsNullOrEmpty( sError ) )
          {
            return true;
          } else
          {
            ErrorNotification( sError );
            return false;
          }
        } // ................................................. WriteWebChannelsEx ..........................

        //
        // WriteWebChannels
        //
        bool fDboWriter::WriteWebChannels(
          IElement ^ pElement, System::Collections::Generic::IEnumerable<RTValue> ^ pData, [Out] System::String ^% sError)
        {
          return WriteWebChannelsEx( pElement, pData, NULL, sError );
        } // ...................................... WriteWebChannels .........................

        //
        // WriteWebChannelsBlocking
        //
        bool fDboWriter::WriteWebChannelsBlocking(
          IElement                                    ^ pElement,
          System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fRT::RTValue> ^ pData,
          bool                         % bStop,
          [ System::Runtime::InteropServices::Out ] System::String  ^% sError 
          )
        {
          pin_ptr<volatile const bool> oStop( &bStop );
          return WriteWebChannelsEx( pElement, pData, oStop, sError );
        } // ........................................ WriteWebChannelsBlocking ........................

        //
        // WriteNcWebChannel
        //
        bool fDboWriter::WriteNcWebChannel(
          IElement                                                ^ pElement,
          SNC::OptiRamp::Services::fWeb::IWebChannel              ^ pWebChannel,
          SNC::OptiRamp::Services::fRT::RTDataString              ^ pData,
          SNC::OptiRamp::Services::fRT::ApplicationInfo           ^ pAppInfo,
          [System::Runtime::InteropServices::Out] System::String ^% sError
          )
        {
          _ASSERT(pWebChannel != nullptr);
          _ASSERT(pData != nullptr);
          _ASSERT(pAppInfo != nullptr);

          sError = System::String::Empty;

          try
          {
            if (pWriter == nullptr)
            {
              // This could occur if the fDboConnection object is disposed before this fDboWriter.
              sError = "The database channels have not been registered. Has the fDboConnection been closed?";
            }
            else if (!pWebChannel->NCUType)
            {
              sError = System::String::Format("Channel, \"{0}\" is not an NCUType.", pWebChannel->Name);
            }
            else
            {
              bool bChannelOwned = false;
              if (pElement == nullptr)
                bChannelOwned = true;
              else
              {
                IDataChannel ^ pChannel = nullptr;
                System::Collections::Generic::IEnumerable<IDataChannel ^> ^ pDataChannels = pElement->dataChannels;
                for each(pChannel in pDataChannels)
                {
                  if (System::String::CompareOrdinal(pChannel->Name, pWebChannel->Name) == 0)
                  {
                    bChannelOwned = true;
                    break;
                  }
                }
              }

              if (!bChannelOwned)
              {
                sError = System::String::Format(
                  "The element, \"{0}\", does not own channel \"{1}\".",
                  pElement->Name, pWebChannel->Name);
              }
              else
              {
                System::String ^ sAppInfoKey = pAppInfo->HashString();
                if (System::String::CompareOrdinal(sAppInfoKey, "BAD_HASH_STRING") == 0)
                {
                  sError = "The Application Info hash string is empty. " +
                    "Review ComputerName, FullName, ShortName and UserInfo.";
                }
                else
                {
                  aTV_REC::I_DB_WriteChannels::C_AppInfo_Record tAppInfoRecord;
                  tAppInfoRecord.sFullPath = pAppInfo->FullPath;
                  tAppInfoRecord.sComputerName = pAppInfo->ComputerName;
                  tAppInfoRecord.iInstanceNumber = pAppInfo->InstanceNumber;
                  tAppInfoRecord.sFullName = pAppInfo->FullName;
                  tAppInfoRecord.sShortName = pAppInfo->ShortName;
                  tAppInfoRecord.sUserInfo = pAppInfo->UserInfo;

                  int iAppID = -1;
                  sError = pWriter->Get_Applications_ID(tAppInfoRecord, iAppID);

                  if (System::String::IsNullOrEmpty(sError) && iAppID != -1)
                  {
                    aTV_REC::I_DB_WriteChannels::C_NCValue_Record tNcRecord;
                    tNcRecord.sChannelName = pWebChannel->Name;
                    tNcRecord.tPTime = pData->Timestamp;
                    tNcRecord.iQuality = pData->Quality;
                    tNcRecord.sPValue = pData->Value;
                    tNcRecord.iAppID = iAppID;

                    sError = pWriter->NC_Value_Write(tNcRecord);
                  }
                }
              } // else bChannelOwned
            } // else pWriter != nullptr
          }
          catch (const std::exception & oErr) {
            sError = string_to_String(oErr.what());
          }
          catch (System::Exception ^ pErr) {
            sError = pErr->Message;
          }

          if (System::String::IsNullOrEmpty(sError))
          {
            return true;
          }
          else
          {
            if (sError->Contains("duplicate column values"))
              sError = System::String::Format("Duplicate timestamp for Channel, \"{0}\".", pWebChannel->Name);
            ErrorNotification(sError);
            return false;
          }
        } // ................................................ WriteNcWebChannel .........................

        //
        // WriteNotification
        //
        bool fDboWriter::WriteNotification(
          NotificationDescriptor                                 ^  pNdescriptor,
          [System::Runtime::InteropServices::Out] System::String ^% sError)
        {
          _ASSERT(pNdescriptor != nullptr);
          _ASSERT(!System::String::IsNullOrEmpty(pNdescriptor->NSource));
          _ASSERT(!System::String::IsNullOrEmpty(pNdescriptor->NMessage));
          _ASSERT(pNdescriptor->NSeverity >= 0 && pNdescriptor->NSeverity < 1001);

          System::String ^ nstate = "";
          switch (pNdescriptor->NState)
          {
            case ConditionStatus::none:
              nstate = "";
              break;
            case ConditionStatus::active:
              nstate = "A";
              break;
            case ConditionStatus::cleared:
              nstate = "C";
              break;
            case ConditionStatus::ackn:
              nstate = "K";
              break;
            case ConditionStatus::disabled:
              nstate = "D";
              break;
            default:
              nstate = "";
              break;
          }

          System::String ^ ntrigger = "";
          switch (pNdescriptor->NArchiveTrigger)
          {
            case ArchiveTrigger::none:
              ntrigger = "";
              break;
            case ArchiveTrigger::onCondition:
              ntrigger = "C";
              break;
            case ArchiveTrigger::onDeviation:
              ntrigger = "D";
              break;
            default:
              ntrigger = "";
              break;
          }

          aTV_REC::I_DB_WriteChannels::C_NRecord oRecord;
          oRecord.tNTime = pNdescriptor->NTime;
          oRecord.sNSource = pNdescriptor->NSource;
          oRecord.sNMessage = pNdescriptor->NMessage;
          oRecord.sNState = nstate;
          oRecord.sNArchiveTrigger = ntrigger;
          oRecord.iNSeverity = pNdescriptor->NSeverity;
          oRecord.tNTime1 = pNdescriptor->NTime1;
          oRecord.tNTime2 = pNdescriptor->NTime2;
          oRecord.sNGroupName = pNdescriptor->NGroupName;
          oRecord.iNEvent = pNdescriptor->NEvent;

          sError = pWriter->Notification_Write(oRecord);
          return System::String::IsNullOrEmpty(sError);
        } // ........................................ WriteNotification ........................

        //
        // WriteCAData
        //
        //
        bool fDboWriter::WriteCAData(
          SNC::OptiRamp::Services::fDatabase::CADescriptor ^pDescriptor,
          System::IO::MemoryStream ^pBlob,
          [System::Runtime::InteropServices::Out] System::String ^% sError)
        {
          _ASSERT(pDescriptor != nullptr);
          _ASSERT(pDescriptor->Channels != nullptr);
          _ASSERT(pBlob != nullptr);

          sError = System::String::Empty;

          aTV_REC::I_DB_WriteChannels::C_CA_Event_Record tRecord;
          tRecord.sEvent_Name = pDescriptor->EventName;
          tRecord.sEvent_Description = pDescriptor->EventReason;
          tRecord.tTStart = pDescriptor->TStart;
          tRecord.tTEnd = pDescriptor->TEnd;
          tRecord.tTEvent = pDescriptor->TEvent;

          if (pBlob == nullptr)
            return false;
          else
          {
            array<unsigned char> ^ pBuf = pBlob->GetBuffer();

            const size_t iSize = static_cast<size_t>(pBlob->Length);
            pin_ptr<unsigned char> oBuf(&pBuf[0]);

            sError = pWriter->CA_Event_Write(tRecord, oBuf, iSize, gcnew System::Collections::Generic::List<System::String^>(pDescriptor->Channels));
            return System::String::IsNullOrEmpty(sError);
          }
        } // ...................................... WriteCAData .........................


        //
        // WriteStreamToBlob
        //
        bool fDboWriter::WriteStreamToBlob(
          System::String            ^ tableName,
          System::String            ^ dataName,
          System::DateTime            timeStampUTC,
          System::IO::MemoryStream  ^ pBlob,
          System::String          ^ % error
          )
        {
          try
          {
            array<unsigned char> ^ pBuf = (pBlob == nullptr) ? (gcnew array<unsigned char>(1)) : pBlob->GetBuffer();
            const size_t iSize = (pBlob == nullptr) ? 0 : static_cast<size_t>(pBlob->Length);
            pin_ptr<unsigned char> oBuf( &pBuf[ 0 ] );

            System::String ^ const sRes = pWriter->WriteStreamToBlob(
              tableName, dataName, timeStampUTC, oBuf, iSize
              );
            if (sRes == nullptr)
            {
              error = nullptr; return true;
            } else {
              error = sRes; return false;
            }


          } catch ( System::Exception ^ pErr ) {
            SILENT_EXCEPTION( pErr );
            error = pErr->Message;
            return false;
          }

        } // .................................... WriteStreamToBlob .................................

        //
        // Notify
        //
        void fDboWriter::Notify(fDboEventInfo^ ei)
        {
          try
          {
            notificationEvent( ei );
          } catch ( System::Exception ^ pErr ) {
            SILENT_EXCEPTION( pErr );
          }
        } // ...................................... Notify .........................

        //
        // ErrorNotification
        //
        void fDboWriter::ErrorNotification( System::String ^ sError )
        {
          _ASSERT( !System::String::IsNullOrEmpty( sError ) );
          fDboEventInfo ^ pInfo = gcnew fDboEventInfo();
          pInfo->error = true;
          pInfo->message = sError;
          pInfo->data = nullptr;
          Notify( pInfo );
        } // ...................................... ErrorNotification .........................

        //
        // FindParentApp
        //
        static IElement ^ FindParentApp( IElement ^ pElement, System::String ^ sApp )
        {
          fElement ^ pE = dynamic_cast<fElement^>(pElement);
          if ( pE == nullptr || pE->pNode == nullptr || pE->pNode->pParent == nullptr ) { return nullptr; }

          if ( pE->pNode->pType->sTag == sApp )
          {
            return pElement;
          } else
          {
            return FindParentApp( pElement->Parent, sApp );
          }
        } // ...................................... FindParentApp .........................

        //
        // fDataMining
        //
        // ctor
        //
        fDataMining::fDataMining(
          fDboConnection                                          ^ connection_,
          System::Collections::Generic::IEnumerable < IElement^ > ^ pPotentialElements,
          System::String                                          ^ sRuntimeTag
          ) : pConnection( connection_ )
        {
          _ASSERT( pConnection != nullptr );
          _ASSERT( pPotentialElements != nullptr );
          _ASSERT( sRuntimeTag != nullptr );
          aFW::Write_Log_Header( "fDataMining ctor" );

          auto pDM_Elements = gcnew System::Collections::Generic::List < IElement^ >;
          pData_Mining_Elements = pDM_Elements;

          if ( pPotentialElements == nullptr )
          { 
            System::Diagnostics::Trace::WriteLine( "List of potential elements is null" );
          } else if ( pConnection == nullptr )
          {
            System::Diagnostics::Trace::WriteLine( "fDboConnection is null" );
          } else {
            const int iRunTime = fApp::fApplications::Tag_to_Idx( sRuntimeTag );
            if ( iRunTime < 0 )
            {
              throw gcnew System::ArgumentException( System::String::Format(
                "Invalid parameter sRuntimeTag \"{0}\"", sRuntimeTag) );
            }

            IElement ^ pParentDMElement = nullptr;
            for each (IElement ^ pel in pPotentialElements)
            {
              pParentDMElement = FindParentApp( pel, aCC::C_Project_Node_CC_App::pTag );
              if ( pParentDMElement != nullptr )
              {
                break;
              }
            }

            fElement ^ pParentDataMining = dynamic_cast<fElement ^>(pParentDMElement);
            if ( pParentDataMining != nullptr )
            {
              System::Collections::Generic::List<System::String^> pCheck_Names;
              aCONF::C_Project_Node::C_Kids pKids;

              for each (IElement ^ pel in pPotentialElements)
              {
                fElement ^ pIter = dynamic_cast<fElement^>(pel);
                if ( pIter == nullptr ) { continue; }
                pKids.Add( pIter->pNode );
                pCheck_Names.Add( pel->Name );
              }

              const int iKids_Count_Before = pKids.Count;

              pData_Mining = aDM::Create_Data_Mining( iRunTime, pParentDataMining->pNode, % pKids, % pCheck_Names );

              if ( pData_Mining != nullptr )
              {
                pData_Mining->Assign_Web_HMI_Proxy(
                  gcnew aDM::I_Data_Mining::DLG_Signal_New_Value( this, &fDataMining::SignalNewValue ) );

                auto pNode_Element = pParentDataMining->pNode_Element;
                C_Node_Channel tKey;
                RTValue tValue;

                for ( int I = iKids_Count_Before; I < pKids.Count; ++I )
                {

                  tKey.pNode = pKids[ I ];
                  fElement ^ const pIter_Elem = pNode_Element[ tKey.pNode ];
                  pDM_Elements->Add( pIter_Elem );

                  tKey.iWebChannel = -1;

                  for each (IWebChannel ^ pIterCh in pIter_Elem->WebChannels)
                  {
                    ++tKey.iWebChannel;
                    tValue.DataChannel = pIterCh;
                    oTranslate_Nodes.Add( tKey, tValue );
                  }


                } // for ( int I = iKids_Count_Before ....

              } // if ( pData_Mining != nullptr ) ....


            } else {
              System::Diagnostics::Trace::WriteLine( "can't find parent data mining app" );
            }
          } // if ( pPotentialElements == nullptr ..... else .....

        } // ................................. ctor ....................................


        // Start
        //
        void fDataMining::Start()
        {
          if ( pData_Mining != nullptr )
          {
            pData_Mining->Start(pConnection->DBmain);
          } // if ( pData_Mining != nullptr ) .....

        } // ...................................... Start .........................

        //
        // SignalNewValue
        //
        void fDataMining::SignalNewValue(
          aCONF::C_Project_Node    ^ pNode,
          int                        iWebChannel,
          __int64                    iTime_UTC,
          System::Nullable<double>   tValue
          )
        {
          C_Node_Channel tKey;
          tKey.pNode = pNode;
          tKey.iWebChannel = iWebChannel;
          RTValue tBuf;
          if ( !oTranslate_Nodes.TryGetValue( tKey, tBuf ) )
          {
            _ASSERT( false );
          } else {
            _ASSERT( tBuf.DataChannel != nullptr );
            tBuf.Timestamp = System::DateTime( iTime_UTC, System::DateTimeKind::Utc );
            tBuf.Value = tValue;
            try
            {
              notificationData( tBuf );
            } catch ( System::Exception ^ pErr ) {
              SILENT_EXCEPTION( pErr );
            }
          } // if ( !oTranslate_Nodes.TryGetValue ....

          //if (iTime_UTC > iLast_Nudge)
          //{
          //  pData_Mining->Nudge(iTime_UTC);
          //  iLast_Nudge = iTime_UTC;
          //}

        } // ...................................... SignalNewValue .........................

        //
        // !
        //
        fDataMining::!fDataMining()
        {
          delete pData_Mining; pData_Mining = nullptr;
        }
      } // namespace fDatabase
    } // namespace Services
  } // namespace OptiRamp
} // namespace SNC

