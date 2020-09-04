import React, {
  FunctionComponent,
  useState,
  useEffect,
  createRef,
  Dispatch
} from 'react';
import InfiniteScroll from 'react-infinite-scroll-component';

import {Notification, NewNotificationToss} from './_Notifications';
import {NotificationType, TimersType} from '../../lib/type';
import {AlertForNoData} from '../../pages/_HomePage';
import {Params, ACTIONS} from './reducers/useNotificationsReducer';

interface NotificationsProps {
  timers: TimersType[];
  notifications: NotificationType[];
  getFetchNotifications: () => void;
  notificationsVisible: boolean;
  receivedNewNotification: boolean;
  removeNotification: (id: number) => void;
  readAllNotificationsForDocument: (loanApplicationId: string) => Promise<void>;
  dispatch: Dispatch<Params>;
  DeleteAll: boolean;
}

export const Notifications: FunctionComponent<NotificationsProps> = ({
  timers,
  notifications,
  getFetchNotifications,
  notificationsVisible,
  receivedNewNotification,
  removeNotification,
  readAllNotificationsForDocument,
  dispatch,
  DeleteAll
}) => {
  const [showToss, setShowToss] = useState(false); //apex false
  const notificationRef = createRef<HTMLDivElement>();

  useEffect(() => {
    if (receivedNewNotification === true && notificationsVisible === true) {
      setShowToss(true);

      receivedNewNotification === true &&
        dispatch({
          type: ACTIONS.UPDATE_STATE,
          payload: {receivedNewNotification: false}
        });
    }
  }, [dispatch, receivedNewNotification, notificationsVisible]);

  const clearTimeOut = (id: number, timers: TimersType[]) => {
    const timer = timers.find((timer) => timer.id === id);

    if (timer) {
      clearTimeout(timer.timer);

      dispatch({type: ACTIONS.RESET_DELETE_TIMERS, payload: {timerId: id}});
    }
  };

  const handleScrollToTop = () => {
    !!notificationRef.current && notificationRef.current.scrollTo(0, 0);
    setShowToss(false);
  };

  return notifications.length > 0 ? (
    <section
      className={`notify-content ${DeleteAll ? '' : ' animated2 fadeIn'}`}
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
