#include "..\..\common\Attributes_SNC.h"
#include "..\..\version.h"
namespace aFW
{ 
  const wchar_t * ctApp_Full_Name               = OPTIRAMP L" RPService";
  const char    * ctApp_Full_Name_S             = OPTIRAMP_S " RPService";
  const char    * ctApp_Short_Notification_Name = "RPService";   
}

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;
using namespace System::Runtime::InteropServices;
using namespace System::Security::Permissions;

//
// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly:AssemblyTitleAttribute("RPService")];
[assembly:AssemblyProductAttribute("RPService")];

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the value or you can default the Revision and Build Numbers
// by using the '*' as shown below:

[assembly:AssemblyVersionAttribute(MY_VERSION_ASSEMBLY)];


[assembly:ComVisible(false)];

[assembly:CLSCompliantAttribute(true)];