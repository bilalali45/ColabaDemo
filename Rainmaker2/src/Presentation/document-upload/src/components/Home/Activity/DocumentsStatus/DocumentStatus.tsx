import React, { useEffect, useContext, useState } from 'react'
import { SVG } from './../../../../shared/Components/Assets/SVG';
import { Link, useHistory } from 'react-router-dom';
import { DocumentActions } from '../../../../store/actions/DocumentActions';
import { Store } from '../../../../store/store';
import { DocumentsActionType } from '../../../../store/reducers/documentReducer';

export const DocumentStatus = () => {

    const [pendingDocs, setPendingDocs] = useState([]);

    const history = useHistory();


    useEffect(() => {
        if (!pendingDocs.length) {
            fetchPendingDocs();
        }
    }, []);

    const fetchPendingDocs = async () => {
        let docs: any = await DocumentActions.getPendingDocuments('1', '1');
        if (docs) {
            setPendingDocs(docs);
        }
    }

    const getStarted = () => {
        history.push('/documentsRequest');
    }

    if (pendingDocs) {

        return (
            <div className="DocumentStatus box-wrap">
                <div className="row">
                    <div className="col-md-7 DocumentStatus--left-side">
                        <div className="box-wrap--header">
                            <h2 className="heading-h2"> Document Request </h2>
                            <p>You have <span className="DocumentStatus--count">{pendingDocs?.length}</span> items to complete</p>
                        </div>
                        <div className="box-wrap--body">
                            <ul className="list">
                                {pendingDocs.map((item: any) => {
                                    return <li> {item.docName} </li>
                                })}
                            </ul>
                            {pendingDocs.length > 4 && <div>
                                <a href='' className="DocumentStatus--get-link">Show {pendingDocs.length - 4} more Tasks <SVG shape="arrowFarword" /></a>
                            </div>}
                        </div>
                    </div>
                    <div className="col-md-5 DocumentStatus--right-side">
                        <SVG shape="storage" />
                        <button onClick={getStarted} className="btn btn-primary float-right">Get Started</button>
                    </div>
                </div>
            </div>
        )

    }

    return <h1>...loading...</h1>

}
