import React, {FunctionComponent} from 'react';
import InfiniteScroll from 'react-infinite-scroll-component';

import {Notification} from './_Notifications';
import {NotificationType} from '../lib/type';

interface NotificationsProps {
  notifications: NotificationType[];
  getFetchNotifications: () => void;
}

export const Notifications: FunctionComponent<NotificationsProps> = ({
  notifications,
  getFetchNotifications
}) => {
  return (
    <div className="notify-body" id="notification-ul">
      <div className="notification-ul">
        <InfiniteScroll
          // height={'calc(100vh - 100px)'}
          dataLength={notifications.length}
          hasMore={true}
          loader={''}
          next={getFetchNotifications}
          scrollableTarget="notification-ul"
          className="InfiniteScroll"
          style={{overflow: 'initial'}}
        >
          {notifications.map((notification, index) => {
            return <Notification key={index} {...notification} />;
          })}
        </InfiniteScroll>
      </div>
    </div>
  );
};
