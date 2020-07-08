import React, { useContext, useRef, useEffect } from 'react'
import { DocumentsRequired } from './DocumentsRequired/DocumentsRequired'
import { DocumentUpload } from './DocumentUpload/DocumentUpload'
import { Store } from '../../../store/store';

export const DocumentRequest = () => {
    const { state, dispatch } = useContext(Store);
    const { pendingDocs, currentDoc }: any = state.documents;
    let pendingDocsCount = pendingDocs ? pendingDocs.length : 0;
    const selectedFiles = currentDoc?.files || [];

    const sideBarNav = useRef<HTMLDivElement>(null);

    useEffect(() => {
        let files = selectedFiles.filter(f => f.file && f.uploadStatus === 'pending').length > 0;
        const showAlert = () => {
            // alert('in here!!1');
        };
        if (sideBarNav.current) {
            if (files) {
                sideBarNav.current.addEventListener('click', showAlert, false);
            } else {
                sideBarNav.current?.removeEventListener('click', showAlert, false);
            }
        }
    }, [selectedFiles])

    return (
        <main className="dr-upload">
            <section className="dr-upload--header">
                <div className="row">
                    <article className="col-sm-12">
                        <div className="dr-head">
                            <h2 className="heading-h2"> Document Request</h2>
                            {pendingDocsCount ? <p>You have <span className="DocumentStatus--count">{pendingDocsCount}</span> items to complete</p> : ''}
                        </div>
                    </article>
                </div>
            </section>
            <section className="dr-c-wrap">
                <div className="row">
                    <aside className="col-sm-4">
                        <div ref={sideBarNav} className="dr-asideWrap">
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
