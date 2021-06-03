import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { Endpoints } from "../endpoints/Endpoint";


export default class IncomeReviewActions {
    //#endregion
    static async GetIncomeSectionReview(loanApplicationId :number) {
        let url = Endpoints.Income.GET.GetIncomeSectionReview(loanApplicationId);
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }

    static async GetBorrowerIncomesForReview(loanApplicationId :number) {
        let url = Endpoints.Income.GET.GetBorrowerIncomesForReview(loanApplicationId);
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }


}