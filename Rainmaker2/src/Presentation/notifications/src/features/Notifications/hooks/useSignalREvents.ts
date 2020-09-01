import {useEffect, Dispatch, useRef} from 'react';
import {SignalRHub} from 'rainsoft-js';

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

        dispatch({
          type: ACTIONS.RECEIVED_NOTIFICATION,
          payload: {notification: JSON.parse(notification) as NotificationType}
        });
      });

      SignalRHub.hubConnection.on(
        'NotificationSeen',
        (notificationIds: number[]) => {
          if (!notificationsRef.current) return;

          dispatch({
            type: ACTIONS.SEEN_NOTIFICATIONS,
            payload: {notificationIds}
          });
        }
      );

      SignalRHub.hubConnection.on(
        'NotificationDelete',
        (deletedNotificationId: number) => {
          if (!notificationsRef.current) return;

          dispatch({
            type: ACTIONS.DELETE_NOTIFICATION,
            payload: {notificationId: deletedNotificationId}
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

          dispatch({
            type: ACTIONS.READ_NOTIFICATIONS,
            payload: {notificationIds: readNotificationIds}
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
