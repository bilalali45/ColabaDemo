import React, { useState, useEffect, useContext } from 'react'
import { Link, useHistory } from 'react-router-dom';
import { DocumentActions } from '../../../../store/actions/DocumentActions';
import { Endpoints } from '../../../../store/endpoints/Endpoints';
import { Auth } from '../../../../services/auth/Auth';
import IconEmptyDocRequest from '../../../../assets/images/empty-doc-req-icon.svg';
import { Store } from '../../../../store/store';
import { DocumentsActionType } from '../../../../store/reducers/documentReducer';
import { DocumentRequest } from '../../../../entities/Models/DocumentRequest';
import { Loader } from '../../../../shared/Components/Assets/loader';

export const DocumentsStatus = () => {

    const [pendingDocs, setPendingDocs] = useState<DocumentRequest[] | null>(null)
    const { state, dispatch } = useContext(Store);

    const history = useHistory();


    useEffect(() => {

        if (!pendingDocs?.length) {
            fetchPendingDocs();
        }
    }, []);

    const getStarted = () => {
        history.push('/documentsRequest');
    }

    const fetchPendingDocs = async () => {

        let docsPending = await DocumentActions.getPendingDocuments(Auth.getLoanAppliationId(), Auth.getTenantId());
        if (docsPending) {
            dispatch({ type: DocumentsActionType.FetchPendingDocs, payload: docsPending });
            setPendingDocs(docsPending);
        }
    }

    const renderNoPendingDocs = () => {
        return (
            <div className="DocumentStatus box-wrap empty">
                <div className="box-wrap--header clearfix">
                    <h2 className="heading-h2"> Document Request</h2>
                </div>
                <div className="box-wrap--body clearfix">
                    <div className="edc-flex">
                        <div className="eds-wrap">
                            <div className="eds-img">
                                <img src={IconEmptyDocRequest} alt="" />
                            </div>
                            <div className="eds-txt">
                                <p>
                                    You have 0 task to complete
                            </p>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        )
    }

    if (!pendingDocs) {
        return <Loader containerHeight={"476px"}  />
    }

    if (pendingDocs.length == 0) {
        renderNoPendingDocs();
    }

    return (
        <div className="DocumentStatus hasData box-wrap">
            <div className="box-wrap--header clearfix">
                <h2 className="heading-h2"> Document Request</h2>
                <p>You have <span className="DocumentStatus--count">{pendingDocs.length}</span> items to complete</p>
            </div>
            <div className="box-wrap--body clearfix">
                <ul className="list">
                    {pendingDocs.map((item: any, index: any) => {
                        return <li key={index}> {item.docName} </li>
                    })}
                </ul>
            </div>
            <div className="box-wrap--footer clearfix">
                <button className="btn btn-primary float-right">Get Start <em className="zmdi zmdi-arrow-right"></em></button>
                {/* <button onClick={getStarted} className="btn btn-primary float-right">Get Start <em className="zmdi zmdi-arrow-right"></em></button> */}
            </div>
        </div>
    )

}
