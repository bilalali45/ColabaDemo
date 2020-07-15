import React from 'react'
import { Document } from '../../../../../../Entities/Models/Document'
import { TemplateActions } from '../../../../../../Store/actions/TemplateActions'


type SelectedTypeType = {
    documentList: Document[],
    addNewDoc: Function
}

export const SelectedDocumentTypeList = ({ documentList, addNewDoc }: SelectedTypeType) => {

    return (
        <div>
            <ul>
                {documentList &&
                    documentList?.map(dl => {
                        return (
                            <li onClick={() => addNewDoc(dl.docTypeId)}>{dl?.docType}</li>
                        )
                    })
                }
            </ul>
        </div>
    )
}
