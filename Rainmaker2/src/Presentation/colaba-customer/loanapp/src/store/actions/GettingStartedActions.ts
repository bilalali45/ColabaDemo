import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { Endpoints } from "../endpoints/Endpoint";


export default class GettingStartedActions {

   static async getLoInfo() {
      let url = Endpoints.GettingStarted.GET.getLoInfo();
      try {
        let response = await Http.get(url);
        return new APIResponse(response.status, response.data);
      } catch (error) {
        console.log(error);
        return error;
      }
   }

   static async getAllLoanPurpose() {
       let url = Endpoints.GettingStarted.GET.getAllLoanPurpose();
       try {
        let response = await Http.get(url);
        return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
   }

   static async getAllLoanGoal(purposeId: number) {
       let url = Endpoints.GettingStarted.GET.getAllLoanGoal(purposeId);
       try {
        let response = await Http.get(url);
        return new APIResponse(response.status, response.data);
       } catch (error) {
        console.log(error);
        return error;
       }
   } 

   static async getLoanGoal(loanApplicationId: number) {
    let url = Endpoints.GettingStarted.GET.getLoanGoal(loanApplicationId);
    try {
     let response = await Http.get(url);
     return new APIResponse(response.status, response.data);
    } catch (error) {
     console.log(error);
     return error;
    }
} 

   static async createLoanGoal(loanApplicationId: number, loanGoalId: number, loanPurposeId: number, state: string){
     let url = Endpoints.GettingStarted.POST.createLoanGoal();
     try {
       let response = await Http.post(url,{
        loanApplicationId: loanApplicationId,
        loanGoal: loanGoalId,
        loanPurpose: loanPurposeId,
        state: state
       })
       return new APIResponse(response.status, response.data);
     } catch (error) {
       console.log(error);
       return null;
     }

   }
}
