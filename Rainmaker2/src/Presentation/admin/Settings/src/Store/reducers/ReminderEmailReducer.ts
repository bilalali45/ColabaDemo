import { ActionMap, Actions } from "./reducers";
import ReminderSettingTemplate from "../../Entities/Models/ReminderEmailListTemplate";

export enum ReminderEmailActionsType {
    SetReminderEmailData = "SET_REMINDER_EMAIL_DATA",
    SetSelectedReminderEmail ="SET_SELECTED_REMINDER_EMAIL",
    SetAllReminderEmailEnable = "SET_ALL_REMINDER_EMAIL_ENABLE"
}

export type ReminderEmailType ={
    reminderEmailData: ReminderSettingTemplate[] ,
    selectedReminderEmail: ReminderSettingTemplate,
    allReminderEnable: boolean
}
export type ReminderEmailActionPayload = {
    [ReminderEmailActionsType.SetReminderEmailData]: ReminderSettingTemplate[] ,
    [ReminderEmailActionsType.SetSelectedReminderEmail]: ReminderSettingTemplate,
    [ReminderEmailActionsType.SetAllReminderEmailEnable]: boolean
}

export type ReminderEmailActions = ActionMap<ReminderEmailActionPayload>[keyof ActionMap<ReminderEmailActionPayload>];

export const emailReminderReducer = (state : ReminderEmailType | {}, {type, payload} : Actions) => {
    switch (type) {
        case ReminderEmailActionsType.SetReminderEmailData:
            return {
                ...state,
                reminderEmailData: payload
            }

            case ReminderEmailActionsType.SetSelectedReminderEmail:
                return{
                    ...state,
                    selectedReminderEmail: payload
                }
           case ReminderEmailActionsType.SetAllReminderEmailEnable:
               return {
                   ...state,
                   allReminderEnable: payload
               }
        default:
            return state
    }
}