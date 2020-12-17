import axios from 'axios';
import PSPDFKit from 'pspdfkit';
import Instance from 'pspdfkit/dist/types/typescript/Instance';
import { Viewer } from './Viewer';
import { Endpoints } from '../Store/endpoints/Endpoints';
import { Http } from 'rainsoft-js';
import { AxiosResponse } from 'axios';


export class AnnotationActions extends Viewer {


    static async getAnnotationForThePage(body: any) {
        let res : any = await Http.post(Endpoints.Document.POST.ViewCategoryAnnotations(), body);

        for (const annotation of res.data) {
            
            let n = this.instance.createAnnotation(PSPDFKit.Annotations.fromSerializableObject(annotation));
        }
    }

    static async fetchAnnotations(body: any, isWorkbenchFile:boolean) {
        let res:any;
        if(isWorkbenchFile){
            res= await Http.post(Endpoints.Document.POST.viewWorkbenchAnnotations(), body);
        }
        else{
            res= await Http.post(Endpoints.Document.POST.ViewCategoryAnnotations(), body);
        }
         
        if(!res.data.annotations) {
            return;
        }
        for (const annotation of res?.data.annotations) {            
            let n : any = this?.instance?.createAnnotation(PSPDFKit.Annotations.fromSerializableObject(annotation));
        }
    }

    static async saveAnnotations(AnnotationObj:any, fileId: string,  wasDragged: boolean, pageIndex?: number) {
        let body: any;
        let url:any;
        
        if(AnnotationObj.isFromWorkbench){
            let{ id } = AnnotationObj;
            url = Endpoints.Document.POST.saveWorkbenchAnnotations();
            body = {
                    id, fileId
            }
        }
        else if(AnnotationObj.isFromCategory){
            let{ id, requestId,docId } = AnnotationObj;
            url = Endpoints.Document.POST.saveCategoryAnnotations();
            body = {
                    id, requestId, docId, fileId
            }
            
        }
        else if(AnnotationObj.isFromTrash){
            let{ id } = AnnotationObj;
            console.log(AnnotationObj)
            url = Endpoints.Document.POST.saveTrashAnnotations();
            body = {
                id, fileId
            }
            
        }
        
        
        if(wasDragged && pageIndex) {

            let annotationsAsString = this.extractPageAnnotationsAsString(pageIndex);
            let data = {
                ...body,
                annotations: annotationsAsString
            }

            return this.saveAnnotationsLocal(url, data);

        }else {

            let annotations = await this.extractAnnotationsAsString();
            let data = {
                ...body,
                annotations
            }

            return this.saveAnnotationsLocal(url, data);
        }
    }

    static async extractAnnotationsAsString() {
        const allAnnotations = await this.instance.exportInstantJSON();
        const annotationsString = JSON.stringify(allAnnotations);
        return annotationsString;
    }

    static async extractPageAnnotationsAsString(pageIndex: number) {
        const annotations: any = await this.instance.getAnnotations(pageIndex);

        let annotationsToJSON = [];

        for (const annotation of annotations) {
            let json = PSPDFKit.Annotations.toSerializableObject(annotation);
            annotationsToJSON.push(json);
        }

        return JSON.stringify(annotations);
    }

    
    static async saveAnnotationsLocal(url: any, body: any) {
        try {
            let res: AxiosResponse = await Http.post(
                url,
                body
            );
            return res;
        } catch (error) {
            console.log(error);
        }
    }

    static async getAnnoations(document: any, file: any) {
        const { id, requestId, docId }: any = document;
        const { fileId, isWorkBenchFile }: any = file;
        let body = {
          id, fromRequestId: requestId, fromDocId: docId, fromFileId: fileId
        }
        await AnnotationActions.fetchAnnotations(body, isWorkBenchFile)
    
      }

}