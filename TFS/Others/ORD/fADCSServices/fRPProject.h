//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
//-------------------------------------------------------------------
//
// Implementation of IRPProject interface.
//

#pragma once

namespace SNC
{
  namespace OptiRamp
  {
    namespace Services
    {
      namespace RodPump
      {
        public interface class IRPProject 
        {
          /// <summary>
          ///     The function returns list of rod pump IElement objects
          /// </summary>
          /// <param name="file">
          /// </param>
          /// <param name="options">
          /// </param>
          /// <param name="status">
          /// </param>
          /// <returns>
          ///     returns nullptr and fills in sError in case of error
          /// </returns>
           System::Collections::Generic::IEnumerable<SNC::OptiRamp::Services::fDefs::IElement^>  ^ GetRPElems(
            [ System::Runtime::InteropServices::Out ] System::String ^ %  sError );
        };

      } // namespace RodPump

    } // namespace Services
  } // namespace OptiRamp
} // namespace SNC
