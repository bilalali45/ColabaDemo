import React, { useEffect, useContext } from 'react'
import { DocumentActions } from '../../../../store/actions/DocumentActions';
import { Store } from '../../../../store/store';
import { isArray } from 'util';
import { Auth } from '../../../../services/auth/Auth';
import { DocumentsActionType } from '../../../../store/reducers/documentReducer';
import { DocumentRequest } from '../../../../entities/Models/DocumentRequest';

export const DocumentsRequired = () => {

    const { state, dispatch } = useContext(Store);
    const { pendingDocs }: any = state.documents;
    const { currentDoc }: any = state.documents;
    console.log(pendingDocs);

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
        dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: curDoc })
    }

    const renderRequiredDocs = () => {

        if (pendingDocs) {
            return (
                <ul>
                    {
                        pendingDocs.map((pd: DocumentRequest) => {
                            return (
                                <li onClick={() => changeCurrentDoc(pd)}>
                                    <a className={pd.docId === currentDoc?.docId ? 'active' : ''}><span> {pd.docName}</span></a>
                                </li>
                            )
                        })
                    }
                </ul>
            )
        }
        return '';
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
