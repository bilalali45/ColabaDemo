import React from 'react'
import { DocumentsRequired } from './DocumentsRequired/DocumentsRequired'
import { DocumentUpload } from './DocumentUpload/DocumentUpload'

export const DocumentRequest = () => {
    return (
        <div>
            <p>Document Requests</p>
            <DocumentsRequired/>
            <DocumentUpload/>
        </div>
    )
}
