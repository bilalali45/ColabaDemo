import {useEffect, Dispatch} from 'react';
import {NotificationType} from '../../../lib/type';
import {Http} from 'rainsoft-js';
import {cloneDeep} from 'lodash';
import {Params, ACTIONS} from '../reducers/useNotificationsReducer';

interface UseEffectNotificationSeen {
  notificationsVisible: boolean | undefined;
  notifications: NotificationType[] | null | undefined;
  http: Http;
  dispatch: Dispatch<Params>;
}

export const useNotificationSeen = (props: UseEffectNotificationSeen): void => {
  const {notifications, dispatch, notificationsVisible, http} = props;

  useEffect(() => {
    if (!notifications) return;

    if (notificationsVisible) {
      try {
        const clonedNotifications = cloneDeep(notifications);

        const unseenNotificationIds = clonedNotifications
          .filter((notification) => notification.status === 'Unseen')
          .map((notification) => notification.id);

        if (unseenNotificationIds.length > 0) {
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

          dispatch({
            type: ACTIONS.RESET_NOTIFICATIONS,
            payload: {notifications: clonedNotifications}
          });
        }
      } catch (error) {
        console.warn(error);
      }
    }
  }, [dispatch, notificationsVisible, notifications, http]);
};
