import {InitialStateType} from '../Store';
import { DashboardActions, dashboardReducer } from './DashboardReducer';
import { ErrorActions, errorReducer } from './ErrorReducer';
import {UserActions, userReducer} from  './UserReducer';

export type ActionMap<M extends { [index: string]: any }> = {
    [Key in keyof M] : M[Key] extends undefined
    ? {
        type: Key;
    }
    : {
        type: Key;
        payload: M[Key];
    }
}

export type Actions = UserActions | ErrorActions | DashboardActions;

export const mainReducer = ({user, error, dashboard} : InitialStateType, action: Actions) => ({
user: userReducer(user, action),
error: errorReducer(error, action), 
dashboard: dashboardReducer(dashboard,action)
});