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
    // Build new Hub Connection, url is currently hard coded.
    let userAgent = navigator.userAgent;
    let accessToken;
    const hubUrl = 'https://alphavortex.rainsoftfn.com/SignalR';
    const param = `?userName=khalid&typeId=1&userAgent=${userAgent}`;
    SignalRHub.hubConnection = new HubConnectionBuilder()
      .withUrl(hubUrl, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
        accessTokenFactory: accessToken
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
    window.addEventListener('TestSignalR', (data: any) => {
      alert(data.detail.name);
    });

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
