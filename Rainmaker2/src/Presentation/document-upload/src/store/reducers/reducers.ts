import { AuthActions, authReducer } from "./aauthReducer";
import { UserActions, userReducer } from "./userReducer";
import { InitialStateType } from "../store";

export type ActionMap<M extends { [index: string]: any }> = {
    [Key in keyof M]: M[Key] extends undefined
    ? {
        type: Key;
    }
    : {
        type: Key;
        payload: M[Key];
    }
};


export const mainReducer = ({ auth, user } : InitialStateType  , action: Actions) => ({
    auth: authReducer(auth, action),
    user: userReducer(user, action)
});

export type Actions = AuthActions | UserActions;
