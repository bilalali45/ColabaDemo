import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
  HttpTransportType
} from '@microsoft/signalr';
import { UserActions } from "../../Store/actions/UserActions";
import { LocalDB } from '../LocalDB';
export class SignalRHub {
  static hubConnection: any;
  // Set the initial SignalR Hub Connection.
  static configureHubConnection = async () => {
    // Events Register

    let accessToken= LocalDB.getAuthToken();
    const hubUrl = 'http://172.16.100.117:8065/serverhub';
    SignalRHub.hubConnection = new HubConnectionBuilder()
      .withUrl(hubUrl, {
        //skipNegotiation: true,
        //transport: HttpTransportType.WebSockets,
        // accessTokenFactory: () => {
        //   if (accessToken) {
        //     return accessToken;
        //   }
        //   return '';
        // }
      })
      .configureLogging(LogLevel.Trace)
      .build();
    await SignalRHub.hubStart();
    SignalRHub.eventsRegister();
  };

  static hubStart = async () => {
    try {
      await SignalRHub.hubConnection.start();
      console.log('Connection start successful!');
    } catch (err) {
      console.log(err);
      SignalRHub.signalRHubResume();
    }
  };

  static hubStop = async () => {
    try {
      SignalRHub.hubConnection.stop();
      console.log('Connection stop successful!');
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

  static eventsRegister = () => {
    // Example Event Listner

    SignalRHub.hubConnection.on('TestSignalR', () => {
      console.log(`TestSignalR Tested`);
      window.dispatchEvent(
        new CustomEvent('TestSignalR', {
          detail: {name: 'John'}
        })
      );
    }); 
    
    SignalRHub.hubConnection.onclose(() => {
      console.log(`Signal R disconnected`);
      const auth = LocalDB.getAuthToken();
      if(auth){
        SignalRHub.signalRHubResume();
      }
    });

  };

  
}
