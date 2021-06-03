import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { Endpoints } from "../endpoints/Endpoint";


export default class SelfEmploymentActions {

    static async getSelfBusinessIncome(borrowerId : number, incomeInfoId: number) {
        let url = Endpoints.Income.GET.GetSelfBusinessIncome(borrowerId, incomeInfoId);
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }

    static async addOrUpdateSelfBusiness(data) {
        let url = Endpoints.Income.POST.AddOrUpdateSelfBusiness();
        try {
            let response  = await Http.post(url, data);
            return new APIResponse(response.status,response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

}