import PSPDFKit from "pspdfkit";
import { FileUpload } from "./helpers/FileUpload";
import { Viewer } from "./Viewer";

export class PDFActions extends Viewer {
    


    static async createPDFforDownload() {
        try{
            const buffer = await this.instance.exportPDF();
            const blob = new Blob([buffer], { type: "application/pdf" });
            if (navigator.msSaveOrOpenBlob) {
                navigator.msSaveOrOpenBlob(blob, "download.pdf");
            } else {
                let a: any = document.createElement("a");
                const objectUrl = window.URL.createObjectURL(blob);
                a.href = objectUrl;
                a.style = "display: none";
                a.download = "download.pdf";
                document.body.appendChild(a);
                a.click();
                window.URL.revokeObjectURL(objectUrl);
            }
            return true
        } catch (error){
            return false;
        }
        
    }

    static async createPDFWithAnnotations() {
        this.createPDFforDownload();
    }
    static async createPDFWithoutAnnotations() {

        let annotations:any  = []
        for (let i = 0; i < Viewer.instance.totalPageCount; i++) {
            annotations.push( await this.instance.getAnnotations(i));
            for (const annotation of annotations[i]) {
                await this.instance.updateAnnotation(annotation.set("noView", true));
            }
            
        }
        

        let res = await this.createPDFforDownload();


        for (let i = 0; i < Viewer.instance.totalPageCount; i++) {
            for (const annotation of annotations[i]) {
                await this.instance.updateAnnotation(annotation);
            }
         }
         return res
    }

    static async removeAndReattachAnnotations(cb:any) {

        let annotations:any  = []
        for (let i = 0; i < Viewer.instance.totalPageCount; i++) {
            annotations.push( await this.instance.getAnnotations(i));
            for (const annotation of annotations[i]) {
                await this.instance.updateAnnotation(annotation.set("noView", true));
            }
            
        }
        
            await cb()
        


        for (let i = 0; i < Viewer.instance.totalPageCount; i++) {
            for (const annotation of annotations[i]) {
                await this.instance.updateAnnotation(annotation);
            }
         }
         
    }

    static async createPDFFromInstance(name:string) {
        const buffer = await this.instance.exportPDF();
        const blob = new Blob([buffer], { type: "application/pdf" });
        let file = new File([blob], name, { lastModified: Date.now(), type:  "application/pdf"  });
        return file; 

    }

    static async printPDF(){

        let annotations:any  = []
        for (let i = 0; i < Viewer.instance.totalPageCount; i++) {
            annotations.push( await Viewer.instance.getAnnotations(i));
            for (const annotation of annotations[i]) {
                await Viewer.instance.updateAnnotation(annotation.set("noView", true));
            }
            
        }
        

        await Viewer.instance.print();


        for (let i = 0; i < Viewer.instance.totalPageCount; i++) {
            for (const annotation of annotations[i]) {
                await Viewer.instance.updateAnnotation(annotation);
            }
         }
    }


}