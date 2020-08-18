import React, {FunctionComponent} from 'react';
import {Link} from 'react-router-dom';

import uploadedFile from './../assets/icons/uploaded-file.svg';
import calendar from './../assets/icons/calendar.svg';
import close from './../assets/icons/close.svg';
import {NotificationType} from '../lib/type';
import {formatDateTime} from '../lib/utils';

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
  } = payload;

  return (
    <li
      className={`notification-list ${status === 'Unread' ? 'unSeenList' : ''}`}
    >
      <div className="n-wrap">
        <div className="n-icon">
          <Link to={link} target="_blank">
            <img src={uploadedFile} alt="" />
          </Link>
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
            <img src={calendar} alt="" /> {formatDateTime(dateTime)}
          </div>
        </div>
      </div>
      <div className="n-close">
        <img src={close} alt="" title="Close" />
      </div>
    </li>
  );
};
