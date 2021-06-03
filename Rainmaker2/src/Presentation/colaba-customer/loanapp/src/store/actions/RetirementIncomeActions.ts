import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { RetirementIncomeInfo } from "../../Entities/Models/types";
import { Endpoints } from "../endpoints/Endpoint";


export default class RetirementIncomeActions {

    static async GetRetirementIncomeTypes() {
        let url = Endpoints.RetirementIncome.GET.GetRetirementIncomeTypes();
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async GetRetirementIncomeInfo(incomeInfoId: number, borrowerId: number) {
        let url = Endpoints.RetirementIncome.GET.GetRetirementIncomeInfo()
        try {
            let response = await Http.get(`${url}?incomeInfoId=${incomeInfoId}&borrowerId=${borrowerId}`)
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async AddOrUpdateRetirementIncomeInfo(retirementIncomeInfo: RetirementIncomeInfo) {
        let url = Endpoints.RetirementIncome.POST.AddOrUpdateRetirementIncomeInfo()
        try {
            let response = await Http.post(url, retirementIncomeInfo)
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

}