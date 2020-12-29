import React, { useState, useRef, useEffect } from 'react';
import { SVGFileEdit } from '../SVGs';

interface FileWidgetProps {
    mode?: string; // view - edit
    fileName: string;
    uploadProgress?: any;
    handlerEditFileName: Function;
    handlerViewDocument?: () => void;
    handlerDeleteFile?: () => void;
}

const FileWidget: React.FC<FileWidgetProps> = ({ mode, fileName, uploadProgress, handlerEditFileName, handlerViewDocument, handlerDeleteFile }) => {

    const _mode = mode ? mode : 'view'; // view - edit
    const [widgetMode, setWidgetMode] = useState<any>(_mode);

    const refInput = useRef<any>(null);
  

    return (
        <div className="doc-li" data-testid="file-item">
            <div className="noneditable doc-liWrap">
                <div className="doc-icon"><i className="far fa-file-image"></i></div>
                <div data-testid="file-container-0" className="doc-list-content">
                    <div className="title">
                        {widgetMode == 'edit' &&
                            <input autoFocus ref={refInput} 
                                type="text" 
                                value={fileName} 
                                onChange={(e) => handlerEditFileName(e)} 
                                //onBlur={(e) =>setWidgetMode('view')}
                                />
                        }
                        {(widgetMode == 'view' || !mode == undefined) &&
                            <p title={fileName}>{fileName}</p>
                        }
                    </div>
                </div>
                <div className="doc-list-actions">

                    {widgetMode == 'view' &&
                        <ul className="readable-actions">
                            <li>
                                <a title="Edit File Name" onClick={() =>{setWidgetMode('edit');}}>
                                    <SVGFileEdit />
                                </a>
                            </li>
                            {handlerViewDocument &&
                                <li>
                                    <a title="View Document" onClick={handlerViewDocument}>
                                        <svg data-testid="DocviewIcon" xmlns="http://www.w3.org/2000/svg" width="14.137" height="17.914" viewBox="0 0 14.137 17.914"><g id="doc-file-view-icon" transform="translate(408.95 33.347)"><g id="Group_434" data-name="Group 434" transform="translate(-10)"><path id="Path_334" data-name="Path 334" d="M-384.963-24.439q0,3.748,0,7.5a1.262,1.262,0,0,1-1.309,1.317h-11.174a1.259,1.259,0,0,1-1.353-1.35q0-5.961,0-11.922a1.294,1.294,0,0,1,.391-.955q1.5-1.494,2.991-2.991a1.314,1.314,0,0,1,.972-.4h8.125a1.263,1.263,0,0,1,1.358,1.365Q-384.962-28.157-384.963-24.439Zm-12.593-4.41v.222q0,5.676,0,11.352c0,.3.108.406.4.406h10.525c.324,0,.424-.1.424-.422q0-7.151,0-14.3c0-.293-.109-.4-.4-.4H-394.2c-.063,0-.126.006-.2.011v.228c0,.551.005,1.1,0,1.653a1.228,1.228,0,0,1-.885,1.191,1.937,1.937,0,0,1-.5.062C-396.376-28.844-396.952-28.849-397.556-28.849Z" transform="translate(0 0.044)" fill="#7e829e" stroke="#7e829e" stroke-width="0.3"></path><path id="search_1_" data-name="search (1)" d="M6.332,5.571a3.518,3.518,0,1,0-.762.763l2.3,2.3.763-.763-2.3-2.3Zm-2.827.363A2.427,2.427,0,1,1,5.932,3.507,2.429,2.429,0,0,1,3.505,5.934Z" transform="translate(-396.109 -27.699)" fill="#7e829e" stroke="#7e829e" stroke-width="0.1"></path></g></g></svg>
                                    </a>
                                </li>
                            }
                            {handlerDeleteFile &&
                                <li>
                                    <a data-testid="done-upload" title="Delete Document" className="icon-delete" onClick={handlerDeleteFile}>
                                        <i className="zmdi zmdi-close"></i>
                                    </a>
                                </li>
                            }
                        </ul>
                    }

                    {widgetMode == 'edit' &&
                        <button onClick={()=>{setWidgetMode('view')}} className={`btn btn-sm btn-primary`}>Save</button>
                    }

                </div>
            </div>
            {uploadProgress &&
                <div className="progress-upload" style={{ width: `${uploadProgress}` }}></div>
            }
        </div>
    )
}

export default FileWidget;
