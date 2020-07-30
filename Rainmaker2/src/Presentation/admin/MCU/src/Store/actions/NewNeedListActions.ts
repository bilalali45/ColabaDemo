import { Endpoints } from "../endpoints/Endpoints";
import { Http } from "rainsoft-js";
import { LocalDB } from "../../Utils/LocalDB";
import { DocumentRequest } from "../../Entities/Models/DocumentRequest";

const http = new Http();

export class NewNeedListActions {
    static async getDocumentsFromSelectedTemplates(ids: string[], tenantId: number = 1) {
        let url = Endpoints.NewNeedList.POST.getByTemplateIds()
        try {
            let res = await http.post(url, {
                id: ids, tenantId
            })
            console.log('res', res)
            return res.data;
        } catch (error) {
            console.log(error);
        }
    }

    static async getDraft(loanApplicationId: string, tenantId: string) {
        let url = Endpoints.NewNeedList.GET.getDraft(loanApplicationId, tenantId);
        try {
            let res = await http.get(url);
            console.log('res', res)
            return res.data;
        } catch (error) {
            console.log(error);
        }
    }



    static async saveNeedList(
        loanApplicationId: string,
        tenantId: string,
        isDraf: boolean,
        emailText: string,
        documents: DocumentRequest[]) {
        let url = Endpoints.NewNeedList.POST.save(false);

        let requestData = {
            tenantId,
            loanApplicationId,
            requests: [
                {
                    message: emailText,
                    documents
                }
            ]
        }

        try {
            let res = await http.post(url, requestData);
            console.log('res', res)
            return res.data;
        } catch (error) {
            console.log(error);
        }
    }


}