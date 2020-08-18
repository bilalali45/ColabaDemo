import React, {useState, useEffect, FunctionComponent} from 'react';

import {AppRoutes} from './routes';
import {authenticate} from '../lib/authenticate';

export const App: FunctionComponent = () => {
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getAuthToken = async () => {
      try {
        await authenticate();
        setLoading(false);
      } catch (error) {
        setLoading(false);
      }
    };

    getAuthToken();
  }, []);

  return loading ? null : <AppRoutes />;
};
