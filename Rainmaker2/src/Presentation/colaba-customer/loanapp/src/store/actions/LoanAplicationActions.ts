import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";

import { Endpoints } from "../endpoints/Endpoint";

export default class LoanAplicationActions {

  static async getLoanApplicationFirstReview(loanApplicationId: number) {
    let url = Endpoints.LoanApplication.GET.getLoanApplicationFirstReview(loanApplicationId);
    try {
      let response = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error.response.data;
    }
  }
  static async getLoanApplicationSecondReview(loanApplicationId: number) {
    let url = Endpoints.LoanApplication.GET.getLoanApplicationSecondReview(loanApplicationId);
    try {
      let response = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error.response.data;
    }
  }
}
