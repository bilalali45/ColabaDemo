import React, { useState, useEffect } from 'react'
import { SVGstorage } from '../../../../shared/Components/Assets/SVG';
import { Link, useHistory } from 'react-router-dom';
import { DocumentActions } from '../../../../store/actions/DocumentActions';
import { Endpoints } from '../../../../store/endpoints/Endpoints';
import { Auth } from '../../../../services/auth/Auth';

export const DocumentStatus = () => {

    const [pendingDocs, setPendingDocs] = useState([])

    const history = useHistory();


    useEffect(() => {
        console.log('in here!!@!');

        if(!pendingDocs.length) {
            fetchPendingDocs();
        }
    }, []);

    const getStarted = () => {
        history.push('/documentsRequest');
    }
    
    const fetchPendingDocs = async () => {

        let docsPending = await DocumentActions.getPendingDocuments(Auth.getLoanAppliationId(), Auth.getTenantId());
        console.log('docsPending', docsPending);
        if(docsPending) {
            setPendingDocs(docsPending);
        }
    }

    // heading="Document Request"
    // counts={8}
    // moreTask="/#"
    // getStarted="/#"
    // tasks={this.state.tasks

    if(!pendingDocs) {
        return <p>...loading...</p>
    }

    return (
        <div className="DocumentStatus box-wrap">
            <div className="box-wrap--header clearfix">
                <h2 className="heading-h2"> Document Request</h2>
                <p>You have <span className="DocumentStatus--count">{pendingDocs.length}</span> items to complete</p>
            </div>
            <div className="box-wrap--body clearfix">
                <ul className="list">
                    {pendingDocs.map((item: any,index:any) => {
                        return <li key={index}> {item.docName} </li>
                    })}
                </ul>
            </div>
            <div className="box-wrap--footer clearfix">
                <button onClick={getStarted} className="btn btn-primary float-right">Get Start <em className="zmdi zmdi-arrow-right"></em></button>
            </div>
        </div>
    )
}
