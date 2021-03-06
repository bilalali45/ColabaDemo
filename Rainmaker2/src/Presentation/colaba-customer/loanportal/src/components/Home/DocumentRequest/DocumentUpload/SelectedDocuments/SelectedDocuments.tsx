import React, { useState, useEffect, useContext, useRef } from "react";

import { DocumentItem } from "./DocumentItem/DocumentItem";
import _ from "lodash";
import { Store } from "../../../../../store/store";
import { Document } from "../../../../../entities/Models/Document";
import { DocumentActions } from "../../../../../store/actions/DocumentActions";
import { DocumentsActionType } from "../../../../../store/reducers/documentReducer";
import { Auth } from "../../../../../services/auth/Auth";
import { DocumentRequest } from "../../../../../entities/Models/DocumentRequest";
import { DocumentUploadActions } from "../../../../../store/actions/DocumentUploadActions";
import { FileUpload } from "../../../../../utils/helpers/FileUpload";
import { ApplicationEnv } from "../../../../../utils/helpers/AppEnv";
import { useLocation, useHistory } from "react-router-dom";
import cameraIcon from "../../../../../assets/images/camera-icon.svg";
import folderIcon from "../../../../../assets/images/folder-icon.svg";
import { PSPDFKitWebViewer } from "../../../../../shared/Components/PSPDFKit/PSPDFKitWebViewer";
import { DocumentView } from "../../../../../shared/Components/DocumentView/DocumentView";
import { render } from "@testing-library/react";
import {SVGUploadCameraIcon, SVGUploadFolderIcon} from "../../../../../shared/Components/SVGs";

interface SelectedDocumentsType {
  addMore: Function;
  setFileInput: Function;
  setFileLimitError: Function;
  fileLimitError: { value: boolean };
  setCurrentInview?: any;
  setFiles: Function
  // filesChange: Function;
}

interface ViewDocumentType {
  id: string;
  requestId: string;
  docId: string;
  clientName?: string;
  fileId?: string;
  file?: any;

}

export const SelectedDocuments = ({
  addMore,
  setFileInput,
  fileLimitError,
  setFileLimitError,
  setCurrentInview,
  setFiles
}: // filesChange,
  SelectedDocumentsType) => {
  const [currentDoc, setCurrentDoc] = useState<ViewDocumentType | null>(null);
  const [subBtnPressed, setSubBtnPressed] = useState<boolean>(false);
  const [uploadingFiles, setUploadingFiles] = useState<boolean>(false);
  const [donePressed, setDonePressed] = useState<boolean>(false);
  const { state, dispatch } = useContext(Store);
  const loan: any = state.loan;
  const { isMobile } = loan;
  const [currentDocIndex, setCurrentDocIndex] = useState<number>(0);

  const documents: any = state.documents;
  const pendingDocs: any = documents.pendingDocs;
  const currentSelected: any = documents.currentDoc;
  const selectedFiles = currentSelected.files || [];
  const [blobData, setBlobData] = useState<any | null>();
  const inputRef = useRef<HTMLInputElement>(null);
  const inputRef2 = useRef<HTMLInputElement>(null);


  const [fileToRemove, setFileToRemove] = useState<any | null>();
  const location = useLocation();
  const history = useHistory();

 
  useEffect(() => {
    setFileInput(inputRef.current);

    let curentFileIndex = pendingDocs.findIndex(
      (pd: DocumentRequest) => pd?.docId === currentSelected?.docId
    );
    setCurrentDocIndex(curentFileIndex);
  }, [selectedFiles, selectedFiles.length, currentSelected]);

  useEffect(() => {
    if (selectedFiles.length < ApplicationEnv.MaxDocumentCount) {
      setFileLimitError({ value: false });
    }

  }, [selectedFiles, selectedFiles.length, uploadingFiles]);

  const handleDeleteAction = (file) => {
    let updatedFiles = selectedFiles.map((f: Document) => {
      if (file.file && f.clientName === file.clientName) {
        f.deleteBoxVisible = !f.deleteBoxVisible;
        return f;
      }

      return f;
    });
   
    dispatch({ type: DocumentsActionType.AddFileToDoc, payload: updatedFiles });
  };

  const viewDocument = (document: any) => {
    checkFreezBody();
    clearBlob();
    const {
      currentDoc: { id, requestId, docId },
    }: any = state.documents;
    const { id: fileId, clientName } = document;

    setCurrentDoc({
      id,
      requestId,
      docId,
      fileId,
      clientName,
      file: document.file,
    });
    if (!document.file) {
      getSubmittedDocumentForView(id, requestId, docId, fileId);
    } else {
      let reader = new FileReader();
      reader.onload = (event: any) => {
        let dataUrl = event.target.result;
        setBlobData({
          data: dataUrl
        });
      };
      reader.readAsDataURL(document.file);
    }
    history.push(`${location.pathname}/${clientName}/view`);
  };

  const getSubmittedDocumentForView = async (id, requestId, docId, fileId) => {
    try {
      const response = await DocumentActions.getSubmittedDocumentForView({
        id,
        requestId,
        docId,
        fileId,
      });
      setBlobData(response);
    } catch (error) {
      if (location.pathname.includes('/view')) {
        history.back();
      }
    }
  };
  const clearBlob = () => {
    DocumentActions.documentViewCancelToken.cancel();
    setBlobData(null);
  };

  const uploadFiles = async () => {
    setSubBtnPressed(true);
    setUploadingFiles(true);
    for (const file of selectedFiles) {
      if (file.file && file.uploadStatus !== "done" && !file.notAllowed && file.uploadStatus !== 'failed') {
        try {
          await DocumentUploadActions.submitDocuments(
            currentSelected,
            file,
            dispatch,
            Auth.getLoanAppliationId()
          );
        } catch (error) {
          file.uploadStatus = "failed";
          console.log("error during file submit", error);
          console.log("error during file submit", error.response);
        }
      }
    }
    setSubBtnPressed(false);
    setUploadingFiles(false);
    try {
      Promise.resolve(fetchUploadedDocuments());
    } catch (error) { }
  };

  const fileAlreadyExists = (file, newName) => {
    var alreadyExist = selectedFiles.find(
      (f) =>
        f !== file &&
        FileUpload.removeDefaultExt(f.clientName).toLowerCase() ===
        newName.toLowerCase()
    );
    if (alreadyExist) {
      return true;
    }
    return false;
  };

  const changeName = (file: Document, newName: string) => {
    let name = newName.replace(/\s+/g, " ");

    if (fileAlreadyExists(file, name)) {
      return false;
    }
    let updatedFiles = selectedFiles.map((f: Document) => {
      if (file.file && f.clientName === file.clientName) {
        if (name.trim()) {
          f.clientName = `${name}.${FileUpload.getExtension(file, "dot")}`;
        }
        f.editName = !f.editName;
        return f;
      }
      return f;
    });
    
    dispatch({ type: DocumentsActionType.AddFileToDoc, payload: updatedFiles });
  };

  const toggleFocus = (
    file: Document,
    focus: boolean,
    shouldMoveFocus?: boolean
  ) => {
    let nextInd = 0;
    let updatedFiles = selectedFiles.map((f: Document, i: number) => {
      if (file.file && f.clientName === file.clientName) {
        f.focused = focus;
        nextInd = i + 1;
      }
      return f;
    });
    let updatedFilesWithFocus = updatedFiles.map((f: Document, i: number) => {
      if (i === nextInd) {
        f.focused = true;
      }
      return f;
    });

    dispatch({
      type: DocumentsActionType.AddFileToDoc,
      payload: updatedFilesWithFocus,
    });
  };

  const deleteDoc = (fileName: string) => {
    DocumentUploadActions.removeActualFile(fileName, selectedFiles, dispatch);
    let updatedFiles = selectedFiles.filter((f: Document) => {
      if (f?.clientName !== fileName) {
        return f;
      }
    });

    dispatch({ type: DocumentsActionType.AddFileToDoc, payload: updatedFiles });
  };

  const fetchUploadedDocuments = async () => {
    let uploadedDocs = await DocumentActions.getSubmittedDocuments(
      Auth.getLoanAppliationId()
    );
    if (uploadedDocs) {
      const sortedUploadedDocuments = _.orderBy(uploadedDocs, (item) => item.docName, ["asc",]);
      dispatch({
        type: DocumentsActionType.FetchSubmittedDocs,
        payload: sortedUploadedDocuments,
      });
    }
  };

  const doneDoc = async () => {
    setDonePressed(true);
    let fields = ["id", "requestId", "docId"];
    let data = {};

    if (currentSelected) {
      for (const field of fields) {
        data[field] = currentSelected[field];
      }
      let docs:
        | DocumentRequest[]
        | undefined = await DocumentActions.finishDocument(
          Auth.getLoanAppliationId(),
          data
        );
      if (docs?.length) {
        let indForCurrentDoc = currentDocIndex;
        if (currentDocIndex === pendingDocs.length - 1) {
          setCurrentDocIndex(0);
          indForCurrentDoc = 0;
        }
        dispatch({ type: DocumentsActionType.FetchPendingDocs, payload: docs });
        dispatch({
          type: DocumentsActionType.SetCurrentDoc,
          payload: docs[indForCurrentDoc],
        });
      } else if (docs?.length === 0) {
        dispatch({ type: DocumentsActionType.FetchPendingDocs, payload: docs });
      }

      await fetchUploadedDocuments();
    }
    if (isMobile?.value && pendingDocs.length === 1) {
      setCurrentInview('documetsRequired');
    }
    setDonePressed(false);
  };

  const checkFocus = (file: Document, index: number) => {
    let foundIndx = selectedFiles.filter(
      (f: Document) => f.uploadStatus === "done" || f.editName === false
    ).length;
    return foundIndx === index;
  };

  const handleSubmitBtnDisabled = () => {
    let newFiles = selectedFiles?.filter(f => f.uploadStatus === 'pending');
    let filesEditing = newFiles?.filter(f => f.editName);

    if (subBtnPressed) {
      return true;
    }

    if (!newFiles.length) {
      return true;
    }

    if (filesEditing.length) {
      return true;
    }
  }
  
  const checkFreezBody = async () => {

    if (document.body.style.overflow == "hidden") {
      document.body.removeAttribute("style");
      document.body.classList.remove("lockbody");
    } else {
      document.body.style.overflow = "hidden";
      document.body.classList.add("lockbody");
    }
  };


  const retryFile = (file: Document) => {
    if (file) {
      setFileToRemove(file);
    }
    addMore(fileToRemove);
  }
  // useEffect(() => {
  //   if (currentSelected?.isRejected === true && !currentSelected?.resubmittedNewFiles) {
  //     setDoneVisible(false);
  //     setBtnDisabled(true);
  //   }
  // }, [currentSelected?.docName, currentSelected?.isRejected === true && !selectedFiles.filter((df) => df.uploadStatus === "pending").length])


  const renderUploadButton = () => {

    if (isMobile?.value) {
      return (
        <div className="upload-btns-wrap">
          <label
            htmlFor="inputFile1"
            data-testid="add-more-btn"
           // className="addmoreDoc camera-wrap"
            className={subBtnPressed ? "addmoreDoc camera-wrap disabled" : "addmoreDoc camera-wrap"}
            onClick={(e) => {
              //setFileInput(inputRef2.current);
              // addMore(e, inputRef2);
              // setFiles(e.target.files, fileToRemove);
            }}
          >
            {/*<span className="iconic-btn-img"><img src={cameraIcon} className="img-responsive" /></span>*/}
            <span className="iconic-btn-img"><SVGUploadCameraIcon/></span>
            <span className="iconic-btn-lbl">   Camera </span>
            <input
              onChange={(e) => {
                setFiles(e.target.files, fileToRemove);
                e.target.value = ''
              }}
              data-testid="file-input"
              type="file"
              accept={'image/*'}
              id="inputFile1"
              ref={inputRef}
              multiple
              style={{ display: "none" }}
              capture="environment"
            />
          </label>

          <label
            htmlFor="inputFile2"
            data-testid="add-more-btn"
            // className="addmoreDoc folder-wrap"
            className={subBtnPressed ? "addmoreDoc folder-wrap disabled" : "addmoreDoc folder-wrap"}
            onClick={(e) => {
              // addMore(e, null);
            }}
          >
            {/*<span className="iconic-btn-img"><img src={folderIcon} className="img-responsive" /></span>*/}
            <span className="iconic-btn-img"><SVGUploadFolderIcon/></span>
            <span className="iconic-btn-lbl">   Folder </span>
            <input
              onChange={(e) => {
                setFiles(e.target.files, fileToRemove);
                e.target.value = ''
              }}
              data-testid="file-input"
              type="file"
              accept={FileUpload.allowedExtensions}
              id="inputFile2"
              ref={inputRef}
              multiple
              style={{ display: "none" }}
            />
          </label>


        </div>
      )
    } else {
      return (
        <a
          data-testid="add-more-btn"
          //className="addmoreDoc"
          className={subBtnPressed ? "addmoreDoc disabled" : "addmoreDoc"}
          onClick={(e) => {
            addMore(e);
          }}
        >
          {" "}
    Add more files
          <input
            onChange={(e) => {            
              setFiles(e.target.files, fileToRemove);
            }}
            data-testid="file-input"
            type="file"
            accept={FileUpload.allowedExtensions}
            id="inputFile"
            ref={inputRef}
            multiple
            style={{ display: "none" }}
          />
        </a>
      )
    }

  }

  return (
    <section data-testid="files-container" className="file-drop-box-wrap">
      <div className="file-drop-box havefooter">
        <div className="list-selected-doc">
          <ul className="doc-list-ul">
            {selectedFiles.map((f, index) => {
              return (
                <DocumentItem
                  key={f.clientName + index}
                  toggleFocus={toggleFocus}
                  handleDelete={handleDeleteAction}
                  fileAlreadyExists={fileAlreadyExists}
                  retry={(fileToRemove) => retryFile(fileToRemove)}
                  file={f}
                  viewDocument={viewDocument}
                  changeName={changeName}
                  deleteDoc={deleteDoc}
                  indexKey={index}
                  shouldFocus={checkFocus(f, index)}
                />
              );
            })}
          </ul>
          <div className="addmore-wrap">
            {selectedFiles.length < ApplicationEnv.MaxDocumentCount ? renderUploadButton() : (
              isMobile?.value ? null
                :
                <a className="addmoreDoc disabled">
                  {" "}
                Add more files
                  <input
                    onChange={(e) => {
                      setFiles(e.target.files, fileToRemove);
                    }}
                    data-testid="more-file-input"
                    type="file"
                    accept={FileUpload.allowedExtensions}
                    id="inputFile"
                    ref={inputRef}
                    multiple
                    style={{ display: "none" }}
                  />
                </a>

            )}

            {!(selectedFiles.length < ApplicationEnv.MaxDocumentCount) ? (
              <p className="text-danger">
                Only {ApplicationEnv.MaxDocumentCount} files can be uploaded per
                document. Please contact us if you'd like to upload more files.
              </p>
            ) : (
                ""
              )}
          </div>
        </div>
      </div>
      <div className="doc-upload-footer">
        {!selectedFiles.filter(f => f.uploadStatus !== 'done').length && !uploadingFiles && !donePressed ? (
          <div className="doc-confirm-wrap">
            <div className="row">
              {/* {!isMobile?.value &&
                <div className="col-xs-12 col-md-6 col-lg-7">
                  <div className="dc-text">
                    <p>
                      You won't be able to come back to this once you're done.
                  </p>
                  </div>
                </div>
              } */}
              <div className="col-xs-12 col-md-7 col-lg-6">
                <div className="dc-actions">
                  <button
                    className="btn btn-small btn-secondary"
                    onClick={() => {

                      if (pendingDocs.length > 1) {
                        let curDocInd = 0;
                        pendingDocs?.forEach((d, i) => {
                          if (d.docId === currentSelected.docId) {
                            curDocInd = i;
                          }
                        });

                        if (curDocInd === pendingDocs?.length - 1) {
                          curDocInd = 0;
                        } else {
                          curDocInd = curDocInd + 1;
                        }

                        dispatch({
                          type: DocumentsActionType.SetCurrentDoc,
                          payload: pendingDocs[curDocInd],
                        });
                      }
                    }}
                  >
                    {"I'LL Come Back"}
                  </button>
                  <button
                    className="btn btn-small btn-primary"
                    onClick={doneDoc}
                  >
                    {"Send for Review"}
                  </button>
                </div>
              </div>
              {/* {isMobile?.value &&
                <div className="col-xs-12 col-md-6 col-lg-7">
                  <div className="dc-text">
                    <p>
                      You won't be able to come back to this once you're done.
                  </p>
                  </div>
                </div>
              } */}
            </div>
          </div>
        ) : (
            <div className="doc-submit-wrap">
              <button
                disabled={handleSubmitBtnDisabled()}
                className="btn btn-primary"
                onClick={uploadFiles}
              >
                Submit
                </button>
            </div>
          )}
      </div>
      {
        currentDoc && location.pathname.includes("view") && (
          <DocumentView
            hideViewer={() => {
              setCurrentDoc(null);
              document.body.style.overflow = "visible";
              document.body.removeAttribute("style");
              document.body.classList.remove("lockbody");
              history.back();
            }}
            {...currentDoc}
            blobData={blobData}
            submittedDocumentCallBack={getSubmittedDocumentForView}
            isMobile={isMobile}
          />
        )

      }
    </section>
  );
};
