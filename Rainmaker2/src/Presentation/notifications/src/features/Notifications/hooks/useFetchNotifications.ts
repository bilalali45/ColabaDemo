import {useCallback, useState, useRef, useEffect, Dispatch} from 'react';
import {Http} from 'rainsoft-js';

import {NotificationType} from '../../../lib/types';
import {Actions} from '../reducers/useNotificationsReducer';

interface UseFetchNotificationsProps {
  dispatch: Dispatch<Actions>;
  notifications: NotificationType[] | null;
}

export const useFetchNotifications = (
  props: UseFetchNotificationsProps
): {
  getFetchNotifications: (lastId: number) => Promise<void>;
  lastId: number;
} => {
  const {dispatch, notifications} = props;

  /**
   * This is last Id of notification inside notifications array on every API hit.
   * This needs to be send with API Call to fetch previous notifications on scroll.
   * -1 is the default value
   */
  const [lastId, setLastId] = useState(-1);

  const notificationsRef = useRef(notifications);
  const lastIdRef = useRef(lastId);

  useEffect(() => {
    notificationsRef.current = notifications;
    lastIdRef.current = lastId;
  });

  const getFetchNotifications = useCallback(
    async (lastIdParam: number) => {
      try {
        const {data: response} = await Http.get<NotificationType[]>(
          `/api/Notification/notification/GetPaged?pageSize=10&lastId=${
            lastIdParam || lastIdRef.current
          }&mediumId=1`
        );

        if (response.length > 0) {
          setLastId(response[response.length - 1].id);
          /**
           * We are reseting notifications list to 10 notifications on following two conditions
           * 1. if we just logged in
           * 2. if SignalR connection Reset
           */

          lastIdParam === -1
            ? dispatch({
                type: 'RESET_ON_CONNECT',
                notifications: [...response]
              })
            : dispatch({
                type: 'APPEND_NOTIFICATIONS',
                notifications: response
              });
        } else {
          /**
           * 1. !notificationsRef.current will be true if we are fetching notifications only for the first time
           * 2. lastId=== -1 will be true if we are fetching notifications only for the first time
           */
          if (!notificationsRef.current && lastIdRef.current === -1) {
            dispatch({
              type: 'RESET_NOTIFICATIONS',
              notifications: []
            });
          }
        }
      } catch (error) {
        console.warn('error', error);
      }
    },
    [dispatch]
  );

  return {
    getFetchNotifications,
    lastId
  };
};
