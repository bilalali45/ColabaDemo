import PSPDFKit from "pspdfkit";
import { FileUpload } from "./helpers/FileUpload";
import { Viewer } from "./Viewer";

export class PDFActions extends Viewer {



    static async createPDFforDownload() {
        try {
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
        } catch (error) {
            return false;
        }

    }


    static async createPDFWithoutAnnotations() {
        try {
            const pagesAnnotations = await Promise.all(
                Array.from({ length: Viewer.instance.totalPageCount }).map((_, pageIndex) =>
                    Viewer.instance.getAnnotations(pageIndex)
                )
            );
            await Promise.all(Array.from(pagesAnnotations).map(async (pageList) => {

                if (pageList.toJS().length) {

                    return Array.from(pageList.toArray()).map((annotation: any) => (
                        Viewer.instance.update(annotation.set("noView", true))
                    )

                    )
                }
                else
                    return Promise.resolve()

            }).flat())

            let res = await this.createPDFforDownload();


            await Promise.all(Array.from(pagesAnnotations).map(async (pageList) => {

                if (pageList.toJS().length) {
    
                    return Array.from(pageList.toArray()).map((annotation: any) => (
                        // Viewer.instance.update(annotation.set("noView", false))
                             Viewer.instance.updateAnnotation(annotation)
                    )
    
                    )
                }
                else
                    return Promise.resolve()
    
            }).flat())
            return res
        }
        catch (error) {
            console.log(error)
        }
    }


    static async createPDFFromInstance(name: string) {

        console.time("completeExport")

        const pagesAnnotations = await Promise.all(
            Array.from({ length: Viewer.instance.totalPageCount }).map((_, pageIndex) =>
                Viewer.instance.getAnnotations(pageIndex)
            )
        );

        await Promise.all(Array.from(pagesAnnotations).map(async (pageList) => {

            if (pageList.toJS().length) {

                return Array.from(pageList.toArray()).map((annotation: any) => (
                    Viewer.instance.update(annotation.set("noView", true))
                )

                )
            }
            else
                return Promise.resolve()

        }).flat())



        const buffer = await this.instance.exportPDF();
        const blob = new Blob([buffer], { type: "application/pdf" });
        let file = new File([blob], name, { lastModified: Date.now(), type: "application/pdf" });

        await Promise.all(Array.from(pagesAnnotations).map(async (pageList) => {

            if (pageList.toJS().length) {

                return Array.from(pageList.toArray()).map((annotation: any) => (
                    // Viewer.instance.update(annotation.set("noView", false))
                         Viewer.instance.updateAnnotation(annotation)
                )

                )
            }
            else
                return Promise.resolve()

        }).flat())
        

        console.timeEnd("completeExport")
        return file;


    }

    static async createNewFileFromThumbnail(pageIndex: number) {

        const pagesAnnotations = await Viewer.instance.getAnnotations(pageIndex)

        await Promise.all(Array.from(pagesAnnotations).map(async (pageList) => {

            if (pageList.toJS().length) {

                return Array.from(pageList.toArray()).map((annotation: any) => (
                    Viewer.instance.update(annotation.set("noView", true))
                )

                )
            }
            else
                return Promise.resolve()

        }).flat())
        let buffer: any;

        try {
            buffer = await this.instance.exportPDFWithOperations([
                {
                    type: "keepPages",
                    pageIndexes: [+pageIndex]
                }
            ], null);
        }
        catch (error) {
            console.log(error)
        }
        const blob = new Blob([buffer], { type: "application/pdf" });
        let file = new File([blob], FileUpload.getFileNameByDate(), { lastModified: Date.now(), type: "application/pdf" });


        await Promise.all(Array.from(pagesAnnotations).map(async (pageList) => {

            if (pageList.toJS().length) {

                return Array.from(pageList.toArray()).map((annotation: any) => (
                         Viewer.instance.updateAnnotation(annotation)
                )

                )
            }
            else
                return Promise.resolve()

        }).flat())
        return file;

    }


    static async printPDF() {

        const pagesAnnotations = await Promise.all(
            Array.from({ length: Viewer.instance.totalPageCount }).map((_, pageIndex) =>
                Viewer.instance.getAnnotations(pageIndex)
            )
        );

        await Promise.all(Array.from(pagesAnnotations).map(async (pageList) => {

            if (pageList.toJS().length) {

                return Array.from(pageList.toArray()).map((annotation: any) => (
                    Viewer.instance.update(annotation.set("noPrint", true))
                )

                )
            }
            else
                return Promise.resolve()

        }).flat())


        await Viewer.instance.print();
    }


}