import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { EmploymentHistoryDetails } from "../../Entities/Models/EmploymentHistory";
import { Endpoints } from "../endpoints/Endpoint";


export default class EmploymentHistoryActions {

    static async getBorrowersEmploymentHistory(loanApplicationId: number) {
        let url = Endpoints.EmploymentHistory.GET.getBorrowersEmploymentHistory(loanApplicationId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async GetBorrowerIncomes(loanApplicationId: number, borrowerId:number) {
        let url = Endpoints.EmploymentHistory.GET.getBorrowerIncomes(loanApplicationId, borrowerId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async addOrUpdatePreviousEmploymentDetail(dataValues: EmploymentHistoryDetails) {
        const { BorrowerId, LoanApplicationId, State, EmploymentInfo, EmployerAddress, WayOfIncome }: EmploymentHistoryDetails = dataValues;
        delete EmploymentInfo.HasOwnershipInterest;
        let url = Endpoints.EmploymentHistory.POST.addOrUpdatePreviousEmploymentDetail();
        try {
            let response = await Http.post(url, {
                BorrowerId: BorrowerId,
                LoanApplicationId: LoanApplicationId,
                State: State,
                EmploymentInfo: EmploymentInfo,
                EmployerAddress: EmployerAddress,
                WayOfIncome: WayOfIncome
            })
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return null;
        }

    }
}
