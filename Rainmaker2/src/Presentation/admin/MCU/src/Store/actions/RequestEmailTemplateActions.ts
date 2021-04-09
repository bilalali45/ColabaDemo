import { AxiosResponse } from "axios";
import { Http } from "rainsoft-js";
import {RequestEmailTemplate} from "../../Entities/Models/RequestEmailTemplate";
import { Endpoints } from "../endpoints/Endpoints";


export class RequestEmailTemplateActions {

    static async fetchEmailTemplates() {
       let url = Endpoints.RequestEmailTemplateManager.GET.emailTemplates();
       try {
        let res: AxiosResponse<RequestEmailTemplate[]> = await Http.get<RequestEmailTemplate[]>(url);
        let mappedData = res.data.map((item: any) => {
            return new RequestEmailTemplate(item.id, item.templateTypeId, item.tenantId,item.templateName, item.templateDescription,item.fromAddress, item.toAddress, item.ccAddress,item.subject,item.emailBody,item.sortOrder)
        })
        return {status:res.status, data:mappedData};
           
       } catch (error) {
           console.log('error',error)
           return error
       }
    }

    static async fetchDraftEmailTemplate(id?: string, loanApplicationId?: string) {
        if(id && loanApplicationId){
            let url = Endpoints.RequestEmailTemplateManager.GET.GetDraftEmailTemplateById(id,loanApplicationId);
            try {
             let res  = await Http.get(url)       
             return res;
                
            } catch (error) {
                console.log('error',error)
                return error;
            }
        }       
     }

    static async deleteEmailTemplate(id?: number){
        let url = Endpoints.RequestEmailTemplateManager.DELETE.deleteEmailTemplate();
        try {
             let res = await Http.delete(url, {
                id: id
             });
            return res;
        } catch (error) {
            console.log('error',error) 
            return error;
        }
    }

    static async updateEmailTemplate(emailTemplatesModel: RequestEmailTemplateActions[]){
        let url = Endpoints.RequestEmailTemplateManager.POST.updateSortOrder();
        try {
            let res = await Http.post(url, emailTemplatesModel);
            return res;
        } catch (error) {
            console.log('error',error) 
            return error
        }
    }

    static async fetchTokens() {
        let url = Endpoints.RequestEmailTemplateManager.GET.tokens();
        try {
            let res = await Http.get(url);
            return res;
        } catch (error) {
            console.log('error',error) 
            return error;
        }
    }
    
    static async insertEmailTemplate(templateName: string, templateDescription: string, fromAddress: string, CCAddress: string, subject: string, emailBody: string){
     
        let url = Endpoints.RequestEmailTemplateManager.POST.insertEmailTemplate();
       try {
           let res = await Http.post(url, {
            templateName: templateName,
            templateDescription: templateDescription,
            fromAddress: fromAddress,
            CCAddress: CCAddress,
            subject: subject,
            emailBody: emailBody,
            templateTypeId : 1
           });
           return res;
       } catch (error) {
        console.log('error',error) 
        return error
       }
    }
}