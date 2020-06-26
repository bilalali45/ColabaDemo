import React, { ChangeEvent, useState, useRef, useEffect } from 'react'
import { DocEditIcon, DocviewIcon } from '../../../../../../shared/Components/Assets/SVG'
import { Document } from '../../../../../../entities/Models/Document'
type DocumentItemType = {
    file: Document,
    viewDocument: Function,
    changeName: Function,
}
export const DocumentItem = ({ file, viewDocument, changeName }: DocumentItemType) => {
    // export const DocumentItem = () => {
    const [filename, setfilename] = useState<string>(file.clientName);
    const [iseditable, seteditable] = useState<any>(true)
    const [isdeleted, setdeleted] = useState<any>(false)
    
    const Rename = () => {
        seteditable(false)
        changeName(file, filename);
    }
    const EditTitle = () => {

        seteditable(true)
    }

    const DeleteDOChandeler = () => {
        setdeleted(true)
    }
    const CancelDeleteDOC = () => {
        setdeleted(false)
    }

    return (
        <li className="doc-li">
            {/* <div className="editableview"></div>
            <button onClick={() => viewDocument(file)}>View</button>
            <input type="text" onChange={(e) => changeName(file, e.target.value)}/>
            */}
            {!isdeleted ?
                <div className={iseditable ? "editableview doc-liWrap" : "noneditable doc-liWrap"}>
                    <div className="doc-icon">
                        <i className="far fa-file-image"></i>
                    </div>
                    <div className="doc-list-content">
                        <div className="tilte">
                            <input readOnly={iseditable ? false : true} type="text" value={filename} onChange={(e) => {
                                setfilename(e.target.value);
                            }} />

                            {/* <p>{file.name}</p> */}
                        </div>
                        <div className="dl-info">
                            <span className="dl-date">May 28, 2020  17:30</span>
                            <span className="dl-text-by"> by </span>
                            <span className="dl-text-auther">Hussain</span>
                            <span className="dl-pipe"> | </span>
                            <span className="dl-filesize">415 kb</span>
                        </div>
                    </div>
                    <div className="doc-list-actions">
                        {iseditable ?
                            <ul className="editable-actions">
                                <li>
                                    <button onClick={Rename} className="btn btn-primary doc-rename-btn">Save</button>
                                </li>
                            </ul>
                            : <>

                                <ul className="readable-actions">
                                    <li>
                                        <a onClick={EditTitle} title="Rename" tabIndex={-1}>{<DocEditIcon />}</a>
                                    </li>
                                    <li>
                                        <a onClick={() => viewDocument(file)} title="View Document" tabIndex={-1}>{<DocviewIcon />}</a>
                                    </li>
                                    <li>
                                        <a title="Cancel" onClick={() => DeleteDOChandeler()} tabIndex={-1}><i className="zmdi zmdi-close"></i></a>
                                    </li>
                                    <li>
                                        <a title="Uploaded" className="icon-uploaded" tabIndex={-1}><i className="zmdi zmdi-check"></i></a>
                                    </li>
                                </ul>
                            </>
                        }

                    </div>
                </div>
                : <>
                    <div className="document-confirm-wrap">
                        <div className="row">
                            <div className="col-sm-8">
                                <div className="dc-text">
                                    <p>Are you sure to delete this file?</p>

                                </div>

                            </div>

                            <div className="col-sm-4">
                                <div className="dc-actions">
                                    <button className="btn btn-small btn-secondary" onClick={() => CancelDeleteDOC()} >No</button>
                                    <button className="btn btn-small btn-primary">Yes</button>
                                </div>
                            </div>

                        </div>

                    </div>
                </>
            }






        </li>
    )
}
