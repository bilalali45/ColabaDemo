import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { Endpoints } from "../endpoints/Endpoint";
import {CurrentBusinessDetails} from "../../Entities/Models/Business";


export default class BusinessActions {

    static async addOrUpdateBusiness(currentBusinessDetails: CurrentBusinessDetails) {
        let url = Endpoints.Business.POST.AddOrUpdateBusiness();
        try {
            let response = await Http.post(url, currentBusinessDetails)
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }

    }
    static async getAllBusinessTypes() {
        let url = Endpoints.Business.GET.GetAllBusinessTypes();
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async getBusinessIncome(borrowerId :number,incomeInfoId :number) {
        let url = Endpoints.Business.GET.GetBusinessIncome(borrowerId,incomeInfoId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async deleteEmploymentIncome(loanApplicationId :number,borrowerId :number,incomeInfoId :number) {
        let url = Endpoints.Business.DELETE.DeleteEmploymentIncome(loanApplicationId, borrowerId, incomeInfoId);
        try {
            let response = await Http.delete(url,{},{},false);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async deleteAsset(loanApplicationId :number,borrowerId :number,assetId :number) {
        let url = Endpoints.Business.DELETE.DeleteAsset(loanApplicationId, borrowerId, assetId);
        try {
            let response = await Http.delete(url,{},{},false);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }
}
