import { APIResponse } from "../../../Entities/Models/APIResponse";
import { LoanAmountDetails } from "../../../Entities/Models/LoanAmountDetail";


const allPropertyMock = [
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

const loanAmountDetailMock = {
    statusCode: 200,
    data: {
        "loanApplicationId": 4002,
        "propertyValue": 500000,
        "downPayment": 150000,
        "giftPartOfDownPayment": true,
        "giftPartReceived": true,
        "dateOfTransfer": "2021-04-10T00:00:00",
        "giftAmount": 70000
    }

}

export default class MyNewMortgageActions {

    static async getAllpropertytypes() {
        return new APIResponse(200, allPropertyMock);
        //return Promise.resolve(allPropertyMock);
    }

    static async getpropertytype(loanApplicationId: number) {
        return Promise.resolve(allPropertyMock[0]);
    }

    static async addorupdatepropertytype(loanApplicationId: number, propertyTypeId: number, state: string) {
        return Promise.resolve(true);
    }

    static async getSubjectPropertyLoanAmountDetail(loanApplicationId: number) {

        return Promise.resolve(loanAmountDetailMock);
    }

    static async addOrUpdateLoanAmountDetail(loanDetailModel: LoanAmountDetails) {
        return Promise.resolve(true);
    }

}
