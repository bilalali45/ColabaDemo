import React, {FunctionComponent} from 'react';
import {SVGToggle} from '../SVGIcons';

interface HeaderProps {
  handleClear: () => void;
  clearAllDisplay: boolean;
}

export const Header: FunctionComponent<HeaderProps> = ({
  handleClear,
  clearAllDisplay
}) => {
  return (
    <div className="notify-header">
      <h2>Notifications</h2>
      {clearAllDisplay && (
        <button className="notify-btn-clear" onClick={handleClear}>
          Clear all <SVGToggle />
        </button>
      )}
    </div>
  );
};

export const BellIcon: FunctionComponent<{
  onClick: () => void;
  notificationsCounter: number;
}> = ({onClick, notificationsCounter}) => {
  return (
    <div className="notify-tigger-area">
      <button onClick={onClick} className="btn-notify">
        <i className="zmdi zmdi-notifications"></i>
        <span className="notify-counts">{notificationsCounter}</span>
      </button>
    </div>
  );
};
