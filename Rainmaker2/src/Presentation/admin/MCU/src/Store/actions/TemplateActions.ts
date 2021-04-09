import {Endpoints} from '../endpoints/Endpoints';
import axios, {AxiosResponse} from 'axios';
import {LocalDB} from '../../Utils/LocalDB';
import {Http} from 'rainsoft-js';
import {Template} from '../../Entities/Models/Template';
import {CategoryDocument} from '../../Entities/Models/CategoryDocument';

let fetchTemplateDocumentCancelToken = axios.CancelToken.source();

export class TemplateActions {
  static async fetchTemplates() {
    let url = Endpoints.TemplateManager.GET.templates();
    try {
      let res: AxiosResponse<Template[]> = await Http.get<Template[]>(url);
      return res;
    } catch (error) {
      console.log(error);
      return error
    }
  }

  static async fetchCategoryDocuments() {
    let url = Endpoints.TemplateManager.GET.categoryDocuments();

    try {
      let res: AxiosResponse<CategoryDocument[]> = await Http.get<
        CategoryDocument[]
      >(url);
      return res;
    } catch (error) {
      console.log(error);
      return error
    }
  }

  static async fetchTemplateDocuments(id: string) {
    fetchTemplateDocumentCancelToken.cancel();
    fetchTemplateDocumentCancelToken = axios.CancelToken.source();
    let url = Endpoints.TemplateManager.GET.templateDocuments(id);

    try {
      let res = await Http.fetch(
        {
          url: Http.createUrl(Http.baseUrl, url),
          cancelToken: fetchTemplateDocumentCancelToken.token
        },
        {
          Authorization: `Bearer ${LocalDB.getAuthToken()}`
        }
      );
      return res;
    } catch (error) {
      console.log(error);
      return error
    }
  }

  static async fetchEmailTemplate() {
    let url = Endpoints.TemplateManager.GET.getEmailTemplate();
    try {
      let res = await Http.get(url);
      return res;
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async insertTemplate(name: string) {
    fetchTemplateDocumentCancelToken.cancel();
    let url = Endpoints.TemplateManager.POST.insertTemplate();
    let template = {
      name
    };
    try {
      let res = await Http.post(url, template);
      return res;
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async renameTemplate(templateId: string, name: string) {
    let url = Endpoints.TemplateManager.POST.renameTemplate();
    try {
      let res = await Http.post(url, {
        id: templateId,
        name
      });
      return res;
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async deleteTemplate(templateId: string) {
    let url = Endpoints.TemplateManager.DELETE.template();
    try {
      let res: any = await Http.fetch(
        {
          url: Http.createUrl(Http.baseUrl, url),
          method: 'DELETE',
          data: {
            templateId
          }
        },
        {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${LocalDB.getAuthToken()}`
        }
      );
      return res;
    } catch (error) {
      console.log(error);
      return error;
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
      [type]: docTypeOrName
    };
    try {
      let res = await Http.post(url, document);
      return res;
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async deleteTemplateDocument(templateId: string, documentId: string) {
    let url = Endpoints.TemplateManager.DELETE.deleteTemplateDocument();
    try {
      let res: any = await Http.fetch(
        {
          url: Http.createUrl(Http.baseUrl, url),
          method: 'DELETE',
          data: {
            id: templateId,
            documentId
          }
        },
        {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${LocalDB.getAuthToken()}`
        }
      );
      return res;
    } catch (error) {
      console.log(error);
      return error;
    }
  }

  static async isDocumentDraft(loanApplicationId: string) {
    let url = Endpoints.DocumentManager.GET.documents.isDocumentDraft(
      loanApplicationId
    );
    try {
      let res: any = await Http.get(url);
      return {
        requestId: res?.data?.requestId
      };
    } catch (error) {
      console.log(error);
      return error;
    }
  }
}
