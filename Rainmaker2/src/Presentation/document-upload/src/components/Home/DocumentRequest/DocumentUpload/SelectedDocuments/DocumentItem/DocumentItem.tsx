import React, { ChangeEvent, useState, useRef, useEffect } from 'react'
import { DocEditIcon, DocviewIcon } from '../../../../../../shared/Components/Assets/SVG'
import { UserActions } from '../../../../../../store/actions/UserActions'
import { formatBytes } from '../../../../../../utils/helpers/FileConversion'
import { DateFormat } from '../../../../../../utils/helpers/DateFormat'
import { Document } from '../../../../../../entities/Models/Document'
import moment from 'moment';

type DocumentItemType = {
    file: Document,
    viewDocument: Function,
    changeName: Function,
    deleteDoc: Function
}
// export const DocumentItem = ({ file, viewDocument, changeName }: DocumentItemType) => {
    
    // const RemoveExtension = () => {
    //     if(file == undefined || file.name == undefined)
    //     return '';
    //     let splitData = file.name.split('.');
    //     let onlyName = "";
    //     for (let i = 0; i < splitData.length - 1; i++) {
    //         if (i != splitData.length - 2)
    //             onlyName += splitData[i] + '.';
    //         else
    //             onlyName += splitData[i];
    //     }
    //     return onlyName != "" ? onlyName : file.name ;
    // }

    // const [filename, setfilename] = useState<string>(file.name);
    // const [filenameWithoutExt, setOnlyFilename] = useState<string>(RemoveExtension());
    // const [iseditable, seteditable] = useState<any>(true)
    // const [isdeleted, setdeleted] = useState<any>(false)
    // const tokenData: any = UserActions.getUserInfo();

    // const displayName = tokenData?.FirstName+' '+tokenData?.LastName;
    // const modifiedDate = file ? DateFormat(file.file.lastModified.toString(), true) : '';
    // const fileSize = file ? formatBytes(file.file.size, 0) : '';
   
    

    // const Rename = () => {


const removeSpecialChars = (text: string) => {
    return text.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '')
}

const removeDefaultExt = (fileName: string) => {
    //return fileName.split('.')[0]
        let splitData = fileName.split('.');
        let onlyName = "";
        for (let i = 0; i < splitData.length - 1; i++) {
            if (i != splitData.length - 2)
                onlyName += splitData[i] + '.';
            else
                onlyName += splitData[i];
        }
        return onlyName != "" ? onlyName : fileName ;
}

let nameTest = /^[ A-Za-z0-9-\s]*$/i;


export const DocumentItem = ({ file, viewDocument, changeName, deleteDoc }: DocumentItemType) => {
    
    const [filename, setfilename] = useState<string>(removeSpecialChars(removeDefaultExt(file.clientName)));
    const [iseditable, seteditable] = useState<any>(true)
    const [isdeleted, setdeleted] = useState<any>(false)
    const todayDate = moment().format('MMM DD, YYYY hh:mm A')

    const rename = () => {
        seteditable(false)
        changeName(file, filename);
    }
    const EditTitle = () => {
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

    return (
        <li className="doc-li">
            {/* <div className="editableview"></div>
            <button onClick={() => viewDocument(file)}>View</button>
            <input type="text" onChange={(e) => changeName(file, e.target.value)}/>
            */}
            {!isdeleted ?
                <div className={file.editName ? "editableview doc-liWrap" : "noneditable doc-liWrap"}>
                    <div className="doc-icon">
                        <i className={file.docLogo}></i>
                        
                    </div>
                    <div className="doc-list-content">
                        <div className="tilte">
                            {file.editName ? <input onBlur={rename} type="text" value={filename.split('.')[0]} onChange={(e) => {
                                if (nameTest.test(e.target.value)) {
                                    setfilename(e.target.value);
                                    return
                                }
                                alert('File names shoul not contain special charaters apart from "-"')
                            }} />
                                :
                                <p>{file.clientName}</p>
                               
                             }    
                        </div>
                        <div className="dl-info">
                            <span className="dl-date">{file.fileUploadedOn ? DateFormat(file.fileUploadedOn, true) : todayDate}</span>
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
                            <div className="col-sm-8">
                                <div className="dc-text">
                                    <p>Are you sure to delete this file?</p>

                                </div>

                            </div>

                            <div className="col-sm-4">
                                <div className="dc-actions">
                                    <button className="btn btn-small btn-secondary" onClick={() => cancelDeleteDOC()} >No</button>
                                    <button className="btn btn-small btn-primary" onClick={() => deleteDoc(filename)}>Yes</button>
                                </div>
                            </div>

                        </div>

                    </div>
                </>
            }
{(file.file && file.uploadProgress < 100 && file.uploadProgress > 0) ? <div className="progress-upload" style={{width:file.uploadProgress + "%"}}></div> : ''}




        </li>
    )
}
