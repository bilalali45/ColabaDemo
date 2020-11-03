import React, { useState, useEffect, useContext } from 'react'
import { LoanProgress } from './LoanProgress/LoanProgress'
import { DocumentsStatus } from './DocumentsStatus/DocumentsStatus'
import { ContactUs } from './ContactUs/ContactUs'
import contactAvatar from '../../../assets/images/contact-avatar-icon.svg';
import { LoanStatus } from './LoanStatus/LoanStatus'
import { StoreProvider, Store } from "../../../store/store";

export class Activity extends React.Component {
    render() {
        return (
            <div className="activity-Page">
                <Store.Consumer>
                    {(store: any) => {
                        let loan = store.state.loan;
                        let isMobile = loan.isMobile;
                        return (
                            <React.Fragment>
                                {isMobile.value && <section className="compo-loan-status compo-loan-status-mobile"><LoanStatus /></section>}
                            </React.Fragment>
                        )
                    }}
                </Store.Consumer>
                <section>
                    <div className="row gutter15">
                        <div className="col-md-6">
                            {/* <LoanStatus /> */}
                            <DocumentsStatus />

                        </div>
                        <div className="col-md-6">
                            <LoanProgress />
                            <ContactUs />
                        </div>
                    </div>
                </section>
            </div>
        );
    }
}
