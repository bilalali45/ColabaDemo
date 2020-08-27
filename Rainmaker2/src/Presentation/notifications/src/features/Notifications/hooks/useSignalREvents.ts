import {useEffect, MutableRefObject, Dispatch, SetStateAction} from 'react';
import {SignalRHub} from 'rainsoft-js';
import {cloneDeep} from 'lodash';

import {NotificationType} from '../../../lib/type';
import {LocalDB} from '../../../Utils/LocalDB';

interface UseSignalREvents {
  getFetchNotifications: (lastId: number) => void;
  getUnseenNotificationsCount: () => void;
  notificationsRef: MutableRefObject<NotificationType[] | null>;
  notificationsVisibleRef: MutableRefObject<boolean>;
  setUnSeenNotificationsCount: Dispatch<SetStateAction<number>>;
  setReceivedNewNotification: Dispatch<SetStateAction<boolean>>;
  setNotifications: Dispatch<SetStateAction<NotificationType[] | null>>;
}

export const useSignalREvents = ({
  getFetchNotifications,
  getUnseenNotificationsCount,
  notificationsRef,
  notificationsVisibleRef,
  setUnSeenNotificationsCount,
  setReceivedNewNotification,
  setNotifications
}: UseSignalREvents): void => {
  useEffect(() => {
    const signalREventRegister = async () => {
      if (SignalRHub.hubConnection.connectionState === 'Connected') {
        Promise.all([getFetchNotifications(-1), getUnseenNotificationsCount()]);
      }

      SignalRHub.hubConnection.on('SendNotification', (notification: any) => {
        if (!notificationsRef.current) return;

        const clonedNotifications = cloneDeep(notificationsRef.current);

        clonedNotifications.unshift(
          JSON.parse(notification) as NotificationType
        );

        notificationsVisibleRef.current === false &&
          setUnSeenNotificationsCount((count) => count + 1);

        notificationsVisibleRef.current === true &&
          setReceivedNewNotification(() => true);

        setNotifications(() => clonedNotifications);
      });

      SignalRHub.hubConnection.on(
        'NotificationSeen',
        (notificationIds: number[]) => {
          if (!notificationsRef.current) return;

          const clonedNotifications = cloneDeep(notificationsRef.current);

          notificationIds.forEach((SeenNotificationId) => {
            if (notificationsRef.current) {
              const notification = clonedNotifications.find(
                (notification) => notification.id === SeenNotificationId
              );

              if (notification) {
                notification.status = 'Seen';
              }
            }
          });

          setUnSeenNotificationsCount(
            (count) => count - notificationIds.length
          );
          setNotifications(() => clonedNotifications);
        }
      );

      SignalRHub.hubConnection.on(
        'NotificationDelete',
        (deletedNotificationId: number) => {
          console.log('delete event fired', deletedNotificationId);

          if (!notificationsRef.current) return;

          const clonedNotifications = cloneDeep(notificationsRef.current);

          const filteredNotifications = clonedNotifications.filter(
            (notification) => notification.id !== deletedNotificationId
          );

          setNotifications(() => filteredNotifications);
        }
      );

      SignalRHub.hubConnection.onclose(() => {
        const auth = LocalDB.getAuthToken();

        if (auth) {
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
  }, [
    notificationsRef,
    notificationsVisibleRef,
    setNotifications,
    setReceivedNewNotification,
    setUnSeenNotificationsCount,
    getFetchNotifications,
    getUnseenNotificationsCount
  ]);
};
