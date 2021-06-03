import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { CurrentEmploymentDetails } from "../../Entities/Models/Employment";
import { Endpoints } from "../endpoints/Endpoint";


export default class EmploymentActions {

    static async getEmployerInfo(loanApplicationId: number, borrowerId: number, incomeInfoId:number) {
        let url = Endpoints.Employemnt.GET.getEmployerInfo(loanApplicationId, borrowerId, incomeInfoId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async addOrUpdateCurrentEmployer(dataValues: CurrentEmploymentDetails) {
        const {BorrowerId, LoanApplicationId, EmploymentInfo, EmployerAddress, WayOfIncome, EmploymentOtherIncomes, State }: CurrentEmploymentDetails = dataValues;
        let url = Endpoints.Employemnt.POST.addOrUpdateCurrentEmploymentDetail();
        try {
            let response = await Http.post(url, {
                BorrowerId: BorrowerId,
                LoanApplicationId: LoanApplicationId,
                State: State,
                EmploymentInfo: EmploymentInfo,
                EmployerAddress: EmployerAddress,
                WayOfIncome: WayOfIncome,
                EmploymentOtherIncomes: EmploymentOtherIncomes
            })
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return null;
        }

    }

    static async getEmploymentOtherDefaultIncomeTypes() {
        let url = Endpoints.Employemnt.GET.getEmploymentOtherDefaultIncomeTypes();
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }
}
