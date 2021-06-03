import { Http } from "rainsoft-js";
import { APIResponse } from "../../../Entities/Models/APIResponse";

export default class LoanAplicationActions {

  static async getLoanApplicationFirstReview(loanApplicationId: number) {
   
      return new APIResponse(200, {});
  }
  static async getLoanApplicationSecondReview(loanApplicationId: number) {
    return new APIResponse(200, {});
  }
}
