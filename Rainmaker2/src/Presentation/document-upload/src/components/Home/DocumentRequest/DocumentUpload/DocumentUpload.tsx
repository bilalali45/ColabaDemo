import React, { useState, ChangeEvent, useContext, useEffect, useRef, Fragment } from "react";
import { DocumentDropBox, FileDropper } from "../../../../shared/Components/DocumentDropBox/DocumentDropBox";
import { SelectedDocuments } from "./SelectedDocuments/SelectedDocuments";
import { Store } from "../../../../store/store";
import { Document } from "../../../../entities/Models/Document";
import { DocumentUploadActions } from "../../../../store/actions/DocumentUploadActions";


export const DocumentUpload = () => {
  const [fileInput, setFileInput] = useState<HTMLInputElement>();
  const { state, dispatch } = useContext(Store);
  const { currentDoc }: any = state.documents;
  const selectedfiles: Document[] = currentDoc?.files || null;
  let docTitle = currentDoc ? currentDoc.docName : "";
  let docMessage = currentDoc ? currentDoc.docMessage : "";


  const parentRef = useRef<HTMLDivElement>(null);

  const getFileInput = (fileInputEl: HTMLInputElement) => {
    setFileInput(fileInputEl);
  };

  const showFileExplorer = (fileToRemnove: Document | null = null) => {
    if (fileInput?.value) {
      fileInput.value = '';
    }
    fileInput?.click();
    if (fileInput) {
      fileInput.onchange = (e: any) => {
        let files = e?.target?.files;
        if (files) {
          let updatedFiles = selectedfiles.filter(sf => sf !== fileToRemnove);
          DocumentUploadActions.updateFiles(files, updatedFiles, dispatch);
        }
      };
    }
  };

  return (
    <section className="Doc-upload" ref={parentRef}>

      <FileDropper
        parent={parentRef.current}
        getDroppedFiles={(files) => DocumentUploadActions.updateFiles(files, selectedfiles, dispatch)}
      >
        {currentDoc && <div className="Doc-head-wrap">
          <h2> {docTitle}</h2>
          <div className="doc-note">
            <p>
              <i className="fas fa-info-circle"></i>
              {docMessage}
            </p>
          </div>
        </div>}

        {
          currentDoc && <Fragment>
            {!selectedfiles?.length ? (
              <DocumentDropBox
                getFiles={(files) => DocumentUploadActions.updateFiles(files, selectedfiles, dispatch)}
                setFileInput={getFileInput} />
            ) : (
                <>
                  <SelectedDocuments
                    addMore={showFileExplorer}
                    setFileInput={getFileInput}
                  />
                </>
              )}
          </Fragment>
        }

      </FileDropper>
    </section>
  );
};
