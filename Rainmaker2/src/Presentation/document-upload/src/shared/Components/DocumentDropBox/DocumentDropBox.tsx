import React, { useState, ChangeEvent, useRef, useEffect, DragEvent, Component } from 'react';
import DocUploadIcon from '../../../assets/images/upload-doc-icon.svg';
import { FileUpload } from '../../../utils/helpers/FileUpload';

type DocumentDropBoxPropsType = { getFiles: Function, setFileInput: Function };

export const DocumentDropBox = ({ getFiles, setFileInput }: DocumentDropBoxPropsType) => {

    const inputRef = useRef<HTMLInputElement>(null);

    useEffect(() => {
        setFileInput(inputRef.current)
    }, []);

    const handleChange = ({ target: { files } }: ChangeEvent<HTMLInputElement>) => {
        getFiles(files);
    }

    return (
        <section className="empty-uploader">
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
                            accept={FileUpload.allowedExtensions} />
                        <div className="upload-note">
                            <p>
                                File Type: PDF, JPEG, PNG <br />
                                File Size: {FileUpload.allowedSize}mb
                                </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    )
}


export class FileDropper extends Component<{ getDroppedFiles: Function, parent: HTMLDivElement | null }> {

    getDroppedFile(e: DragEvent<HTMLDivElement>) {
        e.preventDefault();
        for (var i = 0; i < e.dataTransfer.files.length; i++) {
            let { files } = e.dataTransfer;
            this.props.getDroppedFiles(files);
        }
    }

    onDragEnter(e: any) {
        e.preventDefault();
        if (this.props.parent) {
            this.props.parent.classList.add('drag-enter')
        }
        return false;
    }

    onDragLeave(e: any) {
        e.preventDefault();
        if (this.props.parent) {
            this.props.parent.classList.remove('drag-enter')
        }
        return false;
    }

    onDrop(e: any) {

        e.preventDefault();
        if (this.props.parent) {
            this.props.parent.classList.remove('drag-enter')
            this.getDroppedFile(e);
        }
        return false;
    }

    ondragover(e: any) {
        e.preventDefault();
        if (this.props.parent) {
            this.props.parent.classList.add('drag-enter')
        }
        return false;
    }

    render() {
        return (
            <div id="file-dropper" className="file-drop-box"
                onDragEnter={(e) => this.onDragEnter(e)}
                onDragLeave={(e) => this.onDragLeave(e)}
                onDragOver={(e) => this.ondragover(e)}
                onDrop={(e) => this.onDrop(e)}>
                {this.props.children}
            </div>
        )
    }
}