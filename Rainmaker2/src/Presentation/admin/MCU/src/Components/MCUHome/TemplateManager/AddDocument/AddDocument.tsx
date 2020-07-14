import React, { useState } from 'react'
import Popover from 'react-bootstrap/Popover'
import OverlayTrigger from 'react-bootstrap/OverlayTrigger'
import { DocumentTypes } from './DocumentTypes/DocumentTypes'
import { CommonDocuments } from './SelectedDocumentType/CommonDocuments/CommonDocuments'

export const AddDocument = () => {
    const [popshow, setshow] = useState(false);
    const showpopover = () => {
        setshow(true)
    }
    const popover = (
        <Popover id="popover-basic">
            <Popover.Content>

                <div className="popup-add-doc">
<div className="row">
                    <div className="col-sm-4">
                        <div className="list-doc-cat">
                            <div className="listAll">
                                All
        </div>
                            <ul>
                                <li>Assets</li>
                                <li>{"Credit & risk"}</li>
                                <li>Income</li>
                                <li>Letter of explanation</li>
                                <li>Personal</li>
                                <li>Property</li>
                                <li>Other</li>
                            </ul>
                        </div>
                    </div>
                    <div className="col-sm-8">

                        <div className="pop-detail-doc">

                            <div className="s-wrap">
                                <input type="name" placeholder="Enter follow up name..." />
                            </div>
                            <div className="pop-detail-doc--body">
                                <div className="b-title"><h4>Commonly used</h4></div>

                                <div className="pop-detail-doc--lists">
                                    <ul>
                                        <li>Credit Report</li>
                                        <li>Earnest Money Deposit</li>
                                        <li>Financial Statement</li>
                                        <li>Form 1099</li>
                                        <li>Government-issued ID</li>
                                        <li>Letter of Explanation - General</li>
                                        <li>Mortgage Statement</li>
                                        <li>Paystubs</li>
                                    </ul>
                                </div>

                                <div className="add-custom-doc">
                                    <div className="title-wrap"><h3>Add Custom Document</h3></div>
                                    <div className="input-wrap">
                                    <input type="name" placeholder="Type document name" />
                                    <button className="btn btn-primary btn-sm">Add</button>
                                    </div>
                                </div>
                            </div>


                            </div>
                        </div>
                    </div>
                </div>

            </Popover.Content>
        </Popover>
    );
    return (
        <div className="Compo-add-document">

            <div className="add-doc-link-wrap">
                <OverlayTrigger trigger="click" placement="auto" overlay={popover} >
                    <a className="add-doc-link">
                        Add Document <i className="zmdi zmdi-plus"></i>
                    </a>
                </OverlayTrigger>
            </div>

            {/* <DocumentTypes />
            <CommonDocuments /> */}
        </div>
    )
}
