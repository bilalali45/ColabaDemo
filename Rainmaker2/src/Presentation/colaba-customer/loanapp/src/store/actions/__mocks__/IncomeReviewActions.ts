import {APIResponse} from "../../../Entities/Models/APIResponse";

export default class IncomeReviewActions {
    //#endregion
    static async GetIncomeSectionReview(loanApplicationId :number) {
        return new APIResponse(200, {} );
    }
}