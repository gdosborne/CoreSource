#define SrcApp "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\UpdateVersionSharp.exe"
#define VerString GetFileVersion(SrcApp)

#define MyAppName "Update Version Sharp"
#define MyInstallDirName "UpdateVersion"
#define MyAppVersion VerString
#define MyAppPublisher "Greg Osborne"
#define MyAppURL "http://www.gdosborne.com"
#define MyAppExeName "UpdateVersionSharp.exe"

[Setup]
AppId={{09D09FF0-0920-401F-B871-3727A44C9017}
AppName={#MyAppName}
AppVersion={#VerString}
AppVerName={#MyAppName} {#VerString}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf64}\{#MyInstallDirName}
DefaultGroupName={#MyAppName}
OutputBaseFilename={#MyAppName} {#VerString}
Compression=zip
SolidCompression=yes
DisableWelcomePage=False
SetupIconFile=..\..\..\Einstein.ico
AllowUNCPath=False
WizardImageFile=..\..\..\LargeImage.bmp
WizardSmallImageFile=..\..\..\SmallImage.bmp
OutputDir=OutputDir
VersionInfoVersion={#VerString}
VersionInfoCompany=Greg Osborne
VersionInfoDescription=Updates version information
VersionInfoCopyright=Copyright © 2019 Greg Osborne
VersionInfoProductName={#MyAppName}
VersionInfoProductVersion={#VerString}
VersionInfoProductTextVersion={#VerString}
AlwaysShowGroupOnReadyPage=True
AlwaysShowDirOnReadyPage=True
AppCopyright=Copyright © 2019 Greg Osborne
DisableDirPage=no
ChangesAssociations=True

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\_errors.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\_readme.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\ConsoleUtilities.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\ConsoleUtilities.dll.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\ConsoleUtilities.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\GregOsborne.Application.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\GregOsborne.Application.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\UpdateVersionSharp.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\UpdateVersionSharp.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\UpdateVersionSharp.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\VersionMaster.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\VersionMaster.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\bin\x64\Release\UpdateVersion.Projects.xml"; DestDir: "{app}"; Flags: ignoreversion

Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\EnableVersioning\bin\x64\Release\EnableVersioning.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\EnableVersioning\bin\x64\Release\EnableVersioning.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\EnableVersioning\bin\x64\Release\EnableVersioning.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\EnableVersioning\bin\x64\Release\EnableVersioning.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\EnableVersioning\bin\x64\Release\GregOsborne.MVVMFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\EnableVersioning\bin\x64\Release\GregOsborne.MVVMFramework.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\EnableVersioning\bin\x64\Release\Microsoft.WindowsAPICodePack.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\EnableVersioning\bin\x64\Release\Microsoft.WindowsAPICodePack.Shell.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\EnableVersioning\bin\x64\Release\Ookii.Dialogs.Wpf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\greg\source\Workspaces\Greg.Osborne\Tools\Console\UpdateVersionSharp\EnableVersioning\bin\x64\Release\Ookii.Dialogs.Wpf.pdb"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Enable Versioning"; Filename: "{app}\EnableVersioning.exe"

[Registry]
Root: HKCR; Subkey: ".updateversion"; ValueData: "{#MyAppName}"; Flags: uninsdeletevalue; ValueType: string; ValueName: ""
Root: HKCR; Subkey: "{#MyAppName}"; ValueData: "Program {#MyAppName}"; Flags: uninsdeletekey; ValueType: string;  ValueName: ""
Root: HKCR; Subkey: "{#MyAppName}\DefaultIcon"; ValueData: "{app}\{#MyAppExeName},0"; ValueType: string; ValueName: ""
Root: HKCR; Subkey: "{#MyAppName}\shell\open\command"; ValueData: """{app}\{#MyAppExeName}"" ""%1""";  ValueType: string;  ValueName: ""
