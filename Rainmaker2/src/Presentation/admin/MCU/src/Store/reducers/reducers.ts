import {InitialStateType} from '../Store'
import { UserActions, userReducer } from './UserReducer'
import { TemplateActions, templateReducer } from './TemplatesReducer';
import { needListReducer, NeedListActions } from './NeedListReducer';
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
export type Actions = UserActions | TemplateActions | NeedListActions


export const mainReducer = ({user, templateManager, needListManager} : InitialStateType, action: Actions) => ({
    user: userReducer(user, action),
    templateManager: templateReducer(templateManager, action),
    needListManager: needListReducer(needListManager, action)
});
