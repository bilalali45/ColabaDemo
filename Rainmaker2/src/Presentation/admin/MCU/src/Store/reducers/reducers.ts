import {InitialStateType} from '../Store'
import { UserActions, userReducer } from './UserReducer'
import { TemplateActions, templateReducer } from './TemplatesReducer';

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
export type Actions = UserActions | TemplateActions

export const mainReducer = ({user, templates} : InitialStateType, action: Actions) => ({
    user: userReducer(user, action),
    templates: templateReducer(templates, action)
});
