import React from 'react'
import { LoanStatus } from './LoanStatus/LoanStatus'
import { LoanProgress } from './LoanProgress/LoanProgress'
import { DocumentStatus } from './DocumentsStatus/DocumentStatus'
import { ContactUs } from './ContactUs/ContactUs'

// 3.7.5

export const Activity = () => {
    return (
        <div>
            <section className="page-content">
                <div className="container">
                    <div className="row gutter15">
                        <div className="col-md-5">
                            <LoanStatus />
                        </div>
                        <div className="col-md-7">
                            <DocumentStatus />
                        </div>
                    </div>
                </div>
            </section>
            {/* <h1>Activity</h1>
            <LoanStatus/>
            <LoanProgress/>
            <DocumentStatus/>
            <ContactUs/> */}
        </div>
    )
}
