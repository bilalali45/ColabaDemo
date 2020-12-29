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


export const DocumentsTable = () => {
    const refReassignDropdown = useRef<HTMLDivElement>(null);
    const { state, dispatch } = useContext(Store);

    const documents: any = state.documents;
    const catScrollFreeze: any = documents?.catScrollFreeze;
    const { currentDoc, documentItems, uploadFailedDocs, fileUploadInProgress }: any = state.documents;
    const {isLoading, selectedFileData, currentFile}:any  = state.viewer;
    const [fileClicked, setFileClicked]= useState<boolean>(false);
    const [retryFile, setRetryFile] = useState<any>();
    const [failedDocs, setFailedDocs] = useState<DocumentFile[]>([]);
    const [selectedDoc, setSelectedDoc] = useState<DocumentRequest>();
    const [popshow, popsetShow] = useState(false);
    const [isDocumentSelected, setIsDocumentSelected] = useState<boolean>(false);
    const [poptarget, popsetTarget] = useState(null);
    const docTypePopUp = useRef(null);
    const popUpDv = useRef<any>(null);
    const refAddFileLink = useRef<any>(null);


    useEffect(() => {
      window.addEventListener("mousedown", handleClickOutside);
      return () => {
          window.removeEventListener("mousedown", handleClickOutside);
      };
  },[popshow] );

  
    const handlePopClick = (event) => {
        popsetShow(!popshow);
        popsetTarget(event.target);
    };

    const handleClickOutside = (event:any) => {
        
        if (popshow && popUpDv && !popUpDv.current?.contains(event.target) && refAddFileLink && !refAddFileLink.current?.contains(event.target)) {
            popsetShow(!popshow);
        }
    }
    
    const inputRef = useRef<HTMLInputElement>(null);
    
    const [openedReassignDropdown,setOpenReassignDropdown] = useState<boolean>(false);

    useEffect(() => {
        if (!documentItems) {
            DocumentActions.getCurrentDocumentItems(dispatch, true);
            checkIsByteProAuto()
        }
    }, [!documentItems]);

    const checkIsByteProAuto = async () => {
        let res: any = await DocumentActions.checkIsByteProAuto();
        let isAuto = res?.syncToBytePro != 2 ? true : false;
        dispatch({type: DocumentActionsType.SetIsByteProAuto, payload: isAuto});
      };

      const handleChange = async (e: ChangeEvent<HTMLInputElement>) => {
        let target = e.target;
        await addFiles(e.target.files, selectedDoc).then(() => {
          target.value = '';
        });
      };

    //   const removeFile = () => {
    //     let files = retryFile?.document?.files.filter((docFile: any) => docFile.id !== retryFile?.file?.id)

    //     let docItems = documentItems.map((doc: any) => {
    //         if (doc.docId === retryFile?.document?.docId) {
    //             doc.files = files
    //         }
    //         return doc
    //     })
    //     dispatch({ type: DocumentActionsType.SetDocumentItems, payload: docItems });
        
    // }

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
        let d = await DocumentActions.getDocumentItems(dispatch)
        return d;
    
      }
      const addFiles = async (selectedFiles: FileList, document:any) => {
    
        if (document) {
          if (selectedFiles) {
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

      const selectDocTypeClick=(doc:DocumentRequest)=>{
        setSelectedDoc(doc);
        inputRef.current.click();
          popsetShow(!popshow);
      }

    
    return (
        <div id="c-DocTable" className="dm-docTable c-DocTable" >

            <div className="dm-dt-thead">
                <div className="dm-dt-thead-left">Document</div>
                <div className="dm-dt-thead-right">Status</div>
            </div>

            <div className={`dm-dt-tbody ${catScrollFreeze?" freeze":""}`} ref={refReassignDropdown}>

                {
                    documentItems && documentItems.length ?( documentItems?.map((d: any, i: number) => {
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
                            />
                        )
                    })
                    ):null
                }

            </div>
            {!fileUploadInProgress && 
                        <div className="dm-dt-foot" ref={popUpDv}>
                            {/*<button>Add Files</button>*/}
                            {/*<OverlayTrigger trigger="click" placement="right-end" overlay={popover} rootClose={true}>
                                <a>Add Files +</a>
                            </OverlayTrigger>*/}
                            <a ref={refAddFileLink} onClick={handlePopClick} className="addFile">Add Files +</a>
                            <div >
                            <Overlay placement="right-end" target={poptarget} show={popshow} rootClose={true}>
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
                                        {/* <Popover.Title as="div" bsPrefix="popover-footer">
                                            <div className="dh-actions-lbl-wrap">
                                                <div className="dm-h-icon"><AddDocIcon /></div>
                                                <div className="dm-h-lbl">
                                                    <span>Add Document</span>
                                                </div>
                                            </div>
                                        </Popover.Title> */}
                                    </div>
                                </Popover>
                            </Overlay>
                            </div>
                            <div className="add-files-toCat" style={{display:"none"}} >

                                                    <input
                                                        data-testid="file-input"
                                                        ref={inputRef}
                                                        type="file"
                                                        name="file"
                                                        id="inputFile"
                                                        onChange={(e) => handleChange(e)}
                                                        multiple
                                                        accept={FileUpload.allowedExtensions}
                                                    />

                                                </div>
                        </div>
            }
            
        </div>
    )
}
