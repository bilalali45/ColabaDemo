import React, { useState, useEffect, useRef } from 'react';

import { Notifications } from '../features/Notifications';
import { Header, BellIcon } from './_HomePage';

export const dropdownConfig = () => {
  let queryString = window.location.search.substring(1, 100);
  const urlParams = new URLSearchParams(queryString);
  return {
    width: urlParams.get('width'),
    height: urlParams.get('height')
  }
}

export const HomePage = () => {

  let notifyStatus = 'close';
  const [notificationsVisible, setNotificationsVisible] = useState(false);
  const [notifyClass, setNotifyClass] = useState('close');
  const refContainerSidebar = useRef<HTMLDivElement>(null);


  const closeSidebar = (event: any) => {
    if (refContainerSidebar.current && !refContainerSidebar.current.contains(event.target)) {
      openEffect();
      setTimeout(() => { setNotificationsVisible(false) }, 1000)
    }
  }

  useEffect(() => {
    document.addEventListener("click", closeSidebar);
    return () => {
      document.removeEventListener("click", closeSidebar);
    };
  }, [notificationsVisible]);

  const openEffect = () => {
    setNotifyClass(notificationsVisible ? "animated slideOutRight" : "animated slideInRight");
  }

  const toggleNotificationSidebar = () => {
    if (notificationsVisible === false) {
      openEffect()
      setNotificationsVisible((prevState) => !prevState)
    }
    else {
      openEffect()
      setTimeout(() => { setNotificationsVisible((prevState) => !prevState) }, 1000)
    }

  }

  useEffect(() => {
    dropdownConfig()
    return () => {
      dropdownConfig()
    }
  }, [dropdownConfig])

  return (
    <div className={`notify`} ref={refContainerSidebar}>
      <BellIcon
        onClick={toggleNotificationSidebar}
      />
      {!!notificationsVisible && (

        <div className={`notify-dropdown ${notifyClass}`}>
          <Header />
          <Notifications />
        </div>

      )}
    </div>
  );
};
