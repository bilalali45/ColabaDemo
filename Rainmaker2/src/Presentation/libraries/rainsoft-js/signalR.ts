import {
    HubConnection,
    HubConnectionBuilder,
    LogLevel,
    HttpTransportType
  } from '@microsoft/signalr';
 
  export class SignalRHub {
    public static hubConnection: any;

    static configureHubConnection = async (hubUrl: string, accessToken: string, eventsRegister: Function) => {
      
      SignalRHub.hubConnection = new HubConnectionBuilder()
        .withUrl(hubUrl, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
        accessTokenFactory: () => {
          if (accessToken) {
            return accessToken;
          }
          return '';
         }
        })
        .configureLogging(LogLevel.Trace)
        .build();
      await SignalRHub.hubStart();
        eventsRegister();
    };
  
    static hubStart = async () => {
      try {
        await SignalRHub.hubConnection.start();
        console.log('SignalR Connection start successful!');
      } catch (err) {
        console.log(err);
        SignalRHub.signalRHubResume();
      }
    };
  
    static hubStop = async () => {
      try {
        SignalRHub.hubConnection.stop();
        console.log('SignalR Connection stop successful!');
      } catch (err) {
        console.log(err);
      }
    };
  
   static signalRHubResume = async () => {
    try{
      setTimeout(()=> {
        console.log("SignalR Hub Resume");
        SignalRHub.hubStart();
      },3000)  
    }
    catch (err) {
      console.log(err);
    } 
    };

      
  }
  