import { Http } from "rainsoft-js";

import SubjectPropertyUseEndpoints from "../endpoints/SubjectPropertyUseEndpoints";
import {AddOrUpdatePropertyUsagePayload} from "../../Entities/Models/PropertSubjectUseEntities";


export default class SubjectPropertyUseActions {
    static async getAllPropertyUsages() {
        let url = SubjectPropertyUseEndpoints.GET.getAllpropertyUsages();
        try {
            let response: any = await Http.get(url, {}, false);
            return response.data;
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }
    static async getPropertyUsage(loanApplicatonId: number) {
        let url = SubjectPropertyUseEndpoints.GET.getPropertyUsage(loanApplicatonId);
        try {
            let response: any = await Http.get(url, {}, false);
            return response.data;
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }

    static async addOrUpdatePropertyUsage(addOrUpdatePropertyUsagePayload : AddOrUpdatePropertyUsagePayload) {
        let url = SubjectPropertyUseEndpoints.POST.addOrUpdatePropertyUsage();
        try {
            let response: any = await Http.post(url, addOrUpdatePropertyUsagePayload, {}, false);
            return response.data;
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }
};