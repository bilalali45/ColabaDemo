call kill.bat

set workingDir=%CD%

:choice
set /P c=Do you want to build the solution[Y/N]?
if /I "%c%" EQU "Y" goto :buildSolution
if /I "%c%" EQU "N" goto :skipBuildSolution
goto :choice

:buildSolution

call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\Tools\VsDevCmd.bat"

msbuild -t:restore "%workingDir%\\src\Solutions\Rainmaker2.sln"

msbuild "%workingDir%\\src\Solutions\Rainmaker2.sln" 

:skipBuildSolution

cd %workingDir%\src\MicroServices\KeyStore\KeyStore.API\bin\Debug\netcoreapp3.1\
start KeyStore.API.exe

TIMEOUT 10

cd %workingDir%\src\APIGateways\MainGateway\bin\Debug\netcoreapp3.1\
start MainGateway.exe


cd %workingDir%\src\MicroServices\ByteWebConnector\ByteWebConnector.API\bin\Debug\netcoreapp3.1\
start ByteWebConnector.API.exe


cd %workingDir%\src\MicroServices\DocumentManagement\DocumentManagement.API\bin\Debug\netcoreapp3.1\
start DocumentManagement.API.exe


cd %workingDir%\src\MicroServices\Identity\Identity.API\bin\Debug\netcoreapp3.1\
start Identity.exe


cd %workingDir%\src\MicroServices\LosIntegration\LosIntegration.API\bin\Debug\netcoreapp3.1\
start LosIntegration.API.exe


cd %workingDir%\src\MicroServices\Notification\Notification.API\bin\Debug\netcoreapp3.1\
start Notification.API.exe


cd %workingDir%\src\MicroServices\Rainmaker\Rainmaker.API\bin\Debug\netcoreapp3.1\
start Rainmaker.API.exe

cd %workingDir%\src\MicroServices\ByteWebConnector\ByteWebConnector.SDK\bin\Debug\net472\
start ByteWebConnector.SDK.exe

cd %workingDir%\src\MicroServices\Milestone\Milestone.api\bin\Debug\netcoreapp3.1\
start milestone.api.exe

cd %workingDir%\src\MicroServices\DocManager\DocManager.api\bin\Debug\netcoreapp3.1\
start DocManager.api.exe

cd %workingDir%\src\MicroServices\Setting\Setting.api\bin\Debug\netcoreapp3.1\
start Setting.api.exe

cd %workingDir%

timeout 3

