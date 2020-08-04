import { Endpoints } from "../endpoints/Endpoints";
import { Http } from "rainsoft-js";
import { LocalDB } from "../../Utils/LocalDB";
import { DocumentRequest } from "../../Entities/Models/DocumentRequest";
import { TemplateDocument } from "../../Entities/Models/TemplateDocument";

const http = new Http();

type SaveAsTemnplateDocumentType = {
    typeId: string
} | {
    docName: string
}

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
        documents: any[]) {
        let url = Endpoints.NewNeedList.POST.save(false);

        let requestData = {
            tenantId: parseInt(tenantId),
            loanApplicationId: parseInt(loanApplicationId),
            requests: [
                {
                    message: emailText,
                    documents: documents.map(d => {
                        return {
                            typeId: d.typeId,
                            displayName: d.docName,
                            message: d.docMessage,
                            docId: d.docId,
                            requestId: null
                        }
                    })
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

    static async saveAsTemplate(tenantId: string, name: string, documents: TemplateDocument[]) {
        let url = Endpoints.NewNeedList.POST.saveAsTemplate();

        let templateData = {
            tenantId: parseInt(tenantId),
            name,
            documentTypes: documents.map((d: TemplateDocument) => {
                if(d.docId) {
                    return {
                        typeId: d.docId
                    }
                }
                return {
                    docName: d.docName
                }
            })
        }
        console.log(templateData);

        try {
            let res = await http.post(url, templateData);
            console.log(res.data);
            return res?.data;
        } catch (error) {
            
        }
    }


}