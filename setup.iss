[Files]
Source: D:\vs\TerribleFate\MajorasClock\bin\Release\MajorasClock.exe; DestDir: {app};
Source: D:\vs\TerribleFate\MajorasClock\bin\Release\MajorasClock.exe.config; DestDir: {app};
Source: D:\vs\TerribleFate\MajorasClock\bin\Release\TerribleFate.dll; DestDir: {app};
Source: D:\vs\TerribleFate\MajorasClock\bin\Release\Xceed.Wpf.Toolkit.dll; DestDir: {app};
Source: D:\vs\TerribleFate\MajorasClock\bin\Release\de\TerribleFate.resources.dll; DestDir: {app}\de\; 
Source: D:\vs\TerribleFate\MajorasClock\bin\Release\en\TerribleFate.resources.dll; DestDir: {app}\en\; 
Source: D:\vs\TerribleFate\MajorasClock\bin\Release\clock.ico; DestDir: {app}; 
Source: D:\vs\TerribleFate\MajorasClock\bin\Release\notifier.exe; DestDir: {app}; 

[Dirs]
Name: {app}\de;
Name: {app}\en;

[Setup]
AppName=Majora큦 Clock
AppVerName=1.0
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
Name: "{commonstartup}\Majora큦 Clock"; Filename: "{app}\MyProg.exe"; Components: StartwithSystem; 

[Run]
Filename: {app}\MajorasClock.exe; Description: Start Application after Install; Flags: postinstall shellexec skipifsilent
