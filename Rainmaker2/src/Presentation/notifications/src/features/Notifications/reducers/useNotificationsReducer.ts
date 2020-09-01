import {useReducer, Dispatch} from 'react';

import {NotificationType, TimersType} from '../../../lib/type';
import {cloneDeep} from 'lodash';

export enum ACTIONS {
  UPDATE_STATE = 'UPDATE_STATE',
  APPEND_NOTIFICATIONS = 'APPEND_NOTIFICATIONS',
  RESET_NOTIFICATIONS = 'RESET_NOTIFICATIONS',
  DECREMEMNT_UNSEEN_COUNTER = 'DECREMEMNT_UNSEEN_COUNTER',
  ADD_DELETE_TIMER = 'ADD_DELETE_TIMER',
  RESET_DELETE_TIMERS = 'RESET_DELETE_TIMERS',
  DELETE_NOTIFICATION = 'DELETE_NOTIFICATION',
  RECEIVED_NOTIFICATION = 'RECEIVED_NOTIFICATION',
  SEEN_NOTIFICATIONS = 'SEEN_NOTIFICATIONS',
  READ_NOTIFICATIONS = 'READ_NOTIFICATIONS'
}

interface StateType {
  notificationsVisible: boolean;
  notifications: NotificationType[] | null;
  receivedNewNotification: boolean;
  confirmDeleteAll: boolean;
  unSeenNotificationsCount: number;
  notifyClass: string;
  timers: TimersType[];
  showToss: boolean;
  notificationIds: number[];
}

interface UpdateParams {
  notificationsVisible: boolean;
  notifications: NotificationType[];
  notification: NotificationType;
  receivedNewNotification: boolean;
  confirmDeleteAll: boolean;
  unSeenNotificationsCount: number;
  notifyClass: string;
  timers: TimersType[];
  timer: TimersType;
  timerId: number;
  notificationId: number;
  showToss: boolean;
  notificationIds: number[];
}

export interface Params {
  type: string;
  payload: Partial<UpdateParams>;
}

export const initialState: StateType = {
  notificationsVisible: false,
  notifications: null,
  receivedNewNotification: false,
  confirmDeleteAll: false,
  unSeenNotificationsCount: 0,
  notifyClass: 'close',
  timers: [],
  showToss: false,
  notificationIds: []
};

export const useNotificationsReducer = (): {
  state: Partial<StateType>;
  dispatch: Dispatch<Params>;
} => {
  const reducer = (
    state: Partial<StateType> = initialState,
    action: Params
  ): Partial<StateType> => {
    const {type, payload} = action;

    switch (type) {
      case ACTIONS.UPDATE_STATE:
        return {
          ...state,
          ...payload
        };
      case ACTIONS.APPEND_NOTIFICATIONS:
        return {
          ...state,
          notifications: [...state.notifications, ...payload.notifications]
        };
      case ACTIONS.RESET_NOTIFICATIONS: {
        if (payload.notifications?.length === 0) {
          return {
            ...state,
            notifications: [...payload.notifications],
            confirmDeleteAll: false
          };
        }

        return {
          ...state,
          notifications: [...payload.notifications]
        };
      }
      case ACTIONS.DECREMEMNT_UNSEEN_COUNTER:
        return {
          ...state,
          unSeenNotificationsCount:
            state.unSeenNotificationsCount! - payload.unSeenNotificationsCount!
        };
      case ACTIONS.ADD_DELETE_TIMER:
        return {
          ...state,
          timers: [...state.timers, payload.timer!]
        };
      case ACTIONS.RESET_DELETE_TIMERS: {
        const timers = state.timers?.filter(
          (timer) => timer.id !== payload.timerId
        );

        return {
          ...state,
          timers: [...timers]
        };
      }
      case ACTIONS.DELETE_NOTIFICATION: {
        const notifications = state.notifications?.filter(
          (notification) => notification.id !== payload.notificationId
        );

        return {
          ...state,
          notifications: [...notifications]
        };
      }
      case ACTIONS.RECEIVED_NOTIFICATION: {
        const notificationsVisible = state.notificationsVisible;

        if (notificationsVisible) {
          return {
            ...state,
            unSeenNotificationsCount: state.unSeenNotificationsCount! + 1,
            receivedNewNotification: true,
            notifications: [payload.notification!, ...state.notifications]
          };
        }

        return {
          ...state,
          unSeenNotificationsCount: state.unSeenNotificationsCount! + 1,
          notifications: [payload.notification!, ...state.notifications]
        };
      }
      case ACTIONS.SEEN_NOTIFICATIONS: {
        const {notificationIds} = payload;

        const clonedNotifications = cloneDeep(state.notifications);

        notificationIds!.forEach((seenNotificationId) => {
          const notification = clonedNotifications!.find(
            (notification) => notification.id === seenNotificationId
          );

          if (notification) {
            notification.status = 'Seen';
          }
        });

        return {
          ...state,
          notifications: [...clonedNotifications],
          unSeenNotificationsCount:
            state.unSeenNotificationsCount! - notificationIds!.length
        };
      }
      case ACTIONS.READ_NOTIFICATIONS: {
        const {notificationIds} = payload;

        const clonedNotifications = cloneDeep(state.notifications);

        notificationIds!.forEach((readNotificationId) => {
          const notification = clonedNotifications!.find(
            (notification) => notification.id === readNotificationId
          );

          if (notification) {
            notification.status = 'Read';
          }
        });

        return {
          ...state,
          notifications: [...clonedNotifications]
        };
      }

      default:
        return state;
    }
  };

  const [state, dispatch] = useReducer(reducer, initialState);

  return {state, dispatch};
};
