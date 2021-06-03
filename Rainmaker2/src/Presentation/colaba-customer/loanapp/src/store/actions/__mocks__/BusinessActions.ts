import { CurrentBusinessDetails } from "../../../Entities/Models/Business";
import { APIResponse } from "../../../Entities/Models/APIResponse";


export default class BusinessActions {

    static async addOrUpdateBusiness(currentBusinessDetails: CurrentBusinessDetails) {
        return new APIResponse(200, {});
    }

    static async getAllBusinessTypes() {
        return new APIResponse(200, [{ "id": 3, "name": "Partnership", "fieldsInfo": null }, {
            "id": 4,
            "name": "Cooperation",
            "fieldsInfo": null
        }]);
    }

    static async getBusinessIncome(borrowerId: number, incomeInfoId: number) {
        return new APIResponse(200, {
            "loanApplicationId": 1175,
            "id": 2163,
            "borrowerId": 1300,
            "incomeTypeId": 3,
            "businessName": "Test Business Name",
            "businessPhone": "1231231231",
            "startDate": "2021-05-17T00:00:00",
            "jobTitle": "Test Job Title",
            "ownershipPercentage": 22.00,
            "address": {
                "street": "ased",
                "unit": "22",
                "city": "Atlanta",
                "stateId": 11,
                "zipCode": "30349",
                "countryId": 1,
                "countryName": null,
                "stateName": "Georgia"
            },
            "annualIncome": 22.00,
            "state": null
        });
    }

    static async deleteEmploymentIncome(loanApplicationId: number, borrowerId: number, incomeInfoId: number) {
        return new APIResponse(200, {});
    }

    static async deleteAsset(loanApplicationId: number, borrowerId: number, assetId: number) {
        return new APIResponse(200, true);
    }
}
