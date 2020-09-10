cd D:\work\Rainmaker2\Rainmaker2\src\MicroServices\KeyStore\KeyStore.API\bin\Debug\netcoreapp3.1\
start KeyStore.API.exe

TIMEOUT 3

cd D:\work\Rainmaker2\Rainmaker2\src\APIGateways\MainGateway\bin\Debug\netcoreapp3.1\
start MainGateway.exe


cd D:\work\Rainmaker2\Rainmaker2\src\MicroServices\ByteWebConnector\ByteWebConnector.API\bin\Debug\netcoreapp3.1\
start ByteWebConnector.API.exe


cd D:\work\Rainmaker2\Rainmaker2\src\MicroServices\DocumentManagement\DocumentManagement.API\bin\Debug\netcoreapp3.1\
start DocumentManagement.API.exe


cd D:\work\Rainmaker2\Rainmaker2\src\MicroServices\Identity\Identity.API\bin\Debug\netcoreapp3.1\
start Identity.exe


cd D:\work\Rainmaker2\Rainmaker2\src\MicroServices\LosIntegration\LosIntegration.API\bin\Debug\netcoreapp3.1\
start LosIntegration.API.exe


cd D:\work\Rainmaker2\Rainmaker2\src\MicroServices\Notification\Notification.API\bin\Debug\netcoreapp3.1\
start Notification.API.exe


cd D:\work\Rainmaker2\Rainmaker2\src\MicroServices\Rainmaker\Rainmaker.API\bin\Debug\netcoreapp3.1\
start Rainmaker.API.exe

timeout 1

