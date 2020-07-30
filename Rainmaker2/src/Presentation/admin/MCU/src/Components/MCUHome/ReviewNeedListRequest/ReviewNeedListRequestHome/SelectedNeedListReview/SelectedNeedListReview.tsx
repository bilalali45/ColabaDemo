import React from 'react'
import { TemplateDocument } from '../../../../../Entities/Models/TemplateDocument'

type SelectedNeedListReviewProps = {
    documentList: TemplateDocument[];
}

export const SelectedNeedListReview = ({documentList}:SelectedNeedListReviewProps) => {

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
                <h2 className="h2">Review Needs List</h2>
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
