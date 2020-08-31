import {useEffect, Dispatch, useRef} from 'react';
import {SignalRHub} from 'rainsoft-js';
import {cloneDeep} from 'lodash';

import {NotificationType} from '../../../lib/type';
import {LocalDB} from '../../../Utils/LocalDB';
import {Params, ACTIONS} from '../reducers/useNotificationsReducer';

interface UseSignalREventsProps {
  getFetchNotifications: (lastId: number) => void;
  getUnseenNotificationsCount: () => void;
  notifications: NotificationType[] | null | undefined;
  notificationsVisible: boolean | undefined;
  dispatch: Dispatch<Params>;
}

export const useSignalREvents = ({
  getFetchNotifications,
  getUnseenNotificationsCount,
  notifications,
  notificationsVisible,
  dispatch
}: UseSignalREventsProps): void => {
  const notificationsVisibleRef = useRef(notificationsVisible);
  const notificationsRef = useRef(notifications);

  useEffect(() => {
    notificationsVisibleRef.current = notificationsVisible;
    notificationsRef.current = notifications;
  });

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

        dispatch({
          type: ACTIONS.INCREMEMNT_UNSEEN_COUNTER,
          payload: {unSeenNotificationsCount: 1}
        });

        notificationsVisibleRef.current === true &&
          dispatch({
            type: ACTIONS.UPDATE_STATE,
            payload: {receivedNewNotification: true}
          });

        dispatch({
          type: ACTIONS.RESET_NOTIFICATIONS,
          payload: {notifications: clonedNotifications}
        });
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

          dispatch({
            type: ACTIONS.RESET_NOTIFICATIONS,
            payload: {notifications: clonedNotifications}
          });
          dispatch({
            type: ACTIONS.DECREMEMNT_UNSEEN_COUNTER,
            payload: {unSeenNotificationsCount: notificationIds.length}
          });
        }
      );

      SignalRHub.hubConnection.on(
        'NotificationDelete',
        (deletedNotificationId: number) => {
          if (!notificationsRef.current) return;

          const clonedNotifications = cloneDeep(notificationsRef.current);

          const filteredNotifications = clonedNotifications.filter(
            (notification) => notification.id !== deletedNotificationId
          );

          dispatch({
            type: ACTIONS.RESET_NOTIFICATIONS,
            payload: {notifications: filteredNotifications}
          });
        }
      );

      SignalRHub.hubConnection.on('NotificationDeleteAll', () => {
        if (!notificationsRef.current) return;

        dispatch({
          type: ACTIONS.RESET_NOTIFICATIONS,
          payload: {notifications: []}
        });
      });

      SignalRHub.hubConnection.on(
        'NotificationRead',
        (readNotificationIds: number[]) => {
          if (!notificationsRef.current) return;

          const clonedNotifications = cloneDeep(notificationsRef.current);

          readNotificationIds.forEach((readNotificationId) => {
            const notification = clonedNotifications.find(
              (notification) => notification.id === readNotificationId
            );

            if (notification) {
              notification.status = 'Read';
            }
          });

          dispatch({
            type: ACTIONS.RESET_NOTIFICATIONS,
            payload: {notifications: clonedNotifications}
          });
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
  }, [dispatch, getFetchNotifications, getUnseenNotificationsCount]);
};
