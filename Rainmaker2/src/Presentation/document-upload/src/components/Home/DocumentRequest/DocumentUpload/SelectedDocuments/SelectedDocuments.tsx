import React, { useState, useEffect, useContext } from "react";

import { DocumentItem } from "./DocumentItem/DocumentItem";
import { DocumentView } from "../../../../../shared/Components/DocumentView/DocumentView";
import { Store } from "../../../../../store/store";
import { Document } from "../../../../../entities/Models/Document";
import { DocumentActions } from "../../../../../store/actions/DocumentActions";
import { DocumentsActionType } from "../../../../../store/reducers/documentReducer";
import { Auth } from "../../../../../services/auth/Auth";
import { DocumentRequest } from "../../../../../entities/Models/DocumentRequest";

interface SelectedDocumentsType {
  removeActualFile: Function;
  addMore: Function;
  files: Document[];
  url: string;
}

interface ViewDocumentType {
  id: string;
  requestId: string;
  docId: string;
  clientName?: string;
  fileId?: string;
}

export const SelectedDocuments = ({
  files,
  url,
  addMore,
  removeActualFile,
}: SelectedDocumentsType) => {
  const [selectedFiles, setSelectedFiles] = useState<Document[]>(files);
  const [currentDoc, setCurrentDoc] = useState<ViewDocumentType | null>(null);
  const [btnDisabled, setBtnDisabled] = useState<boolean>(true);
  const [subBtnPressed, setSubBtnPressed] = useState<boolean>(false);
  const [doneVisible, setDoneVisible] = useState<boolean>(false);
  const { state, dispatch } = useContext(Store);

  const documents: any = state.documents;
  const currentSelected: any = documents.currentDoc;
  const docTitle = currentSelected ? currentSelected.docName : "";

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
    });
  };

  const uploadFiles = async () => {
    setSubBtnPressed(true);

    for (const file of files) {
      if (file.file && file.uploadStatus !== "done") {
        await DocumentActions.submitDocuments(currentSelected, file, dispatch);
      }
    }
  };

  const changeName = (file: Document, newName: string) => {
    let updatedFiles = selectedFiles.map((f: Document) => {
      if (f.file && f.file.name === file?.file?.name) {
        f.clientName = `${newName}.${file.file.type.split("/")[1]}`;
        f.editName = !f.editName;
        return f;
      }

      return f;
    });

    dispatch({ type: DocumentsActionType.AddFileToDoc, payload: updatedFiles });
  };

  const deleteDoc = (fileName: string) => {
    removeActualFile(fileName);

    let updatedFiles = selectedFiles.filter((f: Document) => {
      if (f?.clientName.split(".")[0] !== fileName) {
        return f;
      }
    });

    dispatch({ type: DocumentsActionType.AddFileToDoc, payload: updatedFiles });
  };

  const disableSubmitBtn = () => {
    let docFile = files.find((df) => df.file && df.uploadStatus === "pending");
    let docEdit = files.find((de) => de.editName);

    if (!docFile || docEdit) {
      setBtnDisabled(true);
    } else {
      setBtnDisabled(false);
    }
  };

  const doneDoc = async () => {
    let fields = ["id", "requestId", "docId"];
    let data = {};

    if (currentSelected) {
      for (const field of fields) {
        data[field] = currentSelected[field];
      }
      let docs : DocumentRequest[] | undefined = await DocumentActions.finishDocument(Auth.getLoanAppliationId(), Auth.getTenantId(), data);
      if(docs?.length) {
        dispatch({ type: DocumentsActionType.FetchPendingDocs, payload: docs });
        dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: docs[0] });
      }
      setDoneVisible(false);
    }
  };

  const hasSubmitted = () => {
    let lastItem = files[files.length - 1];
    if (files.filter((f) => f.uploadStatus !== "done").length === 0) {
      setDoneVisible(true);
      return;
    }
    return lastItem.file && lastItem.uploadStatus === "done"
      ? setDoneVisible(true)
      : setDoneVisible(false);
  };

  useEffect(() => {
    setSelectedFiles(files);

    disableSubmitBtn();

    hasSubmitted();

    if (files[files.length - 1].uploadStatus === "done") {
      setSubBtnPressed(false);
    }
  }, [files, files.length]);

  return (
    <section className="file-drop-box-wrap">
      <div className="file-drop-box havefooter">
        <div className="list-selected-doc">
          <ul className="doc-list-ul">
            {selectedFiles.map((f, index) => {
              return (
                <DocumentItem
                  file={f}
                  viewDocument={viewDocument}
                  changeName={changeName}
                  deleteDoc={deleteDoc}
                  key={index}

                />
              );
            })}
          </ul>
          <div className="addmore-wrap">
            <a className="addmoreDoc" onClick={(e) => addMore(e)}>
              Add more files
            </a>
          </div>
        </div>
        {!!currentDoc && (
          <DocumentView
            hideViewer={() => setCurrentDoc(null)}
            {...currentDoc}
          />
        )}
        {/* {showProgressBar && <progress value={uploadedPercent} max="100">{uploadedPercent + '%'}</progress>} */}
      </div>
      <div className="doc-upload-footer">
        {/* {!hasSubmitted() && !subBtnPressed &&} */}

        {doneVisible ? (
          <div className="doc-confirm-wrap">
            <div className="row">
              <div className="col-sm-8">
                <div className="dc-text">
                  <p>Are you done with this {docTitle}?</p>
                </div>
              </div>

              <div className="col-sm-4">
                <div className="dc-actions">
                  <button
                    className="btn btn-small btn-secondary"
                    onClick={() => setDoneVisible(false)}
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
            <button
              disabled={btnDisabled || subBtnPressed}
              className="btn btn-primary"
              onClick={uploadFiles}
            >
              Submit
            </button>
          </div>
        )}
      </div>
    </section>
  );
};
