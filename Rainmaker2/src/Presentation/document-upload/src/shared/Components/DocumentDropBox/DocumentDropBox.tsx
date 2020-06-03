import React, { useState, ChangeEvent, useRef, useEffect } from 'react';
import { Http } from '../../../services/http/Http';


const httpClient = new Http();

type DocumentDropBoxPropsType = { url: string, setSelectedFile: Function, setFileInput: Function };

export const DocumentDropBox = ({ url, setSelectedFile, setFileInput }: DocumentDropBoxPropsType) => {

    const inputRef = useRef<HTMLInputElement>(null);

    const [file, setFile] = useState<File>();
    const [uploadedPercent, setUploadPercent] = useState<number>();

    useEffect(() => {
        setFileInput(inputRef.current)
    }, [])

    const handleChange = ({ target: { files } }: ChangeEvent<HTMLInputElement>) => {
        console.log('files', files);
        if (files) {
            setFile(() => files[0]);
            setSelectedFile(files[0]);
        }
    }

    const getDroppedFile = (e: any) => {
        e.preventDefault();
        for (var i = 0; i < e.dataTransfer.files.length; i++) {
            let file = e.dataTransfer.files[i];
            setFile(file);
            setSelectedFile(file);
        }
    }

    const uploadFile = async () => {
        console.log(file);
        const data = new FormData();
        if (file) {
            data.append('file', file);
            try {
                let res = await httpClient.fetch({
                    method: httpClient.methods.POST,
                    url,
                    data,
                    onUploadProgress: e => {
                        setUploadPercent(e.loaded / e.total * 100);
                    }
                });
                console.log(res);
            } catch (error) {

            }
        }
    }

    const onDragEnter = (e: any) => {
        e.preventDefault();
        e.target.classList.add('drag-enter')
        return false;
    }

    const onDragLeave = (e: any) => {
        e.preventDefault();
        e.target.classList.remove('drag-enter')
        return false;
    }

    const onDrop = (e: any) => {
        e.preventDefault();
        e.target.classList.remove('drag-enter');
        getDroppedFile(e);
        return false;
    }

    const ondragover = (e: any) => {
        e.preventDefault();
        return false;
    }

    return (
        <div className="file-drop-box"
            onDragEnter={onDragEnter}
            onDragLeave={onDragLeave}
            onDragOver={ondragover}
            onDrop={onDrop}>
            <h1>Document Drop Box</h1>
            <h1>{file?.name}</h1>
            <h1>{uploadedPercent}</h1>
            <input 
                ref={inputRef} 
                type="file" 
                name="file" 
                onChange={(e) => handleChange(e)}
                multiple />
            <button onClick={uploadFile}>Testing</button>
        </div>
    )
}
