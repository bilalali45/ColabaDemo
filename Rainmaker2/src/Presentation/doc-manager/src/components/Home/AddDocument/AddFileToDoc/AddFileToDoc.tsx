import React, {
  useContext,
  useEffect,
  useState,
  useRef,
  ChangeEvent,
} from "react";
import { DocumentFile } from "../../../../Models/DocumentFile";
import DocumentActions from "../../../../Store/actions/DocumentActions";
import { ViewerActions } from "../../../../Store/actions/ViewerActions";
import { DocumentActionsType } from "../../../../Store/reducers/documentsReducer";
import { ViewerActionsType } from "../../../../Store/reducers/ViewerReducer";
import { Store } from "../../../../Store/Store";
import { FileUpload } from "../../../../Utilities/helpers/FileUpload";
import { DocumentItem } from "../../DocumentsContainer/DocumentTableView/DocumentsTable/DocumentItem/DocumentItem";


type AddFileToDocType = {
  selectedDocTypeId: any;
  showFileDialog: boolean;
  setVisible:Function;
  setAddFileDialog:Function;
  retryFile:any;
  selectedDocName:string
};


export const AddFileToDoc = ({selectedDocTypeId, showFileDialog, setVisible, setAddFileDialog, retryFile, selectedDocName}: AddFileToDocType) => {
  const refReassignDropdown = useRef<HTMLDivElement>(null);
  const { state, dispatch } = useContext(Store);
  const inputRef = useRef<HTMLInputElement>(null);
  
  const [failedDocs, setFailedDocs] = useState<DocumentFile[]>([]);
  
  const {
    currentDoc,
    documentItems,
    uploadFailedDocs,
    fileUploadInProgress,
    importedFileIds,

  }: any = state.documents;
  const {
    currentFile,
  }: any = state.viewer;


  useEffect(()=>{
    if(showFileDialog)
    inputRef.current.click();
  },[showFileDialog])


  const handleChange = async (e: ChangeEvent<HTMLInputElement>) => {
    let target = e.target;
    setVisible(false)
    let selectedDoc:any = []
    if(selectedDocTypeId.length && selectedDocTypeId.length){
      selectedDoc = documentItems.filter((doc:any)=> doc.typeId === selectedDocTypeId)
    }
    else{
      selectedDoc = documentItems.filter((doc:any)=> doc.docName === selectedDocName)
    }
    
    await addFiles(e.target.files, selectedDoc[0]).then(() => {
      target.value = '';
      setAddFileDialog(false)
    });
  };

  const handleClick = (e:any) =>{
  
    // document.body.onfocus = handleBlur
      e.target.value = ""
  }
  const handleBlur = (e:any) =>{
    
    
    // document.body.onfocus = null
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
  return (
    <div className="add-files-toCat" style={{ display: "none" }}>
      <input
        data-testid="file-input"
        ref={inputRef}
        type="file"
        name="file"
        id="inputFile"
        onClick={(e)=> handleClick(e)}
        onBlur={(e)=> handleBlur(e)}
        onChange={(e) => handleChange(e)}
        multiple
        accept={FileUpload.allowedExtensions}
      />
     </div>
  );
};
