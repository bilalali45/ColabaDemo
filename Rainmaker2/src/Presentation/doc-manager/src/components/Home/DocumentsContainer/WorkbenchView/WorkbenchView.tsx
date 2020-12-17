import React from 'react'
import { WorkbenchTable } from './WorkbenchTable/WorkbenchTable'

export const WorkbenchView = () => {
    return (
        <div className="c-WorkbenchView">
            <div className="wb-head">
                <h2>Uncategorized</h2>
            </div>
            <WorkbenchTable/>
        </div>
    )
}
