import React, { useState, ChangeEvent, useRef, useEffect, DragEvent } from 'react';
import { Http } from '../../../services/http/Http';
import DocUploadIcon from '../../../assets/images/upload-doc-icon.svg';


const allowedExtensions = ".doc, .jpg, .jpeg, .png";

type DocumentDropBoxPropsType = { url: string, setSelectedFiles: Function, setFileInput: Function };

export const DocumentDropBox = ({ url, setSelectedFiles, setFileInput }: DocumentDropBoxPropsType) => {

    const inputRef = useRef<HTMLInputElement>(null);

    useEffect(() => {
        setFileInput(inputRef.current)
    }, [])

    const handleChange = ({ target: { files } }: ChangeEvent<HTMLInputElement>) => {
        let selectedFilesAllowed: File[] = [];
        let selectedFilesNotAllowed: File[] = [];
        if (files && files.length) {
            for (let i = 0; i < files.length; i++) {
                const f = files[i];
                let ext = f.type.split('/')[1]
                if (allowedExtensions.includes(ext) && f.size / 1000 < 1000) {
                    selectedFilesAllowed.push(f);
                } else {
                    selectedFilesNotAllowed.push(f);
                }
            }
        }

        // for (const f of selectedFilesNotAllowed) {
        //     if (f.size > 100) {
        //         alert(`file size must be less than 100kbs only file."`);
        //     }
        //     alert(`${f.type} is not allowed allowed files are "${allowedExtensions}"`);
        // }
        setSelectedFiles(selectedFilesAllowed);
    }

    const getDroppedFile = (e: DragEvent<HTMLDivElement>) => {
        e.preventDefault();
        for (var i = 0; i < e.dataTransfer.files.length; i++) {
            let { files } = e.dataTransfer;
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
                                Your don't have any files.
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
