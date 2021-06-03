import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { Endpoints } from "../endpoints/Endpoint";
import { LoanAmountDetails } from "../../Entities/Models/LoanAmountDetail";
import { PropertyIdentification } from "../../components/Home/MyNewMortgage/SubjectPropertyIntend/SubjectPropertyIntend";


export default class MyNewMortgageActions {

  static async getAllpropertytypes() {
    let url = Endpoints.MyNewMortgage.GET.getAllpropertytypes();
    try {
      let response = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async getpropertytype(loanApplicationId: number) {
    let url = Endpoints.MyNewMortgage.GET.getpropertytype(loanApplicationId);
    try {
      let response = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async addorupdatepropertytype(loanApplicationId: number, propertyTypeId: number, state: string) {
    let url = Endpoints.MyNewMortgage.POST.addorupdatepropertytype()
    try {
      let response = await Http.post(url, {
        loanApplicationId: loanApplicationId,
        propertyTypeId: propertyTypeId,
        state: state
      })
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async getSubjectPropertyLoanAmountDetail(loanApplicationId: number) {
    let url = Endpoints.MyNewMortgage.GET.getSubjectPropertyLoanAmountDetail(loanApplicationId);
    try {
      let response = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async addOrUpdateLoanAmountDetail(loanDetailModel: LoanAmountDetails) {
    let url = Endpoints.MyNewMortgage.POST.addOrUpdateLoanAmountDetail()
    try {
      let response = await Http.post(url, loanDetailModel)
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async getPropertyIdentifiedFlag(loanApplicationId: number) {
    let url = Endpoints.MyNewMortgage.GET.getPropertyIdentifiedFlag(loanApplicationId);
    try {
      let response = await Http.get(url)
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error.response.data;
    }
  }
  static async updatePropertyIdentified(data: PropertyIdentification) {
    let url = Endpoints.MyNewMortgage.POST.updatePropertyIdentified()
    try {
      let response = await Http.post(url, data)
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error.response.data;
    }
  }


}
