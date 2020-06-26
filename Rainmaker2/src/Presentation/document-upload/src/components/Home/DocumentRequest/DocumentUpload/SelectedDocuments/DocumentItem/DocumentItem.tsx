import React, { ChangeEvent, useState, useRef, useEffect } from 'react'
import { FileSelected } from '../../DocumentUpload'
import { DocEditIcon, DocviewIcon } from '../../../../../../shared/Components/Assets/SVG'
import { UserActions } from '../../../../../../store/actions/UserActions'
import { formatBytes } from '../../../../../../utils/helpers/FileConversion'
import { DateFormat } from '../../../../../../utils/helpers/DateFormat'

type DocumentItemType = {
    file: FileSelected,
    viewDocument: Function,
    changeName: Function,
}
export const DocumentItem = ({ file, viewDocument, changeName }: DocumentItemType) => {
    
    const RemoveExtension = () => {
        if(file == undefined || file.name == undefined)
        return '';
        let splitData = file.name.split('.');
        let onlyName = "";
        for (let i = 0; i < splitData.length - 1; i++) {
            if (i != splitData.length - 2)
                onlyName += splitData[i] + '.';
            else
                onlyName += splitData[i];
        }
        return onlyName != "" ? onlyName : file.name ;
    }

    const [filename, setfilename] = useState<string>(file.name);
    const [filenameWithoutExt, setOnlyFilename] = useState<string>(RemoveExtension());
    const [iseditable, seteditable] = useState<any>(true)
    const [isdeleted, setdeleted] = useState<any>(false)
    const tokenData: any = UserActions.getUserInfo();

    const displayName = tokenData?.FirstName+' '+tokenData?.LastName;
    const modifiedDate = file ? DateFormat(file.file.lastModified.toString(), true) : '';
    const fileSize = file ? formatBytes(file.file.size, 0) : '';
   
    const getExtension = () => {
        return file.file.type.split('/')[1];
    }

    const Rename = () => {
        seteditable(false)
        changeName(file, filenameWithoutExt);
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
    const renderFileLogo = () => {
        let ext = getExtension();
        if(ext === 'pdf')
            return <i className="far fa-file-pdf"></i>
        else
            return <i className="far fa-file-image"></i>   
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
                        {renderFileLogo()}
                    </div>
                    <div className="doc-list-content">
                        <div className="tilte">
                            <input readOnly={iseditable ? false : true} type="text" value={iseditable ? filenameWithoutExt : filenameWithoutExt+'.'+getExtension()} onChange={(e) => {
                                setOnlyFilename(e.target.value) 
                                
                            }} />

                            {/* <p>{file.name}</p> */}
                        </div>
                        <div className="dl-info">
                        <span className="dl-date">{modifiedDate}</span>
                            <span className="dl-text-by"> by </span>
                            <span className="dl-text-auther">{displayName}</span>
                            <span className="dl-pipe"> | </span>
                        <span className="dl-filesize">{fileSize} kb</span>
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
