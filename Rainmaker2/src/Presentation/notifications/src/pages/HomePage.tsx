import React, {
  useEffect,
  FunctionComponent,
  useRef,
  useCallback,
  useMemo
} from 'react';
import {Http} from 'rainsoft-js';

import {Notifications} from '../features/Notifications/Notifications';
import {Header, BellIcon, ConfirmDeleteAll, LoadingSpinner} from './_HomePage';
import {
  useFetchNotifications,
  useHandleClickOutside,
  useNotificationSeen,
  useSignalREvents,
  useReadAllNotificationsForDocument,
  useRemoveNotification
} from '../features/Notifications/hooks';
import {
  useNotificationsReducer,
  ACTIONS
} from '../features/Notifications/reducers/useNotificationsReducer';

export const HomePage: FunctionComponent = () => {
  const http = useMemo(() => new Http(), []);

  const {state, dispatch} = useNotificationsReducer();
  const {
    notifications,
    notifyClass,
    confirmDeleteAll,
    receivedNewNotification,
    notificationsVisible,
    unSeenNotificationsCount,
    timers
  } = state;

  const notificationsVisibleRef = useRef(notificationsVisible);
  const unSeenNotificationsCountRef = useRef(unSeenNotificationsCount);
  const refContainerSidebar = useRef<HTMLDivElement>(null);

  useEffect(() => {
    lastIdRef.current = lastId;
    notificationsVisibleRef.current = notificationsVisible;
    unSeenNotificationsCountRef.current = unSeenNotificationsCount;
  });

  useHandleClickOutside({
    refContainerSidebar,
    dispatch
  });

  const {getFetchNotifications, lastId} = useFetchNotifications(
    http,
    dispatch,
    notifications
  );
  const lastIdRef = useRef(lastId);

  const openEffect = useCallback(() => {
    dispatch({
      type: ACTIONS.UPDATE_STATE,
      payload: {
        notifyClass: notificationsVisible
          ? 'animated slideOutRight'
          : 'animated slideInRight'
      }
    });
  }, [dispatch, notificationsVisible]);

  const toggleNotificationSidebar = () => {
    if (notificationsVisible === false) {
      openEffect();

      dispatch({
        type: ACTIONS.UPDATE_STATE,
        payload: {notificationsVisible: !notificationsVisible}
      });
    } else {
      openEffect();

      setTimeout(() => {
        dispatch({
          type: ACTIONS.UPDATE_STATE,
          payload: {notificationsVisible: !notificationsVisible}
        });
      }, 10);
    }
  };

  const getUnseenNotificationsCount = useCallback(async () => {
    try {
      const {data} = await http.get<number>(
        '/api/Notification/notification/GetCount'
      );

      dispatch({
        type: ACTIONS.UPDATE_STATE,
        payload: {unSeenNotificationsCount: data}
      });
    } catch (error) {
      console.warn(error);
    }
  }, [dispatch, http]);

  const onCDeleteAllNotifications = async () => {
    try {
      await http.put('/api/Notification/notification/DeleteAll', null);

      dispatch({
        type: ACTIONS.RESET_NOTIFICATIONS,
        payload: {notifications: []}
      });
    } catch (error) {
      console.warn('error', error);
    }
  };

  const deleteAllNotifications = async () => {
    try {
      await onCDeleteAllNotifications();

      dispatch({
        type: ACTIONS.UPDATE_STATE,
        payload: {confirmDeleteAll: false}
      });
    } catch (error) {
      console.warn(error);
    }
  };

  useNotificationSeen({
    http,
    notifications,
    dispatch,
    notificationsVisible
  });

  const {readAllNotificationsForDocument} = useReadAllNotificationsForDocument({
    http,
    notifications
  });

  useSignalREvents({
    getFetchNotifications,
    getUnseenNotificationsCount,
    notifications,
    notificationsVisible,
    dispatch
  });

  const {removeNotification} = useRemoveNotification({
    notifications,
    http,
    dispatch,
    timers
  });

  useEffect(() => {
    if (!notifications) return;

    if (notifications.length === 6 && lastIdRef.current !== -1) {
      getFetchNotifications(lastIdRef.current);
    }
  }, [notifications, getFetchNotifications]);

  return (
    <div className={`notify`} ref={refContainerSidebar}>
      <BellIcon
        onClick={toggleNotificationSidebar}
        notificationsCounter={unSeenNotificationsCount!}
      />
      {!!notificationsVisible && (
        <div className={`notify-dropdown ${notifyClass}`}>
          <Header
            showClearAllButton={
              !!notifications &&
              notifications.length > 0 &&
              confirmDeleteAll === false
            }
            onDeleteAll={() =>
              dispatch({
                type: ACTIONS.UPDATE_STATE,
                payload: {confirmDeleteAll: true}
              })
            }
          />
          {confirmDeleteAll === true && (
            <ConfirmDeleteAll
              onYes={deleteAllNotifications}
              onNo={() =>
                dispatch({
                  type: ACTIONS.UPDATE_STATE,
                  payload: {confirmDeleteAll: false}
                })
              }
            />
          )}
          {!notifications ? (
            <LoadingSpinner />
          ) : (
            <Notifications
              timers={timers || []}
              removeNotification={removeNotification}
              receivedNewNotification={receivedNewNotification!}
              notificationsVisible={notificationsVisible}
              notifications={notifications}
              getFetchNotifications={() => getFetchNotifications(lastId)}
              readAllNotificationsForDocument={readAllNotificationsForDocument}
              dispatch={dispatch}
            />
          )}
        </div>
      )}
    </div>
  );
};
