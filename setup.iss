[Files]
Source: MajorasClock\bin\Release\MajorasClock.exe; DestDir: {app};  Flags: ignoreversion;
Source: MajorasClock\bin\Release\MajorasClock.exe.config; DestDir: {app};      Flags: ignoreversion;
Source: MajorasClock\bin\Release\TerribleFate.dll; DestDir: {app};    Flags: ignoreversion;
Source: MajorasClock\bin\Release\Xceed.Wpf.Toolkit.dll; DestDir: {app};         Flags: ignoreversion;
Source: MajorasClock\bin\Release\de\TerribleFate.resources.dll; DestDir: {app}\de\;  Flags: ignoreversion;
Source: MajorasClock\bin\Release\en\TerribleFate.resources.dll; DestDir: {app}\en\;   Flags: ignoreversion;
Source: MajorasClock\clock.ico; DestDir: {app};                          Flags: ignoreversion;
Source: MajorasClock\bin\Release\notifier.exe; DestDir: {app};         Flags: ignoreversion;

[Dirs]
Name: {app}\de;
Name: {app}\en;

[Setup]
AppName=Majora�s Clock
AppVerName=Majora`s Clock 1.0 Beta 2
RestartIfNeededByRun=false
AppPublisherURL=http://terriblefate.codeplex.com
AppSupportURL=http://terriblefate.codeplex.com
AppUpdatesURL=http://terriblefate.codeplex.com
UninstallDisplayIcon={app}\clock.ico
VersionInfoCompany=Recursivebytes
VersionInfoProductName=Majora�s Clock
DefaultDirName={pf}\Recursivebytes\
SolidCompression=true
DisableProgramGroupPage=yes
DefaultGroupName=Majora�s Clock
UninstallDisplayName=Majora�s Clock
AppVersion=1.0
UninstallLogMode=new
AppPublisher=Recursivebytes

[Components]
Name: StartwithSystem; Description: "Add to Autostart"; 

[Icons] 
Name: "{commonstartup}\Majora�s Clock"; Filename: "{app}\MajorasClock.exe";  Parameters: "-i"; Components: StartwithSystem; 
Name: "{app}\Majora�s Clock (light)"; Filename: "{app}\MajorasClock.exe";  Parameters: "-i";

[Run]
Filename: {app}\MajorasClock.exe;  Parameters: "-i"; Description: Start Application after Install; Flags: postinstall shellexec skipifsilent
