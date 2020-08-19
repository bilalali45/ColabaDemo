import React, {FunctionComponent} from 'react';
import {useEffect, useState} from 'react';

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

      UserActions.addExpiryListener();
    };

    authenticate();
    // component unmount
    return () => {
      LocalDB.removeAuth();
    };
  }, []);

  console.log('Authorize User is authenticated', authenticated);
  if (!authenticated) {
    return null;
  }

  return <AppRoutes />;
};
