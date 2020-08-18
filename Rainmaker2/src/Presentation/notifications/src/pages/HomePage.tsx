import React, { useState, useEffect, useRef } from 'react';

import {Notifications} from '../features/Notifications';
import {Header, BellIcon} from './_HomePage';
import { SSL_OP_SSLEAY_080_CLIENT_DH_BUG } from 'constants';
import { AlertForRemove, AlertForNoData } from '../features/NotificationAlerts';

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
  const [unClear,setClear] = useState(false);
  const [clearAllConfirm,setClearAllConfirm] = useState(false);
  const refContainerSidebar = useRef<HTMLDivElement>(null);


  const closeSidebar = (event: any) => {
    // console.log(event.target.classList);
    
    if (!event.target?.className?.includes('btn-notify') && refContainerSidebar.current && !refContainerSidebar.current.contains(event.target)) {
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

  const clearAll = () => {
    setClear(true);
  }

  const clearAllVerification = (verify:boolean) => {
    if(verify===true){
       setClearAllConfirm(true);
       setClear(true);
    }else{
       setClearAllConfirm(false);
       setClear(false);
       console.log('NOOO!')
    }
  }

  const notifying = () => {
    if(unClear === false && clearAllConfirm === false){
      return <Notifications /> 
    }else{
      if(clearAllConfirm===true){
        return <AlertForNoData/>;
      }else{
        return <AlertForRemove handleClearVerification={clearAllVerification}/>;
      }
      
    }
  }

  return (
    <div className={`notify`} ref={refContainerSidebar}>
      <BellIcon
        onClick={toggleNotificationSidebar}
      />
      {!!notificationsVisible && (

        <div className={`notify-dropdown ${notifyClass}`}>
          <Header handleClear={clearAll} />            
            { notifying() }
        </div>

      )}

    </div>
  );
};
