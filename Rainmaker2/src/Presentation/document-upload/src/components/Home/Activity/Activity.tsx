import React from 'react'
// import { LoanStatus } from './LoanStatus/LoanStatus'
import { LoanProgress } from './LoanProgress/LoanProgress'
import { DocumentStatus } from './DocumentStatus/DocumentStatus'
import { ContactUs } from './ContactUs/ContactUs'
import contactAvatar from '../../../assets/images/contact-avatar-icon.svg';
import { LoanStatus } from './LoanStatus/LoanStatus'

export class Activity extends React.Component {

    render() {
        return (
            <section>
                        <div className="row gutter15">
                            <div className="col-md-6">
                                {/* <LoanStatus /> */}
                                <DocumentStatus/>
                            </div>
                            <div className="col-md-6">
                                <LoanProgress />
                                <ContactUs/>
                            </div>
                        </div>
            </section>
        );
    }
}
