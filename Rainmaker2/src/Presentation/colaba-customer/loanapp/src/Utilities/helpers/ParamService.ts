import { LocalDB } from "../../lib/LocalDB";

const location = window.location;

export class ParamsService {
  
  static getParam(param: string) {
    console.log('---------> 13');
    const params = location.search.split("&");
    return params.find((p) => p?.includes(param.toLowerCase()))?.split("=")[1];
  }

  static storeParams(loanApplicationId: string) {
    if(loanApplicationId)
      LocalDB.setLoanAppliationId(loanApplicationId);
  }
}
