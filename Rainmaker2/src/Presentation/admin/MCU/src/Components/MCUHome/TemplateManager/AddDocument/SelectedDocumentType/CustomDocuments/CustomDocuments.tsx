import React, { useState, ChangeEvent } from 'react'
import { nameTest } from '../../../TemplateHome/TemplateHome';
import Spinner from 'react-bootstrap/Spinner';

type CustomDocumentsType = {
    setVisible: Function,
    addDocToTemplate: Function
}

export const CustomDocuments = ({ addDocToTemplate, setVisible }: CustomDocumentsType) => {

    const [docName, setDocName] = useState('');
    const [requestSent, setRequestSent] = useState<boolean>(false);

    const hanldeChange = ({ target: { value } }: ChangeEvent<HTMLInputElement>) => {
        if (!nameTest.test(value)) {
            return;
        }
        setDocName(value);
    }

    const addDoc = async () => {
        setRequestSent(true);
        setDocName('');
        await addDocToTemplate(docName, 'docName');
        setRequestSent(false);
        // setVisible()        
    }

    return (
        <div className="add-custom-doc">
            <div className="s-wrap">
                <h4>Other</h4>
            </div>

            <div className="others-doc-list">
                <div className="active-docs">
                    <ul className="ul-others-doc">
                        {/* <li>Bank Statement</li>
                    <li>W-2s 2017</li>
                    <li>W-2s 2018</li>
                    <li>Personal Tax Returns</li>
                    <li>Tax Transcripts</li>
                    <li>Home Insurance</li>
                    <li>Bank Deposit Slip</li>
                    <li>Alimony Income Verification</li>
                    <li>Bank  statement</li>
                    <li>Pay slip</li> */}
                    </ul>
                </div>
            </div>
            <div className="others-doc-input-wrap">
                <div className="title-wrap"><h3>Add Custom Document</h3></div>
                <div className="input-wrap">

                    <input autoFocus={true} value={docName} onChange={hanldeChange} type="name" placeholder="Type document name" />

                    <div className="input-btn-wrap">
                        {requestSent ? <button className="btn btn-primary btn-sm">
                            <Spinner size="sm" animation="border" role="status">
                            <span className="sr-only">Loading...</span>
                            </Spinner>
                        </button> :
                            <button onClick={addDoc} className="btn btn-primary btn-sm">Add</button>}
                    </div>
                </div>

            </div>
        </div>
    )
}
