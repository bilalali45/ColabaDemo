export class NotificationEndpoints {
    
    static GET = {
        notificationSetting: () => `/api/Setting/Notification/GetSettings`
    }

    static POST ={
        updateSettings: () => `/api/Setting/Notification/UpdateSettings`
    }
}