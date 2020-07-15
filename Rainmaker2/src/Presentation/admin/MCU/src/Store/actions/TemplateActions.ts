import { Endpoints } from '../endpoints/Endpoints';
import axios, { AxiosResponse } from 'axios';
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

        let url = Endpoints.TemplateManager.DELETE.template();
        try {
            let res: any = http.fetch({
                url: http.createUrl(http.baseUrl, url),
                method: 'DELETE',
                data: {
                    tenantId: Number(tenantId), id: templateId
                }
            }, {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${http.auth}`,
            })
            return true;

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
            console.log(res);
            return true;
        } catch (error) {
            console.log(error)
        }
    }

    static async deleteTemplateDocument(tenantId: string, templateId: string, documentId: string) {

        let url = Endpoints.TemplateManager.DELETE.deleteTemplateDocument();
        try {

            let res: any = http.fetch({
                url: http.createUrl(http.baseUrl, url),
                method: 'DELETE',
                data: {
                    tenantId: Number(tenantId), id: templateId, documentId
                }
            }, {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${http.auth}`,
            })

            return true;
        } catch (error) {
            console.log(error)
        }
    }
}