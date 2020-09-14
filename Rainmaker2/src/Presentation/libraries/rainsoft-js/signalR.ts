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
    await SignalRHub.hubStart(eventsRegister);
  };

  static hubStart = async (eventsRegister:any) => {
    try {
      if(SignalRHub.hubConnection.connectionState==='Disconnected'){

        await SignalRHub.hubConnection.start();
        eventsRegister()
      }
      console.log('SignalR Connection start successful!');
    } catch (err) {
      console.log(err);
      SignalRHub.signalRHubResume(eventsRegister);
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

 static signalRHubResume = async (eventsRegister:any) => {
  try{
    setTimeout(()=> {
      console.log("SignalR Hub Resume");
      SignalRHub.hubStart(eventsRegister);
    },3000)  
  }
  catch (err) {
    console.log(err);
  } 
  };

    
}