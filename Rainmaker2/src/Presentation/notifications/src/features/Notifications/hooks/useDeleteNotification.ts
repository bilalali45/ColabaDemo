import {Dispatch} from 'react';
import {Http} from 'rainsoft-js';

import {TimersType, NotificationType} from '../../../lib/types';
import {Actions} from '../reducers/useNotificationsReducer';

interface UseDeleteNotificationProps {
  notifications: NotificationType[] | null;
  timers: TimersType[];
  http: Http;
  dispatch: Dispatch<Actions>;
}

export const useDeleteNotification = (
  props: UseDeleteNotificationProps
): {
  deleteNotification: (id: number) => void;
} => {
  const {timers, http, dispatch, notifications} = props;

  const deleteNotification = (id: number) => {
    if (!notifications) return;
    try {
      if (!!timers && timers.length > 0) {
        if (timers.some((timer) => timer.id === id)) {
          return;
        }

        const timer = setTimeout(async () => {
          await http.put('/api/Notification/notification/Delete', {
            id
          });

          dispatch({
            type: 'DELETE_NOTIFICATION',
            notificationId: id
          });
        }, 5000);

        dispatch({
          type: 'ADD_DELETE_TIMER',
          timer: {id, timer}
        });
      } else {
        const timer = setTimeout(async () => {
          await http.put('/api/Notification/notification/Delete', {
            id
          });

          dispatch({
            type: 'DELETE_NOTIFICATION',
            notificationId: id
          });
        }, 5000);

        dispatch({
          type: 'ADD_DELETE_TIMER',
          timer: {id, timer}
        });
      }
    } catch (error) {
      console.warn(error);
    }
  };

  return {deleteNotification};
};
