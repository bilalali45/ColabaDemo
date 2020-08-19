import {AppRoutes} from './routes';
import {useEffect, useState} from 'react';
import {LocalDB} from '../Utils/LocalDB';
import {UserActions} from '../actions/UserActions';
import React from 'react';

declare global {
  interface Window {
    envConfig: any;
  }
}
window.envConfig = window.envConfig || {};

export const App = () => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);
  useEffect(() => {
    console.log('MCU App Version', '0.0.1');
    authenticate();
    // component unmount
    return () => {
      LocalDB.removeAuth();
    };
  }, []);

  const authenticate = async () => {
    console.log('Before Authorize');
    let isAuth = await UserActions.authorize();
    setAuthenticated(Boolean(isAuth));
    console.log('After Authorize');
    UserActions.addExpiryListener();
  };

  console.log('Authorize User is authenticated', authenticated);
  if (!authenticated) {
    return null;
  }

  return <AppRoutes />;
};
