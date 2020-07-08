import React, { useState, useEffect, useContext, useRef } from "react";

import { DocumentItem } from "./DocumentItem/DocumentItem";
import { DocumentView } from "../../../../../shared/Components/DocumentView/DocumentView";
import { Store } from "../../../../../store/store";
import { Document } from "../../../../../entities/Models/Document";
import { DocumentActions } from "../../../../../store/actions/DocumentActions";
import { DocumentsActionType } from "../../../../../store/reducers/documentReducer";
import { Auth } from "../../../../../services/auth/Auth";
import { DocumentRequest } from "../../../../../entities/Models/DocumentRequest";
import { DocumentUploadActions } from "../../../../../store/actions/DocumentUploadActions";
import { FileUpload } from "../../../../../utils/helpers/FileUpload";

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

  const inputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    setFileInput(inputRef.current);
    disableSubmitBtn();
  }, [selectedFiles, selectedFiles.length, currentSelected]);

  useEffect(() => {
    if (selectedFiles.length < 10) {
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

  const uploadFiles = async () => {
    setSubBtnPressed(true);
    for (const file of selectedFiles) {
      if (file.file && file.uploadStatus !== "done" && !file.notAllowed) {
        await DocumentUploadActions.submitDocuments(
          currentSelected,
          file,
          dispatch,
          Auth.getLoanAppliationId(),
          Auth.getTenantId()
        );
      }
    }
    setSubBtnPressed(false);
    try {
      let docs = await DocumentActions.getPendingDocuments(
        Auth.getLoanAppliationId(),
        Auth.getTenantId()
      );
      if (docs) {
        if (pendingDocs.length) {
          let updatedDocs = pendingDocs.map((p) => {
            let filesAdded = p.files.filter(
              (f) => f.file && f.uploadStatus === "pending"
            );
            let docToUpdate = docs?.find((d) => d.docId === p.docId);
            if (docToUpdate && docToUpdate.files) {
              docToUpdate.files = [...docToUpdate?.files, ...filesAdded];
            }
            return docToUpdate;
          });
          dispatch({
            type: DocumentsActionType.FetchPendingDocs,
            payload: updatedDocs,
          });
          let doc = docs.find((d) => d.docId === currentSelected?.docId);
          dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: doc });
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
      Auth.getLoanAppliationId(),
      Auth.getTenantId()
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
        Auth.getTenantId(),
        data
      );
      if (docs?.length) {
        dispatch({ type: DocumentsActionType.FetchPendingDocs, payload: docs });
        dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: docs[0] });
      } else if (docs?.length === 0) {
        dispatch({ type: DocumentsActionType.FetchPendingDocs, payload: docs });
      }
      setDoneVisible(false);
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

  return (
    <section className="file-drop-box-wrap">
      <div className="file-drop-box havefooter">
        <div className="list-selected-doc">
          <ul className="doc-list-ul">
            {selectedFiles.map((f, index) => {
              return (
                <DocumentItem
                  handleDelete={handleDeleteAction}
                  disableSubmitButton={setBtnDisabled}
                  fileAlreadyExists={fileAlreadyExists}
                  retry={(fileToRemove) => addMore(fileToRemove)}
                  file={f}
                  viewDocument={viewDocument}
                  changeName={changeName}
                  deleteDoc={deleteDoc}
                  indexKey={index}
                />
              );
            })}
          </ul>
          <div className="addmore-wrap">
            <a
              className="addmoreDoc"
              onClick={(e) => {
                addMore(e);
              }}
            >
              {selectedFiles.length < 10 && "Add more files"}
              <input
                type="file"
                accept={FileUpload.allowedExtensions}
                id="inputFile"
                ref={inputRef}
                multiple
                style={{ display: "none" }}
              />
            </a>
          </div>
        </div>
        {!!currentDoc && (
          <DocumentView
            hideViewer={() => setCurrentDoc(null)}
            {...currentDoc}
          />
        )}
      </div>
      <div className="doc-upload-footer">
        {doneVisible ? (
          <div className="doc-confirm-wrap">
            <div className="row">
              <div className="col-sm-7">
                <div className="dc-text">
                  <p>Are you done with this {docTitle}?</p>
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
        ) : fileLimitError.value ? (
          <p className="text-danger">
            Only 10 files can be uploaded per document.{" "}
            <i
              onClick={() => {
                setFileLimitError({ value: false });
              }}
              className="zmdi zmdi-close"
            ></i>
          </p>
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
