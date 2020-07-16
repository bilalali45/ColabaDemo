import { Http } from 'rainsoft-js';
import { AxiosResponse } from "axios";
import { LoanApplication } from '../../Entities/Models/LoanApplication';
import { Endpoints } from '../endpoints/Endpoints';
import { NeedList } from '../../Entities/Models/NeedList';

const http = new Http();

export class NeedListActions {

    static async getLoanApplicationDetail(loanApplicationId: string) {
        try {
            let result : AxiosResponse<LoanApplication> = await http.get<LoanApplication>(Endpoints.NeedListManager.GET.loan.info(loanApplicationId));
            
            return new LoanApplication().fromJson(result.data);
        } catch (error) {
            console.log(error);
        }
    }

    static async getNeedList(loanApplicationId: string, tenantId: string, status: boolean){
        let url = Endpoints.NeedListManager.GET.documents.submitted(loanApplicationId, tenantId, status);
        try {
            let res : AxiosResponse<NeedList> = await http.get<NeedList>(url);
            console.log('getNeedList',res);
            return res.data;
        } catch (error) {
            console.log(error);
        }
    }

    static async deleteNeedListDocument(id: string, requestId: string, docId: string, tenantId: number){
        let url = Endpoints.NeedListManager.PUT.documents.deleteDoc();
        try {
            let res = await http.put(url, {
                id, requestId, docId, tenantId
            })
            console.log('res deleteDocument', res)
            return res.status;
        } catch (error) {
            console.log(error);
        }
    }
}