import React, { useEffect, useRef, useState } from 'react'
import { useContext } from 'react';
import DocumentActions from '../../../../../../Store/actions/DocumentActions';
import { ViewerActions } from '../../../../../../Store/actions/ViewerActions';
import { DocumentActionsType } from '../../../../../../Store/reducers/documentsReducer';
import { ViewerActionsType } from '../../../../../../Store/reducers/ViewerReducer';
import { Store } from '../../../../../../Store/Store';
import { AnnotationActions } from '../../../../../../Utilities/AnnotationActions';
import { PDFActions } from '../../../../../../Utilities/PDFActions';
import { PDFThumbnails } from '../../../../../../Utilities/PDFThumbnails';
import { ViewerTools } from '../../../../../../Utilities/ViewerTools';
// import { ViewerActions } from '../../../../../../Utilities/ViewerActions';
import { DocumentDropBox } from '../DocumentDropBox/DocumentDropBox';
import { FileItem } from '../FileItem/FileItem';

export const FilesList = ({document, refReassignDropdown, docInd, setRetryFile, getDocswithfailedFiles, setOpenReassignDropdown, inputRef }: any) => {

    const [draggingSelf, setDraggingSelf] = useState<boolean>(false);
    const [draggingItem, setDraggingItem] = useState<boolean>(false);
    const { state, dispatch } = useContext(Store);

    const documents: any = state.documents;
    const { isFileChanged, currentFile }: any = state.viewer;
    
    const isDragging: any = documents?.isDragging;
    const uploadFailedDocs: any = documents?.uploadFailedDocs;
    const documentItems: any = documents?.documentItems;

    const onDrophandler = async (e: any) => {
        let file = JSON.parse(e.dataTransfer.getData('file'))
        if (isFileChanged && file?.fromFileId === currentFile?.fileId) {
            dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });

            return;
        }
        if (isDragging) {
            dispatch({ type: DocumentActionsType.SetIsDragging, payload: false });
            dispatch({ type: DocumentActionsType.SetIsDraggingCurrentFile, payload: false });
        }


        let { isFromWorkbench, isFromCategory, isFromThumbnail, isFromTrash } = file;
        if (isFromWorkbench) {

            let success = await DocumentActions.moveFromWorkBenchToCategory(
                document.id,
                document.requestId,
                document.docId,
                file.fromFileId,
            );

            if (success) {
                await DocumentActions.getDocumentItems(dispatch)
                await DocumentActions.getWorkBenchItems(dispatch);
            }

        } else if (isFromCategory) {
            let success = await DocumentActions.reassignDoc(
                file.id,
                file.fromRequestId,
                file.fromDocId,
                file.fromFileId,
                document.requestId,
                document.docId

            );

            if (success) {
                await DocumentActions.getDocumentItems(dispatch)
            }
        } else if (isFromThumbnail) {
            let { id, requestId, docId } = document
            let fileObj = {
                id,
                requestId,
                docId,
                fileId: "000000000000000000000000",
                isFromCategory: true
            }
            let fileData = await PDFActions.createNewFileFromThumbnail(file.index);
            let success = await ViewerTools.saveFileWithAnnotations(fileObj, fileData, true, dispatch, document);

            // let saveAnnotation = await AnnotationActions.saveAnnotations(annotationObj,true);
            if(!!success){
            await PDFThumbnails.removePages([file.index])
            await DocumentActions.getDocumentItems(dispatch)
            dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true })
            }
           
        } else if(isFromTrash){

            let success = await DocumentActions.moveFromTrashToCategory(
                document.id,
                document.requestId,
                document.docId,
                file.fromFileId,
            );

            if (success) {
                await DocumentActions.getDocumentItems(dispatch)
                await DocumentActions.getTrashedDocuments(dispatch);
            }

        }

    }


    
    const deleteFiles = async (deletedFile: any) => {
        let files = document?.files.filter((docFile: any) => docFile.id !== deletedFile.id)

        let docItems = documentItems.map((doc: any) => {
            if (doc.docId === document?.docId) {
                doc.files = files
            }
            return doc
        })
        dispatch({ type: DocumentActionsType.SetDocumentItems, payload: docItems });
        let updatedFailedFiles = uploadFailedDocs.filter((file: any) => file.id !== deletedFile.id)
        
        dispatch({
            type: DocumentActionsType.SetFailedDocs,
            payload: updatedFailedFiles
        })
    }
    
    const retry = (file: any) => {
        setRetryFile({file, document})
        // removeFile(file);

        if (inputRef?.value) {
            inputRef.value = "";
        }
        if (inputRef) {
            inputRef.current.click();
        }
    }

    return (
        <div onDragEnd={() => {
            setDraggingSelf(false);
        }} className="dm-dt-tr2">

            <ul className="dm-dt-docList"
            // onDrop={handleFileDrop}
            >
                {
                    document.files.length > 0 ?
                        (
                            document.files?.map((f: any, i: number) => {
                                return (
                                    <FileItem key={f.id}
                                        retry={retry}
                                        file={f}
                                        docInd={docInd}
                                        fileInd={i}
                                        document={document}
                                        setDraggingSelf={setDraggingSelf}
                                        setDraggingItem={setDraggingItem}
                                        refReassignDropdown={refReassignDropdown}
                                        deleteFile={deleteFiles}
                                        getDocswithfailedFiles={getDocswithfailedFiles}
                                        setOpenReassignDropdown={setOpenReassignDropdown}
                                    />
                                )
                            })
                        ) :
                        (
                            <li className="empty-items">Document type is empty</li>
                        )

                }
                {isDragging && !draggingSelf &&
                    <li
                        onDragOver={(e) => e.preventDefault()}
                        onDrop={(e: any) => {
                            e.preventDefault();
                            onDrophandler(e)
                        }}
                        className="drag-wrap"
                    >
                        <p>Drop Here</p>
                    </li>
                }
            </ul>

        </div>
    )
}
