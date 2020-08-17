import React from 'react';
import { dropdownConfig } from './HomePage';


export const Header = () => {
  return (
    <div className="notify-header">
      <h2>Notifications</h2>
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
