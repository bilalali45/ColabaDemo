import React, { useState } from 'react'
import { DocumentItem } from './DocumentItem/DocumentItem'
import { DocumentView } from '../../../../../shared/Components/DocumentView/DocumentView'
import { Http } from '../../../../../services/http/Http';

const httpClient = new Http();


type SelectedDocumentsType = {
    files: File[],
    url: string
}

export const SelectedDocuments = ({ files, url }: SelectedDocumentsType) => {

    const [showingDoc, setShowingDoc] = useState<boolean>(false);
    const [currentDoc, setCurrentDoc] = useState<string>('');
    const [fileType, setFileType] = useState<string>('');
    const [uploadedPercent, setUploadPercent] = useState<number>();
    const [showProgressBar, setShowProgressBar] = useState<boolean>();

    const viewDocument = (file: File) => {
        setShowingDoc(true);
        setFileType(file.type);
        setCurrentDoc(file.name);
    }


    const closeDocumentView = () => {
        setShowingDoc(false);
    }

    const uploadFile = async () => {
        console.log(files[0]);
        const data = new FormData();

        for (const file of files) {
            data.append('file', file);
        }
        setShowProgressBar(true);
        try {
            let res = await httpClient.fetch({
                method: httpClient.methods.POST,
                url,
                data,
                onUploadProgress: e => {
                    let p = (e.loaded / e.total * 100);
                    setUploadPercent(p);
                }
            });
            console.log(res);
            setShowProgressBar(false);
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <div className="file-drop-box">
            <h1>Selected Documents</h1>
            {
                files.map(f => {
                    return (<DocumentItem
                        file={f}
                        viewDocument={viewDocument} />)
                })
            }
            {showingDoc ? <DocumentView
                type={fileType}
                url={`http://localhost:5000/pdf/${currentDoc}`}
                hide={closeDocumentView} />
                : ''}
            {showProgressBar && <progress value={uploadedPercent} max="100">{uploadedPercent + '%'}</progress>}
            <button onClick={uploadFile}>Submit</button>
        </div>
    )
}
