call kill.bat

set workingDir=%CD%

:choice
REM set /P c=Do you want to build the solution[Y/N]?
REM if /I "%c%" EQU "Y" goto :buildSolution
REM if /I "%c%" EQU "N" goto :skipBuildSolution
REM goto :choice

:buildSolution

REM call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\Tools\VsDevCmd.bat"

REM msbuild -t:restore "%workingDir%\\src\Solutions\Rainmaker2.sln"

REM msbuild "%workingDir%\\src\Solutions\Rainmaker2.sln" 

:skipBuildSolution

cd %workingDir%\src\MicroServices\KeyStore\KeyStore.API\bin\Debug\netcoreapp3.1\
start KeyStore.API.exe

TIMEOUT 10

cd %workingDir%\src\Presentation\Colaba\Colaba.Web\bin\Debug\netcoreapp3.1\
start Colaba.Web.exe

cd %workingDir%\src\APIGateways\MainGateway\bin\Debug\netcoreapp3.1\
start MainGateway.exe

cd %workingDir%\src\APIGateways\McuGateway\bin\Debug\netcoreapp3.1\
start McuGateway.exe

cd %workingDir%\src\APIGateways\MobileGateway\bin\Debug\netcoreapp3.1\
start MobileGateway.exe

REM cd %workingDir%\src\MicroServices\ByteWebConnector\ByteWebConnector.API\bin\Debug\netcoreapp3.1\
REM start ByteWebConnector.API.exe


REM cd %workingDir%\src\MicroServices\DocumentManagement\DocumentManagement.API\bin\Debug\netcoreapp3.1\
REM start DocumentManagement.API.exe


cd %workingDir%\src\MicroServices\Identity\Identity.API\bin\Debug\netcoreapp3.1\
start Identity.exe


REM cd %workingDir%\src\MicroServices\LosIntegration\LosIntegration.API\bin\Debug\netcoreapp3.1\
REM start LosIntegration.API.exe


REM cd %workingDir%\src\MicroServices\Notification\Notification.API\bin\Debug\netcoreapp3.1\
REM start Notification.API.exe


REM cd %workingDir%\src\MicroServices\Rainmaker\Rainmaker.API\bin\Debug\netcoreapp3.1\
REM start Rainmaker.API.exe

REM cd %workingDir%\src\MicroServices\ByteWebConnector\ByteWebConnector.SDK\bin\Debug\net472\
REM start ByteWebConnector.SDK.exe

REM cd %workingDir%\src\MicroServices\Milestone\Milestone.api\bin\Debug\netcoreapp3.1\
REM start milestone.api.exe

REM cd %workingDir%\src\MicroServices\DocManager\DocManager.api\bin\Debug\netcoreapp3.1\
REM start DocManager.api.exe

REM cd %workingDir%\src\MicroServices\Setting\Setting.api\bin\Debug\netcoreapp3.1\
REM start Setting.api.exe

cd %workingDir%\src\MicroServices\TenantConfig\TenantConfig.api\bin\Debug\netcoreapp3.1\
start TenantConfig.api.exe

cd %workingDir%\src\MicroServices\loanapplication\loanapplication.api\bin\Debug\netcoreapp3.1\
start loanapplication.api.exe

cd %workingDir%

timeout 3