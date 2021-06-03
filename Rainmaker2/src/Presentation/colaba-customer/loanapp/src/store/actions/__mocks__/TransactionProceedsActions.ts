import { Http } from "rainsoft-js";
import { APIResponse } from "../../../Entities/Models/APIResponse";
import { TransactionProceedsFromLoanDTO, TransactionProceedsFromRealAndNonRealEstateDTO } from "../../../Entities/Models/TransactionProceeds";




export default class TransactionProceedsActions {

    static async GetFromLoanRealState(BorrowerAssetId: number, AssetTypeId: number, BorrowerId: number, LoanApplicationId :number) {
        return new APIResponse(200,{"result":{"id":2303,"assetTypeId":14,"asstTypeName":"Proceeds from selling real estate","assetTypeCategoryName":"Proceeds from Transactions","description":"Proceeds from selling real estate","value":786},"id":45,"exception":null,"status":5,"isCanceled":false,"isCompleted":true,"isCompletedSuccessfully":true,"creationOptions":0,"asyncState":null,"isFaulted":false});
    }

    static async GetFromLoanNonRealState(BorrowerAssetId: number, AssetTypeId: number, BorrowerId: number, LoanApplicationId :number) {
        return new APIResponse(200,{"result":{"id":2301,"assetTypeId":13,"asstTypeName":"Proceeds from selling non-real estate assets","assetTypeCategoryName":"Proceeds from Transactions","description":"Proceeds from selling non-real estate","value":110},"id":42,"exception":null,"status":5,"isCanceled":false,"isCompleted":true,"isCompletedSuccessfully":true,"creationOptions":0,"asyncState":null,"isFaulted":false});
    }

    static async GetProceedsfromloan(BorrowerAssetId: number, AssetTypeId: number, BorrowerId: number, LoanApplicationId :number) {
        return new APIResponse(200,{"result":{"id":2272,"borrowerId":6615,"assetTypeId":12,"asstTypeName":"Proceeds from a Loan","assetTypeCategoryName":"Proceeds from Transactions","description":null,"value":1101111,"collateralAssetTypeId":4,"collateralAssetName":"Other","collateralAssetOtherDescription":"sdfsdf","securedByCollateral":true},"id":38,"exception":null,"status":5,"isCanceled":false,"isCompleted":true,"isCompletedSuccessfully":true,"creationOptions":0,"asyncState":null,"isFaulted":false});
    }

    static async AddOrUpdateAssestsRealState(transactionProceeds: TransactionProceedsFromRealAndNonRealEstateDTO) {
        return new APIResponse(200,{});
    }

    static async AddOrUpdateAssestsNonRealState(transactionProceeds: TransactionProceedsFromRealAndNonRealEstateDTO) {
        return new APIResponse(200,{});
    }


    static async AddOrUpdateProceedsfromloan(transactionProceeds: TransactionProceedsFromLoanDTO) {
        return new APIResponse(200,{});
    }

    static async AddOrUpdateProceedsfromloanOther(transactionProceeds: TransactionProceedsFromLoanDTO) {
        return new APIResponse(200,{});
    }
}