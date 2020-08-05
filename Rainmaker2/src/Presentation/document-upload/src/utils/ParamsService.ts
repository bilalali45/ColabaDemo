import { Auth } from "../services/auth/Auth";

const location = window.location;

export class ParamsService {
  static params = location.search.split("&");
  static getParam(param: string) {
    return this.params.find((p) => p?.includes(param))?.split("=")[1];
  }

  static storeParams(loanApplicationId) {
    Auth.setLoanAppliationId(loanApplicationId);
  }
}
