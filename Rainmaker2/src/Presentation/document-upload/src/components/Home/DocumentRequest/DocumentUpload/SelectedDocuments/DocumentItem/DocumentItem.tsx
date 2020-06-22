import React, { ChangeEvent ,useState, useRef, useEffect } from 'react'
import { FileSelected } from '../../DocumentUpload'
type DocumentItemType = {
    file: FileSelected,
    viewDocument: Function,
    changeName: Function,
}
export const DocumentItem = ({file, viewDocument, changeName }: DocumentItemType) => {
    // export const DocumentItem = () => {
        const [filename,setfilename] = useState<string>("");
        useEffect(() => {
            setfilename(file.name);
        })
    return (
        <li className="doc-li">
            {/* <div className="editableview"></div>
            <button onClick={() => viewDocument(file)}>View</button>
            <input type="text" onChange={(e) => changeName(file, e.target.value)}/>
            */}

            <div className="noneditable">
              <div className="doc-icon">
              <i className="far fa-file-pdf"></i>
              </div>
              <div className="doc-list-content">
                <div className="tilte">
                <input type="text" value={filename}  onChange={(e) => changeName(file, e.target.value)}/>
                
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
                    <ul>
                        <li>
                            <button className="btn btn-primary">save</button>
                        </li>
                    </ul>
                </div>
            </div>
            
            



        </li>
    )
}
