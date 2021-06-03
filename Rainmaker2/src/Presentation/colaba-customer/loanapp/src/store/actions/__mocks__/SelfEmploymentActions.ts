import {APIResponse} from "../../../Entities/Models/APIResponse";


export default class SelfEmploymentActions {

    static async getSelfBusinessIncome(borrowerId : number, incomeInfoId: number) {
        return new APIResponse(200, "");
    }

    static async addOrUpdateSelfBusiness(data) {
        return new APIResponse(200, "");
    }

}