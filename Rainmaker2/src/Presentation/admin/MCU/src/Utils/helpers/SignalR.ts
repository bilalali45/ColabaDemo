import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
  HttpTransportType
} from '@aspnet/signalr';

export class SignalRHub {
  static hubConnection: any;
  // Set the initial SignalR Hub Connection.
  static configureHubConnection = async () => {
    // Events Register

    let userAgent = navigator.userAgent;
    let accessToken;
    const hubUrl = 'https://localhost:44322/serverhub';
    SignalRHub.hubConnection = new HubConnectionBuilder()
      .withUrl(hubUrl, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
        //accessTokenFactory: accessToken
      })
      .configureLogging(LogLevel.Trace)
      .build();
    SignalRHub.eventsRegister();
    await SignalRHub.hubStart();
  };

  static hubStart = async () => {
    try {
      await SignalRHub.hubConnection.start();
      console.log('Connection start successful!');
    } catch (err) {
      alert(err);
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
