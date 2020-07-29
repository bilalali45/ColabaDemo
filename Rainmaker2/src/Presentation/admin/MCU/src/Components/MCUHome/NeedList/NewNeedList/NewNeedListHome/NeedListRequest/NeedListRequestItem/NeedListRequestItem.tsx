import React, { useState, useContext, useEffect } from 'react'
import { Document } from '../../../../../../../Entities/Models/Document';
import { DocumentRequest } from '../../../../../../../Entities/Models/DocumentRequest';
import { TemplateDocument } from '../../../../../../../Entities/Models/TemplateDocument';

type NeedListRequestItemType = {
    changeDocument: Function,
    document: TemplateDocument,
    isSelected: boolean
}

export const NeedListRequestItem = ({document, changeDocument, isSelected}: NeedListRequestItemType) => {
    const [toRemoveList, setRemoveList] = useState<boolean>(false);
    return (
         <li
            onClick={() => changeDocument(document)}>
            <div className="l-wrap">
                {!toRemoveList ?
                    <div className={`c-list ${isSelected ? 'active' : ''}`}>
                    {document.docName}
                            <span className="BTNclose" onClick={() => {}}><i className="zmdi zmdi-close"></i></span>
                    </div>
                    : <>
                        <div className="alert-cancel">
                            <span>Remove this template?</span>
                            <div className="l-remove-actions">
                                <button className="lbtn btn-no" onClick={() => {}}> No</button>
                                <button className="lbtn btn-yes" onClick={() => {}}>Yes</button></div>
                        </div>
                    </>
                }


            </div>
         </li>
    )
}
