import {Dispatch} from 'react';
import {Http} from 'rainsoft-js';

import {TimersType, NotificationType} from '../../../lib/type';
import {Params, ACTIONS} from '../reducers/useNotificationsReducer';

interface UseRemoveNotificationProps {
  notifications: NotificationType[] | null | undefined;
  timers: TimersType[] | undefined;
  http: Http;
  dispatch: Dispatch<Params>;
}

export const useRemoveNotification = (
  props: UseRemoveNotificationProps
): {
  removeNotification: (id: number) => void;
} => {
  const {timers, http, dispatch, notifications} = props;

  const removeNotification = (id: number) => {
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
            type: ACTIONS.DELETE_NOTIFICATION,
            payload: {notificationId: id}
          });
        }, 5000);

        dispatch({
          type: ACTIONS.ADD_DELETE_TIMER,
          payload: {timer: {id, timer}}
        });
      } else {
        const timer = setTimeout(async () => {
          await http.put('/api/Notification/notification/Delete', {
            id
          });

          dispatch({
            type: ACTIONS.DELETE_NOTIFICATION,
            payload: {notificationId: id}
          });
        }, 5000);

        dispatch({
          type: ACTIONS.ADD_DELETE_TIMER,
          payload: {timer: {id, timer}}
        });
      }
    } catch (error) {
      console.warn(error);
    }
  };

  return {removeNotification};
};
