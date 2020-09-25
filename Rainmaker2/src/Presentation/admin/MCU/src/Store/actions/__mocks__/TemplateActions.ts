import axios, {AxiosResponse} from 'axios';
import {Http} from 'rainsoft-js';
import {debug} from 'console';

let fetchTemplateDocumentCancelToken = axios.CancelToken.source();

export class TemplateActions {
  static async fetchTemplates() {
    return Promise.resolve([]);
  }

  static async fetchCategoryDocuments() {}

  static async fetchTemplateDocuments(id: string) {}

  static async fetchEmailTemplate() {}

  static async insertTemplate(name: string) {}

  static async renameTemplate(templateId: string, name: string) {}

  static async deleteTemplate(templateId: string) {}

  static async addDocument(
    templateId: string,
    docTypeOrName: string,
    type: string
  ) {}

  static async deleteTemplateDocument(templateId: string, documentId: string) {}

  static async isDocumentDraft(loanApplicationId: string) {}
}
