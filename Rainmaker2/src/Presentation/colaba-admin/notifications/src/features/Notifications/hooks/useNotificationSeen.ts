import {useEffect} from 'react';
import {Http} from 'rainsoft-js';

import {NotificationType} from '../../../lib/types';

interface UseEffectNotificationSeen {
  notificationsVisible: boolean;
  notifications: NotificationType[] | null;
}

export const useNotificationSeen = (props: UseEffectNotificationSeen): void => {
  const {notifications, notificationsVisible} = props;

  useEffect(() => {
    if (!notifications) return;

    if (notificationsVisible) {
      try {
        const unseenNotificationIds = notifications
          .filter((notification) => notification.status === 'Unseen')
          .map((notification) => notification.id);

        if (unseenNotificationIds.length > 0) {
          Http.put('/api/Notification/notification/Seen', {
            ids: unseenNotificationIds
          });
        }
      } catch (error) {
        console.warn(error);
      }
    }
  }, [notificationsVisible, notifications]);
};
