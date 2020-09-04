import React, {FunctionComponent} from 'react';
import {Link} from 'react-router-dom';

import {NotificationType, TimersType} from '../../lib/type';
import {formatDateTime} from '../../lib/utils';
import {SVGDocument, SVGClose, SVGCalender} from '../../assets/icons/SVGIcons';

interface NotificationProps {
  removeNotification: () => void;
  notification: NotificationType;
  timers: TimersType[];
  clearTimeOut: (id: number, timers: TimersType[]) => void;
  readAllNotificationsForDocument: (loanApplicationId: string) => Promise<void>;
}

export const Notification: FunctionComponent<NotificationProps> = ({
  removeNotification,
  clearTimeOut,
  timers,
  notification,
  readAllNotificationsForDocument
}) => {
  const {
    status,
    id,
    payload: {
      data: {
        loanApplicationId,
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

  const readDocumentsAndOpenLink = async (
    loanApplicationId: string,
    link: string,
    notificationStatus: string
  ) => {
    try {
      //Prevent unecessary API call
      if (['Unseen', 'Unread', 'Seen'].includes(notificationStatus)) {
        await readAllNotificationsForDocument(loanApplicationId);
      }
    } catch (error) {
      console.warn(error);
    }

    window.open(link, '_self');
  };

  return (
    <div
      className={`notification-list ${
        ['Unseen', 'Unread', 'Seen'].includes(notification.status)
          ? 'unSeenList'
          : ''
      }`}
    >
      {timers.some((timer) => timer.id === notification.id) ? (
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
          <Link
            className="n-wrap"
            onClick={() =>
              readDocumentsAndOpenLink(loanApplicationId, link, status)
            }
            to="#"
          >
            <div className="n-icon">
              <SVGDocument />
            </div>
            <div className="n-content">
              <div className="n-cat" title={'Document Submission'}>
                {`${notificationType}`}
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

export const NewNotificationToss: FunctionComponent<{
  showToss: boolean;
  handleScrollToTop: () => void;
}> = ({showToss, handleScrollToTop}) => {
  return showToss === true ? (
    <div className="notify-toast">
      <div
        className="notify-toast-alert animated fadeIn"
        onClick={handleScrollToTop}
      >
        See New Notifications
      </div>
    </div>
  ) : (
    <></>
  );
};