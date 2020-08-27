import {Dispatch, SetStateAction} from 'react';
import {Http} from 'rainsoft-js';
import {cloneDeep} from 'lodash';

import {TimersType, NotificationType} from '../../../lib/type';

interface UseRemoveNotificationProps {
  timers: TimersType[] | undefined;
  http: Http;
  setNotifications: Dispatch<SetStateAction<NotificationType[] | null>>;
  setTimers: Dispatch<SetStateAction<TimersType[] | undefined>>;
}

export const useRemoveNotification = (
  props: UseRemoveNotificationProps
): {
  removeNotification: (id: number) => void;
} => {
  const {timers, http, setNotifications, setTimers} = props;

  const removeNotification = (id: number) => {
    try {
      if (!!timers && timers.length > 0) {
        if (timers.some((timer) => timer.id === id)) {
          return;
        }

        const timer = setTimeout(async () => {
          await http.put('/api/Notification/notification/Delete', {
            id
          });

          setNotifications((prevNotifications) =>
            prevNotifications!.filter((notification) => notification.id !== id)
          );
        }, 5000);

        const clonedTimeers = cloneDeep(timers);
        clonedTimeers!.push({id, timer});
        setTimers(clonedTimeers);
      } else {
        const timer = setTimeout(async () => {
          await http.put('/api/Notification/notification/Delete', {
            id
          });

          setNotifications((prevNotifications) =>
            prevNotifications!.filter((notification) => notification.id !== id)
          );
        }, 5000);

        setTimers(() => [{id, timer}]);
      }
    } catch (error) {
      console.warn(error);
    }
  };

  return {removeNotification};
};
