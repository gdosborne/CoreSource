#define SrcApp "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\DesktopClock.exe"
#define VerString GetFileVersion(SrcApp)
#define MyAppName "Desktop Clock"
#define MyAppPublisher "OSoft"
#define MyAppURL "http://www.gdosborne.com"
#define MyAppExeName "DesktopClock.exe"
#define UserDir GetEnv("USERPROFILE")
#define PicturesDir UserDir + "\Pictures"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{4D1D6119-1DF6-4F62-A718-030A8FD1C414}
AppName={#MyAppName}
AppVersion={#VerString}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputDir=C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\Install\{#VerString}
OutputBaseFilename=setup
SetupIconFile=C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\clock.ico
Compression=lzma
SolidCompression=yes
;SignTool=MSSignTool $f
;SignedUninstaller=Yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\DesktopClock.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\DesktopClock.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\MVVMFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\MVVMFramework.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\Ookii.Dialogs.Wpf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\OSControls.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\OSControls.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\Xceed.Wpf.Toolkit.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\OSoftCOmponents.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\OSoftCOmponents.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\Application.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Greg\Source\Workspaces\Domaination\DesktopClock\DesktopClock\DesktopClock\bin\Release\Application.pdb"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
