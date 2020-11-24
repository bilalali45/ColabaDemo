import { AxiosResponse } from "axios";
import { Http } from "rainsoft-js";
import {RequestEmailTemplate} from "../../Entities/Models/RequestEmailTemplate";
import { Endpoints } from "../endpoints/Endpoints";

const emailMock = [
    
    {
        "id": 2,
        "templateTypeId": 1,
        "tenantId": 1,
        "templateName": "Template #2",
        "templateDescription": "Please upload Bank statement",
        "fromAddress": "###LoginUserEmail###",
        "toAddress": null,
        "ccAddress": "Ali@gmail.com,hasan@gmail.com",
        "subject": "You have new tasks to complete for your ###BusinessUnitName### loan application",
        "emailBody": "Hi ###CUSTOMERFIRSTNAME###,To continue your application, we need some more information.",
        "sortOrder": 2
    },
    {
        "id": 4,
        "templateTypeId": 1,
        "tenantId": 1,
        "templateName": "Template #4",
        "templateDescription": "Sir, Your document is rejected",
        "fromAddress": "###LoginUserEmail###",
        "toAddress": null,
        "ccAddress": "Ali@gmail.com,hasan@gmail.com",
        "subject": "You have new tasks to complete for your ###BusinessUnitName### loan application",
        "emailBody": "Hi ###CUSTOMERFIRSTNAME###,To continue your application, we need some more information.",
        "sortOrder": 4
    },
    {
        "id": 3,
        "templateTypeId": 1,
        "tenantId": 1,
        "templateName": "Template #3",
        "templateDescription": "Kindly upload rent detail statement",
        "fromAddress": "###LoginUserEmail###",
        "toAddress": null,
        "ccAddress": "Ali@gmail.com,hasan@gmail.com",
        "subject": "You have new tasks to complete for your ###BusinessUnitName### loan application",
        "emailBody": "Hi ###CUSTOMERFIRSTNAME###,To continue your application, we need some more information.",
        "sortOrder": 1
    },
    {
        "id": 1,
        "templateTypeId": 1,
        "tenantId": 1,
        "templateName": "Template #1",
        "templateDescription": "Sed ut perspiciatis unde omnis iste natus",
        "fromAddress": "###LoginUserEmail###",
        "toAddress": null,
        "ccAddress": "Ali@gmail.com,hasan@gmail.com",
        "subject": "You have new tasks to complete for your ###BusinessUnitName### loan application",
        "emailBody": "Hi ###CUSTOMERFIRSTNAME###,To continue your application, we need some more information.",
        "sortOrder": 3
    }
    
    
]

export class RequestEmailTemplateActions {

    static async fetchEmailTemplates() {
       let url = Endpoints.RequestEmailTemplateManager.GET.emailTemplates();
       try {
        let res: AxiosResponse<RequestEmailTemplate[]> = await Http.get<RequestEmailTemplate[]>(url);
        let mappedData = res.data.map((item: any) => {
            return new RequestEmailTemplate(item.id, item.templateTypeId, item.tenantId,item.templateName, item.templateDescription,item.fromAddress, item.toAddress, item.ccAddress,item.subject,item.emailBody,item.sortOrder)
        })
        return mappedData;
           
       } catch (error) {
           console.log('error',error)
       }
    }

    static async fetchDraftEmailTemplate(id?: string, loanApplicationId?: string) {
        let url = Endpoints.RequestEmailTemplateManager.GET.GetDraftEmailTemplateById(id,loanApplicationId);
        try {
         let res  = await Http.get(url)       
         return res.data;
            
        } catch (error) {
            console.log('error',error)
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
        }
    }

    static async updateEmailTemplate(emailTemplatesModel: RequestEmailTemplateActions[]){
        let url = Endpoints.RequestEmailTemplateManager.POST.updateSortOrder();
        try {
            let res = await Http.post(url, emailTemplatesModel);
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
           return res.status;
       } catch (error) {
        console.log('error',error) 
       }
    }
}