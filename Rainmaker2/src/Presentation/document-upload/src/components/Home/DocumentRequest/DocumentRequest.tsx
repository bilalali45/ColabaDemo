import React, { useContext, useRef, useEffect, useState } from 'react'
import { DocumentsRequired } from './DocumentsRequired/DocumentsRequired'
import { DocumentUpload } from './DocumentUpload/DocumentUpload'
import { Store } from '../../../store/store';
import { AlertBox } from '../../../shared/Components/AlertBox/AlertBox';

export const DocumentRequest = () => {
    const { state, dispatch } = useContext(Store);
    const { pendingDocs, currentDoc }: any = state.documents;
    let pendingDocsCount = pendingDocs ? pendingDocs.length : 0;
    
    return (
        <main className="dr-upload" data-testid="task-list">
            <section className="dr-upload--header">
                <div className="row">
                    <article className="col-sm-12">
                        <div className="dr-head">
                            <h2 className="heading-h2">Task List</h2>
                            {pendingDocsCount ? <p>You have <span className="DocumentStatus--count">{pendingDocsCount}</span> items to complete</p> : ''}
                        </div>
                    </article>
                </div>
            </section>
            <section className="dr-c-wrap">
                <div className="row">
                    <aside className="col-sm-4">
                        <div className="dr-asideWrap">
                            <DocumentsRequired />
                        </div>
                    </aside>
                    <article className="col-sm-8">
                        <DocumentUpload />
                    </article>

                </div>

            </section>
           
        </main>
    )
}
