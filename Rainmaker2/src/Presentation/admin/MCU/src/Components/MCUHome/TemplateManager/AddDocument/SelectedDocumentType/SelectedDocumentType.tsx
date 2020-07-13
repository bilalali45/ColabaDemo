import React from 'react'
import { CommonDocuments } from './CommonDocuments/CommonDocuments'
import { SelectedDocumentTypeList } from './SelectedDocumentTypeList/SelectedDocumentTypeList'
import { CustomDocuments } from './CustomDocuments/CustomDocuments'

export const SelectedType = () => {
    return (
        <div>
            <CommonDocuments/>
            <CustomDocuments/>
            <SelectedDocumentTypeList/>
        </div>
    )
}
