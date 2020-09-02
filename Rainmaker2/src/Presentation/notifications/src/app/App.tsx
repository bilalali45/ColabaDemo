import React, {FunctionComponent, useEffect, useState} from 'react';

import '../../public/envconfig.js';
import {LocalDB} from '../lib/localStorage';
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
    const authenticate = async () => {
      const isAuth = await UserActions.authorize();

      setAuthenticated(Boolean(isAuth));

      UserActions.addExpiryListener();
    };

    authenticate();

    return () => {
      LocalDB.removeAuth();
    };
  }, []);

  if (!authenticated) {
    return null;
  }

  return <AppRoutes />;
};
