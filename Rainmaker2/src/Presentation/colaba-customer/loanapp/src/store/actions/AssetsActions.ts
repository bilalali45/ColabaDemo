import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { AssetPayloadType, FinancialSatementPayloadType, RetirementPayloadType } from "../../Entities/Models/types";
import { Endpoints } from "../endpoints/Endpoint";


export default class AssetsActions {

    static async GetMyMoneyHomeScreen(loanApplicationId: number) {
        let url = Endpoints.Business.GET.GetMyAssetsHomeScreen(loanApplicationId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async GetAssetTypesByCategory(categoryId: number) {
        let url = Endpoints.AssetsEndpoints.GET.GetAssetTypesByCategory(categoryId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async GetEarnestMoneyDeposit(loanApplicationId :number) {
        let url = Endpoints.AssetsEndpoints.GET.GetEarnestMoneyDeposit(loanApplicationId);
        try {
            let response: any = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async UpdateEarnestMoneyDeposit(loanApplicationId: number, deposit: number | null,State: string, isEarnestMoneyProvided: boolean) {
        let url = Endpoints.AssetsEndpoints.POST.UpdateEarnestMoneyDeposit()
        try {
          let response = await Http.post(url, {
            LoanApplicationId: loanApplicationId,
            Deposit: deposit,
            State: State,
            isEarnestMoneyProvided
          });
          return new APIResponse(response.status,response.data);
        } catch (error) {
          console.log(error);
          return error;
        }
      }

      static async GetAllAssetCategories() {
        let url = Endpoints.AssetsEndpoints.GET.GetAllAssetCategories();
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }
    
    
      static async GetCollateralAssetTypes() {
        let url = Endpoints.AssetsEndpoints.GET.GetCollateralAssetTypes();
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async GetBorrowerAssetDetail(loanApplicationId: number, borrowerId: number, borrowerAssetId: number) {
        let url = Endpoints.AssetsEndpoints.GET.GetBorrowerAssetDetail(loanApplicationId, borrowerId, borrowerAssetId);
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }

    static async AddOrUpdateBorrowerAsset(assetInfo: AssetPayloadType) {
        let url = Endpoints.AssetsEndpoints.POST.AddOrUpdateBorrowerAsset()
        try {
          let response = await Http.post(url, assetInfo);
          return new APIResponse(response.status,response.data);
        } catch (error) {
          console.log(error);
          return error;
        }
      }

      static async GetRetirementAccount(loanApplicationId: number, borrowerId: number, borrowerAssetId: number) {
        let url = Endpoints.AssetsEndpoints.GET.GetRetirementAccount(loanApplicationId, borrowerId, borrowerAssetId);
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }

    static async UpdateRetirementAccount(assetInfo: RetirementPayloadType) {
        let url = Endpoints.AssetsEndpoints.POST.UpdateRetirementAccount()
        try {
          let response = await Http.post(url, assetInfo);
          return new APIResponse(response.status,response.data);
        } catch (error) {
          console.log(error);
          return error;
        }
      }

      static async GetAllFinancialAsset() {
        let url = Endpoints.AssetsEndpoints.GET.GetAllFinancialAsset();
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

      static async GetFinancialAsset(loanApplicationId: number, borrowerId: number, borrowerAssetId: number) {
        let url = Endpoints.AssetsEndpoints.GET.GetFinancialAsset(loanApplicationId, borrowerId, borrowerAssetId);
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }

    static async AddOrUpdateFinancialAsset(assetInfo: FinancialSatementPayloadType) {
        let url = Endpoints.AssetsEndpoints.POST.AddOrUpdateFinancialAsset()
        try {
          let response = await Http.post(url, assetInfo);
          return new APIResponse(response.status,response.data);
        } catch (error) {
          console.log(error);
          return error;
        }
      }
}