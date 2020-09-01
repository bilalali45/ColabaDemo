import {useReducer, Dispatch} from 'react';
import {cloneDeep} from 'lodash';

import {NotificationType, TimersType} from '../../../lib/type';

interface State {
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
  | {type: 'RESET_NOTIFICATIONS'; notifications: NotificationType[]}
  | {type: 'UPDATE_STATE'; state: Partial<State>}
  | {type: 'APPEND_NOTIFICATIONS'; notifications: NotificationType[]}
  | {type: 'ADD_DELETE_TIMER'; timer: TimersType}
  | {type: 'RESET_DELETE_TIMERS'; timerId: number}
  | {type: 'DELETE_NOTIFICATION'; notificationId: number}
  | {type: 'RECEIVED_NOTIFICATION'; notification: NotificationType}
  | {
      type: 'SEEN_OR_READ_NOTIFICATIONS';
      notificationIds: number[];
      updateType: string;
    }
  | {type: 'RESET_DELETE_TIMERS'; timerId: number};

export const useNotificationsReducer = (): {
  state: State;
  dispatch: Dispatch<Actions>;
} => {
  const reducer = (state: State = initialState, action: Actions): State => {
    switch (action.type) {
      case 'APPEND_NOTIFICATIONS':
      case 'RESET_NOTIFICATIONS': {
        if (action.notifications.length === 0) {
          return {
            ...state,
            notifications: [...action.notifications],
            confirmDeleteAll: false
          };
        }
        return {
          ...state,
          notifications: !state.notifications
            ? [...action.notifications]
            : [...state.notifications, ...action.notifications]
        };
      }
      case 'ADD_DELETE_TIMER':
        return {
          ...state,
          timers: [...state.timers, action.timer]
        };
      case 'DELETE_NOTIFICATION': {
        const notifications = state.notifications!.filter(
          (notification) => notification.id !== action.notificationId
        );

        return {
          ...state,
          notifications: [...notifications]
        };
      }
      case 'RECEIVED_NOTIFICATION': {
        if (state.notificationsVisible) {
          return {
            ...state,
            unSeenNotificationsCount: state.unSeenNotificationsCount + 1,
            receivedNewNotification: true,
            notifications: [action.notification, ...state.notifications]
          };
        }

        return {
          ...state,
          unSeenNotificationsCount: state.unSeenNotificationsCount + 1,
          notifications: [action.notification, ...state.notifications]
        };
      }
      case 'SEEN_OR_READ_NOTIFICATIONS': {
        const {notificationIds} = action;

        const clonedNotifications = cloneDeep(state.notifications);

        notificationIds.forEach((readNotificationId) => {
          const notification = clonedNotifications!.find(
            (notification) => notification.id === readNotificationId
          );

          if (notification) {
            notification.status =
              action.updateType === 'Read' ? 'Read' : 'Seen';
          }
        });

        return {
          ...state,
          notifications: [...clonedNotifications],
          unSeenNotificationsCount:
            state.unSeenNotificationsCount - notificationIds.length
        };
      }
      case 'RESET_DELETE_TIMERS': {
        const filteredTimers = state.timers.filter(
          (timer) => timer.id !== action.timerId
        );

        return {
          ...state,
          timers: [...filteredTimers]
        };
      }
      case 'UPDATE_STATE':
        return {
          ...state,
          ...action.state
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
