import { Actions, ActionMap } from "./reducers";
import { LoanNagivator } from "../../Utilities/Navigation/LoanNavigator";

export enum LeftMenuActionType {
    SetNavigation = 'SET_NAVIGATION',
    SetLeftMenuItems = 'SET_LEFT_MENU_ITEMS',
    SetNotAllowedItems = 'SET_NOT_ALLOWED_ITEMS',
}

export type LeftMenuType = {
    leftMenuItems: [],
    notAllowedItems: [],
    navigation: null,
};

type LeftMenuPayload = {
    [LeftMenuActionType.SetLeftMenuItems] : [],
    [LeftMenuActionType.SetNotAllowedItems] : [],
    [LeftMenuActionType.SetNavigation] : LoanNagivator,
}

export type LeftMenuActions = ActionMap<LeftMenuPayload>[keyof ActionMap<LeftMenuPayload>];

export const leftMenuReducer = (state: LeftMenuType | any, {type, payload} : Actions) => {
    switch (type) {
        case LeftMenuActionType.SetLeftMenuItems :
            return {
                ...state,
                leftMenuItems: payload
            };
        case LeftMenuActionType.SetNotAllowedItems :
            return {
                ...state,
                notAllowedItems: payload
            };
        case LeftMenuActionType.SetNavigation :
            return {
                ...state,
                navigation: payload
            };
        default:
            return state;
    }
}