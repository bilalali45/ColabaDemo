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
            <div className="row sub-header-wrap">
                <div className="col-6">
                Dashboard
                </div>
                <div className="col-6 text-right">
                Uploaded Document
                </div>
            </div>
            </div>
            </div>
            </section>
            
        </div>
    )
}

export default ActivityHeader;