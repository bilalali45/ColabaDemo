import React, { useState } from 'react'
import { DocumentItem } from './DocumentItem/DocumentItem'
import { DocumentView } from '../../../../../shared/Components/DocumentView/DocumentView'
import { Http } from '../../../../../services/http/Http';
import { FileSelected } from '../DocumentUpload';

const httpClient = new Http();


type SelectedDocumentsType = {
    files: FileSelected[],
    url: string
}

export const SelectedDocuments = ({ files, url }: SelectedDocumentsType) => {

    const [showingDoc, setShowingDoc] = useState<boolean>(false);
    const [selectedFiles, setSelectedFiles] = useState<FileSelected[]>(files);
    const [currentDoc, setCurrentDoc] = useState<File | null>(null);
    const [fileType, setFileType] = useState<string>('');
    const [uploadedPercent, setUploadPercent] = useState<number>();
    const [showProgressBar, setShowProgressBar] = useState<boolean>();


    const data = new FormData();

    const viewDocument = (file: File) => {
        setShowingDoc(true);
        setFileType(file.type);
        setCurrentDoc(file);
    }


    const closeDocumentView = () => {
        setShowingDoc(false);
    }

    const uploadFile = async () => {

        for (const file of files) {
            console.log(file);
            data.append('file', file.file, `${file.name}`);
        }
        console.log(data);
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
            setShowProgressBar(false);
        } catch (error) {
            console.log(error);
        }
    }

    const changeName = (file: FileSelected, newName: string) => {
        console.log(file, newName);
        setSelectedFiles((prevFiles: FileSelected[]) => {
            return prevFiles.map((f: FileSelected) => {
                if (f.file.name === file.file.name) {
                    f.name = `${newName}.${file.file.type.split('/')[1]}`;
                    return f;
                }
                return f;
            })
        })
        // console.log(data.get('file'));
    }

    return (
        <div className="file-drop-box">
            <h1>Selected Documents</h1>
            {
                selectedFiles.map(f => {
                    return (<DocumentItem
                        file={f}
                        viewDocument={viewDocument}
                        changeName={changeName} />)
                })
            }
            {showingDoc ? <DocumentView
                file={currentDoc}
                type={fileType}
                url={`http://localhost:5000/pdf/${currentDoc?.name}`}
                hide={closeDocumentView} />
                : ''}
            {showProgressBar && <progress value={uploadedPercent} max="100">{uploadedPercent + '%'}</progress>}
            <button onClick={uploadFile}>Submit</button>
        </div>
    )
}
