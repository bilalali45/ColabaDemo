import React, {FunctionComponent} from 'react';
import {useEffect, useState} from 'react';
import {SignalRHub} from 'rainsoft-js';

import {LocalDB} from '../Utils/LocalDB';
import {UserActions} from '../actions/UserActions';
import {AppRoutes} from './routes';

declare global {
  interface Window {
    envConfig: any;
  }
}
window.envConfig = window.envConfig || {};

export const App: FunctionComponent = () => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);

  useEffect(() => {
    console.log('MCU App Version', '0.0.1');
    const authenticate = async () => {
      console.log('Before Authorize');
      const isAuth = await UserActions.authorize();
      setAuthenticated(Boolean(isAuth));
      console.log('After Authorize');
      const accessToken: string = LocalDB.getAuthToken() || '';
      SignalRHub.configureHubConnection(
        window.envConfig.API_BASE_URL + '/serverhub',
        accessToken,
        eventsRegister
      );
      UserActions.addExpiryListener();
    };

    authenticate();
    // component unmount
    return () => {
      LocalDB.removeAuth();
    };
  }, []);

  const eventsRegister = () => {
    console.log('signalR eventsRegister on Client', SignalRHub.hubConnection);
    SignalRHub.hubConnection.on('TestSignalR', (data: string) => {
      console.log(`TestSignalR`, data);
    });
    SignalRHub.hubConnection.on('SendNotification', (data: any) => {
      console.log('Notification comes from SignalR on Client', data);
    });

    SignalRHub.hubConnection.onclose((e: any) => {
      console.log(`SignalR disconnected on Client`, e);
      const auth = LocalDB.getAuthToken();
      if (auth) {
        SignalRHub.signalRHubResume();
      }
    });
  };

  console.log('Authorize User is authenticated', authenticated);
  if (!authenticated) {
    return null;
  }

  return <AppRoutes />;
};
