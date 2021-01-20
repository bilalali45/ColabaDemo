import React, { useContext, useEffect, useState, useRef, ChangeEvent } from 'react'
import { DocumentItem } from './DocumentItem/DocumentItem';
import DocumentActions from '../../../../../Store/actions/DocumentActions';
import { Store } from '../../../../../Store/Store';
import { DocumentActionsType } from '../../../../../Store/reducers/documentsReducer';
import { ViewerActionsType } from '../../../../../Store/reducers/ViewerReducer';
import { LocalDB } from '../../../../../Utilities/LocalDB';
import { async } from 'q';
import { CurrentInView } from '../../../../../Models/CurrentInView';
import { AnnotationActions } from '../../../../../Utilities/AnnotationActions';
import { SelectedFile } from '../../../../../Models/SelectedFile';
import {Popover, OverlayTrigger, Button, Overlay} from "react-bootstrap";
import { FileUpload } from '../../../../../Utilities/helpers/FileUpload';
import { DocumentFile } from '../../../../../Models/DocumentFile';
import { ViewerActions } from '../../../../../Store/actions/ViewerActions';
import { DocumentRequest } from '../../../../../Models/DocumentRequest';
import {AddDocIcon} from "../../../../../shared/Components/Assets/SVG";
import { AddFileToDoc } from '../../../AddDocument/AddFileToDoc/AddFileToDoc';


export const DocumentsTable = () => {
    const refReassignDropdown = useRef<HTMLDivElement>(null);
    const { state, dispatch } = useContext(Store);

    const documents: any = state.documents;
    const catScrollFreeze: any = documents?.catScrollFreeze;
    const { currentDoc, documentItems, uploadFailedDocs, fileUploadInProgress, importedFileIds,searchdocumentItems,docSearchTerm }: any = state.documents;
    const {isLoading, SaveCurrentFile, DiscardCurrentFile,  currentFile}:any  = state.viewer;
    const [fileClicked, setFileClicked]= useState<boolean>(false);
    const [retryFile, setRetryFile] = useState<any>();
    const [failedDocs, setFailedDocs] = useState<DocumentFile[]>([]);
    const [selectedDoc, setSelectedDoc] = useState<DocumentRequest>();
    const [popshow, popsetShow] = useState(false);
    const [poptarget, popsetTarget] = useState(null);
    const docTypePopUp = useRef(null);
    const popUpDv = useRef<any>(null);
    const refAddFileLink = useRef<any>(null);
    const inputRef = useRef<HTMLInputElement>(null);
    const [addFileDialog, setAddFileDialog] = useState<boolean>(false);

    const nonExistentDocId = '000000000000000000000000';
    useEffect(() => {
      window.addEventListener("mousedown", handleClickOutside);
      return () => {
          window.removeEventListener("mousedown", handleClickOutside);
      };
  },[popshow] );

    useEffect(()=>{
      
      if(DiscardCurrentFile && importedFileIds && importedFileIds.length){
          removeHiddenFilesFromStore(importedFileIds.length)
        
      }
    }, [DiscardCurrentFile])

    useEffect(()=>{
      
      if(SaveCurrentFile && importedFileIds){
      if( importedFileIds.length){
        removeOriginalFiles()
      }
      
      removeHiddenFilesFromStore(importedFileIds?.length)
    }
    }, [SaveCurrentFile])

    const removeOriginalFiles = async() =>{
    const temp  = await Promise.all(Array.from(importedFileIds).map(async (fileData) => {
         return removeOriginalFile(fileData)
         
    }).flat())    
       
    }
    const removeOriginalFile = async (fileData:any)=>{
          
      if(fileData.isFromWorkbench){
        return await DocumentActions.DeleteWorkbenchFile(fileData.id, fileData.fromFileId)
      } else if(fileData.isFromCategory){
        return await DocumentActions.DeleteCategoryFile(fileData)
      } else if(fileData.isFromTrash){
        return await DocumentActions.DeleteTrashFile(fileData.id, fileData.fromFileId)
      }

      
      
    }

    const removeHiddenFilesFromStore = async (isRemoveAllFiles)=> {
      
      dispatch({ type: DocumentActionsType.SetImportedFileIds, payload: [] })
      dispatch({ type: ViewerActionsType.SetSaveFile, payload: false })
      dispatch({ type: ViewerActionsType.SetDiscardFile, payload: false })
      if(isRemoveAllFiles){
        await DocumentActions.getTrashedDocuments(dispatch, [])
        await DocumentActions.getDocumentItems(dispatch, [])
        await DocumentActions.getWorkBenchItems(dispatch, [])
        
      }
        
      else{
        if(currentDoc.docId === nonExistentDocId){
          await DocumentActions.getWorkBenchItems(dispatch, [])
        }
        else{
          await DocumentActions.getDocumentItems(dispatch, [])
        }
      }
      
    }
    const handlePopClick = (event) => {
        popsetShow(!popshow);
        popsetTarget(event.target);
        setAddFileDialog(false)
    };
    const hideAddfilePopover = () => {
        popsetShow(false);
    };
    const openAddDocPopover = () => {
      let addDocLInk = window.document.getElementById("dm-h-linkAddDoc");
      addDocLInk.click();  
    };

    const handleClickOutside = (event:any) => {
        
        if (popshow && popUpDv && !popUpDv.current?.contains(event.target) && refAddFileLink && !refAddFileLink.current?.contains(event.target)) {
            popsetShow(!popshow);
        }
    }
    
      
    const [openedReassignDropdown,setOpenReassignDropdown] = useState<boolean>(false);

    useEffect(() => {
        if (!documentItems) {
            DocumentActions.getCurrentDocumentItems(dispatch, true, importedFileIds);
            checkIsByteProAuto()
        }
    }, [!documentItems]);

    const checkIsByteProAuto = async () => {
        let res: any = await DocumentActions.checkIsByteProAuto();
        let isAuto = res?.syncToBytePro != 2 ? true : false;
        dispatch({type: DocumentActionsType.SetIsByteProAuto, payload: isAuto});
      };

      const getDocswithfailedFiles = async() => {
        let foundFirstFileDoc: any = null;
        let foundFirstFile: any = null;
    
        let docs:any = await fetchDocuments()
        
            let uploadFailedFiles:DocumentFile[] = uploadFailedDocs.length? uploadFailedDocs : failedDocs;
        
            let failedFiles:DocumentFile[] = []
            if(uploadFailedFiles && uploadFailedFiles.length > 0){
                
               failedFiles= uploadFailedDocs.length? uploadFailedFiles.concat(failedDocs): uploadFailedFiles
              failedFiles = failedFiles.filter((file)=> file.id !== retryFile?.file?.id)
    
              
              dispatch({
                type:DocumentActionsType.SetFailedDocs, 
                payload:failedFiles
              })
              
              
                let allDocs:any;
                for (let index = 0; index < failedFiles.length; index++) {
                  allDocs = docs?.map((doc:any)=> {
                    if(doc.docId === failedFiles[index].docCategoryId){
                      doc.files = [...doc.files, failedFiles[index]]
                    }
                    return doc
                  })
                  
                  
                }
              
                setFailedDocs([])
                
                if(allDocs && allDocs.length) {
                  dispatch({ type: DocumentActionsType.SetDocumentItems, payload: allDocs });
                }
              }
              else 
              if(docs && !currentFile){
            
                    for (const doc of docs) {
                        if (doc?.files?.length) {
                            dispatch({ type: DocumentActionsType.SetCurrentDoc, payload: doc });
                            dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });
                            foundFirstFileDoc = doc;
                            foundFirstFile = doc?.files[0];
                            ViewerActions.resetInstance(dispatch)
                            
                            await DocumentActions.viewFile(foundFirstFileDoc, foundFirstFile, dispatch);
                            break;
                        }
                        
                    }
                }
      } 

      const fetchDocuments = async()=>{
        let d = await DocumentActions.getDocumentItems(dispatch, importedFileIds)
        return d;
    
      }
      const selectDocTypeClick=(doc:DocumentRequest)=>{
        setSelectedDoc(doc);
          popsetShow(!popshow);
          setAddFileDialog(true)
      }
      const noDocFound=()=>{
        if(docSearchTerm){return (<div> No Results Found for “{docSearchTerm}”</div>)}
        else {return (<div>  </div> )}
        }

    return (
        <div id="c-DocTable" className="dm-docTable c-DocTable" > 

            <div className="dm-dt-thead">
                <div className="dm-dt-thead-left">Document</div>
                <div className="dm-dt-thead-right">Status</div>
            </div>

            <div className={`dm-dt-tbody ${catScrollFreeze?" freeze":""}`} ref={refReassignDropdown}>

            {
                 documentItems && documentItems.length ?
                 documentItems?.map((d: any, i: number) => {
                      return (
                          <DocumentItem key={i}
                              docInd={i}
                              document={d}
                              refReassignDropdown={refReassignDropdown}
                              setFileClicked={setFileClicked}
                              inputRef={inputRef}
                              fileClicked = {fileClicked}
                              setOpenReassignDropdown={setOpenReassignDropdown}
                              getDocswithfailedFiles ={getDocswithfailedFiles}
                              setRetryFile = {setRetryFile}
                              selectedDoc={selectedDoc}
                              retryFile = {retryFile}
                          />
                      )
                  }) :  noDocFound()
                  // :
                  
                  //   documentItems && documentItems.length ?( documentItems?.map((d: any, i: number) => {
                  //       return (
                  //           <DocumentItem key={i}
                  //               docInd={i}
                  //               document={d}
                  //               refReassignDropdown={refReassignDropdown}
                  //               setFileClicked={setFileClicked}
                  //               inputRef={inputRef}
                  //               fileClicked = {fileClicked}
                  //               setOpenReassignDropdown={setOpenReassignDropdown}
                  //               getDocswithfailedFiles ={getDocswithfailedFiles}
                  //               setRetryFile = {setRetryFile}
                  //               selectedDoc={selectedDoc}
                  //               retryFile = {retryFile}
                  //           />
                  //       )
                  //   })
                  //   ):null
                }

            </div>
            {!fileUploadInProgress && 
                        <div className="dm-dt-foot" >
                            {/*<button>Add Files</button>*/}
                            {/*<OverlayTrigger trigger="click" placement="right-end" overlay={popover} rootClose={true}>
                                <a>Add Files +</a>
                            </OverlayTrigger>*/}
                            <a ref={refAddFileLink} onClick={handlePopClick} className="addFile">Add Files +</a>
                            <div ref={popUpDv}>
                            <Overlay placement="right-end" onHide={hideAddfilePopover} container={popUpDv.current} target={poptarget} show={popshow} rootClose={true}>
                                <Popover id="addFiles-popover" className="ReassignOverlay">
                                    <div >
                                        <Popover.Title as="h3">Select Document Type</Popover.Title>
                                        <Popover.Content>
                                            {documentItems && documentItems.length > 0 ? (
                                                <div className="wrap-doc-type">
                                                    <ol className="dm-dt-docList pop" ref={docTypePopUp}>
                                                        {documentItems &&
                                                        documentItems.length > 0 &&
                                                        documentItems.map((doc: any, i: number) => (
                                                            <li title={doc.docName} key={i} onMouseDown={() => selectDocTypeClick(doc)}>
                                                                {doc.docName}
                                                            </li>
                                                        ))}
                                                    </ol>

                                                </div>
                                            ) : (
                                                <div className="emptyList">
                                                    <p>There is no item in List</p>
                                                </div>
                                            )}
                                        </Popover.Content>
                                        <Popover.Title as="div" bsPrefix="popover-footer">
                                            <div className="dh-actions-lbl-wrap" onClick={openAddDocPopover}>
                                                <div className="dm-h-icon"><AddDocIcon /></div>
                                                <div className="dm-h-lbl">
                                                    <span>Add Document</span>
                                                </div>
                                            </div>
                                        </Popover.Title>
                                    </div>
                                </Popover>
                            </Overlay>
                            </div>
                            {addFileDialog &&
                            <AddFileToDoc 
                                selectedDocTypeId = {selectedDoc?.typeId}
                                showFileDialog = {addFileDialog}
                                setVisible={popsetShow}
                                setAddFileDialog={setAddFileDialog}
                                retryFile = {retryFile}
                                selectedDocName={selectedDoc?.docName}/>
                            }
            </div>
            }
        </div>
    )
}
