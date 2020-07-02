import React from 'react'

export const LoanSnapshot = () => {
    return (
        <div className="loansnapshot">
            <div className="row">
                <div className="col-md-9">
                    <ul>
                        <li>
                            <label className="mcu-label">Loan No.</label>
                            <span className="mcu-label-value">293893033989</span>
                        </li>
                        <li>
                            <label className="mcu-label">Property Address</label>
                            <span className="mcu-label-value">727 Ashleigh LN # 222 Dallas, TX 76099</span>
                        </li>
                        <li>
                            <label className="mcu-label">Est. Closing Date</label>
                            <span className="mcu-label-value">05-10-2020</span>
                        </li>
                        <li>
                            <label className="mcu-label">Loan Purpose</label>
                            <span className="mcu-label-value">Refinance</span>
                        </li>
                        <li>
                            <label className="mcu-label">Property Value</label>
                            <span className="mcu-label-value"><sup className="text-primary">$</sup>10,000</span>
                        </li>
                        <li>
                            <label className="mcu-label">Loan Amount</label>
                            <span className="mcu-label-value"><sup className="text-primary">$</sup>80,000</span>
                        </li>
                        <li>
                            <label className="mcu-label">Primary & co-Borrower</label>
                            <span className="mcu-label-value">Richard Glenn Randall, Maria Randall</span>
                        </li>
                        <li>
                            <label className="mcu-label">Milestone/Status</label>
                            <span className="mcu-label-value">Review By Team</span>
                        </li>
                        <li>
                            <label className="mcu-label">Property type</label>
                            <span className="mcu-label-value">Single Family Detached</span>
                        </li>
                        <li>
                            <label className="mcu-label">Rate</label>
                            <span className="mcu-label-value">2.875<sup className="text-primary">%</sup></span>
                        </li>
                        <li>
                            <label className="mcu-label">Loan Program</label>
                            <span className="mcu-label-value">5 <span className="text-wrap"><small>Year</small><small className="text-primary">ARM</small></span> </span>
                        </li>
                    </ul>
                </div>
                <div className="col-md-3">

                </div>
            </div>
        </div>
    )
}
