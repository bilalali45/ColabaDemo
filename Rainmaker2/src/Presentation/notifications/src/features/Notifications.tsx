import React from 'react';

import { Notification } from './_Notifications';

export const Notifications = () => {
  return (
    <div className="notify-body">
      <ul className="notification-ul">
        <Notification unSeen={true} />
        <Notification unSeen={true} />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
        <Notification />
      </ul>
    </div>
  );
};
