import {InitialStateType} from '../Store';
import { UserActions, userReducer } from './UserReducer'
import { TemplateActions, templateReducer } from './TemplatesReducer';
import { NotificationActions, notificationReducer } from './NotificationReducer';
import { assignedRoleReducer, AssignedRoleActions } from './AssignedRoleReducer';
import { requestEmailTemplateReducer, RequestEmailTemplateActions } from './RequestEmailTemplateReducer';
import { loanOfficerReducer, LoanOfficerActions } from './LoanOfficerReducer';

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
export type Actions = UserActions | TemplateActions | NotificationActions | AssignedRoleActions | RequestEmailTemplateActions | LoanOfficerActions


export const mainReducer = ({user, templateManager, notificationManager, assignedRolesManager, requestEmailTemplateManager,loanOfficerManager } : InitialStateType, action: Actions) => ({
    user: userReducer(user, action),
    templateManager: templateReducer(templateManager, action),
    notificationManager: notificationReducer(notificationManager, action),
    assignedRolesManager: assignedRoleReducer(assignedRolesManager, action),
    requestEmailTemplateManager: requestEmailTemplateReducer(requestEmailTemplateManager, action),
    loanOfficerManager:loanOfficerReducer(loanOfficerManager,action)
});
