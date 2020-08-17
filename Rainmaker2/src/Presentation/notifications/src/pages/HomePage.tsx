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
  const [notificationsVisible, setNotificationsVisible] = useState(false);

  const styling = {
    top : dropdownConfig().height + 'px',
    width: dropdownConfig().width + 'px',
    height: 'calc(100vh - '+ dropdownConfig().height +'px)'
  }

  useEffect(() => {
    dropdownConfig()
    return () => {
      dropdownConfig()
    }
  }, [dropdownConfig])

  return (
    <div className="notify">
      <BellIcon
        onClick={() => { setNotificationsVisible((prevState) => !prevState);}}
      />
      {!!notificationsVisible && (
        
          <div className="notify-dropdown animated slideInRight" style={styling}>
            <Header />
            <Notifications />
          </div>
        
      )} 
    </div>
  );
};
