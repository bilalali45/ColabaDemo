import {ActionMap, Actions} from "./reducers";

export enum UserActionsType {
    SetUserInfo = "SET_USER_INFO",
    SetTenantInfo = "SET_TENANT_INFO"
}

export type UserType ={
    userInfo: {},
    tenantInfo:{logo:string,cookiePath:string}
}

export type UserActionPayload = {
    [UserActionsType.SetUserInfo]: {},
    [UserActionsType.SetTenantInfo]:{}
}

export type UserActions = ActionMap<UserActionPayload>[keyof ActionMap<UserActionPayload>];

export const userReducer = (state:UserType | any, {type, payload} : Actions) => {
    switch (type) {
        case UserActionsType.SetUserInfo:
            return {
                ...state,
                userInfo: payload
            }
        case UserActionsType.SetTenantInfo:{
            return {
                ...state,
                tenantInfo: payload
            }
        }
            
            

        default:
            return state
    }
}