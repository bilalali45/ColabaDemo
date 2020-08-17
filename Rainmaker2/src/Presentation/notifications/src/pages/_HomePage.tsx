import React from 'react';
import { dropdownConfig } from './HomePage';
import toggle from './../assets/icons/toggle.svg';


export const Header = () => {
  const ClearNotification = () => {
    console.log('ClearNotification');
  }

  return (
    <div className="notify-header">
      <h2>Notifications</h2>
      <button className="notify-btn-clear" onClick={ClearNotification}>Clear all <img src={toggle} /></button>
    </div>
  );
};

export const BellIcon = ({onClick}: {onClick: () => void}) => {

  const styling = {
    height : dropdownConfig().height + 'px',
    width : dropdownConfig().width + 'px'
  }

  return (
    <div className="notify-tigger-area" style={styling}>
      <button onClick={onClick} className="btn-notify"><i className="zmdi zmdi-notifications"></i></button>
    </div>    
  );
};
