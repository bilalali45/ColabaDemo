import React, {FunctionComponent, useState, useEffect, createRef} from 'react';
import InfiniteScroll from 'react-infinite-scroll-component';
import _ from 'lodash';

import {Notification, NewNotificationToss} from './_Notifications';
import {NotificationType, TimersType} from '../lib/type';
import {AlertForNoData} from '../pages/_HomePage';

interface NotificationsProps {
  timers: TimersType[];
  notifications: NotificationType[];
  getFetchNotifications: () => void;
  notificationsVisible: boolean;
  receivedNewNotification: boolean;
  removeNotification: (id: number) => void;
  setTimers: React.Dispatch<React.SetStateAction<TimersType[] | undefined>>;
  readAllNotificationsForDocument: (loanApplicationId: string) => void;
  setReceivedNewNotification: React.Dispatch<React.SetStateAction<boolean>>;
}

export const Notifications: FunctionComponent<NotificationsProps> = ({
  timers,
  notifications,
  getFetchNotifications,
  notificationsVisible,
  receivedNewNotification,
  removeNotification,
  setTimers,
  readAllNotificationsForDocument,
  setReceivedNewNotification
}) => {
  const [showToss, setShowToss] = useState(false); //apex false
  const notificationRef = createRef<HTMLDivElement>();

  useEffect(() => {
    if (receivedNewNotification === true && notificationsVisible === true) {
      setShowToss(true);

      receivedNewNotification === true && setReceivedNewNotification(false);
    }
  }, [
    receivedNewNotification,
    notificationsVisible,
    setReceivedNewNotification
  ]);

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
    !!notificationRef.current && notificationRef.current.scrollTo(0, 0);
    setShowToss(false);
  };

  return notifications.length > 0 ? (
    <section className="notify-content">
      <NewNotificationToss
        showToss={showToss}
        handleScrollToTop={handleScrollToTop}
      />
      <div className="notify-body" id="notification-ul" ref={notificationRef}>
        <div className="notification-ul">
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
  ) : (
    <AlertForNoData />
  );
};
