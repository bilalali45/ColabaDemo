import React from 'react'
import { CommonDocuments } from './CommonDocuments/CommonDocuments'
import { SelectedDocumentTypeList } from './SelectedDocumentTypeList/SelectedDocumentTypeList'
import { CustomDocuments } from './CustomDocuments/CustomDocuments'
import { Document } from '../../../../../Entities/Models/Document'
import { Template } from '../../../../../Entities/Models/Template'

type SelectedTypeType = {
    documentList: Document[],
    addNewDoc: Function,
}

export const SelectedType = ({documentList, addNewDoc} : SelectedTypeType) => {
    return (
        <div>
            {/* <CommonDocuments/>
            <CustomDocuments/> */}
            <SelectedDocumentTypeList
               documentList={documentList}
               addNewDoc={addNewDoc} />
        </div>
    )
}
