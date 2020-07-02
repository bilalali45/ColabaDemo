import React, { ChangeEvent, useState, useRef, useEffect } from 'react'
import { DocEditIcon, DocviewIcon } from '../../../../../../shared/Components/Assets/SVG'
import { UserActions } from '../../../../../../store/actions/UserActions'
import { Document } from '../../../../../../entities/Models/Document'
import { FileUpload } from '../../../../../../utils/helpers/FileUpload'

type DocumentItemType = {
    file: Document,
    viewDocument: Function,
    changeName: Function,
    deleteDoc: Function,
    indexKey: number,
    retry: Function,
    fileAlreadyExists: Function
}


export const DocumentItem = ({ file, viewDocument, changeName, deleteDoc, fileAlreadyExists, retry }: DocumentItemType) => {
    const [filename, setfilename] = useState<string>('');
    const [iseditable, seteditable] = useState<any>(true)
    const [nameExists, setNameExists] = useState<any>(false)
    const [isdeleted, setdeleted] = useState<any>(false)
    
    const txtInput: any = useRef(null);


    useEffect(() => {
        setfilename(FileUpload.removeSpecialChars(FileUpload.removeDefaultExt(file.clientName)))
    }, [file])


    useEffect(() => {
        if (txtInput.current) {
            txtInput.current.focus();
        }
    }, [file.editName === true])

    const rename = () => {
        seteditable(false)
        if (filename === '') {
            setNameExists(true);
            return;
        }
        let nameExists = changeName(file, filename);
        if(nameExists === false) {
            setNameExists(true);
        }
    }

    const EditTitle = () => {
        setNameExists(false);
        changeName(file, filename)
    }

    const deleteDOChandeler = () => {
        setdeleted(true)
    }
    const cancelDeleteDOC = () => {
        setdeleted(false)
    }

    const getFileSize = () => {
        let size = file.size || file.file?.size;
        if (size) {
            let inKbs = size / 1000;
            if (inKbs > 1000) {
                return `${Math.ceil(inKbs / 1000)}mb(s)`
            }
            return `${Math.ceil(inKbs)}kb(s)`;
        }
        return `${0}kbs`
    }

    const renderAllowedFile = () => {
        return (
            (
                <li className="doc-li">
                    {!isdeleted ?
                        <div className={file.editName ? "editableview doc-liWrap" : "noneditable doc-liWrap"}>
                            <div className="doc-icon">
                                <i className={file.docLogo}></i>

                            </div>
                            <div className="doc-list-content">
                                <div className="tilte">
                                    {file.editName ? <input onMouseOut={() => setNameExists(false)} style={{ border: nameExists ? '1px solid red' : 'none' }} ref={txtInput} maxLength={255} type="text" value={filename.split('.')[0]} onChange={(e) => {
                                        setNameExists(false);
                                        if (fileAlreadyExists(file, e.target.value)) {
                                            setNameExists(true);
                                        }
                                        if (FileUpload.nameTest.test(e.target.value)) {
                                            setfilename(e.target.value);
                                            return
                                        }
                                        setTimeout(() => {
                                            setNameExists(false);
                                        }, 3000);
                                       
                                        setNameExists(true);
                                        // alert('File names shoul not contain special charaters apart from "-"')
                                    }} />
                                        :
                                        <p>{file.clientName}</p>

                                    }
                                </div>
                                <div className="dl-info">
                                    <span className="dl-date">{file.fileUploadedOn}</span>
                                    <span className="dl-text-by"> by </span>
                                    <span className="dl-text-auther">{UserActions.getUserName()}</span>
                                    <span className="dl-pipe"> | </span>
                                    <span className="dl-filesize">{getFileSize()}</span>
                                </div>
                            </div>
                            <div className="doc-list-actions">
                                {file.editName ?
                                    <ul className="editable-actions">
                                        <li>
                                            <button onClick={rename} className="btn btn-primary doc-rename-btn">Save</button>
                                        </li>
                                    </ul>
                                    : <>

                                        <ul className="readable-actions">
                                            {file.file && !file.uploadProgress && <li>
                                                <a onClick={EditTitle} title="Rename" tabIndex={-1}>{<DocEditIcon />}</a>
                                            </li>}
                                            {(!file.file || file.uploadStatus === 'done')
                                                && <li>
                                                    <a onClick={() => viewDocument(file)} title="View Document" tabIndex={-1}>{<DocviewIcon />}</a>
                                                </li>}
                                            {file.file && file.uploadProgress < 100 && <li>
                                                <a title="Cancel" onClick={() => deleteDOChandeler()} tabIndex={-1}><i className="zmdi zmdi-close"></i></a>
                                            </li>}
                                            {file.uploadStatus === 'done' && <li>
                                                <a title="Uploaded" className="icon-uploaded" tabIndex={-1}><i className="zmdi zmdi-check"></i></a>
                                            </li>}

                                        </ul>
                                    </>
                                }

                            </div>
                        </div>
                        : <>
                            <div className="document-confirm-wrap">
                                <div className="row">
                                    <div className="col-sm-7">
                                        <div className="dc-text">
                                            <p>Are you sure to delete this file?</p>

                                        </div>

                                    </div>

                                    <div className="col-sm-5">
                                        <div className="dc-actions">
                                            <button className="btn btn-small btn-secondary" onClick={() => cancelDeleteDOC()} >No</button>
                                            <button className="btn btn-small btn-primary" onClick={() => {
                                                deleteDoc(file.clientName);
                                                cancelDeleteDOC();
                                            }}>Yes</button>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </>
                    }
                    {(file.file && file.uploadProgress < 100 && file.uploadProgress > 0) ? <div className="progress-upload" style={{ width: file.uploadProgress + "%" }}></div> : ''}

                </li>
            )
        )
    }

    const renderNotAllowedFile = () => {
        return (
            <li className="doc-li">
                this file is not allowed
                <button onClick={() => {
                    retry(file)
                }}>retry</button>
            </li>
        )
    }

    if (file.notAllowed) {
        return renderNotAllowedFile();
    }

    return renderAllowedFile()
}
