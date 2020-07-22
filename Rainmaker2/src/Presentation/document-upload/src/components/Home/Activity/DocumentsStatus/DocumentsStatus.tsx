import React, { useState, useEffect, useContext } from 'react'
import { Link, useHistory } from 'react-router-dom';
import { DocumentActions } from '../../../../store/actions/DocumentActions';
import { Endpoints } from '../../../../store/endpoints/Endpoints';
import { Auth } from '../../../../services/auth/Auth';
import IconEmptyDocRequest from '../../../../assets/images/empty-doc-req-icon.svg';
import IconDoneDocRequest from '../../../../assets/images/done-doc-req-icon.svg';
import { Store } from '../../../../store/store';
import { DocumentsActionType } from '../../../../store/reducers/documentReducer';
import { DocumentRequest } from '../../../../entities/Models/DocumentRequest';
import { Loader } from '../../../../shared/Components/Assets/loader';

export const DocumentsStatus = () => {

    const { state, dispatch } = useContext(Store);

    const history = useHistory();

    const { pendingDocs, submittedDocs }: any = state.documents;

    useEffect(() => {

        if (!pendingDocs?.length) {
            fetchPendingDocs();
        }

        if (!submittedDocs?.length) {
            fetchSubmittedDocs();
        }
    }, []);

    const getStarted = () => {
        history.push('/documentsRequest');
    }

    const fetchPendingDocs = async () => {

        let docsPending = await DocumentActions.getPendingDocuments(Auth.getLoanAppliationId(), Auth.getTenantId());
        if (docsPending) {
            dispatch({ type: DocumentsActionType.FetchPendingDocs, payload: docsPending });
            dispatch({ type: DocumentsActionType.SetCurrentDoc, payload: docsPending[0] });
            // setPendingDocs(docsPending);
        }
    }

    const fetchSubmittedDocs = async () => {
        let submittedDocs = await DocumentActions.getSubmittedDocuments(Auth?.getLoanAppliationId(), Auth?.getTenantId());
        if (submittedDocs) {
            dispatch({ type: DocumentsActionType.FetchSubmittedDocs, payload: submittedDocs })
        }
    }

    const renderNoPendingDocs = () => {
        return (
            <div className="DocumentStatus box-wrap empty">
                <div className="box-wrap--header clearfix">
                    <h2 className="heading-h2"> Task List</h2>
                </div>
                <div className="box-wrap--body clearfix">
                    <div className="edc-flex">
                        <div className="eds-wrap">
                            <div className="eds-img">
                                <img src={IconEmptyDocRequest} alt="" />
                            </div>
                            <div className="eds-txt">
                                <p>
                                    You have 0 tasks to complete.
                            </p>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        )
    }

    const renderCompletedDocs = () => {
        return (
            <div className="DocumentStatus box-wrap empty">
                <div className="box-wrap--header clearfix">
                    <h2 className="heading-h2">Task List</h2>
                </div>
                <div className="box-wrap--body clearfix">
                    <div className="edc-flex">
                        <div className="eds-wrap">
                            <div className="eds-img">
                                <img src={IconDoneDocRequest} alt="" />
                            </div>
                            <div className="eds-txt">
                                <p>
                                You’ve completed all tasks for now!<br />We’ll let you know if we need anything else.
                            </p>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        )
    }

    if (!pendingDocs) {
        return <Loader containerHeight={"476px"} />
    }

    if (submittedDocs?.length && !pendingDocs?.length) {
        return renderCompletedDocs();
    }

    if (pendingDocs.length == 0) {
         return renderNoPendingDocs();
    }

    return (
        <div className="DocumentStatus hasData box-wrap">
            <div className="overlay-DocumentStatus"> 
            <div className="box-wrap--header clearfix">
                <h2 className="heading-h2"> Task List</h2>
                <p>You have <span className="DocumentStatus--count">{pendingDocs.length}</span> items to complete</p>
            </div>
            <div className="box-wrap--body clearfix">
                <ul className="list">
                    {pendingDocs.map((item: any, index: any) => {
                        if (index < 8)
                            return <li title={item.docName} key={index}> {item.docName} </li>
                    })}
                </ul>
            </div>
            <div className="box-wrap--footer clearfix">
                {/* <button className="btn btn-primary float-right">Get Start <em className="zmdi zmdi-arrow-right"></em></button> */}
                <button onClick={getStarted} className="btn btn-primary float-right">Get Started <em className="zmdi zmdi-arrow-right"></em></button>
            </div>
            </div>
        </div>
    )

}
