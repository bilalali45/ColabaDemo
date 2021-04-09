import { RequestEmailTemplate } from "../../../Entities/Models/RequestEmailTemplate";


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

const tokensMock = [
    {
        "id": "5fa02360c873e00f73864f36",
        "name": "Login User Email",
        "symbol": "###LoginUserEmail###",
        "description": "Key for enabling user email address",
        "key": "LoginUserEmail"
    },
    {
        "id": "5fa0236fc873e00f73864f6e",
        "name": "Customer First Name",
        "symbol": "###CustomerFirstname###",
        "description": "Key for enabling customer first name",
        "key": "CustomerFirstName"
    },
    {
        "id": "5fa02379c873e00f73864f83",
        "name": "Request Document List",
        "symbol": "###DoucmentList###",
        "description": "Key for enabling list of all requested document",
        "key": "RequestDocumentList"
    },
    {
        "id": "5fa02381c873e00f73864f94",
        "name": "Business Unit Name",
        "symbol": "###BusinessUnitName###",
        "description": "Business Unit Name",
        "key": "BusinessUnitName"
    }
]

export class RequestEmailTemplateActions {

    static async fetchEmailTemplates() {
        let mappedData = emailMock.map((data: any) => {
            return new RequestEmailTemplate(data.id, data.templateTypeId,data.tenantId, data.templateName, data.templateDescription, data.fromAddress, data.toAddress, data.ccAddress, data.subject, data.emailBody, data.sortOrder);
        });
       return Promise.resolve(mappedData);
    }

    static async deleteEmailTemplate(id?: number){
        emailMock.splice(0,1);
        return Promise.resolve(true);
    }

    static async updateEmailTemplateSort(emailTemplatesModel: RequestEmailTemplateActions[]){
        return Promise.resolve(true);
    }

    static async fetchTokens() {
        return Promise.resolve(tokensMock);
    }

    static async insertEmailTemplate(templateName: string, templateDescription: string, fromAddress: string, CCAddress: string, subject: string, emailBody: string){
        let item = {
         id : 5,
         templateTypeId: 1,
         tenantId: 1,
         templateName: templateName,
         templateDescription: templateDescription,
         fromAddress: fromAddress,
         toAddress: null,
         ccAddress: CCAddress,
         subject: subject,
         emailBody: emailBody,
         sortOrder: 5
        }
        emailMock.push(item);
        return Promise.resolve(200);
    }

    static async updateEmailTemplate(id: number, templateName: string, templateDescription: string, fromAddress: string, CCAddress: string, subject: string, emailBody: string){
        let item: any  = emailMock.find(i  =>  i.id === id);
        item.templateName = templateName;
        emailMock.shift();
        emailMock.unshift(item);       
        return Promise.resolve(200);
    }
}