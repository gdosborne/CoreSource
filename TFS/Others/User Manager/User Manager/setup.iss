#define SrcApp "C:\Users\Greg\Documents\Visual Studio 2013\Projects\User Manager\User Manager\bin\Release\User Manager.exe"
#define VerString GetFileVersion(SrcApp)

#define MyAppName "User Manager"
#define MyAppVersion "1.0"
#define MyAppPublisher "Greg Osborne"
#define MyAppURL "http://www.gdosborne.com/"
#define MyAppExeName "User Manager.exe"

[Setup]
AppId={{4A0C4855-FE5A-4A33-9F21-03036F49EDA4}
AppName={#MyAppName}
AppVersion={#VerString}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName=Greg Osborne
OutputBaseFilename=UserManagerSetup
Compression=lzma
SolidCompression=yes
OutputDir=\\192.168.0.100\transfer\gosborne\installs
;Files in the [Files] section are subs of SourceDir 
SourceDir=C:\Users\Greg\Documents\Visual Studio 2013\Projects\User Manager\User Manager
SetupIconFile=images\blueSecurity.ico
WizardImageFile=images\WizModernImage.bmp

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "bin\Release\User Manager.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\ICSharpCode.SharpZipLib.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\Mono.Security.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\PCLCrypto.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\PCLCrypto.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\Validation.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\Validation.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\Ookii.Dialogs.Wpf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\SNCAuthorization.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\SNCAuthorization.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\User Manager.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\User Manager.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\_Main\_Web\_Installer\OptiRampInstallPackager\Output\Installer\Framework\NDP451-KB2858728-x86-x64-AllOS-ENU.exe"; DestDir: {tmp}; Flags: deleteafterinstall; Check: CheckForFramework
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: {tmp}\NDP451-KB2858728-x86-x64-AllOS-ENU.exe; Parameters: "/q:a /c:""install /l /q"""; Check: CheckForFramework; StatusMsg: Microsoft Framework 4.5.1 is beïng installed. Please wait...
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
Function CheckForFramework : boolean;
Var regresult : cardinal;
Var regversion : String;
Begin
  RegQueryDWordValue(HKLM, 'Software\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install', regresult);
  RegQueryStringValue(HKLM, 'Software\Microsoft\NET Framework Setup\NDP\v4\Full', 'Version', regversion);
  If (regresult = 0) or (pos('4.5', regversion) = 0) Then
  Begin
    Result := true;
  End
  Else
    Result := false;
End;
