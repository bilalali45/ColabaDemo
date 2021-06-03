
import { APIResponse } from "../../../Entities/Models/APIResponse";
import { MilitaryIncomeInfo } from "../../../Entities/Models/types";


const mock = {
    "loanApplicationId": 2185,
    "id": 2167,
    "borrowerId": 2306,
    "employerName": "fff",
    "jobTitle": "ff",
    "startDate": "2021-04-26T00:00:00",
    "yearsInProfession": 77,
    "address": {
        "street": "16601 N Pima Rd",
        "unit": "",
        "city": "Scottsdale",
        "stateId": 4,
        "zipCode": "85260",
        "countryId": 1,
        "countryName": "United States",
        "stateName": "Arizona"
    },
    "monthlyBaseSalary": 44444.00,
    "militaryEntitlements": 44444,
    "state": null
}
export default class MilitaryIncomeActions {

    static async GetMilitaryIncome(borrowerId: number, incomeInfoId: number) {
        console.log("======> GetMilitaryIncome Mock action", incomeInfoId)
        return new APIResponse(200, mock);
       
    }

    static async AddOrUpdateMilitaryIncome(militaryIncomeInfo: MilitaryIncomeInfo) {
       
        return new APIResponse(200, true);
    }

}