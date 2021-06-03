import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { Endpoints } from "../endpoints/Endpoint";
import {
    TransactionProceedsFromLoanDTO,
    TransactionProceedsFromRealAndNonRealEstateDTO
} from "../../Entities/Models/TransactionProceeds";


export default class TransactionProceedsActions {

    static async GetFromLoanRealState(BorrowerAssetId: number, AssetTypeId: number, BorrowerId: number, LoanApplicationId :number) {
        let url = Endpoints.TransactionProceeds.GET.GetFromLoanRealState(BorrowerAssetId, AssetTypeId, BorrowerId, LoanApplicationId);
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }

    static async GetFromLoanNonRealState(BorrowerAssetId: number, AssetTypeId: number, BorrowerId: number, LoanApplicationId :number) {
        let url = Endpoints.TransactionProceeds.GET.GetFromLoanNonRealState(BorrowerAssetId, AssetTypeId, BorrowerId, LoanApplicationId);
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }

    static async GetProceedsfromloan(BorrowerAssetId: number, AssetTypeId: number, BorrowerId: number, LoanApplicationId :number) {
        let url = Endpoints.TransactionProceeds.GET.GetProceedsfromloan(BorrowerAssetId, AssetTypeId, BorrowerId, LoanApplicationId);
        try{
            let response = await Http.get(url);
            return new APIResponse(response.status,response.data);
        } catch(error) {
            console.log(error);
            return error;
        }
    }

    static async AddOrUpdateAssestsRealState(transactionProceeds: TransactionProceedsFromRealAndNonRealEstateDTO) {
        let url = Endpoints.TransactionProceeds.POST.AddOrUpdateAssestsRealState();
        try {
            let response  = await Http.post(url, transactionProceeds);
            return new APIResponse(response.status,response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async AddOrUpdateAssestsNonRealState(transactionProceeds: TransactionProceedsFromRealAndNonRealEstateDTO) {
        let url = Endpoints.TransactionProceeds.POST.AddOrUpdateAssestsNonRealState();
        try {
            let response  = await Http.post(url, transactionProceeds);
            return new APIResponse(response.status,response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }


    static async AddOrUpdateProceedsfromloan(transactionProceeds: TransactionProceedsFromLoanDTO) {
        let url = Endpoints.TransactionProceeds.POST.AddOrUpdateProceedsfromloan();
        try {
            let response  = await Http.post(url, transactionProceeds);
            return new APIResponse(response.status,response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async AddOrUpdateProceedsfromloanOther(transactionProceeds: TransactionProceedsFromLoanDTO) {
        let url = Endpoints.TransactionProceeds.POST.AddOrUpdateProceedsfromloanOther();
        try {
            let response  = await Http.post(url, transactionProceeds);
            return new APIResponse(response.status,response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }
}