import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { AddOrUpdateOtherAssetsInfoProto } from "../../Entities/Models/types";
import { Endpoints } from "../endpoints/Endpoint";


export default class OtherAssetsActions {

    static async GetAllAssetCategories() {
        let url = Endpoints.OtherAssets.GET.GetAllAssetCategories();
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async GetAssetTypesByCategory(categoryId: number) {
        let url = Endpoints.OtherAssets.GET.GetAssetTypesByCategory(categoryId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async GetOtherAssetsInfo(otherAssetId :number) {
        let url = Endpoints.OtherAssets.GET.GetOtherAssetsInfo(otherAssetId);
        try {
            let response: any = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async AddOrUpdateOtherAssetsInfo(addOrUpdateOtherAssets: AddOrUpdateOtherAssetsInfoProto) {
        let url = Endpoints.OtherAssets.POST.AddOrUpdateOtherAssetsInfo()
        try {
          let response = await Http.post(url, 
            addOrUpdateOtherAssets
          );
          return new APIResponse(response.status,response.data);
        } catch (error) {
          console.log(error);
          return error;
        }
      }
}