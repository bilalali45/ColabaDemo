import LoanStatusUpdateModel from "../../../Entities/Models/LoanStatusUpdate";

const mockData = {
    "isActive": true,
    "loanStatuses": [
        {
            "id": 2,
            "mcuName": "Application Submitted",
            "statusId": 1,
            "tenantId": 1,
            "fromStatusId": 2,
            "fromStatus": "Application Submitted",
            "toStatusId": 3,
            "toStatus": "Processing",
            "noofDays": 0,
            "recurringTime": new Date("2021-01-24T20:26:45.707"),
            "isActive": true,
            "emailId": 1,
            "fromAddress": "jahangir@gmail.com",
            "ccAddress": "cc@gmail.com",
            "subject": "test subject........",
            "body": "<p>Application submitted ------&gt; Processing</p>\n<p>edited!llllkkkknnnn</p>\n"
        },
        {
            "id": 3,
            "mcuName": "Processing",
            "statusId": 0,
            "tenantId": 0,
            "fromStatusId": 0,
            "fromStatus": undefined,
            "toStatusId": 0,
            "toStatus": undefined,
            "noofDays": 0,
            "recurringTime": new Date("2021-01-24T20:26:45.707"),
            "isActive": true,
            "emailId": 0,
            "fromAddress": undefined,
            "ccAddress": undefined,
            "subject": undefined,
            "body": undefined
        },
        {
            "id": 4,
            "mcuName": "Underwriting",
            "statusId": 2,
            "tenantId": 1,
            "fromStatusId": 4,
            "fromStatus": "Underwriting",
            "toStatusId": 6,
            "toStatus": "Closing",
            "noofDays": 0,
            "recurringTime": new Date("2021-01-24T20:26:45.707"),
            "isActive": true,
            "emailId": 2,
            "fromAddress": "###RequestorUserEmail###",
            "ccAddress": "###Co-BorrowerEmailAddress###",
            "subject": "ddd",
            "body": "<p>Edited</p>\n"
        },                                      
    ]
}



export class LoanStatusUpdateActions {
    
    static async fetchLoanStatusUpdate(){         
           let mappedData = new LoanStatusUpdateModel(mockData.isActive, mockData?.loanStatuses);
            return Promise.resolve(mappedData);        
    }
}