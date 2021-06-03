import { APIResponse } from "../../../Entities/Models/APIResponse";
import { GiftAsset } from "../../../Entities/Models/types";

export class GiftAssetActionMockDataCollection {
    static GiftAssetData = {
        assetTypeId: 11,
        borrowerId: 14927,
        description: null,
        giftSourceId: 1,
        id: 2345,
        isDeposited: null,
        loanApplicationId: 14958,
        state: "{\"navState\":{\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\",\"lastPath\":\"/loanApplication/MyMoney/Assets/AssetsHome\",\"disabledSteps\":[\"Subject Property Intend\",\"Properties Owned\",\"Subject Property New Home\"],\"history\":[{\"name\":\"Loan Officer\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"},{\"name\":\"How Can We Help\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOiIxIn0=\"},{\"name\":\"Purchase Process State\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"},{\"name\":\"Loan Officer\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"},{\"name\":\"Purchase Process State\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"},{\"name\":\"Loan Officer\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"},{\"name\":\"Purchase Process State\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"},{\"name\":\"About Yourself\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"},{\"name\":\"Loan Officer\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"},{\"name\":\"Purchase Process State\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"},{\"name\":\"Income Home\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"},{\"name\":\"Earnest Money Deposit\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"},{\"name\":\"Income Home\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"},{\"name\":\"Earnest Money Deposit\",\"query\":\"eyJsb2ltYWdldXJsIjoiaHR0cHM6Ly9hcHBseS5sZW5kb3ZhLmNvbTo1MDAzL2NvbGFiYWNkbi9sZW5kb3ZhL2xlbmRvdmEvYWxpeWEuanBlZyIsImxvYW5wdXJwb3NlaWQiOjEsImxvYW5hcHBsaWNhdGlvbmlkIjoiMTQ5NTgiLCJsb2FuZ29hbGlkIjoiNCIsImJvcnJvd2VyaWQiOiIxNDkyNyJ9\"}]},\"loanapplicationid\":14958,\"borrowerid\":14927,\"loanpurposeid\":1,\"loangoalid\":4,\"incomeid\":null,\"myPropertyTypeId\":null}",
        value: 123,
        valueDate: null
    }
}

export default class GiftAssetActions {

    static async GetGiftSources() {
        return new APIResponse(200, null);
    }

    static async GetGiftAsset(borrowerAssetId: number, borrowerId: number, loanApplicationId: number) {
        console.log("!!! Getting gift asset from MOCK");
        return new APIResponse(200, GiftAssetActionMockDataCollection.GiftAssetData);
    }

    static async AddOrUpdateGiftAsset(giftAsset: GiftAsset) {
        return new APIResponse(200, {});
    }
}