import Notificaiton from "../../Entities/Models/Notification";
import { ActionMap, Actions } from "./reducers"

export enum NotificationActionsType {
    SetNotificationData = "SET_NOTIFICATION_Data"
}

export type NotificationType = {
    notificationData: Notificaiton[] 
}

export type NotificationActionPayload = {
    [NotificationActionsType.SetNotificationData]: Notificaiton[] 
}

export type NotificationActions = ActionMap<NotificationActionPayload>[keyof ActionMap<NotificationActionPayload>];

export const notificationReducer = (state : NotificationType | {}, {type, payload} : Actions) => {
    switch (type) {
        case NotificationActionsType.SetNotificationData:
            return {
                ...state,
                notificationData: payload
            }

        default:
            return state
    }
}
