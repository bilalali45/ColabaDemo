import React, {FunctionComponent} from 'react';
import {SVGToggle, SVGNoBell, SVGBellSleep} from '../SVGIcons';

interface HeaderProps {
  onDeleteAll: () => void;
  showClearAllButton: boolean;
}

export const Header: FunctionComponent<HeaderProps> = ({
  onDeleteAll,
  showClearAllButton
}) => {
  return (
    <div className="notify-header">
      <h2>Notifications</h2>
      {showClearAllButton === true && (
        <button className="notify-btn-clear" onClick={onDeleteAll}>
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
  onYes: () => void;
  onNo: () => void;
}

export const ConfirmDeleteAll: FunctionComponent<AlertForRemoveProps> = ({
  onYes,
  onNo
}) => {
  return (
    <div className="notify-alert-msg">
      <div className="notify-alert-msg--wrap">
        <SVGNoBell />
        <h4>Are you sure you want to remove all notifications?</h4>
        <p>
          <button onClick={onNo} className="btn-notify secondry">
            No
          </button>
          <button onClick={onYes} className="btn-notify primary">
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

export const NotifyLoading: FunctionComponent = () => {
  return (
    <div className="notify-loading">
      <span className="notify-loading--circle"></span>
      <span className="notify-loading--circle-bold"></span>
    </div>
  );
};
