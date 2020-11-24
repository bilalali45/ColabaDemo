import { LocalDB } from "../LocalDB";

const location = window.location;

export class ParamsService {
  static params = location.search.split("&");
  static getParam(param: string) {
    return this.params.find((p) => p?.includes(param))?.split("=")[1];
  }

  static storeParams(loanApplicationId: string) {
    LocalDB.setLoanAppliationId(loanApplicationId);
    // for (const param of params) {
    //   let extractedParam = this.getParam(param);
    //   if (extractedParam) {
    //     LocalDB.storeItem(param, extractedParam);
    //   }
    // }
  }
}
