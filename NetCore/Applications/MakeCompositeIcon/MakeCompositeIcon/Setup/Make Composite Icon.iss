#define SrcApp "F:\Git\CoreSource\NetCore\Applications\MakeCompositeIcon\MakeCompositeIcon\bin\x64\Release\net7.0-windows8.0\MakeCompositeIcon.exe"
#define VerString GetFileVersion(SrcApp)  

; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Make Composite Icon " + VerString
#define MyAppPublisher "Greg Osborne"
#define MyAppExeName "MakeCompositeIcon.exe"
#define MyAppAssocName MyAppName + " File"
#define MyAppAssocExt ".compo"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{7FE3D26E-F05E-4FF3-BE3B-0B86A9FBA947}
AppName={#MyAppName}
AppVersion={#VerString}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf64}\{#MyAppName}
ChangesAssociations=no
DisableWelcomePage=no
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
OutputDir=F:\Git\CoreSource\NetCore\Applications\MakeCompositeIcon\MakeCompositeIcon\Setup\Output
OutputBaseFilename=MakeCompositeIconSetup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
AlwaysShowGroupOnReadyPage=yes
AlwaysShowDirOnReadyPage=yes
DisableDirPage=no
SetupIconFile=F:\Git\CoreSource\NetCore\Applications\MakeCompositeIcon\MakeCompositeIcon\bin\x64\Release\net7.0-windows8.0\MakeComositeIcon.ico

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "F:\Git\CoreSource\NetCore\Applications\MakeCompositeIcon\MakeCompositeIcon\bin\x64\Release\net7.0-windows8.0\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""
Root: HKA; Subkey: "Software\Classes\Applications\{#MyAppExeName}\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

