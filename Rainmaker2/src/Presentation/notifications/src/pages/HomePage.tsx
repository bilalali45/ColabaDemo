import React, {useState, useEffect, FunctionComponent, useRef} from 'react';

import {Notifications} from '../features/Notifications';
import {Header, BellIcon} from './_HomePage';
import {apiV1} from '../lib/api';
import {NotificationType} from '../lib/type';

export const HomePage: FunctionComponent = () => {
  const [notificationsVisible, setNotificationsVisible] = useState(false);
  const [notifications, setNotifications] = useState<NotificationType[]>([]);
  const [lastId, setLastId] = useState(-1);
  const lastIdRef = useRef(lastId);

  useEffect(() => {
    lastIdRef.current = lastId;
  });

  const fetchNotifications = async (lastId: number) => {
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
  };

  useEffect(() => {
    fetchNotifications(lastIdRef.current);
  }, []);

  return (
    <div className="notify">
      <BellIcon
        onClick={() => setNotificationsVisible(!notificationsVisible)}
      />
      {!!notificationsVisible && (
        <div className="notify-dropdown animated slideInRight">
          <Header />
          <Notifications notifications={notifications} />
        </div>
      )}
    </div>
  );
};
