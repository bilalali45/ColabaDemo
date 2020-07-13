import React from 'react';
import {DocumentSnipet} from './DocumentSnipet/DocumentSnipet';

export const ReviewDocumentStatement = () => {
    return (
        <div id="ReviewDocumentStatement" data-component="ReviewDocumentStatement" className="document-statement">
            <header className="document-statement--header">
                <h2>Bank Statement</h2>
            </header>
            <section className="document-statement--body">
                <h3>Documents</h3>
                
                <DocumentSnipet status="active" />
                <DocumentSnipet />

                <hr/>

                <div className="clearfix">
                    <span className="float-right activity-logs-icon"><em className="icon-letter"></em></span>
                    <h3>Activity Logs</h3>
                </div>

            </section>
        </div>
    )
}