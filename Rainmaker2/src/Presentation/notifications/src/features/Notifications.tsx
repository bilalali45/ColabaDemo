import React, {FunctionComponent} from 'react';

import {Notification} from './_Notifications';
import {NotificationType} from '../lib/type';

interface NotificationsProps {
  notifications: NotificationType[];
}

export const Notifications: FunctionComponent<NotificationsProps> = ({
  notifications
}) => {
  return (
    <div className="notify-body">
      <ul className="notification-ul">
        {notifications.map((notification, index) => {
          return <Notification key={index} {...notification} />;
        })}
      </ul>
    </div>
  );
};
