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
AppName=Majora큦 Clock
AppVerName=Majora`s Clock 1.0 Beta 2
RestartIfNeededByRun=false
AppPublisherURL=http://terriblefate.codeplex.com
AppSupportURL=http://terriblefate.codeplex.com
AppUpdatesURL=http://terriblefate.codeplex.com
UninstallDisplayIcon={app}\clock.ico
VersionInfoCompany=Recursivebytes
VersionInfoProductName=Majora큦 Clock
DefaultDirName={pf}\Recursivebytes\
SolidCompression=true
DisableProgramGroupPage=yes
DefaultGroupName=Majora큦 Clock
UninstallDisplayName=Majora큦 Clock
AppVersion=1.0
UninstallLogMode=new
AppPublisher=Recursivebytes

[Components]
Name: StartwithSystem; Description: "Add to Autostart"; 

[Icons] 
Name: "{commonstartup}\Majora큦 Clock"; Filename: "{app}\MajorasClock.exe";  Parameters: "-i"; Components: StartwithSystem; 
Name: "{app}\Majora큦 Clock (light)"; Filename: "{app}\MajorasClock.exe";  Parameters: "-i";

[Run]
Filename: {app}\MajorasClock.exe;  Parameters: "-i"; Description: Start Application after Install; Flags: postinstall shellexec skipifsilent
