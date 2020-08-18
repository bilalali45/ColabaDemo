import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
  HttpTransportType
} from '@aspnet/signalr';
import { UserActions } from "../../Store/actions/UserActions";
export class SignalRHub {
  static hubConnection: any;
  // Set the initial SignalR Hub Connection.
  static configureHubConnection = async () => {
    // Events Register

    let userAgent = navigator.userAgent;
    let accessToken;
    const hubUrl = 'http://172.16.100.117:8065/serverhub';
    SignalRHub.hubConnection = new HubConnectionBuilder()
      .withUrl(hubUrl, {
        //skipNegotiation: true,
        //transport: HttpTransportType.WebSockets,
        //accessTokenFactory: accessToken
      })
      .configureLogging(LogLevel.Trace)
      .build();
    await SignalRHub.hubStart();
    SignalRHub.eventsRegister();
    SignalRHub.checkSignalR();
  };

  static hubStart = async () => {
    try {
      await SignalRHub.hubConnection.start();
      console.log('Connection start successful!');
    } catch (err) {
      alert(err);
      SignalRHub.signalRHubResume();
    }
  };

  static hubStop = async () => {
    try {
      SignalRHub.hubConnection.stop();
      console.log('Connection stop successful!');
    } catch (err) {
      alert(err);
    }
  };

 static checkSignalR = async () => {
  SignalRHub.hubConnection.onclose(() => {
    alert(`Signal R disconnected`);
    UserActions.authorize().then((data) => {
    console.log('isAuthorize',data)
    if(data){
      SignalRHub.signalRHubResume();
    }
    });
  });
 }

 static signalRHubResume = async () => {
  try{
    setTimeout(()=> {
      alert("SignalR Hub Resume");
      SignalRHub.hubStart();
    },3000)  
  }
  catch (err) {
    alert(err);
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

  };

  
}
