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
  useDeleteNotification
} from '../features/Notifications/hooks';
import {useNotificationsReducer} from '../features/Notifications/reducers/useNotificationsReducer';

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
    timers,
    showToss,
    isNewNotification
  } = state;

  const refContainerSidebar = useRef<HTMLDivElement>(null);

  useHandleClickOutside({
    refContainerSidebar,
    dispatch
  });

  const {getFetchNotifications, lastId} = useFetchNotifications({
    http,
    dispatch,
    notifications
  });

  const lastIdRef = useRef(lastId);

  useEffect(() => {
    lastIdRef.current = lastId;
  });

  const openEffect = useCallback(() => {
    dispatch({
      type: 'UPDATE_STATE',
      state: {
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
        type: 'UPDATE_STATE',
        state: {notificationsVisible: !notificationsVisible}
      });
    } else {
      openEffect();

      setTimeout(() => {
        dispatch({
          type: 'UPDATE_STATE',
          state: {notificationsVisible: !notificationsVisible}
        });
      }, 500);
    }
  };

  const getUnseenNotificationsCount = useCallback(async () => {
    try {
      const {data} = await http.get<number>(
        '/api/Notification/notification/GetCount'
      );

      dispatch({
        type: 'UPDATE_STATE',
        state: {unSeenNotificationsCount: data}
      });
    } catch (error) {
      console.warn(error);
    }
  }, [dispatch, http]);

  const deleteAllNotifications = async () => {
    try {
      await http.put('/api/Notification/notification/DeleteAll', null);

      dispatch({
        type: 'DELETE_ALL_NOTIFICATIONS'
      });
    } catch (error) {
      console.warn(error);
    }
  };

  useNotificationSeen({
    http,
    notifications,
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

  const {deleteNotification} = useDeleteNotification({
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

  const onDeleteAll = (): void =>
    dispatch({
      type: 'UPDATE_STATE',
      state: {confirmDeleteAll: true, showToss: false}
    });

  const cancelDeleteAllNotifications = (): void =>
    dispatch({
      type: 'UPDATE_STATE',
      state: {confirmDeleteAll: false}
    });

  const showClearAllButton: boolean =
    !!notifications && notifications.length > 0 && confirmDeleteAll === false;

  return (
    <div className={`notify`} ref={refContainerSidebar}>
      <BellIcon
        onClick={toggleNotificationSidebar}
        notificationsCounter={unSeenNotificationsCount!}
      />
      {!!notificationsVisible && (
        <div className={`notify-dropdown ${notifyClass}`}>
          <Header
            showClearAllButton={showClearAllButton}
            onDeleteAll={onDeleteAll}
          />
          {confirmDeleteAll === true && (
            <ConfirmDeleteAll
              onYes={deleteAllNotifications}
              onNo={cancelDeleteAllNotifications}
            />
          )}
          {!notifications ? (
            <LoadingSpinner />
          ) : (
            <Notifications
              timers={timers || []}
              removeNotification={deleteNotification}
              receivedNewNotification={receivedNewNotification!}
              notificationsVisible={notificationsVisible}
              notifications={notifications}
              getFetchNotifications={() => getFetchNotifications(lastId)}
              readAllNotificationsForDocument={readAllNotificationsForDocument}
              showToss={showToss}
              dispatch={dispatch}
              deleteAll={confirmDeleteAll}
              isNewNotification={isNewNotification}
            />
          )}
        </div>
      )}
    </div>
  );
};
