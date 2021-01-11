import PSPDFKit from "pspdfkit";
import { FileUpload } from "./helpers/FileUpload";
import { Rename } from "./helpers/Rename";
import { Viewer } from "./Viewer";

export class PDFActions extends Viewer {



    static async createPDFforDownload(filename: any) {
        try {
            const buffer = await this.instance.exportPDF();
            const blob = new Blob([buffer], { type: "application/pdf" });
            if (navigator.msSaveOrOpenBlob) {
                navigator.msSaveOrOpenBlob(blob, filename);
            } else {
                let a: any = document.createElement("a");
                const objectUrl = window.URL.createObjectURL(blob);
                a.href = objectUrl;
                a.style = "display: none";
                a.download = filename;
                document.body.appendChild(a);
                a.click();
                window.URL.revokeObjectURL(objectUrl);
            }
            return true
        } catch (error) {
            return false;
        }

    }


    static async createPDFWithoutAnnotations(fileName: any) {
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

            let res = await this.createPDFforDownload(fileName);


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
        let newName = `${Rename.removeExt(name)}.pdf`;
        let file = new File([blob], newName, { lastModified: Date.now(), type: "application/pdf" });

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

    static async createNewFileFromThumbnail(pageIndexes: any[], currentFile: any, files: any[]) {

        const pagesAnnotations = await Promise.all(
            Array.from({ length: Viewer.instance.totalPageCount }).map((_, pageIndex) =>
                Viewer.instance.getAnnotations(pageIndex)
            )
        );

        await Promise.all(Array.from(pagesAnnotations).map(async (pageList) => {

            if (pageList.toJS().length) {

                return Array.from(pageList.toArray()).map((annotation: any) => (
                    Viewer.instance.update(annotation.set("noView", true))
                ))
            }
            else
                return Promise.resolve()

        }).flat())
        let buffer: any;

        try {
            buffer = await this.instance.exportPDFWithOperations([
                {
                    type: "keepPages",
                    pageIndexes: pageIndexes.map(pi => Number(pi))
                }
            ], null);
        }
        catch (error) {
            console.log(error)
        }
        const blob = new Blob([buffer], { type: "application/pdf" });
        let newName = currentFile.name;

        if (files?.length) {
            currentFile.clientName = newName;
            let file = new File([blob], `${newName}`, { lastModified: Date.now(), type: "application/pdf" });
            currentFile.file = file;
            newName = Rename.rename(files, currentFile).clientName;
        }
        let file = new File([blob], `${newName}`, { lastModified: Date.now(), type: "application/pdf" });

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