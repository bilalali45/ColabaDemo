import React, { useState, ChangeEvent, useContext, useEffect } from 'react'
import { DocumentDropBox } from '../../../../shared/Components/DocumentDropBox/DocumentDropBox'
import { SelectedDocuments } from './SelectedDocuments/SelectedDocuments'
import { Store } from '../../../../store/store'
import { DocumentsActionType, DocumentsType } from '../../../../store/reducers/documentReducer'
import { DocumentRequest } from '../../../../entities/Models/DocumentRequest'
import { Document } from '../../../../entities/Models/Document'

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

    let docTitle = currentDoc ? currentDoc.docName : '';
    let docMessage = currentDoc ? currentDoc.docMessage : '';
    console.log('docTitle and docMessage', docTitle, docMessage)

    useEffect(() => {
        dispatch({ type: DocumentsActionType.AddFileToDoc, payload: files })
    }, [files?.length])

    useEffect(() => {
        setFiles(currentDoc?.files)
    }, [currentDoc?.docName])


    const updateFiles = (files: File[]) => {
        setFiles((previousFiles) => {
            let allSelectedFiles: Document[] = [];
            allSelectedFiles = [...previousFiles];
            for (let f of files) {
                const selectedFile = new Document('', f.name, '', 0, 0, f);
                allSelectedFiles.push(selectedFile);
            }
            return allSelectedFiles;
        });
    }


    const getSelectedFiles = (files: File[]) => {
        
        (files);
    }

    const getFileInput = (fileInputEl: HTMLInputElement) => {
        setFileInput(fileInputEl);
    }

    const showFileExplorer = () => {
        fileInput?.click();
        if (fileInput) {
            fileInput.onchange = (e: any) => {
                let files = e?.target?.files;
                if (files) {
                    updateFiles(files);
                }
            };
        }
    }

    console.log(state);
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
                {!selectedfiles?.length ?
                    <DocumentDropBox
                        url={'http://localhost:5000/upload'}
                        setSelectedFiles={getSelectedFiles}
                        setFileInput={getFileInput} />
                    : <>
                        <SelectedDocuments
                            addMore={showFileExplorer}
                            files={selectedfiles}
                            url={'https://alphamaingateway.rainsoftfn.com/api/Documentmanagement/file/submit'} />
                        {/* <button onClick={showFileExplorer}>Add More</button> */}
                    </>
                }
            </div>
        </section>
    )
}
