import React, {
  useState,
  useEffect,
  FunctionComponent,
  useRef,
  useCallback
} from 'react';
import {SignalRHub} from 'rainsoft-js';

import {Notifications} from '../features/Notifications';
import {Header, BellIcon} from './_HomePage';
import {AlertForRemove, AlertForNoData} from '../features/NotificationAlerts';
import {apiV1} from '../lib/api';
import {NotificationType} from '../lib/type';
import {LocalDB} from '../Utils/LocalDB';

export const HomePage: FunctionComponent = () => {
  const [notificationsVisible, setNotificationsVisible] = useState(false);
  const [notifications, setNotifications] = useState<NotificationType[]>([]);
  /**
   * This is last Id of notification inside notifications array on every API hit.
   * This needs to be send with API Call to fetch previous notifications on scroll.
   * -1 is the default value
   */
  const [lastId, setLastId] = useState(-1);
  const lastIdRef = useRef(lastId);
  const [notifyClass, setNotifyClass] = useState('close');
  const [unClear, setClear] = useState(false);
  const [clearAllConfirm, setClearAllConfirm] = useState(false);
  const refContainerSidebar = useRef<HTMLDivElement>(null);

  const openEffect = useCallback(() => {
    setNotifyClass(
      notificationsVisible ? 'animated slideOutRight' : 'animated slideInRight'
    );
  }, [notificationsVisible]);

  useEffect(() => {
    const closeSidebar = (event: any) => {
      if (
        !event.target?.className?.includes('btn-notify') &&
        refContainerSidebar.current &&
        !refContainerSidebar.current.contains(event.target)
      ) {
        openEffect();
        setNotificationsVisible(false);
      }
    };

    document.addEventListener('click', closeSidebar);
    return () => {
      document.removeEventListener('click', closeSidebar);
    };
  }, [openEffect]);

  const toggleNotificationSidebar = () => {
    if (notificationsVisible === false) {
      openEffect();
      setNotificationsVisible(!notificationsVisible);
    } else {
      openEffect();
      setTimeout(() => {
        setNotificationsVisible(!notificationsVisible);
      }, 50);
    }
  };

  useEffect(() => {
    lastIdRef.current = lastId;
  });

  const getFetchNotifications = useCallback(async (lastId: number) => {
    try {
      const {data: response} = await apiV1.get<NotificationType[]>(
        '/api/Notification/notification/GetPaged',
        {
          params: {
            pageSize: 10,
            lastId,
            mediumId: 1
          }
        }
      );

      if (response.length > 0) {
        setLastId(response[response.length - 1].id);
        setNotifications((prevNotifications) =>
          prevNotifications.concat(response)
        );
      }
    } catch (error) {
      console.warn('error', error);
    }
  }, []);

  const onClearNotifications = async () => {
    try {
      await apiV1.put('/api/Notification/notification/DeleteAll');

      setNotifications([]);
    } catch (error) {
      console.warn('error', error);
    }
  };

  useEffect(() => {
    getFetchNotifications(lastIdRef.current);
  }, [getFetchNotifications]);

  const clearAll = () => {
    setClear(true);
  };

  const clearAllVerification = async (verify: boolean) => {
    if (verify === true) {
      await onClearNotifications();
      setClearAllConfirm(true);
      setClear(true);
    } else {
      setClearAllConfirm(false);
      setClear(false);
    }
  };

  const notifying = (notifications: NotificationType[], lastId: number) => {
    if (notifications.length === 0) {
      return <AlertForNoData />;
    } else if (unClear === false && clearAllConfirm === false) {
      return (
        <Notifications
          notifications={notifications}
          getFetchNotifications={() => getFetchNotifications(lastId)}
        />
      );
    } else {
      if (clearAllConfirm === true) {
        return <AlertForNoData />;
      } else {
        return (
          <AlertForRemove handleClearVerification={clearAllVerification} />
        );
      }
    }
  };

  const eventsRegister = () => {
    console.log(
      'signalR eventsRegister on Client',
      SignalRHub.hubConnection.connectionState
    );
    SignalRHub.hubConnection.on('TestSignalR', (data: string) => {
      console.log(`TestSignalR`, data);
    });
    SignalRHub.hubConnection.on('SendNotification', (notification: any) => {
      console.log(
        'Notification comes from SignalR on Client',
        JSON.parse(notification)
      );
    });

    SignalRHub.hubConnection.onclose((e: any) => {
      console.log(`SignalR disconnected on Client`, e);
      const auth = LocalDB.getAuthToken();
      if (auth) {
        SignalRHub.signalRHubResume();
      }
    });
  };

  useEffect(() => {
    const accessToken: string = LocalDB.getAuthToken() || '';
    SignalRHub.configureHubConnection(
      window.envConfig.API_BASE_URL + '/serverhub',
      accessToken,
      eventsRegister
    );
  }, []);

  return (
    <div className={`notify`} ref={refContainerSidebar}>
      <BellIcon
        onClick={toggleNotificationSidebar}
        notificationsCounter={
          notifications.filter(
            (notification) => notification.status === 'Unread'
          ).length
        }
      />
      {!!notificationsVisible && (
        <div className={`notify-dropdown ${notifyClass}`}>
          <Header
            clearAllDisplay={notifications.length > 0}
            handleClear={clearAll}
          />
          {notifying(notifications, lastId)}
        </div>
      )}
    </div>
  );
};
