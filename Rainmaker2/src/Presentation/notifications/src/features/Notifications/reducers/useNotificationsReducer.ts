import {useReducer, Dispatch} from 'react';
import {cloneDeep} from 'lodash';

import {NotificationType, TimersType} from '../../../lib/types';
import {
  RESET_NOTIFICATIONS,
  UPDATE_STATE,
  APPEND_NOTIFICATIONS,
  ADD_DELETE_TIMER,
  RESET_DELETE_TIMERS,
  DELETE_NOTIFICATION,
  RECEIVED_NOTIFICATION,
  SEEN_OR_READ_NOTIFICATIONS,
  DELETE_ALL_NOTIFICATIONS,
  RESET_ON_CONNECT
} from './actionTypes';

export interface State {
  notifications: NotificationType[] | null;
  confirmDeleteAll: boolean;
  unSeenNotificationsCount: number;
  timers: TimersType[];
  notificationsVisible: boolean;
  receivedNewNotification: boolean;
  showToss: boolean;
  notifyClass: string;
}

const initialState: State = {
  notifications: null,
  confirmDeleteAll: false,
  unSeenNotificationsCount: 0,
  timers: [],
  notificationsVisible: false,
  receivedNewNotification: false,
  showToss: false,
  notifyClass: 'close'
};

export type Actions =
  | RESET_NOTIFICATIONS
  | UPDATE_STATE
  | APPEND_NOTIFICATIONS
  | ADD_DELETE_TIMER
  | RESET_DELETE_TIMERS
  | DELETE_NOTIFICATION
  | RECEIVED_NOTIFICATION
  | SEEN_OR_READ_NOTIFICATIONS
  | RESET_DELETE_TIMERS
  | DELETE_ALL_NOTIFICATIONS
  | RESET_ON_CONNECT;

const readOrSeenNotification = (
  state: State,
  action: SEEN_OR_READ_NOTIFICATIONS
): {
  notifications: NotificationType[];
  unSeenNotificationsCount: number;
} => {
  const {notificationIds} = action;

  const clonedNotifications = cloneDeep(state.notifications);

  notificationIds.forEach((readNotificationId) => {
    const notification = clonedNotifications!.find(
      (notification) => notification.id === readNotificationId
    );

    if (notification) {
      notification.status = action.updateType === 'Read' ? 'Read' : 'Seen';
    }
  });

  return {
    notifications: [...clonedNotifications],
    unSeenNotificationsCount:
      state.unSeenNotificationsCount - notificationIds.length
  };
};

const resetNotifications = (
  state: State,
  action: RESET_NOTIFICATIONS | APPEND_NOTIFICATIONS
):
  | {
      notifications: NotificationType[];
      confirmDeleteAll: boolean;
    }
  | {
      notifications: NotificationType[];
      confirmDeleteAll?: undefined;
    } => {
  if (action.notifications.length === 0) {
    return {
      notifications: [...action.notifications],
      confirmDeleteAll: false
    };
  }

  return {
    notifications: !state.notifications
      ? [...action.notifications]
      : [...state.notifications, ...action.notifications]
  };
};

const receivedNotification = (
  state: State,
  action: RECEIVED_NOTIFICATION
):
  | {
      unSeenNotificationsCount: number;
      receivedNewNotification: boolean;
      notifications: NotificationType[];
    }
  | {
      unSeenNotificationsCount: number;
      notifications: NotificationType[];
      receivedNewNotification?: undefined;
    } => {
  if (state.notificationsVisible) {
    return {
      unSeenNotificationsCount: state.unSeenNotificationsCount + 1,
      receivedNewNotification: true,
      notifications: [action.notification, ...state.notifications]
    };
  }

  return {
    unSeenNotificationsCount: state.unSeenNotificationsCount + 1,
    notifications: [action.notification, ...state.notifications]
  };
};

const resetDeleteTimers = (
  state: State,
  action: RESET_DELETE_TIMERS
): {
  timers: TimersType[];
} => {
  const filteredTimers = state.timers.filter(
    (timer) => timer.id !== action.timerId
  );

  return {
    timers: [...filteredTimers]
  };
};

const deleteNotification = (
  state: State,
  action: DELETE_NOTIFICATION
): {
  notifications: NotificationType[];
} => {
  const notifications = state.notifications!.filter(
    (notification) => notification.id !== action.notificationId
  );

  return {
    notifications: [...notifications]
  };
};

export const useNotificationsReducer = (): {
  state: State;
  dispatch: Dispatch<Actions>;
} => {
  const reducer = (state: State = initialState, action: Actions): State => {
    switch (action.type) {
      case 'APPEND_NOTIFICATIONS':
      case 'RESET_NOTIFICATIONS':
        return {
          ...state,
          ...resetNotifications(state, action)
        };
      case 'ADD_DELETE_TIMER':
        return {
          ...state,
          timers: [...state.timers, action.timer]
        };
      case 'DELETE_NOTIFICATION':
        return {
          ...state,
          ...deleteNotification(state, action)
        };
      case 'RECEIVED_NOTIFICATION':
        return {
          ...state,
          ...receivedNotification(state, action)
        };
      case 'SEEN_OR_READ_NOTIFICATIONS':
        return {
          ...state,
          ...readOrSeenNotification(state, action)
        };
      case 'RESET_DELETE_TIMERS':
        return {
          ...state,
          ...resetDeleteTimers(state, action)
        };
      case 'DELETE_ALL_NOTIFICATIONS':
        return {
          ...state,
          notifications: [],
          confirmDeleteAll: false
        };
      case 'UPDATE_STATE':
        return {
          ...state,
          ...action.state
        };
      case 'RESET_ON_CONNECT':
        return {
          ...state,
          notifications: action.notifications
        };
      default:
        return state;
    }
  };

  const [state, dispatch] = useReducer<React.Reducer<State, Actions>>(
    reducer,
    initialState
  );

  return {state, dispatch};
};
