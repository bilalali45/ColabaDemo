import React, {FunctionComponent, useEffect, useState} from 'react';
import '../../public/envconfig.js';

import {LocalDB} from '../lib/localStorage';

import {AppRoutes} from './routes';

import '../../public/rs-authorization';

// if(process.env.NODE_ENV === 'development'){
//   debugger
//   (async ()=>{
//     const authorization = await import('../../public/rs-authorization');
//     console.log("Authorize library imported successfully.", authorization)
//     window.Authorization = authorization.default;
//     await window.Authorization.authorize();
//   })();
// }

declare global {
  interface Window {
    envConfig: any;
    Authorization: any;
  }
}
window.envConfig = window.envConfig || {};

export const App: FunctionComponent = () => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);

  useEffect(() => {
    const authenticate = async () => {
      if (process.env.NODE_ENV === 'development') {
        await window.Authorization.authorize();
      }

      if (LocalDB.getAuthToken()) {
        setAuthenticated(Boolean(true));
      }
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
