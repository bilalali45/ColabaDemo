import React from 'react'
import { DocumentTypes } from './DocumentTypes/DocumentTypes'
import { CommonDocuments } from './CommonDocuments/CommonDocuments'

export const AddDocument = () => {
    return (
        <div>
            <h1>AddDocument</h1>
            <DocumentTypes/>
            <CommonDocuments/>
        </div>
    )
}
