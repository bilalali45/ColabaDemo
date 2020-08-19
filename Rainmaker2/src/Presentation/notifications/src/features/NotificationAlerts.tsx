import React, {FunctionComponent} from 'react';
import {SVGNoBell, SVGBellSleep} from '../SVGIcons';

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
