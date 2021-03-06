import React, {FunctionComponent, useEffect, createRef, Dispatch} from 'react';
import InfiniteScroll from 'react-infinite-scroll-component';

import {
  Notification,
  NewNotificationToss,
  AlertForNoData
} from './_Notifications';
import {NotificationType, TimersType} from '../../lib/types';
import {Actions} from './reducers/useNotificationsReducer';

interface NotificationsProps {
  timers: TimersType[];
  notifications: NotificationType[];
  getFetchNotifications: () => void;
  notificationsVisible: boolean;
  receivedNewNotification: boolean;
  removeNotification: (id: number) => void;
  readAllNotificationsForDocument: (loanApplicationId: string) => Promise<void>;
  showToss: boolean;
  dispatch: Dispatch<Actions>;
  deleteAll: boolean;
}

export const Notifications: FunctionComponent<NotificationsProps> = (props) => {
  const {
    timers,
    notifications,
    getFetchNotifications,
    notificationsVisible,
    receivedNewNotification,
    removeNotification,
    readAllNotificationsForDocument,
    showToss,
    dispatch,
    deleteAll
  } = props;
  const notificationRef = createRef<HTMLDivElement>();

  useEffect(() => {
    if (receivedNewNotification === true && notificationsVisible === true) {
      dispatch({
        type: 'UPDATE_STATE',
        state: {showToss: true}
      });

      receivedNewNotification === true &&
        dispatch({
          type: 'UPDATE_STATE',
          state: {receivedNewNotification: false}
        });
    }
  }, [dispatch, receivedNewNotification, notificationsVisible]);

  const clearTimeOut = (id: number, timers: TimersType[]) => {
    const timer = timers.find((timer) => timer.id === id);

    if (timer) {
      clearTimeout(timer.timer);

      dispatch({type: 'RESET_DELETE_TIMERS', timerId: id});
    }
  };

  const handleScrollToTop = () => {
    !!notificationRef.current && notificationRef.current.scrollTo(0, 0);
    dispatch({
      type: 'UPDATE_STATE',
      state: {showToss: false}
    });
  };

  return notifications.length > 0 ? (
    <section
      className={`notify-content ${deleteAll ? '' : ' animated2 fadeIn'}`}
    >
      <NewNotificationToss
        showToss={showToss}
        handleScrollToTop={handleScrollToTop}
      />
      <div className="notify-body " id="notification-ul" ref={notificationRef}>
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
            {notifications.map((notification) => {
              const {id} = notification;

              return (
                <Notification
                  key={id}
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
