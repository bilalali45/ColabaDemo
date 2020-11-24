import Notification from "../../../Entities/Models/Notification";

const mockOriginalData = [
    {
        "id": 1,
        "tenantId": 1,
        "userId": null,
        "deliveryModeId": 1,
        "notificationMediumId": 1,
        "notificationTypeId": 1,
        "delayedInterval": null,
        "notificationType" : "Document Submit"
    },
    {
        "id": 2,
        "tenantId": 1,
        "userId": null,
        "deliveryModeId": 3,
        "notificationMediumId": 1,
        "notificationTypeId": 2,
        "delayedInterval": null,
        "notificationType" : "Loan Application Submitted"
    },
    {
        "id": 3,
        "tenantId": 1,
        "userId": null,
        "deliveryModeId": 2,
        "notificationMediumId": 1,
        "notificationTypeId": 3,
        "delayedInterval": 20,
        "notificationType" : "Loan Funded"
    }
]

export class  NotificationActions {
    static fetchNotificationSettings() {
        let mappedData = mockOriginalData.map((d:any) => {
             return new Notification(d.id, d.notificationType, d.deliveryModeId, d.notificationTypeId, d.delayedInterval);
       });
           return Promise.resolve(mappedData);
    }

    static async updateNotificationSettings(notificationTypeId: number, deliveryModeId: number, delayedInterval: number){
        return Promise.resolve(true);
     }
}
