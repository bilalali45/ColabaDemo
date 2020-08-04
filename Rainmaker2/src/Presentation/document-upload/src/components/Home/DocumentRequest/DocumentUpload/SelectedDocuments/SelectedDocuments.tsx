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
import { update } from "lodash";
//import { DocumentView } from "../../../../../shared/Components/DocumentView/DocumentView";

interface SelectedDocumentsType {
  addMore: Function;
  setFileInput: Function;
  setFileLimitError: Function;
  fileLimitError: { value: boolean };
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
}: SelectedDocumentsType) => {
  const [currentDoc, setCurrentDoc] = useState<ViewDocumentType | null>(null);
  const [btnDisabled, setBtnDisabled] = useState<boolean>(true);
  const [subBtnPressed, setSubBtnPressed] = useState<boolean>(false);
  const [doneVisible, setDoneVisible] = useState<boolean>(false);
  const [doneHit, setDoneHit] = useState<boolean>(false);
  const { state, dispatch } = useContext(Store);
  const [filesLimitErrorVisible, setFilesLimitErrorVisible] = useState<boolean>(
    false
  );

  const documents: any = state.documents;
  const pendingDocs: any = documents.pendingDocs;
  const currentSelected: any = documents.currentDoc;
  const selectedFiles = currentSelected.files || [];
  const docTitle = currentSelected ? currentSelected.docName : "";
  const [blobData, setBlobData] = useState<any | null>();
  const inputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    setFileInput(inputRef.current);
    disableSubmitBtn();
  }, [selectedFiles, selectedFiles.length, currentSelected]);

  useEffect(() => {
    if (selectedFiles.length < ApplicationEnv.MaxDocumentCount) {
      setFileLimitError({ value: false });
    }
    hasSubmitted();
  }, [selectedFiles, selectedFiles.length]);

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
  };

  const getSubmittedDocumentForView = async (id, requestId, docId, fileId) => {
    const response = (await DocumentActions.getSubmittedDocumentForView({
      id,
      requestId,
      docId,
      fileId,
    })) as any;
    setBlobData(response);
  };
  const clearBlob = () => {
    DocumentActions.documentViewCancelToken.cancel();
    setBlobData(null);
  };

  const uploadFiles = async () => {
    setSubBtnPressed(true);
    for (const file of selectedFiles) {
      if (file.file && file.uploadStatus !== "done" && !file.notAllowed) {
        await DocumentUploadActions.submitDocuments(
          currentSelected,
          file,
          dispatch,
          Auth.getLoanAppliationId()
        );
      }
    }
    setSubBtnPressed(false);
    try {
      let docs = await DocumentActions.getPendingDocuments(
        Auth.getLoanAppliationId()
      );
      if (docs) {
        if (docs?.length) {
          dispatch({
            type: DocumentsActionType.FetchPendingDocs,
            payload: docs,
          });
          let currentDoc = docs.find((d) => d.docId === currentSelected.docId);
          dispatch({
            type: DocumentsActionType.SetCurrentDoc,
            payload: currentDoc,
          });
        }
      }
    } catch (error) {}
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
    if (fileAlreadyExists(file, newName)) {
      return false;
    }
    let updatedFiles = selectedFiles.map((f: Document) => {
      if (file.file && f.clientName === file.clientName) {
        f.clientName = `${newName}.${FileUpload.getExtension(file, "dot")}`;
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
        // if (shouldMoveFocus) {
        //   // debugger
        //   selectedFiles[i + 1].focused = true;
        //   console.log('dsskdjflksjdflksjfd');
        //   console.log(selectedFiles[i + 1]);
        //   console.log('dsskdjflksjdflksjfd');
        // }
        return f;
      }
      // debugger
      return f;
    });
    dispatch({ type: DocumentsActionType.AddFileToDoc, payload: updatedFiles });
    // moveFocus(updatedFiles, selectedFiles[nextInd])
  };

  const moveFocus = (previousFiles: Document[], fileToFocus: Document) => {
    let updatedFiles = previousFiles.map((f: Document) => {
      if (f.clientName === fileToFocus.clientName) {
        f.focused = true;
        return fileToFocus;
      }
      return f;
    });
    dispatch({ type: DocumentsActionType.AddFileToDoc, payload: updatedFiles });
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
        dispatch({ type: DocumentsActionType.FetchPendingDocs, payload: docs });
        dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: docs[0] });
      } else if (docs?.length === 0) {
        dispatch({ type: DocumentsActionType.FetchPendingDocs, payload: docs });
      }
      setDoneVisible(false);
      setDoneHit(false);
      fetchUploadedDocuments();
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

  const checkFocus = (f: Document, index: number) => {
    let foundIndx = selectedFiles.filter(
      (f: Document) => f.uploadStatus === "done" || f.editName === false
    ).length;
    return foundIndx === index;
  };

  return (
    <section className="file-drop-box-wrap">
      <div className="file-drop-box havefooter">
        <div className="list-selected-doc">
          <ul className="doc-list-ul">
            {selectedFiles.map((f, index) => {
              return (
                <DocumentItem
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
                className="addmoreDoc"
                onClick={(e) => {
                  addMore(e);
                }}
              >
                {" "}
                Add more files
                <input
                  type="file"
                  accept={FileUpload.allowedExtensions}
                  id="inputFile"
                  ref={inputRef}
                  multiple
                  style={{ display: "none" }}
                />
              </a>
            ) : (
              <a className="addmoreDoc disabled">
                {" "}
                Add more files
                <input
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
        {!!currentDoc && (
          <DocumentView
            hideViewer={() => setCurrentDoc(null)}
            {...currentDoc}
            blobData={blobData}
            submittedDocumentCallBack={getSubmittedDocumentForView}
          />
        )}
      </div>
      <div className="doc-upload-footer">
        {doneVisible ? (
          <div className="doc-confirm-wrap">
            <div className="row">
              <div className="col-sm-7">
                <div className="dc-text">
                  {/* {docTitle} */}
                  <p>
                    Have you submitted all required files for this document?
                  </p>
                </div>
              </div>

              <div className="col-sm-5">
                <div className="dc-actions">
                  <button
                    className="btn btn-small btn-secondary"
                    onClick={() => {
                      setDoneVisible(false);
                      disableSubmitBtn();
                    }}
                  >
                    No
                  </button>
                  <button
                    className="btn btn-small btn-primary"
                    onClick={doneDoc}
                  >
                    Yes
                  </button>
                </div>
              </div>
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
