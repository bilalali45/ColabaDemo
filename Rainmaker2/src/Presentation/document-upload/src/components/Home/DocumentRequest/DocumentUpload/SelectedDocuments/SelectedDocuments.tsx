import React from 'react'
import { DocumentItem } from './DocumentItem/DocumentItem'
import { DocumentView } from '../../../../../shared/Components/DocumentView/DocumentView'

export const SelectedDocuments = () => {
    return (
        <div>
            <h1>Selected Documents</h1>
            <DocumentItem/>
            <DocumentView/>
        </div>
    )
}
