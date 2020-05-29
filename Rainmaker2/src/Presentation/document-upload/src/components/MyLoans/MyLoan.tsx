import React from 'react';
import { Switch, Route } from 'react-router-dom';

const MyLoan = () => {
    return(
        <div className="my-loan-widget">
            <div className="my-loan-widget--body">
                <ul>
                    <li>
                        <h2 className="heading-h2">Loan Application</h2>
                    </li>
                    <li>
                        <div className="flex">
                            <div className="flex--side">
                                <h4 className="my-loan-widget--heading">Loan Purpose</h4>
                                <p className="my-loan-widget--text">Purchase a home</p>
                            </div>
                            <div className="flex--side">
                                <h4 className="my-loan-widget--heading">Property Type</h4>
                                <p className="my-loan-widget--text">Single Family Detached</p>
                            </div>
                        </div>
                    </li>
                    <li>
                        <h5 className="my-loan-widget--heading">Property Address</h5>
                        <p className="my-loan-widget--text">7951 Northeast Bayshore Court, Miami, FL, USA</p>
                    </li>
                    <li>
                        <h5 className="my-loan-widget--heading">Loan Amount</h5>
                        <p className="my-loan-widget--text">$40,000</p>
                    </li>
                </ul>
            </div>
        </div>
    );
}

export default MyLoan;