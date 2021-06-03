export class ReminderEmailListEndpoints {
    
    static GET = {
        getReminderEmailListSettings: () => `/api/Documentmanagement/emailreminder/GetEmailReminders`
    }
    static DELETE = {
        deleteEmailReminderSettings: () => `/api/Documentmanagement/emailreminder/DeleteEmailReminder`
    }
    static POST = {
        addReminderEmailListSetting: () => `/api/Documentmanagement/emailreminder/AddEmailReminder`,
        updateReminderEmailListSetting: () => `/api/Documentmanagement/emailreminder/UpdateEmailReminder`,
        updateEnableDisableSettings: () => `/api/Documentmanagement/emailreminder/EnableDisableEmailReminder`,
        updateEnableDisableAllSettings: () => `/api/Documentmanagement/emailreminder/EnableDisableAllEmailReminders`
        
    }
}