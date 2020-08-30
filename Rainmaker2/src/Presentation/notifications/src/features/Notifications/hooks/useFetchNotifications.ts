import {useCallback, useState, useRef, useEffect, Dispatch} from 'react';
import {Http} from 'rainsoft-js';

import {NotificationType} from '../../../lib/type';
import {Params} from '../reducers/useNotificationsReducer';

export const useFetchNotifications = (
  http: Http,
  dispatch: Dispatch<Params>,
  notifications: NotificationType[] | null | undefined
): {
  getFetchNotifications: (lastId: number) => Promise<void>;
  lastId: number;
} => {
  /**
   * This is last Id of notification inside notifications array on every API hit.
   * This needs to be send with API Call to fetch previous notifications on scroll.
   * -1 is the default value
   */
  const [lastId, setLastId] = useState(-1);

  const notificationsRef = useRef(notifications);

  useEffect(() => {
    notificationsRef.current = notifications;
  });

  const getFetchNotifications = useCallback(
    async (lastId: number) => {
      try {
        const {data: response} = await http.get<NotificationType[]>(
          `/api/Notification/notification/GetPaged?pageSize=10&lastId=${lastId}&mediumId=1`
        );

        if (response.length > 0) {
          setLastId(response[response.length - 1].id);
          /**
           * We are reseting notifications list to 10 notifications on following two conditions
           * 1. if we just logged in
           * 2. if SignalR connection Reset
           */
          lastId === -1
            ? dispatch({
                type: 'RESET_NOTIFICATIONS',
                payload: {notifications: [...response]}
              })
            : dispatch({
                type: 'APPEND_NOTIFICATIONS',
                payload: {notifications: response}
              });
        } else {
          /**
           * 1. !notificationsRef.current will be true if we are fetching notifications only for the first time
           * 2. lastId=== -1 will be true if we are fetching notifications only for the first time
           */
          if (!notificationsRef.current && lastId === -1) {
            dispatch({
              type: 'RESET_NOTIFICATIONS',
              payload: {notifications: []}
            });
          }
        }
      } catch (error) {
        console.warn('error', error);
      }
    },
    [dispatch, http]
  );

  return {
    getFetchNotifications,
    lastId
  };
};
