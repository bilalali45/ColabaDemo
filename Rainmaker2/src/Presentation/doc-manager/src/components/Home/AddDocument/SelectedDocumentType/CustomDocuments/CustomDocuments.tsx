import React, { useState, ChangeEvent, useEffect, useContext } from 'react'
import Spinner from 'react-bootstrap/Spinner';
import { kMaxLength } from 'buffer';
import { AddFileToDoc } from '../../AddFileToDoc/AddFileToDoc';
import { Store } from '../../../../../Store/Store';

type CustomDocumentsType = {
    setVisible: Function,
    addDocToTemplate: Function
}

export const CustomDocuments = ({ addDocToTemplate, setVisible }: CustomDocumentsType) => {

    const [docName, setDocName] = useState('');
    const [docNameError, setDocNameError] = useState('');
    const [requestSent, setRequestSent] = useState<boolean>(false);
    const [isValid, setIsValid] = useState<boolean>(true);
    const [addFileDialog, setAddFileDialog] = useState<boolean>(false);
    const [newDoc, setNewDoc] = useState<any>();
    const { state, dispatch } = useContext(Store);
    const {
        documentItems
    
      }: any = state.documents;

    useEffect(() => {
        setIsValid(true);
    }, [docName === '']);


    const hanldeChange = ({ target: { value } }: ChangeEvent<HTMLInputElement>) => {
        setIsValid(true);
        setDocNameError('')
        if (value?.length > 50) {
            setIsValid(false);
            // setDocNameError('Only 50 chars allowed'); 
            
        }

        setDocName(value);
    }

    const addDoc = async () => {
        // if (!nameTest.test(docName)) {
        //     return;
        // }

        if (!docName.trim()?.length) {
            setDocNameError('Document name cannot be empty');
            setIsValid(false);
            return;
        }
        if (isValid) {
            setRequestSent(true);
            let newDoc = {
                docTypeId: '',
                docType: docName,
                docMessage: '',
                isCustom: true
            }
            setNewDoc(docName)
            await addDocToTemplate(newDoc, 'docName');
            
            setDocName('');
            setAddFileDialog(true)
            setRequestSent(false);
        }
        // setVisible()        
    }

    
    return (
        <div className="add-custom-doc">

            <div className="others-doc-input-wrap">
                <div className="title-wrap"><h3>Add Custom Document</h3></div>
                <div className="input-wrap">

                    <input maxLength={50} onKeyDown={(e: any) => {
                        if (e.keyCode === 9) {
                            e.preventDefault()
                            return;
                        }
                        if (e.keyCode === 13) {
                            addDoc();
                        }
                    }} className={!docNameError ? '' : 'error'} autoFocus={true} value={docName} onChange={hanldeChange} type="name" placeholder="Type document name" />


                    <div className="input-btn-wrap">
                        {requestSent ?
                            <button className="btn btn-primary btn-sm btn-loading">
                                <Spinner size="sm" animation="border" role="status">
                                    <span className="sr-only">Loading...</span>
                                </Spinner>
                                <span className="btn-text">Add</span>
                            </button>
                            :
                            <button onClick={addDoc} className="btn btn-primary btn-sm">
                                <span className="btn-text">Add</span>
                            </button>
                        }
                    </div>

                </div>

                {docNameError && <label className={'error'}>{docNameError}</label>}
            </div>
            {console.log(newDoc)}
            {addFileDialog && 
      <AddFileToDoc 
      selectedDocTypeId = {""}
      showFileDialog = {addFileDialog}
      setVisible={setVisible}
      setAddFileDialog = {setAddFileDialog}
      retryFile = {null}
      selectedDocName = {newDoc}/>
      }
        </div>
    )
}
