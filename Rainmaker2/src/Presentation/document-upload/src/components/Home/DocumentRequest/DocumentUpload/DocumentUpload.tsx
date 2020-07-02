import React, { useState, ChangeEvent, useContext, useEffect, useRef } from "react";
import { DocumentDropBox, FileDropper } from "../../../../shared/Components/DocumentDropBox/DocumentDropBox";
import { SelectedDocuments } from "./SelectedDocuments/SelectedDocuments";
import { Store } from "../../../../store/store";
import { Document } from "../../../../entities/Models/Document";
import { isFileAllowed, updateFiles } from "../../../../store/actions/DocumentActions";


export const DocumentUpload = () => {
  const [fileInput, setFileInput] = useState<HTMLInputElement>();
  const { state, dispatch } = useContext(Store);
  const { currentDoc }: any = state.documents;
  const selectedfiles: Document[] = currentDoc?.files || [];
  let docTitle = currentDoc ? currentDoc.docName : "";
  let docMessage = currentDoc ? currentDoc.docMessage : "";


  const parentRef = useRef<HTMLDivElement>(null);

  const getFileInput = (fileInputEl: HTMLInputElement) => {
    setFileInput(fileInputEl);
  };

  const showFileExplorer = () => {
    if (fileInput?.value) {
      fileInput.value = '';
    }
    fileInput?.click();
    if (fileInput) {
      fileInput.onchange = (e: any) => {
        let files = e?.target?.files;
        if (files) {
          updateFiles(files, selectedfiles, dispatch);
        }
      };
    }
  };

  return (
    <section className="Doc-upload" ref={parentRef}>
              <div className="Doc-head-wrap">
          <h2> {docTitle}</h2>
          <div className="doc-note">
            <p>
              <i className="fas fa-info-circle"></i>
              {docMessage}
            </p>
          </div>
        </div>
      <FileDropper
        parent={parentRef.current}
        getDroppedFiles={(files) => updateFiles(files, selectedfiles, dispatch)}
      >
          {selectedfiles ? (
            <DocumentDropBox
              getFiles={(files) => updateFiles(files, selectedfiles, dispatch)}
              setFileInput={getFileInput} />
          ) : (
              <>
                <SelectedDocuments
                  addMore={showFileExplorer}
                  setFileInput={getFileInput}
                />
              </>
            )}

      </FileDropper>
    </section>
  );
};
