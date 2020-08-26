import React, { FunctionComponent, useState, useEffect, useRef } from 'react';
import InfiniteScroll from 'react-infinite-scroll-component';
import _ from 'lodash';

import { Notification } from './_Notifications';
import { NotificationType, TimersType } from '../lib/type';

interface NotificationsProps {
  timers: TimersType[];
  notifications: NotificationType[];
  getFetchNotifications: () => void;
  notificationsVisible: boolean;
  receivedNewNotification: boolean;
  removeNotification: (id: number) => void;
  setTimers: React.Dispatch<React.SetStateAction<TimersType[] | undefined>>;
  readAllNotificationsForDocument: (loanApplicationId: string) => void;
}

export const Notifications: FunctionComponent<NotificationsProps> = ({
  timers,
  notifications,
  getFetchNotifications,
  notificationsVisible,
  receivedNewNotification,
  removeNotification,
  setTimers,
  readAllNotificationsForDocument
}) => {
  const [showToast, setShowToast] = useState(false); //apex false
  const notificationRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (receivedNewNotification === true && notificationsVisible === true) {
      setShowToast(true);
    }
  }, [receivedNewNotification, notificationsVisible]);

  useEffect(() => {
    if (notificationsVisible) {
      setShowToast(true);
    }
  }, [notifications?.length]);

  useEffect(() => {
    setShowToast(false);
  }, [notificationsVisible === false]);


  const clearTimeOut = (id: number, timers: TimersType[]) => {
    const timer = timers.find((timer) => timer.id === id);

    if (timer) {
      clearTimeout(timer.timer);

      const clonedTimers = _.cloneDeep(timers);
      const filtredTiemrs = clonedTimers.filter((timer) => timer.id !== id);

      setTimers(filtredTiemrs);
    }
  };

  const handleScrollToTop = () => {
    notificationRef.current!.scrollTo(0, 0);
    setShowToast(false);
  };

  return (
    <section className="notify-content">
      {showToast === true && (
        <div className="notify-toast">
          <div
            className="notify-toast-alert animated fadeIn"
            onClick={handleScrollToTop}
          >
            See New Notifications
          </div>
        </div>
      )}
      <div className="notify-body" id="notification-ul" ref={notificationRef}>
        <div className="notification-ul">
          <InfiniteScroll
            dataLength={notifications.length}
            hasMore={true}
            loader={''}
            next={getFetchNotifications}
            scrollableTarget="notification-ul"
            className="InfiniteScroll"
            style={{ overflow: 'initial' }}
          >
            {notifications.map((notification, index) => {
              const { id } = notification;

              return (
                <Notification
                   key={index}
                  removeNotification={() => removeNotification(id)}
                  clearTimeOut={clearTimeOut}
                  timers={timers}
                  notification={notification}
                  readAllNotificationsForDocument={
                    readAllNotificationsForDocument
                  }
                />
              );
            })}
          </InfiniteScroll>
        </div>
      </div>
    </section>
  );
};
