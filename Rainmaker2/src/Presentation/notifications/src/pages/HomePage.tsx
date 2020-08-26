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
import {Header, BellIcon, ConfirmDeleteAll} from './_HomePage';
import {NotificationType, TimersType} from '../lib/type';
import {LocalDB} from '../Utils/LocalDB';

export const HomePage: FunctionComponent = () => {
  const [notificationsVisible, setNotificationsVisible] = useState(false);
  const notificationsVisibleRef = useRef(notificationsVisible);
  const [notifications, setNotifications] = useState<NotificationType[]>([]);
  const [unSeenNotificationsCount, setUnSeenNotificationsCount] = useState(0);
  const unSeenNotificationsCountRef = useRef(unSeenNotificationsCount);
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
  const refContainerSidebar = useRef<HTMLDivElement>(null);
  const [timers, setTimers] = useState<TimersType[]>();
  const [confirmDeleteAll, setConfimDeleteAll] = useState(false);

  useEffect(() => {
    lastIdRef.current = lastId;
    notificationsRef.current = notifications;
    notificationsVisibleRef.current = notificationsVisible;
    unSeenNotificationsCountRef.current = unSeenNotificationsCount;
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
        setConfimDeleteAll(() => false);
      }
    };

    const iframes = document.querySelectorAll('iframe');

    iframes.forEach((iframe: any) => {
      iframe.contentWindow.addEventListener('click', handleClickOutside, true);
    });

    document.addEventListener('click', handleClickOutside, true);

    return () => {
      iframes.forEach((iframe: any) => {
        iframe.contentWindow.removeEventListener(
          'click',
          handleClickOutside,
          true
        );
      });

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
      }, 10);
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
      console.log(error);
    }
  };

  useEffect(() => {
    if (notificationsVisible) {
      const clonedNotifications = _.cloneDeep(notifications);

      const unseenNotificationIds = clonedNotifications
        .filter((notification) => notification.status === 'Unseen')
        .map((notification) => notification.id);

      if (unseenNotificationIds.length > 0) {
        setUnSeenNotificationsCount((count) =>
          count === 0 ? 0 : count - unseenNotificationIds.length
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

        notificationsVisibleRef.current === false &&
          setUnSeenNotificationsCount((count) => count + 1);

        notificationsVisibleRef.current === true &&
          setReceivedNewNotification(() => true);

        setNotifications(() => clonedNotifications);
      });

      SignalRHub.hubConnection.onclose(() => {
        const auth = LocalDB.getAuthToken();

        if (auth) {
          //SignalRHub.signalRHubResume(signalREventRegister);
          SignalRHub.configureHubConnection(
            window.envConfig.API_BASE_URL + '/serverhub',
            LocalDB.getAuthToken() || '',
            signalREventRegister
          );
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

  let notificationsCounter = 0;
  if (receivedNewNotification === false) {
    notificationsCounter = unSeenNotificationsCount;
  } else {
    notificationsCounter = notifications.filter((n) => n.status === 'Unseen')
      .length;
  }

  return (
    <div className={`notify`} ref={refContainerSidebar}>
      <BellIcon
        onClick={toggleNotificationSidebar}
        notificationsCounter={notificationsCounter}
      />
      {!!notificationsVisible && (
        <div className={`notify-dropdown ${notifyClass}`}>
          <Header
            showClearAllButton={
              notifications.length > 0 && confirmDeleteAll === false
            }
            onDeleteAll={() => setConfimDeleteAll(true)}
          />
          {confirmDeleteAll === true && (
            <ConfirmDeleteAll
              onYes={deleteAllNotifications}
              onNo={() => setConfimDeleteAll(false)}
            />
          )}
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
        </div>
      )}
    </div>
  );
};
