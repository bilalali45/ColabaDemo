import PSPDFKit from "pspdfkit";
import Instance from "pspdfkit/dist/types/typescript/Instance";
import { idText } from "typescript";
import { CurrentInView } from "../Models/CurrentInView";
import { SelectedFile } from "../Models/SelectedFile";
import DocumentActions from "../Store/actions/DocumentActions";
import { ViewerActionsType } from "../Store/reducers/ViewerReducer";
import { AnnotationActions } from "./AnnotationActions";
import { editIcon, saveIcon, downloadIcon, printIcon, trashIcon, panIcon, annotationsIcon } from "./CustomIcons";
import { PDFActions } from "./PDFActions";
import { Viewer } from "./Viewer";
import { Rename } from '../Utilities/helpers/Rename';
import { DocumentActionsType } from "../Store/reducers/documentsReducer";

const baseUrl = `${window.location.protocol}//${window.location.host}/DocManager/`;
const licenseKey = window?.envConfig?.PSPDFKIT_LICENCE;

export class ViewerTools extends Viewer {
    static regex = /[^a-zA-Z0-9- ]/g; // This regex will allow only alphanumeric values with - and spaces.
    static currentRotation: any = 90;
    static isDocChanged: boolean = false;
    static currentFileName: string = '';

    static currentToolbar: Array<string> = [
        "pan",
        "annotate",
        "text-highlighter",
        "highlighter",
        "ink",
        "ink-eraser",
        "note",
        "text",
        "arrow",
        "line",
        "rectangle",
        "ellipse",
        "polygon",
        "polyline",
        "spacer"
    ];


    static createToolbarItem(type: string, id?: string, title?: string, icon?: string, onPress?: Function, className?: string) {
        return { type, id, title, icon, onPress, className }
    }

    static async saveFileWithAnnotations(fileObj: any, file: File, isFileChanged: boolean, dispatch: Function, currentDoc: any, importedFileIds: any, pageIndexes?: [number]) {
        return this.uploadFileWithoutAnnotations(fileObj, file, isFileChanged, dispatch, currentDoc, importedFileIds, pageIndexes)

    }
    static async uploadFileWithoutAnnotations(fileObj: any, file: File, isFileChanged: boolean, dispatch: Function, currentDoc: any, importedFileIds: any, pageIndexes?: [number]) {
        let fileId = fileObj.fileId;

        if (fileObj.isFromCategory) {
            fileId = await DocumentActions.SaveCategoryDocument(fileObj, file, dispatch, currentDoc)

        } else if (fileObj.isFromWorkbench) {
            fileId = await DocumentActions.SaveWorkbenchDocument(fileObj, file, dispatch, currentDoc)

        } else if (fileObj.isFromTrash) {
            fileId = await DocumentActions.SaveTrashDocument(fileObj, file, dispatch, currentDoc)

        }
        ViewerTools.isDocChanged = false
        this.saveFileAnnotations(fileObj, fileId, dispatch, pageIndexes)
        // dispatch({ type: ViewerActionsType.SetIsLoading, payload: false })
        // }


        return fileId

    }

    static async saveFileAnnotations(fileObj: any, fileId: any, dispatch: any, pageIndexes: any) {
        if (fileId) {
            await AnnotationActions.saveAnnotations(fileObj, fileId, false, pageIndexes)
            dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: false })

            dispatch({ type: ViewerActionsType.SetSaveFile, payload: true })
            dispatch({
                type: ViewerActionsType.SetFileProgress,
                payload: 0,
            });
        }
    }

    static async saveViewerFileWithAnnotations(fileObj: any, isFileChanged: boolean, dispatch: Function, currentDoc: any, currentFile: any, importedFileIds: any, selectedFileData: any) {
        dispatch({
            type: ViewerActionsType.SetIsSaving,
            payload: true
        });
        dispatch({
            type: ViewerActionsType.SetFileProgress,
            payload: 0,
        });
        await dispatch({ type: ViewerActionsType.SetRenameEditMode, payload: false });
        if (currentFile && currentFile.name && !currentFile.name.includes('.pdf')) {
            ViewerTools.isDocChanged = true
        }
        if (!ViewerTools.isDocChanged) {
            this.saveFileAnnotations(fileObj, fileObj.fileId, dispatch, [])
            dispatch({
                type: ViewerActionsType.SetIsSaving,
                payload: false
            });
            await DocumentActions.getDocumentItems(dispatch, [])
            dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: false })
            return;
        }
        let fileName = this.setCurrentFileName(currentFile.name, currentDoc.files)
        let file = await PDFActions.createPDFFromInstance(fileName);

        selectedFileData.name = `${Rename.removeExt(fileName)}.pdf`
        selectedFileData = new SelectedFile(selectedFileData.id, selectedFileData.name, selectedFileData.fileId)
        await dispatch({ type: ViewerActionsType.SetSelectedFileData, payload: selectedFileData });
        let currFile = new CurrentInView(currentFile.id, currentFile.src, selectedFileData.name, currentFile.isWorkBenchFile, currentFile.fileId );
        await dispatch({ type: ViewerActionsType.SetCurrentFile, payload: currFile });
        let res = await ViewerTools.saveFileWithAnnotations(fileObj, file, isFileChanged, dispatch, currentDoc, importedFileIds)
        let id = currentFile.isWorkBenchFile ? currentFile.id : currentDoc.id
        let fileId = currentFile.fileId;

        dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: false });
        await DocumentActions.getDocumentItems(dispatch, [])

        return res;
    }

    static setCurrentFileName(fileName: string, files: any) {
        if (files && files.length) {
            let currentFileName = `${Rename.removeExt(ViewerTools.currentFileName)}.pdf`
            if (ViewerTools.currentFileName !== '' && !Rename.checkFileNameExist(currentFileName, files) && !ViewerTools.regex.test(ViewerTools.currentFileName)) {
                return currentFileName
            }
            return fileName
        }
        return fileName


    }
    static downloadFile(file: any) {
        PDFActions.createPDFWithoutAnnotations(file.name);
    }

    static rotateLeft(indexes: any[]) {
        Viewer.instance.applyOperations([
            {
                type: "rotatePages",
                pageIndexes: indexes.map(i => Number(i)),
                rotateBy: 270,
            }
        ], null);
    }

    static rotateRight(indexes: any[]) {
        Viewer.instance.applyOperations([
            {
                type: "rotatePages",
                pageIndexes: indexes.map(i => Number(i)),
                rotateBy: 90
            }
        ], null);
    }

    static async discardChanges(dispatch: Function, currentFile: any) {

        await dispatch({ type: ViewerActionsType.SetCurrentFile, payload: null });
        await dispatch({ type: ViewerActionsType.SetDiscardFile, payload: true });
        await dispatch({ type: ViewerActionsType.SetCurrentFile, payload: currentFile });


    }


    static async generateToolBarData(fileObj: any, isFileChanged: boolean, dispatch: Function, currentDoc: any, currentFile: any, importedFileIds: any, selectedFileData: any) {

        let toolbarItems = PSPDFKit.defaultToolbarItems;
        // let saveButton: any = null;
        // enable only when changes found
        // if (isFileChanged) {
        //     saveButton = this.createToolbarItem('custom', 'save', 'Save', saveIcon, () => ViewerTools.saveViewerFileWithAnnotations(fileObj, isFileChanged, dispatch, currentDoc))
        // } else {
        //     saveButton = this.createToolbarItem('custom', 'save', 'Save', saveIconDisabled, () => {}, 'disabled-save-icon')
        // }
        const saveButton = this.createToolbarItem('custom', 'save', 'Save', saveIcon, () => ViewerTools.saveViewerFileWithAnnotations(fileObj, isFileChanged, dispatch, currentDoc, currentFile, importedFileIds, selectedFileData))
        const discardButton = this.createToolbarItem('custom', 'discard', 'Discard', trashIcon, () => this.discardChanges(dispatch, currentFile));
        const editPDF: any = this.createToolbarItem('custom', 'edit-pdf', 'Edit PDF', editIcon, () => this.editPDF(this.instance, dispatch));
        const downloadButton: any = this.createToolbarItem('custom', 'download', 'Download', downloadIcon, () => this.downloadFile(currentFile));
        const printButton: any = this.createToolbarItem('custom', 'print', 'Print', printIcon, PDFActions.printPDF);

        let customizedToolBarItems: any = [editPDF, downloadButton, printButton];

        this.currentToolbar.forEach((toolbaritem) => {
            customizedToolBarItems.push(
                toolbarItems?.filter((el) => el.type === toolbaritem)[0]
            );
        });

        customizedToolBarItems = customizedToolBarItems.map((toolbarItem: any) => {
            if (toolbarItem?.type === "annotate") {
                toolbarItem.icon = annotationsIcon
            }
            else if (toolbarItem?.type === "pan") {
                toolbarItem.icon = panIcon
            }
            return toolbarItem
        })

        //if (isFileChanged) {
        customizedToolBarItems.push(discardButton)
        //}
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

    static async updateAnnotationDefaultValues(instance: any) {

        const red = new PSPDFKit.Color({ r: 255, g: 0, b: 0 });
        const yellow = new PSPDFKit.Color({ r: 255, g: 255, b: 0 })

        for (const [key, value] of Object.entries(PSPDFKit.defaultAnnotationPresets)) {
            if (typeof value === 'object') {

                const ap = instance.annotationPresets;

                switch (key) {
                    case 'text-highlighter':
                    case 'note':
                        ap[key] = {
                            ...value,
                            color: yellow
                        }
                        break;

                    case 'highlighter':
                        ap[key] = {
                            ...value,
                            strokeColor: yellow
                        }
                        break;

                    case 'text':
                        ap[key] = {
                            ...value,
                            fontColor: red,
                            fontSize: 12,
                            fontFamily: 'Helvetica'
                        }
                        break;

                    default:
                        ap[key] = {
                            ...value,
                            strokeWidth: 4,
                            strokeColor: red
                        }
                        break;
                }


                instance.setAnnotationPresets(ap)
            }
        }
    }

    static editPDF(instance: any, dispatch: any) {
        instance.setViewState((viewState: any) =>
            viewState.set(
                "interactionMode", 'DOCUMENT_EDITOR'
            )
        );

        dispatch({
            type: ViewerActionsType.SetIsFileChanged,
            payload: false
        })
    }

    static async convertImageToPDF(src: any, isImported: boolean, fileData: any, isPDF: boolean) {

        let file: any = "";
        let pageIndex = 0;

        if (isImported) {
            pageIndex = 1;
        }


        let el = document.createElement('div');
        el.id = "local-viewer-container";
        el.style.height = '0.01vh';
        el.style.width = '0.01vh';
        el.style.display = 'none';
        document.body.appendChild(el);
        let localInstance: any = await this.loadlocalInstance(src);
        try {


            if (!isPDF) {
                let imageSize = localInstance?.pageInfoForIndex(0);

                await this.addAnEmptyPage(localInstance, pageIndex);
                let newPageSize = await localInstance?.pageInfoForIndex(0);
                await this.addImageAsAnnotationOnThePage(localInstance, src, newPageSize, pageIndex, imageSize);
                file = await this.createPDFFileFromImage(localInstance, isPDF, true);
                if (localInstance) {
                    await PSPDFKit.unload(localInstance);
                }
                localInstance = await this.loadlocalInstance(file);
            }

            if (isImported)
                await this.addAnnotationToPage(localInstance, fileData)

            file = await this.createPDFFileFromImage(localInstance, isPDF, false);

        } catch (error) {

            console.log('error', error);
        }
        if (localInstance) {
            PSPDFKit.unload(localInstance);
            document.body.removeChild(el);
        }

        return file;
    }

    static async loadlocalInstance(src: any) {
        try {
            let instance = await PSPDFKit.load({
                document: await src?.arrayBuffer(),
                container: '#local-viewer-container',
                licenseKey: licenseKey,
                baseUrl: baseUrl,
            });
            return instance;
        }
        catch (error) {
            console.log(error)
        }
    }

    static async addAnEmptyPage(instance: any, pageToKeep: number) {




        await instance.applyOperations([
            {
                type: "addPage",
                beforePageIndex: 0, // Add new page after page 1.
                backgroundColor: new PSPDFKit.Color({ r: 255, g: 255, b: 255 }), // Set the new page background color.
                pageWidth: 602.986083984375,
                pageHeight: 782.9860229492188,
                rotateBy: 0 // No rotation.
                // Insets are optional.
            },
            {
                type: "keepPages",
                pageIndexes: [pageToKeep] // Remove all pages except pages 0 to 2.
            }
        ]);
    }

    static async addImageAsAnnotationOnThePage(instance: any, src: any, pageSize: any, pageToAnnotate: number, originalImage: any) {

        let imageDimensions = this.getImagedimensoins(originalImage, pageSize)

        const imageAttachmentId = await instance.createAttachment(src);
        const annotation = new PSPDFKit.Annotations.ImageAnnotation({
            pageIndex: pageToAnnotate,
            contentType: "image/jpeg",
            imageAttachmentId,
            description: "Example Image Annotation",
            boundingBox: new PSPDFKit.Geometry.Rect({
                left: 0,
                top: 0,
                width: imageDimensions.width,
                height: imageDimensions.height
            }),
        });
        let anno = await instance.createAnnotation(annotation);
        await instance.saveAnnotations();
        await instance.ensureAnnotationSaved(anno);
    }


    static getImagedimensoins(originalImage: any, pageSize: any) {
        let pageWidth: any = originalImage.width > pageSize.width ? pageSize.width : originalImage.width
        let pageHeight: any = originalImage.height > pageSize.height ? pageSize.height : originalImage.height

        let imageDimensions = {
            width: pageWidth,
            height: pageHeight
        }

        return imageDimensions;

    }

    static async addAnnotationToPage(instance: any, fileData: any) {
        let { id, fromRequestId, fromDocId, fromFileId, isFromCategory, isFromWorkbench, isFromTrash } = fileData
        let currentDoc = {
            id,
            fromRequestId,
            fromDocId,
            fromFileId
        }

        let annotations = await AnnotationActions?.fetchAnnotations(currentDoc, isFromWorkbench, isFromCategory, isFromTrash);
        if (annotations && annotations.length) {
            for (const annotation of annotations) {
                let n: any = await instance?.create(PSPDFKit.Annotations.fromSerializableObject(annotation));
            }
        }

    }

    static async createPDFFileFromImage(instance: any, isPDF: boolean, isFlatten: boolean) {

        const buffer = await instance.exportPDF({ flatten: isFlatten });
        const blob = new Blob([buffer], { type: "arraybuffer" });
        let file = await new File([blob], "file_name.pdf", { lastModified: Date.now(), type: "application/pdf" });
        return file;
    }

}

