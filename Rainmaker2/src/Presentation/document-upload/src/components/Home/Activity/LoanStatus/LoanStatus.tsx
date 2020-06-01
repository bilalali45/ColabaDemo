import React, {useState} from 'react'


 export const LoanStatus = () => {
    return (
        <div className="LoanStatus">
            <div className="LoanStatus--body">
                <ul>
                    <li>
                        <h2 className="heading-h2">Loan Application</h2>
                    </li>
                    <li>
                        <div className="flex">
                            <div className="flex--side">
                                <h4 className="LoanStatus--heading">Loan Purpose</h4>
                                <p className="LoanStatus--text">Purchase a home</p>
                            </div>
                            <div className="flex--side">
                                <h4 className="LoanStatus--heading">Property Type</h4>
                                <p className="LoanStatus--text">Single Family Detached</p>
                            </div>
                        </div>
                    </li>
                    <li>
                        <h5 className="LoanStatus--heading">Property Address</h5>
                        <p className="LoanStatus--text">7951 Northeast Bayshore Court, Miami, FL, USA</p>
                    </li>
                    <li>
                        <h5 className="LoanStatus--heading">Loan Amount</h5>
                        <p className="LoanStatus--text">$40,000</p>
                    </li>
                </ul>
            </div>
        </div>
    )
}
