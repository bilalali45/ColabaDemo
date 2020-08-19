import React, {useState, useEffect, FunctionComponent} from 'react';

import {AppRoutes} from './routes';
import {postAuthenticate} from '../lib/authenticate';

export const App: FunctionComponent = () => {
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getAuthToken = async () => {
      try {
        await postAuthenticate();
        setLoading(false);
      } catch (error) {
        setLoading(false);
      }
    };

    getAuthToken();
  }, []);

  return loading ? null : <AppRoutes />;
};
