import {NotificationType, TimersType} from '../../../lib/types';
import {State} from './useNotificationsReducer';

export interface RESET_NOTIFICATIONS {
  type: 'RESET_NOTIFICATIONS';
  notifications: NotificationType[];
}

export interface SEEN_OR_READ_NOTIFICATIONS {
  type: 'SEEN_OR_READ_NOTIFICATIONS';
  notificationIds: number[];
  updateType: string;
}

export interface UPDATE_STATE {
  type: 'UPDATE_STATE';
  state: Partial<State>;
}

export interface APPEND_NOTIFICATIONS {
  type: 'APPEND_NOTIFICATIONS';
  notifications: NotificationType[];
}

export interface ADD_DELETE_TIMER {
  type: 'ADD_DELETE_TIMER';
  timer: TimersType;
}

export interface RESET_DELETE_TIMERS {
  type: 'RESET_DELETE_TIMERS';
  timerId: number;
}

export interface DELETE_NOTIFICATION {
  type: 'DELETE_NOTIFICATION';
  notificationId: number;
}

export interface RECEIVED_NOTIFICATION {
  type: 'RECEIVED_NOTIFICATION';
  notification: NotificationType;
}

export interface DELETE_ALL_NOTIFICATIONS {
  type: 'DELETE_ALL_NOTIFICATIONS';
}
