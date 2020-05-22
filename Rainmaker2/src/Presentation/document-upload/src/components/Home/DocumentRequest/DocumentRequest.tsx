import React from 'react'
import { DocumentsRequired } from './DocumentsRequired/DocumentsRequired'
import { DocumentUpload } from './DocumentUpload/DocumentUpload'

export const DocumentRequest = () => {
    return (
        <div>
            <h1>Document Requests</h1>
            <DocumentsRequired/>
            <DocumentUpload/>
        </div>
    )
}
