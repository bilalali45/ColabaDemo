import React from 'react'
import { useHistory } from 'react-router-dom';
import { LoanStatus } from '../Activity/LoanStatus/LoanStatus'

const ActivityHeader = () => {
    const history = useHistory();
    const uploadDocumentHandler = () => {
        history.push('/uploadedDocuments')
    }
    const gotoDashboardHandler = () => {
        window.open('/Dashboard', '_self');
    }
    return (
        <div className="activityHeader">
            <section className="compo-loan-status">
                <LoanStatus />
            </section>
            <section className="row-subheader">
                <div className="row">
                    <div className="container">
                        <div className="sub-header-wrap">
                            <div className="row">
                                <div className="col-6">
                                    <ul className="breadcrmubs">
                                        <li>
                                            <a onClick={gotoDashboardHandler} ><i className="zmdi zmdi-arrow-left"></i> Dashboard</a>
                                        </li>
                                    </ul>
                                </div>
                                <div className="col-6 text-right">

                                    <div className="action-doc-upload">
                                        <a onClick={uploadDocumentHandler} >
                                            Uploaded Document
                                  </a>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

        </div>
    )
}

export default ActivityHeader;