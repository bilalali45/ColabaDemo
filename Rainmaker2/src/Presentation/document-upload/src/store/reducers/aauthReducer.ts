import { Actions, ActionMap } from "./reducers";

export enum AuthTypes {
    Save = 'SAVE_AUTH',
    Remove = 'REMOVE_AUTH'
}

type AuthType = {} | null;

type AuthPayload = {
    [AuthTypes.Save] : {};
    [AuthTypes.Remove] : {}
}

export type AuthActions = ActionMap<AuthPayload>[keyof ActionMap<AuthPayload>];


export const authReducer = (state: AuthType, {type, payload}: Actions) => {
    switch (type) {
        case AuthTypes.Save:
            return {
                token: payload
            };

        case AuthTypes.Remove: 
            return null;
    
        default:
            return state;
    }
}