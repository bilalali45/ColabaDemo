import React, { useState, ChangeEvent, useContext, useEffect } from "react";
import { DocumentDropBox } from "../../../../shared/Components/DocumentDropBox/DocumentDropBox";
import { SelectedDocuments } from "./SelectedDocuments/SelectedDocuments";
import { Store } from "../../../../store/store";
import {
  DocumentsActionType,
  DocumentsType,
} from "../../../../store/reducers/documentReducer";
import { DocumentRequest } from "../../../../entities/Models/DocumentRequest";
import { Document } from "../../../../entities/Models/Document";
import { getDocLogo } from "../../../../store/actions/DocumentActions";

// export interface FileSelected  {
//     name: string;
//     file: File;
// }

export const DocumentUpload = () => {
  const [files, setFiles] = useState<Document[]>([]);
  const [fileInput, setFileInput] = useState<HTMLInputElement>();
  const [docs, setDocs] = useState<Document[]>();
  const { state, dispatch } = useContext(Store);
  const { currentDoc }: any = state.documents;
  const selectedfiles: Document[] = currentDoc?.files;

  let docTitle = currentDoc ? currentDoc.docName : "";
  let docMessage = currentDoc ? currentDoc.docMessage : "";

  useEffect(() => {
    console.log('files ----------------------------', files)
    dispatch({ type: DocumentsActionType.AddFileToDoc, payload: files });
  }, [files?.length]);

  useEffect(() => {
    setFiles(currentDoc?.files);
  }, [currentDoc?.docName]);

  const removeActualFile = (fileName: string) => {
    // f?.clientName.split('.')[0]
    setFiles((preFiles) => {
      return preFiles.filter(f => {
        if(f?.clientName.split('.')[0] !== fileName) {
          return f;
        }
      })
    });
  }

  const updateFiles = (files: File[]) => {
    setFiles((previousFiles) => {
      let allSelectedFiles: Document[] = [];
      allSelectedFiles = [...previousFiles];
      for (let f of files) {
        const selectedFile = new Document("", f.name, "", 0, 0, getDocLogo(f,'slash'),'pending', f);
        selectedFile.editName = true;
        allSelectedFiles.push(selectedFile);
      }
      return allSelectedFiles;
    });
  };

  

  const getSelectedFiles = (files: File[]) => {
    //  (files);
  };

  const getFileInput = (fileInputEl: HTMLInputElement) => {
    setFileInput(fileInputEl);
  };

  const showFileExplorer = () => {
    if(fileInput?.value) {
      fileInput.value = '';
    }
    fileInput?.click();
    if (fileInput) {
      fileInput.onchange = (e: any) => {
        let files = e?.target?.files;
        if (files) {
          updateFiles(files);
        }
      };
    }
  };

  return (
    <section className="Doc-upload">
      <div className="Doc-head-wrap">
        <h2> {docTitle}</h2>
        <div className="doc-note">
          <p>
            <i className="fas fa-info-circle"></i>
            {docMessage}
          </p>
        </div>
      </div>
      <div>
        {!selectedfiles?.length ? (
          <DocumentDropBox
            url={"http://localhost:5000/upload"}
            setSelectedFiles={getSelectedFiles}
            setFileInput={getFileInput}
          />
        ) : (
          <>
            <SelectedDocuments
              addMore={showFileExplorer}
              removeActualFile={removeActualFile}
              files={selectedfiles}
              url={
                "https://alphamaingateway.rainsoftfn.com/api/Documentmanagement/file/submit"
              }
            />
            {/* <button onClick={showFileExplorer}>Add More</button> */}
          </>
        )}
      </div>
    </section>
  );
};
