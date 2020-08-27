import {Dispatch, SetStateAction} from 'react';
import {Http} from 'rainsoft-js';
import {cloneDeep} from 'lodash';

import {NotificationType} from '../../../lib/type';

interface UseReadAllNotificationsForDocumentProps {
  notifications: NotificationType[] | null;
  http: Http;
  setNotifications: Dispatch<SetStateAction<NotificationType[] | null>>;
}

export const useReadAllNotificationsForDocument = (
  props: UseReadAllNotificationsForDocumentProps
): {
  readAllNotificationsForDocument: (loanApplicationId: string) => Promise<void>;
} => {
  const {notifications, http, setNotifications} = props;

  const readAllNotificationsForDocument = async (loanApplicationId: string) => {
    try {
      if (!notifications) return;

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

        const clonedNotifications = cloneDeep(notifications);

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
      console.warn(error);
    }
  };

  return {readAllNotificationsForDocument};
};
