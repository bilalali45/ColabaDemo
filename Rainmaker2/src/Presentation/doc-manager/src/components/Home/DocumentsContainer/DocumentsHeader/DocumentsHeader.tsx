import React, { useState, useRef, useContext, useEffect } from "react";
import {
  AddDocIcon,
  EmptyTrashIcon,
  ReassignIcon,
  TrashIcon,
  FileIcon,
  TrashCan,
  SyncIcon,
  ReassignListIcon,
  PutBackIcon,
} from "../../../../shared/Components/Assets/SVG";
import { AddDocument } from "../../AddDocument/AddDocument";
import Overlay from "react-bootstrap/Overlay";
import Popover from "react-bootstrap/Popover";
import { TrashItem } from "../../../../Models/TrashItem";
import { Document } from "../../../../Models/Document";
import DocumentActions from "../../../../Store/actions/DocumentActions";
import { Store } from "../../../../Store/Store";
import { TemplateActions } from "../../../../Store/actions/TemplateActions";
import { TemplateActionsType } from "../../../../Store/reducers/TemplatesReducer";
import { DocumentActionsType } from "../../../../Store/reducers/documentsReducer";
import { LocalDB } from "../../../../Utilities/LocalDB";
import { getDateString, getFileDate } from "../../../../Utilities/helpers/DateFormat";
import { PDFThumbnails } from "../../../../Utilities/PDFThumbnails";
import { AnnotationActions } from "../../../../Utilities/AnnotationActions";
import { ViewerTools } from "../../../../Utilities/ViewerTools";
import { ViewerActionsType } from "../../../../Store/reducers/ViewerReducer";
import { ViewerActions } from "../../../../Store/actions/ViewerActions";
import { DocumentFile } from "../../../../Models/DocumentFile";
import { PDFActions } from "../../../../Utilities/PDFActions";


export const DocumentsHeader = () => {
  const [showTrashOverlay, setShowTrash] = useState<boolean>(false);
  const [targetTrash, setTargetTrash] = useState(null);
  const refTrashOverlay = useRef(null);
  const [trashedDocs, setTrashedDocs] = useState<TrashItem[]>([]);
  const { state, dispatch } = useContext(Store);
  const { currentDoc,uploadFailedDocs }: any = state.documents;
  const { currentFile, selectedFileData }: any = state.viewer;

  const templateManager: any = state.templateManager;
  const currentTemplate = templateManager?.currentTemplate;
  const templates = templateManager?.templates;
  const templateDocuments = templateManager?.templateDocuments;
  const categoryDocuments = templateManager?.categoryDocuments;
  const { trashedDoc }: any = state.documents;
  const documents: any = state.documents;
  const isDragging: any = documents?.isDragging;
  const [failedDocs, setFailedDocs] = useState<DocumentFile[]>([]);
  const [retryFile, setRetryFile] = useState<DocumentFile>();
  let loanApplicationId = LocalDB.getLoanAppliationId();

  useEffect(() => {
    if (!categoryDocuments) {
      fetchCurrentCatDocs();
      fetchTrashedDocs();
    }
  }, []);

  const handleClickTrash = async (event: any) => {

    setShowTrash(!showTrashOverlay);
    setTargetTrash(event.target);
  };
  const TrashOverlay = () => {
    setShowTrash(false);
  };

  const fetchTrashedDocs = async () => {
    let res = await DocumentActions.getTrashedDocuments(dispatch);
    
  }
  const fetchCurrentCatDocs = async () => {
    let currentCatDocs: any = await TemplateActions.fetchCategoryDocuments();

    if (currentCatDocs) {
      dispatch({
        type: TemplateActionsType.SetCategoryDocuments,
        payload: currentCatDocs,
      });
    }
  };

  const addDocumentToList = async (doc: Document, type: string) => {
    let data = {
      document: {
        typeId: doc.docTypeId,
        displayName: doc.docType,
      },
    };
    try {
      let res = await DocumentActions.addDocCategory(loanApplicationId, data);
      if (res) {
        let d = await DocumentActions.getDocumentItems(dispatch);
        
      }
    } catch (error) { }
  };

  const moveTrashFileToWorkbench = async (file: any) => {
    let success = await DocumentActions.moveTrashFileToWorkBench(
      file.id,
      file.fileId
    );
    if (success) {
      let res = await DocumentActions.getTrashedDocuments(dispatch);
      let d = await DocumentActions.getWorkBenchItems(dispatch);
      
    }
  };

  const onDrophandler= async(e:any)=>{
    let success = false;
    let file = JSON.parse(e.dataTransfer.getData('file'))
    let{isFromWorkbench, isFromCategory, isFromThumbnail } = file;
    let cancelCurrentFileViewRequest:boolean = false;
    if(selectedFileData && selectedFileData?.fileId === file.fromFileId){
      cancelCurrentFileViewRequest = true;
    }

    if(isFromWorkbench){
      success = await DocumentActions.moveWorkBenchFileToTrash(
        file.id,
        file.fromFileId,
        cancelCurrentFileViewRequest
      );
      if (success) { 
        
        if(selectedFileData && selectedFileData?.fileId === file.fromFileId){
          if (currentFile) {
            ViewerActions.resetInstance(dispatch)
          }
          dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });
          await DocumentActions.getCurrentWorkbenchItem(dispatch);
        }
        else{
          
        let d = await DocumentActions.getWorkBenchItems(dispatch);
        }
        let docs = await DocumentActions.getTrashedDocuments(dispatch)
      }
    }
    else if(isFromCategory){
      let cancelCurrentFileViewRequest:boolean = false;
    if(selectedFileData && selectedFileData?.fileId === file.fromFileId){
      cancelCurrentFileViewRequest = true;
    }
    
      success = await DocumentActions.moveCatFileToTrash(
        file.id,
        file.fromRequestId,
        file.fromDocId,
        file.fromFileId,
        cancelCurrentFileViewRequest
      );
      if (success) {
        
  
        if (selectedFileData && selectedFileData?.fileId === file.fromFileId) {
          await DocumentActions.getCurrentDocumentItems(dispatch, false);
        }
        await getDocswithfailedFiles()
        let res = await DocumentActions.getTrashedDocuments(dispatch);
      }
    } else if(isFromThumbnail){

      let{id} = currentFile
            let fileObj = {
                id, 
                
                fileId:"000000000000000000000000",
                isFromTrash:true
                }
                let fileData = await PDFActions.createNewFileFromThumbnail(file.index);
            let success = await ViewerTools.saveFileWithAnnotations(fileObj, fileData, true, dispatch,  trashedDoc );
                
                    // let saveAnnotation = await AnnotationActions.saveAnnotations(annotationObj,true);
                    // if(!!success){
                        await PDFThumbnails.removePages([file.index])
                        await DocumentActions.getTrashedDocuments(dispatch)
                        dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true })
                    // }
    }
}


const getDocswithfailedFiles = async() => {
  let foundFirstFileDoc: any = null;
  let foundFirstFile: any = null;

  
  let docs:any = await DocumentActions.getDocumentItems(dispatch)
  
      let uploadFailedFiles:DocumentFile[] = uploadFailedDocs.length? uploadFailedDocs : failedDocs;
  
      let failedFiles:DocumentFile[] = []
      if(uploadFailedFiles && uploadFailedFiles.length > 0){
          
         failedFiles= uploadFailedDocs.length? uploadFailedFiles.concat(failedDocs): uploadFailedFiles
        failedFiles = failedFiles.filter((file)=> file.id !== retryFile?.id)

        
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

  return (
    <div id="c-DocHeader" className="c-DocHeader">
      <div className="c-DocHeader-wrap">
        <div className="h-title">
          <h2>Doc Manager</h2>
        </div>
        <div className="dh-actions" ref={refTrashOverlay}>
          <ul>
            {/* <li className="active reassignlink">
                            <div className="dh-actions-lbl-wrap">
                                <div className="dm-h-icon"><ReassignIcon /></div>
                                <div className="dm-h-lbl">
                                    <span>Reassign</span>
                                    <div className="lbl-itemSelected"><span>1</span> File Select</div>
                                </div>
                            </div>
                        </li> */}
            <li onClick={handleClickTrash} className={showTrashOverlay?'active':''}>
            {/* <div
                  onDragOver={(e) => e.preventDefault()}
                  onDrop={(e: any) => {
                      onDrophandler(e)
                  }}
                  className={isDragging ? "drag-wrap" : ""}> */}
              <div
                    onDragOver={(e) => e.preventDefault()}
                    onDrop={(e: any) => {
                        onDrophandler(e)
                    }}
                    className={isDragging ? "dh-actions-lbl-wrap drag-wrap" : "dh-actions-lbl-wrap"}
              // className="dh-actions-lbl-wrap"
              >

                <div className="dm-h-icon">
                  {trashedDoc && trashedDoc.length > 0 ? (<TrashIcon />) : (<EmptyTrashIcon />)}
                </div>
                <div className="dm-h-lbl">
                  
                  
                  <span>Trash Bin</span>
                  </div>
                </div>
              {/* </div> */}
            </li>
            {/* <li>
                            <div className="dh-actions-lbl-wrap">
                                <div className="dm-h-icon"><AddDocIcon /></div>
                                <div className="dm-h-lbl">
                                    <span>Add Document</span>
                                </div>
                            </div>
                        </li> */}
            <AddDocument
              addDocumentToList={addDocumentToList}
              setLoaderVisible={() => { }}
              popoverplacement="right-start"
            />
          </ul>

          <Overlay
            show={showTrashOverlay}
            target={refTrashOverlay.current}
            //placement="bottom"
            container={refTrashOverlay.current}
            containerPadding={20}
            onHide={TrashOverlay}
            rootClose={true}
            placement="bottom-end">
            <Popover id="popover-contained" className={`TrashOverlay ${trashedDoc && trashedDoc.length > 0 ? "" : "empty"}`}>
              <Popover.Title as="h3">Document</Popover.Title>
              <Popover.Content>
                {trashedDoc && trashedDoc.length > 0 ? (
                <div className="trashBin-listWrap">
                  <ol className="dm-dt-docList">
                    {trashedDoc &&
                      trashedDoc.length > 0 &&
                      trashedDoc.map((doc: any, i:number) => {
                        return (
                          <li key={i}  title={doc.mcuName}>
                            <div className="l-icon">
                              <FileIcon />
                            </div>
                            <div className="d-name">
                              <p title={DocumentActions.getFileName(doc)}>{DocumentActions.getFileName(doc)}</p>
                              {doc.file && doc.uploadProgress <= 100?null:
                              <div className="modify-info">
                                <span className="mb-lbl">{doc.fileModifiedOn ? "Modified By:" :"Uploaded By:"}</span>{" "}
                                <span className="mb-name">
                                  {" "}
                                  {doc.userName
                                    ? doc.userName
                                    : "Borrower"}{" "}
                                  {getFileDate(doc)}
                                </span>
                              </div>}
                            </div>
                            <div className="dl-actions">
                              <ul>
                                <li>
                                  <a
                                    data-title="Restore"
                                    onClick={() =>
                                      moveTrashFileToWorkbench(doc)
                                    }>
                                    <PutBackIcon />
                                  </a>
                                </li>
                              </ul>
                            </div>

                            {doc.file && doc.uploadProgress > 0 &&  (
                              <div
                                data-testid="upload-progress-bar"
                                className="progress-upload"
                                style={{ width: doc.uploadProgress + "%" }}
                              ></div>
                            )}
                          </li>
                        );
                      })}
                    {/* <li>
                                            <div className="l-icon"><FileIcon /></div>
                                            <div className="d-name"><p>Bank-statement-Jan-to-Mar-2020-1.jpg</p>
                                                <div className="modify-info">
                                                    <span className="mb-lbl">Modified By:</span> <span className="mb-name"> Angela W Mercado on Jul 4, 2020 01:30 PM</span>
                                                </div>
                                            </div>
                                            <div className="dl-actions">
                                                <ul>
                                                    <li><a data-title="Put Back"><PutBackIcon /></a></li>
                                                </ul>
                                            </div>
                                        </li>
                                        <li>
                                            <div className="l-icon"><FileIcon /></div>
                                            <div className="d-name"><p>Bank-statement-Jan-to-Mar-2020-1.jpg</p>
                                                <div className="modify-info">
                                                    <span className="mb-lbl">Modified By:</span> <span className="mb-name"> Angela W Mercado on Jul 4, 2020 01:30 PM</span>
                                                </div>
                                            </div>
                                            <div className="dl-actions">
                                                <ul>
                                                    <li><a data-title="Put Back"><PutBackIcon /></a></li>
                                                </ul>
                                            </div>
                                        </li> */}
                  </ol>
                </div>
                ):(
                    <div className="emptyList">
                      <p>Trash Bin is empty.</p>
                    </div>
                )}
              </Popover.Content>
            </Popover>
          </Overlay>
        </div>
      </div>
    </div>
  );
};
