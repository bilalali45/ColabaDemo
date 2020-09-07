import {Http} from 'rainsoft-js';

import {NotificationType} from '../../../lib/types';

interface UseReadAllNotificationsForDocumentProps {
  notifications: NotificationType[] | null;
  http: Http;
}

export const useReadAllNotificationsForDocument = (
  props: UseReadAllNotificationsForDocumentProps
): {
  readAllNotificationsForDocument: (loanApplicationId: string) => Promise<void>;
} => {
  const {notifications, http} = props;

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
        await http.put<number[], {ids: number[]}>(
          '/api/Notification/notification/Read',
          {
            ids: documentIds
          }
        );
      }
    } catch (error) {
      console.warn(error);
    }
  };

  return {readAllNotificationsForDocument};
};
