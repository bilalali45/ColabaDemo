import React from 'react'
import { Document } from '../../../../../../Entities/Models/Document'
import { TemplateActions } from '../../../../../../Store/actions/TemplateActions'


type SelectedTypeType = {
    documentList: Document[],
    addNewDoc: Function
}

export const SelectedDocumentTypeList = ({ documentList, addNewDoc }: SelectedTypeType) => {

    if (!documentList) {
        return null;
    }

    return (

            <div className="active-docs"> 
            <ul>
                {documentList &&
                    documentList?.map(dl => {
                        return (
                            <li onClick={() => addNewDoc(dl.docTypeId, 'typeId')}>{dl?.docType}</li>
                        )
                    })
                }
                
            </ul>
            {!documentList.length && <div className="doc-notfound"><p>No Results Found for “Page 5 0f 5 Case checking  account”</p></div>}
            </div>
    )
}
