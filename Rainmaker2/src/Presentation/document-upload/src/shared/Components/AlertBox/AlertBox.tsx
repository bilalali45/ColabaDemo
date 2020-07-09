import React, { useState, useContext } from 'react'
import { Store } from '../../../store/store';
import { DocumentsActionType } from '../../../store/reducers/documentReducer';
import { DocumentRequest } from '../../../entities/Models/DocumentRequest';
import { update } from 'lodash';
import { useHistory } from 'react-router-dom';


type AlertBoxType = {
    hideAlert: Function,
    triedSelected?: any,
    navigateUrl?: string
}

export const AlertBox = ({ hideAlert, triedSelected, navigateUrl }: AlertBoxType) => {

    const history = useHistory();

    const { state, dispatch } = useContext(Store);
    const { pendingDocs, currentDoc }: any = state.documents;

    const yesHandler = () => {
        try {
            let updatedFiles = currentDoc.files.filter(f => {
                if (f.uploadStatus !== 'pending') {
                    return f;
                } else {
                    f.uploadReqCancelToken.cancel();
                }
            });
            dispatch({ type: DocumentsActionType.AddFileToDoc, payload: updatedFiles });
            dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: triedSelected });
            if (navigateUrl) {
                history.push(navigateUrl)
            }
            hideAlert();

        } catch (error) {
            console.log(error);
        }
    }
    const noHandler = () => {
        hideAlert();
    }


    return (
        <div className="alert-box" id="AlertBox" data-component="AlertBox">
            <div className="alert-box--modal">
                {/* <button className="alert-box--modal-close" onClick={() => { hideAlert() }} ><em className="zmdi zmdi-close"></em></button> */}
                {/* <header className="alert-box--modal-header">
                    <h2 className="text-center">Are you sure, you want to proceed!</h2>
                </header> */}
                <section className="alert-box--modal-body">
                    <div className="alert-box--modal-body-content">
                    <p>Some files are still in progess, please complete them before navigating away!</p>
                    <p>If you select yes, all selected files will be lost!</p>
                    </div>
                </section>
                <footer className="alert-box--modal-footer">
                    <div className="text-center">
                        <button onClick={yesHandler} className="btn btn-secondary ">Yes</button>
                        <button onClick={noHandler} className="btn btn-primary">No</button>
                    </div>
                </footer>
            </div>
        </div>
    )
}
