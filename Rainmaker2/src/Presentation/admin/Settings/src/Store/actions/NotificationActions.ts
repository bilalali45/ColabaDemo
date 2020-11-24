import { Http } from "rainsoft-js";
import Notification from "../../Entities/Models/Notification";
import { Endpoints } from "../endpoints/Endpoints";

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
export class NotificationActions {

    static async fetchNotificationSettings() {
        let url = Endpoints.NotificationManger.GET.notificationSetting();
        try {
           let res: any = await Http.get(url);
            let mappedData = res.data.map((d:any) => {
                return new Notification(d.id, d.notificationType, d.deliveryModeId, d.notificationTypeId, d.delayedInterval);
            });
              return mappedData;

        } catch (error) {
            console.log('error',error)
        }   
   
    }

    static async updateNotificationSettings(notificationTypeId: number, deliveryModeId: number, delayedInterval: number){
        
       let url = Endpoints.NotificationManger.POST.updateSettings();
       try {
           let res = await Http.post(url, {
            notificationTypeId: notificationTypeId,
            deliveryModeId: deliveryModeId,
            delayedInterval: delayedInterval
           });
           return res;
       } catch (error) {
        console.log('error',error)
       }
    }
   
}