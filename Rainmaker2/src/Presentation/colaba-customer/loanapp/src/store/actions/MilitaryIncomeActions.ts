
import { APIResponse } from "../../Entities/Models/APIResponse";
import { MilitaryIncomeInfo } from "../../Entities/Models/types";
import { Endpoints } from "../endpoints/Endpoint";
import { Http } from "rainsoft-js";

export default class MilitaryIncomeActions {

    static async GetMilitaryIncome(borrowerId: number, incomeInfoId: number) {
        
        let url = Endpoints.MilitaryIncome.GET.GetMilitaryIncome(borrowerId, incomeInfoId);
        try{
            let response = await Http.get(url);
            
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            
            return error;
        }
    }

    static async AddOrUpdateMilitaryIncome(militaryIncomeInfo: MilitaryIncomeInfo) {
        let url = Endpoints.MilitaryIncome.POST.AddOrUpdateMilitaryIncome()
        try {
          let response = await Http.post(url, militaryIncomeInfo)
          return new APIResponse(response.status,response.data);
        } catch (error) {
          console.log(error);
           return error;
        }
     }

}