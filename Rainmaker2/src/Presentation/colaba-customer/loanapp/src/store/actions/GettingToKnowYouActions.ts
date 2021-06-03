import { Http } from "rainsoft-js";
import { AxiosResponse } from 'axios';
import { Endpoints } from "../endpoints/Endpoint";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { CurrentHomeAddressReqObj, DobSSNReqObj } from "../../Entities/Models/types";


export default class GettingToKnowYouActions {
  static async getAllCountries() {
    let url = Endpoints.GettingToKnowYou.GET.getAllCountries();
    try {
      let response: any = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async getStates(tenantState:boolean) {
    let url:string;
    if(tenantState) url = Endpoints.GettingToKnowYou.GET.getTenantStates();
    else url = Endpoints.GettingToKnowYou.GET.getAllStates();
    try {
      let response: any = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async getHomeOwnershipTypes() {
    let url = Endpoints.GettingToKnowYou.GET.getHomeOwnershipTypes();
    try {
      let response: any = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async addOrUpdatePrimaryBorrowerAddress(reqObj: CurrentHomeAddressReqObj) {
    let url = Endpoints.GettingToKnowYou.POST.addOrUpdatePrimaryBorrowerAddress();
    let { loanApplicationId, id, street, unit, city, stateId, stateName, zipCode, countryId, countryName, housingStatusId, rent, state }: CurrentHomeAddressReqObj = reqObj;
    try {
      let res
        : AxiosResponse = await Http.post(
          url,
          {
            loanApplicationId: loanApplicationId,
            id:id,
            street: street,
            unit: unit,
            city: city,
            stateId: stateId,
            stateName: stateName,
            countryName: countryName,
            zipCode: zipCode,
            countryId: countryId,
            housingStatusId: housingStatusId,
            rent: rent,
            state: state

          }
        );
      return new APIResponse(res.status, "");
    } catch (error) {
      console.log(error);
      return error;
    }

  }


  static async getBorrowerAddress(loanApplicationId: number, borrowerId:number) {
    let url = Endpoints.GettingToKnowYou.GET.getBorrowerAddress(loanApplicationId, borrowerId);
    try {
      let response: any = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async getSearchByZipCode(searchKey: number) {
    let url = Endpoints.GettingToKnowYou.GET.getSearchByZipCode(searchKey);
    try {
      let response: any = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async getSearchByStateCountyCity(searchKey: string) {
    let url = Endpoints.GettingToKnowYou.GET.getSearchByStateCountyCity(searchKey);
    try {
      let response: any = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async getZipCodeByStateCountryCity(cityId: number, countryId: number, stateId: number) {
    let url = Endpoints.GettingToKnowYou.GET.getZipCodeByStateCountryCity(cityId, countryId, stateId);
    try {
      let response: any = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async getZipCodeByStateCountyCityName(cityName: string, stateName: string, countyName: string, zipCode: string) {
    let url = Endpoints.GettingToKnowYou.GET.getZipCodeByStateCountyCityName(cityName, stateName, countyName, zipCode);
    try {
      let response: any = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async getDobSSN(loanApplicationId:number, borrowerId:number) {
    
    let url = Endpoints.DobSSN.GET.getBorrowerDobSsn(loanApplicationId, borrowerId);
    try {
      let response: any = await Http.get(url);
      return new APIResponse(response.status, response.data);
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async addOrUpdateDobSSN(reqObj: DobSSNReqObj) {
    let url = Endpoints.GettingToKnowYou.POST.addOrUpdateDobSsn();
    let { LoanApplicationId, BorrowerId, DobUtc, Ssn }: DobSSNReqObj = reqObj;
    
    try {
      let res
        : AxiosResponse = await Http.post(
          url,
          {
            LoanApplicationId: LoanApplicationId,
            BorrowerId:BorrowerId,
            DobUtc: DobUtc,
            Ssn: Ssn,

          }
        );
      return new APIResponse(res.status, "");
    } catch (error) {
      console.log(error);
      return error;
    }

  }

};
