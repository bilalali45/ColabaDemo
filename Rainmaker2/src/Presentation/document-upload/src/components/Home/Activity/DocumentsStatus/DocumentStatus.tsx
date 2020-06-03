import React from 'react'
import arrowForward from './../../../../assets/images/arrow-forward.svg';

export const DocumentStatus = () => {
    return (
        <div className="DocumentStatus box-wrap">
            <div className="DocumentStatus--header">
                <h2 className="heading-h2">Loan Application</h2>
                <p>You have <span className="count">8</span> items to complete</p>
            </div>
            <div className="DocumentStatus--body">
                <ul className="list">
                    <li>Bank Statement</li>
                    <li>W-2s 2017</li>
                    <li>W-2s 2018</li>
                    <li>Personal Tax Returns</li>
                </ul>
                <div>
                    <a href="" className="DocumentStatus--get-link">Show 4 more Tasks <img src={arrowForward} alt="go" /></a>
                </div>
            </div>

        </div>
    )
}
