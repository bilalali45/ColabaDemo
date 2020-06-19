import React, { useState, ChangeEvent } from 'react'
import { DocumentDropBox } from '../../../../shared/Components/DocumentDropBox/DocumentDropBox'
import { SelectedDocuments } from './SelectedDocuments/SelectedDocuments'

export type FileSelected = {
    name: string;
    file: File;
}

export const DocumentUpload = () => {
    const [files, setFiles] = useState<FileSelected[]>([]);
    const [fileInput, setFileInput] = useState<HTMLInputElement>();



    const updateFiles = (files: File[]) => {
        setFiles((previousFiles) => {
            let allSelectedFiles: FileSelected[] = [];
            allSelectedFiles = [...previousFiles];
            for (let f of files) {
                let selectedFile = {
                    name: f.name,
                    file: f
                }
                allSelectedFiles.push(selectedFile);
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
        <section className="Doc-upload">
            <div className="Doc-head-wrap">
                <h2> Bank statement</h2>
                <div className="doc-note">
                    <p>
                        <i className="fas fa-info-circle"></i>
                        Hi {"Raza"}, Please submit 2 months of the most recent Bank Statement
                    </p>
                </div>
            </div>
            <div>
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
        </section>
    )
}
