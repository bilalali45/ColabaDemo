import {Endpoints} from '../endpoints/Endpoints';
import axios, {AxiosResponse} from 'axios';
import {Http} from 'rainsoft-js';
import {debug} from 'console';
import { LocalDB } from '../../Utilities/LocalDB';
import { CategoryDocument } from '../../Models/CategoryDocument';

let fetchTemplateDocumentCancelToken = axios.CancelToken.source();

export class TemplateActions {
//   static async fetchTemplates() {
//     let url = Endpoints.TemplateManager.GET.templates();
//     try {
//       let res: AxiosResponse<Template[]> = await Http.get<Template[]>(url);
//       return res.data;
//     } catch (error) {
//       console.log(error);
//     }
//   }

  static async fetchCategoryDocuments() {
    let url = Endpoints.TemplateManager.GET.categoryDocuments();

    try {
      let res: AxiosResponse<CategoryDocument[]> = await Http.get<
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
      let res = await Http.fetch(
        {
          url: Http.createUrl(Http.baseUrl, url),
          cancelToken: fetchTemplateDocumentCancelToken.token
        },
        {
          Authorization: `Bearer ${LocalDB.getAuthToken()}`
        }
      );
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

//   static async fetchEmailTemplate() {
//     let url = Endpoints.TemplateManager.GET.getEmailTemplate();
//     try {
//       let res = await Http.get(url);
//       return res.data;
//     } catch (error) {
//       console.log(error);
//     }
//   }

//   static async insertTemplate(name: string) {
//     fetchTemplateDocumentCancelToken.cancel();
//     let url = Endpoints.TemplateManager.POST.insertTemplate();
//     let template = {
//       name
//     };
//     try {
//       let res = await Http.post(url, template);
//       return res.data;
//     } catch (error) {
//       console.log(error);
//     }
//   }

//   static async renameTemplate(templateId: string, name: string) {
//     let url = Endpoints.TemplateManager.POST.renameTemplate();
//     try {
//       let res = await Http.post(url, {
//         id: templateId,
//         name
//       });
//       return true;
//     } catch (error) {
//       console.log(error);
//     }
//   }

//   static async deleteTemplate(templateId: string) {
//     let url = Endpoints.TemplateManager.DELETE.template();
//     try {
//       let res: any = await Http.fetch(
//         {
//           url: Http.createUrl(Http.baseUrl, url),
//           method: 'DELETE',
//           data: {
//             templateId
//           }
//         },
//         {
//           'Content-Type': 'application/json',
//           Authorization: `Bearer ${LocalDB.getAuthToken()}`
//         }
//       );
//       return true;
//     } catch (error) {
//       console.log(error);
//     }
//   }

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
      return true;
    } catch (error) {
      console.log(error);
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
      return res?.status;
    } catch (error) {
      console.log(error);
    }
  }

//   static async isDocumentDraft(loanApplicationId: string) {
//     let url = Endpoints.DocumentManager.GET.documents.isDocumentDraft(
//       loanApplicationId
//     );
//     try {
//       let res: any = await Http.get(url);
//       return {
//         requestId: res?.data?.requestId
//       };
//     } catch (error) {
//       console.log(error);
//     }
//   }
}
