import React from 'react'

export const CustomDocuments = () => {
    return (
        <div className="add-custom-doc">
            <div className="s-wrap">
                <h4>Other</h4>
            </div>

            <div className="others-doc-list">
                <div className="active-docs"><ul>
                    <li>Bank Statement</li>
                    <li>W-2s 2017</li>
                    <li>W-2s 2018</li>
                    <li>Personal Tax Returns</li>
                    <li>Tax Transcripts</li>
                    <li>Home Insurance</li>
                    <li>Bank Deposit Slip</li>
                    <li>Alimony Income Verification</li>
                    <li>Bank  statement</li>
                    <li>Pay slip</li>
                </ul>
                </div>
            </div>
            <div className="others-doc-input-wrap">
            <div className="title-wrap"><h3>Add Custom Document</h3></div>
            <div className="input-wrap">
               
                <input type="name" placeholder="Type document name" />
                
                <div className="input-btn-wrap">
                <button className="btn btn-secondary btn-sm">Cancel</button>
                <button className="btn btn-primary btn-sm">Add</button>
                </div>
            </div>

</div>
        </div>
    )
}
