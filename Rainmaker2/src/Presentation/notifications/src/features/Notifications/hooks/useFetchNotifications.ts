import React, {useCallback, useState, useRef, useEffect} from 'react';
import {Http} from 'rainsoft-js';

import {NotificationType} from '../../../lib/type';

export const useFetchNotifications = (
  http: Http
): {
  getFetchNotifications: (lastId: number) => Promise<void>;
  notifications: NotificationType[] | null;
  lastId: number;
  setNotifications: React.Dispatch<
    React.SetStateAction<NotificationType[] | null>
  >;
} => {
  /**
   * This is last Id of notification inside notifications array on every API hit.
   * This needs to be send with API Call to fetch previous notifications on scroll.
   * -1 is the default value
   */
  const [lastId, setLastId] = useState(-1);
  const [notifications, setNotifications] = useState<NotificationType[] | null>(
    null
  );
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

          setNotifications((prevNotifications) => {
            /**
             * We are reseting notifications list to 10 notifications on following two conditions
             * 1. if we just logged in
             * 2. if SignalR connection Reset
             */
            return lastId === -1
              ? [...response]
              : prevNotifications!.concat(response);
          });
        } else {
          /**
           * 1. !notificationsRef.current will be true if we are fetching notifications only for the first time
           * 2. lastId=== -1 will be true if we are fetching notifications only for the first time
           */
          if (!notificationsRef.current && lastId === -1) {
            setNotifications([]);
          }
        }
      } catch (error) {
        console.warn('error', error);
      }
    },
    [http]
  );

  return {
    getFetchNotifications,
    notifications,
    lastId,
    setNotifications
  };
};
