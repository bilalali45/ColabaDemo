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
  static async getDocumentsFromSelectedTemplates(ids: string[]) {
    let url = Endpoints.NewNeedList.POST.getByTemplateIds();
    try {
      let res = await http.post(url, {
        id: ids,
      });
      console.log("res", res);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  static async getDraft(loanApplicationId: string) {
    let url = Endpoints.NewNeedList.GET.getDraft(loanApplicationId);
    try {
      let res = await http.get(url);
      console.log("res", res);
      return res.data;
    } catch (error) {
      console.log(error);
    }



    static async saveNeedList(
        loanApplicationId: string,
        tenantId: string,
        isDraft: boolean,
        emailText: string,
        documents: any[]) {
        let url = Endpoints.NewNeedList.POST.save(isDraft);

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
                            requestId: d.requestId
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
