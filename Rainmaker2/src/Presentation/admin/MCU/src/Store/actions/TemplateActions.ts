import { Endpoints } from '../endpoints/Endpoints';
import axios from 'axios';
import { LocalDB } from '../../Utils/LocalDB';
import { Http } from 'rainsoft-js';

const http = new Http();

export class TemplateActions {
    static async fetchTemplates(tenantId: string) {
        let url = Endpoints.TemplateManager.GET.templates(tenantId);
        try {
            let res = await http.get(url);
            console.log(res);
            return res.data;
        } catch (error) {
            console.log(error);
        }
    }

    static async fetchCategoryDocuments() {
        let url = Endpoints.TemplateManager.GET.categoryDocuments();

        try {
            let res = await http.get(url);
            return res.data;
        } catch (error) {
            console.log(error);

        }
    }

    static async fetchTemplateDocuments(id: string) {
        let url = Endpoints.TemplateManager.GET.templateDocuments(id);

        try {
            let res = await http.get(url);
            console.log(res);
            return res.data;
        } catch (error) {
            console.log(error);
        }
    }

    static async insertTemplate(tenantId: string, name: string) {
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

    static async renameTemplate(tenantId: string, templateId: string, name: string) {
        let url = Endpoints.TemplateManager.POST.renameTemplate();
        try {
            let res = await http.post(url, {
                tenantId, templateId, name
            });
            return res.data;
        } catch (error) {
            console.log(error)
        }
    }

    static async deleteTemplate(tenantId: string, templateId: string) {

        let url = Endpoints.TemplateManager.DELETE.templateById(tenantId, templateId);
        try {
            let res = await http.delete(url);
            return res.data;
        } catch (error) {
            console.log(error)
        }
    }

    
    static async addDocument(tenantId: string, templateId: string, docTypeOrName: string, type: string) {

        let url = Endpoints.TemplateManager.POST.addDocument();
        let document = {
            templateId, tenantId: Number(tenantId), [type]: docTypeOrName
        }
        // console.log(document);
        try {
            let res = await http.post(url, document);
            return null;
        } catch (error) {
            console.log(error)
        }
    }

    static async deleteTemplateDocument(tenantId: string, templateId: string, documentId: string) {

        let url = Endpoints.TemplateManager.DELETE.deleteTemplateDocument(tenantId, templateId, documentId);
        try {
            let res = await http.delete(url);
            return res.data;
        } catch (error) {
            console.log(error)
        }
    }
}