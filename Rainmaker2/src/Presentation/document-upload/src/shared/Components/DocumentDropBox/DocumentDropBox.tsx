import React, { useState, ChangeEvent, useRef, useEffect, DragEvent } from 'react';
import { Http } from '../../../services/http/Http';


const httpClient = new Http();

type DocumentDropBoxPropsType = { url: string, setSelectedFiles: Function, setFileInput: Function };

export const DocumentDropBox = ({ url, setSelectedFiles, setFileInput }: DocumentDropBoxPropsType) => {

    const inputRef = useRef<HTMLInputElement>(null);

    useEffect(() => {
        setFileInput(inputRef.current)
    }, [])

    const handleChange = ({ target: { files } }: ChangeEvent<HTMLInputElement>) => {

        if (files) {
            setSelectedFiles(files);
        }
    }

    const getDroppedFile = (e: DragEvent<HTMLDivElement>) => {
        e.preventDefault();
        for (var i = 0; i < e.dataTransfer.files.length; i++) {
            let {files} = e.dataTransfer;
            setSelectedFiles(files);
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
            <input 
                ref={inputRef} 
                type="file" 
                name="file" 
                onChange={(e) => handleChange(e)}
                multiple />
        </div>
    )
}
