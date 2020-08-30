import {Dispatch, SetStateAction} from 'react';
import {Http} from 'rainsoft-js';
import {cloneDeep} from 'lodash';

import {TimersType, NotificationType} from '../../../lib/type';
import {Params} from '../reducers/useNotificationsReducer';

interface UseRemoveNotificationProps {
  notifications: NotificationType[] | null | undefined;
  timers: TimersType[] | undefined;
  http: Http;
  dispatch: Dispatch<Params>;
  setTimers: Dispatch<SetStateAction<TimersType[] | undefined>>;
}

export const useRemoveNotification = (
  props: UseRemoveNotificationProps
): {
  removeNotification: (id: number) => void;
} => {
  const {timers, http, dispatch, notifications, setTimers} = props;

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

          const filteredNotifications = notifications.filter(
            (notification) => notification.id !== id
          );

          dispatch({
            type: 'RESET_NOTIFICATIONS',
            payload: {notifications: filteredNotifications}
          });
        }, 5000);

        const clonedTimeers = cloneDeep(timers);
        clonedTimeers!.push({id, timer});
        setTimers(clonedTimeers);
      } else {
        const timer = setTimeout(async () => {
          await http.put('/api/Notification/notification/Delete', {
            id
          });

          const filteredNotifications = notifications.filter(
            (notification) => notification.id !== id
          );

          dispatch({
            type: 'RESET_NOTIFICATIONS',
            payload: {notifications: filteredNotifications}
          });
        }, 5000);

        setTimers(() => [{id, timer}]);
      }
    } catch (error) {
      console.warn(error);
    }
  };

  return {removeNotification};
};
