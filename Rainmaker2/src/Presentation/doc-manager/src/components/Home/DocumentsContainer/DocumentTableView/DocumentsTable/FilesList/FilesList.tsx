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
import { AddFileToDoc } from '../../../../AddDocument/AddFileToDoc/AddFileToDoc';
// import { ViewerActions } from '../../../../../../Utilities/ViewerActions';
import { DocumentDropBox } from '../DocumentDropBox/DocumentDropBox';
import { FileItem } from '../FileItem/FileItem';

export const FilesList = ({document, refReassignDropdown, docInd, setRetryFile, getDocswithfailedFiles, setOpenReassignDropdown, inputRef, retryFile }: any) => {

    const [draggingSelf, setDraggingSelf] = useState<boolean>(false);
    const [draggingItem, setDraggingItem] = useState<boolean>(false);
    const { state, dispatch } = useContext(Store);

    const documents: any = state.documents;
    const { isFileChanged, currentFile }: any = state.viewer;

    const isDragging: any = documents?.isDragging;
    const uploadFailedDocs: any = documents?.uploadFailedDocs;
    const documentItems: any = documents?.documentItems;
    const importedFileIds:any = documents?.importedFileIds;
    const [addFileDialog, setAddFileDialog] = useState<boolean>(false);


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
        setAddFileDialog(!addFileDialog)
    }

    return (
        <div>
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
                {/* {isDragging && !draggingSelf &&
                    <li

                        className="drag-wrap"
                    >
                        <p>Drop Here</p>
                    </li>
                } */}
            </ul>

        </div>
            { addFileDialog && <AddFileToDoc 
            selectedDocTypeId = {document?.typeId}
            showFileDialog = {true}
            setVisible={()=>{}}
            setAddFileDialog={setAddFileDialog}
            retryFile = {retryFile}
            selectedDocName={document.docName}/>}
    </div>
    )
}
