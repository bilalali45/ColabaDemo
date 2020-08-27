import React, {
  useState,
  useEffect,
  FunctionComponent,
  useRef,
  useCallback,
  useMemo
} from 'react';
import {Http} from 'rainsoft-js';

import {Notifications} from '../features/Notifications/Notifications';
import {Header, BellIcon, ConfirmDeleteAll, LoadingSpinner} from './_HomePage';
import {TimersType} from '../lib/type';
import {useFetchNotifications} from '../features/Notifications/hooks/useFetchNotifications';
import {useHandleClickOutside} from '../features/Notifications/hooks/useHandleClickOutside';
import {useNotificationSeen} from '../features/Notifications/hooks/useNotificationSeen';
import {useSignalREvents} from '../features/Notifications/hooks/useSignalREvents';
import {useReadAllNotificationsForDocument} from '../features/Notifications/hooks/useReadAllNotificationsForDocument';
import {useRemoveNotification} from '../features/Notifications/hooks/useRemoveNotification';

export const HomePage: FunctionComponent = () => {
  const [notificationsVisible, setNotificationsVisible] = useState(false);
  const notificationsVisibleRef = useRef(notificationsVisible);
  const [unSeenNotificationsCount, setUnSeenNotificationsCount] = useState(0);
  const unSeenNotificationsCountRef = useRef(unSeenNotificationsCount);
  const [receivedNewNotification, setReceivedNewNotification] = useState(false);
  const http = useMemo(() => new Http(), []);
  const [notifyClass, setNotifyClass] = useState('close');
  const refContainerSidebar = useRef<HTMLDivElement>(null);
  const [timers, setTimers] = useState<TimersType[]>();
  const [confirmDeleteAll, setConfimDeleteAll] = useState(false);

  useHandleClickOutside({
    refContainerSidebar,
    setNotificationsVisible,
    setReceivedNewNotification,
    setConfimDeleteAll
  });
  const {
    setNotifications,
    notifications,
    getFetchNotifications,
    lastId,
    notificationsRef
  } = useFetchNotifications(http);
  const lastIdRef = useRef(lastId);

  useEffect(() => {
    lastIdRef.current = lastId;
    notificationsVisibleRef.current = notificationsVisible;
    unSeenNotificationsCountRef.current = unSeenNotificationsCount;
  });

  const openEffect = useCallback(() => {
    setNotifyClass(
      notificationsVisible ? 'animated slideOutRight' : 'animated slideInRight'
    );
  }, [notificationsVisible]);

  const toggleNotificationSidebar = () => {
    if (notificationsVisible === false) {
      openEffect();

      setNotificationsVisible(!notificationsVisible);
    } else {
      openEffect();

      setTimeout(() => {
        setNotificationsVisible(!notificationsVisible);
      }, 10);
    }
  };

  const getUnseenNotificationsCount = useCallback(async () => {
    try {
      const {data} = await http.get<number>(
        '/api/Notification/notification/GetCount'
      );

      setUnSeenNotificationsCount(data);
    } catch (error) {
      console.warn(error);
    }
  }, [http]);

  const onCDeleteAllNotifications = async () => {
    try {
      await http.put('/api/Notification/notification/DeleteAll', null);

      setNotifications([]);
      setUnSeenNotificationsCount(0);
    } catch (error) {
      console.warn('error', error);
    }
  };

  const deleteAllNotifications = async () => {
    try {
      await onCDeleteAllNotifications();

      setConfimDeleteAll(false);
    } catch (error) {
      console.warn(error);
    }
  };

  useNotificationSeen({
    http,
    notifications,
    setNotifications,
    notificationsVisible,
    setUnSeenNotificationsCount
  });

  const {readAllNotificationsForDocument} = useReadAllNotificationsForDocument({
    http,
    setNotifications,
    notifications
  });

  useSignalREvents({
    getFetchNotifications,
    getUnseenNotificationsCount,
    setUnSeenNotificationsCount,
    setNotifications,
    notificationsRef,
    notificationsVisibleRef,
    setReceivedNewNotification
  });

  const {removeNotification} = useRemoveNotification({
    http,
    setNotifications,
    setTimers,
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
        notificationsCounter={unSeenNotificationsCount}
      />
      {!!notificationsVisible && (
        <div className={`notify-dropdown ${notifyClass}`}>
          <Header
            showClearAllButton={
              !!notifications &&
              notifications.length > 0 &&
              confirmDeleteAll === false
            }
            onDeleteAll={() => setConfimDeleteAll(true)}
          />
          {confirmDeleteAll === true && (
            <ConfirmDeleteAll
              onYes={deleteAllNotifications}
              onNo={() => setConfimDeleteAll(false)}
            />
          )}
          {!notifications ? (
            <LoadingSpinner />
          ) : (
            <Notifications
              timers={timers || []}
              removeNotification={removeNotification}
              receivedNewNotification={receivedNewNotification}
              notificationsVisible={notificationsVisible}
              notifications={notifications}
              getFetchNotifications={() => getFetchNotifications(lastId)}
              setTimers={setTimers}
              readAllNotificationsForDocument={readAllNotificationsForDocument}
              setReceivedNewNotification={setReceivedNewNotification}
            />
          )}
        </div>
      )}
    </div>
  );
};
