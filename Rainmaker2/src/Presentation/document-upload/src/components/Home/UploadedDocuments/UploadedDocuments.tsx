import React from 'react'
import { UploadedDocumentsTable } from './UploadedDocumentsTable/UploadedDocumentsTable'
import { DocumentView } from '../../../shared/Components/DocumentView/DocumentView'

export const UploadedDocuments = () => {
    return (
        <div className="UploadedDocuments box-wrap">
            <div className="box-wrap--header">
                <h2>Uploaded Documents</h2>
            </div>
            <div className="box-wrap--body clearfix">
                <UploadedDocumentsTable/>
            </div>
        </div>
    )
}
