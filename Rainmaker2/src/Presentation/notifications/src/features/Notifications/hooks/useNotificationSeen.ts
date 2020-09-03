import {useEffect} from 'react';
import {Http} from 'rainsoft-js';

import {NotificationType} from '../../../lib/types';

interface UseEffectNotificationSeen {
  notificationsVisible: boolean;
  notifications: NotificationType[] | null;
  http: Http;
}

export const useNotificationSeen = (props: UseEffectNotificationSeen): void => {
  const {notifications, notificationsVisible, http} = props;

  useEffect(() => {
    if (!notifications) return;

    if (notificationsVisible) {
      try {
        const unseenNotificationIds = notifications
          .filter((notification) => notification.status === 'Unseen')
          .map((notification) => notification.id);

        if (unseenNotificationIds.length > 0) {
          http.put('/api/Notification/notification/Seen', {
            ids: unseenNotificationIds
          });
        }
      } catch (error) {
        console.warn(error);
      }
    }
  }, [notificationsVisible, notifications, http]);
};
