import { Endpoints } from '../endpoints/Endpoints';
import axios from 'axios';
import { LocalDB } from '../../Utils/LocalDB';

// const http = new Http();

// http.setBaseUrl('https://alphatx.rainsoftfn.com/');

export class TemplateActions {
    static async fetchTemplates(tenantId: string) {
        let url = `https://alphamaingateway.rainsoftfn.com${Endpoints.TemplateManager.GET.templates(tenantId)}`;
        try {
            let res = await axios.get(url, {
                headers: {
                    Authorization: `Bearer ${LocalDB.getAuthToken()}`
                }
            });
            console.log(res);
            return res.data;
        } catch (error) {
            console.log(error);
        }
    }

    static async fetchCategoryDocuments() {
        let url = `https://alphamaingateway.rainsoftfn.com${Endpoints.TemplateManager.GET.categoryDocuments()}`;

        try {
           
        } catch (error) {
            console.log(error);

        }
    }

    static async fetchTemplateDocuments(id: string) {
        let url = `https://alphamaingateway.rainsoftfn.com${Endpoints.TemplateManager.GET.templateDocuments(id)}`;

        try {
           
        } catch (error) {
            console.log(error);
        }
    }

    static async insertTemplate() {
        try {
            
        } catch (error) {
            console.log(error)
        }
    }
    
    static async renameTemplate() {
        try {
            
        } catch (error) {
            console.log(error)
        }
    }
   
    static async deleteTemplate() {
        try {
            
        } catch (error) {
            console.log(error)
        }
    }
}