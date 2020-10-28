import React, { useState, useEffect, useContext, useRef } from "react";

import { DocumentItem } from "./DocumentItem/DocumentItem";
import { DocumentView } from "rainsoft-rc";
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

interface SelectedDocumentsType {
  addMore: Function;
  setFileInput: Function;
  setFileLimitError: Function;
  fileLimitError: { value: boolean };
  setCurrentInview?: any
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
  setCurrentInview
}: // filesChange,
  SelectedDocumentsType) => {
  const [currentDoc, setCurrentDoc] = useState<ViewDocumentType | null>(null);
  const [btnDisabled, setBtnDisabled] = useState<boolean>(true);
  const [subBtnPressed, setSubBtnPressed] = useState<boolean>(false);
  const [doneVisible, setDoneVisible] = useState<boolean>(false);
  const [doneHit, setDoneHit] = useState<boolean>(false);
  const [uploadingFiles, setUploadingFiles] = useState<boolean>(false);
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

  const location = useLocation();
  const history = useHistory();

  useEffect(() => {
    setFileInput(inputRef.current);
    disableSubmitBtn();

    let curentFileIndex = pendingDocs.findIndex(
      (pd: DocumentRequest) => pd?.docId === currentSelected?.docId
    );
    setCurrentDocIndex(curentFileIndex);
  }, [selectedFiles, selectedFiles.length, currentSelected]);

  useEffect(() => {
    if (selectedFiles.length < ApplicationEnv.MaxDocumentCount) {
      setFileLimitError({ value: false });
    }
    if (!uploadingFiles) {
      hasSubmitted();
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
    history.push(`${location.pathname}/${clientName}/view`);
  };

  const getSubmittedDocumentForView = async (id, requestId, docId, fileId) => {
    const response = await DocumentActions.getSubmittedDocumentForView({
      id,
      requestId,
      docId,
      fileId,
    });
    setBlobData(response);
  };
  const clearBlob = () => {
    DocumentActions.documentViewCancelToken.cancel();
    setBlobData(null);
  };

  const uploadFiles = async () => {
    setSubBtnPressed(true);
    setUploadingFiles(true);
    for (const file of selectedFiles) {
      if (file.file && file.uploadStatus !== "done" && !file.notAllowed) {
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
        // f.clientName = `${newName}.${Rename.getExt(file.file)}`;
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
      // debugger
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

  const disableSubmitBtn = () => {
    let docFiles = selectedFiles.filter((df) => df.uploadStatus === "pending");
    let docEdits = selectedFiles.filter((de) => de.editName);
    let docDelete = selectedFiles.filter((dd) => dd.deleteBoxVisible);

    if (docFiles.length > 0) {
      setBtnDisabled(false);
    } else {
      setBtnDisabled(true);
    }
    if (docEdits.length > 0) {
      setBtnDisabled(true);
    } else if (doneVisible) {
      setBtnDisabled(true);
    } else if (docDelete.length > 0) {
      setBtnDisabled(true);
    } else {
      setBtnDisabled(false);
    }
  };

  const fetchUploadedDocuments = async () => {
    let uploadedDocs = await DocumentActions.getSubmittedDocuments(
      Auth.getLoanAppliationId()
    );
    if (uploadedDocs) {
      dispatch({
        type: DocumentsActionType.FetchSubmittedDocs,
        payload: uploadedDocs,
      });
    }
  };

  const doneDoc = async () => {
    setDoneVisible(false);
    setDoneHit(true);
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
      setDoneVisible(false);
      setDoneHit(false);
      await fetchUploadedDocuments();
    }
    if (isMobile?.value && pendingDocs.length === 1) {
      setCurrentInview('documetsRequired');
    }
  };

  const hasSubmitted = () => {
    let pending = selectedFiles.filter((f) => f.uploadStatus === "pending");
    if (pending.length > 0) {
      setDoneVisible(false);
      return;
    } else {
      setDoneVisible(true);
    }
  };

  const checkFocus = (file: Document, index: number) => {
    let foundIndx = selectedFiles.filter(
      (f: Document) => f.uploadStatus === "done" || f.editName === false
    ).length;
    return foundIndx === index;
  };

  const checkFreezBody = async () => {
    
    let page:any = document.querySelector("html");
    if (document.body.style.overflow == "hidden" ) {
      document.body.removeAttribute("style");
    } else {
      document.body.style.overflow = "hidden";
    }
    if (page.style.overflow == "hidden" ) {
      page.removeAttribute("style");
    } else {
      page.style.overflow = "hidden"
    }

    
  };


  // useEffect(() => {
  //   if (currentSelected?.isRejected === true && !currentSelected?.resubmittedNewFiles) {
  //     setDoneVisible(false);
  //     setBtnDisabled(true);
  //   }
  // }, [currentSelected?.docName, currentSelected?.isRejected === true && !selectedFiles.filter((df) => df.uploadStatus === "pending").length])

  return (
    <section className="file-drop-box-wrap">
      <div className="file-drop-box havefooter">
        <div className="list-selected-doc">
          <ul className="doc-list-ul">
            {selectedFiles.map((f, index) => {
              return (
                <DocumentItem
                  key={f.clientName + index}
                  toggleFocus={toggleFocus}
                  handleDelete={handleDeleteAction}
                  disableSubmitButton={setBtnDisabled}
                  fileAlreadyExists={fileAlreadyExists}
                  retry={(fileToRemove) => addMore(fileToRemove)}
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
            {selectedFiles.length < ApplicationEnv.MaxDocumentCount ? (
              <a
                data-testid="add-more-btn"
                className="addmoreDoc"
                onClick={(e) => {
                  addMore(e);
                }}
              >
                {" "}
                Add more files
                <input
                  onChange={(e) => addMore(e)}
                  data-testid="file-input"
                  type="file"
                  accept={FileUpload.allowedExtensions}
                  id="inputFile"
                  ref={inputRef}
                  multiple
                  style={{ display: "none" }}
                />
              </a>
            ) : (
              null
                // <a className="addmoreDoc disabled">
                //   {" "}
                // Add more files
                //   <input
                //     type="file"
                //     accept={FileUpload.allowedExtensions}
                //     id="inputFile"
                //     ref={inputRef}
                //     multiple
                //     style={{ display: "none" }}
                //   />
                // </a>
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
        {!!currentDoc && location.pathname.includes("view") && (
          <DocumentView
            hideViewer={() => {
              setCurrentDoc(null);
              document.body.style.overflow = "visible";
              document.body.removeAttribute("style");
              let page:any = document.querySelector("html");
                page.style.overflow = "visible";
                page.removeAttribute("style");
              history.goBack();
            }}
            {...currentDoc}
            blobData={blobData}
            submittedDocumentCallBack={getSubmittedDocumentForView}
            isMobile={isMobile}
          />
        )}
      </div>
      <div className="doc-upload-footer">
        {doneVisible ? (
          <div className="doc-confirm-wrap">
            <div className="row">
{!isMobile?.value && 
              <div className="col-xs-12 col-md-6 col-lg-7">
                <div className="dc-text">
                  {/* {docTitle} */}
                  <p>
                    You won't be able to come back to this once you're done.
                  </p>
                </div>
              </div>
            }
              <div className="col-xs-12 col-md-6 col-lg-5">
                <div className="dc-actions">
                  <button
                    className="btn btn-small btn-secondary"
                    onClick={() => {
                      setDoneVisible(false);
                      disableSubmitBtn();
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
                    {"I'M Done"}
                  </button>
                </div>
              </div>
              {isMobile?.value && 
              <div className="col-xs-12 col-md-6 col-lg-7">
                <div className="dc-text">
                  <p>
                    You won't be able to come back to this once you're done.
                  </p>
                </div>
              </div>
            }
            </div>
          </div>
        ) : (
            <div className="doc-submit-wrap">
              {!doneHit && (
                <button
                  disabled={btnDisabled || subBtnPressed}
                  className="btn btn-primary"
                  onClick={uploadFiles}
                >
                  Submit
                </button>
              )}
            </div>
          )}
      </div>
    </section>
  );
};
