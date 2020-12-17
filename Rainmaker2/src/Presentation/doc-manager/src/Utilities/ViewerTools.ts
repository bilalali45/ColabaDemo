import PSPDFKit from "pspdfkit";
import DocumentActions from "../Store/actions/DocumentActions";
import { Endpoints } from "../Store/endpoints/Endpoints";
import { ViewerActionsType } from "../Store/reducers/ViewerReducer";
import { AnnotationActions } from "./AnnotationActions";
import { rotateLeftIcon, roatateRightIcon, flipIcon, saveIcon, downloadIcon, printIcon, saveIconDisabled } from "./CustomIcons";
import { PDFActions } from "./PDFActions";
import { Viewer } from "./Viewer";


const baseUrl = `${window.location.protocol}//${window.location.host}/DocManager/`;
const licenseKey = 'Xq2sbPLKcoMngmloCFRhq1HUgk0jQLLbOf6LosAo6oO8y2G9QoaX3w3aX0PWavM6WOVdQo49a7UbnVe1GG6vkS1oSYDJv4EsuCckA4sx6M1qwqn9NbaszHkR6dvE8F0UhxZUsvIIRKUQQ67XwqwCd5G5iBfiJG6NE6gZRu-zasYtvEoyQ1uufbWcWF6FoXV6P_1FOcHrXqToVEXUqYVdYoPtXT3o_gEhLIp3mkLmIWXA2sUuMYZKKAHie1Wqu1eD1mpL0EzxadBtTVAPjLL8xMgl3h0PRZppCtQswQVFCQQMYwMLmDXG7Mzc_v8SO7z_3-CpjubR71MiAaMiw-jRCS8NfVnpso5pCws5gB3uhgxb4x94ISus4h1I0kiN9n7rsihbeJwn16L0-wuxDhuRr-Yyhh2WYdcQz-BfX6XXTTEThzESMHyrWWSJ6KNSNJLq';

export class ViewerTools extends Viewer {

    static currentToolbar: Array<string> = [
        "pan",
        "annotate",
        "ink",
        "highlighter",
        "text-highlighter",
        "ink-eraser",
        "note",
        "text",
        "line",
        "arrow",
        "rectangle",
        "ellipse",
        "polygon",
        "polyline",
        "spacer"
    ];


    static createToolbarItem(type: string, id?: string, title?: string, icon?: string, onPress?: Function, className?: string) {
        return { type, id, title, icon, onPress, className }
    }

    static rotateLeft() {

        Viewer.instance?.setViewState((viewState: { rotateLeft: () => any }) => viewState.rotateLeft())
    }

    static rotateRight() {
        Viewer.instance?.setViewState((viewState: { rotateRight: () => any }) => viewState.rotateRight());
    }

    static async saveFileWithAnnotations(fileObj: any, file: File, isFileChanged: boolean, dispatch: Function, currentDoc: any) {
        return this.uploadFileWithoutAnnotations(fileObj, file, isFileChanged, dispatch, currentDoc)

    }
    static async uploadFileWithoutAnnotations(fileObj: any, file: File, isFileChanged: boolean, dispatch: Function, currentDoc: any) {
        let fileId = fileObj.fileId;
        // if(isFileChanged){
        if (fileObj.isFromCategory) {
            fileId = await DocumentActions.SaveCategoryDocument(fileObj, file, dispatch, currentDoc)
            await DocumentActions.getDocumentItems(dispatch)
        } else if (fileObj.isFromWorkbench) {
            fileId = await DocumentActions.SaveWorkbenchDocument(fileObj, file, dispatch, currentDoc)
            await DocumentActions.getWorkBenchItems(dispatch)
        } else if (fileObj.isFromTrash) {
            fileId = await DocumentActions.SaveTrashDocument(fileObj, file, dispatch, currentDoc)
            await DocumentActions.getTrashedDocuments(dispatch)
        }
        dispatch({ type: ViewerActionsType.SetIsLoading, payload: false })
        // }

        if (fileId) {
            await AnnotationActions.saveAnnotations(fileObj, fileId, false)
            dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: false })
            
            return fileId
        }

    }

    static async saveViewerFileWithAnnotations(fileObj: any, isFileChanged: boolean, dispatch: Function, currentDoc: any) {

        dispatch({
            type: ViewerActionsType.SetIsLoading,
            payload: true
        })
        let file = await PDFActions.createPDFFromInstance(fileObj.name);
        let res = await ViewerTools.saveFileWithAnnotations(fileObj, file, isFileChanged, dispatch, currentDoc)

        dispatch({
            type: ViewerActionsType.SetIsLoading,
            payload: false
        })
        dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: false });
        return res;
    }

    static downloadFile() {
        PDFActions.createPDFWithoutAnnotations();
    }



    static async generateToolBarData(fileObj: any, isFileChanged: boolean, dispatch: Function, currentDoc: any) {

        let toolbarItems = PSPDFKit.defaultToolbarItems;
        let saveButton: any = null;
        // enable only when changes found
        // if (isFileChanged) {
        //     saveButton = this.createToolbarItem('custom', 'save', 'Save', saveIcon, () => ViewerTools.saveViewerFileWithAnnotations(fileObj, isFileChanged, dispatch, currentDoc))
        // } else {
        //     saveButton = this.createToolbarItem('custom', 'save', 'Save', saveIconDisabled, () => {}, 'disabled-save-icon')
        // }
        saveButton = this.createToolbarItem('custom', 'save', 'Save', saveIcon, () => ViewerTools.saveViewerFileWithAnnotations(fileObj, isFileChanged, dispatch, currentDoc))
        const rotateLeftButton: any = this.createToolbarItem('custom', 'rotate-left', 'Rotate Left', rotateLeftIcon, this.rotateLeft);
        const rotateRightButton: any = this.createToolbarItem('custom', 'rotate-right', 'Rotate Right', roatateRightIcon, this.rotateRight);
        const flipButton: any = this.createToolbarItem('custom', 'flip', 'Flip', flipIcon);
        const downloadButton: any = this.createToolbarItem('custom', 'download', 'Download', downloadIcon, this.downloadFile);
        const printButton: any = this.createToolbarItem('custom', 'print', 'Print', printIcon, PDFActions.printPDF);

        let customizedToolBarItems: any = [rotateLeftButton, rotateRightButton, downloadButton, printButton];

        this.currentToolbar.forEach((toolbaritem) => {
            customizedToolBarItems.push(
                toolbarItems.filter((el) => el.type === toolbaritem)[0]
            );
        });
        customizedToolBarItems.push(saveButton)
        this.instance?.setToolbarItems(customizedToolBarItems);
    };

    static zoomIn() {
        this.instance.setViewState(viewState => viewState.zoomIn())

    }

    static zoomOut() {
        this.instance.setViewState(viewState => viewState.zoomOut())

    }

    static fitToScreen() {
        this.instance.setViewState((viewState: any) => viewState.set("zoom", "FIT_TO_VIEWPORT"));
    }

    static async convertImageToPDF(src: any) {



        let el = document.createElement('div');
        el.id = "viewer-container";
        el.style.height = '800px';
        el.style.width = '400px';
        el.style.display = 'none';
        document.body.appendChild(el);

        try {
            let localInstance = await PSPDFKit.load({
                document: src,
                container: '#viewer-container',
                licenseKey: licenseKey,
                baseUrl: baseUrl,
            });

            const buffer = await localInstance.exportPDF();
            const blob = new Blob([buffer], { type: "arraybuffer" });
            let file = new File([blob], "file_name", { lastModified: Date.now() });
            document.body.removeChild(el);

            await PSPDFKit.unload(localInstance)
            return file;
        } catch (error) {
            console.log(error);

        }

        return null;
    }

}

