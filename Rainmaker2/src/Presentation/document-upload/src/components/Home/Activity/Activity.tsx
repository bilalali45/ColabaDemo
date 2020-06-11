import React from 'react'
// import { LoanStatus } from './LoanStatus/LoanStatus'
import { LoanProgress } from './LoanProgress/LoanProgress'
import { DocumentStatus } from './DocumentStatus/DocumentStatus'
import { ContactUs } from './ContactUs/ContactUs'
import { LoanStatus } from './LoanStatus/LoanStatus'

export const Activity = () => {
    return (
        <div>
            <section className="page-content">
                <div className="container">
                    <div className="row gutter15">
                        <div className="col-md-5">
                            <LoanStatus />
                            <LoanProgress />
                        </div>
                        <div className="col-md-7">
                            {/* <DocumentStatus /> */}
                            <ContactUs />
                        </div>
                    </div>
                </div>
            </section>
        </div>
    );
}
