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