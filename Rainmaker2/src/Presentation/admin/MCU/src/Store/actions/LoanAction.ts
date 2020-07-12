import { Http } from 'rainsoft-js';
import { AxiosResponse } from "axios";
import { LoanApplication } from '../../Entities/Models/LoanApplication';
import { Endpoints } from '../endpoints/Endpoints';


const http = new Http();
http.setAuth('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNQ1UiLCJVc2VyUHJvZmlsZUlkIjoiMSIsIlVzZXJOYW1lIjoicmFpbnNvZnQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicmFpbnNvZnQiLCJGaXJzdE5hbWUiOiJTeXN0ZW0iLCJMYXN0TmFtZSI6IkFkbWluaXN0cmF0b3IiLCJFbXBsb3llZUlkIjoiMSIsImV4cCI6MTU5NDM5OTg2NCwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.Mxi1ev9QONxoyITMO0Qz5mKF9YtesPRr9T5Hk5hSnkg')
http.setBaseUrl('https://alphamaingateway.rainsoftfn.com')
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