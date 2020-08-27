import {useEffect} from 'react';
import {NotificationType} from '../../../lib/type';
import {Http} from 'rainsoft-js';
import _ from 'lodash';

interface UseEffectNotificationSeen {
  notificationsVisible: boolean;
  notifications: NotificationType[] | null;
  http: Http;
  setUnSeenNotificationsCount: React.Dispatch<React.SetStateAction<number>>;
  setNotifications: React.Dispatch<
    React.SetStateAction<NotificationType[] | null>
  >;
}

export const useNotificationSeen = (props: UseEffectNotificationSeen): void => {
  const {notifications, setNotifications, notificationsVisible, http} = props;

  useEffect(() => {
    if (!notifications) return;

    if (notificationsVisible) {
      try {
        const clonedNotifications = _.cloneDeep(notifications);

        const unseenNotificationIds = clonedNotifications
          .filter((notification) => notification.status === 'Unseen')
          .map((notification) => notification.id);

        if (unseenNotificationIds.length > 0) {
          // setUnSeenNotificationsCount((count) =>
          //   count === 0 ? 0 : count - unseenNotificationIds.length
          // );

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
      } catch (error) {
        console.warn(error);
      }
    }
  }, [notificationsVisible, notifications, http, setNotifications]);
};
