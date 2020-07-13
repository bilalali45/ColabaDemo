import { Http } from 'rainsoft-js';
import { AxiosResponse } from "axios";
import { LoanApplication } from '../../Entities/Models/LoanApplication';
import { Endpoints } from '../endpoints/Endpoints';

const http = new Http();

export class LoanAction {

    static async getLoanApplicationDetail(loanApplicationId: string) {
        try {
            let result : AxiosResponse<LoanApplication> = await http.get<LoanApplication>(Endpoints.NeedList.GET.loan.info(loanApplicationId));
            
            return new LoanApplication().fromJson(result.data);
        } catch (error) {
            console.log(error);
        }
    }
}