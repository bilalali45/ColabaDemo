import { Http } from "rainsoft-js";
import MaritalStatusEndpoints from '../endpoints/MaritalStatusEndpoints'
import { AxiosResponse } from 'axios';
import { APIResponse } from "../../Entities/Models/APIResponse";


export default class MaritalStatusActions {
    static async getAllMaritalStatuses() {
        let url = MaritalStatusEndpoints.GET.getAllMaritalstatus()
        try {
            let response: any = await Http.get(url, {}, false);
            return response.data;
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }
    static async getMaritalStatus(loanapplicationId: number, borrowerId: number ) {
        let url = MaritalStatusEndpoints.GET.getMaritalStatus(loanapplicationId, borrowerId);
        try {
            let response: any = await Http.get(url, {
            }, false);
            return response.data;
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }
    static async addOrUpdateMaritalStatus(payload: MaritalStatusActions) {
        let url = MaritalStatusEndpoints.POST.addOrUpdateMaritalStatus();
        try {
            let res: AxiosResponse = await Http.post(
                url,
                payload, {}, false
            );
            return new APIResponse(res.status, "");
        } catch (error) {
            console.log(error);
            return error;
        }

    }

    static async isRelationAlreadyMapped(loanapplicationId: number,  borrowerId: number) {
        let url = MaritalStatusEndpoints.GET.isRelationAlreadyMapped(loanapplicationId, borrowerId);
        try {
            let response: any = await Http.get(url, {}, false);
            return response.data;
        } catch (error) {
            console.log(error);
            return error;
        }

    }

    /*static async addOrUpdateSecondaryBorrowerMaritalStatus(payload: SecondaryAddOrUpdateMaritalStatusPayload) {
        /* let url = MaritalStatusEndpoints.POST.addOrUpdateSecondaryBorrowerMaritalStatus();
        try {
            let res: AxiosResponse = await Http.post(
                url,
                payload, {}, false
            );
            return new APIResponse(res.status, "");
        } catch (error) {
            console.log(error);
            return error;
        } 

    }*/
}