import React, {FunctionComponent} from 'react';
import {Link} from 'react-router-dom';

import {NotificationType, TimersType} from '../../lib/types';
import {formatDateTime} from '../../lib/utils';
import {
  SVGDocument,
  SVGClose,
  SVGCalender,
  SVGBellSleep
} from '../../assets/icons/SVGIcons';

interface NotificationProps {
  removeNotification: () => void;
  notification: NotificationType;
  timers: TimersType[];
  clearTimeOut: (id: number, timers: TimersType[]) => void;
  readAllNotificationsForDocument: (loanApplicationId: string) => Promise<void>;
}

export const Notification: FunctionComponent<NotificationProps> = (props) => {
  const {
    removeNotification,
    clearTimeOut,
    timers,
    notification,
    readAllNotificationsForDocument
  } = props;

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
      className={`notification-list animated1 fadeIn ${
        ['Unseen', 'Unread', 'Seen'].includes(notification.status)
          ? 'unSeenList'
          : ''
      }`}
    >
      {timers.some((timer) => timer.id === notification.id) ? (
        <div className="notification-list-item-remove animated2 fadeIn">
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
            className="n-wrap animated2 fadeIn"
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
                {address} {!!unitNumber && `# ${unitNumber}`}{' '}
                {(!!address || !!unitNumber) && '<br />'}
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
