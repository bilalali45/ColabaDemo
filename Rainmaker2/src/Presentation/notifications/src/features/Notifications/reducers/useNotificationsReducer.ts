import {NotificationType} from '../../../lib/type';
import {useReducer, Dispatch} from 'react';

const UPDATE_STATE = 'UPDATE_STATE';
const ADD_NEW_NOTIFICATIONS = 'ADD_NEW_NOTIFICATIONS';
const APPEND_NOTIFICATIONS = 'APPEND_NOTIFICATIONS';
const RESET_NOTIFICATIONS = 'RESET_NOTIFICATIONS';
const INCREMEMNT_UNSEEN_COUNTER = 'INCREMEMNT_UNSEEN_COUNTER';
const DECREMEMNT_UNSEEN_COUNTER = 'DECREMEMNT_UNSEEN_COUNTER';

interface StateType {
  notificationsVisible: boolean;
  notifications: NotificationType[] | null;
  receivedNewNotification: boolean;
  confirmDeleteAll: boolean;
  unSeenNotificationsCount: number;
  notifyClass: string;
}

interface UpdateParams {
  notificationsVisible: boolean;
  notifications: NotificationType[];
  notification: NotificationType;
  receivedNewNotification: boolean;
  confirmDeleteAll: boolean;
  unSeenNotificationsCount: number;
  notifyClass: string;
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
  notifyClass: 'close'
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
      case UPDATE_STATE:
        return {
          ...state,
          ...payload
        };
      case ADD_NEW_NOTIFICATIONS:
        return {
          ...state,
          notifications: [payload.notification!, ...state.notifications]
        };
      case APPEND_NOTIFICATIONS:
        return {
          ...state,
          notifications: [...state.notifications, ...payload.notifications]
        };
      case RESET_NOTIFICATIONS:
        return {
          ...state,
          notifications: [...payload.notifications]
        };
      case INCREMEMNT_UNSEEN_COUNTER:
        return {
          ...state,
          unSeenNotificationsCount:
            state.unSeenNotificationsCount! + payload.unSeenNotificationsCount!
        };
      case DECREMEMNT_UNSEEN_COUNTER:
        return {
          ...state,
          unSeenNotificationsCount:
            state.unSeenNotificationsCount! - payload.unSeenNotificationsCount!
        };
      default:
        return state;
    }
  };

  const [state, dispatch] = useReducer(reducer, initialState);

  return {state, dispatch};
};
