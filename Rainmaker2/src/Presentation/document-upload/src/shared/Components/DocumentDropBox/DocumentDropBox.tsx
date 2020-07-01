import React, { useState, ChangeEvent, useRef, useEffect, DragEvent, Component } from 'react';
import { Http } from '../../../services/http/Http';
import DocUploadIcon from '../../../assets/images/upload-doc-icon.svg';
import { isFileAllowed } from '../../../store/actions/DocumentActions';


const allowedExtensions = ".pdf, .jpg, .jpeg, .png";

type DocumentDropBoxPropsType = {getFiles: Function, setFileInput: Function };

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
                                accept={allowedExtensions} />
                        </div>
                    </div>
                </div>
        </section>
    )
}


export class FileDropper extends Component<{getDroppedFiles: Function}> {

    getDroppedFile(e: DragEvent<HTMLDivElement>) {
        e.preventDefault();
        for (var i = 0; i < e.dataTransfer.files.length; i++) {
            let { files } = e.dataTransfer;
            this.props.getDroppedFiles(files);
        }
    }

    onDragEnter(e: any) {
        e.preventDefault();
        e.target.classList.add('drag-enter')
        return false;
    }

    onDragLeave(e: any) {
        e.preventDefault();
        e.target.classList.remove('drag-enter')
        return false;
    }

    onDrop(e: any) {

        e.preventDefault();
        e.target.classList.remove('drag-enter')
        this.getDroppedFile(e);
        return false;
    }

    ondragover(e: any) {
        e.preventDefault();
        return false;
    }

    // #4484F4

    render() {
        return (
            <div id="file-dropper" className="file-drop-box"
                onDragEnter={this.onDragEnter}
                onDragLeave={this.onDragLeave}
                onDragOver={this.ondragover}
                onDrop={(e) => this.onDrop(e)}>
                {this.props.children}
            </div>
        )
    }
}