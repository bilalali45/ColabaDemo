import {InitialStateType} from '../Store';
import { UserActions, userReducer } from './UserReducer'
import { TemplateActions, templateReducer } from './TemplatesReducer';
import { NotificationActions, notificationReducer } from './NotificationReducer';
import { assignedRoleReducer, AssignedRoleActions } from './AssignedRoleReducer';
import { requestEmailTemplateReducer, RequestEmailTemplateActions } from './RequestEmailTemplateReducer';
import { loanOfficerReducer, LoanOfficerActions } from './LoanOfficerReducer';
import { OrganizationActions, organizationReducer } from './OrganizationReducer';
import { ReminderEmailActions, emailReminderReducer } from './ReminderEmailReducer';
import { LoanStatusActions, loanStatusUpdateReducer } from './LoanStatusUpdateReducer';

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
export type Actions = UserActions | TemplateActions | NotificationActions | AssignedRoleActions | RequestEmailTemplateActions | LoanOfficerActions | OrganizationActions | ReminderEmailActions | LoanStatusActions


export const mainReducer = ({user, templateManager, notificationManager, assignedRolesManager, requestEmailTemplateManager,loanOfficerManager,organizationManager,emailReminderManager, loanStatusManager } : InitialStateType, action: Actions) => ({
    user: userReducer(user, action),
    templateManager: templateReducer(templateManager, action),
    notificationManager: notificationReducer(notificationManager, action),
    assignedRolesManager: assignedRoleReducer(assignedRolesManager, action),
    requestEmailTemplateManager: requestEmailTemplateReducer(requestEmailTemplateManager, action),
    loanOfficerManager:loanOfficerReducer(loanOfficerManager,action),
    organizationManager:organizationReducer(organizationManager,action),
    emailReminderManager:emailReminderReducer(emailReminderManager,action),
    loanStatusManager: loanStatusUpdateReducer(loanStatusManager, action)
});
