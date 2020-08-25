import React, {FunctionComponent} from 'react';
import {SVGToggle, SVGNoBell, SVGBellSleep} from '../SVGIcons';

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
        {notificationsCounter > 0 && (
          <React.Fragment>
            <span className="notify-counts">{notificationsCounter}</span>
          </React.Fragment>
        )}
      </button>
    </div>
  );
};

interface AlertForRemoveProps {
  handleClearVerification: (value: boolean) => void;
}

export const AlertForRemove: FunctionComponent<AlertForRemoveProps> = ({
  handleClearVerification
}) => {
  return (
    <div className="notify-alert-msg">
      <div className="notify-alert-msg--wrap">
        <SVGNoBell />
        <h4>Are you sure you want to remove all notifications?</h4>
        <p>
          <button
            onClick={() => handleClearVerification(false)}
            className="btn-notify secondry"
          >
            No
          </button>
          <button
            onClick={() => handleClearVerification(true)}
            className="btn-notify primary"
          >
            Yes
          </button>
        </p>
      </div>
    </div>
  );
};

export const AlertForNoData: FunctionComponent = () => {
  return (
    <div className="notify-alert-msg">
      <div className="notify-alert-msg--wrap">
        <SVGBellSleep />
        <h4>No Notifications Yet</h4>
        <p>
          Stay tuned! Notifications about loan applications will show up here.{' '}
        </p>
      </div>
    </div>
  );
};
