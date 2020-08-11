import { Endpoints } from "../endpoints/Endpoints";
import { Http } from "rainsoft-js";
import { LocalDB } from "../../Utils/LocalDB";
import { DocumentRequest } from "../../Entities/Models/DocumentRequest";
import { TemplateDocument } from "../../Entities/Models/TemplateDocument";
import { debug } from "console";
import { AxiosResponse } from "axios";

const http = new Http();

export type DocumentsWithTemplateDetails = {
    id: string,
    name: string,
    docs: TemplateDocument[]
}

export class NewNeedListActions {
    static async getDocumentsFromSelectedTemplates(ids: string[]) {
        let url = Endpoints.NewNeedList.POST.getByTemplateIds()
        try {
            let res: AxiosResponse<DocumentsWithTemplateDetails[]> = await http.post(url, {
                id: ids
            })
            return res.data;
        } catch (error) {
            console.log(error);
        }
    }

    static async getDraft(loanApplicationId: string) {
        let url = Endpoints.NewNeedList.GET.getDraft(loanApplicationId);
        try {
            let res = await http.get(url);
            return res.data;
        } catch (error) {
            console.log(error);
        }
    }



    static async saveNeedList(
        loanApplicationId: string,
        isDraft: boolean,
        emailText: string,
        documents: any[]) {
        let url = Endpoints.NewNeedList.POST.save(isDraft);

        let mappedDocs = documents.map(d => {
            return {
                typeId: d.typeId,
                displayName: d.docName,
                message: d.docMessage || '',
                docId: d?.docId,
                requestId: d?.requestId
            }
        });

        console.log(mappedDocs);
        let requestData = {
            loanApplicationId: parseInt(loanApplicationId),
            requests: [
                {
                    message: emailText,
                    documents: mappedDocs
                }
            ]
        }
        console.log(requestData);
        try {
            let res = await http.post(url, requestData);
            return res.data;
        } catch (error) {
            console.log(error);
        }
    }

    static async saveAsTemplate(name: string, documents: TemplateDocument[]) {
        let url = Endpoints.NewNeedList.POST.saveAsTemplate();

        let templateData = {
            name,
            documentTypes: documents.map((d: TemplateDocument) => {
                if (d.typeId) {
                    return {
                        typeId: d.typeId
                    }
                }
                return {
                    docName: d.docName
                }
            })
        }

        try {
            let res = await http.post(url, templateData);
            console.log(res.data);
            return res?.data;
        } catch (error) {

        }
    }
}