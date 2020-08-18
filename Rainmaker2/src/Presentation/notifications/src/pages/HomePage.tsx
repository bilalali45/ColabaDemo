import React, {useState,useEffect} from 'react';

import {Notifications} from '../features/Notifications';
import {Header, BellIcon} from './_HomePage';

export const dropdownConfig = () => {
  let queryString  = window.location.search.substring(1, 100);
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

  console.log(notificationsVisible)

  const styling = {
    top : dropdownConfig().height + 'px',
    width: dropdownConfig().width + 'px',
    height: 'calc(100vh - '+ dropdownConfig().height +'px)'
  }

  const openEffect = () => {
    setTimeout(() => {
        setNotifyClass(notificationsVisible ? "open" : "close");
    }, 200);
  }

  useEffect(() => {
    dropdownConfig()
    return () => {
      dropdownConfig()
    }
  }, [dropdownConfig])

  return (
    <div className={`notify ${notifyClass}`}>
      <BellIcon
        onClick={() => { setNotificationsVisible((prevState) => !prevState); openEffect()}}
      />
      {!!notificationsVisible && (
        
          <div className="notify-dropdown" style={styling}>
            <Header />
            <Notifications />
          </div>
        
      )} 
    </div>
  );
};
