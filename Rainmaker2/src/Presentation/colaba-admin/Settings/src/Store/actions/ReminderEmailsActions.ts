import { Http } from "rainsoft-js";
import ReminderSettingTemplate from "../../Entities/Models/ReminderEmailListTemplate";
import { Endpoints } from "../endpoints/Endpoints";
export class ReminderEmailListActions {

    static async fetchReminderEmails() {
        let url = Endpoints.ReminderEmailListManager.GET.getReminderEmailListSettings();
        try {
                let res: any = await Http.get(url);               
                let mappedData = res.data.emailReminders.map((ReminderEmail:ReminderSettingTemplate) => {
                    return new ReminderSettingTemplate(ReminderEmail.id,ReminderEmail.noOfDays, ReminderEmail.recurringTime, ReminderEmail.isActive,ReminderEmail.email);
                });
                let finalResult = {"isActive": res.data.isActive, "emailReminders": mappedData};
              return finalResult;

        } catch (error) {
            console.log('error',error)
        }   
   
    }

    static async updateReminderEmails(ReminderEmail: ReminderSettingTemplate){
       let url = Endpoints.ReminderEmailListManager.POST.updateReminderEmailListSetting();
       try {
           let res = await Http.post(url, ReminderEmail);
           return res.status;
       } catch (error) {
        console.log('error',error)
       }
    }
    static async addReminderEmails(ReminderEmail: ReminderSettingTemplate){
        let url = Endpoints.ReminderEmailListManager.POST.addReminderEmailListSetting();
        try {
            let res = await Http.post(url, ReminderEmail);
            return res.status;
        } catch (error) {
         console.log('error',error)
        }
     }

     static async updateEnableDisableAllEmails(isActive: boolean){
        let url = Endpoints.ReminderEmailListManager.POST.updateEnableDisableAllSettings();
        try {
            let res = await Http.post(url, {isActive: isActive});
            return res;
        } catch (error) {
         console.log('error',error)
        }
    }

    static async updateEnableDisableEmails(ReminderEmail: ReminderSettingTemplate){
        let url = Endpoints.ReminderEmailListManager.POST.updateEnableDisableSettings();
        try {
            let res = await Http.post(url, ReminderEmail);
            return res;
        } catch (error) {
         console.log('error',error)
        }
    }
    static async deleteEmailReminder(ReminderEmail: ReminderSettingTemplate){
        let url = Endpoints.ReminderEmailListManager.DELETE.deleteEmailReminderSettings();
        try {
            let res = await Http.delete(url, ReminderEmail);
            return res;
        } catch (error) {
         console.log('error',error)
        }
    }

    static async fetchTokens() {
        let url = Endpoints.RequestEmailTemplateManager.GET.tokens();
        try {
            let res = await Http.get(url);
            return res.data;
        } catch (error) {
            console.log('error',error) 
        }
    }
   
}