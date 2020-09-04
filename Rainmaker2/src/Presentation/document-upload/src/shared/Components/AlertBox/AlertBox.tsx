import React, { useState, useContext } from 'react'
import { Store } from '../../../store/store';
import { DocumentsActionType } from '../../../store/reducers/documentReducer';
import { DocumentRequest } from '../../../entities/Models/DocumentRequest';
import { update } from 'lodash';
import { useHistory } from 'react-router-dom';


type AlertBoxType = {
    hideAlert: Function,
    triedSelected?: any,
    navigateUrl?: string,
    isBrowserBack?: boolean
}

export const AlertBox = ({ hideAlert, triedSelected, navigateUrl, isBrowserBack }: AlertBoxType) => {

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
            if(triedSelected) {
          
            dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: triedSelected });

            }else {
            // debugger
            if (navigateUrl) {
                history.push(navigateUrl);
            }else if(!isBrowserBack || !navigateUrl) {
                history.goBack();
            }

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
                <section className="alert-box--modal-body">
                    <div className="alert-box--modal-body-content">
                    <p>Are you sure you want to leave?</p>
                    <p>The files that have not been submitted will be lost.</p>
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