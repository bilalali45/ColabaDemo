import React, { useEffect, useContext } from 'react'
import { DocumentActions } from '../../../../store/actions/DocumentActions';
import { Store } from '../../../../store/store';
import { isArray } from 'util';
import { Auth } from '../../../../services/auth/Auth';
import { DocumentsActionType } from '../../../../store/reducers/documentReducer';
import { DocumentRequest } from '../../../../entities/Models/DocumentRequest';
import { Redirect } from 'react-router-dom';

export const DocumentsRequired = () => {

    const { state, dispatch } = useContext(Store);
    const { pendingDocs }: any = state.documents;
    const { currentDoc }: any = state.documents;

    useEffect(() => {
        if (pendingDocs?.length) {
            setCurrentDoc(pendingDocs[0]);
        }
        fetchPendingDocs();
    }, []);

    const setCurrentDoc = (doc) => {
        dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: doc });

    }

    const fetchPendingDocs = async () => {
        if (!pendingDocs) {
            let docs = await DocumentActions.getPendingDocuments(Auth.getLoanAppliationId(), Auth.getTenantId());
            if (docs) {
                dispatch({ type: DocumentsActionType.FetchPendingDocs, payload: docs });
                setCurrentDoc(docs[0])
            }

        }
    }


    const changeCurrentDoc = (curDoc: DocumentRequest) => {
        let uploadInProgress = currentDoc?.files?.find(f => f.uploadProgress > 0 && f.uploadStatus !== 'done')
        if (!uploadInProgress) {
            dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: curDoc })
        } else {
            alert('please wait for the files to finish before nevavigating!');
        }
    }


    const renderRequiredDocs = () => {
        if (pendingDocs) {
            return (

                <ul>
                    {
                        pendingDocs.map((pd: DocumentRequest) => {
                            return (
                                <li onClick={() => changeCurrentDoc(pd)}>
                                    <a className={currentDoc && pd.docId === currentDoc.docId ? 'active' : ''}><span> {pd.docName}</span></a>
                                </li>
                            )
                        })
                    }
                </ul>
            )
        }
        return '';
    }

    if (pendingDocs?.length === 0) {
        return <Redirect to="activity" />
    }


    return (
        <div className="dr-list-wrap">
            <nav>
                {
                    pendingDocs && renderRequiredDocs()
                }
            </nav>

        </div>
    )
}
