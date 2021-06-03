import { Http } from "rainsoft-js";
import { APIResponse } from "../../../Entities/Models/APIResponse";


export class CommonActions {
    static async getAllMaritialStatuses() {
       
    }

    static async getPendingLoanApplication(loanAppId: string) {
        console.log(" getPendingLoanApplication >>>>>>>>>>>>>>>>>>>>>");
        return new APIResponse(200,{"loanApplicationId":6357,"setting":[{"name":"AdditionalPropertyMortgage","value":1},{"name":"AnyPartOfDownPaymentGift","value":1},{"name":"CashOut","value":1},{"name":"CoBorower_VeteranStatus","value":1},{"name":"CoBorrower_CellNumber","value":3},{"name":"CoBorrower_DOB","value":3},{"name":"CoBorrower_EmailAddress","value":3},{"name":"CoBorrower_HomeNumber","value":3},{"name":"CoBorrower_SSN","value":1},{"name":"CoBorrower_WorkNumber","value":3},{"name":"CurrentResidenceMortgage","value":1},{"name":"DebtConsolidation","value":1},{"name":"EmploymentHistorySection","value":1},{"name":"IncomeSection","value":1},{"name":"InsuranceIncludedInPayment","value":2},{"name":"MyProperties","value":1},{"name":"PrimaryBorower_VeteranStatus","value":1},{"name":"PrimaryBorrower_CellNumber","value":3},{"name":"PrimaryBorrower_DOB","value":1},{"name":"PrimaryBorrower_HomeNumber","value":3},{"name":"PrimaryBorrower_SSN","value":3},{"name":"PrimaryBorrower_WorkNumber","value":3},{"name":"PropertyType_MyProperties","value":1},{"name":"PropertyType_SubjectProperty","value":2},{"name":"TaxIncludedInPayment","value":1},{"name":"WhereAreYouInPurchaseProcess","value":1}],"state":"{\"navState\":{\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOiIxIiwibG9hbmFwcGxpY2F0aW9uaWQiOiI2MzU3IiwibG9hbmdvYWxpZCI6IjQiLCJib3Jyb3dlcmlkIjoiNjYxNSJ9\",\"lastPath\":\"/loanApplication/MyMoney/Assets/AssetsHome\",\"disabledSteps\":[],\"history\":[{\"name\":\"Type Of Proceeds From Transaction\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOiIxIiwibG9hbmFwcGxpY2F0aW9uaWQiOiI2MzU3IiwibG9hbmdvYWxpZCI6IjQiLCJib3Jyb3dlcmlkIjoiNjYxNSJ9\"}]},\"loanapplicationid\":6357,\"borrowerid\":6615,\"loanpurposeid\":1,\"loangoalid\":4,\"incomeid\":null,\"myPropertyTypeId\":null}","restartLoanApplication":true});
    }

    static async getAllloanpurpose() {
       
    }

    static async updateState(loanApplicationId: number, state: string) {
       
    }

};

