import React, {useState,useEffect} from 'react';

import {Notifications} from '../features/Notifications';
import {Header, BellIcon} from './_HomePage';
import { SSL_OP_SSLEAY_080_CLIENT_DH_BUG } from 'constants';
import { AlertForRemove } from '../features/NotificationAlerts';


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
  const [unClear,setClear] = useState(false);
  const [clearAllConfirm,setClearAllConfirm] = useState(false);

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
        return <div>No Data Found!</div>;
      }else{
        return <AlertForRemove handleClearVerification={clearAllVerification}/>;
      }
      
    }
  }

  return (
    <div className={`notify ${notifyClass}`}>
      <BellIcon
        onClick={() => { setNotificationsVisible((prevState) => !prevState); openEffect()}}
      />
      {!!notificationsVisible && (
        
          <div className="notify-dropdown" style={styling}>
            <Header handleClear={clearAll} />            
            { notifying() }                        
          </div>
        
      )} 
    </div>
  );
};
