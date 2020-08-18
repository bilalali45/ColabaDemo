import React from 'react';
import { dropdownConfig } from './HomePage';
import { SVGToggle } from '../SVGIcons';
import { types } from 'util';

interface  propsData{
  handleClear: Function
}

export const Header = ({handleClear}:propsData) => {

  return (
    <div className="notify-header">
      <h2>Notifications</h2>
      <button className="notify-btn-clear" onClick={(e) => handleClear(e)}>Clear all <SVGToggle/></button>
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
