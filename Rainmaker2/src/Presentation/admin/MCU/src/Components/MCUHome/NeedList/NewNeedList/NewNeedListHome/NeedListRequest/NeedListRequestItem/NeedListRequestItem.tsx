import React, { useState, useContext, useEffect } from 'react'
import { Document } from '../../../../../../../Entities/Models/Document';
import { DocumentRequest } from '../../../../../../../Entities/Models/DocumentRequest';
import { TemplateDocument } from '../../../../../../../Entities/Models/TemplateDocument';

type NeedListRequestItemType = {
    changeDocument: Function,
    document: TemplateDocument,
    isSelected: boolean,
    removeDocumentFromList: Function
}

export const NeedListRequestItem = ({ document, changeDocument, isSelected, removeDocumentFromList }: NeedListRequestItemType) => {
    const [toRemoveList, setRemoveList] = useState<boolean>(false);
    //const [getDelete, setDelete] = useState<boolean>(false);

    useEffect(() => {
        setRemoveList(false);
    }, [!isSelected])

    
    return (
         <li>           
            <div className="l-wrap" onClick={() => changeDocument(document)}>
                {!toRemoveList ?
                    <div className={`c-list ${isSelected ? 'active' : ''}`}>
                       <span className="text-ellipsis" title={document.docName}>{document.docName}</span>
                        {isSelected && <span className="BTNclose" onClick={() => {
                                    removeDocumentFromList(document)
                                    
                                }}><i className="zmdi zmdi-close"></i></span>}
                    </div>
                    : <>
                        {<div className="alert-cancel">
                            <span>Remove this document?</span>
                            <div className="l-remove-actions">
                                <button className="lbtn btn-no" onClick={() => { setRemoveList(false) }}> No</button>
                                <button className="lbtn btn-yes" onClick={() => {
                                    removeDocumentFromList(document)
                                    setRemoveList(false);
                                }}>Yes</button></div>
                        </div>}
                    </>
                }


            </div>
        </li>
    )
}
