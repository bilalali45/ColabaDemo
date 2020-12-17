import DocumentActions from "../Store/actions/DocumentActions";
import { ViewerActions } from "../Store/actions/ViewerActions";
import { ViewerActionsType } from "../Store/reducers/ViewerReducer";
import { FileUpload } from "./helpers/FileUpload";
import { PDFActions } from "./PDFActions";
import { Viewer } from "./Viewer";
import { ViewerTools } from "./ViewerTools";

export class PDFThumbnails extends Viewer {
    static async generateThumbnailData(index: number) {

        if (this.instance) {

            const src = await this.instance?.renderPageAsImageURL({ width: 400 }, index);
            return src;
        };
        return '';
    }

    static goToPage(page: number) {
        this.instance?.setViewState(state => state.set("currentPageIndex", page));
    }

    static getFileDataToView = async (file: any) => {
        let buffer = await DocumentActions.getFileToView(
          file.id,
          file.fromRequestId,
          file.fromDocId,
          file.fromFileId
        );
    
        return buffer;
      };
  
      
    static async addAPage(fileData: any, index: number, dispatch: React.Dispatch<any>) {
        let file: any = null;

        
        if (!fileData.fileName?.includes('.pdf')) {
            let buffer = await PDFThumbnails.getFileDataToView(fileData);
            file = await ViewerTools.convertImageToPDF(buffer);
        } else {
            let blob = await PDFThumbnails.getFileDataToView(fileData);
            file = new Blob([blob], { type: "application/pdf" });
        }
        
        try{
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
        }catch(error){
            return false;
        }
        
    }

    static async removePages(pages: number[]) {

        try{
        await this.instance.applyOperations([
            {
                type: "removePages",
                pageIndexes: pages,
            }
        ], null);
        return true;
        }catch(error){
            return false;
        }
        // await generateThumbnails()
    }


    static async movePages(pageIndex: number, moveIndex: number) {
        if (pageIndex < moveIndex) {
            let cachedIndex = pageIndex;
            pageIndex = moveIndex;
            moveIndex = cachedIndex;
        }
        try{
        await this.instance.applyOperations([
            {
                type: "movePages",
                pageIndexes: [pageIndex], // Move pages 0 and 4.
                beforePageIndex: moveIndex // The specified pages will be moved after page 7.
            }
        ], null);
        return true;
        }catch(error){
            return false
        }
    }

    static async createNewFileFromThumbnail(pageIndex: number) {

        const buffer = await this.instance.exportPDFWithOperations([
            {
                type: "keepPages",
                pageIndexes: [+pageIndex]
            }   
        ], null);
        const blob = new Blob([buffer], { type: "application/pdf" });
        let file = new File([blob], FileUpload.getFileNameByDate(), { lastModified: Date.now(),  type:  "application/pdf" });
        return file;
    }

}