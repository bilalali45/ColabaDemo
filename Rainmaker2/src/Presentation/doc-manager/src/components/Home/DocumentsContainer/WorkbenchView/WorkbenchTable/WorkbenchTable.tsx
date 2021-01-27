import React, { useContext, useEffect, useState, useRef } from 'react'
import { WorkbenchItem } from './WorkbenchItem/WorkbenchItem'
import { BorrowerDocIcon, MCUDocIcon } from '../../../../../shared/Components/Assets/SVG'
import DocumentActions from '../../../../../Store/actions/DocumentActions';
import { Store } from '../../../../../Store/Store';
import { DocumentActionsType } from '../../../../../Store/reducers/documentsReducer'
import { FileItem } from '../../DocumentTableView/DocumentsTable/FileItem/FileItem';
import { LocalDB } from '../../../../../Utilities/LocalDB';
import { DocumentRequest } from '../../../../../Models/DocumentRequest';
import { PDFThumbnails } from '../../../../../Utilities/PDFThumbnails';
import { AnnotationActions } from '../../../../../Utilities/AnnotationActions';
import { ViewerTools } from '../../../../../Utilities/ViewerTools';
import { ViewerActionsType } from '../../../../../Store/reducers/ViewerReducer';
import { PDFActions } from '../../../../../Utilities/PDFActions';
import { CurrentInView } from '../../../../../Models/CurrentInView';
import { FileUpload } from '../../../../../Utilities/helpers/FileUpload';
import { SelectedFile } from '../../../../../Models/SelectedFile';

const nonExistentFileId = '000000000000000000000000';


export const WorkbenchTable = () => {

    const [draggingSelf, setDraggingSelf] = useState<boolean>(false);
    const [draggingItem, setDraggingItem] = useState<boolean>(false);
    const [isDraggingOver, setIsDraggingOver] = useState<boolean>(false);
    const [failedFiles, setFailedFiles] = useState([]);
    const refReassignDropdownWB = useRef<any>(null);

    const { state, dispatch } = useContext(Store);
    const { currentFile, isFileChanged }: any = state.viewer;
    const { currentDoc, importedFileIds }: any = state.documents;
    const selectedfiles: Document[] = currentDoc?.files || null;
    const documents: any = state.documents;
    let loanId = documents?.loanApplicationId;
    const workbenchItems: any = documents?.workbenchItems;
    const isDragging: any = documents?.isDragging;
    let loanApplicationId = LocalDB.getLoanAppliationId();


    useEffect(() => {
        if (!workbenchItems) {
            getWorkbenchItems();
        }
    }, [!workbenchItems])

    const getWorkbenchItems = async () => {
        let d = await DocumentActions.getWorkBenchItems(dispatch, importedFileIds);
    }

    const removeFailedItem = item => setFailedFiles(pre => pre?.filter(ff => ff !== item))


    const setCurrentDocument = () => {
        let document = new DocumentRequest(currentFile?.id,
            nonExistentFileId,
            nonExistentFileId,
            "",
            "",
            "",
            [],
            "",
            ""
        )
        if (document) {
            dispatch({ type: DocumentActionsType.SetCurrentDoc, payload: null });

            dispatch({ type: DocumentActionsType.SetCurrentDoc, payload: document });
        }

    }
    const handleOnDrop = async (e: any) => {
        setIsDraggingOver(false);

        let filesFromPC = e?.dataTransfer?.files;
        if (filesFromPC?.length) {
            e.preventDefault();

            // dispatch({ type: ViewerActionsType.SetIsSaving, payload: true });


            for (const file of filesFromPC) {
                let fileObj = {
                    id: loanId,
                    fileId: "000000000000000000000000",
                    isFromWorkbench: true
                }
                dispatch({
                    type: ViewerActionsType.SetFileProgress,
                    payload: 0,
                });

                if (await !FileUpload.isSizeAllowed(file)) {

                    let failedFile = {
                        clientName: file.name,
                        notAllowed: true,
                        notAllowedReason: 'FileSize',
                    }
                    setFailedFiles((pre: any) => {
                        return [failedFile, ...pre]
                    })

                } else if ((await FileUpload.isTypeAllowed(file)) === false) {
                    let failedFile = {
                        clientName: file.name,
                        notAllowed: true,
                        notAllowedReason: 'FileType'
                    }
                    setFailedFiles((pre: any) => {
                        return [failedFile, ...pre]
                    })

                } else {
                    
                    let newWorkBenchItems = await DocumentActions.getWorkBenchItems(dispatch, [])
                    let success: any = await ViewerTools.saveFileWithAnnotations(fileObj, file, true, dispatch, newWorkBenchItems, []);
                    await DocumentActions.getWorkBenchItems(dispatch, [])
                    
                }
            }


            // dispatch({ type: ViewerActionsType.SetIsSaving, payload: false });
            return;
        }

        let file: any = JSON.parse(e.dataTransfer.getData('file'));

        if (isFileChanged && file?.fromFileId === currentFile?.fileId) {
            dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });
            dispatch({ type: ViewerActionsType.SetFileToChangeWhenUnSaved, payload: { file: null, document: null, action: "dragged", isWorkbenchFile: false } });
            return;
        }
        e.preventDefault();
        if (isDragging) {
            dispatch({ type: DocumentActionsType.SetIsDragging, payload: false });
            dispatch({ type: DocumentActionsType.SetIsDraggingCurrentFile, payload: false });
        }
        if (draggingItem) {
            setDraggingItem(false);
            return;
        }

        let { isFromThumbnail, isFromCategory, isFromTrash } = file


        if (isFromCategory) {
            let { id, fromRequestId, fromDocId, fromFileId }: any = file;
            let newWorkBench = await DocumentActions.moveFileToWorkbench({ id, fromRequestId, fromDocId, fromFileId }, false);
            if (newWorkBench) {
                await setCurrentDocument()
                if(currentFile && currentFile.fileId === file.fromFileId){
                    let currFile = new CurrentInView(currentFile.id, currentFile.src, currentFile.name, true, currentFile.fileId);
                    await dispatch({ type: ViewerActionsType.SetCurrentFile, payload: currFile });
                }
                await DocumentActions.getDocumentItems(dispatch, importedFileIds)
                await DocumentActions.getWorkBenchItems(dispatch, importedFileIds)
        
            }
        }

        if (isFromThumbnail) {

            dispatch({ type: ViewerActionsType.SetIsSaving, payload: true });
            let { id } = currentFile
            let fileObj = {
                id,

                fileId: "000000000000000000000000",
                isFromWorkbench: true
            }

            dispatch({
                type: ViewerActionsType.SetFileProgress,
                payload: 0,
            });
            let fileData = await PDFActions.createNewFileFromThumbnail(file.indexes, currentFile, workbenchItems);
            let success: any = await ViewerTools.saveFileWithAnnotations(fileObj, fileData, true, dispatch, workbenchItems, importedFileIds, file.indexes);

            // let saveAnnotation = await AnnotationActions.saveAnnotations(annotationObj,true);
            // if(!!success){
            await PDFThumbnails.removePages(file.indexes)
            await DocumentActions.getWorkBenchItems(dispatch, importedFileIds)
            dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true })
            // }
            dispatch({ type: ViewerActionsType.SetIsSaving, payload: false });

        }

        if (isFromTrash) {
            let { id, fromFileId }: any = file;
            let success = await DocumentActions.moveTrashFileToWorkBench(
                id,
                fromFileId
            );
            if (success) {
                await DocumentActions.getTrashedDocuments(dispatch, importedFileIds);
                await DocumentActions.getWorkBenchItems(dispatch, importedFileIds);

            }
        }

        dispatch({ type: ViewerActionsType.SetPerformNextAction, payload: false });
    }


    return (

        <div
            onMouseLeave={() => setDraggingSelf(false)}
            onDragEnd={() => {
                setDraggingSelf(false);
                // setIsDraggingOver(false)
            }}
            onDragOver={(e) => {
                e.preventDefault();
                setIsDraggingOver(true)
            }}


            onDrop={handleOnDrop}
            id="c-WorkbenchTable"
            className="dm-workbenchTable c-WorkbenchTable">
            <div className="dm-wb-thead">
                <div className="dm-wb-thead-left">Document</div>
            </div>


            {isDraggingOver && !draggingSelf && <div className="dropwarp"
                // onDrop={handleOnDrop}
                onDragLeave={(e) => {

                    e.preventDefault();
                    setIsDraggingOver(false);
                    setDraggingSelf(false);
                }}
                onMouseLeave={(e) => {
                    e.preventDefault();
                    setIsDraggingOver(false)
                    setDraggingSelf(false);
                }}
            ><span>Drop Here</span></div>}
            <div className="dm-wb-tbody" ref={refReassignDropdownWB}

            //style={{ minHeight: (isDragging && !draggingSelf ? '227px' : '287px') }}

            >

                <section className={`dm-wb-tr dm-wb-doc-list`} >
                    <div className="dm-wb-row">
                        <ul className={`dm-dt-docList ${isDraggingOver ? 'dragActive' : ''}`}
                        >
                            {
                                ((workbenchItems || failedFiles) && (failedFiles?.length || workbenchItems?.length)) ? [...failedFiles, ...workbenchItems]?.map((d: any, i: number) => {
                                    return (
                                        <WorkbenchItem key={i}
                                            file={d}
                                            setDraggingSelf={setDraggingSelf}
                                            setDraggingItem={setDraggingItem}
                                            setIsDraggingOver={setIsDraggingOver}
                                            refReassignDropdown={refReassignDropdownWB}
                                            removeFailedItem={removeFailedItem}
                                        />
                                        // file={d} />
                                    )
                                }) : ''
                            }

                            {/* {isDraggingOver && !draggingSelf && <li
                                className="drag-wrap"
                            >
                                <p>Drop Here</p>
                            </li>} */}
                        </ul>
                    </div>
                </section>
            </div>

        </div>


    )
}
