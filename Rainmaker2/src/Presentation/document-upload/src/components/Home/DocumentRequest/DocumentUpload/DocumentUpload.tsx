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

export const DocumentUpload = () => {
  const [fileInput, setFileInput] = useState<HTMLInputElement>();
  const [fileLimitError, setFileLimitError] = useState({ value: false });
  const [showAlert, setshowAlert] = useState<boolean>(false);
  const { state, dispatch } = useContext(Store);
  const { currentDoc }: any = state.documents;
  const selectedfiles: Document[] = currentDoc?.files || null;
  let docTitle = currentDoc ? currentDoc.docName : "";
  let docMessage = currentDoc?.docMessage
    ? currentDoc.docMessage
    : "Please upload the following documents.";

  const parentRef = useRef<HTMLDivElement>(null);

  const getFileInput = (fileInputEl: HTMLInputElement) => {
    setFileInput(fileInputEl);
  };

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
    fileInput?.click();
    if (fileInput) {
      fileInput.onchange = async (e: any) => {
        let files = e?.target?.files;
        if (files) {
          let updatedFiles = selectedfiles.filter((sf) => sf !== fileToRemnove);
          DocumentUploadActions.updateFiles(
            files,
            updatedFiles,
            dispatch,
            setFileLimitError
          );
        }
      };
    }
  };

  return (
    <section className="Doc-upload" ref={parentRef}>
      <FileDropper
        parent={parentRef.current}
        getDroppedFiles={(files) =>
          DocumentUploadActions.updateFiles(
            files,
            selectedfiles,
            dispatch,
            setFileLimitError
          )
        }
      >
        {currentDoc && (
          <div className="Doc-head-wrap">
            <h2 title={docTitle}><span className="text-ellipsis">
              {docTitle}</span>
              {currentDoc?.isRejected && (
                <span
                  style={{
                    color: "red",
                    fontSize: 11,
                  }}
                  className="Doc-head-wrap--alert"
                >
                  CHANGES REQUESTED
                </span>
              )}
            </h2>
            <div className="doc-note">
              <p>
                <i className="fas fa-info-circle"></i>
                {docMessage}
              </p>
            </div>
          </div>
        )}

        {currentDoc && (
          <Fragment>
            {!selectedfiles?.length ? (
              <DocumentDropBox
                getFiles={(files) =>
                  DocumentUploadActions.updateFiles(
                    files,
                    selectedfiles,
                    dispatch,
                    setFileLimitError
                  )
                }
                setFileInput={getFileInput}
              />
            ) : (
              <>
                <SelectedDocuments
                  fileLimitError={fileLimitError}
                  setFileLimitError={setFileLimitError}
                  addMore={showFileExplorer}
                  setFileInput={getFileInput}
                />
              </>
            )}
          </Fragment>
        )}
      </FileDropper>
      {showAlert && <AlertBox hideAlert={() => setshowAlert(false)} />}
    </section>
  );
};
