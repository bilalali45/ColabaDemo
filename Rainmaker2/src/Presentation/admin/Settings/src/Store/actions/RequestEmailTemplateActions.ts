import { AxiosResponse } from "axios";
import { Http } from "rainsoft-js";
import RequestEmailTemplates from "../../Components/RequestEmailTemplates/RequestEmailTemplates";
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

const tokensMock = [
    {
        "id": "5fc798c72048c60af5bfa6de",
        "name": "Date",
        "symbol": "###Date###",
        "description": "Current Date",
        "key": "Date",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6df",
        "name": "Primary Borrower Email Address",
        "symbol": "###PrimaryBorrowerEmailAddress###",
        "description": "Primary Borrower Email Address",
        "key": "PrimaryBorrowerEmailAddress",
        "fromAddess": false,
        "ccAddess": true,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6e0",
        "name": "Primary Borrower First Name",
        "symbol": "###PrimaryBorrowerFirstName###",
        "description": "Primary Borrower First Name",
        "key": "PrimaryBorrowerFirstName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6e1",
        "name": "Primary Borrower Last Name",
        "symbol": "###PrimaryBorrowerLastName###",
        "description": "Primary Borrower Last Name",
        "key": "PrimaryBorrowerLastName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6e2",
        "name": "Co-Borrower First Name",
        "symbol": "###Co-BorrowerFirstName###",
        "description": "Co-Borrower First Name",
        "key": "Co-BorrowerFirstName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6e3",
        "name": "Co-Borrower Last Name",
        "symbol": "###Co-BorrowerLastName###",
        "description": "Co-Borrower First Name",
        "key": "Co-BorrowerLastName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6e4",
        "name": "Co-Borrower Email Address",
        "symbol": "###Co-BorrowerEmailAddress###",
        "description": "Co-Borrower Email Address",
        "key": "Co-BorrowerEmailAddress",
        "fromAddess": false,
        "ccAddess": true,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6e5",
        "name": "EmailTag",
        "symbol": "###EmailTag###",
        "description": "Unique email tag for identifying email sender",
        "key": "EmailTag",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6e6",
        "name": "Loan Portal Url",
        "symbol": "###LoanPortalUrl###",
        "description": "Loan Portal Url",
        "key": "LoanPortalUrl",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6e7",
        "name": "Loan Status",
        "symbol": "###LoanStatus###",
        "description": "Status of Borrower Loan Application on Colaba",
        "key": "LoanStatus",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6e8",
        "name": "Subject Property Address",
        "symbol": "###SubjectPropertyAddress###",
        "description": "Subject Property Address",
        "key": "SubjectPropertyAddress",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6e9",
        "name": "Subject Property State",
        "symbol": "###SubjectPropertyState###",
        "description": "Subject Property State",
        "key": "SubjectPropertyState",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6ea",
        "name": "Subject Property State Abbreviation",
        "symbol": "###SubjectPropertyStateAbbreviation###",
        "description": "Subject Property State Abbreviation",
        "key": "SubjectPropertyStateAbbreviation",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6eb",
        "name": "Subject Property County",
        "symbol": "###SubjectPropertyCounty###",
        "description": "Subject Property County",
        "key": "SubjectPropertyCounty",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6ec",
        "name": "Subject Property City",
        "symbol": "###SubjectPropertyCity###",
        "description": "Subject Property City",
        "key": "SubjectPropertyCity",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6ed",
        "name": "Subject Property ZipCode",
        "symbol": "###SubjectPropertyZipCode###",
        "description": "Subject Property Zip Code",
        "key": "SubjectPropertyZipCode",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6ee",
        "name": "Loan Purpose",
        "symbol": "###LoanPurpose###",
        "description": "Loan Purpose",
        "key": "LoanPurpose",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6ef",
        "name": "Loan Amount",
        "symbol": "###LoanAmount###",
        "description": "Loan Application Loan Amount",
        "key": "LoanAmount",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6f0",
        "name": "Property Value",
        "symbol": "###PropertyValue###",
        "description": "Property Value",
        "key": "PropertyValue",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6f1",
        "name": "Property Type",
        "symbol": "###PropertyType###",
        "description": "Property Type",
        "key": "PropertyType",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6f2",
        "name": "Property Usage",
        "symbol": "###PropertyUsage###",
        "description": "Property Usage",
        "key": "PropertyUsage",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6f3",
        "name": "Residency Type",
        "symbol": "###ResidencyType###",
        "description": "Residency Type",
        "key": "ResidencyType",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6f4",
        "name": "Branch Nmls No",
        "symbol": "###BranchNmlsNo###",
        "description": "Branch Nmls No",
        "key": "BranchNmlsNo",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6f5",
        "name": "Business Unit Name",
        "symbol": "###BusinessUnitName###",
        "description": "Loan Application Business Unit Name",
        "key": "BusinessUnitName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6f6",
        "name": "Business Unit Phone Number",
        "symbol": "###BusinessUnitPhoneNumber###",
        "description": "Loan Application Business Unit Toll Free Number",
        "key": "BusinessUnitPhoneNumber",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6f7",
        "name": "Business Unit WebSite Url",
        "symbol": "###BusinessUnitWebSiteUrl###",
        "description": "Business Unit Website URL",
        "key": "BusinessUnitWebSiteUrl",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6f8",
        "name": "Loan Application Login Link",
        "symbol": "###LoanApplicationLoginLink###",
        "description": "Loan Application Login Link",
        "key": "LoanApplicationLoginLink",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6f9",
        "name": "Loan Officer Page Url",
        "symbol": "###LoanOfficerPageUrl###",
        "description": "Web Page of Loan Officer",
        "key": "LoanOfficerPageUrl",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6fa",
        "name": "Loan Officer First Name",
        "symbol": "###LoanOfficerFirstName###",
        "description": "Loan Officer First Name",
        "key": "LoanOfficerFirstName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6fb",
        "name": "Loan Officer Last Name",
        "symbol": "###LoanOfficerLastName###",
        "description": "Loan Officer Last Name",
        "key": "LoanOfficerLastName",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6fc",
        "name": "Request Document List",
        "symbol": "###RequestDocumentList###",
        "description": "Bullet Point list of Documents being requested",
        "key": "RequestDocumentList",
        "fromAddess": false,
        "ccAddess": false,
        "emailBody": true,
        "emailSubject": true
    },
    {
        "id": "5fc798c72048c60af5bfa6fd",
        "name": "Requestor User Email",
        "symbol": "###RequestorUserEmail###",
        "description": "Email Address of the Needs List requestor",
        "key": "RequestorUserEmail",
        "fromAddess": true,
        "ccAddess": true,
        "emailBody": true,
        "emailSubject": true
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

    static async updateEmailTemplateSort(emailTemplatesModel: RequestEmailTemplateActions[]){
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
            //let res = await Http.get(url);
            //return res.data;
            return Promise.resolve(tokensMock);
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

    static async updateEmailTemplate(id: number, templateName: string, templateDescription: string, fromAddress: string, CCAddress: string, subject: string, emailBody: string){
        let url = Endpoints.RequestEmailTemplateManager.POST.updateEmailTemplate();
       try {
           let res = await Http.post(url, {
            id: id,
            templateName: templateName,
            templateDescription: templateDescription,
            fromAddress: fromAddress,
            CCAddress: CCAddress,
            subject: subject,
            emailBody: emailBody,
           });
           return res.status;
       } catch (error) {
        console.log('error',error) 
       }
    }
}