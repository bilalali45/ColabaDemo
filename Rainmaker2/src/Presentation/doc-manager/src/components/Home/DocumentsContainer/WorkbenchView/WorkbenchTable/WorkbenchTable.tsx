import React, { useContext, useEffect, useState,useRef } from 'react'
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

const nonExistentFileId = '000000000000000000000000';


export const WorkbenchTable = () => {

    const [draggingSelf, setDraggingSelf] = useState<boolean>(false);
    const [draggingItem, setDraggingItem] = useState<boolean>(false);
    const refReassignDropdown = useRef<any>(null);
    const { state, dispatch } = useContext(Store);
    const { currentFile }: any = state.viewer;
    const { currentDoc }: any = state.documents;
    const documents: any = state.documents;
    const workbenchItems: any = documents?.workbenchItems;
    const isDragging: any = documents?.isDragging;
    let loanApplicationId = LocalDB.getLoanAppliationId();
    
    
    useEffect(() => {
        if (!workbenchItems) {
            getWorkbenchItems();
        }
    }, [!workbenchItems])

    const getWorkbenchItems = async () => {
        let d = await DocumentActions.getWorkBenchItems(dispatch);
    }

    const handleOnDrop = async (e: any) => {
        dispatch({ type: DocumentActionsType.SetIsDragging, payload: false });
        if (draggingItem) {
            setDraggingItem(false);
            return;
        }
       
        let file: any = JSON.parse(e.dataTransfer.getData('file'));
        let {isFromThumbnail, isFromCategory, isFromTrash} = file
        
        if(isFromCategory){
            let {id, fromRequestId, fromDocId, fromFileId} : any = file;
            let newWorkBench = await DocumentActions.moveFileToWorkbench({id, fromRequestId, fromDocId, fromFileId}, false);
            if(newWorkBench){
                await DocumentActions.getWorkBenchItems(dispatch)
    
                await DocumentActions.getDocumentItems(dispatch)
            }
        }

        if(isFromThumbnail){
            

          let{id} = currentFile
            let fileObj = {
                id, 
                
                fileId:"000000000000000000000000",
                isFromWorkbench:true
                }

                let fileData = await PDFActions.createNewFileFromThumbnail(file.index);
            let success : any = await ViewerTools.saveFileWithAnnotations(fileObj, fileData, true, dispatch, workbenchItems  );
                
                    // let saveAnnotation = await AnnotationActions.saveAnnotations(annotationObj,true);
                    // if(!!success){
                        await PDFThumbnails.removePages([file.index])
                        await DocumentActions.getWorkBenchItems(dispatch)
                        dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true })
                    // }
      
        }
        
        if(isFromTrash){
            let {id, fileId} : any = file;
            let success = await DocumentActions.moveTrashFileToWorkBench(
                id,
                fileId
              );
              if (success) {
                await DocumentActions.getTrashedDocuments(dispatch);
                await DocumentActions.getWorkBenchItems(dispatch);
                
              }

        }
        
    }

    
    return (

        <div
            onDragEnd={() => setDraggingSelf(false)}
            onDragOver={(e) => e.preventDefault()}
           
            id="c-WorkbenchTable"
            className="dm-workbenchTable c-WorkbenchTable">
            <div className="dm-wb-thead">
                <div className="dm-wb-thead-left">Document</div>
            </div>
            
            {isDragging && !draggingSelf &&<div  onDrop={handleOnDrop} className="isDragging-wrap"><div className="drag-wrap"><p>Drop Here</p></div></div>}
            <div className="dm-wb-tbody" style={{ minHeight: (isDragging && !draggingSelf ? '227px' : '287px') }}>
            
                <section className="dm-wb-tr dm-wb-doc-list" ref={refReassignDropdown}>
                    <div className="dm-wb-row">
                        <ul  className={`dm-dt-docList ${isDragging ? 'dragActive' : ''}`}
                        >
                            {
                                workbenchItems && workbenchItems.length >0 && workbenchItems?.map((d: any, i:number) => {
                                    return (
                                        <WorkbenchItem key={i}
                                        file={d}
                                        setDraggingSelf={setDraggingSelf}
                                        setDraggingItem={setDraggingItem}
                                        refReassignDropdown={refReassignDropdown}
                                        />
                                        // file={d} />
                                    )
                                })
                            }

                            {/*{isDragging && !draggingSelf && <li
                                className="drag-wrap"
                                >
                                <p>Drop Here</p>
                            </li>}*/}
                        </ul>
                    </div>
                </section>
            </div>
            
        </div>


    )
}
