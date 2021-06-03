
import { Http } from "rainsoft-js";
import { AxiosResponse } from 'axios';
import { Endpoints } from "../endpoints/Endpoint";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { SubjectAddressPropertyReqObj } from "../../Entities/Models/types";


export default class MyNewPropertyAddressActions {
    static async addOrUpdatePropertyAddress(reqObj: SubjectAddressPropertyReqObj) {
    let url = Endpoints.MyNewPropertyAddress.POST.addOrUpdatePropertyAddress();
    let { loanApplicationId, street, unit, city, stateId, stateName, zipCode, estimatedClosingDate, state }: SubjectAddressPropertyReqObj = reqObj;
    try {
      let res
        : AxiosResponse = await Http.post(
          url,
          {
            loanApplicationId: loanApplicationId,
            street: street,
            unit: unit,
            city: city,
            stateId: stateId,
            stateName: stateName,
            zipCode: zipCode,
            estimatedClosingDate:estimatedClosingDate,
            state: state

          }
        );
      return new APIResponse(res.status, "");
    } catch (error) {
      console.log(error);
      return error;
    }

  }

  static async getPropertyAddress(loanApplicationId: number) {
    let url = Endpoints.MyNewPropertyAddress.GET.getPropertyAddress(loanApplicationId);
    try {
      let response: any = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }
}
