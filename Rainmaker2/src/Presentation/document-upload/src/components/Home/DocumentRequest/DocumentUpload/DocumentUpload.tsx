import React, { useState, ChangeEvent } from 'react'
import { DocumentDropBox } from '../../../../shared/Components/DocumentDropBox/DocumentDropBox'
import { SelectedDocuments } from './SelectedDocuments/SelectedDocuments'

export const DocumentUpload = () => {
    const [files, setFiles] = useState<File[]>([]);
    const [fileInput, setFileInput] = useState<HTMLInputElement>();

    const getSelectedFile = (file: File) => {
        console.log(file);
        setFiles((previousFiles) => {
            return [
                ...previousFiles,
                file
            ]
        })
    }

    const getFileInput = (fileInputEl: HTMLInputElement) => {
        setFileInput(fileInputEl);
    }

    const showFileExplorer = () => {
        fileInput?.click();
        if (fileInput) {
            fileInput.onchange = (e: any) => {
                if (e?.target?.files) {
                    setFiles((previousFiles) => {
                        return [
                            ...previousFiles,
                            e.target.files[0]
                        ]
                    })
                }
            };
        }
        setTimeout(() => {
            console.log(fileInput?.files);
        }, 10000);
    }

    return (
        <div>
            <h1>Document Upload</h1>
            {!files.length ?
                <DocumentDropBox
                    url={'http://localhost:5000/upload'}
                    setSelectedFile={getSelectedFile}
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
