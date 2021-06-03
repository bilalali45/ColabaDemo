import { APIResponse } from "../../../Entities/Models/APIResponse";
import { SubjectAddressPropertyReqObj } from "../../../Entities/Models/types";

const subjectPropertyData = { "id": 26513, "street": "543trt", "unit": "", "city": "Traverse City", "stateId": 23, "stateName": "Michigan", "zipCode": "49684", "countryId": 1, "countryName": "United States", "estimatedClosingDate": "2021-04-21" }

export default class MyNewPropertyAddressActions {
    static async addOrUpdatePropertyAddress(reqObj: SubjectAddressPropertyReqObj) {
        return new APIResponse(200, true);
    }

    static async getPropertyAddress(loanApplicationId: number) {

        return new APIResponse(200, subjectPropertyData);

    }
}
