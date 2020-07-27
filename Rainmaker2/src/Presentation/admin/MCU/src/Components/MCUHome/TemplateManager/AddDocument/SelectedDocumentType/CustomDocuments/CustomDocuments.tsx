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
    const [requestSent, setRequestSent] = useState<boolean>(false);
    const [isValid, setIsValid] = useState<boolean>(true);

    useEffect(() => {
        setIsValid(true);
    }, [docName === ''])

    const hanldeChange = ({ target: { value } }: ChangeEvent<HTMLInputElement>) => {
        setIsValid(true);
        if (value?.length > 255) {
            setIsValid(false);
        }

        if (!nameTest.test(value)) {
            return;
        }
        setDocName(value);
    }

    const addDoc = async () => {
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

                    <input style={{ border: (isValid ? '' : '1px solid red') }} autoFocus={true} value={docName} onChange={hanldeChange} type="name" placeholder="Type document name" />


                    <div className="input-btn-wrap">
                        {requestSent ? <button className="btn btn-primary btn-sm">
                            <Spinner size="sm" animation="border" role="status">
                                <span className="sr-only">Loading...</span>
                            </Spinner>
                        </button> :
                            <button onClick={addDoc} className="btn btn-primary btn-sm">Add</button>}
                    </div>

                </div>

                {!isValid && <span className={'text-danger'}>Chars length must be less than 255.</span>}
            </div>
        </div>
    )
}
