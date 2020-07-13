import { ActionMap, Actions } from "./reducers"

export enum UserActionsType {
    SetUserInfo = "SET_USER_INFO"
}

export type Usertype = {
    userInfo: {} 
}

export type UserActionPayload = {
    [UserActionsType.SetUserInfo]: {}
}

export type UserActions = ActionMap<UserActionPayload>[keyof ActionMap<UserActionPayload>];

export const userReducer = (state : Usertype, {type, payload} : Actions) => {
    switch (type) {
        case UserActionsType.SetUserInfo:
            return {
                ...state,
                userInfo: payload
            }

        default:
            return state
    }
}
