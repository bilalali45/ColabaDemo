import React, {
  useState,
  useEffect,
  FunctionComponent,
  useRef,
  useCallback,
  useMemo
} from 'react';
import {SignalRHub, Http} from 'rainsoft-js';
import _ from 'lodash';

import {Notifications} from '../features/Notifications';
import {Header, BellIcon} from './_HomePage';
import {AlertForRemove, AlertForNoData} from '../features/NotificationAlerts';
import {NotificationType, TimersType} from '../lib/type';
import {LocalDB} from '../Utils/LocalDB';

export const HomePage: FunctionComponent = () => {
  const [notificationsVisible, setNotificationsVisible] = useState(false);
  const notificationsVisibleRef = useRef(notificationsVisible);
  const [notifications, setNotifications] = useState<NotificationType[]>([]);
  const [unSeenNotificationsCount, setUnSeenNotificationsCount] = useState(0);
  const notificationsRef = useRef(notifications);
  const [receivedNewNotification, setReceivedNewNotification] = useState(false);
  const http = useMemo(() => new Http(), []);
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
  const [timers, setTimers] = useState<TimersType[]>();

  useEffect(() => {
    lastIdRef.current = lastId;
    notificationsRef.current = notifications;
    notificationsVisibleRef.current = notificationsVisible;
  });

  const openEffect = useCallback(() => {
    setNotifyClass(
      notificationsVisible ? 'animated slideOutRight' : 'animated slideInRight'
    );
  }, [notificationsVisible]);

  useEffect(() => {
    const handleClickOutside = (event: any) => {
      if (
        refContainerSidebar.current &&
        !refContainerSidebar.current.contains(event.target)
      ) {
        setNotificationsVisible(() => false);
        setReceivedNewNotification(() => false);
      }
    };

    document.addEventListener('click', handleClickOutside, true);
    return () => {
      document.removeEventListener('click', handleClickOutside, true);
    };
  }, []);

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

  const getFetchNotifications = useCallback(
    async (lastId: number) => {
      try {
        const {data: response} = await http.get<NotificationType[]>(
          `/api/Notification/notification/GetPaged?pageSize=10&lastId=${lastId}&mediumId=1`
        );

        if (response.length > 0) {
          setLastId(response[response.length - 1].id);

          setNotifications((prevNotifications) => {
            /**
             * We are reseting notifications list to 10 notifications on following two conditions
             * 1. if we just logged in
             * 2. if SignalR connection Reset
             */
            return lastId === -1
              ? [...response]
              : prevNotifications.concat(response);
          });
        }
      } catch (error) {
        console.warn('error', error);
      }
    },
    [http]
  );

  const getUnseenNotificationsCount = useCallback(async () => {
    try {
      const resp: any = await http.get(
        '/api/Notification/notification/GetCount'
      );

      setUnSeenNotificationsCount(resp.data);
    } catch (error) {
      console.warn(error);
    }
  }, [http]);

  const onClearNotifications = async () => {
    try {
      await http.put('/api/Notification/notification/DeleteAll', null);

      setNotifications([]);
      setUnSeenNotificationsCount(0);
    } catch (error) {
      console.warn('error', error);
    }
  };

  const clearAll = () => {
    setClear(true);
  };

  const clearAllVerification = async (verify: boolean) => {
    if (verify === true) {
      try {
        await onClearNotifications();

        setClearAllConfirm(true);
        setClear(true);
      } catch (error) {
        console.log(error);
      }
    } else {
      setClearAllConfirm(false);
      setClear(false);
    }
  };

  useEffect(() => {
    if (notificationsVisible) {
      const clonedNotifications = _.cloneDeep(notifications);

      const unseenNotificationIds = clonedNotifications
        .filter((notification) => notification.status === 'Unseen')
        .map((notification) => notification.id);

      if (unseenNotificationIds.length > 0) {
        setUnSeenNotificationsCount(
          (count) => count - unseenNotificationIds.length
        );

        unseenNotificationIds.forEach((id) => {
          const notification = clonedNotifications.find(
            (notification) => notification.id === id
          );

          if (notification) {
            notification.status = 'Seen';
          }
        });

        http.put('/api/Notification/notification/Seen', {
          ids: unseenNotificationIds
        });

        setNotifications(() => clonedNotifications);
      }
    }
  }, [notificationsVisible, notifications, http]);

  const readAllNotificationsForDocument = async (loanApplicationId: string) => {
    try {
      const documentIds = notifications
        .filter(
          (notification) =>
            ['Unseen', 'Unread', 'Seen'].includes(notification.status) &&
            notification.payload.data.loanApplicationId === loanApplicationId
        )
        .map((notification) => notification.id);

      if (documentIds.length > 0) {
        const {data: readDocumentIds} = await http.put<
          number[],
          {ids: number[]}
        >('/api/Notification/notification/Read', {
          ids: documentIds
        });

        const clonedNotifications = _.cloneDeep(notifications);

        readDocumentIds.forEach((id) => {
          const notificationIndex = clonedNotifications.findIndex(
            (notification) => notification.id === id
          );

          if (notificationIndex !== -1) {
            clonedNotifications[notificationIndex].status = 'Read';
          }
        });

        setNotifications(clonedNotifications);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const renderNotifications = (
    tiemrs: TimersType[],
    removeNotification: (id: number) => void,
    notifications: NotificationType[],
    lastId: number,
    setTimers: React.Dispatch<React.SetStateAction<TimersType[] | undefined>>,
    readAllNotificationsForDocument: (loanApplicationId: string) => void
  ) => {
    if (notifications.length === 0) {
      return <AlertForNoData />;
    } else if (unClear === false && clearAllConfirm === false) {
      return (
        <Notifications
          timers={timers || []}
          removeNotification={removeNotification}
          receivedNewNotification={receivedNewNotification}
          notificationsVisible={notificationsVisible}
          notifications={notifications}
          getFetchNotifications={() => getFetchNotifications(lastId)}
          setTimers={setTimers}
          readAllNotificationsForDocument={readAllNotificationsForDocument}
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

  useEffect(() => {
    const signalREventRegister = async () => {
      if (SignalRHub.hubConnection.connectionState === 'Connected') {
        Promise.all([getFetchNotifications(-1), getUnseenNotificationsCount()]);
      }

      SignalRHub.hubConnection.on('SendNotification', (notification: any) => {
        const clonedNotifications = _.cloneDeep(notificationsRef.current);

        clonedNotifications.unshift(
          JSON.parse(notification) as NotificationType
        );

        setNotifications(() => clonedNotifications);
        notificationsVisibleRef.current === true &&
          setReceivedNewNotification(() => true);

        notificationsVisibleRef.current === false &&
          setUnSeenNotificationsCount((count) => count + 1);
      });

      SignalRHub.hubConnection.onclose(() => {
        const auth = LocalDB.getAuthToken();

        if (auth) {
          SignalRHub.signalRHubResume(signalREventRegister);
        }
      });
    };

    const accessToken = LocalDB.getAuthToken() || '';

    SignalRHub.configureHubConnection(
      window.envConfig.API_BASE_URL + '/serverhub',
      accessToken,
      signalREventRegister
    );
  }, [getFetchNotifications, getUnseenNotificationsCount]);

  const removeNotification = (id: number) => {
    try {
      if (!!timers && timers.length > 0) {
        if (timers.some((timer) => timer.id === id)) {
          return;
        }

        const timer = setTimeout(async () => {
          await http.put('/api/Notification/notification/Delete', {
            id
          });

          setNotifications((prev) => prev.filter((item) => item.id !== id));
        }, 5000);

        const clonedTimeers = _.cloneDeep(timers);
        clonedTimeers!.push({id, timer});
        setTimers(clonedTimeers);
      } else {
        const timer = setTimeout(async () => {
          await http.put('/api/Notification/notification/Delete', {
            id
          });

          setNotifications((prev) => prev.filter((item) => item.id !== id));
        }, 5000);

        setTimers(() => [{id, timer}]);
      }
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (notificationsRef.current.length === 6 && lastIdRef.current !== -1) {
      getFetchNotifications(lastIdRef.current);
    }
  });

  return (
    <div className={`notify`} ref={refContainerSidebar}>
      <BellIcon
        onClick={toggleNotificationSidebar}
        notificationsCounter={unSeenNotificationsCount}
      />
      {!!notificationsVisible && (
        <div className={`notify-dropdown ${notifyClass}`}>
          <Header
            clearAllDisplay={notifications.length > 0}
            handleClear={clearAll}
          />
          {renderNotifications(
            timers || [],
            removeNotification,
            notifications,
            lastId,
            setTimers,
            readAllNotificationsForDocument
          )}
        </div>
      )}
    </div>
  );
};
