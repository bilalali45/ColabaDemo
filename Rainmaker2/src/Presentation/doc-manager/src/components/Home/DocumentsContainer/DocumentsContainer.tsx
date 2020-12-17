import React from 'react'
import { DocumentsHeader } from './DocumentsHeader/DocumentsHeader'
import { DocumentsTableView } from './DocumentTableView/DocumentsTableView'
import { WorkbenchView } from './WorkbenchView/WorkbenchView'

export const DocumentsContainer = () => {
    return (
        <div className="c-DocContainer">
            <DocumentsHeader/>
            <DocumentsTableView/>
            <WorkbenchView/>
        </div>
    )
}
