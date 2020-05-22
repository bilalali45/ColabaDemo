import React from 'react'
import { DocumentDropBox } from '../../../../shared/Components/DocumentDropBox/DocumentDropBox'
import { SelectedDocuments } from './SelectedDocuments/SelectedDocuments'

export const DocumentUpload = () => {
    return (
        <div>
            <h1>Document Upload</h1>
            <DocumentDropBox/>
            <SelectedDocuments/>
        </div>
    )
}
