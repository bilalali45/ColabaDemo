import React, {FunctionComponent} from 'react';
import {Link} from 'react-router-dom';

import {NotificationType} from '../lib/type';
import {formatDateTime} from '../lib/utils';
import {SVGDocument, SVGClose, SVGCalender} from '../SVGIcons';

export const Notification: FunctionComponent<NotificationType> = ({
  status,
  payload
}) => {
  const {
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
  }: any = payload;

  return (
    <li
      className={`notification-list ${status === 'Unread' ? 'unSeenList' : ''}`}
    >
      <div className="n-wrap">
        <Link to={link} target="_blank">
          <div className="n-icon">
            <SVGDocument />
          </div>
          <div className="n-content">
            <div className="n-cat" title={'Document Submission'}>
              {notificationType}
            </div>
            <h4 className="n-title">{name}</h4>
            <p className="n-address">
              {address} # {unitNumber} <br />
              {city}, {state} {zipCode}
            </p>
            <div className="n-date">
              <SVGCalender />{' '}
              {formatDateTime(dateTime, 'MMM, DD, YYYY hh:mm A')}
            </div>
          </div>
        </Link>
        <div className="n-close">
          <SVGClose />
        </div>
      </div>

      <div className="notification-list-item-remove">
        <span className="n-alert-text">
          This notification has been removed.
        </span>
        <button className="btn-undo">Undo</button>
      </div>
    </li>
  );
};
