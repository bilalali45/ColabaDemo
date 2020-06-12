import React from 'react'
// import { LoanStatus } from './LoanStatus/LoanStatus'
import { LoanProgress } from './LoanProgress/LoanProgress'

import { ContactUs } from './ContactUs/ContactUs'
import { LoanStatus } from './LoanStatus/LoanStatus'

export const Activity = () => {
    return (
        <div>
            <section className="page-content">
                <div className="container">
                    <div className="row gutter15">
                        <div className="col-md-5">
                            <LoanProgress />
                        </div>
                        <div className="col-md-7">
                            <ContactUs />
                        </div>
                    </div>
                </div>
            </section>
        </div>
    );
}
