import React, { useState, useRef, useContext, useEffect } from "react";
import {
  MinusIcon,
  PlusIcon,
} from "../../../../../../shared/Components/Assets/SVG";
import DocumentActions from "../../../../../../Store/actions/DocumentActions";
import { DocumentRequest } from "../../../../../../Models/DocumentRequest";
import { Document } from "../../../../../../Models/Document";
import { DocumentActionsType } from "../../../../../../Store/reducers/documentsReducer";
import { Store } from "../../../../../../Store/Store";
import { FilesList } from "../FilesList/FilesList";
import { ViewerActions } from "../../../../../../Store/actions/ViewerActions";
import { LocalDB } from "../../../../../../Utilities/LocalDB";
import { DocumentFile } from "../../../../../../Models/DocumentFile";
import { CategoryDocument } from "../../../../../../Models/CategoryDocument";
import { fail } from "assert";
import { ViewerActionsType } from "../../../../../../Store/reducers/ViewerReducer";

type DocumentItemType = {
  // isDragging: boolean
  docInd: number,
  document: DocumentRequest;
  refReassignDropdown: any;
  setFileClicked:Function;
  fileClicked:boolean;
  setOpenReassignDropdown:any;
};

export const DocumentItem = ({
  docInd,
  document,
  refReassignDropdown,
  setFileClicked, 
  fileClicked,
  setOpenReassignDropdown
}: DocumentItemType) => {
  const [isDragging, setIsDragging] = useState<boolean>(false);

  const [show, setShow] = useState(true);

  const [showReassignOverlay, setShowReassign] = useState<boolean>(false);

  const [targetReassign, setTargetReassign] = useState(null);
  const refReassignOverlay = useRef(null);
  const { state, dispatch } = useContext(Store);
  const { currentDoc, documentItems, uploadFailedDocs }: any = state.documents;
  const selectedfiles: Document[] = currentDoc?.files || null;
  let loanApplicationId = LocalDB.getLoanAppliationId();
  const { currentFile }: any = state.viewer;
  const [failedDocs, setFailedDocs] = useState<DocumentFile[]>([]);
  const [retryFile, setRetryFile] = useState<DocumentFile>();
  
  let fileListRef = useRef<HTMLUListElement>(null);



    useEffect(()=>{
      
        if(document && currentDoc && document.docId === currentDoc.docId && !fileClicked){
          fileListRef.current?.scrollIntoView({
            behavior: 'smooth',
            block: 'start',
          });
          setFileClicked(true)
        }
      },[currentDoc])
      
  const handleClick = () => {
    setShow(!show);
  };
  const handleClickReassign = (event: any) => {
    setShowReassign(!showReassignOverlay);
    setTargetReassign(event.target);
  };
  const hideReassignOverlay = () => {
    setShowReassign(false);
  };

  const createNewFileForThePage = (index: number) => {
    // DocumentActions.uploadFile(index);
  };

  const handleSync = () => {
    setShow(!show);
  };

  const deleteDoc = async () =>{

    try {
      await DocumentActions.deleteDocCategory(
        document.id,
        document.requestId,
        document.docId
      );
    } catch (error) {
      // file.uploadStatus = "failed";
      console.log("error during file submit", error);
      console.log("error during file submit", error.response);
    }
    getDocswithfailedFiles()
    
  }

  const getDocswithfailedFiles = async() => {
    let foundFirstFileDoc: any = null;
    let foundFirstFile: any = null;

    let docs:any = await fetchDocuments()
    
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

  const addFiles = async (selectedFiles: FileList) => {
    
    if (document) {
      if (selectedFiles) {
        // DocumentActions.addFilesToDocCategory(document, files)
        dispatch({
          type: DocumentActionsType.SetFileUploadInProgress,
          payload: true,
        });
        for (let index = 0; index < selectedFiles.length; index++) {
          const file = selectedFiles[index];
          if (file) {
            try {
              let d =  new Date();
              let fileId =  d.getDate().toString() + d.getMonth().toString() + d.getFullYear().toString() + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString()+ d.getMilliseconds().toString()
              let res = await DocumentActions.submitDocuments(
                documentItems,
                document,
                fileId,
                file,
                dispatch
              );

              if(res.notAllowed || res.uploadStatus === 'failed'){
                failedDocs.push(res)
                
              }
              // console.log(documentItems)
              // if(documentItems?.files?.length === 1 ){
              //   console.log(documentItems)
              // }
              
            } catch (error) {
              // file.uploadStatus = "failed";
              console.log("error during file submit", error);
              console.log("error during file submit", error.response);
            }
          }
        }
        await getDocswithfailedFiles();
        dispatch({
          type: DocumentActionsType.SetFileUploadInProgress,
          payload: false,
        });
      }
    }
    
    
  };

  const fetchDocuments = async()=>{
    let d = await DocumentActions.getDocumentItems(dispatch)
    return d;

  }
  const CapitalizeText = (text:string ) =>{
if(text){
      var splitStr = text.toLowerCase().split(' ');
      for (var i = 0; i < splitStr.length; i++) {
          splitStr[i] = splitStr[i].charAt(0).toUpperCase() + splitStr[i].substring(1);     
      }
      return splitStr.join(' '); 
    }
  }


  const renderStatus = (status: string) => {
    let cssClass = '';
    switch (status) {
      case 'Pending review':
        cssClass = 'pending';
        break;
      case 'Started':
        cssClass = 'started';
        break;
      case 'Borrower to do':
        cssClass = 'borrower';
        break;
      case 'Completed':
        cssClass = 'completed';
        break;
      case 'In draft':
        cssClass = 'indraft';
        break;
      case 'Manually added':
        cssClass = 'manualyadded';
        break;
      default:
        cssClass = 'pending';
    }
    return cssClass;
  };

  const renderDocumentTile = () => {
    return (
      <div className="dm-dt-tr1">
        <div className="dm-dt-tr1-left">
          <h4 title={document.docName} className="viewed" onClick={handleSync}>
            {show ? <MinusIcon /> : <PlusIcon />} {document.docName}
          </h4>
          {/* {show && <div className="link-hiddenFiles"><a >Hidden (2 files)</a> </div>} */}
        </div>
        <div className="dm-dt-tr1-right d-flex align-items-center">
          <div className={`lbl-status capitalize ${renderStatus(document?.status)}`}>{CapitalizeText(document?.status)}</div>
        {/*</div>*/}
        {/*<div>*/}
          {document.files && document.files.length === 0 ?
            (<button
                data-testid="btn-delete"
                onClick={deleteDoc}
                className="btn btn-delete btn-sm"
              >
                <em className="zmdi zmdi-close"></em>
              </button>): null
          }
        
        </div>
      </div>
    );
  };
  return (
    <section className="dm-dt-tr doc-m-cat-list"
    ref={fileListRef}>
      {renderDocumentTile()}
      {show && (
        <FilesList
          addFiles={(files: any) => addFiles(files)}
          document={document}
          docInd={docInd}
          refReassignDropdown={refReassignDropdown}
          setRetryFile = {setRetryFile}
          setFileClicked ={setFileClicked}
          getDocswithfailedFiles = {getDocswithfailedFiles}
          setOpenReassignDropdown={setOpenReassignDropdown}
        />
      )}
    </section>
  );
};
