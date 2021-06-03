import { APIResponse } from "../../../Entities/Models/APIResponse";
import { CurrentPropertyVal, FirstMortgageValue, HasFirstMortgage, HasSecondMortgage, PrimaryPropertyType, SecondMortgageValue } from "../../../Entities/Models/CurrentResidence";
import { SectionTypeEnum } from "../../../Utilities/Enumerations/MyPropertyEnums";

const propertyAddress = {
    "borrowerId": 1289,
    "housingStatusId": 1,
    "address": {
        "street": "ghgjhgjg",
        "unit": "",
        "city": "Denton",
        "stateId": 6,
        "zipCode": "76210",
        "countryId": 1,
        "countryName": "United States",
        "stateName": "Colorado"
    }
}
const haveFirstMortgage = { "hasFirstMortgage": false, "propertyTax": null, "homeOwnerInsurance": null, "loanApplicationId": 2, "id": 1004, "state": null }
const haveSecondMortgage = true;
const firstMortgageValue = {
    "id": 1004,
    "propertyTax": 200.00,
    "propertyTaxesIncludeinPayment": false,
    "homeOwnerInsurance": 300.00,
    "homeOwnerInsuranceIncludeinPayment": true,
    "loanApplicationId": 2,
    "firstMortgagePayment": 100,
    "unpaidFirstMortgagePayment": 400,
    "helocCreditLimit": 600,
    "isHeloc": true,
    "state": null
}
const secondMortgageValue = { "id": 1004, "loanApplicationId": 2, "secondMortgagePayment": 100, "unpaidSecondMortgagePayment": 400, "helocCreditLimit": 600, "isHeloc": true, "state": null }
const currentResidenceTypes = [
    {
        "id": 1,
        "name": "Single Family Property",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 2,
        "name": "Condominium",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 3,
        "name": "Townhouse",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 4,
        "name": "Duplex (2 Unit)",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 9,
        "name": "Manufactured Home",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 5,
        "name": "Triplex (3 Unit)",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 6,
        "name": "Quadplex (4 Unit)",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    }
]
const additionalResidenceTypes = [
    {
        "id": 1,
        "name": "Single Family Property",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 2,
        "name": "Condominium",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 3,
        "name": "Townhouse",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 16,
        "name": "Cooperative",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 4,
        "name": "Duplex (2 Unit)",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 9,
        "name": "Manufactured Home",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 5,
        "name": "Triplex (3 Unit)",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 6,
        "name": "Quadplex (4 Unit)",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    }
]
const primaryPropertyTypeResult = {
    id: 1053,
    propertyTypeId: 4,
    rentalIncome: 500
}
const finalScreenReviewResult = [{
    "id": 1033,
    "propertyType": "Townhouse",
    "typeId": 2,
    "firstName": "Shana",
    "lastName": "Lamb",
    "ownTypeId": 1,
    "address": {
        "street": "112 Somerset Rd",
        "unit": "",
        "city": "Singapore",
        "stateId": 0,
        "zipCode": "238164",
        "countryId": 201,
        "countryName": "Singapore",
        "stateName": "None"
    }
},
{
    "id": 1053,
    "propertyType": "Duplex (2 Unit)",
    "typeId": 1,
    "firstName": "Shana",
    "lastName": "Lamb",
    "ownTypeId": 1,
    "address": {
        "street": "111 Somerset Rd",
        "unit": "",
        "city": "Singapore",
        "stateId": 0,
        "zipCode": "238164",
        "countryId": 201,
        "countryName": "Singapore",
        "stateName": "None"
    }
}]

export default class MyPropertyActions {
    static PropertyValData = { "loanApplicationId": 1466, "id": 10, "propertyValue": 789789, "ownersDue": 789789, "isSelling": false, "state": null }

    static AdditionalPropertyInfoData = {
        id: 2080,
        propertyUsageId: 4,
        rentalIncome: 50
    }

    static AdditionalPropertyUsageData = [
        {
            "id": 3,
            "name": "This Will Be A Second Home",
            "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
        },
        {
            "id": 4,
            "name": "This Is An Investment Property",
            "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
        }
    ]

    static OwnAdditionalPropertyData = false;

    static PropertiesData = [{
        "id": 92,
        "propertyType": "Quadplex (4 Unit)",
        "typeId": 2,
        "firstName": null,
        "lastName": null,
        "ownTypeId": 0,
        "address": {
            "street": "12300 Bermuda Rd",
            "unit": "",
            "city": "Henderson",
            "stateId": 34,
            "zipCode": "89044",
            "countryId": 1,
            "countryName": "United States",
            "stateName": "Nevada"
        }
    }]

    static AdditionalPropertyAddressData = {
        city: "Henderson",
        countryId: 1,
        countryName: "United States",
        id: 2155,
        stateId: 34,
        stateName: "Nevada",
        street: "12300 Bermuda Rd",
        unit: "",
        zipCode: "89044",
    };

    static async getAllpropertytypes(sectionType: SectionTypeEnum) {
        if (sectionType == SectionTypeEnum.CurrentResidence) {
            return new APIResponse(200, currentResidenceTypes);
        }
        else if (sectionType == SectionTypeEnum.AdditionalProperty) {
            return new APIResponse(200, additionalResidenceTypes);
        }
    }

    static async deleteProperty(loanApplicationId: number, borrowerPropertyId: number) {
        console.log("!!!! Calling api mock function")
        return new APIResponse(200, true);
    }

    static async getPropertyValue(loanApplicationId: number, borrowerPropertyId: number) {

        return new APIResponse(200, this.PropertyValData);
    }

    static async getPrimaryBorrowerAddressDetail(loanApplicationId: number) {
        return new APIResponse(200, propertyAddress);
    }

    static async getPropertyList(loanApplicationId: number, borrowerId: number) {
        return new APIResponse(200, this.PropertiesData);
    }

    static async doYouOwnAdditionalProperty(loanApplicationId: number, borrowerId: number) {
        return new APIResponse(200, this.OwnAdditionalPropertyData);
    }

    static async addOrUpdatePrimaryPropertyType(dataValues: PrimaryPropertyType) {

        return new APIResponse(200, primaryPropertyTypeResult.id);
    }

    static async getBorrowerAdditionalPropertyInfo(loanApplicationId: number, borrowerPropertyId: number) {
        return new APIResponse(200, this.AdditionalPropertyInfoData)
    }

    static async getAllPropertyUsages(sectionId: number) {
        if (sectionId == 3) {
            return new APIResponse(200, this.AdditionalPropertyUsageData);
        }
    }

    static async getBorrowerAdditionalPropertyAddress(loanApplicationId: number, borrowerPropertyId: number) {
        return new APIResponse(200, this.AdditionalPropertyAddressData);
    }

    static async addOrUpdateAdditionalPropertyInfo(dataValues) {
        return new APIResponse(200, {});
    }

    static async doYouHaveFirstMortgage(loanApplicationId: number, borrowerPropertyId: number) {

        return new APIResponse(200, haveFirstMortgage);

    }

    static async getBorrowerPrimaryPropertyType(loanApplicationId: number, borrowerPropertyId: number) {
        return new APIResponse(200, primaryPropertyTypeResult);
    }

    static async getBorrowerAdditionalPropertyType(loanApplicationId: number, borrowerPropertyId: number) {
        return new APIResponse(200, primaryPropertyTypeResult);
    }

    static async getFirstMortgageValue(loanApplicationId: number, borrowerPropertyId: number) {

        return new APIResponse(200, firstMortgageValue);
    }

    static async doYouHaveSecondMortgage(loanApplicationId: number, borrowerPropertyId: number) {

        return new APIResponse(200, haveSecondMortgage)
    }

    static async getSecondMortgageValue(loanApplicationId: number, borrowerPropertyId: number) {

        return new APIResponse(200, secondMortgageValue)
    }

    static async addOrUpdatePropertyValue(dataValues: CurrentPropertyVal) {
        return new APIResponse(200, true);
    }

    static async addOrUpdateAdditionalPropertyType(dataValues: PrimaryPropertyType) {
        return new APIResponse(200, dataValues.id);
    }

    static async getFinalScreenReview(borrowerId: number, loanApplicationId: number) {
        return new APIResponse(200, finalScreenReviewResult);
    }

    static async addOrUpdateHasFirstMortgage(dataValues: HasFirstMortgage) {
        return new APIResponse(200, true);
    }

    static async addOrUpdateFirstMortgageValue(dataValues: FirstMortgageValue) {
        return new APIResponse(200, true);

    }

    static async addOrUpdateHasSecondMortgage(dataValues: HasSecondMortgage) {
        return new APIResponse(200, true);

    }

    static async addOrUpdateSecondMortgageValue(dataValues: SecondMortgageValue) {
        return new APIResponse(200, true);
    }

    
}
