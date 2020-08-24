import React, {FunctionComponent, useState, useEffect} from 'react';
import InfiniteScroll from 'react-infinite-scroll-component';
import _ from 'lodash';

import {Notification} from './_Notifications';
import {NotificationType, TimersType} from '../lib/type';

interface NotificationsProps {
  timers: TimersType[];
  notifications: NotificationType[];
  getFetchNotifications: () => void;
  notificationsVisible: boolean;
  receivedNewNotification: boolean;
  removeNotification: (id: number) => void;
  setTimers: React.Dispatch<React.SetStateAction<TimersType[] | undefined>>;
}

export const Notifications: FunctionComponent<NotificationsProps> = ({
  timers,
  notifications,
  getFetchNotifications,
  notificationsVisible,
  receivedNewNotification,
  removeNotification,
  setTimers
}) => {
  const [showToast, setShowToast] = useState(false);

  useEffect(() => {
    if (receivedNewNotification === true && notificationsVisible === true) {
      setShowToast(true);
    }
  }, [receivedNewNotification, notificationsVisible]);

  const clearTimeOut = (id: number, timers: TimersType[]) => {
    const timer = timers.find((timer) => timer.id === id);

    if (timer) {
      clearTimeout(timer.timer);

      const clonedTimers = _.cloneDeep(timers);
      const filtredTiemrs = clonedTimers.filter((timer) => timer.id !== id);

      setTimers(filtredTiemrs);
    }
  };

  return (
    <div className="notify-body" id="notification-ul">
      <div className="notification-ul">
        {showToast === true && (
          <div>
            <h1>See New Notifications</h1>
          </div>
        )}
        <InfiniteScroll
          dataLength={notifications.length}
          hasMore={true}
          loader={''}
          next={getFetchNotifications}
          scrollableTarget="notification-ul"
          className="InfiniteScroll"
          style={{overflow: 'initial'}}
        >
          {notifications.map((notification, index) => {
            const {id} = notification;

            return (
              <Notification
                removeNotification={() => removeNotification(id)}
                clearTimeOut={clearTimeOut}
                timers={timers}
                notificationId={id}
                key={index}
                notification={notification}
              />
            );
          })}
        </InfiniteScroll>
      </div>
    </div>
  );
};
