import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { Endpoints } from "../endpoints/Endpoint";
import { GiftAsset } from '../../Entities/Models/types';

export default class GiftAssetActions {

    static async GetGiftSources() {
        let url = Endpoints.GiftAssetEndpoints.GET.GetGiftSources();

        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async GetGiftAsset(borrowerAssetId: number, borrowerId: number, loanApplicationId: number) {
        let url = Endpoints.GiftAssetEndpoints.GET.GetGiftAsset(borrowerAssetId, borrowerId, loanApplicationId);

        try {
            let response = await Http.get(url)
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async AddOrUpdateGiftAsset(giftAsset: GiftAsset) {
        let url = Endpoints.GiftAssetEndpoints.POST.AddOrUpdateGiftAsset()
        try {
            let response = await Http.post(url, giftAsset)
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }
}