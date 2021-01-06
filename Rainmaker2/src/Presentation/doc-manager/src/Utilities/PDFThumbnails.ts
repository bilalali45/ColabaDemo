import DocumentActions from "../Store/actions/DocumentActions";
import { ViewerActions } from "../Store/actions/ViewerActions";
import { ViewerActionsType } from "../Store/reducers/ViewerReducer";
import { FileUpload } from "./helpers/FileUpload";
import { PDFActions } from "./PDFActions";
import { Viewer } from "./Viewer";
import { ViewerTools } from "./ViewerTools";

export class PDFThumbnails extends Viewer {
    static async generateThumbnailData(index: number) {

        try {
            if (this.instance) {

                const src = await this.instance?.renderPageAsImageURL({ width: 400 }, index);
                return src;
            };
            return '';
        }
        catch (error) {
            console.log(error)
        }
        return '';
    }

    static goToPage(page: number) {
        this.instance?.setViewState(state => state.set("currentPageIndex", page));
    }

    static getFileDataToView = async (file: any, dispatch:Function) => {
        let buffer = await DocumentActions.getFileToView(
            file.id,
            file.fromRequestId,
            file.fromDocId,
            file.fromFileId,
            file.isFromCategory, 
            file.isFromWorkbench, 
            false,
            dispatch

        );

        return buffer;
    };


    static async addAPage(fileData: any, index: number, dispatch: React.Dispatch<any>) {
        let file: any = null;


        // if (!fileData.fileName?.includes('.pdf')) {
        //     let buffer = await PDFThumbnails.getFileDataToView(fileData);
        //     file = await ViewerTools.convertImageToPDF(buffer, true, fileData );
        // } else {
        //     let blob = await PDFThumbnails.getFileDataToView(fileData);
        //     file = new Blob([blob], { type: "application/pdf" });
        // }

        let isPDF:boolean = fileData.fileName?.includes('.pdf')
        let buffer = await PDFThumbnails.getFileDataToView(fileData, dispatch);
        file = await ViewerTools.convertImageToPDF(buffer, true, fileData, isPDF );

        try {

            let importDocumentOp = {
                type: "importDocument",
                afterPageIndex: index,
                treatImportedDocumentAsOnePage: false,
                document: file,
            };

            let operations: any = [importDocumentOp];


            await this.instance.applyOperations(operations, null);

            dispatch({
                type: ViewerActionsType.SetInstance,
                payload: this.instance
            });
            return true;
        } catch (error) {
            return false;
        }

    }

    static async removePages(pages: number[]) {

        try {
            await this.instance.applyOperations([
                {
                    type: "removePages",
                    pageIndexes: pages,
                }
            ], null);
            return true;
        } catch (error) {
            return false;
        }
        // await generateThumbnails()
    }


    static async movePages(pageIndexes: number[], moveIndex: number) {
        try {
            await this.instance.applyOperations([
                {
                    type: "movePages",
                    pageIndexes: pageIndexes, // Move pages 0 and 4.
                    beforePageIndex: moveIndex // The specified pages will be moved after page 7.
                }
            ], null);
            return true;
        } catch (error) {
            return false
        }
    }



}