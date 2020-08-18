import React, {
  useState,
  useEffect,
  FunctionComponent,
  useRef,
  useCallback
} from 'react';

import {Notifications} from '../features/Notifications';
import {Header, BellIcon} from './_HomePage';
import {apiV1} from '../lib/api';
import {NotificationType} from '../lib/type';

export const HomePage: FunctionComponent = () => {
  const [notificationsVisible, setNotificationsVisible] = useState(false);
  const [notifications, setNotifications] = useState<NotificationType[]>([]);
  /**
   * This is lasId of notification inside notifications array
   * This needs to be send with API Call to fetch previous notifications on scroll.
   * -1 is the default value
   */
  const [lastId, setLastId] = useState(-1);
  const lastIdRef = useRef(lastId);

  useEffect(() => {
    lastIdRef.current = lastId;
  });

  const getFetchNotifications = useCallback(
    async (lastId: number) => {
      const {data: response} = await apiV1.get<NotificationType[]>(
        '/api/Notification/notification/GetPaged',
        {
          params: {
            pageSize: 10,
            lastId,
            mediumId: 1
          }
        }
      );

      setLastId(response[response.length - 1].id);
      setNotifications(response);
    },
    [setLastId]
  );

  useEffect(() => {
    getFetchNotifications(lastIdRef.current);
  }, [getFetchNotifications]);

  return (
    <div className="notify">
      <BellIcon
        onClick={() => setNotificationsVisible(!notificationsVisible)}
      />
      {!!notificationsVisible && (
        <div className="notify-dropdown animated slideInRight">
          <Header />
          <Notifications
            notifications={notifications}
            fetchNotifications={() => getFetchNotifications(lastId)}
          />
        </div>
      )}
    </div>
  );
};
