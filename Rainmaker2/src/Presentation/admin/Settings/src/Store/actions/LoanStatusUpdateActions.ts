import { AxiosResponse } from "axios";
import { Http } from "rainsoft-js";
import LoanStatusUpdateModel, { LoanStatus } from "../../Entities/Models/LoanStatusUpdate";
import { Endpoints } from "../endpoints/Endpoints";

let mockData = 
    {
        "isActive": true,
        "loanStatuses": [
            {
                "id": 2,
                "mcuName": "Application Submitted",
                "statusId": 1,
                "tenantId": 1,
                "fromStatusId": 3,
                "fromStatus": "Processing",
                "toStatusId": 4,
                "toStatus": "Underwriting",
                "noofDays":2,
                "recurringTime":"2021-01-19T14:00:08Z",
                "isActive":true,
                "emailId":1,
                "fromAddress":"###RequestorUserEmail###",
                "ccAddress":"###Co-BorrowerEmailAddress###",
                "subject": "###BusinessUnitName###",
                "body": "<p>vvvv</p>\n",
            },
            {
                "id": 3,
                "mcuName": "Processing",
                "statusId": 2,
                "tenantId": 1,
                "fromStatusId": 5,
                "fromStatus": "Approved with Conditions",
                "toStatusId": 6,
                "toStatus": "Closing",
                "noofDays":2,
                "recurringTime":"2021-01-19T14:00:08Z",
                "isActive":true,
                "emailId":2,
                "fromAddress":"###RequestorUserEmail###",
                "ccAddress":"###Co-BorrowerEmailAddress###",
                "subject": "###BusinessUnitName###",
                "body": "<p>vvvv</p>\n",
            }
        ]
    }


export class LoanStatusUpdateActions {

 static async fetchLoanStatusUpdate(){
     
     let url = Endpoints.LoanStatusUpdateManager.GET.getLoanStatusUpdateSettings();
     try {
        let res: any = await Http.get(url);
        let mappedData = new LoanStatusUpdateModel(res.data.isActive, res.data?.loanStatuses);
         return mappedData;
         
     } catch (error) {
        console.log('error',error)
     }
 }

 static async updateAllEnableDisableLoanStatusEmail(isActive: boolean){
    let url = Endpoints.LoanStatusUpdateManager.POST.updateAllEnableDisableLoanStatusEmail();
    try {
        let res = await Http.post(url, {"isActive": isActive});
        return res;
    } catch (error) {
     console.log('error',error)
    }
}

 static async updateEnableDisableLoanStatusEmail(id?: number, isActive?: boolean){
    let url = Endpoints.LoanStatusUpdateManager.POST.updateEnableDisableLoanStatusEmail();
    try {
        let res = await Http.post(url, {
            id: id,
            isActive: isActive
        });
        return res.status;
    } catch (error) {
     console.log('error',error)
    }
 }
 static async addLoanStatusEmail(loanStatusModel: LoanStatus){
     let stringyfydata = {"isActive": true,"loanStatuses": [loanStatusModel]}
     let url = Endpoints.LoanStatusUpdateManager.POST.addLoanStatusUpdateListSetting();
     try {
         let res = await Http.post(url, stringyfydata);
         return res.status;
     } catch (error) {
      console.log('error',error)
     }
  }

  static async updateLoanStatusEmail(loanStatusModel: LoanStatusUpdateModel){
    let url = Endpoints.LoanStatusUpdateManager.POST.updateLoanStatusListSetting()
    try {
        let res = await Http.post(url, loanStatusModel);
        return res.status;
    } catch (error) {
     console.log('error',error)
    }
 }
 
 static async fetchTokens() {
    let url = Endpoints.RequestEmailTemplateManager.GET.tokens();
    try {
        let res: any = await Http.get(url);
        return res.data;
    } catch (error) {
        console.log('error',error) 
    }
}
}