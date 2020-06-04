import React, { useState, ChangeEvent } from 'react'
import { DocumentDropBox } from '../../../../shared/Components/DocumentDropBox/DocumentDropBox'
import { SelectedDocuments } from './SelectedDocuments/SelectedDocuments'

export const DocumentUpload = () => {
    const [files, setFiles] = useState<File[]>([]);
    const [fileInput, setFileInput] = useState<HTMLInputElement>();



    const updateFiles = (files: File[]) => {
        setFiles((previousFiles) => {
            let allSelectedFiles: File[] = [];
            allSelectedFiles = [...previousFiles];
            for (let f of files) {
                allSelectedFiles.push(f);
            }
            return allSelectedFiles;
        });
    }


    const getSelectedFiles = (files: File[]) => {
        updateFiles(files);
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

    return (
        <div>
            <p>Document Upload</p>
            {!files.length ?
                <DocumentDropBox
                    url={'http://localhost:5000/upload'}
                    setSelectedFiles={getSelectedFiles}
                    setFileInput={getFileInput} />
                : <>
                    <SelectedDocuments
                        files={files}
                        url={'http://localhost:5000/upload'} />
                    <button onClick={showFileExplorer}>Add More</button>
                </>
            }
        </div>
    )
}
