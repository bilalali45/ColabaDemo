import React, { useEffect } from 'react'
import { TemplateDocument } from '../../../../../Entities/Models/TemplateDocument'
import Spinner from 'react-bootstrap/Spinner';
import { useHistory } from 'react-router-dom';
import { LocalDB } from '../../../../../Utils/LocalDB';

type SelectedNeedListReviewProps = {
    documentList: TemplateDocument[];
}

export const SelectedNeedListReview = ({documentList}:SelectedNeedListReviewProps) => {

    const history = useHistory()

    useEffect(() => {
        history.push(`/newNeedList/${LocalDB.getLoanAppliationId()}`);
    }, [!documentList?.length])

    if(!documentList){
        return (
            <div className="loader-widget loansnapshot">
              <Spinner animation="border" role="status">
                <span className="sr-only">Loading...</span>
              </Spinner>
            </div>
          );
    }

    const displayRequestDocumentsList = () => {
        return (
            <>
                <div className="listing">
                    <ul>
                        {
                         documentList?.map((t: TemplateDocument) => {
                         return  <li><a>{t.docName}</a></li>
                         })
                        }
                    </ul>
                </div>
            </>
        )
    }

    return (
        <div className="mcu-panel-body--aside">
            <header className="mcu-panel-header">
                <h3 className="h3">Review Needs List</h3>
            </header>

            <div className="mcu-panel-body padding">
                {displayRequestDocumentsList()}
            </div>

            <footer className="mcu-panel-footer">
                &nbsp;
            </footer>
        </div>
    )
}
