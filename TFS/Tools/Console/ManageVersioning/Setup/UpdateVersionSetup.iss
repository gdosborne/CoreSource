#define SrcApp "C:\Git\CoreSource\TFS\Tools\Console\ManageVersioning\bin\x64\Release\net8.0-windows10.0.17763.0\ManageVersioning.exe"
#define VerString GetFileVersion(SrcApp)

#define MyAppName "Update Version"
#define MyManagerName "Manage Versioning"
#define MyInstallDirName "UpdateVersion"
#define MyAppVersion VerString
#define MyAppPublisher "Greg Osborne"
#define MyAppURL "http://www.gdosborne.com"
#define MyAppExeName "ManageVersioning.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{A14D6136-9774-43D2-936D-0E22A698D6B5}
AppName={#MyAppName}
AppVersion={#VerString}
AppVerName={#MyAppName} {#VerString}
AppPublisher={#MyAppPublisher}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf64}\{#MyInstallDirName}
DisableWelcomePage=No
AllowUNCPath=False
PrivilegesRequired=lowest
OutputDir=OutputDir
OutputBaseFilename=UpdateVersionSetup
SetupIconFile=C:\Git\CoreSource\TFS\Tools\Console\ManageVersioning\UpdateVersion.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern
VersionInfoVersion={#VerString}
VersionInfoCompany=Greg Osborne
VersionInfoDescription=Updates version information
VersionInfoCopyright=Copyright © 2024 Greg Osborne
VersionInfoProductName={#MyAppName}
VersionInfoProductVersion={#VerString}
VersionInfoProductTextVersion={#VerString}
AlwaysShowGroupOnReadyPage=True
AlwaysShowDirOnReadyPage=True
AppCopyright=Copyright © 2024 Greg Osborne
DisableDirPage=no

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Git\CoreSource\TFS\Tools\Console\ManageVersioning\bin\x64\Release\net8.0-windows10.0.17763.0\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Git\CoreSource\TFS\Tools\Console\ManageVersioning\bin\x64\Release\net8.0-windows10.0.17763.0\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Git\CoreSource\TFS\Tools\Console\UpdateVersion\bin\x64\Release\net8.0-windows10.0.17763.0\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyManagerName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyManagerName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

