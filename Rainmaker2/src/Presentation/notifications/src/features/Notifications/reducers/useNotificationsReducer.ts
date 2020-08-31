import {NotificationType, TimersType} from '../../../lib/type';
import {useReducer, Dispatch} from 'react';

export enum ACTIONS {
  UPDATE_STATE = 'UPDATE_STATE',
  ADD_NEW_NOTIFICATIONS = 'ADD_NEW_NOTIFICATIONS',
  APPEND_NOTIFICATIONS = 'APPEND_NOTIFICATIONS',
  RESET_NOTIFICATIONS = 'RESET_NOTIFICATIONS',
  INCREMEMNT_UNSEEN_COUNTER = 'INCREMEMNT_UNSEEN_COUNTER',
  DECREMEMNT_UNSEEN_COUNTER = 'DECREMEMNT_UNSEEN_COUNTER',
  ADD_DELETE_TIMER = 'ADD_DELETE_TIMER',
  RESET_DELETE_TIMERS = 'RESET_DELETE_TIMERS',
  REMOVE_NOTIFICATION = 'REMOVE_NOTIFICATION'
}

interface StateType {
  notificationsVisible: boolean;
  notifications: NotificationType[] | null;
  receivedNewNotification: boolean;
  confirmDeleteAll: boolean;
  unSeenNotificationsCount: number;
  notifyClass: string;
  timers: TimersType[] | undefined;
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
  timers: []
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
      case ACTIONS.ADD_NEW_NOTIFICATIONS:
        return {
          ...state,
          notifications: [payload.notification!, ...state.notifications]
        };
      case ACTIONS.APPEND_NOTIFICATIONS:
        return {
          ...state,
          notifications: [...state.notifications, ...payload.notifications]
        };
      case ACTIONS.RESET_NOTIFICATIONS:
        return {
          ...state,
          notifications: [...payload.notifications]
        };
      case ACTIONS.INCREMEMNT_UNSEEN_COUNTER:
        return {
          ...state,
          unSeenNotificationsCount:
            state.unSeenNotificationsCount! + payload.unSeenNotificationsCount!
        };
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
      case ACTIONS.REMOVE_NOTIFICATION: {
        const notifications = state.notifications?.filter(
          (notification) => notification.id !== payload.notificationId
        );

        return {
          ...state,
          notifications: [...notifications]
        };
      }
      default:
        return state;
    }
  };

  const [state, dispatch] = useReducer(reducer, initialState);

  return {state, dispatch};
};
