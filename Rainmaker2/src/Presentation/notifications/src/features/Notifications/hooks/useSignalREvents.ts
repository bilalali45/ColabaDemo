import {useEffect, Dispatch, useRef} from 'react';
import {SignalRHub} from 'rainsoft-js';

import {NotificationType} from '../../../lib/type';
import {LocalDB} from '../../../Utils/LocalDB';
import {Actions} from '../reducers/useNotificationsReducer';

interface UseSignalREventsProps {
  getFetchNotifications: (lastId: number) => void;
  getUnseenNotificationsCount: () => void;
  notifications: NotificationType[] | null | undefined;
  notificationsVisible: boolean | undefined;
  dispatch: Dispatch<Actions>;
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
          type: 'RECEIVED_NOTIFICATION',
          notification: JSON.parse(notification) as NotificationType
        });
      });

      SignalRHub.hubConnection.on(
        'NotificationSeen',
        (notificationIds: number[]) => {
          if (!notificationsRef.current) return;

          dispatch({
            type: 'SEEN_OR_READ_NOTIFICATIONS',
            notificationIds,
            updateType: 'Seen'
          });
        }
      );

      SignalRHub.hubConnection.on(
        'NotificationDelete',
        (deletedNotificationId: number) => {
          if (!notificationsRef.current) return;

          dispatch({
            type: 'DELETE_NOTIFICATION',
            notificationId: deletedNotificationId
          });
        }
      );

      SignalRHub.hubConnection.on('NotificationDeleteAll', () => {
        if (!notificationsRef.current) return;

        dispatch({
          type: 'RESET_NOTIFICATIONS',
          notifications: []
        });
      });

      SignalRHub.hubConnection.on(
        'NotificationRead',
        (notificationIds: number[]) => {
          if (!notificationsRef.current) return;

          dispatch({
            type: 'SEEN_OR_READ_NOTIFICATIONS',
            notificationIds,
            updateType: 'Read'
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
