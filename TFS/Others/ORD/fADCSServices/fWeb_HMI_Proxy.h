//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
//-------------------------------------------------------------------
//
// Wrapper for Web_HMI_Proxy module 
//

#pragma once
#include "fProject.h"
#include "fProjectEUFactory.h"
#include "..\..\common\Utils_Native.h"
#include "..\..\common\Utils_Main.h"
#include "..\..\common\Utils_Array.h"
#include "..\..\common\web\Web_HMI_I.h"
#include "..\..\common\web\Web_HMI_Proxy.h"

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace fWeb
      {
        //
        // IWebProxy
        //
        public ref struct IWebProxy abstract
        {
          /// <summary>
          ///     starts inner threads 
          /// </summary>
          virtual void Start() = 0;

          /// <summary>
          ///     stops inner threads 
          /// </summary>
          virtual void Stop() = 0;

          /// <summary>
          ///     checks if we need to fire this channel (is used in Web HMI)
          /// </summary>
          virtual bool Web_Channel_Is_InUse(IWebChannel^ pChannel) = 0;

          /// <summary>
          ///     thread safe, non blocking
          /// </summary>
          virtual void Fire_Value(IWebChannel^ pChannel, System::Nullable<double>  tValue) = 0;

          /// <summary>
          ///     thread safe, non blocking
          /// </summary>
          virtual void Fire_Picture(IWebChannel^ pChannel, IPictureBuilder^ pPictureBuilder) = 0;

          /// <returns>
          ///     nullptr if error or no WebProxy is required
          ///     for error case sError is filled in  
          /// </returns>
          static IWebProxy ^ Create(
            fDefs::IProject                                                 ^ pProject, 
            System::Collections::Generic::IEnumerable < fDefs::IElement^ >  ^ pPotentialElements,
            [ System::Runtime::InteropServices::Out ] System::String      ^ % sError
            );

          //
          typedef System::Collections::Generic::Dictionary<System::String^, IWebChannel^> C_Tag_WebChannel;
          //
          static IWebProxy ^ Create_VTS( 
            fDefs::IProject                                                 ^ pProject,
            [ System::Runtime::InteropServices::Out ] C_Tag_WebChannel    ^ % pTag_WebChannel,  
            [ System::Runtime::InteropServices::Out ] System::String      ^ % sError
            );

        }; // ....................... IWebProxy ...................

      } // namespace fWeb
    } // namespace Services
  } // namespace OptiRamp
} // namespace SNC


