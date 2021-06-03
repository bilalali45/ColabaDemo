import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { OtherIncomeType } from "../../Entities/Models/types";
import { Endpoints } from "../endpoints/Endpoint";


export default class IncomeActions {

    static async GetSourceOfIncomeList() {
        let url = Endpoints.Income.GET.GetSourceOfIncomeList();
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }
    static async GetMyMoneyHomeScreen(loanApplicationId :number) {
        let url = Endpoints.Business.GET.GetMyMoneyHomeScreen(loanApplicationId);
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }

    //#region Other Income Type

    static async GetAllIncomeGroupsWithOtherIncomeTypes() {
        let url = Endpoints.Income.GET.GetAllIncomeGroupsWithOtherIncomeTypes();
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }

    static async GetOtherIncomeInfo(incomeInfoId: number) {
        let url = Endpoints.Income.GET.GetOtherIncomeInfo(incomeInfoId);
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }

    static async AddOrUpdateOtherIncome(otherMonthlyIncome: OtherIncomeType) {
        let url = Endpoints.Income.POST.AddOrUpdateOtherIncome();
        try {
            let response  = await Http.post(url, otherMonthlyIncome);
            return new APIResponse(response.status,response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }
}