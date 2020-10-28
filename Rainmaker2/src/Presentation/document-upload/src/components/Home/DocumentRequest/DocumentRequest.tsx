import React, { useContext, useRef, useEffect, useState } from 'react'
import { DocumentsRequired } from './DocumentsRequired/DocumentsRequired'
import { DocumentUpload } from './DocumentUpload/DocumentUpload'
import { Store } from '../../../store/store';
import { AlertBox } from '../../../shared/Components/AlertBox/AlertBox';

export const DocumentRequest = () => {

    const [currentInView, setCurrentInView] = useState('documetsRequired');
    const [docReqClass, setdocReqClass] = useState('');

    const { state, dispatch } = useContext(Store);
    const { pendingDocs, currentDoc }: any = state.documents;
    let pendingDocsCount = pendingDocs ? pendingDocs.length : 0;
    const loan: any = state.loan;
    const { isMobile } = loan;


    const desktopView = () => {
        return (
            <section className="dr-c-wrap">
                <div className="row">
                    <aside className="col-xs-12 col-md-4">
                        <div className="dr-asideWrap">
                            <DocumentsRequired />
                        </div>
                    </aside>
                    <article className="col-xs-12 col-md-8">
                        <DocumentUpload />
                    </article>

                </div>

            </section>
        )
    }

  const  getClass =(cls) =>{
    setdocReqClass(cls)
    }
    
    const mobileView = () => {
        return (
            <section className={`dr-c-wrap ${docReqClass} ${currentInView === 'documetsRequired' ? "PageDocListView":"PageDocUploadView"}`}>
                <div className="row">
                    {currentInView === 'documetsRequired' ? <aside className="col-xs-12 col-md-4">
                        <div className="dr-asideWrap">
                            <DocumentsRequired 
                                setCurrentInview={setCurrentInView} 
                                setClass={getClass}/>
                        </div>
                    </aside> :
                        <article className="col-xs-12 col-md-8">
                            <DocumentUpload  setCurrentInview={setCurrentInView}  />
                        </article>
                    }
                </div>

            </section>
        )
    }

    return (


        <main className="dr-upload" data-testid="task-list">
            <section className="dr-upload--header">
                <div className="row">
                    <article className="col-sm-12">
                        <div className="dr-head">
                            <h2 className="heading-h2"> 
                            {isMobile?.value && currentInView === 'documentUploadView' && <div className="dr-head-back-arrow" onClick={() => setCurrentInView('documetsRequired')}><span><i className="zmdi zmdi-arrow-left"></i>Back</span></div>} 
                            Task List</h2>
                            {pendingDocsCount ? <p>You have <span className="DocumentStatus--count">{pendingDocsCount}</span> {pendingDocsCount == 1 ? "item" : "items"} to complete</p> : ''}
                        </div>
                    </article>
                </div>
            </section>

            {!isMobile.value ?
                desktopView() : mobileView()
            }


        </main>
    )
}
