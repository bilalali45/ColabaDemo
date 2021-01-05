import React, { useState, useContext, useRef, Fragment } from "react";
import {
  DocumentDropBox,
  FileDropper,
} from "../../../../shared/Components/DocumentDropBox/DocumentDropBox";
import { SelectedDocuments } from "./SelectedDocuments/SelectedDocuments";
import { Store } from "../../../../store/store";
import { Document } from "../../../../entities/Models/Document";
import { DocumentUploadActions } from "../../../../store/actions/DocumentUploadActions";
import { AlertBox } from "../../../../shared/Components/AlertBox/AlertBox";
type DocumentUploadType = {
  setCurrentInview?: any
}
 
export const DocumentUpload = ({setCurrentInview} : DocumentUploadType) => {
  const [fileInput, setFileInput] = useState<HTMLInputElement>();
  const [fileLimitError, setFileLimitError] = useState({ value: false });
  const [showAlert, setshowAlert] = useState<boolean>(false);
  const { state, dispatch } = useContext(Store);
  const { currentDoc }: any = state.documents;
  const selectedfiles: Document[] = currentDoc?.files || null;
  let docTitle = currentDoc ? currentDoc.docName : "";
  const documents: any = state.documents;
  const submitBtnPressed: any = documents.submitBtnPressed;
  let docMessage = currentDoc?.docMessage
    ? currentDoc.docMessage
    : "Please upload the following documents.";
  const parentRef = useRef<HTMLDivElement>(null);
  const getFileInput = (fileInputEl: HTMLInputElement) => {
    setFileInput(fileInputEl);
  };

  const setFiles = (files, fileToRemnove) => {
    if (files) {
      let updatedFiles = selectedfiles.filter((sf) => sf !== fileToRemnove);
      DocumentUploadActions.updateFiles(
        files,
        updatedFiles,
        dispatch,
        setFileLimitError,
        'WithoutReq'
      );
    }
  }

  const showFileExplorer = (fileToRemnove: Document | null = null) => {
    let files =
      selectedfiles.filter(
        (f) => f.uploadProgress > 0 && f.uploadStatus === "pending"
      ).length > 0;
    if (files) {
      setshowAlert(true);
      return;
    }
    if (fileInput?.value) {
      fileInput.value = "";
    }
    if (fileInput) {
      fileInput.click();
    }
  };
  
  const renderDroperBox = () => {
    return(
      <>
       <FileDropper
        parent={parentRef.current}
        getDroppedFiles={(files) =>
          DocumentUploadActions.updateFiles(
            files,
            selectedfiles,
            dispatch,
            setFileLimitError,
            'WithoutReq'
          )
        }
      >     
        <div className="popup-doc-upload--content-area-body">
        {currentDoc && (
          <Fragment>
            {!selectedfiles?.length ? (
              <DocumentDropBox
                getFiles={(files) =>
                  DocumentUploadActions.updateFiles(
                    files,
                    selectedfiles,
                    dispatch,
                    setFileLimitError,
                    'WithoutReq'
                  )
                }
                setFileInput={getFileInput}
              />
            ) : (
              <>
                <SelectedDocuments
                  setFiles={setFiles}
                  fileLimitError={fileLimitError}
                  setFileLimitError={setFileLimitError}
                  addMore={showFileExplorer}
                  setFileInput={getFileInput}
                  setCurrentInview={setCurrentInview}
                />
              </>
            )}
          </Fragment>
        )}
        </div>
      </FileDropper>
      </>
    )
  }
  
  const renderWithoutDropper = () => {
    return(
      <>      
        <div className="popup-doc-upload--content-area-body">
        {currentDoc && (
          <Fragment>
            {!selectedfiles?.length ? (
              <DocumentDropBox
                getFiles={(files) =>
                  DocumentUploadActions.updateFiles(
                    files,
                    selectedfiles,
                    dispatch,
                    setFileLimitError,
                    'WithoutReq'
                  )
                }
                setFileInput={getFileInput}
              />
            ) : (
              <>
                <SelectedDocuments
                  setFiles={setFiles}
                  fileLimitError={fileLimitError}
                  setFileLimitError={setFileLimitError}
                  addMore={showFileExplorer}
                  setFileInput={getFileInput}
                  setCurrentInview={setCurrentInview}
                />
              </>
            )}
          </Fragment>
        )}
        </div>
     
      </>
    )
  }
  
  return (
    <>
    {currentDoc && (<>
           <header className={`popup-doc-upload--content-area-header`}>
           <h2 className="h2" title={docTitle}><span className="text-ellipsis">{docTitle}</span>
           {currentDoc?.isRejected && (
                <span className="Doc-head-wrap--alert">CHANGES REQUESTED</span>
              )}
          </h2>
          {/* <div className="doc-note">
              <p>
                <i className="fas fa-info-circle"></i>
                {docMessage?.replace(/<br\s*[\/]?>/gi, "\n")}
              </p>
            </div> */}
         </header>
          {/* <div className="Doc-head-wrap">
            <h2 title={docTitle}>
              <span data-testid="selected-doc-title" className="text-ellipsis">
                {docTitle}
              </span>
              {currentDoc?.isRejected && (
                <span className="Doc-head-wrap--alert">CHANGES REQUESTED</span>
              )}
            </h2>
            
          </div> */}
          </>
        )}
    <section  data-testid="document-dropper" className={`Doc-upload ${!selectedfiles?.length ?"pageEmptyUpload":"pageHaveDocUpload" }`} ref={parentRef}>
    { !submitBtnPressed 
      ? 
      renderDroperBox()
      :
      renderWithoutDropper()
    }
   
      
      
      {showAlert && <AlertBox hideAlert={() => setshowAlert(false)} />}
    </section>
   </>
  );
};
