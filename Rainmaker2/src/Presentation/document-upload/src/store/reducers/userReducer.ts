import { Actions, ActionMap } from "./reducers";

export enum UserActionTypes {
    Set = 'Set_CURRENT_USER'
}

type UserType = {} | null;

type UserPayload = {
    [UserActionTypes.Set] : {}
}

export type UserActions = ActionMap<UserPayload>[keyof ActionMap<UserPayload>];

export const userReducer = (state: UserType, {type, payload} : Actions) => {
    switch (type) {
        case UserActionTypes.Set:
            return payload;
    
        default:
            return state;
    }
}