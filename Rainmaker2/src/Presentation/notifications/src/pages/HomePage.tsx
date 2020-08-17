import React from 'react';

import {Notifications} from '../features/Notifications';
import {Header} from './_HomePage';

export const HomePage = () => {
  return (
    <div>
      <Header />
      <Notifications />
    </div>
  );
};
