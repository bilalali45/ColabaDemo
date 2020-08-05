import { Endpoints } from "../endpoints/Endpoints";
import axios, { AxiosResponse } from "axios";
import { LocalDB } from "../../Utils/LocalDB";
import { Http } from "rainsoft-js";
import { Template } from "../../Entities/Models/Template";
import { CategoryDocument } from "../../Entities/Models/CategoryDocument";
import { debug } from "console";

const http = new Http();

let fetchTemplateDocumentCancelToken = axios.CancelToken.source();

export class TemplateActions {
  static async fetchTemplates() {
    let url = Endpoints.TemplateManager.GET.templates();
    try {
      let res: AxiosResponse<Template[]> = await http.get<Template[]>(url);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  static async fetchCategoryDocuments() {
    let url = Endpoints.TemplateManager.GET.categoryDocuments();

    try {
      let res: AxiosResponse<CategoryDocument[]> = await http.get<
        CategoryDocument[]
      >(url);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  static async fetchTemplateDocuments(id: string) {
    fetchTemplateDocumentCancelToken.cancel();
    fetchTemplateDocumentCancelToken = axios.CancelToken.source();
    let url = Endpoints.TemplateManager.GET.templateDocuments(id);

    try {
      let res = await http.fetch(
        {
          url: http.createUrl(http.baseUrl, url),
          cancelToken: fetchTemplateDocumentCancelToken.token,
        },
        {
          Authorization: `Bearer ${LocalDB.getAuthToken()}`,
        }
      );
      return res.data;
    } catch (error) {
      console.log(error);
    }

    static async fetchEmailTemplate(tenantId: string){
        let url = Endpoints.TemplateManager.GET.getEmailTemplate(tenantId);
        try {
            let res = await http.get(url)
            return res.data;
        } catch (error) {
          console.log(error);
        }
    }

    static async insertTemplate(tenantId: string, name: string) {
        fetchTemplateDocumentCancelToken.cancel();
        let url = Endpoints.TemplateManager.POST.insertTemplate();
        let template = {
            tenantId: Number(tenantId),
            name
        }
        try {
            let res = await http.post(url, template);
            return res.data;
        } catch (error) {
            console.log(error)
        }
    }
  }

  static async renameTemplate(templateId: string, name: string) {
    let url = Endpoints.TemplateManager.POST.renameTemplate();
    try {
      let res = await http.post(url, {
        id: templateId,
        name,
      });
      return true;
    } catch (error) {
      console.log(error);
    }
  }

  static async deleteTemplate(templateId: string) {
    let url = Endpoints.TemplateManager.DELETE.template();
    try {
      let res: any = await http.fetch(
        {
          url: http.createUrl(http.baseUrl, url),
          method: "DELETE",
          data: {
            templateId,
          },
        },
        {
          "Content-Type": "application/json",
          Authorization: `Bearer ${http.auth}`,
        }
      );
      return true;
    } catch (error) {
      console.log(error);
    }
  }

  static async addDocument(
    templateId: string,
    docTypeOrName: string,
    type: string
  ) {
    let url = Endpoints.TemplateManager.POST.addDocument();
    let document = {
      templateId,
      [type]: docTypeOrName,
    };
    try {
      let res = await http.post(url, document);
      return true;
    } catch (error) {
      console.log(error);
    }
  }

  static async deleteTemplateDocument(templateId: string, documentId: string) {
    let url = Endpoints.TemplateManager.DELETE.deleteTemplateDocument();
    try {
      let res: any = await http.fetch(
        {
          url: http.createUrl(http.baseUrl, url),
          method: "DELETE",
          data: {
            id: templateId,
            documentId,
          },
        },
        {
          "Content-Type": "application/json",
          Authorization: `Bearer ${http.auth}`,
        }
      );
      return res?.status;
    } catch (error) {
      console.log(error);
    }

    static async isDocumentDraft(loanApplicationId: string){
     let url = Endpoints.DocumentManager.GET.documents.isDocumentDraft(loanApplicationId);
     try {
         let res : any = await http.get(url);
         return {
             requestId: res?.data?.requestId
         };
     } catch (error) {
        console.log(error);
     }
    }
  }
}
