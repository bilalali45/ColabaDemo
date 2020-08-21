import React, {FunctionComponent} from 'react';
import {Link} from 'react-router-dom';

import {NotificationType, TimersType} from '../lib/type';
import {formatDateTime} from '../lib/utils';
import {SVGDocument, SVGClose, SVGCalender} from '../SVGIcons';

interface NotificationProps {
  removeNotification: () => void;
  notification: NotificationType;
  notificationId: number;
  timers: TimersType[];
  clearTimeOut: (id: number, timers: TimersType[]) => void;
}

export const Notification: FunctionComponent<NotificationProps> = ({
  removeNotification,
  clearTimeOut,
  timers,
  notification
}) => {
  const {
    id,
    payload: {
      data: {
        address,
        notificationType,
        name,
        city,
        state,
        zipCode,
        unitNumber,
        dateTime
      },
      meta: {link}
    }
  } = notification;

  return (
    <div
      className={`notification-list ${
        status === 'Unread' || status === 'Unseen' ? 'unSeenList' : ''
      }`}
    >
      {timers.some((item) => item.id === notification.id) ? (
        <div className="notification-list-item-remove">
          <span className="n-alert-text">
            This notification has been removed.
          </span>
          <button className="btn-undo" onClick={() => clearTimeOut(id, timers)}>
            Undo
          </button>
        </div>
      ) : (
        <div>
          <Link className="n-wrap" to={link} target="_blank">
            <div className="n-icon">
              <SVGDocument />
            </div>
            <div className="n-content">
              <div className="n-cat" title={'Document Submission'}>
                {`${id} ${notificationType}`}
              </div>
              <h4 className="n-title">{name}</h4>
              <p className="n-address">
                {address} # {unitNumber} <br />
                {city}, {state} {zipCode}
              </p>
              <div className="n-date">
                <SVGCalender />
                {formatDateTime(dateTime, 'MMM, DD, YYYY hh:mm A')}
              </div>
            </div>
          </Link>
          <div className="n-close" onClick={removeNotification}>
            <SVGClose />
          </div>
        </div>
      )}
    </div>
  );
};
