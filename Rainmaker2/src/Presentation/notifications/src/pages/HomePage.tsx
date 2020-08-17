import React, {useState} from 'react';

import {Notifications} from '../features/Notifications';
import {Header, BellIcon} from './_HomePage';

export const HomePage = () => {
  const [notificationsVisible, setNotificationsVisible] = useState(false);

  return (
    <div>
      <BellIcon
        onClick={() => setNotificationsVisible((prevState) => !prevState)}
      />
      {!!notificationsVisible && (
        <React.Fragment>
          <Header />
          <Notifications />
        </React.Fragment>
      )}
    </div>
  );
};
