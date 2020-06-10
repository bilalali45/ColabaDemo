import React from 'react'
import { LoanStatus } from '../Activity/LoanStatus/LoanStatus'
const ActivityHeader = () => {
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
                                            <a href="javascript:"  ><i className="zmdi zmdi-arrow-left"></i> Dashboard</a>
                                        </li>
                                    </ul>
                                </div>
                                <div className="col-6 text-right">

                                    <div className="action-doc-upload">
                                        <a href="javascript:" >
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