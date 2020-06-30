import React, { useState, ChangeEvent, useRef, useEffect, DragEvent } from 'react';
import { Http } from '../../../services/http/Http';
import DocUploadIcon from '../../../assets/images/upload-doc-icon.svg';
import { isFileAllowed } from '../../../store/actions/DocumentActions';


const allowedExtensions = ".pdf, .jpg, .jpeg, .png";

type DocumentDropBoxPropsType = { url: string, setSelectedFiles: Function, updateFiles: Function, setFileInput: Function };

export const DocumentDropBox = ({ url, setSelectedFiles, updateFiles, setFileInput }: DocumentDropBoxPropsType) => {

    const inputRef = useRef<HTMLInputElement>(null);

    useEffect(() => {
        setFileInput(inputRef.current)
    }, []);

    const handleChange = ({ target: { files } }: ChangeEvent<HTMLInputElement>) => {
        updateFiles(files);
    }

    const getDroppedFile = (e: DragEvent<HTMLDivElement>) => {
        e.preventDefault();
        for (var i = 0; i < e.dataTransfer.files.length; i++) {
            let { files } = e.dataTransfer;
            updateFiles(files);
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
        e.target.classList.remove('drag-enter')
        getDroppedFile(e);
        return false;
    }

    const ondragover = (e: any) => {
        e.preventDefault();
        return false;
    }

    return (
        <section className="empty-uploader">
            <div className="file-drop-box"
                onDragEnter={onDragEnter}
                onDragLeave={onDragLeave}
                onDragOver={ondragover}
                onDrop={onDrop}>
                <div className="empty-d-box-wrap">
                    <div className="f-dropbox-wrap">
                        <div className="icon-doc-upload">
                            <img src={DocUploadIcon} alt="" />
                        </div>
                        <div className="chosefileWrap">
                            <label htmlFor="inputFile">
                                You don't have any files.
                        <br />
                        Drop it here or <span>upload</span>
                            </label>
                            <input
                                ref={inputRef}
                                type="file"
                                name="file"
                                id="inputFile"
                                onChange={(e) => handleChange(e)}
                                multiple
                                accept={allowedExtensions} />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    )
}
