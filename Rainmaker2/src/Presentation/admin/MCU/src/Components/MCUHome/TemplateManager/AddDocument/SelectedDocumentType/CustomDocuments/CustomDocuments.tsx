import React, { useState, ChangeEvent, useEffect } from 'react'
import { nameTest } from '../../../TemplateHome/TemplateHome';
import Spinner from 'react-bootstrap/Spinner';
import { CategoryDocument } from '../../../../../../Entities/Models/CategoryDocument';

type CustomDocumentsType = {
    setVisible: Function,
    addDocToTemplate: Function
}

export const CustomDocuments = ({ addDocToTemplate, setVisible }: CustomDocumentsType) => {

    const [docName, setDocName] = useState('');
    const [docNameError, setDocNameError] = useState('');
    const [requestSent, setRequestSent] = useState<boolean>(false);
    const [isValid, setIsValid] = useState<boolean>(true);


    useEffect(() => {
        setIsValid(true);
    }, [docName === '']);

    useEffect(() => {
        if (!nameTest.test(docName)) {
            console.log('in here you know where ...');
            setDocNameError('Document name cannot contain any special characters');
        }else {
            setDocNameError('');
        }

        if (!docName?.trim()?.length) {
            setDocNameError('');
        }
    }, [docName]);

    const hanldeChange = ({ target: { value } }: ChangeEvent<HTMLInputElement>) => {
        setIsValid(true);
        if (value?.length > 255) {
            setIsValid(false);
        }

        setDocName(value);
    }

    const addDoc = async () => {
        if (!nameTest.test(docName)) {
            return;
        }

        if (!docName.trim()?.length) {
            setDocNameError('Document name cannot be empty');
            setIsValid(false);
            return;
        }
        if (isValid) {
            setRequestSent(true);
            await addDocToTemplate(docName, 'docName');
            setDocName('');
            setRequestSent(false);
        }
        // setVisible()        
    }

    return (
        <div className="add-custom-doc">

            <div className="others-doc-input-wrap">
                <div className="title-wrap"><h3>Add Custom Document</h3></div>
                <div className="input-wrap">

                    <input maxLength={255} onKeyDown={(e: any) => {
                        if (e.keyCode === 13) {
                            addDoc();
                        }
                    }} style={{ border: (isValid ? '' : '1px solid red') }} autoFocus={true} value={docName} onChange={hanldeChange} type="name" placeholder="Type document name" />


                    <div className="input-btn-wrap">
                        {requestSent ? <button className="btn btn-primary btn-sm">
                            <Spinner size="sm" animation="border" role="status">
                                <span className="sr-only">Loading...</span>
                            </Spinner>
                        </button> :
                            <button onClick={addDoc} className="btn btn-primary btn-sm">Add</button>}
                    </div>

                </div>

                {docNameError && <span className={'text-danger'}>{docNameError}</span>}
            </div>
        </div>
    )
}
