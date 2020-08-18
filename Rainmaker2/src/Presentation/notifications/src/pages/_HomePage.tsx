import React, {FunctionComponent} from 'react';

import toggle from './../assets/icons/toggle.svg';

export const Header: FunctionComponent<{onClearNotifications: () => void}> = ({
  onClearNotifications
}) => {
  return (
    <div className="notify-header">
      <h2>Notifications</h2>
      <button className="notify-btn-clear" onClick={onClearNotifications}>
        Clear all <img src={toggle} />
      </button>
    </div>
  );
};

export const BellIcon: FunctionComponent<{onClick: () => void}> = ({
  onClick
}) => {
  return (
    <div className="notify-tigger-area">
      <button onClick={onClick} className="btn-notify">
        <i className="zmdi zmdi-notifications"></i>
      </button>
    </div>
  );
};
