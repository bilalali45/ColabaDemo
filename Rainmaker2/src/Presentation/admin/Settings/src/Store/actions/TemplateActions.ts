import {Endpoints} from '../endpoints/Endpoints';
import axios, {AxiosResponse} from 'axios';
import {LocalDB} from '../../Utils/LocalDB';
import {Http} from 'rainsoft-js';
import {Template} from '../../Entities/Models/Template';
import {CategoryDocument} from '../../Entities/Models/CategoryDocument';
import {debug} from 'console';
import { Role } from '../Navigation';
import { TenantTemplate } from '../../Components/ManageDocumentTemplate/ManageDocumentTemplate';

let fetchTemplateDocumentCancelToken = axios.CancelToken.source();

export class TemplateActions {

  static async fetchTemplates() {
    let url = Endpoints.TemplateManager.GET.templates();
    try {
      let res: AxiosResponse<Template[]> = await Http.get<Template[]>(url);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

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

  static async fetchEmailTemplate() {
    let url = Endpoints.TemplateManager.GET.getEmailTemplate();
    try {
      let res = await Http.get(url);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  static async insertTemplate(name: string, isMcuTemplate: boolean) {
    const role = LocalDB.getUserRole();
    let url: string = '';
    fetchTemplateDocumentCancelToken.cancel();

    if(role === Role.MCU_ROLE){
      url = Endpoints.TemplateManager.POST.insertTemplate();
    }else if(role === Role.ADMIN_ROLE && !isMcuTemplate){
      url = Endpoints.TemplateManager.POST.insertTenantTemplate();
    }else{
      url = Endpoints.TemplateManager.POST.insertTemplate();
    }
    
    let template = {name};
    try {
      let res = await Http.post(url, template);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  static async renameTemplate(template: Template) {
    let templateId = template.id;
    let name = template.name;
    const role = LocalDB.getUserRole();
    let url: string = '';

    if(role === Role.MCU_ROLE){
      url = Endpoints.TemplateManager.POST.renameTemplate();
    }else if(role === Role.ADMIN_ROLE && template.type === TenantTemplate){
      url = Endpoints.TemplateManager.POST.renameTenantTemplate();
    }else{
      url = Endpoints.TemplateManager.POST.renameTemplate();
    }

    try {
      let res = await Http.post(url, {
        id: templateId,
        name
      });
      return true;
    } catch (error) {
      console.log(error);
    }
  }

  static async deleteTemplate(template: Template) {
    let templateId = template.id;
    const role = LocalDB.getUserRole();
    let url: string = '';

    if(role === Role.MCU_ROLE){
      url = Endpoints.TemplateManager.DELETE.template();
    }else if(role === Role.ADMIN_ROLE && template.type === TenantTemplate){
      url = Endpoints.TemplateManager.DELETE.tenantTemplate();
    }else{
      url = Endpoints.TemplateManager.DELETE.template();
    }

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
      return true;
    } catch (error) {
      console.log(error);
    }
  }

  static async addDocument(
    templateId: string,
    docTypeOrName: string,
    type: string,
    templateType: string
  ) {
    const role = LocalDB.getUserRole();
    let url: string = '';

    if(role === Role.MCU_ROLE){
      url = Endpoints.TemplateManager.POST.addDocument();
    }else if(role === Role.ADMIN_ROLE && templateType === TenantTemplate){
      url = Endpoints.TemplateManager.POST.addTenantDocument();
    }else{
      url = Endpoints.TemplateManager.POST.addDocument();
    }
   
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

  static async deleteTemplateDocument(templateId: string, documentId: string, type: string) {
    const role = LocalDB.getUserRole();
    let url: string = '';

    if(role === Role.MCU_ROLE){
      url = Endpoints.TemplateManager.DELETE.deleteTemplateDocument();
    }else if(role === Role.ADMIN_ROLE && type === TenantTemplate){
      url = Endpoints.TemplateManager.DELETE.deleteTenantTemplateDocument();
    }else{
      url = Endpoints.TemplateManager.DELETE.deleteTemplateDocument();
    }
     

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
    }
  }
}
